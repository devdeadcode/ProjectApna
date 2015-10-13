using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class DynamicsGpPayrollModule : DynamicsGpModule
    {

        private const short GpSmartListEmployees = 5;
        private const short GpSmartListPayrollTrx = 10;
        private const short GpSmartListPayrollHistTrx = 18;
        private const short GpSmartListEmployeeSummary = 32;
        
        public DynamicsGpPayrollModule(DynamicsGpConnector connector) : base(connector)
        {
            Name = "Payroll";
            UserDefined1 = "User Defined 1";
            UserDefined2 = "User Defined 2";
            Installed = true;
            ParentConnector = connector;
        }

        public string UserDefined1 { get; set; }
        public string UserDefined2 { get; set; }

        public override void AddEntities()
        {
            Entities.Add(GetEmployeeEntity());
            Entities.Add(GetPayrollTransactionEntity());
            Entities.Add(GetPayrollHistoricalTransactionEntity());
            Entities.Add(GetEmployeeSummaryEntity());
        }

        public DataConnectorEntity GetEmployeeEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListEmployees, "Employees", ParentConnector);

            var upr00100 = entity.AddTable("UPR00100");

            var upr00300 = entity.AddTable("UPR00300", "UPR00100");
            upr00300.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00102 = entity.AddTable("UPR00102", "UPR00100");
            upr00102.AddJoinFields("EMPLOYID", "EMPLOYID");
            upr00102.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddEmployeeEntityFields(upr00100, upr00300, upr00102);

            return entity;
        }
        public void AddEmployeeEntityFields(DataConnectorTable upr00100, DataConnectorTable upr00300, DataConnectorTable upr00102)
        {
            upr00100.AddField("EMPLOYID", "Employee ID", DataConnector.FieldTypeIdString, true);
            upr00100.AddField("LASTNAME", "Last Name", DataConnector.FieldTypeIdString, true);
            upr00100.AddField("FRSTNAME", "First Name", DataConnector.FieldTypeIdString, true);
            upr00102.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString, true);
            upr00102.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString, true);
            upr00102.AddField("CITY", "City", DataConnector.FieldTypeIdString, true);
            upr00102.AddField("STATE", "State", DataConnector.FieldTypeIdString, true);
            upr00102.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString, true);
            upr00102.AddField("PHONE1", "Phone 1", DataConnector.FieldTypeIdPhone, true);
            upr00100.AddField("EMPLCLAS", "Employee Class", DataConnector.FieldTypeIdString);
            upr00100.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("MIDLNAME", "Middle Name", DataConnector.FieldTypeIdString);
            upr00102.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            upr00102.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            upr00100.AddField("SOCSCNUM", "Social Security Number", DataConnector.FieldTypeIdString);
            upr00100.AddField("BRTHDATE", "Birth Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("Calc_Min_Wage_Bal", "Calc Min Wage Bal", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DEPRTMNT", "Department", DataConnector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job Title", DataConnector.FieldTypeIdString);
            upr00100.AddField("ACTINDX", "Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            upr00100.AddField("WKHRPRYR", "Work Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("STRTDATE", "Start Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DEMPINAC", "Date Employee Inactivated", DataConnector.FieldTypeIdDate);
            upr00100.AddField("RSNEMPIN", "Reason Employee Inactivated", DataConnector.FieldTypeIdString);
            upr00100.AddField("MINETPAY", "Minimum Net Pay", DataConnector.FieldTypeIdCurrency);
            upr00100.AddField("SUTASTAT", "Suta State", DataConnector.FieldTypeIdString);
            upr00100.AddField("WRKRCOMP", "Workers Comp", DataConnector.FieldTypeIdString);
            upr00100.AddField("ATACRVAC", "Auto Accrue Vacation", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VACCRAMT", "Vacation Accrual Amount", DataConnector.FieldTypeIdCurrency);
            upr00100.AddField("VACAPRYR", "Vacation Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("VACAVLBL", "Vacation Available", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("ATACRSTM", "Auto Accrue Sick Time", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("SKTMACAM", "Sick Time Accrual Amount", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("SIKTIMAV", "Sick Time Available", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("SKTMHPYR", "Sick Time Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("USERDEF1", UserDefined1, DataConnector.FieldTypeIdString);
            upr00100.AddField("USERDEF2", UserDefined2, DataConnector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            upr00300.AddField("EXMFRFED", "Exempt From Federal", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("FEDEXMPT", "Federal Exemptions", DataConnector.FieldTypeIdInteger);
            upr00300.AddField("ADFDWHDG", "Additional Federal Withholding", DataConnector.FieldTypeIdCurrency);
            upr00300.AddField("ESTFEDWH", "Estimated Federal Withholding", DataConnector.FieldTypeIdCurrency);
            upr00300.AddField("STATECD", "State Code", DataConnector.FieldTypeIdString);
            upr00300.AddField("LOCALTAX", "Local Tax", DataConnector.FieldTypeIdString);
            upr00300.AddField("W2BF942E", "W2 Box For 942 Employee", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCSD", "W2 Box For Deceased", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCMP", "W2 Box For Deferred Compensation", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFLREP", "W2 Box For Legal Representative", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFPPLN", "W2 Box For Retirement Plan", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFSTEM", "W2 Box For Statutory Employee", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("MCRQGEMP", "Medicare Qualified Govt Employee", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("NYTXDiff", "Withhold NY Tax Diff", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("ALTERNATENAME", "Alternate Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("BENADJDATE", "Ben Adj Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("BENEFITEXPIRE_I", "Benefit Expire", DataConnector.FieldTypeIdDate);
            upr00100.AddField("BIRTHDAY", "Birth Day", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("BIRTHMONTH", "Birth Month", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("CHANGEBY_I", "Change By", DataConnector.FieldTypeIdString);
            upr00100.AddField("CHANGEDATE_I", "Change Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("CITIZEN", "Citizen", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DATEOFLASTREVIEW_I", "Date Of Last Review", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DATEOFNEXTREVIEW_I", "Date Of Next Review", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DISABLEDVETERAN", "Disabled Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DIVISIONCODE_I", "Division Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("FEDCLSSCD", "Federal Classification Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("HANDICAPPED", "Handicapped", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("I9RENEW", "I9renew", DataConnector.FieldTypeIdDate);
            upr00100.AddField("LASTDAYWORKED_I", "Last Day Worked", DataConnector.FieldTypeIdDate);
            upr00100.AddField("LOCATNID", "Location ID", DataConnector.FieldTypeIdString);
            upr00100.AddField("NICKNAME", "NickName", DataConnector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX2", "Note Index2", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("Primary_Pay_Record", "Primary Pay Record", DataConnector.FieldTypeIdString);
            upr00100.AddField("RATECLSS", "Rate Class", DataConnector.FieldTypeIdString);
            upr00100.AddField("SMOKER_I", "Smoker", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("SPOUSE", "Spouse", DataConnector.FieldTypeIdString);
            upr00100.AddField("SPOUSESSN", "Spouse SSN", DataConnector.FieldTypeIdString);
            upr00100.AddField("SUPERVISORCODE_I", "Supervisor Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("UNIONCD", "Union Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("UNIONEMPLOYEE", "Union Employee", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VERIFIED", "Verified", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VETERAN", "Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VIETNAMVETERAN", "Vietnam Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("WRNSTFLSBLWZR", "Warn Sick Time Falls Below Zero", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("WRNVCNFLSBLWZR", "Warn Vacation Falls Below Zero", DataConnector.FieldTypeIdYesNo);
            upr00102.AddField("COUNTY", "County", DataConnector.FieldTypeIdString);
            upr00102.AddField("PHONE2", "Phone 2", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("FAX", "Fax", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("Foreign_Address", "Foreign Address", DataConnector.FieldTypeIdYesNo);
            upr00102.AddField("Foreign_StateProvince", "Foreign State/Province", DataConnector.FieldTypeIdString);
            upr00102.AddField("Foreign_Postal_Code", "Foreign Postal Code", DataConnector.FieldTypeIdString);
            upr00102.AddField("CCode", "Country Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("OTHERVET", "Other Veterans", DataConnector.FieldTypeIdYesNo);

            var gender = upr00100.AddField("GENDER", "Gender", DataConnector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });

            var ethnicOrigin = upr00100.AddField("ETHNORGN", "Ethnic Origin", DataConnector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan Native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific Islander" });

            var whichCashAccountForPay = upr00100.AddField("WCACFPAY", "Which Cash Account For Pay", DataConnector.FieldTypeIdEnum);
            whichCashAccountForPay.AddListItems(0, new List<string> { "Checkbook", "Employee" });

            var vacationAccrualMethod = upr00100.AddField("VACCRMTH", "Vacation Accrual Method", DataConnector.FieldTypeIdEnum);
            vacationAccrualMethod.AddListItems(1, new List<string> { "Hours Worked", "Amount" });

            var sickTimeAccrualMethod = upr00100.AddField("STMACMTH", "Sick Time Accrual Method", DataConnector.FieldTypeIdEnum);
            sickTimeAccrualMethod.AddListItems(1, new List<string> { "Hours Worked", "Amount" });

            var federalFilingStatus = upr00300.AddField("FDFLGSTS", "Federal Filing Status", DataConnector.FieldTypeIdEnum);
            federalFilingStatus.AddListItems(1, new List<string> { "Exempt" });

            var eicFilingStatus = upr00300.AddField("EICFLGST", "EIC Filing Status", DataConnector.FieldTypeIdEnum);
            eicFilingStatus.AddListItems(1, new List<string> { "Not Eligible" });

            var hrStatus = upr00100.AddField("HRSTATUS", "HR Status", DataConnector.FieldTypeIdEnum);
            hrStatus.AddListItems(1, new List<string> { "Active", "Family Leave", "Leave of Absence", "Maternity", "Retired", "Separated", "Suspended", "Terminated", "Other" });

            var maritalStatus = upr00100.AddField("MARITALSTATUS", "Marital Status", DataConnector.FieldTypeIdEnum);
            maritalStatus.AddListItems(1, new List<string> { "Married", "Single", "N/A" });

            var typeOfEmployment = upr00100.AddField("EMPLOYMENTTYPE", "Type of Employment", DataConnector.FieldTypeIdEnum);
            typeOfEmployment.AddListItems(1, new List<string> { "Full Time Regular", "Full Time Temp", "Part Time Regular", "Part Time Temp", "Intern", "Other" });
        }
            
        public DataConnectorEntity GetPayrollTransactionEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListPayrollTrx, "Payroll transactions", ParentConnector);

            var upr10302 = entity.AddTable("UPR10302");

            var upr00100 = entity.AddTable("UPR00100", "UPR10302", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00100.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00300 = entity.AddTable("UPR00300", "UPR00100", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00300.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00102 = entity.AddTable("UPR00102", "UPR00100", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00102.AddJoinFields("EMPLOYID", "EMPLOYID");
            upr00102.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddPayrollTransactionEntityFields(upr10302, upr00100, upr00300, upr00102);

            return entity;
        }
        public void AddPayrollTransactionEntityFields(DataConnectorTable upr10302, DataConnectorTable upr00100, DataConnectorTable upr00300, DataConnectorTable upr00102)
        {
            upr10302.AddField("COMPTRNM", "Computer TRX Number", DataConnector.FieldTypeIdInteger, true);
            upr10302.AddField("BACHNUMB", "Batch Number", DataConnector.FieldTypeIdString, true);
            upr10302.AddField("EMPLOYID", "Employee ID", DataConnector.FieldTypeIdString, true);
            upr10302.AddField("TRXBEGDT", "TRX Beginning Date", DataConnector.FieldTypeIdDate, true);
            upr10302.AddField("TRXENDDT", "TRX Ending Date", DataConnector.FieldTypeIdDate, true);
            upr10302.AddField("UPRTRXCD", "UPR TRX Code", DataConnector.FieldTypeIdString);
            upr10302.AddField("TRXHRUNT", "TRX Hours/Units", DataConnector.FieldTypeIdInteger);
            upr10302.AddField("HRLYPYRT", "Hourly Pay Rate", DataConnector.FieldTypeIdCurrency);
            upr10302.AddField("PAYRTAMT", "Pay Rate Amount", DataConnector.FieldTypeIdCurrency);
            upr10302.AddField("VARDBAMT", "Variable Ded/Ben Amount", DataConnector.FieldTypeIdCurrency);
            upr10302.AddField("VARDBPCT", "Variable Ded/Ben Percent", DataConnector.FieldTypeIdPercentage);
            upr10302.AddField("DAYSWRDK", "Days Worked", DataConnector.FieldTypeIdInteger);
            upr10302.AddField("WKSWRKD", "Weeks Worked", DataConnector.FieldTypeIdInteger);
            upr10302.AddField("DEPRTMNT", "Department", DataConnector.FieldTypeIdString);
            upr10302.AddField("JOBTITLE", "Job Title", DataConnector.FieldTypeIdString);
            upr10302.AddField("STATECD", "State Code", DataConnector.FieldTypeIdString);
            upr10302.AddField("LOCALTAX", "Local Tax", DataConnector.FieldTypeIdString);
            upr10302.AddField("SUTASTAT", "Suta State", DataConnector.FieldTypeIdString);
            upr10302.AddField("WRKRCOMP", "Workers Comp", DataConnector.FieldTypeIdString);
            upr10302.AddField("LASTUSER", "Last User", DataConnector.FieldTypeIdString);
            upr10302.AddField("LSTDTEDT", "Last Date Edited", DataConnector.FieldTypeIdDate);
            upr10302.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            upr10302.AddField("VOIDED", "Voided", DataConnector.FieldTypeIdYesNo);
            upr10302.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("EMPLCLAS", "Employee Class", DataConnector.FieldTypeIdString);
            upr00100.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("LASTNAME", "Last Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("FRSTNAME", "First Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("MIDLNAME", "Middle Name", DataConnector.FieldTypeIdString);
            upr00102.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            upr00102.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            upr00102.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            upr00102.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            upr00102.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            upr00102.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            upr00102.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("SOCSCNUM", "Social Security Number", DataConnector.FieldTypeIdString);
            upr00100.AddField("BRTHDATE", "Birth Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("Calc_Min_Wage_Bal", "Calc Min Wage Bal", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DEPRTMNT", "Department from Employee Master", DataConnector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job Title from Employee Master", DataConnector.FieldTypeIdString);
            upr00100.AddField("ACTINDX", "Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            upr00100.AddField("WKHRPRYR", "Work Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("STRTDATE", "Start Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DEMPINAC", "Date Employee Inactivated", DataConnector.FieldTypeIdDate);
            upr00100.AddField("RSNEMPIN", "Reason Employee Inactivated", DataConnector.FieldTypeIdString);
            upr00100.AddField("MINETPAY", "Minimum Net Pay", DataConnector.FieldTypeIdCurrency);
            upr00100.AddField("SUTASTAT", "Suta State from Employee Master", DataConnector.FieldTypeIdString);
            upr00100.AddField("WRKRCOMP", "Workers Comp from Employee Master", DataConnector.FieldTypeIdString);
            upr00100.AddField("ATACRVAC", "Auto Accrue Vacation", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VACCRAMT", "Vacation Accrual Amount", DataConnector.FieldTypeIdCurrency);
            upr00100.AddField("VACAPRYR", "Vacation Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("VACAVLBL", "Vacation Available", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("ATACRSTM", "Auto Accrue Sick Time", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("SKTMACAM", "Sick Time Accrual Amount", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("SIKTIMAV", "Sick Time Available", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("SKTMHPYR", "Sick Time Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("USERDEF1", UserDefined1, DataConnector.FieldTypeIdString);
            upr00100.AddField("USERDEF2", UserDefined2, DataConnector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Note Index from Employee Master", DataConnector.FieldTypeIdInteger);
            upr00300.AddField("EXMFRFED", "Exempt From Federal", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("FEDEXMPT", "Federal Exemptions", DataConnector.FieldTypeIdInteger);
            upr00300.AddField("ADFDWHDG", "Additional Federal Withholding", DataConnector.FieldTypeIdCurrency);
            upr00300.AddField("ESTFEDWH", "Estimated Federal Withholding", DataConnector.FieldTypeIdCurrency);
            upr00300.AddField("STATECD", "State Code from Employee Master", DataConnector.FieldTypeIdString);
            upr00300.AddField("LOCALTAX", "Local Tax from Employee Master", DataConnector.FieldTypeIdString);
            upr00300.AddField("W2BF942E", "W2 Box For 942 Employee", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCSD", "W2 Box For Deceased", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCMP", "W2 Box For Deferred Compensation", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFLREP", "W2 Box For Legal Representative", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFPPLN", "W2 Box For Retirement Plan", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFSTEM", "W2 Box For Statutory Employee", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("MCRQGEMP", "Medicare Qualified Govt Employee", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("NYTXDiff", "Withhold NY Tax Diff", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("ALTERNATENAME", "Alternate Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("BENADJDATE", "Ben Adj Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("BENEFITEXPIRE_I", "Benefit Expire", DataConnector.FieldTypeIdDate);
            upr00100.AddField("BIRTHDAY", "Birth Day", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("BIRTHMONTH", "Birth Month", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("CHANGEBY_I", "Change By", DataConnector.FieldTypeIdString);
            upr00100.AddField("CHANGEDATE_I", "Change Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("CITIZEN", "Citizen", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DATEOFLASTREVIEW_I", "Date Of Last Review", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DATEOFNEXTREVIEW_I", "Date Of Next Review", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DISABLEDVETERAN", "Disabled Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DIVISIONCODE_I", "Division Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("FEDCLSSCD", "Federal Classification Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("HANDICAPPED", "Handicapped", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("I9RENEW", "I9renew", DataConnector.FieldTypeIdDate);
            upr00100.AddField("LASTDAYWORKED_I", "Last Day Worked", DataConnector.FieldTypeIdDate);
            upr00100.AddField("LOCATNID", "Location ID", DataConnector.FieldTypeIdString);
            upr00100.AddField("NICKNAME", "NickName", DataConnector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX2", "Note Index2", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("Primary_Pay_Record", "Primary Pay Record", DataConnector.FieldTypeIdString);
            upr00100.AddField("RATECLSS", "Rate Class", DataConnector.FieldTypeIdString);
            upr00100.AddField("SMOKER_I", "Smoker", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("SPOUSE", "Spouse", DataConnector.FieldTypeIdString);
            upr00100.AddField("SPOUSESSN", "Spouse SSN", DataConnector.FieldTypeIdString);
            upr00100.AddField("SUPERVISORCODE_I", "Supervisor Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("UNIONCD", "Union Code from Employee Master", DataConnector.FieldTypeIdString);
            upr00100.AddField("UNIONEMPLOYEE", "Union Employee", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VERIFIED", "Verified", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VETERAN", "Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VIETNAMVETERAN", "Vietnam Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("WRNSTFLSBLWZR", "Warn Sick Time Falls Below Zero", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("WRNVCNFLSBLWZR", "Warn Vacation Falls Below Zero", DataConnector.FieldTypeIdYesNo);
            upr00102.AddField("COUNTY", "County", DataConnector.FieldTypeIdString);
            upr00102.AddField("PHONE1", "Phone 1", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("PHONE2", "Phone 2", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("FAX", "Fax", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("Foreign_Address", "Foreign Address", DataConnector.FieldTypeIdYesNo);
            upr00102.AddField("Foreign_StateProvince", "Foreign State/Province", DataConnector.FieldTypeIdString);
            upr00102.AddField("Foreign_Postal_Code", "Foreign Postal Code", DataConnector.FieldTypeIdString);
            upr00102.AddField("CCode", "Country Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("OTHERVET", "Other Veterans", DataConnector.FieldTypeIdYesNo);
            upr10302.AddField("INADDNTOSAL", "In Addition To Salary", DataConnector.FieldTypeIdYesNo);
            upr10302.AddField("JOBNUMBR", "Job Number", DataConnector.FieldTypeIdString);
            upr10302.AddField("SHFTCODE", "Shift Code", DataConnector.FieldTypeIdString);
            upr10302.AddField("SHFTPREM", "Shift Premium", DataConnector.FieldTypeIdCurrency);
            upr10302.AddField("UNIONCD", "Union Code", DataConnector.FieldTypeIdString);

            var computerTrxType = upr10302.AddField("COMPTRTP", "Computer TRX Type", DataConnector.FieldTypeIdEnum);
            computerTrxType.AddListItems(1, new List<string> { "Pay Code", "Deduction", "Benefit" });

            var gender = upr00100.AddField("GENDER", "Gender", DataConnector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });

            var ethnicOrigin = upr00100.AddField("ETHNORGN", "Ethnic Origin", DataConnector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan Native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific Islander" });

            var whichCashAccountForPay = upr00100.AddField("WCACFPAY", "Which Cash Account For Pay", DataConnector.FieldTypeIdEnum);
            whichCashAccountForPay.AddListItems(0, new List<string> { "Checkbook", "Employee" });

            var vacationAccrualMethod = upr00100.AddField("VACCRMTH", "Vacation Accrual Method", DataConnector.FieldTypeIdEnum);
            vacationAccrualMethod.AddListItems(0, new List<string> { "Hours Worked", "Amount" });

            var sickTimeAccrualMethod = upr00100.AddField("STMACMTH", "Sick Time Accrual Method", DataConnector.FieldTypeIdEnum);
            sickTimeAccrualMethod.AddListItems(0, new List<string> { "Hours Worked", "Amount" });

            var federalFilingStatus = upr00300.AddField("FDFLGSTS", "Federal Filing Status", DataConnector.FieldTypeIdEnum);
            federalFilingStatus.AddListItems(1, new List<string> { "Exempt" });

            var eicFilingStatus = upr00300.AddField("EICFLGST", "EIC Filing Status", DataConnector.FieldTypeIdEnum);
            eicFilingStatus.AddListItems(1, new List<string> { "Not Eligible" });

            var hrStatus = upr00100.AddField("HRSTATUS", "HR Status", DataConnector.FieldTypeIdEnum);
            hrStatus.AddListItems(1, new List<string> { "Active", "Family Leave", "Leave of Absence", "Maternity", "Retired", "Separated", "Suspended", "Terminated", "Other" });

            var maritalStatus = upr00100.AddField("MARITALSTATUS", "Marital Status", DataConnector.FieldTypeIdEnum);
            maritalStatus.AddListItems(1, new List<string> { "Married", "Single", "N/A" });

            var typeOfEmployment = upr00100.AddField("EMPLOYMENTTYPE", "Type of Employment", DataConnector.FieldTypeIdEnum);
            typeOfEmployment.AddListItems(1, new List<string> { "Full Time Regular", "Full Time Temp", "Part Time Regular", "Part Time Temp", "Intern", "Other" });

            var salaryChange = upr10302.AddField("SALCHG", "Salary Change", DataConnector.FieldTypeIdEnum);
            salaryChange.AddListItems(1, new List<string> { "Reallocate Dollars", "Reallocate Hours", "Reduce Dollars", "Reduce Hours", "Additional Amount" });
        }
            
        public DataConnectorEntity GetPayrollHistoricalTransactionEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListPayrollHistTrx, "Historical payroll transactions", ParentConnector);

            var upr30300 = entity.AddTable("UPR30300");

            var upr00100 = entity.AddTable("UPR00100", "UPR30300", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00100.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00300 = entity.AddTable("UPR00300", "UPR00100", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00300.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00102 = entity.AddTable("UPR00102", "UPR00100", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00102.AddJoinFields("EMPLOYID", "EMPLOYID");
            upr00102.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddPayrollHistoricalTransactionEntityFields(upr30300, upr00100, upr00300, upr00102);

            return entity;
        }
        public void AddPayrollHistoricalTransactionEntityFields(DataConnectorTable upr30300, DataConnectorTable upr00100, DataConnectorTable upr00300, DataConnectorTable upr00102)
        {
            upr30300.AddField("AUCTRLCD", "Audit Control Code", DataConnector.FieldTypeIdString, true);
            upr30300.AddField("EMPLOYID", "Employee ID", DataConnector.FieldTypeIdString, true);
            upr30300.AddField("CHEKNMBR", "Check Number", DataConnector.FieldTypeIdString, true);
            upr30300.AddField("UPRTRXAM", "UPR TRX Amount", DataConnector.FieldTypeIdCurrency, true);
            upr30300.AddField("PYADNMBR", "Payment/Adjustment Number", DataConnector.FieldTypeIdInteger);
            upr30300.AddField("TRXSORCE", "TRX Source", DataConnector.FieldTypeIdString);
            upr30300.AddField("DOCTYPE", "Document Type", DataConnector.FieldTypeIdInteger);
            upr30300.AddField("DEPRTMNT", "Department", DataConnector.FieldTypeIdString);
            upr30300.AddField("JOBTITLE", "Job Title", DataConnector.FieldTypeIdString);
            upr30300.AddField("PAYROLCD", "Payroll Code", DataConnector.FieldTypeIdString);
            upr30300.AddField("TRXBEGDT", "TRX Beginning Date", DataConnector.FieldTypeIdDate);
            upr30300.AddField("TRXENDDT", "TRX Ending Date", DataConnector.FieldTypeIdDate);
            upr30300.AddField("UNTSTOPY", "Units To Pay", DataConnector.FieldTypeIdCurrency);
            upr30300.AddField("PAYRATE", "Pay Rate", DataConnector.FieldTypeIdCurrency);
            upr30300.AddField("RECEIPTS", "Receipts", DataConnector.FieldTypeIdCurrency);
            upr30300.AddField("PAYADVNC", "Pay Advance", DataConnector.FieldTypeIdCurrency);
            upr30300.AddField("STATECD", "State Code", DataConnector.FieldTypeIdString);
            upr30300.AddField("LOCALTAX", "Local Tax", DataConnector.FieldTypeIdString);
            upr30300.AddField("WRKRCOMP", "Workers Comp", DataConnector.FieldTypeIdString);
            upr30300.AddField("CHEKDATE", "Check Date", DataConnector.FieldTypeIdDate);
            upr30300.AddField("USWHPSTD", "User Who Posted", DataConnector.FieldTypeIdString);
            upr30300.AddField("LSTDTEDT", "Last Date Edited", DataConnector.FieldTypeIdDate);
            upr30300.AddField("LASTUSER", "Last User", DataConnector.FieldTypeIdString);
            upr00100.AddField("EMPLCLAS", "Employee Class", DataConnector.FieldTypeIdString);
            upr00100.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("LASTNAME", "Last Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("FRSTNAME", "First Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("MIDLNAME", "Middle Name", DataConnector.FieldTypeIdString);
            upr00102.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            upr00102.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            upr00102.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            upr00102.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            upr00102.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            upr00102.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            upr00102.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("SOCSCNUM", "Social Security Number", DataConnector.FieldTypeIdString);
            upr00100.AddField("BRTHDATE", "Birth Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("Calc_Min_Wage_Bal", "Calc Min Wage Bal", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DEPRTMNT", "Department from Employee Master", DataConnector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job Title from Employee Master", DataConnector.FieldTypeIdString);
            upr00100.AddField("ACTINDX", "Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            upr00100.AddField("WKHRPRYR", "Work Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("STRTDATE", "Start Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DEMPINAC", "Date Employee Inactivated", DataConnector.FieldTypeIdDate);
            upr00100.AddField("RSNEMPIN", "Reason Employee Inactivated", DataConnector.FieldTypeIdString);
            upr00100.AddField("MINETPAY", "Minimum Net Pay", DataConnector.FieldTypeIdCurrency);
            upr00100.AddField("SUTASTAT", "Suta State from Employee Master", DataConnector.FieldTypeIdString);
            upr00100.AddField("WRKRCOMP", "Workers Comp from Employee Master", DataConnector.FieldTypeIdString);
            upr00100.AddField("ATACRVAC", "Auto Accrue Vacation", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VACCRAMT", "Vacation Accrual Amount", DataConnector.FieldTypeIdCurrency);
            upr00100.AddField("VACAPRYR", "Vacation Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("VACAVLBL", "Vacation Available", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("ATACRSTM", "Auto Accrue Sick Time", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("SKTMACAM", "Sick Time Accrual Amount", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("SIKTIMAV", "Sick Time Available", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("SKTMHPYR", "Sick Time Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("USERDEF1", UserDefined1, DataConnector.FieldTypeIdString);
            upr00100.AddField("USERDEF2", UserDefined2, DataConnector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            upr00300.AddField("EXMFRFED", "Exempt From Federal", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("FEDEXMPT", "Federal Exemptions", DataConnector.FieldTypeIdInteger);
            upr00300.AddField("ADFDWHDG", "Additional Federal Withholding", DataConnector.FieldTypeIdCurrency);
            upr00300.AddField("ESTFEDWH", "Estimated Federal Withholding", DataConnector.FieldTypeIdCurrency);
            upr00300.AddField("STATECD", "State Code from Employee Master", DataConnector.FieldTypeIdString);
            upr00300.AddField("LOCALTAX", "Local Tax from Employee Master", DataConnector.FieldTypeIdString);
            upr00300.AddField("W2BF942E", "W2 Box For 942 Employee", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCSD", "W2 Box For Deceased", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCMP", "W2 Box For Deferred Compensation", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFLREP", "W2 Box For Legal Representative", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFPPLN", "W2 Box For Retirement Plan", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFSTEM", "W2 Box For Statutory Employee", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("MCRQGEMP", "Medicare Qualified Govt Employee", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("NYTXDiff", "Withhold NY Tax Diff", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("ALTERNATENAME", "Alternate Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("BENADJDATE", "Ben Adj Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("BENEFITEXPIRE_I", "Benefit Expire", DataConnector.FieldTypeIdDate);
            upr00100.AddField("BIRTHDAY", "Birth Day", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("BIRTHMONTH", "Birth Month", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("CHANGEBY_I", "Change By", DataConnector.FieldTypeIdString);
            upr00100.AddField("CHANGEDATE_I", "Change Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("CITIZEN", "Citizen", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DATEOFLASTREVIEW_I", "Date Of Last Review", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DATEOFNEXTREVIEW_I", "Date Of Next Review", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DISABLEDVETERAN", "Disabled Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DIVISIONCODE_I", "Division Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("FEDCLSSCD", "Federal Classification Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("HANDICAPPED", "Handicapped", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("I9RENEW", "I9renew", DataConnector.FieldTypeIdDate);
            upr00100.AddField("LASTDAYWORKED_I", "Last Day Worked", DataConnector.FieldTypeIdDate);
            upr00100.AddField("LOCATNID", "Location ID", DataConnector.FieldTypeIdString);
            upr00100.AddField("NICKNAME", "NickName", DataConnector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX2", "Note Index2", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("Primary_Pay_Record", "Primary Pay Record", DataConnector.FieldTypeIdString);
            upr00100.AddField("RATECLSS", "Rate Class", DataConnector.FieldTypeIdString);
            upr00100.AddField("SMOKER_I", "Smoker", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("SPOUSE", "Spouse", DataConnector.FieldTypeIdString);
            upr00100.AddField("SPOUSESSN", "Spouse SSN", DataConnector.FieldTypeIdString);
            upr00100.AddField("SUPERVISORCODE_I", "Supervisor Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("UNIONCD", "Union Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("UNIONEMPLOYEE", "Union Employee", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VERIFIED", "Verified", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VETERAN", "Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VIETNAMVETERAN", "Vietnam Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("WRNSTFLSBLWZR", "Warn Sick Time Falls Below Zero", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("WRNVCNFLSBLWZR", "Warn Vacation Falls Below Zero", DataConnector.FieldTypeIdYesNo);
            upr00102.AddField("COUNTY", "County", DataConnector.FieldTypeIdString);
            upr00102.AddField("PHONE1", "Phone 1", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("PHONE2", "Phone 2", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("FAX", "Fax", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("Foreign_Address", "Foreign Address", DataConnector.FieldTypeIdYesNo);
            upr00102.AddField("Foreign_StateProvince", "Foreign State/Province", DataConnector.FieldTypeIdString);
            upr00102.AddField("Foreign_Postal_Code", "Foreign Postal Code", DataConnector.FieldTypeIdString);
            upr00102.AddField("CCode", "Country Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("OTHERVET", "Other Veterans", DataConnector.FieldTypeIdYesNo);
            upr30300.AddField("SHFTCODE", "Shift Code", DataConnector.FieldTypeIdString);
            upr30300.AddField("SHFTPREM", "Shift Premium", DataConnector.FieldTypeIdCurrency);
            upr30300.AddField("TXBLWAGS", "Taxable Wages", DataConnector.FieldTypeIdCurrency);
            upr30300.AddField("DAYSWRDK", "Days Worked", DataConnector.FieldTypeIdInteger);
            upr30300.AddField("WKSWRKD", "Weeks Worked", DataConnector.FieldTypeIdInteger);
            upr30300.AddField("SBJTFUTA", "Subject To Futa", DataConnector.FieldTypeIdYesNo);
            upr30300.AddField("BSDONRTE", "Based On Rate", DataConnector.FieldTypeIdCurrency);
            upr30300.AddField("YEAR1", "Year", DataConnector.FieldTypeIdString);
            upr30300.AddField("SUTASTAT", "Suta State", DataConnector.FieldTypeIdString);
            upr30300.AddField("CMRECNUM", "Record Number", DataConnector.FieldTypeIdString);
            upr30300.AddField("TIMEAVAILABLE", "Time Available", DataConnector.FieldTypeIdString);

            var payrollRecordType = upr30300.AddField("PYRLRTYP", "Payroll Record Type", DataConnector.FieldTypeIdEnum);
            payrollRecordType.AddListItems(1, new List<string> { "Pay Codes", "Deductions", "Benefits", "State Taxes", "Local Taxes" });

            var tipType = upr30300.AddField("TipType", "Tip Type", DataConnector.FieldTypeIdEnum);
            tipType.AddListItems(1, new List<string> { "Directly", "Indirectly" });

            var gender = upr00100.AddField("GENDER", "Gender", DataConnector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });

            var ethnicOrigin = upr00100.AddField("ETHNORGN", "Ethnic Origin", DataConnector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan Native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific Islander" });

            var whichCashAccountForPay = upr00100.AddField("WCACFPAY", "Which Cash Account For Pay", DataConnector.FieldTypeIdEnum);
            whichCashAccountForPay.AddListItems(0, new List<string> { "Checkbook", "Employee" });

            var vacationAccrualMethod = upr00100.AddField("VACCRMTH", "Vacation Accrual Method", DataConnector.FieldTypeIdEnum);
            vacationAccrualMethod.AddListItems(0, new List<string> { "Hours Worked", "Amount" });

            var sickTimeAccrualMethod = upr00100.AddField("STMACMTH", "Sick Time Accrual Method", DataConnector.FieldTypeIdEnum);
            sickTimeAccrualMethod.AddListItems(0, new List<string> { "Hours Worked", "Amount" });

            var federalFilingStatus = upr00300.AddField("FDFLGSTS", "Federal Filing Status", DataConnector.FieldTypeIdEnum);
            federalFilingStatus.AddListItems(1, new List<string> { "Exempt" });

            var eicFilingStatus = upr00300.AddField("EICFLGST", "EIC Filing Status", DataConnector.FieldTypeIdEnum);
            eicFilingStatus.AddListItems(1, new List<string> { "Not Eligible" });

            var hrStatus = upr00100.AddField("HRSTATUS", "HR Status", DataConnector.FieldTypeIdEnum);
            hrStatus.AddListItems(1, new List<string> { "Active", "Family Leave", "Leave of Absence", "Maternity", "Retired", "Separated", "Suspended", "Terminated", "Other" });

            var maritalStatus = upr00100.AddField("MARITALSTATUS", "MaritalStatus", DataConnector.FieldTypeIdEnum);
            maritalStatus.AddListItems(1, new List<string> { "Married", "Single", "N/A" });

            var typeOfEmployment = upr00100.AddField("EMPLOYMENTTYPE", "Type of Employment", DataConnector.FieldTypeIdEnum);
            typeOfEmployment.AddListItems(1, new List<string> { "Full Time Regular", "Full Time Temp", "Part Time Regular", "Part Time Temp", "Intern", "Other" });
        }
            
        public DataConnectorEntity GetEmployeeSummaryEntity()
        {
            var entity = new DataConnectorEntity(GpSmartListEmployeeSummary, "Employee summary", ParentConnector);

            var upr00900 = entity.AddTable("UPR00900");

            var upr00901 = entity.AddTable("UPR00901", "UPR00900", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00901.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00100 = entity.AddTable("UPR00100", "UPR00900", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00100.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00300 = entity.AddTable("UPR00300", "UPR00900", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00300.AddJoinFields("EMPLOYID", "EMPLOYID");

            AddEmployeeSummaryEntityFields(upr00900, upr00901, upr00100, upr00300);

            return entity;
        }
        public void AddEmployeeSummaryEntityFields(DataConnectorTable upr00900, DataConnectorTable upr00901, DataConnectorTable upr00100, DataConnectorTable upr00300)
        {
            upr00900.AddField("EMPLOYID", "Employee ID", DataConnector.FieldTypeIdString, true);
            upr00900.AddField("YEAR1", "Year", DataConnector.FieldTypeIdString, true);
            upr00900.AddField("LPCHKNUM", "Last Paycheck Number", DataConnector.FieldTypeIdString, true);
            upr00900.AddField("LSTPCKDT", "Last Paycheck Date", DataConnector.FieldTypeIdDate, true);
            upr00900.AddField("LPCHKAMT", "Last Paycheck Amount", DataConnector.FieldTypeIdCurrency, true);
            upr00100.AddField("EMPLCLAS", "Employee Class", DataConnector.FieldTypeIdString);
            upr00100.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("LASTNAME", "Last Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("FRSTNAME", "First Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("MIDLNAME", "Middle Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("SOCSCNUM", "Social Security Number", DataConnector.FieldTypeIdString);
            upr00100.AddField("BRTHDATE", "Birth Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("Calc_Min_Wage_Bal", "Calc Min Wage Bal", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DEPRTMNT", "Department", DataConnector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job Title", DataConnector.FieldTypeIdString);
            upr00100.AddField("ACTINDX", "Account Number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            upr00100.AddField("WKHRPRYR", "Work Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("STRTDATE", "Start Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DEMPINAC", "Date Employee Inactivated", DataConnector.FieldTypeIdDate);
            upr00100.AddField("RSNEMPIN", "Reason Employee Inactivated", DataConnector.FieldTypeIdString);
            upr00100.AddField("MINETPAY", "Minimum Net Pay", DataConnector.FieldTypeIdCurrency);
            upr00100.AddField("SUTASTAT", "Suta State", DataConnector.FieldTypeIdString);
            upr00100.AddField("WRKRCOMP", "Workers Comp", DataConnector.FieldTypeIdString);
            upr00100.AddField("ATACRVAC", "Auto Accrue Vacation", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VACCRAMT", "Vacation Accrual Amount", DataConnector.FieldTypeIdCurrency);
            upr00100.AddField("VACAPRYR", "Vacation Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("VACAVLBL", "Vacation Available", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("ATACRSTM", "Auto Accrue Sick Time", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("SKTMACAM", "Sick Time Accrual Amount", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("SIKTIMAV", "Sick Time Available", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("SKTMHPYR", "Sick Time Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("USERDEF1", UserDefined1, DataConnector.FieldTypeIdString);
            upr00100.AddField("USERDEF2", UserDefined2, DataConnector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            upr00300.AddField("EXMFRFED", "Exempt From Federal", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("FEDEXMPT", "Federal Exemptions", DataConnector.FieldTypeIdInteger);
            upr00300.AddField("ADFDWHDG", "Additional Federal Withholding", DataConnector.FieldTypeIdCurrency);
            upr00300.AddField("ESTFEDWH", "Estimated Federal Withholding", DataConnector.FieldTypeIdCurrency);
            upr00300.AddField("STATECD", "State Code", DataConnector.FieldTypeIdString);
            upr00300.AddField("LOCALTAX", "Local Tax", DataConnector.FieldTypeIdString);
            upr00300.AddField("W2BF942E", "W2 Box For 942 Employee", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCSD", "W2 Box For Deceased", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCMP", "W2 Box For Deferred Compensation", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFLREP", "W2 Box For Legal Representative", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFPPLN", "W2 Box For Retirement Plan", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFSTEM", "W2 Box For Statutory Employee", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("MCRQGEMP", "Medicare Qualified Govt Employee", DataConnector.FieldTypeIdYesNo);
            upr00300.AddField("NYTXDiff", "Withhold NY Tax Diff", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("ALTERNATENAME", "Alternate Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("BENADJDATE", "Ben Adj Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("BENEFITEXPIRE_I", "Benefit Expire", DataConnector.FieldTypeIdDate);
            upr00100.AddField("BIRTHDAY", "Birth Day", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("BIRTHMONTH", "Birth Month", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("CHANGEBY_I", "Change By", DataConnector.FieldTypeIdString);
            upr00100.AddField("CHANGEDATE_I", "Change Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("CITIZEN", "Citizen", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DATEOFLASTREVIEW_I", "Date Of Last Review", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DATEOFNEXTREVIEW_I", "Date Of Next Review", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DISABLEDVETERAN", "Disabled Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DIVISIONCODE_I", "Division Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("FEDCLSSCD", "Federal Classification Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("HANDICAPPED", "Handicapped", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("I9RENEW", "I9renew", DataConnector.FieldTypeIdDate);
            upr00100.AddField("LASTDAYWORKED_I", "Last Day Worked", DataConnector.FieldTypeIdDate);
            upr00100.AddField("LOCATNID", "Location ID", DataConnector.FieldTypeIdString);
            upr00100.AddField("NICKNAME", "NickName", DataConnector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX2", "Note Index2", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("Primary_Pay_Record", "Primary Pay Record", DataConnector.FieldTypeIdString);
            upr00100.AddField("RATECLSS", "Rate Class", DataConnector.FieldTypeIdString);
            upr00100.AddField("SMOKER_I", "Smoker", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("SPOUSE", "Spouse", DataConnector.FieldTypeIdString);
            upr00100.AddField("SPOUSESSN", "Spouse SSN", DataConnector.FieldTypeIdString);
            upr00100.AddField("SUPERVISORCODE_I", "Supervisor Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("UNIONCD", "Union Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("UNIONEMPLOYEE", "Union Employee", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VERIFIED", "Verified", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VETERAN", "Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VIETNAMVETERAN", "Vietnam Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("WRNSTFLSBLWZR", "Warn Sick Time Falls Below Zero", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("WRNVCNFLSBLWZR", "Warn Vacation Falls Below Zero", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("OTHERVET", "Other Veterans", DataConnector.FieldTypeIdYesNo);
            upr00900.AddField("Federal_Wages_1", "Federal Wages - January", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_2", "Federal Wages - February", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_3", "Federal Wages - March", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_4", "Federal Wages - April", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_5", "Federal Wages - May", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_6", "Federal Wages - June", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_7", "Federal Wages - July", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_8", "Federal Wages - August", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_9", "Federal Wages - September", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_10", "Federal Wages - October", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_11", "Federal Wages - November", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_12", "Federal Wages - December", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_1", "Federal Tips - January", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_2", "Federal Tips - February", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_3", "Federal Tips - March", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_4", "Federal Tips - April", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_5", "Federal Tips - May", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_6", "Federal Tips - June", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_7", "Federal Tips - July", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_8", "Federal Tips - August", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_9", "Federal Tips - September", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_10", "Federal Tips - October", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_11", "Federal Tips - November", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_12", "Federal Tips - December", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_1", "Gross Wages - January", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_2", "Gross Wages - February", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_3", "Gross Wages - March", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_4", "Gross Wages - April", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_5", "Gross Wages - May", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_6", "Gross Wages - June", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_7", "Gross Wages - July", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_8", "Gross Wages - August", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_9", "Gross Wages - September", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_10", "Gross Wages - October", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_11", "Gross Wages - November", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_12", "Gross Wages - December", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_1", "Federal Withholding - January", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_2", "Federal Withholding - February", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_3", "Federal Withholding - March", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_4", "Federal Withholding - April", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_5", "Federal Withholding - May", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_6", "Federal Withholding - June", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_7", "Federal Withholding - July", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_8", "Federal Withholding - August", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_9", "Federal Withholding - September", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_10", "Federal Withholding - October", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_11", "Federal Withholding - November", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_12", "Federal Withholding - December", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_1", "FICA/Social Security Wages - January", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_2", "FICA/Social Security Wages - February", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_3", "FICA/Social Security Wages - March", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_4", "FICA/Social Security Wages - April", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_5", "FICA/Social Security Wages - May", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_6", "FICA/Social Security Wages - June", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_7", "FICA/Social Security Wages - July", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_8", "FICA/Social Security Wages - August", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_9", "FICA/Social Security Wages - September", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_10", "FICA/Social Security Wages - October", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_11", "FICA/Social Security Wages - November", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_12", "FICA/Social Security Wages - December", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_1", "FICA/Medicare Wages - January", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_2", "FICA/Medicare Wages - February", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_3", "FICA/Medicare Wages - March", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_4", "FICA/Medicare Wages - April", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_5", "FICA/Medicare Wages - May", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_6", "FICA/Medicare Wages - June", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_7", "FICA/Medicare Wages - July", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_8", "FICA/Medicare Wages - August", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_9", "FICA/Medicare Wages - September", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_10", "FICA/Medicare Wages - October", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_11", "FICA/Medicare Wages - November", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_12", "FICA/Medicare Wages - December", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_1", "FICA/Social Security Withholding - January", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_2", "FICA/Social Security Withholding - February", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_3", "FICA/Social Security Withholding - March", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_4", "FICA/Social Security Withholding - April", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_5", "FICA/Social Security Withholding - May", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_6", "FICA/Social Security Withholding - June", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_7", "FICA/Social Security Withholding - July", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_8", "FICA/Social Security Withholding - August", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_9", "FICA/Social Security Withholding - September", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_10", "FICA/Social Security Withholding - October", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_11", "FICA/Social Security Withholding - November", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_12", "FICA/Social Security Withholding - December", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_1", "FICA/Medicare Withholding - January", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_2", "FICA/Medicare Withholding - February", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_3", "FICA/Medicare Withholding - March", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_4", "FICA/Medicare Withholding - April", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_5", "FICA/Medicare Withholding - May", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_6", "FICA/Medicare Withholding - June", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_7", "FICA/Medicare Withholding - July", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_8", "FICA/Medicare Withholding - August", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_9", "FICA/Medicare Withholding - September", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_10", "FICA/Medicare Withholding - October", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_11", "FICA/Medicare Withholding - November", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_12", "FICA/Medicare Withholding - December", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_1", "Suta Wages - January", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_2", "Suta Wages - February", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_3", "Suta Wages - March", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_4", "Suta Wages - April", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_5", "Suta Wages - May", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_6", "Suta Wages - June", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_7", "Suta Wages - July", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_8", "Suta Wages - August", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_9", "Suta Wages - September", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_10", "Suta Wages - October", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_11", "Suta Wages - November", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_12", "Suta Wages - December", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_1", "Futa Wages - January", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_2", "Futa Wages - February", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_3", "Futa Wages - March", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_4", "Futa Wages - April", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_5", "Futa Wages - May", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_6", "Futa Wages - June", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_7", "Futa Wages - July", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_8", "Futa Wages - August", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_9", "Futa Wages - September", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_10", "Futa Wages - October", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_11", "Futa Wages - November", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_12", "Futa Wages - December", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_1", "Net Wages - January", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_2", "Net Wages - February", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_3", "Net Wages - March", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_4", "Net Wages - April", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_5", "Net Wages - May", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_6", "Net Wages - June", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_7", "Net Wages - July", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_8", "Net Wages - August", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_9", "Net Wages - September", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_10", "Net Wages - October", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_11", "Net Wages - November", DataConnector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_12", "Net Wages - December", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_1", "Reported Tips - January", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_2", "Reported Tips - February", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_3", "Reported Tips - March", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_4", "Reported Tips - April", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_5", "Reported Tips - May", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_6", "Reported Tips - June", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_7", "Reported Tips - July", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_8", "Reported Tips - August", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_9", "Reported Tips - September", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_10", "Reported Tips - October", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_11", "Reported Tips - November", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_12", "Reported Tips - December", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_1", "Charged Tips - January", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_2", "Charged Tips - February", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_3", "Charged Tips - March", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_4", "Charged Tips - April", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_5", "Charged Tips - May", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_6", "Charged Tips - June", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_7", "Charged Tips - July", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_8", "Charged Tips - August", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_9", "Charged Tips - September", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_10", "Charged Tips - October", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_11", "Charged Tips - November", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_12", "Charged Tips - December", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_1", "Federal Tax On Tips - January", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_2", "Federal Tax On Tips - February", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_3", "Federal Tax On Tips - March", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_4", "Federal Tax On Tips - April", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_5", "Federal Tax On Tips - May", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_6", "Federal Tax On Tips - June", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_7", "Federal Tax On Tips - July", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_8", "Federal Tax On Tips - August", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_9", "Federal Tax On Tips - September", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_10", "Federal Tax On Tips - October", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_11", "Federal Tax On Tips - November", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_12", "Federal Tax On Tips - December", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_1", "FICA/SS Tips - January", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_2", "FICA/SS Tips - February", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_3", "FICA/SS Tips - March", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_4", "FICA/SS Tips - April", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_5", "FICA/SS Tips - May", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_6", "FICA/SS Tips - June", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_7", "FICA/SS Tips - July", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_8", "FICA/SS Tips - August", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_9", "FICA/SS Tips - September", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_10", "FICA/SS Tips - October", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_11", "FICA/SS Tips - November", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_12", "FICA/SS Tips - December", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_1", "FICA/SS Tax On Tips - January", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_2", "FICA/SS Tax On Tips - February", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_3", "FICA/SS Tax On Tips - March", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_4", "FICA/SS Tax On Tips - April", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_5", "FICA/SS Tax On Tips - May", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_6", "FICA/SS Tax On Tips - June", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_7", "FICA/SS Tax On Tips - July", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_8", "FICA/SS Tax On Tips - August", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_9", "FICA/SS Tax On Tips - September", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_10", "FICA/SS Tax On Tips - October", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_11", "FICA/SS Tax On Tips - November", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_12", "FICA/SS Tax On Tips - December", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_1", "Uncollected FICA/SS Tax - January", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_2", "Uncollected FICA/SS Tax - February", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_3", "Uncollected FICA/SS Tax - March", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_4", "Uncollected FICA/SS Tax - April", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_5", "Uncollected FICA/SS Tax - May", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_6", "Uncollected FICA/SS Tax - June", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_7", "Uncollected FICA/SS Tax - July", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_8", "Uncollected FICA/SS Tax - August", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_9", "Uncollected FICA/SS Tax - September", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_10", "Uncollected FICA/SS Tax - October", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_11", "Uncollected FICA/SS Tax - November", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_12", "Uncollected FICA/SS Tax - December", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_1", "FICA/Med Tips - January", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_2", "FICA/Med Tips - February", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_3", "FICA/Med Tips - March", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_4", "FICA/Med Tips - April", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_5", "FICA/Med Tips - May", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_6", "FICA/Med Tips - June", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_7", "FICA/Med Tips - July", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_8", "FICA/Med Tips - August", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_9", "FICA/Med Tips - September", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_10", "FICA/Med Tips - October", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_11", "FICA/Med Tips - November", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_12", "FICA/Med Tips - December", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_1", "FICA/Med Tax On Tips - January", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_2", "FICA/Med Tax On Tips - February", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_3", "FICA/Med Tax On Tips - March", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_4", "FICA/Med Tax On Tips - April", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_5", "FICA/Med Tax On Tips - May", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_6", "FICA/Med Tax On Tips - June", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_7", "FICA/Med Tax On Tips - July", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_8", "FICA/Med Tax On Tips - August", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_9", "FICA/Med Tax On Tips - September", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_10", "FICA/Med Tax On Tips - October", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_11", "FICA/Med Tax On Tips - November", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_12", "FICA/Med Tax On Tips - December", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_1", "Uncollected FICA/Med Tax - January", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_2", "Uncollected FICA/Med Tax - February", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_3", "Uncollected FICA/Med Tax - March", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_4", "Uncollected FICA/Med Tax - April", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_5", "Uncollected FICA/Med Tax - May", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_6", "Uncollected FICA/Med Tax - June", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_7", "Uncollected FICA/Med Tax - July", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_8", "Uncollected FICA/Med Tax - August", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_9", "Uncollected FICA/Med Tax - September", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_10", "Uncollected FICA/Med Tax - October", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_11", "Uncollected FICA/Med Tax - November", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_12", "Uncollected FICA/Med Tax - December", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_1", "Reported Receipts - January", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_2", "Reported Receipts - February", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_3", "Reported Receipts - March", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_4", "Reported Receipts - April", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_5", "Reported Receipts - May", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_6", "Reported Receipts - June", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_7", "Reported Receipts - July", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_8", "Reported Receipts - August", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_9", "Reported Receipts - September", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_10", "Reported Receipts - October", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_11", "Reported Receipts - November", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_12", "Reported Receipts - December", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_1", "Charged Receipts - January", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_2", "Charged Receipts - February", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_3", "Charged Receipts - March", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_4", "Charged Receipts - April", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_5", "Charged Receipts - May", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_6", "Charged Receipts - June", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_7", "Charged Receipts - July", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_8", "Charged Receipts - August", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_9", "Charged Receipts - September", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_10", "Charged Receipts - October", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_11", "Charged Receipts - November", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_12", "Charged Receipts - December", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_1", "Allocated Tips - January", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_2", "Allocated Tips - February", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_3", "Allocated Tips - March", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_4", "Allocated Tips - April", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_5", "Allocated Tips - May", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_6", "Allocated Tips - June", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_7", "Allocated Tips - July", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_8", "Allocated Tips - August", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_9", "Allocated Tips - September", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_10", "Allocated Tips - October", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_11", "Allocated Tips - November", DataConnector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_12", "Allocated Tips - December", DataConnector.FieldTypeIdCurrency);

            var gender = upr00100.AddField("GENDER", "Gender", DataConnector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });
            
            var ethnicOrigin = upr00100.AddField("ETHNORGN", "Ethnic Origin", DataConnector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan Native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific Islander" });
            
            var whichCashAccountForPay = upr00100.AddField("WCACFPAY", "Which Cash Account For Pay", DataConnector.FieldTypeIdEnum);
            whichCashAccountForPay.AddListItems(0, new List<string> { "Checkbook", "Employee" });
            
            var vacationAccrualMethod = upr00100.AddField("VACCRMTH", "Vacation Accrual Method", DataConnector.FieldTypeIdEnum);
            vacationAccrualMethod.AddListItems(0, new List<string> { "Hours Worked", "Amount" });

            var sickTimeAccrualMethod = upr00100.AddField("STMACMTH", "Sick Time Accrual Method", DataConnector.FieldTypeIdEnum);
            sickTimeAccrualMethod.AddListItems(0, new List<string> { "Hours Worked", "Amount" });
            
            var federalFilingStatus = upr00300.AddField("FDFLGSTS", "Federal Filing Status", DataConnector.FieldTypeIdEnum);
            federalFilingStatus.AddListItems(1, new List<string> { "Exempt" });

            var eicFilingStatus = upr00300.AddField("EICFLGST", "EIC Filing Status", DataConnector.FieldTypeIdEnum);
            eicFilingStatus.AddListItems(1, new List<string> { "Not Eligible" });
            
            var hrStatus = upr00100.AddField("HRSTATUS", "HR Status", DataConnector.FieldTypeIdEnum);
            hrStatus.AddListItems(1, new List<string> { "Active", "Family Leave", "Leave of Absence", "Maternity", "Retired", "Separated", "Suspended", "Terminated", "Other" });

            var maritalStatus = upr00100.AddField("MARITALSTATUS", "Marital Status", DataConnector.FieldTypeIdEnum);
            maritalStatus.AddListItems(1, new List<string> { "Married", "Single", "N/A" });
            
            var typeOfEmployment = upr00100.AddField("EMPLOYMENTTYPE", "Type of Employment", DataConnector.FieldTypeIdEnum);
            typeOfEmployment.AddListItems(1, new List<string> { "Full Time Regular", "Full Time Temp", "Part Time Regular", "Part Time Temp", "Intern", "Other" });
        }

    }
}
