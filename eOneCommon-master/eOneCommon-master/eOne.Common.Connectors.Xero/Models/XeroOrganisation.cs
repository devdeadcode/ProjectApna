using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroOrganisation
    {

        public enum XeroOrganisationVersion
        {
            AU,
            NZ,
            GLOBAL,
            UK,
            US,
            AUONRAMP,
            NZONRAMP,
            GLOBALONRAMP,
            UKONRAMP,
            USONRAMP
        }
        public enum XeroOrganisationStatus
        {
            ACTIVE
        }
        public enum XeroOrganisationEntityType
        {
            COMPANY,
            CHARITY,
            CLUBSOCIETY,
            PARTNERSHIP,
            PRACTICE,
            PERSON,
            SOLETRADER,
            TRUST
        }

        public string APIKey { get; set; }
        public string Name { get; set; }
        public string LegalName { get; set; }
        public bool PaysTax { get; set; }
        public string BaseCurrency { get; set; }
        public string CountryCode { get; set; }
        public bool IsDemoCompany { get; set; }
        public string RegistrationNumber { get; set; }
        public string TaxNumber { get; set; }
        public int FinancialYearEndDay { get; set; }
        public int FinancialYearEndMonth { get; set; }
        public DateTime PeriodLockDate { get; set; }
        public DateTime EndOfYearLockDate { get; set; }
        public DateTime CreatedDateUTC { get; set; }
        public string ShortCode { get; set; }
        public string LineOfBusiness { get; set; }
        public List<XeroExternalLink> ExternalLinks { get; set; }
        public List<XeroAddress> Addresses { get; set; }
        public List<XeroPhone> Phones { get; set; }
        public XeroOrganisationVersion Version { get; set; }
        public XeroOrganisationStatus OrganisationStatus { get; set; }
        public XeroOrganisationEntityType OrganisationEntityType { get; set; }
        public string Timezone { get; set; }
        public string SalesTaxBasis { get; set; }
        public string SalesTaxBasisDescription
        {
            get
            {
                switch (Version)
                {
                    case XeroOrganisationVersion.NZ:
                    case XeroOrganisationVersion.NZONRAMP:
                        switch (SalesTaxBasis)
                        {
                            case "PAYMENTS":
                                return "Payments Basis";
                            case "INVOICE":
                                return "Invoice Basis";
                            default:
                                return "None";
                        }
                    case XeroOrganisationVersion.UK:
                    case XeroOrganisationVersion.UKONRAMP:
                        switch (SalesTaxBasis)
                        {
                            case "CASH":
                                return "Cash Scheme";
                            case "ACCRUAL":
                                return "Accrual Scheme";
                            case "FLATRATECASH":
                                return "Flat Rate Cash Scheme";
                            case "FLATRATEACCRUAL":
                                return "Flat Rate Accrual Scheme";
                            default:
                                return "None";
                        }
                    default:
                        switch (SalesTaxBasis)
                        {
                            case "CASH":
                                return "Cash Basis";
                            case "ACCRUALS":
                                return "Accruals Basis";
                            default:
                                return "None";
                        }
                }
 
            }
        }
        public string SalesTaxPeriod { get; set; }
        public string SalesTaxPeriodDescription
        {
            get
            {
                switch (Version)
                {
                    case XeroOrganisationVersion.AU:
                    case XeroOrganisationVersion.AUONRAMP:
                        switch (SalesTaxPeriod)
                        {
                            case "MONTHLY":
                                return "Monthly";
                            case "QUARTERLY1":
                                return "Quarterly (Option1)";
                            case "QUARTERLY2":
                                return "Quarterly (Option2)";
                            case "QUARTERLY3":
                                return "Quarterly (Option3)";
                            case "ANNUALLY":
                                return "Annually";
                            default:
                                return "None";
                        }
                    case XeroOrganisationVersion.NZ:
                    case XeroOrganisationVersion.NZONRAMP:
                        switch (SalesTaxPeriod)
                        {
                            case "ONEMONTHS":
                                return "Monthly";
                            case "TWOMONTHS":
                                return "2 Monthly";
                            case "SIXMONTHS":
                                return "6 Monthly";
                            default:
                                return "None";
                        }
                    case XeroOrganisationVersion.UK:
                    case XeroOrganisationVersion.UKONRAMP:
                        switch (SalesTaxPeriod)
                        {
                            case "MONTHLY":
                                return "Monthly";
                            case "QUARTERLY":
                                return "Quarterly";
                            case "YEARLY":
                                return "Yearly";
                            default:
                                return "None";
                        }
                    default:
                        switch (SalesTaxPeriod)
                        {
                            case "1MONTHLYS":
                                return "Monthly";
                            case "2MONTHLYS":
                                return "2 Monthly";
                            case "3MONTHLYS":
                                return "3 Monthly";
                            case "6MONTHLYS":
                                return "6 Monthly";
                            case "ANNUALLYS":
                                return "Annually";
                            default:
                                return "None";
                        }
                }
            }
        }
        public XeroOrganisationPaymentTerms PaymentTerms { get; set; }

    }
}

