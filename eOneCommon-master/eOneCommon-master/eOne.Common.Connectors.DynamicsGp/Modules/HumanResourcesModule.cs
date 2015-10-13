using System.Collections.Generic;
using eOne.Common.DataConnectors;

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

        public DataConnectorEntity GetApplicantEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(HumanResourcesSmartListApplicant), "Applicants", ParentConnector);

            var hr2App12 = entity.AddTable("HR2APP12");
            hr2App12.Database = ParentConnector.Database;

            AddApplicantEntityFields(hr2App12);

            return entity;
        }
        public void AddApplicantEntityFields(DataConnectorTable hr2App12)
        {
            hr2App12.AddField("FFIRSTNAME_I", "First Name", DataConnector.FieldTypeIdString, true);
            hr2App12.AddField("LLASTNAME_I", "Last Name", DataConnector.FieldTypeIdString, true);
            hr2App12.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString, true);
            hr2App12.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString, true);
            hr2App12.AddField("CITY", "City", DataConnector.FieldTypeIdString, true);
            hr2App12.AddField("STATE", "State", DataConnector.FieldTypeIdString, true);
            hr2App12.AddField("ZIPCODE_I", "Zip Code", DataConnector.FieldTypeIdString, true);
            hr2App12.AddField("HOMEPHONE", "Home Phone", DataConnector.FieldTypeIdPhone, true);
            hr2App12.AddField("WORKPHONE", "Work Phone", DataConnector.FieldTypeIdPhone, true);
            hr2App12.AddField("APPLICANTNUMBER_I", "Applicant Number", DataConnector.FieldTypeIdInteger);
            hr2App12.AddField("SSN_I", "ISSN", DataConnector.FieldTypeIdString);
            hr2App12.AddField("MIDLNAME", "Middle Name", DataConnector.FieldTypeIdString);
            hr2App12.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            hr2App12.AddField("EXT_I", "Ext", DataConnector.FieldTypeIdString);
            hr2App12.AddField("ISEQUENCENUMBER_I", "Sequence Number", DataConnector.FieldTypeIdInteger);
            hr2App12.AddField("APPLYDATE_I", "Apply Date", DataConnector.FieldTypeIdDate);
            hr2App12.AddField("REQUISITIONNUMBER_I", "Requisition Number", DataConnector.FieldTypeIdInteger);
            hr2App12.AddField("COMPANYCODE_I", "Company Code", DataConnector.FieldTypeIdString);
            hr2App12.AddField("DIVISIONCODE_I", "Division Code", DataConnector.FieldTypeIdString);
            hr2App12.AddField("DEPARTMENTCODE_I", "Department Code", DataConnector.FieldTypeIdString);
            hr2App12.AddField("POSITIONCODE_I", "Position Code", DataConnector.FieldTypeIdString);
            hr2App12.AddField("LOCATNID", "Location Code", DataConnector.FieldTypeIdString);
            hr2App12.AddField("DSCRIPTN", "Reject Comment", DataConnector.FieldTypeIdString);
            hr2App12.AddField("RELOCATION_I", "Relocation", DataConnector.FieldTypeIdInteger);
            hr2App12.AddField("REPLYLETTERSENT_I", "Reply Letter Sent", DataConnector.FieldTypeIdYesNo);
            hr2App12.AddField("COLORCODE_I", "Color Code", DataConnector.FieldTypeIdInteger);
            hr2App12.AddField("COLORSTRING_I", "Color String", DataConnector.FieldTypeIdString);
            hr2App12.AddField("REFERENCESOURCE_I", "Description", DataConnector.FieldTypeIdString);
            hr2App12.AddField("HANDICAPPED", "Disabled", DataConnector.FieldTypeIdYesNo);
            hr2App12.AddField("VETERAN", "Veteran", DataConnector.FieldTypeIdYesNo);
            hr2App12.AddField("DISABLEDVETERAN", "Special Disabled Veteran", DataConnector.FieldTypeIdYesNo);
            hr2App12.AddField("VIETNAMVETERAN", "Vietnam Era Veteran", DataConnector.FieldTypeIdYesNo);
            hr2App12.AddField("NOTESINDEX_I", "Notes Index", DataConnector.FieldTypeIdInteger);
            hr2App12.AddField("CHANGEBY_I", "Change By", DataConnector.FieldTypeIdString);
            hr2App12.AddField("CHANGEDATE_I", "Change Date", DataConnector.FieldTypeIdDate);

            var status = hr2App12.AddField("STATUS0_I", "Status", DataConnector.FieldTypeIdEnum);
            status.AddListItems(1, new List<string> { "Active", "Open", "Rejected", "Hired", "Other" });

            var rejectReason = hr2App12.AddField("REJECTREASON_I", "Reject Reason", DataConnector.FieldTypeIdEnum);
            rejectReason.AddListItems(1, new List<string> { "Testing", "Interview", "References", "Experience", "Education", "Other" });

            var refSource = hr2App12.AddField("REFSOURCEDDL_I", "Ref Source DDL", DataConnector.FieldTypeIdEnum);
            refSource.AddListItems(1, new List<string> { "Word of Mouth", "Referred to by an Employee", "Referred to by an Agency", "Newspaper", "Other", "Internet" });

            var gender = hr2App12.AddField("GENDERGB_I", "Gender", DataConnector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });

            var ethnicOrigin = hr2App12.AddField("EEOETHNICORIGIN_I", "Ethnic Origin", DataConnector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan Native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific Islander" });

            var age = hr2App12.AddField("EEOAGE_I", "Age", DataConnector.FieldTypeIdEnum);
            age.AddListItems(1, new List<string> { "18 - 25", "26 - 35", "36 - 45", "46 - 55", "56 +", "Unknown", "00 - 17" });
        }

        public DataConnectorEntity GetEmployeeEducationEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(HumanResourcesSmartListEmployeeEducation), "Employee education", ParentConnector);

            var upr00112 = entity.AddTable("UPR00112");

            var upr00100 = entity.AddTable("UPR00100", "UPR00112", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00100.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00102 = entity.AddTable("UPR00102", "UPR00100", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00102.AddJoinFields("EMPLOYID", "EMPLOYID");
            upr00102.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddEmployeeEducationEntityFields(upr00112, upr00100, upr00102);

            return entity;
        }
        public void AddEmployeeEducationEntityFields(DataConnectorTable upr00112, DataConnectorTable upr00100, DataConnectorTable upr00102)
        {
            upr00100.AddField("FRSTNAME", "First Name", DataConnector.FieldTypeIdString, true);
            upr00100.AddField("LASTNAME", "Last Name", DataConnector.FieldTypeIdString, true);
            upr00112.AddField("UNIVERSITY", "University", DataConnector.FieldTypeIdString, true);
            upr00112.AddField("MAJOR", "Major", DataConnector.FieldTypeIdString, true);
            upr00112.AddField("GRADUATIONYEAR", "Graduation Year", DataConnector.FieldTypeIdString, true);
            upr00112.AddField("DEGREE", "Degree", DataConnector.FieldTypeIdString, true);
            upr00100.AddField("EMPLOYID", "Employee ID", DataConnector.FieldTypeIdString);
            upr00112.AddField("YEARS", "Years", DataConnector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Notes Index", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("CHANGEBY_I", "Change By", DataConnector.FieldTypeIdString);
            upr00100.AddField("CHANGEDATE_I", "Change Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("EMPLCLAS", "Employee Class", DataConnector.FieldTypeIdString);
            upr00100.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("MIDLNAME", "Middle Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("BRTHDATE", "Birth Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("Calc_Min_Wage_Bal", "Calc Min Wage Bal", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DIVISIONCODE_I", "Division Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("DEPRTMNT", "Department", DataConnector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job Title", DataConnector.FieldTypeIdString);
            upr00100.AddField("SUPERVISORCODE_I", "Supervisor Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("LOCATNID", "Location ID", DataConnector.FieldTypeIdString);
            upr00100.AddField("ACTINDX", "Account Index", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("WKHRPRYR", "Work Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("STRTDATE", "Start Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DEMPINAC", "Date Employee Inactivated", DataConnector.FieldTypeIdDate);
            upr00100.AddField("RSNEMPIN", "Reason Employee Inactivated", DataConnector.FieldTypeIdString);
            upr00100.AddField("MINETPAY", "Minimum Net Pay", DataConnector.FieldTypeIdCurrency);
            upr00100.AddField("SUTASTAT", "SUTA State", DataConnector.FieldTypeIdString);
            upr00100.AddField("WRKRCOMP", "Workers Comp", DataConnector.FieldTypeIdString);
            upr00100.AddField("ATACRVAC", "Auto Accrue Vacation", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("NOTEINDX", "Notes Index", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("VACCRAMT", "Vacation Accrual Amount", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("VACAPRYR", "Vacation Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("VACAVLBL", "Vacation Available", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("WRNVCNFLSBLWZR", "Warn Vacation Falls Before Zero", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("ATACRSTM", "Auto Accrue Sick Time", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("SKTMACAM", "Sick Time Accrual Amount", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("SIKTIMAV", "Sick Time Available", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("SKTMHPYR", "Sick Time Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("WRNSTFLSBLWZR", "Warn Sick Time Falls Before Zero", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            upr00100.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            upr00100.AddField("BENADJDATE", "Ben Adj Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("LASTDAYWORKED_I", "Last Day Worked", DataConnector.FieldTypeIdDate);
            upr00100.AddField("BIRTHDAY", "Birth Day", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("BIRTHMONTH", "Birth Month", DataConnector.FieldTypeIdString);
            upr00100.AddField("SPOUSE", "Spouse", DataConnector.FieldTypeIdString);
            upr00100.AddField("SPOUSESSN", "Spouse SSN", DataConnector.FieldTypeIdString);
            upr00100.AddField("NICKNAME", "NickName", DataConnector.FieldTypeIdString);
            upr00100.AddField("ALTERNATENAME", "Alternate Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("DATEOFLASTREVIEW_I", "Date Of Last Review", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DATEOFNEXTREVIEW_I", "Date Of Next Review", DataConnector.FieldTypeIdDate);
            upr00100.AddField("BENEFITEXPIRE_I", "Benefit Expire", DataConnector.FieldTypeIdDate);
            upr00100.AddField("HANDICAPPED", "Handicapped", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VETERAN", "Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VIETNAMVETERAN", "Vietnam Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DISABLEDVETERAN", "Disabled Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("UNIONEMPLOYEE", "Union Employee", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("SMOKER_I", "Smoker", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("CITIZEN", "Citizen", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VERIFIED", "Verified", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("I9RENEW", "I9renew", DataConnector.FieldTypeIdDate);
            upr00100.AddField("Primary_Pay_Record", "Primary Pay Record", DataConnector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("NOTEINDX2", "Note Index2", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("UNIONCD", "Union Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("RATECLSS", "Rate Class", DataConnector.FieldTypeIdString);
            upr00100.AddField("FEDCLSSCD", "Federal Classification Code", DataConnector.FieldTypeIdString);
            upr00102.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            upr00102.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            upr00102.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            upr00102.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            upr00102.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            upr00102.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            upr00102.AddField("COUNTY", "County", DataConnector.FieldTypeIdString);
            upr00102.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            upr00102.AddField("PHONE1", "Phone 1", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("PHONE2", "Phone 2", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("FAX", "Fax", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("Foreign_Address", "Foreign Address", DataConnector.FieldTypeIdYesNo);
            upr00102.AddField("Foreign_StateProvince", "Foreign State/Province", DataConnector.FieldTypeIdString);
            upr00102.AddField("Foreign_Postal_Code", "Foreign Postal Code", DataConnector.FieldTypeIdString);
            upr00102.AddField("CCode", "Country Code", DataConnector.FieldTypeIdString);

            var status = upr00100.AddField("GENDER", "Gender", DataConnector.FieldTypeIdEnum);
            status.AddListItems(1, new List<string> { "Male", "Female", "N/A" });

            var ethnicOrigin = upr00100.AddField("ETHNORGN", "Ethnic Origin", DataConnector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan Native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific Islander" });
            
            var vacationAccrualMethod = upr00100.AddField("VACCRMTH", "Vacation Accrual Method", DataConnector.FieldTypeIdEnum);
            vacationAccrualMethod.AddListItems(0, new List<string> { "Hours Worked", "Amount" });
            
            var sickTimeAccrualMethod = upr00100.AddField("STMACMTH", "Sick Time Accrual Method", DataConnector.FieldTypeIdEnum);
            sickTimeAccrualMethod.AddListItems(0, new List<string> { "Hours Worked", "Amount" });
            
            var typeOfEmployment = upr00100.AddField("EMPLOYMENTTYPE", "Type of Employment", DataConnector.FieldTypeIdEnum);
            typeOfEmployment.AddListItems(1, new List<string> { "Full Time Regular", "Full Time Temp", "Part Time Regular", "Part Time Temp", "Intern", "Other" });
            
            var maritalStatus = upr00100.AddField("MARITALSTATUS", "Marital Status", DataConnector.FieldTypeIdEnum);
            maritalStatus.AddListItems(1, new List<string> { "Married", "Single", "N/A" });

            var hrStatus = upr00100.AddField("HRSTATUS", "HR Status", DataConnector.FieldTypeIdEnum);
            hrStatus.AddListItems(1, new List<string> { "Active", "Family Leave", "Leave of Absence", "Maternity", "Retired", "Separated", "Suspended", "Terminated", "Other" });
        }

        public DataConnectorEntity GetApplicantEducationEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(HumanResourcesSmartListApplicantEducation), "Applicant education", ParentConnector);

            var hr2App04 = entity.AddTable("HR2APP04");
            hr2App04.Database = ParentConnector.Database;

            var hr2App12 = entity.AddTable("HR2APP12", "HR2APP04", DataConnectorTable.DataConnectorTableJoinType.Inner);
            hr2App12.Database = ParentConnector.Database;
            hr2App12.AddJoinFields("APPLICANTNUMBER_I", "APPLICANTNUMBER_I");

            AddApplicantEducationEntityFields(hr2App04, hr2App12);

            entity.AddCalculation("2 - HR2APP12.RELOCATION_I", "Relocation", DataConnector.FieldTypeIdYesNo);

            return entity;
        }
        public void AddApplicantEducationEntityFields(DataConnectorTable hr2App04, DataConnectorTable hr2App12)
        {
            hr2App12.AddField("FFIRSTNAME_I", "First Name", DataConnector.FieldTypeIdString, true);
            hr2App12.AddField("LLASTNAME_I", "Last Name", DataConnector.FieldTypeIdString, true);
            hr2App04.AddField("UNIVERSITY", "University", DataConnector.FieldTypeIdString, true);
            hr2App04.AddField("MAJOR", "Major", DataConnector.FieldTypeIdString, true);
            hr2App04.AddField("GRADUATIONYEAR", "Graduation Year", DataConnector.FieldTypeIdString, true);
            hr2App04.AddField("DEGREE", "Degree", DataConnector.FieldTypeIdString, true);
            hr2App12.AddField("APPLICANTNUMBER_I", "Applicant Number", DataConnector.FieldTypeIdInteger);
            hr2App12.AddField("SSN_I", "ISSN", DataConnector.FieldTypeIdString);
            hr2App12.AddField("MIDLNAME", "Middle Name", DataConnector.FieldTypeIdString);
            hr2App12.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            hr2App12.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            hr2App12.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            hr2App12.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            hr2App12.AddField("ZIPCODE_I", "Zip Code", DataConnector.FieldTypeIdString);
            hr2App12.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            hr2App12.AddField("HOMEPHONE", "Home Phone", DataConnector.FieldTypeIdPhone);
            hr2App12.AddField("WORKPHONE", "Work Phone", DataConnector.FieldTypeIdPhone);
            hr2App12.AddField("EXT_I", "Ext", DataConnector.FieldTypeIdString);
            hr2App12.AddField("ISEQUENCENUMBER_I", "Sequence Number", DataConnector.FieldTypeIdInteger);
            hr2App12.AddField("APPLYDATE_I", "Apply Date", DataConnector.FieldTypeIdDate);
            hr2App12.AddField("REQUISITIONNUMBER_I", "Requisition Number", DataConnector.FieldTypeIdInteger);
            hr2App12.AddField("COMPANYCODE_I", "Company Code", DataConnector.FieldTypeIdString);
            hr2App12.AddField("DIVISIONCODE_I", "Division Code", DataConnector.FieldTypeIdString);
            hr2App12.AddField("DEPARTMENTCODE_I", "Department Code", DataConnector.FieldTypeIdString);
            hr2App12.AddField("POSITIONCODE_I", "Position Code", DataConnector.FieldTypeIdString);
            hr2App12.AddField("LOCATNID", "Location Code", DataConnector.FieldTypeIdString);
            hr2App12.AddField("REFERENCESOURCE_I", "Description", DataConnector.FieldTypeIdString);
            hr2App12.AddField("REPLYLETTERSENT_I", "Reply Letter Sent", DataConnector.FieldTypeIdYesNo);
            hr2App12.AddField("COLORCODE_I", "Color Code", DataConnector.FieldTypeIdInteger);
            hr2App12.AddField("COLORSTRING_I", "Color String", DataConnector.FieldTypeIdString);
            hr2App12.AddField("REFERENCESOURCE_I", "Reference Source - Other", DataConnector.FieldTypeIdString);
            hr2App12.AddField("HANDICAPPED", "Handicapped", DataConnector.FieldTypeIdYesNo);
            hr2App12.AddField("VETERAN", "Veteran", DataConnector.FieldTypeIdYesNo);
            hr2App12.AddField("DISABLEDVETERAN", "Disabled Veteran", DataConnector.FieldTypeIdYesNo);
            hr2App12.AddField("VIETNAMVETERAN", "Vietnam Veteran", DataConnector.FieldTypeIdYesNo);
            hr2App04.AddField("NOTESINDEX_I", "Notes Index (Applicant", DataConnector.FieldTypeIdInteger);
            hr2App04.AddField("CHANGEBY_I", "Change By (Applicant", DataConnector.FieldTypeIdString);
            hr2App04.AddField("CHANGEDATE_I", "Change Date (Applicant", DataConnector.FieldTypeIdDate);
            hr2App04.AddField("YEARS", "Years", DataConnector.FieldTypeIdString);
            hr2App04.AddField("NOTESINDEX_I", "Notes Index", DataConnector.FieldTypeIdInteger);
            hr2App04.AddField("CHANGEBY_I", "Change By", DataConnector.FieldTypeIdString);
            hr2App04.AddField("CHANGEDATE_I", "Change Date", DataConnector.FieldTypeIdDate);

            var status = hr2App12.AddField("STATUS0_I", "Status", DataConnector.FieldTypeIdEnum);
            status.AddListItems(1, new List<string> { "Active", "Open", "Rejected", "Hired", "Other" });
            
            var rejectionReason = hr2App12.AddField("REJECTREASON_I", "Rejection Reason", DataConnector.FieldTypeIdEnum);
            rejectionReason.AddListItems(1, new List<string> { "Testing", "Interview", "References", "Experience", "Education", "Other" });
            
            var referenceSource = hr2App12.AddField("REFSOURCEDDL_I", "Reference Source", DataConnector.FieldTypeIdEnum);
            referenceSource.AddListItems(1, new List<string> { "Word of Mouth", "Referred to by an Employee", "Referred to by an Agency", "Newspaper", "Other", "Internet" });
            
            var gender = hr2App12.AddField("GENDERGB_I", "Gender", DataConnector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });
            
            var ethnicOrigin = hr2App12.AddField("EEOETHNICORIGIN_I", "Ethnic Origin", DataConnector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan Native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific Islander" });

            var age = hr2App12.AddField("EEOAGE_I", "Age", DataConnector.FieldTypeIdEnum);
            age.AddListItems(1, new List<string> { "18 - 25", "26 - 35", "36 - 45", "46 - 55", "56 +", "Unknown", "00 - 17" });
        }

        public DataConnectorEntity GetEmployeeBenefitEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(HumanResourcesSmartListEmployeeBenefit), "Employee benefits", ParentConnector);

            var be010130 = entity.AddTable("BE010130");

            var upr00100 = entity.AddTable("UPR00100", "BE010130", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00100.AddJoinFields("EMPLOYID", "EMPID_I");

            var upr00102 = entity.AddTable("UPR00102", "UPR00100", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00102.AddJoinFields("EMPLOYID", "EMPLOYID");
            upr00102.AddJoinFields("ADRSCODE", "ADRSCODE");

            AddEmployeeBenefitEntityFields(be010130, upr00100, upr00102);

            return entity;
        }
        public void AddEmployeeBenefitEntityFields(DataConnectorTable be010130, DataConnectorTable upr00100, DataConnectorTable upr00102)
        {
            be010130.AddField("EMPID_I", "Employee ID", DataConnector.FieldTypeIdString, true);
            upr00100.AddField("FRSTNAME", "First Name", DataConnector.FieldTypeIdString, true);
            upr00100.AddField("LASTNAME", "Last Name", DataConnector.FieldTypeIdString, true);
            upr00100.AddField("SOCSCNUM", "Social Security Number", DataConnector.FieldTypeIdString, true);
            be010130.AddField("BENEFIT", "Benefit", DataConnector.FieldTypeIdString, true);
            be010130.AddField("BENEFITKIND_I", "Benefit Kind", DataConnector.FieldTypeIdEnum, true);
            be010130.AddField("ELIGIBILITYDATE_I", "Eligibility Date", DataConnector.FieldTypeIdDate, true);
            be010130.AddField("BNFBEGDT", "Benefit Beginning Date", DataConnector.FieldTypeIdDate, true);
            be010130.AddField("COSTEMPLOYEE_I", "Cost Employee", DataConnector.FieldTypeIdCurrency, true);
            be010130.AddField("COSTEMPLOYER_I", "Cost Employer", DataConnector.FieldTypeIdCurrency, true);
            be010130.AddField("BENEFITTYPE_I", "Benefit Type", DataConnector.FieldTypeIdString);
            be010130.AddField("IINDEX_I", "IIndex", DataConnector.FieldTypeIdInteger);
            be010130.AddField("POLICYNUMBER_I", "Policy Number", DataConnector.FieldTypeIdString);
            be010130.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo);
            be010130.AddField("OVERRIDE_I", "Override", DataConnector.FieldTypeIdYesNo);
            be010130.AddField("CHECK1_I", "Check1", DataConnector.FieldTypeIdYesNo);
            be010130.AddField("CHECK2_I", "Check2", DataConnector.FieldTypeIdYesNo);
            be010130.AddField("CHECK3_I", "Check3", DataConnector.FieldTypeIdYesNo);
            be010130.AddField("CHECK4_I", "Check4", DataConnector.FieldTypeIdYesNo);
            be010130.AddField("CHECK5_I", "Check5", DataConnector.FieldTypeIdYesNo);
            be010130.AddField("DATEDUE_I", "Date Due", DataConnector.FieldTypeIdDate);
            be010130.AddField("DSCRIPTN", "Description", DataConnector.FieldTypeIdString);
            be010130.AddField("COMMENTSTR10_I", "CommentStr10", DataConnector.FieldTypeIdString);
            be010130.AddField("IBENEFITAMOUNT_I", "IBenefit Amount", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("LIFEAMTEMPL_I", "Life Amt Empl", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("LIFEAMTSPOUSE_I", "Life Amt Spouse", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("LIFEAMTCHILDREN_I", "Life Amt Child(ren", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("BNFENDDT", "Benefit End Date", DataConnector.FieldTypeIdDate);
            be010130.AddField("VARBENFT", "Variable Benefit", DataConnector.FieldTypeIdYesNo);
            be010130.AddField("COSTOTHER1_I", "Cost Other 1", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("COSTOTHER2_I", "Cost Other 2", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("I1_I", "i1", DataConnector.FieldTypeIdInteger);
            be010130.AddField("I2_I", "i2", DataConnector.FieldTypeIdInteger);
            be010130.AddField("I3_I", "i3", DataConnector.FieldTypeIdInteger);
            be010130.AddField("I4_I", "i4", DataConnector.FieldTypeIdInteger);
            be010130.AddField("COSTTOTAL_I", "Cost Total", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("COSTCOBRA_I", "Cost COBRA", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("BENEFITDEDUCTIBLE_I", "Benefit Deductible", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("BNPAYPMX", "Benefit Pay Period Max", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("BNFYRMAX", "Benefit Year Max", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("BNFLFMAX", "Benefit Lifetime Max", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("BENEFICIARYINDEX_I", "Beneficiary Index", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("CHANGEBY_I", "Benefit Change By", DataConnector.FieldTypeIdString);
            upr00100.AddField("CHANGEDATE_I", "Benefit Change Date", DataConnector.FieldTypeIdDate);
            be010130.AddField("NOTESINDEX_I", "Notes Index", DataConnector.FieldTypeIdInteger);
            be010130.AddField("CONTRIBPRETAX_I", "Contrib Pre Tax", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("CONTPRETAXDLR_I", "Contrib Pre Tax Dollar", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("CONTRIBAFTERTAX_I", "Contrib After Tax", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("CONTAFTERTAXDLR_I", "Contrib After Tax Dollar", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("CONTRIBBONUS_I", "Contrib Bonus", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("CONTRIBBONUSDOLLAR_I", "Contrib Bonus Dollar", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("HIGHLYCOMPENSATED_I", "Highly Compensated", DataConnector.FieldTypeIdYesNo);
            be010130.AddField("LOANACTIVE_I", "Loan Active", DataConnector.FieldTypeIdYesNo);
            be010130.AddField("TIERSUSED_I", "Tiers Used", DataConnector.FieldTypeIdYesNo);
            be010130.AddField("MAJMEDCOVERAGE_I", "Major Medical Coverage", DataConnector.FieldTypeIdInteger);
            be010130.AddField("MAXOUTOFPOCKET_I", "Max Out-Of-Pocket", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("PRIMARYBENEFICIARY_I", "Primary Beneficiary", DataConnector.FieldTypeIdString);
            be010130.AddField("SECBENEFICIARY_I", "Secondary Beneficiary", DataConnector.FieldTypeIdString);
            upr00100.AddField("EMPLCLAS", "Employee Class", DataConnector.FieldTypeIdString);
            upr00100.AddField("INACTIVE", "Inactive", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("MIDLNAME", "Middle Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("ADRSCODE", "Address Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("BRTHDATE", "Birth Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("Calc_Min_Wage_Bal", "Calc Min Wage Bal", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DIVISIONCODE_I", "Division Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("DEPRTMNT", "Department", DataConnector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job Title", DataConnector.FieldTypeIdString);
            upr00100.AddField("SUPERVISORCODE_I", "Supervisor Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("LOCATNID", "Location ID", DataConnector.FieldTypeIdString);
            upr00100.AddField("ACTINDX", "Account Index", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("WKHRPRYR", "Work Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("STRTDATE", "Start Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DEMPINAC", "Date Employee Inactivated", DataConnector.FieldTypeIdDate);
            upr00100.AddField("RSNEMPIN", "Reason Employee Inactivated", DataConnector.FieldTypeIdString);
            upr00100.AddField("MINETPAY", "Minimum Net Pay", DataConnector.FieldTypeIdCurrency);
            upr00100.AddField("SUTASTAT", "SUTA State", DataConnector.FieldTypeIdString);
            upr00100.AddField("WRKRCOMP", "Workers Comp", DataConnector.FieldTypeIdString);
            upr00100.AddField("ATACRVAC", "Auto Accrue Vacation", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VACCRAMT", "Vacation Accrual Amount", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("VACAPRYR", "Vacation Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("VACAVLBL", "Vacation Available", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("WRNVCNFLSBLWZR", "Warn Vacation Falls Before Zero", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("ATACRSTM", "Auto Accrue Sick Time", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("SKTMACAM", "Sick Time Accrual Amount", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("SIKTIMAV", "Sick Time Available", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("SKTMHPYR", "Sick Time Hours Per Year", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("WRNSTFLSBLWZR", "Warn Sick Time Falls Before Zero", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("USERDEF1", "User Defined 1", DataConnector.FieldTypeIdString);
            upr00100.AddField("USERDEF2", "User Defined 2", DataConnector.FieldTypeIdString);
            upr00100.AddField("BENADJDATE", "Ben Adj Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("LASTDAYWORKED_I", "Last Day Worked", DataConnector.FieldTypeIdDate);
            upr00100.AddField("BIRTHDAY", "Birth Day", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("BIRTHMONTH", "Birth Month", DataConnector.FieldTypeIdString);
            upr00100.AddField("SPOUSE", "Spouse", DataConnector.FieldTypeIdString);
            upr00100.AddField("SPOUSESSN", "Spouse SSN", DataConnector.FieldTypeIdString);
            upr00100.AddField("NICKNAME", "NickName", DataConnector.FieldTypeIdString);
            upr00100.AddField("ALTERNATENAME", "Alternate Name", DataConnector.FieldTypeIdString);
            upr00100.AddField("DATEOFLASTREVIEW_I", "Date Of Last Review", DataConnector.FieldTypeIdDate);
            upr00100.AddField("DATEOFNEXTREVIEW_I", "Date Of Next Review", DataConnector.FieldTypeIdDate);
            upr00100.AddField("BENEFITEXPIRE_I", "Benefit Expire", DataConnector.FieldTypeIdDate);
            upr00100.AddField("HANDICAPPED", "Handicapped", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VETERAN", "Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VIETNAMVETERAN", "Vietnam Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("DISABLEDVETERAN", "Disabled Veteran", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("UNIONEMPLOYEE", "Union Employee", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("SMOKER_I", "Smoker", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("CITIZEN", "Citizen", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("VERIFIED", "Verified", DataConnector.FieldTypeIdYesNo);
            upr00100.AddField("I9RENEW", "I9renew", DataConnector.FieldTypeIdDate);
            upr00100.AddField("Primary_Pay_Record", "Primary Pay Record", DataConnector.FieldTypeIdString);
            upr00100.AddField("NOTEINDX", "Note Index", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("NOTEINDX2", "Note Index2", DataConnector.FieldTypeIdInteger);
            upr00100.AddField("UNIONCD", "Union Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("RATECLSS", "Rate Class", DataConnector.FieldTypeIdString);
            upr00100.AddField("FEDCLSSCD", "Federal Classification Code", DataConnector.FieldTypeIdString);
            upr00102.AddField("ADDRESS1", "Address 1", DataConnector.FieldTypeIdString);
            upr00102.AddField("ADDRESS2", "Address 2", DataConnector.FieldTypeIdString);
            upr00102.AddField("ADDRESS3", "Address 3", DataConnector.FieldTypeIdString);
            upr00102.AddField("CITY", "City", DataConnector.FieldTypeIdString);
            upr00102.AddField("STATE", "State", DataConnector.FieldTypeIdString);
            upr00102.AddField("ZIPCODE", "Zip Code", DataConnector.FieldTypeIdString);
            upr00102.AddField("COUNTY", "County", DataConnector.FieldTypeIdString);
            upr00102.AddField("COUNTRY", "Country", DataConnector.FieldTypeIdString);
            upr00102.AddField("PHONE1", "Phone 1", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("PHONE2", "Phone 2", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("PHONE3", "Phone 3", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("FAX", "Fax", DataConnector.FieldTypeIdPhone);
            upr00102.AddField("Foreign_Address", "Foreign Address", DataConnector.FieldTypeIdYesNo);
            upr00102.AddField("Foreign_StateProvince", "Foreign State/Province", DataConnector.FieldTypeIdString);
            upr00102.AddField("Foreign_Postal_Code", "Foreign Postal Code", DataConnector.FieldTypeIdString);
            upr00102.AddField("CCode", "Country Code", DataConnector.FieldTypeIdString);
            be010130.AddField("DEPYPRMX", "Deduction Pay Period Max", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("DEDYRMAX", "Deduction Year Max", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("DEDINITBAL", "Deduction Initial Balance", DataConnector.FieldTypeIdCurrency);
            be010130.AddField("INACTBENEMPLOYEE", "Inactive Benefit Employee", DataConnector.FieldTypeIdYesNo);
            be010130.AddField("INACTBENEMPLR", "Inactive Benefit Employer", DataConnector.FieldTypeIdYesNo);

            var completionStatus = be010130.AddField("COMPLETIONSTATUS_I", "Completion Status", DataConnector.FieldTypeIdEnum);
            completionStatus.AddListItems(1, new List<string> { "Complete", "Incomplete_Ben", "Incomplete_Ded", "Incomplete_Both", "Incomplete_Both_Ben", "Incomplete_Both_Ded" });

            var benefitStatus = be010130.AddField("BENEFITSTATUS_I", "Benefit Status", DataConnector.FieldTypeIdEnum);
            benefitStatus.AddListItems(1, new List<string> { "Active", "Inactive", "Waived", "Ineligible", "COBRA", "Terminated", "FMLA", "Pending" });

            var benefitFrequency = be010130.AddField("BNFTFREQ", "Benefit Frequency", DataConnector.FieldTypeIdEnum);
            benefitFrequency.AddListItems(1, new List<string> { "Weekly", "Biweekly", "Semimonthly", "Monthly", "Quarterly", "Semiannually", "Annually", "Daily/Miscellaneous" });

            var typeOfCode = be010130.AddField("TYPEOFCODE_I", "Type of Code", DataConnector.FieldTypeIdEnum);
            typeOfCode.AddListItems(1, new List<string> { "Benefit", "Deduction", "Both" });

            var gender = upr00100.AddField("GENDER", "Gender", DataConnector.FieldTypeIdEnum);
            gender.AddListItems(1, new List<string> { "Male", "Female", "N/A" });

            var ethnicOrigin = upr00100.AddField("ETHNORGN", "Ethnic Origin", DataConnector.FieldTypeIdEnum);
            ethnicOrigin.AddListItems(1, new List<string> { "White", "American Indian or Alaskan Native", "Black or African American", "Asian", "Hispanic or Latino", "Two or more races", "N/A", "Native Hawaiian or Pacific Islander" });

            var vacationAccrualMethod = upr00100.AddField("VACCRMTH", "Vacation Accrual Method", DataConnector.FieldTypeIdEnum);
            vacationAccrualMethod.AddListItems(1, new List<string> { "Hours Worked", "Amount" });

            var sickTimeAccrualMethod = upr00100.AddField("STMACMTH", "Sick Time Accrual Method", DataConnector.FieldTypeIdEnum);
            sickTimeAccrualMethod.AddListItems(1, new List<string> { "Hours Worked", "Amount" });

            var typeOfEmployment = upr00100.AddField("EMPLOYMENTTYPE", "Type of Employment", DataConnector.FieldTypeIdEnum);
            typeOfEmployment.AddListItems(1, new List<string> { "Full Time Regular", "Full Time Temp", "Part Time Regular", "Part Time Temp", "Intern", "Other" });

            var maritalStatus = upr00100.AddField("MARITALSTATUS", "Marital Status", DataConnector.FieldTypeIdEnum);
            maritalStatus.AddListItems(1, new List<string> { "Married", "Single", "N/A" });

            var hrStatus = upr00100.AddField("HRSTATUS", "HR Status", DataConnector.FieldTypeIdEnum);
            hrStatus.AddListItems(1, new List<string> { "Active", "Family Leave", "Leave of Absence", "Maternity", "Retired", "Separated", "Suspended", "Terminated", "Other" });

            var benefitMethod = be010130.AddField("BNFTMTHD", "Benefit Method", DataConnector.FieldTypeIdEnum);
            benefitMethod.AddListItems(1, new List<string> { "Percent of Gross Wages", "Percent of Net Wages", "Percent of Deduction", "Fixed Amount", "Amount per Unit" });

            var benefitFormula = be010130.AddField("BNFFRMLA", "Benefit Formula", DataConnector.FieldTypeIdEnum);
            benefitFormula.AddListItems(1, new List<string> { "Single", "Multiple" });

            var deductionMethod = be010130.AddField("DEDNMTHD", "Deduction Method", DataConnector.FieldTypeIdEnum);
            deductionMethod.AddListItems(1, new List<string> { "Percent of Gross Wages", "Percent of Net Wages", "Fixed Amount", "Amount per Unit", "Percent of Earnings Wages" });

            var deductionFormula = be010130.AddField("DEDFRMLA", "Deduction Formula", DataConnector.FieldTypeIdEnum);
            deductionFormula.AddListItems(1, new List<string> { "Single", "Multiple" });
        }

        public DataConnectorEntity GetPostDatedPayRateEntity()
        {
            var entity = new DataConnectorEntity(GetEntityId(HumanResourcesSmartListPostDatedPayRates), "Post dated pay rates", ParentConnector);

            var upr00402 = entity.AddTable("UPR00402");

            var upr00100 = entity.AddTable("UPR00100", "UPR00402", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00100.AddJoinFields("EMPLOYID", "EMPLOYID");

            var upr00400 = entity.AddTable("UPR00400", "UPR00402", DataConnectorTable.DataConnectorTableJoinType.Inner);
            upr00400.AddJoinFields("EMPLOYID", "EMPLOYID");
            upr00400.AddJoinFields("PAYRCORD", "PAYRCORD");

            var hr2Uni01 = entity.AddTable("HR2UNI01", "UPR00402");
            hr2Uni01.AddJoinFields("EMPID_I", "EMPLOYID");

            var hrps0400 = entity.AddTable("HRPS0400", "UPR00402");
            hrps0400.AddJoinFields("PYSTPTBLID", "PYSTPTBLID");

            AddPostDatedPayRateEntityFields(upr00402, upr00100, upr00400, hr2Uni01, hrps0400);

            return entity;
        }
        public void AddPostDatedPayRateEntityFields(DataConnectorTable upr00402, DataConnectorTable upr00100, DataConnectorTable upr00400, DataConnectorTable hr2Uni01, DataConnectorTable hrps0400)
        {
            upr00402.AddField("EMPLOYID", "Employee ID", DataConnector.FieldTypeIdString, true);
            upr00100.AddField("FRSTNAME", "First Name", DataConnector.FieldTypeIdString, true);
            upr00100.AddField("LASTNAME", "Last Name", DataConnector.FieldTypeIdString, true);
            upr00100.AddField("MIDLNAME", "Middle Name", DataConnector.FieldTypeIdString, true);
            upr00402.AddField("PAYRCORD", "Pay Record", DataConnector.FieldTypeIdString, true);
            upr00402.AddField("Effective_Date", "Effective Date", DataConnector.FieldTypeIdDate, true);
            upr00400.AddField("PAYRTAMT", "Current Pay Rate", DataConnector.FieldTypeIdCurrency, true);
            upr00402.AddField("PAYRTAMT", "New Pay Rate", DataConnector.FieldTypeIdCurrency, true);
            upr00402.AddField("CHANGEREASON_I", "Reason for Change", DataConnector.FieldTypeIdString, true);
            upr00100.AddField("DIVISIONCODE_I", "Division Code", DataConnector.FieldTypeIdString);
            upr00100.AddField("DEPRTMNT", "Department", DataConnector.FieldTypeIdString);
            upr00100.AddField("JOBTITLE", "Job Title", DataConnector.FieldTypeIdString);
            upr00100.AddField("LOCATNID", "Location ID", DataConnector.FieldTypeIdString);
            upr00100.AddField("STRTDATE", "Start Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("BENADJDATE", "Ben Adj Date", DataConnector.FieldTypeIdDate);
            upr00100.AddField("Primary_Pay_Record", "Primary Pay Record", DataConnector.FieldTypeIdString);
            hr2Uni01.AddField("UNIONNAME_I", "Union Code", DataConnector.FieldTypeIdString);
            hr2Uni01.AddField("SENIORITYDATE_I", "Seniority Date", DataConnector.FieldTypeIdDate);
            upr00402.AddField("PYSTPTBLID", "Pay Step Table ID", DataConnector.FieldTypeIdString);
            hrps0400.AddField("Pay_Step_Table_Desc", "Pay Step Table Description", DataConnector.FieldTypeIdString);
            upr00400.AddField("Step_Effective_Date", "Step Effective Date", DataConnector.FieldTypeIdDate);
            upr00402.AddField("Step", "Pay Step", DataConnector.FieldTypeIdInteger);

            var baseStepIncreasesOn = upr00400.AddField("Base_Step_Increased_On", "Base Step Increases On", DataConnector.FieldTypeIdEnum);
            baseStepIncreasesOn.AddListItems(1, new List<string> { "Hire Date", "Adjusted Hire Date", "Seniority Date", "Manual" });

            var unitOfPay = hrps0400.AddField("Pay_Step_Unit_Of_Pay", "Unit of Pay", DataConnector.FieldTypeIdEnum);
            unitOfPay.AddListItems(1, new List<string> { "Hourly", "Salary" });
        }

    }
}
