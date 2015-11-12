using System.Collections.Generic;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    class HumanResourcesModule : DynamicsGpModule
    {

        private const short HumanResourcesSmartListApplicant = 20;
        private const short HumanResourcesSmartListEmployeeEducation = 23;
        private const short HumanResourcesSmartListApplicantEducation = 24;
        private const short HumanResourcesSmartListEmployeeBenefit = 25;
        private const short HumanResourcesSmartListPostDatedPayRates = 26;
        
        public HumanResourcesModule(DynamicsGpConnector connector) : base(connector)
        {
            Id = 414;
            Name = "Human Resources";
            ParentConnector = connector;
            Installed = connector.ObjectExists("HR2APP12", "U");
        }

        public override void AddEntities()
        {
            Entities.Add(GetApplicantEntity());
            Entities.Add(GetEmployeeEducationEntity());
            Entities.Add(GetApplicantEducationEntity());
            Entities.Add(GetEmployeeBenefitEntity());
            Entities.Add(GetPostDatedPayRateEntity());
        }

        public ConnectorEntity GetApplicantEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(HumanResourcesSmartListApplicant), "Applicants", ParentConnector);

            var hr2App12 = entity.AddTable("HR2APP12");
            hr2App12.Database = ParentConnector.Database;

            AddApplicantEntityFields(hr2App12);

            return entity;
        }
        public void AddApplicantEntityFields(ConnectorTable hr2App12)
        {
            hr2App12.AddField("FFIRSTNAME_I", "First name", Connector.FieldTypeIdString, true);
            hr2App12.AddField("LLASTNAME_I", "Last name", Connector.FieldTypeIdString, true);
            hr2App12.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString, true);
            hr2App12.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString, true);
            hr2App12.AddField("CITY", "City", Connector.FieldTypeIdString, true);
            hr2App12.AddField("STATE", "State", Connector.FieldTypeIdString, true);
            hr2App12.AddField("ZIPCODE_I", "Zip code", Connector.FieldTypeIdString, true);
            hr2App12.AddField("HOMEPHONE", "Home phone", Connector.FieldTypeIdPhone, true);
            hr2App12.AddField("WORKPHONE", "Work phone", Connector.FieldTypeIdPhone, true);
            hr2App12.AddField("APPLICANTNUMBER_I", "Applicant number", Connector.FieldTypeIdInteger);
            hr2App12.AddField("SSN_I", "ISSN", Connector.FieldTypeIdString);
            hr2App12.AddField("MIDLNAME", "Middle name", Connector.FieldTypeIdString);
            hr2App12.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            hr2App12.AddField("EXT_I", "Extension", Connector.FieldTypeIdString);
            hr2App12.AddField("ISEQUENCENUMBER_I", "Sequence number", Connector.FieldTypeIdInteger);
            hr2App12.AddField("APPLYDATE_I", "Apply date", Connector.FieldTypeIdDate);
            hr2App12.AddField("REQUISITIONNUMBER_I", "Requisition number", Connector.FieldTypeIdInteger);
            hr2App12.AddField("COMPANYCODE_I", "Company code", Connector.FieldTypeIdString);
            hr2App12.AddField("DIVISIONCODE_I", "Division code", Connector.FieldTypeIdString);
            hr2App12.AddField("DEPARTMENTCODE_I", "Department code", Connector.FieldTypeIdString);
            hr2App12.AddField("POSITIONCODE_I", "Position code", Connector.FieldTypeIdString);
            hr2App12.AddField("LOCATNID", "Location code", Connector.FieldTypeIdString);
            hr2App12.AddField("DSCRIPTN", "Reject comment", Connector.FieldTypeIdString);
            hr2App12.AddField("RELOCATION_I", "Relocation", Connector.FieldTypeIdInteger);
            hr2App12.AddField("REPLYLETTERSENT_I", "Reply letter sent", Connector.FieldTypeIdYesNo);
            hr2App12.AddField("COLORCODE_I", "Color code", Connector.FieldTypeIdInteger);
            hr2App12.AddField("COLORSTRING_I", "Color string", Connector.FieldTypeIdString);
            hr2App12.AddField("REFERENCESOURCE_I", "Description", Connector.FieldTypeIdString);
            hr2App12.AddField("HANDICAPPED", "Disabled", Connector.FieldTypeIdYesNo);
            hr2App12.AddField("VETERAN", "Veteran", Connector.FieldTypeIdYesNo);
            hr2App12.AddField("DISABLEDVETERAN", "Special disabled veteran", Connector.FieldTypeIdYesNo);
            hr2App12.AddField("VIETNAMVETERAN", "Vietnam era veteran", Connector.FieldTypeIdYesNo);
            hr2App12.AddField("NOTESINDEX_I", "Notes index", Connector.FieldTypeIdInteger);
            hr2App12.AddField("CHANGEBY_I", "Change by", Connector.FieldTypeIdString);
            hr2App12.AddField("CHANGEDATE_I", "Change date", Connector.FieldTypeIdDate);

            var status = hr2App12.AddField("STATUS0_I", "Status", Connector.FieldTypeIdEnum);
            status.AddListItems(1, new List<string> { "Active", "Open", "Rejected", "Hired", "Other" });

            var rejectReason = hr2App12.AddField("REJECTREASON_I", "Reject Reason", Connector.FieldTypeIdEnum);
            rejectReason.AddListItems(1, new List<string> { "Testing", "Interview", "References", "Experience", "Education", "Other" });

            var refSource = hr2App12.AddField("REFSOURCEDDL_I", "Reference source", Connector.FieldTypeIdEnum);
            refSource.AddListItems(1, new List<string> { "Word of mouth", "Referred to by an employee", "Referred to by an agency", "Newspaper", "Other", "Internet" });

            var gender = hr2App12.AddField("GENDERGB_I", "Gender", Connector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });

            var ethnicOrigin = hr2App12.AddField("EEOETHNICORIGIN_I", "Ethnic origin", Connector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific islander" });

            var age = hr2App12.AddField("EEOAGE_I", "Age", Connector.FieldTypeIdEnum);
            age.AddListItems(1, new List<string> { "18 - 25", "26 - 35", "36 - 45", "46 - 55", "56 +", "Unknown", "00 - 17" });
        }

        public ConnectorEntity GetEmployeeEducationEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(HumanResourcesSmartListEmployeeEducation), "Employee education", ParentConnector);

            var upr00112 = entity.AddTable("UPR00112");

            var upr00100 = entity.AddTable("UPR00100", "UPR00112", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00100.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00102 = entity.AddTable("UPR00102", "UPR00100", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00102.AddJoinFields("EMPLOYID", "EMPLOYID");
            upr00102.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddEmployeeEducationEntityFields(upr00112, upr00100, upr00102);

            return entity;
        }
        public void AddEmployeeEducationEntityFields(ConnectorTable upr00112, ConnectorTable upr00100, ConnectorTable upr00102)
        {
            upr00100.AddField("FRSTNAME", "First name", Connector.FieldTypeIdString, true);
            upr00100.AddField("LASTNAME", "Last name", Connector.FieldTypeIdString, true);
            upr00112.AddField("UNIVERSITY", "University", Connector.FieldTypeIdString, true);
            upr00112.AddField("MAJOR", "Major", Connector.FieldTypeIdString, true);
            upr00112.AddField("GRADUATIONYEAR", "Graduation year", Connector.FieldTypeIdString, true);
            upr00112.AddField("DEGREE", "Degree", Connector.FieldTypeIdString, true);
            upr00100.AddField("EMPLOYID", "Employee ID", Connector.FieldTypeIdString);
            upr00112.AddField("YEARS", "Years", Connector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Notes index", Connector.FieldTypeIdInteger);
            upr00100.AddField("CHANGEBY_I", "Change by", Connector.FieldTypeIdString);
            upr00100.AddField("CHANGEDATE_I", "Change date", Connector.FieldTypeIdDate);
            upr00100.AddField("EMPLCLAS", "Employee class", Connector.FieldTypeIdString);
            upr00100.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo);
            upr00100.AddField("MIDLNAME", "Middle name", Connector.FieldTypeIdString);
            upr00100.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
            upr00100.AddField("BRTHdate", "Birth date", Connector.FieldTypeIdDate);
            upr00100.AddField("Calc_Min_Wage_Bal", "Calculate minimum wage balance", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DIVISIONCODE_I", "Division code", Connector.FieldTypeIdString);
            upr00100.AddField("DEPRTMNT", "Department", Connector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job Title", Connector.FieldTypeIdString);
            upr00100.AddField("SUPERVISORCODE_I", "Supervisor code", Connector.FieldTypeIdString);
            upr00100.AddField("LOCATNID", "Location ID", Connector.FieldTypeIdString);
            upr00100.AddField("ACTINDX", "Account index", Connector.FieldTypeIdInteger);
            upr00100.AddField("WKHRPRYR", "Work hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("STRTdate", "Start date", Connector.FieldTypeIdDate);
            upr00100.AddField("DEMPINAC", "Date employee inactivated", Connector.FieldTypeIdDate);
            upr00100.AddField("RSNEMPIN", "Reason employee inactivated", Connector.FieldTypeIdString);
            upr00100.AddField("MINETPAY", "Minimum net pay", Connector.FieldTypeIdCurrency);
            upr00100.AddField("SUTASTAT", "SUTA state", Connector.FieldTypeIdString);
            upr00100.AddField("WRKRCOMP", "Workers comp", Connector.FieldTypeIdString);
            upr00100.AddField("ATACRVAC", "Auto accrue vacation", Connector.FieldTypeIdYesNo);
            upr00100.AddField("NOTEINDX", "Notes index", Connector.FieldTypeIdInteger);
            upr00100.AddField("VACCRAMT", "Vacation accrual amount", Connector.FieldTypeIdInteger);
            upr00100.AddField("VACAPRYR", "Vacation hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("VACAVLBL", "Vacation available", Connector.FieldTypeIdInteger);
            upr00100.AddField("WRNVCNFLSBLWZR", "Warn when vacation falls below zero", Connector.FieldTypeIdYesNo);
            upr00100.AddField("ATACRSTM", "Auto accrue sick time", Connector.FieldTypeIdYesNo);
            upr00100.AddField("SKTMACAM", "Sick time accrual amount", Connector.FieldTypeIdInteger);
            upr00100.AddField("SIKTIMAV", "Sick time available", Connector.FieldTypeIdInteger);
            upr00100.AddField("SKTMHPYR", "Sick time hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("WRNSTFLSBLWZR", "Warn when sick time falls below zero", Connector.FieldTypeIdYesNo);
            upr00100.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            upr00100.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            upr00100.AddField("BENADJdate", "Benefit adjustment date", Connector.FieldTypeIdDate);
            upr00100.AddField("LASTDAYWORKED_I", "Last day worked", Connector.FieldTypeIdDate);
            upr00100.AddField("BIRTHDAY", "Birth day", Connector.FieldTypeIdInteger);
            upr00100.AddField("BIRTHMONTH", "Birth month", Connector.FieldTypeIdString);
            upr00100.AddField("SPOUSE", "Spouse", Connector.FieldTypeIdString);
            upr00100.AddField("SPOUSESSN", "Spouse SSN", Connector.FieldTypeIdString);
            upr00100.AddField("NICKNAME", "Nickname", Connector.FieldTypeIdString);
            upr00100.AddField("ALTERNATENAME", "Alternate name", Connector.FieldTypeIdString);
            upr00100.AddField("DATEOFLASTREVIEW_I", "Date of last review", Connector.FieldTypeIdDate);
            upr00100.AddField("DATEOFNEXTREVIEW_I", "Date of next review", Connector.FieldTypeIdDate);
            upr00100.AddField("BENEFITEXPIRE_I", "Benefit expiry date", Connector.FieldTypeIdDate);
            upr00100.AddField("HANDICAPPED", "Handicapped", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VETERAN", "Veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VIETNAMVETERAN", "Vietnam veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DISABLEDVETERAN", "Disabled veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("UNIONEMPLOYEE", "Union employee", Connector.FieldTypeIdYesNo);
            upr00100.AddField("SMOKER_I", "Smoker", Connector.FieldTypeIdYesNo);
            upr00100.AddField("CITIZEN", "Citizen", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VERIFIED", "Verified", Connector.FieldTypeIdYesNo);
            upr00100.AddField("I9RENEW", "I9 renew", Connector.FieldTypeIdDate);
            upr00100.AddField("Primary_Pay_Record", "Primary pay record", Connector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            upr00100.AddField("NOTEINDX2", "Note index 2", Connector.FieldTypeIdInteger);
            upr00100.AddField("UNIONCD", "Union code", Connector.FieldTypeIdString);
            upr00100.AddField("RATECLSS", "Rate class", Connector.FieldTypeIdString);
            upr00100.AddField("FEDCLSSCD", "Federal classification code", Connector.FieldTypeIdString);
            upr00102.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            upr00102.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            upr00102.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            upr00102.AddField("CITY", "City", Connector.FieldTypeIdString);
            upr00102.AddField("STATE", "State", Connector.FieldTypeIdString);
            upr00102.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            upr00102.AddField("COUNTY", "County", Connector.FieldTypeIdString);
            upr00102.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            upr00102.AddField("PHONE1", "Phone 1", Connector.FieldTypeIdPhone);
            upr00102.AddField("PHONE2", "Phone 2", Connector.FieldTypeIdPhone);
            upr00102.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            upr00102.AddField("FAX", "Fax", Connector.FieldTypeIdPhone);
            upr00102.AddField("Foreign_Address", "Foreign address", Connector.FieldTypeIdYesNo);
            upr00102.AddField("Foreign_StateProvince", "Foreign state/province", Connector.FieldTypeIdString);
            upr00102.AddField("Foreign_Postal_Code", "Foreign postal code", Connector.FieldTypeIdString);
            upr00102.AddField("CCode", "Country code", Connector.FieldTypeIdString);

            var status = upr00100.AddField("GENDER", "Gender", Connector.FieldTypeIdEnum);
            status.AddListItems(1, new List<string> { "Male", "Female", "N/A" });

            var ethnicOrigin = upr00100.AddField("ETHNORGN", "Ethnic origin", Connector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific islander" });
            
            var vacationAccrualMethod = upr00100.AddField("VACCRMTH", "Vacation accrual method", Connector.FieldTypeIdEnum);
            vacationAccrualMethod.AddListItems(0, new List<string> { "Hours worked", "Amount" });
            
            var sickTimeAccrualMethod = upr00100.AddField("STMACMTH", "Sick time accrual method", Connector.FieldTypeIdEnum);
            sickTimeAccrualMethod.AddListItems(0, new List<string> { "Hours worked", "Amount" });
            
            var typeOfEmployment = upr00100.AddField("EMPLOYMENTTYPE", "Type of employment", Connector.FieldTypeIdEnum);
            typeOfEmployment.AddListItems(1, new List<string> { "Full time regular", "Full time temp", "Part time regular", "Part time temp", "Intern", "Other" });
            
            var maritalStatus = upr00100.AddField("MARITALSTATUS", "Marital status", Connector.FieldTypeIdEnum);
            maritalStatus.AddListItems(1, new List<string> { "Married", "Single", "N/A" });

            var hrStatus = upr00100.AddField("HRSTATUS", "HR status", Connector.FieldTypeIdEnum);
            hrStatus.AddListItems(1, new List<string> { "Active", "Family leave", "Leave of absence", "Maternity", "Retired", "Separated", "Suspended", "Terminated", "Other" });
        }

        public ConnectorEntity GetApplicantEducationEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(HumanResourcesSmartListApplicantEducation), "Applicant education", ParentConnector);

            var hr2App04 = entity.AddTable("HR2APP04");
            hr2App04.Database = ParentConnector.Database;

            var hr2App12 = entity.AddTable("HR2APP12", "HR2APP04", ConnectorTable.ConnectorTableJoinType.Inner);
            hr2App12.Database = ParentConnector.Database;
            hr2App12.AddJoinFields("APPLICANTNUMBER_I", "APPLICANTNUMBER_I");

            AddApplicantEducationEntityFields(hr2App04, hr2App12);

            entity.AddCalculation("2 - HR2APP12.RELOCATION_I", "Relocation", Connector.FieldTypeIdYesNo);

            return entity;
        }
        public void AddApplicantEducationEntityFields(ConnectorTable hr2App04, ConnectorTable hr2App12)
        {
            hr2App12.AddField("FFIRSTNAME_I", "First name", Connector.FieldTypeIdString, true);
            hr2App12.AddField("LLASTNAME_I", "Last name", Connector.FieldTypeIdString, true);
            hr2App04.AddField("UNIVERSITY", "University", Connector.FieldTypeIdString, true);
            hr2App04.AddField("MAJOR", "Major", Connector.FieldTypeIdString, true);
            hr2App04.AddField("GRADUATIONYEAR", "Graduation year", Connector.FieldTypeIdString, true);
            hr2App04.AddField("DEGREE", "Degree", Connector.FieldTypeIdString, true);
            hr2App12.AddField("APPLICANTNUMBER_I", "Applicant number", Connector.FieldTypeIdInteger);
            hr2App12.AddField("SSN_I", "ISSN", Connector.FieldTypeIdString);
            hr2App12.AddField("MIDLNAME", "Middle name", Connector.FieldTypeIdString);
            hr2App12.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            hr2App12.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            hr2App12.AddField("CITY", "City", Connector.FieldTypeIdString);
            hr2App12.AddField("STATE", "State", Connector.FieldTypeIdString);
            hr2App12.AddField("ZIPCODE_I", "Zip code", Connector.FieldTypeIdString);
            hr2App12.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            hr2App12.AddField("HOMEPHONE", "Home Phone", Connector.FieldTypeIdPhone);
            hr2App12.AddField("WORKPHONE", "Work Phone", Connector.FieldTypeIdPhone);
            hr2App12.AddField("EXT_I", "Extension", Connector.FieldTypeIdString);
            hr2App12.AddField("ISEQUENCENUMBER_I", "Sequence number", Connector.FieldTypeIdInteger);
            hr2App12.AddField("APPLYDATE_I", "Apply date", Connector.FieldTypeIdDate);
            hr2App12.AddField("REQUISITIONNUMBER_I", "Requisition number", Connector.FieldTypeIdInteger);
            hr2App12.AddField("COMPANYCODE_I", "Company code", Connector.FieldTypeIdString);
            hr2App12.AddField("DIVISIONCODE_I", "Division code", Connector.FieldTypeIdString);
            hr2App12.AddField("DEPARTMENTCODE_I", "Department code", Connector.FieldTypeIdString);
            hr2App12.AddField("POSITIONCODE_I", "Position code", Connector.FieldTypeIdString);
            hr2App12.AddField("LOCATNID", "Location code", Connector.FieldTypeIdString);
            hr2App12.AddField("REFERENCESOURCE_I", "Description", Connector.FieldTypeIdString);
            hr2App12.AddField("REPLYLETTERSENT_I", "Reply Letter Sent", Connector.FieldTypeIdYesNo);
            hr2App12.AddField("COLORCODE_I", "Color code", Connector.FieldTypeIdInteger);
            hr2App12.AddField("COLORSTRING_I", "Color string", Connector.FieldTypeIdString);
            hr2App12.AddField("REFERENCESOURCE_I", "Reference source - other", Connector.FieldTypeIdString);
            hr2App12.AddField("HANDICAPPED", "Handicapped", Connector.FieldTypeIdYesNo);
            hr2App12.AddField("VETERAN", "Veteran", Connector.FieldTypeIdYesNo);
            hr2App12.AddField("DISABLEDVETERAN", "Disabled veteran", Connector.FieldTypeIdYesNo);
            hr2App12.AddField("VIETNAMVETERAN", "Vietnam veteran", Connector.FieldTypeIdYesNo);
            hr2App04.AddField("NOTESINDEX_I", "Note index (applicant)", Connector.FieldTypeIdInteger);
            hr2App04.AddField("CHANGEBY_I", "Change by (applicant)", Connector.FieldTypeIdString);
            hr2App04.AddField("CHANGEDATE_I", "Change date (applicant)", Connector.FieldTypeIdDate);
            hr2App04.AddField("YEARS", "Years", Connector.FieldTypeIdString);
            hr2App04.AddField("NOTESINDEX_I", "Notes index", Connector.FieldTypeIdInteger);
            hr2App04.AddField("CHANGEBY_I", "Change by", Connector.FieldTypeIdString);
            hr2App04.AddField("CHANGEDATE_I", "Change date", Connector.FieldTypeIdDate);

            var status = hr2App12.AddField("STATUS0_I", "Status", Connector.FieldTypeIdEnum);
            status.AddListItems(1, new List<string> { "Active", "Open", "Rejected", "Hired", "Other" });
            
            var rejectionReason = hr2App12.AddField("REJECTREASON_I", "Rejection reason", Connector.FieldTypeIdEnum);
            rejectionReason.AddListItems(1, new List<string> { "Testing", "Interview", "References", "Experience", "Education", "Other" });
            
            var referenceSource = hr2App12.AddField("REFSOURCEDDL_I", "Reference source", Connector.FieldTypeIdEnum);
            referenceSource.AddListItems(1, new List<string> { "Word of mouth", "Referred to by an employee", "Referred to by an agency", "Newspaper", "Other", "Internet" });
            
            var gender = hr2App12.AddField("GENDERGB_I", "Gender", Connector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });
            
            var ethnicOrigin = hr2App12.AddField("EEOETHNICORIGIN_I", "Ethnic origin", Connector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific islander" });

            var age = hr2App12.AddField("EEOAGE_I", "Age", Connector.FieldTypeIdEnum);
            age.AddListItems(1, new List<string> { "18 - 25", "26 - 35", "36 - 45", "46 - 55", "56 +", "Unknown", "00 - 17" });
        }

        public ConnectorEntity GetEmployeeBenefitEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(HumanResourcesSmartListEmployeeBenefit), "Employee benefits", ParentConnector);

            var be010130 = entity.AddTable("BE010130");

            var upr00100 = entity.AddTable("UPR00100", "BE010130", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00100.AddJoinFields("EMPLOYID", "EMPID_I");

            var upr00102 = entity.AddTable("UPR00102", "UPR00100", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00102.AddJoinFields("EMPLOYID", "EMPLOYID");
            upr00102.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddEmployeeBenefitEntityFields(be010130, upr00100, upr00102);

            return entity;
        }
        public void AddEmployeeBenefitEntityFields(ConnectorTable be010130, ConnectorTable upr00100, ConnectorTable upr00102)
        {
            be010130.AddField("EMPID_I", "Employee ID", Connector.FieldTypeIdString, true);
            upr00100.AddField("FRSTNAME", "First name", Connector.FieldTypeIdString, true);
            upr00100.AddField("LASTNAME", "Last name", Connector.FieldTypeIdString, true);
            upr00100.AddField("SOCSCNUM", "Social security number", Connector.FieldTypeIdString, true);
            be010130.AddField("BENEFIT", "Benefit", Connector.FieldTypeIdString, true);
            be010130.AddField("BENEFITKIND_I", "Benefit kind", Connector.FieldTypeIdEnum, true);
            be010130.AddField("ELIGIBILITYDATE_I", "Eligibility date", Connector.FieldTypeIdDate, true);
            be010130.AddField("BNFBEGDT", "Benefit beginning date", Connector.FieldTypeIdDate, true);
            be010130.AddField("COSTEMPLOYEE_I", "Cost employee", Connector.FieldTypeIdCurrency, true);
            be010130.AddField("COSTEMPLOYER_I", "Cost employer", Connector.FieldTypeIdCurrency, true);
            be010130.AddField("BENEFITTYPE_I", "Benefit type", Connector.FieldTypeIdString);
            be010130.AddField("IINDEX_I", "Index", Connector.FieldTypeIdInteger);
            be010130.AddField("POLICYNUMBER_I", "Policy number", Connector.FieldTypeIdString);
            be010130.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo);
            be010130.AddField("OVERRIDE_I", "Override", Connector.FieldTypeIdYesNo);
            be010130.AddField("CHECK1_I", "Check 1", Connector.FieldTypeIdYesNo);
            be010130.AddField("CHECK2_I", "Check 2", Connector.FieldTypeIdYesNo);
            be010130.AddField("CHECK3_I", "Check 3", Connector.FieldTypeIdYesNo);
            be010130.AddField("CHECK4_I", "Check 4", Connector.FieldTypeIdYesNo);
            be010130.AddField("CHECK5_I", "Check 5", Connector.FieldTypeIdYesNo);
            be010130.AddField("DATEDUE_I", "Date due", Connector.FieldTypeIdDate);
            be010130.AddField("DSCRIPTN", "Description", Connector.FieldTypeIdString);
            be010130.AddField("COMMENTSTR10_I", "Comment", Connector.FieldTypeIdString);
            be010130.AddField("IBENEFITAMOUNT_I", "Benefit amount", Connector.FieldTypeIdCurrency);
            be010130.AddField("LIFEAMTEMPL_I", "Life amount employee", Connector.FieldTypeIdCurrency);
            be010130.AddField("LIFEAMTSPOUSE_I", "Life amount spouse", Connector.FieldTypeIdCurrency);
            be010130.AddField("LIFEAMTCHILDREN_I", "Life amount children", Connector.FieldTypeIdCurrency);
            be010130.AddField("BNFENDDT", "Benefit end date", Connector.FieldTypeIdDate);
            be010130.AddField("VARBENFT", "Variable benefit", Connector.FieldTypeIdYesNo);
            be010130.AddField("COSTOTHER1_I", "Cost other 1", Connector.FieldTypeIdCurrency);
            be010130.AddField("COSTOTHER2_I", "Cost other 2", Connector.FieldTypeIdCurrency);
            be010130.AddField("I1_I", "i1", Connector.FieldTypeIdInteger);
            be010130.AddField("I2_I", "i2", Connector.FieldTypeIdInteger);
            be010130.AddField("I3_I", "i3", Connector.FieldTypeIdInteger);
            be010130.AddField("I4_I", "i4", Connector.FieldTypeIdInteger);
            be010130.AddField("COSTTOTAL_I", "Cost total", Connector.FieldTypeIdCurrency);
            be010130.AddField("COSTCOBRA_I", "Cost COBRA", Connector.FieldTypeIdCurrency);
            be010130.AddField("BENEFITDEDUCTIBLE_I", "Benefit deductible", Connector.FieldTypeIdCurrency);
            be010130.AddField("BNPAYPMX", "Benefit pay period maximum", Connector.FieldTypeIdCurrency);
            be010130.AddField("BNFYRMAX", "Benefit year maximum", Connector.FieldTypeIdCurrency);
            be010130.AddField("BNFLFMAX", "Benefit lifetime maximum", Connector.FieldTypeIdCurrency);
            be010130.AddField("BENEFICIARYINDEX_I", "Beneficiary index", Connector.FieldTypeIdInteger);
            upr00100.AddField("CHANGEBY_I", "Benefit change by", Connector.FieldTypeIdString);
            upr00100.AddField("CHANGEDATE_I", "Benefit change date", Connector.FieldTypeIdDate);
            be010130.AddField("NOTESINDEX_I", "Note index", Connector.FieldTypeIdInteger);
            be010130.AddField("CONTRIBPRETAX_I", "Contribution pre tax", Connector.FieldTypeIdCurrency);
            be010130.AddField("CONTPRETAXDLR_I", "Contribution pre tax amount", Connector.FieldTypeIdCurrency);
            be010130.AddField("CONTRIBAFTERTAX_I", "Contribution after tax", Connector.FieldTypeIdCurrency);
            be010130.AddField("CONTAFTERTAXDLR_I", "Contribution after tax amount", Connector.FieldTypeIdCurrency);
            be010130.AddField("CONTRIBBONUS_I", "Contribution bonus", Connector.FieldTypeIdCurrency);
            be010130.AddField("CONTRIBBONUSDOLLAR_I", "Contribution bonus amount", Connector.FieldTypeIdCurrency);
            be010130.AddField("HIGHLYCOMPENSATED_I", "Highly compensated", Connector.FieldTypeIdYesNo);
            be010130.AddField("LOANACTIVE_I", "Loan active", Connector.FieldTypeIdYesNo);
            be010130.AddField("TIERSUSED_I", "Tiers used", Connector.FieldTypeIdYesNo);
            be010130.AddField("MAJMEDCOVERAGE_I", "Major medical coverage", Connector.FieldTypeIdInteger);
            be010130.AddField("MAXOUTOFPOCKET_I", "Max out-of-pocket", Connector.FieldTypeIdCurrency);
            be010130.AddField("PRIMARYBENEFICIARY_I", "Primary beneficiary", Connector.FieldTypeIdString);
            be010130.AddField("SECBENEFICIARY_I", "Secondary beneficiary", Connector.FieldTypeIdString);
            upr00100.AddField("EMPLCLAS", "Employee class", Connector.FieldTypeIdString);
            upr00100.AddField("INACTIVE", "Inactive", Connector.FieldTypeIdYesNo);
            upr00100.AddField("MIDLNAME", "Middle name", Connector.FieldTypeIdString);
            upr00100.AddField("ADRSCODE", "Address code", Connector.FieldTypeIdString);
            upr00100.AddField("BRTHdate", "Birth date", Connector.FieldTypeIdDate);
            upr00100.AddField("Calc_Min_Wage_Bal", "Calculate minimum wage balance", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DIVISIONCODE_I", "Division code", Connector.FieldTypeIdString);
            upr00100.AddField("DEPRTMNT", "Department", Connector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job title", Connector.FieldTypeIdString);
            upr00100.AddField("SUPERVISORCODE_I", "Supervisor code", Connector.FieldTypeIdString);
            upr00100.AddField("LOCATNID", "Location ID", Connector.FieldTypeIdString);
            upr00100.AddField("ACTINDX", "Account index", Connector.FieldTypeIdInteger);
            upr00100.AddField("WKHRPRYR", "Work hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("STRTdate", "Start date", Connector.FieldTypeIdDate);
            upr00100.AddField("DEMPINAC", "Date employee inactivated", Connector.FieldTypeIdDate);
            upr00100.AddField("RSNEMPIN", "Reason employee inactivated", Connector.FieldTypeIdString);
            upr00100.AddField("MINETPAY", "Minimum net pay", Connector.FieldTypeIdCurrency);
            upr00100.AddField("SUTASTAT", "SUTA state", Connector.FieldTypeIdString);
            upr00100.AddField("WRKRCOMP", "Workers comp", Connector.FieldTypeIdString);
            upr00100.AddField("ATACRVAC", "Auto accrue vacation", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VACCRAMT", "Vacation accrual amount", Connector.FieldTypeIdInteger);
            upr00100.AddField("VACAPRYR", "Vacation hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("VACAVLBL", "Vacation available", Connector.FieldTypeIdInteger);
            upr00100.AddField("WRNVCNFLSBLWZR", "Warn when vacation falls below zero", Connector.FieldTypeIdYesNo);
            upr00100.AddField("ATACRSTM", "Auto accrue sick time", Connector.FieldTypeIdYesNo);
            upr00100.AddField("SKTMACAM", "Sick time accrual amount", Connector.FieldTypeIdInteger);
            upr00100.AddField("SIKTIMAV", "Sick time available", Connector.FieldTypeIdInteger);
            upr00100.AddField("SKTMHPYR", "Sick time hours per year", Connector.FieldTypeIdInteger);
            upr00100.AddField("WRNSTFLSBLWZR", "Warn when sick time falls below zero", Connector.FieldTypeIdYesNo);
            upr00100.AddField("USERDEF1", "User defined 1", Connector.FieldTypeIdString);
            upr00100.AddField("USERDEF2", "User defined 2", Connector.FieldTypeIdString);
            upr00100.AddField("BENADJdate", "Benefit adjustment date", Connector.FieldTypeIdDate);
            upr00100.AddField("LASTDAYWORKED_I", "Last day worked", Connector.FieldTypeIdDate);
            upr00100.AddField("BIRTHDAY", "Birth day", Connector.FieldTypeIdInteger);
            upr00100.AddField("BIRTHMONTH", "Birth month", Connector.FieldTypeIdString);
            upr00100.AddField("SPOUSE", "Spouse", Connector.FieldTypeIdString);
            upr00100.AddField("SPOUSESSN", "Spouse SSN", Connector.FieldTypeIdString);
            upr00100.AddField("NICKNAME", "Nickname", Connector.FieldTypeIdString);
            upr00100.AddField("ALTERNATENAME", "Alternate name", Connector.FieldTypeIdString);
            upr00100.AddField("DATEOFLASTREVIEW_I", "Date of last review", Connector.FieldTypeIdDate);
            upr00100.AddField("DATEOFNEXTREVIEW_I", "Date of next review", Connector.FieldTypeIdDate);
            upr00100.AddField("BENEFITEXPIRE_I", "Benefit expiry date", Connector.FieldTypeIdDate);
            upr00100.AddField("HANDICAPPED", "Handicapped", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VETERAN", "Veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VIETNAMVETERAN", "Vietnam veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("DISABLEDVETERAN", "Disabled veteran", Connector.FieldTypeIdYesNo);
            upr00100.AddField("UNIONEMPLOYEE", "Union employee", Connector.FieldTypeIdYesNo);
            upr00100.AddField("SMOKER_I", "Smoker", Connector.FieldTypeIdYesNo);
            upr00100.AddField("CITIZEN", "Citizen", Connector.FieldTypeIdYesNo);
            upr00100.AddField("VERIFIED", "Verified", Connector.FieldTypeIdYesNo);
            upr00100.AddField("I9RENEW", "I9 renew", Connector.FieldTypeIdDate);
            upr00100.AddField("Primary_Pay_Record", "Primary pay record", Connector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Note index", Connector.FieldTypeIdInteger);
            upr00100.AddField("NOTEINDX2", "Note index 2", Connector.FieldTypeIdInteger);
            upr00100.AddField("UNIONCD", "Union code", Connector.FieldTypeIdString);
            upr00100.AddField("RATECLSS", "Rate class", Connector.FieldTypeIdString);
            upr00100.AddField("FEDCLSSCD", "Federal classification code", Connector.FieldTypeIdString);
            upr00102.AddField("ADDRESS1", "Address 1", Connector.FieldTypeIdString);
            upr00102.AddField("ADDRESS2", "Address 2", Connector.FieldTypeIdString);
            upr00102.AddField("ADDRESS3", "Address 3", Connector.FieldTypeIdString);
            upr00102.AddField("CITY", "City", Connector.FieldTypeIdString);
            upr00102.AddField("STATE", "State", Connector.FieldTypeIdString);
            upr00102.AddField("ZIPCODE", "Zip code", Connector.FieldTypeIdString);
            upr00102.AddField("COUNTY", "County", Connector.FieldTypeIdString);
            upr00102.AddField("COUNTRY", "Country", Connector.FieldTypeIdString);
            upr00102.AddField("PHONE1", "Phone 1", Connector.FieldTypeIdPhone);
            upr00102.AddField("PHONE2", "Phone 2", Connector.FieldTypeIdPhone);
            upr00102.AddField("PHONE3", "Phone 3", Connector.FieldTypeIdPhone);
            upr00102.AddField("FAX", "Fax", Connector.FieldTypeIdPhone);
            upr00102.AddField("Foreign_Address", "Foreign address", Connector.FieldTypeIdYesNo);
            upr00102.AddField("Foreign_StateProvince", "Foreign state/province", Connector.FieldTypeIdString);
            upr00102.AddField("Foreign_Postal_Code", "Foreign postal code", Connector.FieldTypeIdString);
            upr00102.AddField("CCode", "Country code", Connector.FieldTypeIdString);
            be010130.AddField("DEPYPRMX", "Deduction pay period maximum", Connector.FieldTypeIdCurrency);
            be010130.AddField("DEDYRMAX", "Deduction year maximum", Connector.FieldTypeIdCurrency);
            be010130.AddField("DEDINITBAL", "Deduction initial balance", Connector.FieldTypeIdCurrency);
            be010130.AddField("INACTBENEMPLOYEE", "Inactive benefit employee", Connector.FieldTypeIdYesNo);
            be010130.AddField("INACTBENEMPLR", "Inactive benefit employer", Connector.FieldTypeIdYesNo);

            var completionStatus = be010130.AddField("COMPLETIONSTATUS_I", "Completion status", Connector.FieldTypeIdEnum);
            completionStatus.AddListItems(1, new List<string> { "Complete", "Incomplete benefit", "Incomplete deduction", "Incomplete both", "Incomplete both benefit", "Incomplete both deduction" });

            var benefitStatus = be010130.AddField("BENEFITSTATUS_I", "Benefit status", Connector.FieldTypeIdEnum);
            benefitStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Waived", "Ineligible", "COBRA", "Terminated", "FMLA", "Pending" });

            var benefitFrequency = be010130.AddField("BNFTFREQ", "Benefit frequency", Connector.FieldTypeIdEnum);
            benefitFrequency.AddListItems(1, new List<string> { "Weekly", "Biweekly", "Semimonthly", "Monthly", "Quarterly", "Semiannually", "Annually", "Daily/miscellaneous" });

            var typeOfCode = be010130.AddField("TYPEOFCODE_I", "Type of code", Connector.FieldTypeIdEnum);
            typeOfCode.AddListItems(1, new List<string> { "Benefit", "Deduction", "Both" });

            var gender = upr00100.AddField("GENDER", "Gender", Connector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });

            var ethnicOrigin = upr00100.AddField("ETHNORGN", "Ethnic origin", Connector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific islander" });

            var vacationAccrualMethod = upr00100.AddField("VACCRMTH", "Vacation accrual method", Connector.FieldTypeIdEnum);
            vacationAccrualMethod.AddListItems(1, new List<string> { "Hours worked", "Amount" });

            var sickTimeAccrualMethod = upr00100.AddField("STMACMTH", "Sick time accrual method", Connector.FieldTypeIdEnum);
            sickTimeAccrualMethod.AddListItems(1, new List<string> { "Hours worked", "Amount" });

            var typeOfEmployment = upr00100.AddField("EMPLOYMENTTYPE", "Type of employment", Connector.FieldTypeIdEnum);
            typeOfEmployment.AddListItems(1, new List<string> { "Full time regular", "Full time temp", "Part time regular", "Part time temp", "Intern", "Other" });

            var maritalStatus = upr00100.AddField("MARITALSTATUS", "Marital status", Connector.FieldTypeIdEnum);
            maritalStatus.AddListItems(1, new List<string> { "Married", "Single", "N/A" });

            var hrStatus = upr00100.AddField("HRSTATUS", "HR status", Connector.FieldTypeIdEnum);
            hrStatus.AddListItems(1, new List<string> { "Active", "Family leave", "Leave of absence", "Maternity", "Retired", "Separated", "Suspended", "Terminated", "Other" });

            var benefitMethod = be010130.AddField("BNFTMTHD", "Benefit method", Connector.FieldTypeIdEnum);
            benefitMethod.AddListItems(1, new List<string> { "Percent of gross wages", "Percentage of net wages", "Percentage of deduction", "Fixed amount", "Amount per unit" });

            var benefitFormula = be010130.AddField("BNFFRMLA", "Benefit formula", Connector.FieldTypeIdEnum);
            benefitFormula.AddListItems(1, new List<string> { "Single", "Multiple" });

            var deductionMethod = be010130.AddField("DEDNMTHD", "Deduction method", Connector.FieldTypeIdEnum);
            deductionMethod.AddListItems(1, new List<string> { "Percentage of gross wages", "Percentage of net wages", "Fixed amount", "Amount per unit", "Percentage of earnings wages" });

            var deductionFormula = be010130.AddField("DEDFRMLA", "Deduction formula", Connector.FieldTypeIdEnum);
            deductionFormula.AddListItems(1, new List<string> { "Single", "Multiple" });
        }

        public ConnectorEntity GetPostDatedPayRateEntity()
        {
            var entity = new ConnectorEntity(GetEntityId(HumanResourcesSmartListPostDatedPayRates), "Post dated pay rates", ParentConnector);

            var upr00402 = entity.AddTable("UPR00402");

            var upr00100 = entity.AddTable("UPR00100", "UPR00402", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00100.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00400 = entity.AddTable("UPR00400", "UPR00402", ConnectorTable.ConnectorTableJoinType.Inner);
            upr00400.AddJoinFields("EMPLOYID", "EMPLOYID");
            upr00400.AddJoinFields("PAYRCORD", "PAYRCORD");

            var hr2Uni01 = entity.AddTable("HR2UNI01", "UPR00402");
            hr2Uni01.AddJoinFields("EMPID_I", "EMPLOYID");

            var hrps0400 = entity.AddTable("HRPS0400", "UPR00402");
            hrps0400.AddJoinFields("PYSTPTBLID", "PYSTPTBLID");

            AddPostDatedPayRateEntityFields(upr00402, upr00100, upr00400, hr2Uni01, hrps0400);

            return entity;
        }
        public void AddPostDatedPayRateEntityFields(ConnectorTable upr00402, ConnectorTable upr00100, ConnectorTable upr00400, ConnectorTable hr2Uni01, ConnectorTable hrps0400)
        {
            upr00402.AddField("EMPLOYID", "Employee ID", Connector.FieldTypeIdString, true);
            upr00100.AddField("FRSTNAME", "First name", Connector.FieldTypeIdString, true);
            upr00100.AddField("LASTNAME", "Last name", Connector.FieldTypeIdString, true);
            upr00100.AddField("MIDLNAME", "Middle name", Connector.FieldTypeIdString, true);
            upr00402.AddField("PAYRCORD", "Pay record", Connector.FieldTypeIdString, true);
            upr00402.AddField("Effective_date", "Effective date", Connector.FieldTypeIdDate, true);
            upr00400.AddField("PAYRTAMT", "Current pay rate", Connector.FieldTypeIdCurrency, true);
            upr00402.AddField("PAYRTAMT", "New pay rate", Connector.FieldTypeIdCurrency, true);
            upr00402.AddField("CHANGEREASON_I", "Reason for change", Connector.FieldTypeIdString, true);
            upr00100.AddField("DIVISIONCODE_I", "Division code", Connector.FieldTypeIdString);
            upr00100.AddField("DEPRTMNT", "Department", Connector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job title", Connector.FieldTypeIdString);
            upr00100.AddField("LOCATNID", "Location ID", Connector.FieldTypeIdString);
            upr00100.AddField("STRTdate", "Start date", Connector.FieldTypeIdDate);
            upr00100.AddField("BENADJdate", "Benefit adjustment date", Connector.FieldTypeIdDate);
            upr00100.AddField("Primary_Pay_Record", "Primary pay record", Connector.FieldTypeIdString);
            hr2Uni01.AddField("UNIONNAME_I", "Union code", Connector.FieldTypeIdString);
            hr2Uni01.AddField("SENIORITYDATE_I", "Seniority date", Connector.FieldTypeIdDate);
            upr00402.AddField("PYSTPTBLID", "Pay step table ID", Connector.FieldTypeIdString);
            hrps0400.AddField("Pay_Step_Table_Desc", "Pay step table description", Connector.FieldTypeIdString);
            upr00400.AddField("Step_Effective_date", "Step effective date", Connector.FieldTypeIdDate);
            upr00402.AddField("Step", "Pay step", Connector.FieldTypeIdInteger);

            var baseStepIncreasesOn = upr00400.AddField("Base_Step_Increased_On", "Base step increases On", Connector.FieldTypeIdEnum);
            baseStepIncreasesOn.AddListItems(1, new List<string> { "Hire date", "Adjusted hire date", "Seniority date", "Manual" });

            var unitOfPay = hrps0400.AddField("Pay_Step_Unit_Of_Pay", "Unit of pay", Connector.FieldTypeIdEnum);
            unitOfPay.AddListItems(1, new List<string> { "Hourly", "Salary" });
        }

    }
}
