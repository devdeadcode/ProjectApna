namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalBillingInfo
    {

        public enum PaypalBillingInfoLanguage
        {
            None,
            da_DK, 
            de_DE, 
            en_AU, 
            en_GB, 
            en_US, 
            es_ES, 
            es_XC, 
            fr_CA, 
            fr_FR, 
            fr_XC, 
            he_IL, 
            id_ID, 
            it_IT, 
            ja_JP, 
            nl_NL, 
            no_NO, 
            pl_PL, 
            pt_BR, 
            pt_PT, 
            ru_RU, 
            sv_SE, 
            th_TH, 
            tr_TR, 
            zh_CN, 
            zh_HK, 
            zh_TW, 
            zh_XC
        }

        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string business_name { get; set; }
        public PaypalAddress address { get; set; }
        public PaypalBillingInfoLanguage language { get; set; }
        public string additional_info { get; set; }

    }
}