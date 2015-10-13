using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Connectors.ZenPayroll.Models;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.Connectors.ZenPayroll
{
    public class ZenPayrollConnector : RestConnector
    {
        
        public const int EntityIdEmployee = 1;
        public const int EntityIdPayroll = 2;
        public const int EntityIdPayrollDetail = 3;

        public ZenPayrollConnector()
        {
            Name = "ZenPayroll";
            Group = ConnectorGroup.Payroll;
            AuthenticationType = RestConnectorAuthenticationType.OAuth2;
            Multicompany = true;
            CompanyPrompt = "Company";
            CompanyPluralPrompt = "Companies";
            BaseUrl = "https://zenpayroll.com/api/v1/";
        }

        public override void Initialise()
        {
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
                    return GetEmployeeEndpoint(query);
                case EntityIdPayroll:
                case EntityIdPayrollDetail:
                    return GetPayrollEndpoint(query);
            }
            return string.Empty;
        }

        public string GetEmployeeEndpoint(ConnectorQuery query)
        {
            var endpoint = new ConnectorEndpoint("companies/{0}/employees");
            if (query.HasBooleanRestriction("terminated")) endpoint.AddParameter("terminated", "true");
            if (query.HasBooleanRestriction("terminated", false)) endpoint.AddParameter("terminated", "false");
            return endpoint.ToString();
        }
        public string GetPayrollEndpoint(ConnectorQuery query)
        {
            var endpoint = new ConnectorEndpoint("companies/{0}/payrolls");
            
            // add processed parameter
            if (query.HasBooleanRestriction("processed")) endpoint.AddParameter("processed", "true");
            if (query.HasBooleanRestriction("processed", false)) endpoint.AddParameter("processed", "false");

            // add start date parameters
            var restriction = query.FindRestrictionByFieldAndType("start_date", ConnectorRestriction.ConnectorRestrictionType.GreaterThanOrEqualTo);
            if (restriction != null) endpoint.AddParameter("start_date", restriction.Values[0].DateConstant.ToString("yyyy-MM-dd"));
            restriction = query.FindRestrictionByFieldAndType("start_date", ConnectorRestriction.ConnectorRestrictionType.GreaterThan);
            if (restriction != null) endpoint.AddParameter("start_date", restriction.Values[0].DateConstant.AddDays(1).ToString("yyyy-MM-dd"));

            // add end date parameters
            restriction = query.FindRestrictionByFieldAndType("end_date", ConnectorRestriction.ConnectorRestrictionType.LessThanOrEqualTo);
            if (restriction != null) endpoint.AddParameter("end_date", restriction.Values[0].DateConstant.ToString("yyyy-MM-dd"));
            restriction = query.FindRestrictionByFieldAndType("end_date", ConnectorRestriction.ConnectorRestrictionType.LessThan);
            if (restriction != null) endpoint.AddParameter("end_date", restriction.Values[0].DateConstant.AddDays(-1).ToString("yyyy-MM-dd"));

            return endpoint.ToString();
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
