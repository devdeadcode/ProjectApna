using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Actions;
using eOne.Common.Connectors.Service;
using eOne.Common.Connectors.ZenPayroll.Models;
using eOne.Common.Helpers;
using eOne.Common.Query;

namespace eOne.Common.Connectors.ZenPayroll
{
    public class ZenPayrollConnector : RestConnector
    {
        
        public const int EntityIdEmployee = 1;
        public const int EntityIdPayroll = 2;
        public const int EntityIdPayrollDetail = 3;

        public ZenPayrollConnector()
        {
            Name = "Gusto";
            Group = ConnectorGroup.Payroll;
            AuthenticationType = ServiceConnectorAuthenticationType.OAuth2;
            Multicompany = true;
            CompanyPrompt = "Company";
            CompanyPluralPrompt = "Companies";
            BaseUrl = "https://zenpayroll.com/api/v1/";
        }

        public override void Initialise()
        {
            base.Initialise();
            // todo - add companies
            AddEntity(EntityIdEmployee, "Employees", typeof(ZenPayrollEmployee));
            AddEntity(EntityIdPayroll, "Payrolls", typeof(ZenPayrollPayroll));
            AddEntity(EntityIdPayrollDetail, "Payroll details", typeof(ZenPayrollPayrollDetail));
            // todo - add relationships
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdEmployee:
                    return "companies/{0}/employees";
                case EntityIdPayroll:
                case EntityIdPayrollDetail:
                    return "companies/{0}/payrolls";
            }
            return string.Empty;
        }

        public override List<Tuple<string, string>> GetParameters(ConnectorQuery query)
        {
            var parameters = new List<Tuple<string, string>>();
            switch (query.Entity.Id)
            {
                case EntityIdEmployee:
                    if (query.HasBooleanRestriction("terminated")) TupleHelper.AppendTupleStringList(parameters, "terminated", "true");
                    if (query.HasBooleanRestriction("terminated", false)) TupleHelper.AppendTupleStringList(parameters, "terminated", "false");
                    break;
                case EntityIdPayroll:
                case EntityIdPayrollDetail:
                    // add processed parameter
                    if (query.HasBooleanRestriction("processed")) TupleHelper.AppendTupleStringList(parameters, "processed", "true"); 
                    if (query.HasBooleanRestriction("processed", false)) TupleHelper.AppendTupleStringList(parameters, "processed", "false");

                    // add start date parameters
                    var restriction = query.FindRestrictionByFieldAndType("start_date", ConnectorRestriction.ConnectorRestrictionType.GreaterThanOrEqualTo);
                    if (restriction != null) TupleHelper.AppendTupleStringList(parameters, "start_date", restriction.Values[0].DateConstant.ToString("yyyy-MM-dd"));
                    restriction = query.FindRestrictionByFieldAndType("start_date", ConnectorRestriction.ConnectorRestrictionType.GreaterThan);
                    if (restriction != null) TupleHelper.AppendTupleStringList(parameters, "start_date", restriction.Values[0].DateConstant.AddDays(1).ToString("yyyy-MM-dd"));

                    // add end date parameters
                    restriction = query.FindRestrictionByFieldAndType("end_date", ConnectorRestriction.ConnectorRestrictionType.LessThanOrEqualTo);
                    if (restriction != null) TupleHelper.AppendTupleStringList(parameters, "end_date", restriction.Values[0].DateConstant.ToString("yyyy-MM-dd"));
                    restriction = query.FindRestrictionByFieldAndType("end_date", ConnectorRestriction.ConnectorRestrictionType.LessThan);
                    if (restriction != null) TupleHelper.AppendTupleStringList(parameters, "end_date", restriction.Values[0].DateConstant.AddDays(-1).ToString("yyyy-MM-dd"));
                    break;
            }
            return parameters;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdEmployee:
                    return DeserializeJson<List<ZenPayrollEmployee>>(data);
                case EntityIdPayroll:
                    return DeserializeJson<List<ZenPayrollPayroll>>(data);
                case EntityIdPayrollDetail:
                    var details = new List<ZenPayrollPayrollDetail>();
                    var payrolls = DeserializeJson<List<ZenPayrollPayroll>>(data);
                    foreach (var payroll in payrolls)
                    {
                        foreach (var employee in payroll.employee_compensations)
                        {
                            details.AddRange(employee.fixed_compensations.Select(compensation => new ZenPayrollPayrollDetail(payroll, employee.Employee, compensation, ZenPayrollPayrollDetail.ZenPayrollPayrollDetailType.Fixed)));
                            details.AddRange(employee.hourly_compensations.Select(compensation => new ZenPayrollPayrollDetail(payroll, employee.Employee, compensation, ZenPayrollPayrollDetail.ZenPayrollPayrollDetailType.Hourly)));
                            details.AddRange(employee.paid_time_off.Select(compensation => new ZenPayrollPayrollDetail(payroll, employee.Employee, compensation, ZenPayrollPayrollDetail.ZenPayrollPayrollDetailType.PaidTimeOff)));
                        }
                    }
                    return details;
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {

        }

    }
}
