using System.Collections.Generic;

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
            Id = 5;
            Name = "Payroll";
            UserDefined1 = "User defined 1";
            UserDefined2 = "User defined 2";
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

        public ConnectorEntity GetEmployeeEntity()
        {
            var entity = new ConnectorEntity(GpSmartListEmployees, "Employees", ParentConnector);

            var upr00100 = entity.AddTable("UPR00100");

            var upr00300 = entity.AddTable("UPR00300", "UPR00100");
            upr00300.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00102 = entity.AddTable("UPR00102", "UPR00100");
            upr00102.AddJoinFields("EMPLOYID", "EMPLOYID");
            upr00102.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddEmployeeEntityFields(upr00100, upr00300, upr00102);

            return entity;
        }
        public void AddEmployeeEntityFields(ConnectorTable upr00100, ConnectorTable upr00300, ConnectorTable upr00102)
        {
            var employeeId = upr00100.AddField("EMPLOYID", "Employee ID", Connector.FieldTypeIdString, true);
            employeeId.KeyNumber = 1;

            upr00100.AddField("LASTNAME", "Last name", Connector.FieldTypeIdString, true);
            upr00100.AddField("FRSTNAME", "First name", Connector.FieldTypeIdString, true);
            upr00102.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString, true);
            upr00102.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString, true);
            upr00102.AddField("CITY", "City", Connector.FieldTypeIdString, true);
            upr00102.AddField("STATE", "State", Connector.FieldTypeIdString, true);
            upr00102.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString, true);
            upr00102.AddField("PHONE1", "Phone 1", Connector.FieldTypeIdPhone, true);
            upr00100.AddField("EMPLCLAS", "Employee class", Connector.FieldTypeIdString);
            upr00100.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo);
            upr00100.AddField("MIDLNAME", "Middle name", Connector.FieldTypeIdString);
            upr00102.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            upr00102.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            upr00100.AddField("SOCSCNUM", "Social security number", Connector.FieldTypeIdString);
            upr00100.AddField("BRTHdate", "Birth date", Connector.FieldTypeIdDate);
            upr00100.AddField("Calc_Min_Wage_Bal", "Calc minimum wage balance", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DEPRTMNT", "Department", Connector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job title", Connector.FieldTypeIdString);
            upr00100.AddField("ACTINDX", "Account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            upr00100.AddField("WKHRPRYR", "Work hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("STRTdate", "Start date", Connector.FieldTypeIdDate);
            upr00100.AddField("DEMPINAC", "Date employee inactivated", Connector.FieldTypeIdDate);
            upr00100.AddField("RSNEMPIN", "Reason employee inactivated", Connector.FieldTypeIdString);
            upr00100.AddField("MINETPAY", "Minimum net pay", Connector.FieldTypeIdCurrency);
            upr00100.AddField("SUTASTAT", "Suta state", Connector.FieldTypeIdString);
            upr00100.AddField("WRKRCOMP", "Workers comp", Connector.FieldTypeIdString);
            upr00100.AddField("ATACRVAC", "Auto accrue vacation", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VACCRAMT", "Vacation accrual amount", Connector.FieldTypeIdCurrency);
            upr00100.AddField("VACAPRYR", "Vacation hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("VACAVLBL", "Vacation available", Connector.FieldTypeIdInteger);
            upr00100.AddField("ATACRSTM", "Auto accrue sick time", Connector.FieldTypeIdYesNo);
            upr00100.AddField("SKTMACAM", "Sick time accrual amount", Connector.FieldTypeIdInteger);
            upr00100.AddField("SIKTIMAV", "Sick time available", Connector.FieldTypeIdInteger);
            upr00100.AddField("SKTMHPYR", "Sick time hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("USERDEF1", UserDefined1, Connector.FieldTypeIdString);
            upr00100.AddField("USERDEF2", UserDefined2, Connector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            upr00300.AddField("EXMFRFED", "Exempt from federal", Connector.FieldTypeIdYesNo);
            upr00300.AddField("FEDEXMPT", "Federal exemptions", Connector.FieldTypeIdInteger);
            upr00300.AddField("ADFDWHDG", "Additional federal withholding", Connector.FieldTypeIdCurrency);
            upr00300.AddField("ESTFEDWH", "Estimated federal withholding", Connector.FieldTypeIdCurrency);
            upr00300.AddField("STATECD", "State code", Connector.FieldTypeIdString);
            upr00300.AddField("LOCALTAX", "Local tax", Connector.FieldTypeIdString);
            upr00300.AddField("W2BF942E", "W2 box for 942 employee", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCSD", "W2 box for deceased", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCMP", "W2 box for deferred compensation", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFLREP", "W2 box for legal representative", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFPPLN", "W2 box for retirement plan", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFSTEM", "W2 box for statutory employee", Connector.FieldTypeIdYesNo);
            upr00300.AddField("MCRQGEMP", "Medicare qualified government employee", Connector.FieldTypeIdYesNo);
            upr00300.AddField("NYTXDiff", "Withhold NY tax difference", Connector.FieldTypeIdYesNo);
            upr00100.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
            upr00100.AddField("ALTERNATENAME", "Alternate name", Connector.FieldTypeIdString);
            upr00100.AddField("BENADJdate", "Benefit adjustment date", Connector.FieldTypeIdDate);
            upr00100.AddField("BENEFITEXPIRE_I", "Benefit expiry date", Connector.FieldTypeIdDate);
            upr00100.AddField("BIRTHDAY", "Birth day", Connector.FieldTypeIdInteger);
            upr00100.AddField("BIRTHMONTH", "Birth month", Connector.FieldTypeIdInteger);
            upr00100.AddField("CHANGEBY_I", "Change by", Connector.FieldTypeIdString);
            upr00100.AddField("CHANGEDATE_I", "Change date", Connector.FieldTypeIdDate);
            upr00100.AddField("CITIZEN", "Citizen", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DATEOFLASTREVIEW_I", "Date of last review", Connector.FieldTypeIdDate);
            upr00100.AddField("DATEOFNEXTREVIEW_I", "Date of next review", Connector.FieldTypeIdDate);
            upr00100.AddField("DISABLEDVETERAN", "Disabled veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DIVISIONCODE_I", "Division code", Connector.FieldTypeIdString);
            upr00100.AddField("FEDCLSSCD", "Federal classification code", Connector.FieldTypeIdString);
            upr00100.AddField("HANDICAPPED", "Handicapped", Connector.FieldTypeIdYesNo);
            upr00100.AddField("I9RENEW", "I9 renew", Connector.FieldTypeIdDate);
            upr00100.AddField("LASTDAYWORKED_I", "Last day worked", Connector.FieldTypeIdDate);
            upr00100.AddField("LOCATNID", "Location ID", Connector.FieldTypeIdString);
            upr00100.AddField("NICKNAME", "Nickname", Connector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX2", "Note index 2", Connector.FieldTypeIdInteger);
            upr00100.AddField("Primary_Pay_Record", "Primary pay record", Connector.FieldTypeIdString);
            upr00100.AddField("RATECLSS", "Rate class", Connector.FieldTypeIdString);
            upr00100.AddField("SMOKER_I", "Smoker", Connector.FieldTypeIdYesNo);
            upr00100.AddField("SPOUSE", "Spouse", Connector.FieldTypeIdString);
            upr00100.AddField("SPOUSESSN", "Spouse SSN", Connector.FieldTypeIdString);
            upr00100.AddField("SUPERVISORCODE_I", "Supervisor code", Connector.FieldTypeIdString);
            upr00100.AddField("UNIONCD", "Union code", Connector.FieldTypeIdString);
            upr00100.AddField("UNIONEMPLOYEE", "Union employee", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VERIFIED", "Verified", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VETERAN", "Veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VIETNAMVETERAN", "Vietnam veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("WRNSTFLSBLWZR", "Warn when sick time falls below zero", Connector.FieldTypeIdYesNo);
            upr00100.AddField("WRNVCNFLSBLWZR", "Warn when vacation falls below zero", Connector.FieldTypeIdYesNo);
            upr00102.AddField("COUNTY", "County", Connector.FieldTypeIdString);
            upr00102.AddField("PHONE2", "Phone 2", Connector.FieldTypeIdPhone);
            upr00102.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            upr00102.AddField("FAX", "Fax", Connector.FieldTypeIdPhone);
            upr00102.AddField("Foreign_Address", "Foreign address", Connector.FieldTypeIdYesNo);
            upr00102.AddField("Foreign_StateProvince", "Foreign state/province", Connector.FieldTypeIdString);
            upr00102.AddField("Foreign_Postal_Code", "Foreign postal code", Connector.FieldTypeIdString);
            upr00102.AddField("CCode", "Country code", Connector.FieldTypeIdString);
            upr00100.AddField("OTHERVET", "Other veterans", Connector.FieldTypeIdYesNo);

            var gender = upr00100.AddField("GENDER", "Gender", Connector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });

            var ethnicOrigin = upr00100.AddField("ETHNORGN", "Ethnic origin", Connector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific islander" });

            var whichCashAccountForPay = upr00100.AddField("WCACFPAY", "Which cash account for pay", Connector.FieldTypeIdEnum);
            whichCashAccountForPay.AddListItems(0, new List<string> { "Checkbook", "Employee" });

            var vacationAccrualMethod = upr00100.AddField("VACCRMTH", "Vacation accrual method", Connector.FieldTypeIdEnum);
            vacationAccrualMethod.AddListItems(1, new List<string> { "Hours worked", "Amount" });

            var sickTimeAccrualMethod = upr00100.AddField("STMACMTH", "Sick time accrual method", Connector.FieldTypeIdEnum);
            sickTimeAccrualMethod.AddListItems(1, new List<string> { "Hours worked", "Amount" });

            var federalFilingStatus = upr00300.AddField("FDFLGSTS", "Federal filing status", Connector.FieldTypeIdEnum);
            federalFilingStatus.AddListItems(1, new List<string> { "Exempt" });

            var eicFilingStatus = upr00300.AddField("EICFLGST", "EIC filing status", Connector.FieldTypeIdEnum);
            eicFilingStatus.AddListItems(1, new List<string> { "Not eligible" });

            var hrStatus = upr00100.AddField("HRSTATUS", "HR status", Connector.FieldTypeIdEnum);
            hrStatus.AddListItems(1, new List<string> { "Active", "Family leave", "Leave of absence", "Maternity", "Retired", "Separated", "Suspended", "Terminated", "Other" });

            var maritalStatus = upr00100.AddField("MARITALSTATUS", "Marital status", Connector.FieldTypeIdEnum);
            maritalStatus.AddListItems(1, new List<string> { "Married", "Single", "N/A" });

            var typeOfEmployment = upr00100.AddField("EMPLOYMENTTYPE", "Type of employment", Connector.FieldTypeIdEnum);
            typeOfEmployment.AddListItems(1, new List<string> { "Full time regular", "Full time temp", "Part time regular", "Part time temp", "Intern", "Other" });
        }
            
        public ConnectorEntity GetPayrollTransactionEntity()
        {
            var entity = new ConnectorEntity(GpSmartListPayrollTrx, "Payroll transactions", ParentConnector);

            var upr10302 = entity.AddTable("UPR10302");

            var upr00100 = entity.AddTable("UPR00100", "UPR10302", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00100.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00300 = entity.AddTable("UPR00300", "UPR00100", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00300.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00102 = entity.AddTable("UPR00102", "UPR00100", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00102.AddJoinFields("EMPLOYID", "EMPLOYID");
            upr00102.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddPayrollTransactionEntityFields(upr10302, upr00100, upr00300, upr00102);

            return entity;
        }
        public void AddPayrollTransactionEntityFields(ConnectorTable upr10302, ConnectorTable upr00100, ConnectorTable upr00300, ConnectorTable upr00102)
        {
            upr10302.AddField("COMPTRNM", "Computer TRX number", Connector.FieldTypeIdInteger, true);
            upr10302.AddField("BACHNUMB", "Batch number", Connector.FieldTypeIdString, true);
            upr10302.AddField("EMPLOYID", "Employee ID", Connector.FieldTypeIdString, true);
            upr10302.AddField("TRXBEGDT", "TRX Beginning date", Connector.FieldTypeIdDate, true);
            upr10302.AddField("TRXENDDT", "TRX Ending date", Connector.FieldTypeIdDate, true);
            upr10302.AddField("UPRTRXCD", "UPR TRX code", Connector.FieldTypeIdString);
            upr10302.AddField("TRXHRUNT", "TRX Hours/Units", Connector.FieldTypeIdInteger);
            upr10302.AddField("HRLYPYRT", "Hourly Pay rate", Connector.FieldTypeIdCurrency);
            upr10302.AddField("PAYRTAMT", "Pay Rate amount", Connector.FieldTypeIdCurrency);
            upr10302.AddField("VARDBAMT", "Variable Ded/Ben amount", Connector.FieldTypeIdCurrency);
            upr10302.AddField("VARDBPCT", "Variable Ded/Ben Percent", Connector.FieldTypeIdPercentage);
            upr10302.AddField("DAYSWRDK", "Days Worked", Connector.FieldTypeIdInteger);
            upr10302.AddField("WKSWRKD", "Weeks Worked", Connector.FieldTypeIdInteger);
            upr10302.AddField("DEPRTMNT", "Department", Connector.FieldTypeIdString);
            upr10302.AddField("JOBTITLE", "Job Title", Connector.FieldTypeIdString);
            upr10302.AddField("STATECD", "State code", Connector.FieldTypeIdString);
            upr10302.AddField("LOCALTAX", "Local Tax", Connector.FieldTypeIdString);
            upr10302.AddField("SUTASTAT", "Suta State", Connector.FieldTypeIdString);
            upr10302.AddField("WRKRCOMP", "Workers Comp", Connector.FieldTypeIdString);
            upr10302.AddField("LASTUSER", "Last User", Connector.FieldTypeIdString);
            upr10302.AddField("LSTDTEDT", "Last Date Edited", Connector.FieldTypeIdDate);
            upr10302.AddField("TRXSORCE", "TRX source", Connector.FieldTypeIdString);
            upr10302.AddField("VOIDED", "Voided", Connector.FieldTypeIdYesNo);
            upr10302.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            upr00100.AddField("EMPLCLAS", "Employee class", Connector.FieldTypeIdString);
            upr00100.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo);
            upr00100.AddField("LASTNAME", "Last name", Connector.FieldTypeIdString);
            upr00100.AddField("FRSTNAME", "First name", Connector.FieldTypeIdString);
            upr00100.AddField("MIDLNAME", "Middle name", Connector.FieldTypeIdString);
            upr00102.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            upr00102.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            upr00102.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            upr00102.AddField("CITY", "City", Connector.FieldTypeIdString);
            upr00102.AddField("STATE", "State", Connector.FieldTypeIdString);
            upr00102.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            upr00102.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            upr00100.AddField("SOCSCNUM", "Social security number", Connector.FieldTypeIdString);
            upr00100.AddField("BRTHdate", "Birth date", Connector.FieldTypeIdDate);
            upr00100.AddField("Calc_Min_Wage_Bal", "Calculate minimum wage balance", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DEPRTMNT", "Department from employee master", Connector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job title from employee master", Connector.FieldTypeIdString);
            upr00100.AddField("ACTINDX", "Account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            upr00100.AddField("WKHRPRYR", "Work hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("STRTdate", "Start date", Connector.FieldTypeIdDate);
            upr00100.AddField("DEMPINAC", "Date employee inactivated", Connector.FieldTypeIdDate);
            upr00100.AddField("RSNEMPIN", "Reason employee inactivated", Connector.FieldTypeIdString);
            upr00100.AddField("MINETPAY", "Minimum net pay", Connector.FieldTypeIdCurrency);
            upr00100.AddField("SUTASTAT", "Suta state from employee master", Connector.FieldTypeIdString);
            upr00100.AddField("WRKRCOMP", "Workers comp from employee master", Connector.FieldTypeIdString);
            upr00100.AddField("ATACRVAC", "Auto accrue vacation", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VACCRAMT", "Vacation accrual amount", Connector.FieldTypeIdCurrency);
            upr00100.AddField("VACAPRYR", "Vacation hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("VACAVLBL", "Vacation available", Connector.FieldTypeIdInteger);
            upr00100.AddField("ATACRSTM", "Auto accrue sick time", Connector.FieldTypeIdYesNo);
            upr00100.AddField("SKTMACAM", "Sick time accrual amount", Connector.FieldTypeIdInteger);
            upr00100.AddField("SIKTIMAV", "Sick time available", Connector.FieldTypeIdInteger);
            upr00100.AddField("SKTMHPYR", "Sick time hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("USERDEF1", UserDefined1, Connector.FieldTypeIdString);
            upr00100.AddField("USERDEF2", UserDefined2, Connector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Note index from employee master", Connector.FieldTypeIdInteger);
            upr00300.AddField("EXMFRFED", "Exempt from federal", Connector.FieldTypeIdYesNo);
            upr00300.AddField("FEDEXMPT", "Federal exemptions", Connector.FieldTypeIdInteger);
            upr00300.AddField("ADFDWHDG", "Additional federal withholding", Connector.FieldTypeIdCurrency);
            upr00300.AddField("ESTFEDWH", "Estimated federal withholding", Connector.FieldTypeIdCurrency);
            upr00300.AddField("STATECD", "State code from employee master", Connector.FieldTypeIdString);
            upr00300.AddField("LOCALTAX", "Local tax from employee master", Connector.FieldTypeIdString);
            upr00300.AddField("W2BF942E", "W2 box for 942 employee", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCSD", "W2 box for deceased", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCMP", "W2 box for deferred compensation", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFLREP", "W2 box for legal representative", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFPPLN", "W2 box for retirement plan", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFSTEM", "W2 box for statutory employee", Connector.FieldTypeIdYesNo);
            upr00300.AddField("MCRQGEMP", "Medicare qualified government employee", Connector.FieldTypeIdYesNo);
            upr00300.AddField("NYTXDiff", "Withhold NY tax difference", Connector.FieldTypeIdYesNo);
            upr00100.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
            upr00100.AddField("ALTERNATENAME", "Alternate name", Connector.FieldTypeIdString);
            upr00100.AddField("BENADJdate", "Benefit adjustment date", Connector.FieldTypeIdDate);
            upr00100.AddField("BENEFITEXPIRE_I", "Benefit expiry date", Connector.FieldTypeIdDate);
            upr00100.AddField("BIRTHDAY", "Birth day", Connector.FieldTypeIdInteger);
            upr00100.AddField("BIRTHMONTH", "Birth month", Connector.FieldTypeIdInteger);
            upr00100.AddField("CHANGEBY_I", "Change by", Connector.FieldTypeIdString);
            upr00100.AddField("CHANGEDATE_I", "Change date", Connector.FieldTypeIdDate);
            upr00100.AddField("CITIZEN", "Citizen", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DATEOFLASTREVIEW_I", "Date of last review", Connector.FieldTypeIdDate);
            upr00100.AddField("DATEOFNEXTREVIEW_I", "Date of next review", Connector.FieldTypeIdDate);
            upr00100.AddField("DISABLEDVETERAN", "Disabled veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DIVISIONCODE_I", "Division code", Connector.FieldTypeIdString);
            upr00100.AddField("FEDCLSSCD", "Federal classification code", Connector.FieldTypeIdString);
            upr00100.AddField("HANDICAPPED", "Handicapped", Connector.FieldTypeIdYesNo);
            upr00100.AddField("I9RENEW", "I9 renew", Connector.FieldTypeIdDate);
            upr00100.AddField("LASTDAYWORKED_I", "Last day worked", Connector.FieldTypeIdDate);
            upr00100.AddField("LOCATNID", "Location ID", Connector.FieldTypeIdString);
            upr00100.AddField("NICKNAME", "Nickname", Connector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX2", "Note index 2", Connector.FieldTypeIdInteger);
            upr00100.AddField("Primary_Pay_Record", "Primary pay record", Connector.FieldTypeIdString);
            upr00100.AddField("RATECLSS", "Rate class", Connector.FieldTypeIdString);
            upr00100.AddField("SMOKER_I", "Smoker", Connector.FieldTypeIdYesNo);
            upr00100.AddField("SPOUSE", "Spouse", Connector.FieldTypeIdString);
            upr00100.AddField("SPOUSESSN", "Spouse SSN", Connector.FieldTypeIdString);
            upr00100.AddField("SUPERVISORCODE_I", "Supervisor code", Connector.FieldTypeIdString);
            upr00100.AddField("UNIONCD", "Union code from employee master", Connector.FieldTypeIdString);
            upr00100.AddField("UNIONEMPLOYEE", "Union employee", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VERIFIED", "Verified", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VETERAN", "Veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VIETNAMVETERAN", "Vietnam veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("WRNSTFLSBLWZR", "Warn when sick time falls below zero", Connector.FieldTypeIdYesNo);
            upr00100.AddField("WRNVCNFLSBLWZR", "Warn when vacation falls below zero", Connector.FieldTypeIdYesNo);
            upr00102.AddField("COUNTY", "County", Connector.FieldTypeIdString);
            upr00102.AddField("PHONE1", "Phone 1", Connector.FieldTypeIdPhone);
            upr00102.AddField("PHONE2", "Phone 2", Connector.FieldTypeIdPhone);
            upr00102.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            upr00102.AddField("FAX", "Fax", Connector.FieldTypeIdPhone);
            upr00102.AddField("Foreign_Address", "Foreign address", Connector.FieldTypeIdYesNo);
            upr00102.AddField("Foreign_StateProvince", "Foreign state/province", Connector.FieldTypeIdString);
            upr00102.AddField("Foreign_Postal_Code", "Foreign postal code", Connector.FieldTypeIdString);
            upr00102.AddField("CCode", "Country code", Connector.FieldTypeIdString);
            upr00100.AddField("OTHERVET", "Other veterans", Connector.FieldTypeIdYesNo);
            upr10302.AddField("INADDNTOSAL", "In addition to salary", Connector.FieldTypeIdYesNo);
            upr10302.AddField("JOBNUMBR", "Job number", Connector.FieldTypeIdString);
            upr10302.AddField("SHFTCODE", "Shift code", Connector.FieldTypeIdString);
            upr10302.AddField("SHFTPREM", "Shift premium", Connector.FieldTypeIdCurrency);
            upr10302.AddField("UNIONCD", "Union code", Connector.FieldTypeIdString);

            var computerTrxType = upr10302.AddField("COMPTRTP", "Computer transaction type", Connector.FieldTypeIdEnum);
            computerTrxType.AddListItems(1, new List<string> { "Pay code", "Deduction", "Benefit" });

            var gender = upr00100.AddField("GENDER", "Gender", Connector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });

            var ethnicOrigin = upr00100.AddField("ETHNORGN", "Ethnic Origin", Connector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific islander" });

            var whichCashAccountForPay = upr00100.AddField("WCACFPAY", "Which cash account for pay", Connector.FieldTypeIdEnum);
            whichCashAccountForPay.AddListItems(0, new List<string> { "Checkbook", "Employee" });

            var vacationAccrualMethod = upr00100.AddField("VACCRMTH", "Vacation accrual method", Connector.FieldTypeIdEnum);
            vacationAccrualMethod.AddListItems(0, new List<string> { "Hours Worked", "Amount" });

            var sickTimeAccrualMethod = upr00100.AddField("STMACMTH", "Sick time accrual method", Connector.FieldTypeIdEnum);
            sickTimeAccrualMethod.AddListItems(0, new List<string> { "Hours Worked", "Amount" });

            var federalFilingStatus = upr00300.AddField("FDFLGSTS", "Federal filing status", Connector.FieldTypeIdEnum);
            federalFilingStatus.AddListItems(1, new List<string> { "Exempt" });

            var eicFilingStatus = upr00300.AddField("EICFLGST", "EIC filing status", Connector.FieldTypeIdEnum);
            eicFilingStatus.AddListItems(1, new List<string> { "Not Eligible" });

            var hrStatus = upr00100.AddField("HRSTATUS", "HR status", Connector.FieldTypeIdEnum);
            hrStatus.AddListItems(1, new List<string> { "Active", "Family leave", "Leave of absence", "Maternity", "Retired", "Separated", "Suspended", "Terminated", "Other" });

            var maritalStatus = upr00100.AddField("MARITALSTATUS", "Marital status", Connector.FieldTypeIdEnum);
            maritalStatus.AddListItems(1, new List<string> { "Married", "Single", "N/A" });

            var typeOfEmployment = upr00100.AddField("EMPLOYMENTTYPE", "Type of employment", Connector.FieldTypeIdEnum);
            typeOfEmployment.AddListItems(1, new List<string> { "Full time regular", "Full time temp", "Part time regular", "Part time temp", "Intern", "Other" });

            var salaryChange = upr10302.AddField("SALCHG", "Salary change", Connector.FieldTypeIdEnum);
            salaryChange.AddListItems(1, new List<string> { "Reallocate dollars", "Reallocate hours", "Reduce dollars", "Reduce hours", "Additional amount" });
        }
            
        public ConnectorEntity GetPayrollHistoricalTransactionEntity()
        {
            var entity = new ConnectorEntity(GpSmartListPayrollHistTrx, "Historical payroll transactions", ParentConnector);

            var upr30300 = entity.AddTable("UPR30300");

            var upr00100 = entity.AddTable("UPR00100", "UPR30300", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00100.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00300 = entity.AddTable("UPR00300", "UPR00100", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00300.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00102 = entity.AddTable("UPR00102", "UPR00100", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00102.AddJoinFields("EMPLOYID", "EMPLOYID");
            upr00102.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddPayrollHistoricalTransactionEntityFields(upr30300, upr00100, upr00300, upr00102);

            return entity;
        }
        public void AddPayrollHistoricalTransactionEntityFields(ConnectorTable upr30300, ConnectorTable upr00100, ConnectorTable upr00300, ConnectorTable upr00102)
        {
            upr30300.AddField("AUCTRLCD", "Audit control code", Connector.FieldTypeIdString, true);
            upr30300.AddField("EMPLOYID", "Employee ID", Connector.FieldTypeIdString, true);
            upr30300.AddField("CHEKNMBR", "Check number", Connector.FieldTypeIdString, true);
            upr30300.AddField("UPRTRXAM", "Transaction amount", Connector.FieldTypeIdCurrency, true);
            upr30300.AddField("PYADNMBR", "Payment/adjustment number", Connector.FieldTypeIdInteger);
            upr30300.AddField("TRXSORCE", "Transaction source", Connector.FieldTypeIdString);
            upr30300.AddField("DOCTYPE", "Document type", Connector.FieldTypeIdInteger);
            upr30300.AddField("DEPRTMNT", "Department", Connector.FieldTypeIdString);
            upr30300.AddField("JOBTITLE", "Job title", Connector.FieldTypeIdString);
            upr30300.AddField("PAYROLCD", "Payroll code", Connector.FieldTypeIdString);
            upr30300.AddField("TRXBEGDT", "Transaction beginning date", Connector.FieldTypeIdDate);
            upr30300.AddField("TRXENDDT", "Transaction ending date", Connector.FieldTypeIdDate);
            upr30300.AddField("UNTSTOPY", "Units to pay", Connector.FieldTypeIdCurrency);
            upr30300.AddField("PAYRATE", "Pay rate", Connector.FieldTypeIdCurrency);
            upr30300.AddField("RECEIPTS", "Receipts", Connector.FieldTypeIdCurrency);
            upr30300.AddField("PAYADVNC", "Pay advance", Connector.FieldTypeIdCurrency);
            upr30300.AddField("STATECD", "State code", Connector.FieldTypeIdString);
            upr30300.AddField("LOCALTAX", "Local tax", Connector.FieldTypeIdString);
            upr30300.AddField("WRKRCOMP", "Workers comp", Connector.FieldTypeIdString);
            upr30300.AddField("CHEKdate", "Check date", Connector.FieldTypeIdDate);
            upr30300.AddField("USWHPSTD", "User who posted", Connector.FieldTypeIdString);
            upr30300.AddField("LSTDTEDT", "Last date edited", Connector.FieldTypeIdDate);
            upr30300.AddField("LASTUSER", "Last user", Connector.FieldTypeIdString);
            upr00100.AddField("EMPLCLAS", "Employee class", Connector.FieldTypeIdString);
            upr00100.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo);
            upr00100.AddField("LASTNAME", "Last name", Connector.FieldTypeIdString);
            upr00100.AddField("FRSTNAME", "First name", Connector.FieldTypeIdString);
            upr00100.AddField("MIDLNAME", "Middle name", Connector.FieldTypeIdString);
            upr00102.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            upr00102.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            upr00102.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            upr00102.AddField("CITY", "City", Connector.FieldTypeIdString);
            upr00102.AddField("STATE", "State", Connector.FieldTypeIdString);
            upr00102.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            upr00102.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            upr00100.AddField("SOCSCNUM", "Social security number", Connector.FieldTypeIdString);
            upr00100.AddField("BRTHdate", "Birth date", Connector.FieldTypeIdDate);
            upr00100.AddField("Calc_Min_Wage_Bal", "Calculate minimum wage balance", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DEPRTMNT", "Department from employee master", Connector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job title from employee master", Connector.FieldTypeIdString);
            upr00100.AddField("ACTINDX", "Account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            upr00100.AddField("WKHRPRYR", "Work hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("STRTdate", "Start date", Connector.FieldTypeIdDate);
            upr00100.AddField("DEMPINAC", "Date employee inactivated", Connector.FieldTypeIdDate);
            upr00100.AddField("RSNEMPIN", "Reason employee inactivated", Connector.FieldTypeIdString);
            upr00100.AddField("MINETPAY", "Minimum net pay", Connector.FieldTypeIdCurrency);
            upr00100.AddField("SUTASTAT", "Suta state from employee master", Connector.FieldTypeIdString);
            upr00100.AddField("WRKRCOMP", "Workers comp from employee master", Connector.FieldTypeIdString);
            upr00100.AddField("ATACRVAC", "Auto accrue vacation", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VACCRAMT", "Vacation accrual amount", Connector.FieldTypeIdCurrency);
            upr00100.AddField("VACAPRYR", "Vacation hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("VACAVLBL", "Vacation available", Connector.FieldTypeIdInteger);
            upr00100.AddField("ATACRSTM", "Auto accrue sick time", Connector.FieldTypeIdYesNo);
            upr00100.AddField("SKTMACAM", "Sick time accrual amount", Connector.FieldTypeIdInteger);
            upr00100.AddField("SIKTIMAV", "Sick time available", Connector.FieldTypeIdInteger);
            upr00100.AddField("SKTMHPYR", "Sick time hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("USERDEF1", UserDefined1, Connector.FieldTypeIdString);
            upr00100.AddField("USERDEF2", UserDefined2, Connector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            upr00300.AddField("EXMFRFED", "Exempt from federal", Connector.FieldTypeIdYesNo);
            upr00300.AddField("FEDEXMPT", "Federal exemptions", Connector.FieldTypeIdInteger);
            upr00300.AddField("ADFDWHDG", "Additional federal withholding", Connector.FieldTypeIdCurrency);
            upr00300.AddField("ESTFEDWH", "Estimated federal withholding", Connector.FieldTypeIdCurrency);
            upr00300.AddField("STATECD", "State code from employee master", Connector.FieldTypeIdString);
            upr00300.AddField("LOCALTAX", "Local tax from employee master", Connector.FieldTypeIdString);
            upr00300.AddField("W2BF942E", "W2 box For 942 employee", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCSD", "W2 box for deceased", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCMP", "W2 box for deferred compensation", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFLREP", "W2 box for legal representative", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFPPLN", "W2 box for retirement plan", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFSTEM", "W2 box for statutory employee", Connector.FieldTypeIdYesNo);
            upr00300.AddField("MCRQGEMP", "Medicare qualified government employee", Connector.FieldTypeIdYesNo);
            upr00300.AddField("NYTXDiff", "Withhold NY tax difference", Connector.FieldTypeIdYesNo);
            upr00100.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
            upr00100.AddField("ALTERNATENAME", "Alternate name", Connector.FieldTypeIdString);
            upr00100.AddField("BENADJdate", "Benefit adjustment date", Connector.FieldTypeIdDate);
            upr00100.AddField("BENEFITEXPIRE_I", "Benefit expiry date", Connector.FieldTypeIdDate);
            upr00100.AddField("BIRTHDAY", "Birth day", Connector.FieldTypeIdInteger);
            upr00100.AddField("BIRTHMONTH", "Birth month", Connector.FieldTypeIdInteger);
            upr00100.AddField("CHANGEBY_I", "Change by", Connector.FieldTypeIdString);
            upr00100.AddField("CHANGEDATE_I", "Change date", Connector.FieldTypeIdDate);
            upr00100.AddField("CITIZEN", "Citizen", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DATEOFLASTREVIEW_I", "Date of last review", Connector.FieldTypeIdDate);
            upr00100.AddField("DATEOFNEXTREVIEW_I", "Date of next review", Connector.FieldTypeIdDate);
            upr00100.AddField("DISABLEDVETERAN", "Disabled veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DIVISIONCODE_I", "Division code", Connector.FieldTypeIdString);
            upr00100.AddField("FEDCLSSCD", "Federal classification code", Connector.FieldTypeIdString);
            upr00100.AddField("HANDICAPPED", "Handicapped", Connector.FieldTypeIdYesNo);
            upr00100.AddField("I9RENEW", "I9 renew", Connector.FieldTypeIdDate);
            upr00100.AddField("LASTDAYWORKED_I", "Last day worked", Connector.FieldTypeIdDate);
            upr00100.AddField("LOCATNID", "Location ID", Connector.FieldTypeIdString);
            upr00100.AddField("NICKNAME", "Nickname", Connector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX2", "Note index 2", Connector.FieldTypeIdInteger);
            upr00100.AddField("Primary_Pay_Record", "Primary pay record", Connector.FieldTypeIdString);
            upr00100.AddField("RATECLSS", "Rate class", Connector.FieldTypeIdString);
            upr00100.AddField("SMOKER_I", "Smoker", Connector.FieldTypeIdYesNo);
            upr00100.AddField("SPOUSE", "Spouse", Connector.FieldTypeIdString);
            upr00100.AddField("SPOUSESSN", "Spouse SSN", Connector.FieldTypeIdString);
            upr00100.AddField("SUPERVISORCODE_I", "Supervisor code", Connector.FieldTypeIdString);
            upr00100.AddField("UNIONCD", "Union code", Connector.FieldTypeIdString);
            upr00100.AddField("UNIONEMPLOYEE", "Union employee", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VERIFIED", "Verified", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VETERAN", "Veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VIETNAMVETERAN", "Vietnam veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("WRNSTFLSBLWZR", "Warn when sick time falls below zero", Connector.FieldTypeIdYesNo);
            upr00100.AddField("WRNVCNFLSBLWZR", "Warn when vacation falls below zero", Connector.FieldTypeIdYesNo);
            upr00102.AddField("COUNTY", "County", Connector.FieldTypeIdString);
            upr00102.AddField("PHONE1", "Phone 1", Connector.FieldTypeIdPhone);
            upr00102.AddField("PHONE2", "Phone 2", Connector.FieldTypeIdPhone);
            upr00102.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            upr00102.AddField("FAX", "Fax", Connector.FieldTypeIdPhone);
            upr00102.AddField("Foreign_Address", "Foreign address", Connector.FieldTypeIdYesNo);
            upr00102.AddField("Foreign_StateProvince", "Foreign state/province", Connector.FieldTypeIdString);
            upr00102.AddField("Foreign_Postal_Code", "Foreign postal code", Connector.FieldTypeIdString);
            upr00102.AddField("CCode", "Country code", Connector.FieldTypeIdString);
            upr00100.AddField("OTHERVET", "Other veterans", Connector.FieldTypeIdYesNo);
            upr30300.AddField("SHFTCODE", "Shift code", Connector.FieldTypeIdString);
            upr30300.AddField("SHFTPREM", "Shift premium", Connector.FieldTypeIdCurrency);
            upr30300.AddField("TXBLWAGS", "Taxable wages", Connector.FieldTypeIdCurrency);
            upr30300.AddField("DAYSWRDK", "Days worked", Connector.FieldTypeIdInteger);
            upr30300.AddField("WKSWRKD", "Weeks worked", Connector.FieldTypeIdInteger);
            upr30300.AddField("SBJTFUTA", "Subject to futa", Connector.FieldTypeIdYesNo);
            upr30300.AddField("BSDONRTE", "Based on rate", Connector.FieldTypeIdCurrency);
            upr30300.AddField("YEAR1", "Year", Connector.FieldTypeIdString);
            upr30300.AddField("SUTASTAT", "Suta state", Connector.FieldTypeIdString);
            upr30300.AddField("CMRECNUM", "Record number", Connector.FieldTypeIdString);
            upr30300.AddField("TIMEAVAILABLE", "Time Available", Connector.FieldTypeIdString);

            var payrollRecordType = upr30300.AddField("PYRLRTYP", "Payroll record type", Connector.FieldTypeIdEnum);
            payrollRecordType.AddListItems(1, new List<string> { "Pay codes", "Deductions", "Benefits", "State taxes", "Local taxes" });

            var tipType = upr30300.AddField("TipType", "Tip type", Connector.FieldTypeIdEnum);
            tipType.AddListItems(1, new List<string> { "Directly", "Indirectly" });

            var gender = upr00100.AddField("GENDER", "Gender", Connector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });

            var ethnicOrigin = upr00100.AddField("ETHNORGN", "Ethnic origin", Connector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific islander" });

            var whichCashAccountForPay = upr00100.AddField("WCACFPAY", "Which cash account for pay", Connector.FieldTypeIdEnum);
            whichCashAccountForPay.AddListItems(0, new List<string> { "Checkbook", "Employee" });

            var vacationAccrualMethod = upr00100.AddField("VACCRMTH", "Vacation accrual method", Connector.FieldTypeIdEnum);
            vacationAccrualMethod.AddListItems(0, new List<string> { "Hours worked", "Amount" });

            var sickTimeAccrualMethod = upr00100.AddField("STMACMTH", "Sick time accrual method", Connector.FieldTypeIdEnum);
            sickTimeAccrualMethod.AddListItems(0, new List<string> { "Hours worked", "Amount" });

            var federalFilingStatus = upr00300.AddField("FDFLGSTS", "Federal filing status", Connector.FieldTypeIdEnum);
            federalFilingStatus.AddListItems(1, new List<string> { "Exempt" });

            var eicFilingStatus = upr00300.AddField("EICFLGST", "EIC filing status", Connector.FieldTypeIdEnum);
            eicFilingStatus.AddListItems(1, new List<string> { "Not eligible" });

            var hrStatus = upr00100.AddField("HRSTATUS", "HR status", Connector.FieldTypeIdEnum);
            hrStatus.AddListItems(1, new List<string> { "Active", "Family leave", "Leave of absence", "Maternity", "Retired", "Separated", "Suspended", "Terminated", "Other" });

            var maritalStatus = upr00100.AddField("MARITALSTATUS", "Marital status", Connector.FieldTypeIdEnum);
            maritalStatus.AddListItems(1, new List<string> { "Married", "Single", "N/A" });

            var typeOfEmployment = upr00100.AddField("EMPLOYMENTTYPE", "Type of employment", Connector.FieldTypeIdEnum);
            typeOfEmployment.AddListItems(1, new List<string> { "Full time regular", "Full time temp", "Part time regular", "Part time temp", "Intern", "Other" });
        }
            
        public ConnectorEntity GetEmployeeSummaryEntity()
        {
            var entity = new ConnectorEntity(GpSmartListEmployeeSummary, "Employee summary", ParentConnector);

            var upr00900 = entity.AddTable("UPR00900");

            var upr00901 = entity.AddTable("UPR00901", "UPR00900", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00901.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00100 = entity.AddTable("UPR00100", "UPR00900", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00100.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00300 = entity.AddTable("UPR00300", "UPR00900", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00300.AddJoinFields("EMPLOYID", "EMPLOYID");

            AddEmployeeSummaryEntityFields(upr00900, upr00901, upr00100, upr00300);

            return entity;
        }
        public void AddEmployeeSummaryEntityFields(ConnectorTable upr00900, ConnectorTable upr00901, ConnectorTable upr00100, ConnectorTable upr00300)
        {
            upr00900.AddField("EMPLOYID", "Employee ID", Connector.FieldTypeIdString, true);
            upr00900.AddField("YEAR1", "Year", Connector.FieldTypeIdString, true);
            upr00900.AddField("LPCHKNUM", "Last paycheck number", Connector.FieldTypeIdString, true);
            upr00900.AddField("LSTPCKDT", "Last paycheck date", Connector.FieldTypeIdDate, true);
            upr00900.AddField("LPCHKAMT", "Last paycheck amount", Connector.FieldTypeIdCurrency, true);
            upr00100.AddField("EMPLCLAS", "Employee class", Connector.FieldTypeIdString);
            upr00100.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo);
            upr00100.AddField("LASTNAME", "Last name", Connector.FieldTypeIdString);
            upr00100.AddField("FRSTNAME", "First name", Connector.FieldTypeIdString);
            upr00100.AddField("MIDLNAME", "Middle name", Connector.FieldTypeIdString);
            upr00100.AddField("SOCSCNUM", "Social security number", Connector.FieldTypeIdString);
            upr00100.AddField("BRTHdate", "Birth date", Connector.FieldTypeIdDate);
            upr00100.AddField("Calc_Min_Wage_Bal", "Calc Min Wage Bal", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DEPRTMNT", "Department", Connector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job Title", Connector.FieldTypeIdString);
            upr00100.AddField("ACTINDX", "Account number", DynamicsGpConnector.FieldTypeIdAccountIndex);
            upr00100.AddField("WKHRPRYR", "Work Hours Per Year", Connector.FieldTypeIdInteger);
            upr00100.AddField("STRTdate", "Start date", Connector.FieldTypeIdDate);
            upr00100.AddField("DEMPINAC", "Date Employee Inactivated", Connector.FieldTypeIdDate);
            upr00100.AddField("RSNEMPIN", "Reason Employee Inactivated", Connector.FieldTypeIdString);
            upr00100.AddField("MINETPAY", "Minimum Net Pay", Connector.FieldTypeIdCurrency);
            upr00100.AddField("SUTASTAT", "Suta State", Connector.FieldTypeIdString);
            upr00100.AddField("WRKRCOMP", "Workers Comp", Connector.FieldTypeIdString);
            upr00100.AddField("ATACRVAC", "Auto Accrue Vacation", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VACCRAMT", "Vacation Accrual amount", Connector.FieldTypeIdCurrency);
            upr00100.AddField("VACAPRYR", "Vacation Hours Per Year", Connector.FieldTypeIdInteger);
            upr00100.AddField("VACAVLBL", "Vacation Available", Connector.FieldTypeIdInteger);
            upr00100.AddField("ATACRSTM", "Auto Accrue Sick time", Connector.FieldTypeIdYesNo);
            upr00100.AddField("SKTMACAM", "Sick Time Accrual amount", Connector.FieldTypeIdInteger);
            upr00100.AddField("SIKTIMAV", "Sick Time Available", Connector.FieldTypeIdInteger);
            upr00100.AddField("SKTMHPYR", "Sick Time Hours Per Year", Connector.FieldTypeIdInteger);
            upr00100.AddField("USERDEF1", UserDefined1, Connector.FieldTypeIdString);
            upr00100.AddField("USERDEF2", UserDefined2, Connector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            upr00300.AddField("EXMFRFED", "Exempt From Federal", Connector.FieldTypeIdYesNo);
            upr00300.AddField("FEDEXMPT", "Federal Exemptions", Connector.FieldTypeIdInteger);
            upr00300.AddField("ADFDWHDG", "Additional Federal Withholding", Connector.FieldTypeIdCurrency);
            upr00300.AddField("ESTFEDWH", "Estimated Federal Withholding", Connector.FieldTypeIdCurrency);
            upr00300.AddField("STATECD", "State code", Connector.FieldTypeIdString);
            upr00300.AddField("LOCALTAX", "Local Tax", Connector.FieldTypeIdString);
            upr00300.AddField("W2BF942E", "W2 Box For 942 Employee", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCSD", "W2 Box For Deceased", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFDCMP", "W2 Box For Deferred Compensation", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFLREP", "W2 Box For Legal Representative", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFPPLN", "W2 Box For Retirement Plan", Connector.FieldTypeIdYesNo);
            upr00300.AddField("W2BFSTEM", "W2 Box For Statutory Employee", Connector.FieldTypeIdYesNo);
            upr00300.AddField("MCRQGEMP", "Medicare Qualified Govt Employee", Connector.FieldTypeIdYesNo);
            upr00300.AddField("NYTXDiff", "Withhold NY Tax Diff", Connector.FieldTypeIdYesNo);
            upr00100.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
            upr00100.AddField("ALTERNATENAME", "Alternate name", Connector.FieldTypeIdString);
            upr00100.AddField("BENADJdate", "Ben Adj date", Connector.FieldTypeIdDate);
            upr00100.AddField("BENEFITEXPIRE_I", "Benefit Expire", Connector.FieldTypeIdDate);
            upr00100.AddField("BIRTHDAY", "Birth Day", Connector.FieldTypeIdInteger);
            upr00100.AddField("BIRTHMONTH", "Birth Month", Connector.FieldTypeIdInteger);
            upr00100.AddField("CHANGEBY_I", "Change By", Connector.FieldTypeIdString);
            upr00100.AddField("CHANGEDATE_I", "Change date", Connector.FieldTypeIdDate);
            upr00100.AddField("CITIZEN", "Citizen", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DATEOFLASTREVIEW_I", "Date Of Last Review", Connector.FieldTypeIdDate);
            upr00100.AddField("DATEOFNEXTREVIEW_I", "Date Of Next Review", Connector.FieldTypeIdDate);
            upr00100.AddField("DISABLEDVETERAN", "Disabled Veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DIVISIONCODE_I", "Division code", Connector.FieldTypeIdString);
            upr00100.AddField("FEDCLSSCD", "Federal Classification code", Connector.FieldTypeIdString);
            upr00100.AddField("HANDICAPPED", "Handicapped", Connector.FieldTypeIdYesNo);
            upr00100.AddField("I9RENEW", "I9renew", Connector.FieldTypeIdDate);
            upr00100.AddField("LASTDAYWORKED_I", "Last Day Worked", Connector.FieldTypeIdDate);
            upr00100.AddField("LOCATNID", "Location ID", Connector.FieldTypeIdString);
            upr00100.AddField("NICKNAME", "NickName", Connector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX2", "Note Index2", Connector.FieldTypeIdInteger);
            upr00100.AddField("Primary_Pay_Record", "Primary Pay Record", Connector.FieldTypeIdString);
            upr00100.AddField("RATECLSS", "Rate class", Connector.FieldTypeIdString);
            upr00100.AddField("SMOKER_I", "Smoker", Connector.FieldTypeIdYesNo);
            upr00100.AddField("SPOUSE", "Spouse", Connector.FieldTypeIdString);
            upr00100.AddField("SPOUSESSN", "Spouse SSN", Connector.FieldTypeIdString);
            upr00100.AddField("SUPERVISORCODE_I", "Supervisor code", Connector.FieldTypeIdString);
            upr00100.AddField("UNIONCD", "Union code", Connector.FieldTypeIdString);
            upr00100.AddField("UNIONEMPLOYEE", "Union Employee", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VERIFIED", "Verified", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VETERAN", "Veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VIETNAMVETERAN", "Vietnam Veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("WRNSTFLSBLWZR", "Warn Sick Time Falls Below Zero", Connector.FieldTypeIdYesNo);
            upr00100.AddField("WRNVCNFLSBLWZR", "Warn Vacation Falls Below Zero", Connector.FieldTypeIdYesNo);
            upr00100.AddField("OTHERVET", "Other Veterans", Connector.FieldTypeIdYesNo);
            upr00900.AddField("Federal_Wages_1", "Federal Wages - January", Connector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_2", "Federal Wages - February", Connector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_3", "Federal Wages - March", Connector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_4", "Federal Wages - April", Connector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_5", "Federal Wages - May", Connector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_6", "Federal Wages - June", Connector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_7", "Federal Wages - July", Connector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_8", "Federal Wages - August", Connector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_9", "Federal Wages - September", Connector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_10", "Federal Wages - October", Connector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_11", "Federal Wages - November", Connector.FieldTypeIdCurrency);
            upr00900.AddField("Federal_Wages_12", "Federal Wages - December", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_1", "Federal Tips - January", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_2", "Federal Tips - February", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_3", "Federal Tips - March", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_4", "Federal Tips - April", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_5", "Federal Tips - May", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_6", "Federal Tips - June", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_7", "Federal Tips - July", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_8", "Federal Tips - August", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_9", "Federal Tips - September", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_10", "Federal Tips - October", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_11", "Federal Tips - November", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tips_12", "Federal Tips - December", Connector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_1", "Gross Wages - January", Connector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_2", "Gross Wages - February", Connector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_3", "Gross Wages - March", Connector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_4", "Gross Wages - April", Connector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_5", "Gross Wages - May", Connector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_6", "Gross Wages - June", Connector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_7", "Gross Wages - July", Connector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_8", "Gross Wages - August", Connector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_9", "Gross Wages - September", Connector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_10", "Gross Wages - October", Connector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_11", "Gross Wages - November", Connector.FieldTypeIdCurrency);
            upr00900.AddField("GROSWAGS_12", "Gross Wages - December", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_1", "Federal Withholding - January", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_2", "Federal Withholding - February", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_3", "Federal Withholding - March", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_4", "Federal Withholding - April", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_5", "Federal Withholding - May", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_6", "Federal Withholding - June", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_7", "Federal Withholding - July", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_8", "Federal Withholding - August", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_9", "Federal Withholding - September", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_10", "Federal Withholding - October", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_11", "Federal Withholding - November", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FDWTHLDG_12", "Federal Withholding - December", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_1", "FICA/Social Security Wages - January", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_2", "FICA/Social Security Wages - February", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_3", "FICA/Social Security Wages - March", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_4", "FICA/Social Security Wages - April", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_5", "FICA/Social Security Wages - May", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_6", "FICA/Social Security Wages - June", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_7", "FICA/Social Security Wages - July", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_8", "FICA/Social Security Wages - August", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_9", "FICA/Social Security Wages - September", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_10", "FICA/Social Security Wages - October", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_11", "FICA/Social Security Wages - November", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWG_12", "FICA/Social Security Wages - December", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_1", "FICA/Medicare Wages - January", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_2", "FICA/Medicare Wages - February", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_3", "FICA/Medicare Wages - March", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_4", "FICA/Medicare Wages - April", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_5", "FICA/Medicare Wages - May", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_6", "FICA/Medicare Wages - June", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_7", "FICA/Medicare Wages - July", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_8", "FICA/Medicare Wages - August", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_9", "FICA/Medicare Wages - September", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_10", "FICA/Medicare Wages - October", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_11", "FICA/Medicare Wages - November", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWGS_12", "FICA/Medicare Wages - December", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_1", "FICA/Social Security Withholding - January", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_2", "FICA/Social Security Withholding - February", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_3", "FICA/Social Security Withholding - March", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_4", "FICA/Social Security Withholding - April", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_5", "FICA/Social Security Withholding - May", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_6", "FICA/Social Security Withholding - June", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_7", "FICA/Social Security Withholding - July", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_8", "FICA/Social Security Withholding - August", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_9", "FICA/Social Security Withholding - September", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_10", "FICA/Social Security Withholding - October", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_11", "FICA/Social Security Withholding - November", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICASSWH_12", "FICA/Social Security Withholding - December", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_1", "FICA/Medicare Withholding - January", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_2", "FICA/Medicare Withholding - February", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_3", "FICA/Medicare Withholding - March", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_4", "FICA/Medicare Withholding - April", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_5", "FICA/Medicare Withholding - May", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_6", "FICA/Medicare Withholding - June", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_7", "FICA/Medicare Withholding - July", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_8", "FICA/Medicare Withholding - August", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_9", "FICA/Medicare Withholding - September", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_10", "FICA/Medicare Withholding - October", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_11", "FICA/Medicare Withholding - November", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FICAMWDG_12", "FICA/Medicare Withholding - December", Connector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_1", "Suta Wages - January", Connector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_2", "Suta Wages - February", Connector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_3", "Suta Wages - March", Connector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_4", "Suta Wages - April", Connector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_5", "Suta Wages - May", Connector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_6", "Suta Wages - June", Connector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_7", "Suta Wages - July", Connector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_8", "Suta Wages - August", Connector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_9", "Suta Wages - September", Connector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_10", "Suta Wages - October", Connector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_11", "Suta Wages - November", Connector.FieldTypeIdCurrency);
            upr00900.AddField("SUTAWAGS_12", "Suta Wages - December", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_1", "Futa Wages - January", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_2", "Futa Wages - February", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_3", "Futa Wages - March", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_4", "Futa Wages - April", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_5", "Futa Wages - May", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_6", "Futa Wages - June", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_7", "Futa Wages - July", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_8", "Futa Wages - August", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_9", "Futa Wages - September", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_10", "Futa Wages - October", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_11", "Futa Wages - November", Connector.FieldTypeIdCurrency);
            upr00900.AddField("FUTAWAGS_12", "Futa Wages - December", Connector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_1", "Net Wages - January", Connector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_2", "Net Wages - February", Connector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_3", "Net Wages - March", Connector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_4", "Net Wages - April", Connector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_5", "Net Wages - May", Connector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_6", "Net Wages - June", Connector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_7", "Net Wages - July", Connector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_8", "Net Wages - August", Connector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_9", "Net Wages - September", Connector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_10", "Net Wages - October", Connector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_11", "Net Wages - November", Connector.FieldTypeIdCurrency);
            upr00900.AddField("NETWAGES_12", "Net Wages - December", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_1", "Reported Tips - January", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_2", "Reported Tips - February", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_3", "Reported Tips - March", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_4", "Reported Tips - April", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_5", "Reported Tips - May", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_6", "Reported Tips - June", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_7", "Reported Tips - July", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_8", "Reported Tips - August", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_9", "Reported Tips - September", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_10", "Reported Tips - October", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_11", "Reported Tips - November", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Tips12_12", "Reported Tips - December", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_1", "Charged Tips - January", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_2", "Charged Tips - February", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_3", "Charged Tips - March", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_4", "Charged Tips - April", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_5", "Charged Tips - May", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_6", "Charged Tips - June", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_7", "Charged Tips - July", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_8", "Charged Tips - August", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_9", "Charged Tips - September", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_10", "Charged Tips - October", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_11", "Charged Tips - November", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Tips12_12", "Charged Tips - December", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_1", "Federal Tax On Tips - January", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_2", "Federal Tax On Tips - February", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_3", "Federal Tax On Tips - March", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_4", "Federal Tax On Tips - April", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_5", "Federal Tax On Tips - May", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_6", "Federal Tax On Tips - June", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_7", "Federal Tax On Tips - July", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_8", "Federal Tax On Tips - August", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_9", "Federal Tax On Tips - September", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_10", "Federal Tax On Tips - October", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_11", "Federal Tax On Tips - November", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Federal_Tax_On_Tips_12", "Federal Tax On Tips - December", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_1", "FICA/SS Tips - January", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_2", "FICA/SS Tips - February", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_3", "FICA/SS Tips - March", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_4", "FICA/SS Tips - April", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_5", "FICA/SS Tips - May", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_6", "FICA/SS Tips - June", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_7", "FICA/SS Tips - July", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_8", "FICA/SS Tips - August", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_9", "FICA/SS Tips - September", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_10", "FICA/SS Tips - October", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_11", "FICA/SS Tips - November", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tips_12", "FICA/SS Tips - December", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_1", "FICA/SS Tax On Tips - January", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_2", "FICA/SS Tax On Tips - February", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_3", "FICA/SS Tax On Tips - March", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_4", "FICA/SS Tax On Tips - April", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_5", "FICA/SS Tax On Tips - May", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_6", "FICA/SS Tax On Tips - June", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_7", "FICA/SS Tax On Tips - July", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_8", "FICA/SS Tax On Tips - August", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_9", "FICA/SS Tax On Tips - September", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_10", "FICA/SS Tax On Tips - October", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_11", "FICA/SS Tax On Tips - November", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICASS_Tax_On_Tips_12", "FICA/SS Tax On Tips - December", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_1", "Uncollected FICA/SS Tax - January", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_2", "Uncollected FICA/SS Tax - February", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_3", "Uncollected FICA/SS Tax - March", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_4", "Uncollected FICA/SS Tax - April", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_5", "Uncollected FICA/SS Tax - May", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_6", "Uncollected FICA/SS Tax - June", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_7", "Uncollected FICA/SS Tax - July", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_8", "Uncollected FICA/SS Tax - August", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_9", "Uncollected FICA/SS Tax - September", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_10", "Uncollected FICA/SS Tax - October", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_11", "Uncollected FICA/SS Tax - November", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICASS_Tax_12", "Uncollected FICA/SS Tax - December", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_1", "FICA/Med Tips - January", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_2", "FICA/Med Tips - February", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_3", "FICA/Med Tips - March", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_4", "FICA/Med Tips - April", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_5", "FICA/Med Tips - May", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_6", "FICA/Med Tips - June", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_7", "FICA/Med Tips - July", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_8", "FICA/Med Tips - August", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_9", "FICA/Med Tips - September", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_10", "FICA/Med Tips - October", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_11", "FICA/Med Tips - November", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tips_12", "FICA/Med Tips - December", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_1", "FICA/Med Tax On Tips - January", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_2", "FICA/Med Tax On Tips - February", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_3", "FICA/Med Tax On Tips - March", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_4", "FICA/Med Tax On Tips - April", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_5", "FICA/Med Tax On Tips - May", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_6", "FICA/Med Tax On Tips - June", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_7", "FICA/Med Tax On Tips - July", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_8", "FICA/Med Tax On Tips - August", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_9", "FICA/Med Tax On Tips - September", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_10", "FICA/Med Tax On Tips - October", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_11", "FICA/Med Tax On Tips - November", Connector.FieldTypeIdCurrency);
            upr00901.AddField("FICAMed_Tax_On_Tips_12", "FICA/Med Tax On Tips - December", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_1", "Uncollected FICA/Med Tax - January", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_2", "Uncollected FICA/Med Tax - February", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_3", "Uncollected FICA/Med Tax - March", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_4", "Uncollected FICA/Med Tax - April", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_5", "Uncollected FICA/Med Tax - May", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_6", "Uncollected FICA/Med Tax - June", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_7", "Uncollected FICA/Med Tax - July", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_8", "Uncollected FICA/Med Tax - August", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_9", "Uncollected FICA/Med Tax - September", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_10", "Uncollected FICA/Med Tax - October", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_11", "Uncollected FICA/Med Tax - November", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Uncollected_FICAMed_Tax_12", "Uncollected FICA/Med Tax - December", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_1", "Reported Receipts - January", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_2", "Reported Receipts - February", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_3", "Reported Receipts - March", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_4", "Reported Receipts - April", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_5", "Reported Receipts - May", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_6", "Reported Receipts - June", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_7", "Reported Receipts - July", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_8", "Reported Receipts - August", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_9", "Reported Receipts - September", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_10", "Reported Receipts - October", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_11", "Reported Receipts - November", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Reported_Receipts12_12", "Reported Receipts - December", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_1", "Charged Receipts - January", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_2", "Charged Receipts - February", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_3", "Charged Receipts - March", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_4", "Charged Receipts - April", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_5", "Charged Receipts - May", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_6", "Charged Receipts - June", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_7", "Charged Receipts - July", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_8", "Charged Receipts - August", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_9", "Charged Receipts - September", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_10", "Charged Receipts - October", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_11", "Charged Receipts - November", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Charged_Receipts12_12", "Charged Receipts - December", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_1", "Allocated Tips - January", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_2", "Allocated Tips - February", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_3", "Allocated Tips - March", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_4", "Allocated Tips - April", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_5", "Allocated Tips - May", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_6", "Allocated Tips - June", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_7", "Allocated Tips - July", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_8", "Allocated Tips - August", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_9", "Allocated Tips - September", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_10", "Allocated Tips - October", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_11", "Allocated Tips - November", Connector.FieldTypeIdCurrency);
            upr00901.AddField("Allocated_Tips_Array_12", "Allocated Tips - December", Connector.FieldTypeIdCurrency);

            var gender = upr00100.AddField("GENDER", "Gender", Connector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });
            
            var ethnicOrigin = upr00100.AddField("ETHNORGN", "Ethnic Origin", Connector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan Native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific Islander" });
            
            var whichCashAccountForPay = upr00100.AddField("WCACFPAY", "Which Cash Account For Pay", Connector.FieldTypeIdEnum);
            whichCashAccountForPay.AddListItems(0, new List<string> { "Checkbook", "Employee" });
            
            var vacationAccrualMethod = upr00100.AddField("VACCRMTH", "Vacation Accrual method", Connector.FieldTypeIdEnum);
            vacationAccrualMethod.AddListItems(0, new List<string> { "Hours Worked", "Amount" });

            var sickTimeAccrualMethod = upr00100.AddField("STMACMTH", "Sick Time Accrual method", Connector.FieldTypeIdEnum);
            sickTimeAccrualMethod.AddListItems(0, new List<string> { "Hours Worked", "Amount" });
            
            var federalFilingStatus = upr00300.AddField("FDFLGSTS", "Federal Filing status", Connector.FieldTypeIdEnum);
            federalFilingStatus.AddListItems(1, new List<string> { "Exempt" });

            var eicFilingStatus = upr00300.AddField("EICFLGST", "EIC Filing status", Connector.FieldTypeIdEnum);
            eicFilingStatus.AddListItems(1, new List<string> { "Not Eligible" });
            
            var hrStatus = upr00100.AddField("HRSTATUS", "HR status", Connector.FieldTypeIdEnum);
            hrStatus.AddListItems(1, new List<string> { "Active", "Family Leave", "Leave of Absence", "Maternity", "Retired", "Separated", "Suspended", "Terminated", "Other" });

            var maritalStatus = upr00100.AddField("MARITALSTATUS", "Marital status", Connector.FieldTypeIdEnum);
            maritalStatus.AddListItems(1, new List<string> { "Married", "Single", "N/A" });
            
            var typeOfEmployment = upr00100.AddField("EMPLOYMENTTYPE", "Type of Employment", Connector.FieldTypeIdEnum);
            typeOfEmployment.AddListItems(1, new List<string> { "Full Time Regular", "Full Time Temp", "Part Time Regular", "Part Time Temp", "Intern", "Other" });
        }

    }
}
