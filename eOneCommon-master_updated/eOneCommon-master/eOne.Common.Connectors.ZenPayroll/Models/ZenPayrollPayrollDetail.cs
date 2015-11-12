using System;

namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollPayrollDetail
    {

        public enum ZenPayrollPayrollDetailType
        {
            Fixed,
            Hourly,
            PaidTimeOff
        }

        public ZenPayrollPayrollDetail(ZenPayrollPayroll payroll, ZenPayrollEmployee employee, ZenPayrollEmployeeCompensation compensation, ZenPayrollPayrollDetailType type)
        {
            StartDate = payroll.start_date;
            EndDate = payroll.end_date;
            DetailType = type;
            Compensation = compensation;
            Employee = employee;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ZenPayrollPayrollDetailType DetailType { get; set; }
        public string EmployeeFirstName => Employee.first_name;

        public string EmployeeMiddleInitial => Employee.middle_initial;

        public string EmployeeLastName => Employee.last_name;
        public string EmployeeEmail => Employee.email;

        public string EmployeeSsn => Employee.ssn;

        public DateTime EmployeeDateOfBirth => Employee.date_of_birth;

        public string EmployeeName => Employee.name;

        public string CompensationName => Compensation.name;
        public decimal CompensationHours => Compensation.hours;
        public decimal CompensationMultiplier => Compensation.compensation_multiplier;

        public ZenPayrollEmployee Employee { get; set; }
        public ZenPayrollEmployeeCompensation Compensation { get; set; }

    }
}
