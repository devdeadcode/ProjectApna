using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace eOne.Common.Connectors
{
    public class ConnectorEntityModel
    {

        // common enums
        public enum IsoCurrency
        {
            [Description("United Arab Emirates Dirham")]
            AED,
            [Description("Afghanistan Afghani")]
            AFN,
            [Description("Albanian Lek")]
            ALL,
            [Description("Armenian Dram")]
            AMD,
            [Description("Netherlands Antilles Guilder")]
            ANG,
            [Description("Angolan Kwanza")]
            AOA,
            [Description("Argentinian Peso")]
            ARS,
            [Description("Australian Dollar")]
            AUD,
            [Description("Aruba Guilder")]
            AWG,
            [Description("Azerbaijan New Manat")]
            AZN,
            [Description("Bosnia and Herzegovina Convertible Marka")]
            BAM,
            [Description("Barbados Dollar")]
            BBD,
            [Description("Bangladesh Taka")]
            BDT,
            [Description("Bulgarian Lev")]
            BGN,
            [Description("Bahrain Dinar")]
            BHD,
            [Description("Burundi Franc")]
            BIF,
            [Description("Bermuda Dollar")]
            BMD,
            [Description("Brunei Darussalam Dollar")]
            BND,
            [Description("Bolivia Boliviano")]
            BOB,
            [Description("Brazil Real")]
            BRL,
            [Description("Bahamas Dollar")]
            BSD,
            [Description("Bhutan Ngultrum")]
            BTN,
            [Description("Botswana Pula")]
            BWP,
            [Description("Belarus Ruble")]
            BYR,
            [Description("Belize Dollar")]
            BZD,
            [Description("Canadian Dollar")]
            CAD,
            [Description("Congo/Kinshasa Franc")]
            CDF,
            [Description("Swiss Franc")]
            CHF,
            [Description("Chilean Peso")]
            CLP,
            [Description("Chinese Yuan Renminbi")]
            CNY,
            [Description("Colombian Peso")]
            COP,
            [Description("Costa Rican Colon")]
            CRC,
            [Description("Cuban Convertible Peso")]
            CUC,
            [Description("Cuban Peso")]
            CUP,
            [Description("Cape Verde Escudo")]
            CVE,
            [Description("Czech Republic Koruna")]
            CZK,
            [Description("Djibouti Franc")]
            DJF,
            [Description("Denmark Krone")]
            DKK,
            [Description("Dominican Republic Peso")]
            DOP,
            [Description("Algerian Dinar")]
            DZD,
            [Description("Egyptian Pound")]
            EGP,
            [Description("Eritrea Nakfa")]
            ERN,
            [Description("Ethiopian Birr")]
            ETB,
            [Description("Euro")]
            EUR,
            [Description("Fiji Dollar")]
            FJD,
            [Description("Falkland Islands (Malvinas) Pound")]
            FKP,
            [Description("United Kingdom Pound")]
            GBP,
            [Description("Georgian Lari")]
            GEL,
            [Description("Guernsey Pound")]
            GGP,
            [Description("Ghana Cedi")]
            GHS,
            [Description("Gibraltar Pound")]
            GIP,
            [Description("Gambia Dalasi")]
            GMD,
            [Description("Guinea Franc")]
            GNF,
            [Description("Guatemala Quetzal")]
            GTQ,
            [Description("Guyana Dollar")]
            GYD,
            [Description("Hong Kong Dollar")]
            HKD,
            [Description("Honduras Lempira")]
            HNL,
            [Description("Croatia Kuna")]
            HRK,
            [Description("Haiti Gourde")]
            HTG,
            [Description("Hungary Forint")]
            HUF,
            [Description("Indonesian Rupiah")]
            IDR,
            [Description("Israeli Shekel")]
            ILS,
            [Description("Isle of Man Pound")]
            IMP,
            [Description("Indian Rupee")]
            INR,
            [Description("Iraq Dinar")]
            IQD,
            [Description("Iran Rial")]
            IRR,
            [Description("Iceland Krona")]
            ISK,
            [Description("Jersey Pound")]
            JEP,
            [Description("Jamaica Dollar")]
            JMD,
            [Description("Jordan Dinar")]
            JOD,
            [Description("Japanese Yen")]
            JPY,
            [Description("Kenyan Shilling")]
            KES,
            [Description("Kyrgyzstan Som")]
            KGS,
            [Description("Cambodian Riel")]
            KHR,
            [Description("Comoros Franc")]
            KMF,
            [Description("North Korean Won")]
            KPW,
            [Description("South Korean Won")]
            KRW,
            [Description("Kuwait Dinar")]
            KWD,
            [Description("Cayman Islands Dollar")]
            KYD,
            [Description("Kazakhstan Tenge")]
            KZT,
            [Description("Laos Kip")]
            LAK,
            [Description("Lebanon Pound")]
            LBP,
            [Description("Sri Lankan Rupee")]
            LKR,
            [Description("Liberia Dollar")]
            LRD,
            [Description("Lesotho Loti")]
            LSL,
            [Description("Libya Dinar")]
            LYD,
            [Description("Morocco Dirham")]
            MAD,
            [Description("Moldova Leu")]
            MDL,
            [Description("Madagascar Ariary")]
            MGA,
            [Description("Macedonia Denar")]
            MKD,
            [Description("Myanmar (Burma) Kyat")]
            MMK,
            [Description("Mongolia Tughrik")]
            MNT,
            [Description("Macau Pataca")]
            MOP,
            [Description("Mauritania Ouguiya")]
            MRO,
            [Description("Mauritius Rupee")]
            MUR,
            [Description("Maldives (Maldive Islands) Rufiyaa")]
            MVR,
            [Description("Malawi Kwacha")]
            MWK,
            [Description("Mexican Peso")]
            MXN,
            [Description("Malaysian Ringgit")]
            MYR,
            [Description("Mozambique Metical")]
            MZN,
            [Description("Namibia Dollar")]
            NAD,
            [Description("Nigeria Naira")]
            NGN,
            [Description("Nicaragua Cordoba")]
            NIO,
            [Description("Norway Krone")]
            NOK,
            [Description("Nepal Rupee")]
            NPR,
            [Description("New Zealand Dollar")]
            NZD,
            [Description("Oman Rial")]
            OMR,
            [Description("Panama Balboa")]
            PAB,
            [Description("Peru Nuevo Sol")]
            PEN,
            [Description("Papua New Guinea Kina")]
            PGK,
            [Description("Philippines Peso")]
            PHP,
            [Description("Pakistan Rupee")]
            PKR,
            [Description("Poland Zloty")]
            PLN,
            [Description("Paraguay Guarani")]
            PYG,
            [Description("Qatar Riyal")]
            QAR,
            [Description("Romania New Leu")]
            RON,
            [Description("Serbia Dinar")]
            RSD,
            [Description("Russia Ruble")]
            RUB,
            [Description("Rwanda Franc")]
            RWF,
            [Description("Saudi Arabia Riyal")]
            SAR,
            [Description("Solomon Islands Dollar")]
            SBD,
            [Description("Seychelles Rupee")]
            SCR,
            [Description("Sudanese Pound")]
            SDG,
            [Description("Swedish Krona")]
            SEK,
            [Description("Singapore Dollar")]
            SGD,
            [Description("Saint Helena Pound")]
            SHP,
            [Description("Sierra Leone Leone")]
            SLL,
            [Description("Somalia Shilling")]
            SOS,
            [Description("Seborga Luigino")]
            SPL,
            [Description("Suriname Dollar")]
            SRD,
            [Description("São Tomé and Príncipe Dobra")]
            STD,
            [Description("El Salvador Colon")]
            SVC,
            [Description("Syria Pound")]
            SYP,
            [Description("Swaziland Lilangeni")]
            SZL,
            [Description("Thailand Baht")]
            THB,
            [Description("Tajikistan Somoni")]
            TJS,
            [Description("Turkmenistan Manat")]
            TMT,
            [Description("Tunisia Dinar")]
            TND,
            [Description("Tonga Pa'anga")]
            TOP,
            [Description("Turkish Lira")]
            TRY,
            [Description("Trinidad and Tobago Dollar")]
            TTD,
            [Description("Tuvalu Dollar")]
            TVD,
            [Description("Taiwan New Dollar")]
            TWD,
            [Description("Tanzania Shilling")]
            TZS,
            [Description("Ukraine Hryvnia")]
            UAH,
            [Description("Uganda Shilling")]
            UGX,
            [Description("United States Dollar")]
            USD,
            [Description("Uruguay Peso")]
            UYU,
            [Description("Uzbekistan Som")]
            UZS,
            [Description("Venezuela Bolivar")]
            VEF,
            [Description("Viet Nam Dong")]
            VND,
            [Description("Vanuatu Vatu")]
            VUV,
            [Description("Samoa Tala")]
            WST,
            [Description("Communauté Financière Africaine (BEAC) CFA Franc BEAC")]
            XAF,
            [Description("East Caribbean Dollar")]
            XCD,
            [Description("International Monetary Fund (IMF) Special Drawing Rights")]
            XDR,
            [Description("Communauté Financière Africaine (BCEAO) Franc")]
            XOF,
            [Description("Comptoirs Français du Pacifique (CFP) Franc")]
            XPF,
            [Description("Yemen Rial")]
            YER,
            [Description("South Africa Rand")]
            ZAR,
            [Description("Zambia Kwacha")]
            ZMW, 
            [Description("Zimbabwe Dollar")]
            ZWD, 
        }
        public enum IsoCountry2Char
        {
            [Description("Afghanistan")]
            AF,
            [Description("Åland Islands")]
            AX,
            [Description("Albania")]
            AL,
            [Description("Algeria")]
            DZ,
            [Description("American Samoa")]
            AS,
            [Description("Andorra")]
            AD,
            [Description("Angola")]
            AO,
            [Description("Anguilla")]
            AI,
            [Description("Antarctica")]
            AQ,
            [Description("Antigua and Barbuda")]
            AG,
            [Description("Argentina")]
            AR,
            [Description("Armenia")]
            AM,
            [Description("Aruba")]
            AW,
            [Description("Australia")]
            AU,
            [Description("Austria")]
            AT,
            [Description("Azerbaijan")]
            AZ,
            [Description("Bahamas")]
            BS,
            [Description("Bahrain")]
            BH,
            [Description("Bangladesh")]
            BD,
            [Description("Barbados")]
            BB,
            [Description("Belarus")]
            BY,
            [Description("Belgium")]
            BE,
            [Description("Belize")]
            BZ,
            [Description("Benin")]
            BJ,
            [Description("Bermuda")]
            BM,
            [Description("Bhutan")]
            BT,
            [Description("Bolivia (Plurinational State of)")]
            BO,
            [Description("Bonaire, Sint Eustatius and Saba")]
            BQ,
            [Description("Bosnia and Herzegovina")]
            BA,
            [Description("Botswana")]
            BW,
            [Description("Bouvet Island")]
            BV,
            [Description("Brazil")]
            BR,
            [Description("British Indian Ocean Territory")]
            IO,
            [Description("Brunei Darussalam")]
            BN,
            [Description("Bulgaria")]
            BG,
            [Description("Burkina Faso")]
            BF,
            [Description("Burundi")]
            BI,
            [Description("Cambodia")]
            KH,
            [Description("Cameroon")]
            CM,
            [Description("Canada")]
            CA,
            [Description("Cabo Verde")]
            CV,
            [Description("Cayman Islands")]
            KY,
            [Description("Central African Republic")]
            CF,
            [Description("Chad")]
            TD,
            [Description("Chile")]
            CL,
            [Description("China")]
            CN,
            [Description("Christmas Island")]
            CX,
            [Description("Cocos (Keeling) Islands")]
            CC,
            [Description("Colombia")]
            CO,
            [Description("Comoros")]
            KM,
            [Description("Congo")]
            CG,
            [Description("Congo (Democratic Republic of the)")]
            CD,
            [Description("Cook Islands")]
            CK,
            [Description("Costa Rica")]
            CR,
            [Description("Côte d'Ivoire")]
            CI,
            [Description("Croatia")]
            HR,
            [Description("Cuba")]
            CU,
            [Description("Curaçao")]
            CW,
            [Description("Cyprus")]
            CY,
            [Description("Czech Republic")]
            CZ,
            [Description("Denmark")]
            DK,
            [Description("Djibouti")]
            DJ,
            [Description("Dominica")]
            DM,
            [Description("Dominican Republic")]
            DO,
            [Description("Ecuador")]
            EC,
            [Description("Egypt")]
            EG,
            [Description("El Salvador")]
            SV,
            [Description("Equatorial Guinea")]
            GQ,
            [Description("Eritrea")]
            ER,
            [Description("Estonia")]
            EE,
            [Description("Ethiopia")]
            ET,
            [Description("Falkland Islands (Malvinas)")]
            FK,
            [Description("Faroe Islands")]
            FO,
            [Description("Fiji")]
            FJ,
            [Description("Finland")]
            FI,
            [Description("France")]
            FR,
            [Description("French Guiana")]
            GF,
            [Description("French Polynesia")]
            PF,
            [Description("French Southern Territories")]
            TF,
            [Description("Gabon")]
            GA,
            [Description("Gambia")]
            GM,
            [Description("Georgia")]
            GE,
            [Description("Germany")]
            DE,
            [Description("Ghana")]
            GH,
            [Description("Gibraltar")]
            GI,
            [Description("Greece")]
            GR,
            [Description("Greenland")]
            GL,
            [Description("Grenada")]
            GD,
            [Description("Guadeloupe")]
            GP,
            [Description("Guam")]
            GU,
            [Description("Guatemala")]
            GT,
            [Description("Guernsey")]
            GG,
            [Description("Guinea")]
            GN,
            [Description("Guinea-Bissau")]
            GW,
            [Description("Guyana")]
            GY,
            [Description("Haiti")]
            HT,
            [Description("Heard Island and McDonald Islands")]
            HM,
            [Description("Holy See")]
            VA,
            [Description("Honduras")]
            HN,
            [Description("Hong Kong")]
            HK,
            [Description("Hungary")]
            HU,
            [Description("Iceland")]
            IS,
            [Description("India")]
            IN,
            [Description("Indonesia")]
            ID,
            [Description("Iran (Islamic Republic of)")]
            IR,
            [Description("Iraq")]
            IQ,
            [Description("Ireland")]
            IE,
            [Description("Isle of Man")]
            IM,
            [Description("Israel")]
            IL,
            [Description("Italy")]
            IT,
            [Description("Jamaica")]
            JM,
            [Description("Japan")]
            JP,
            [Description("Jersey")]
            JE,
            [Description("Jordan")]
            JO,
            [Description("Kazakhstan")]
            KZ,
            [Description("Kenya")]
            KE,
            [Description("Kiribati")]
            KI,
            [Description("Korea (Democratic People's Republic of)")]
            KP,
            [Description("Korea (Republic of)")]
            KR,
            [Description("Kuwait")]
            KW,
            [Description("Kyrgyzstan")]
            KG,
            [Description("Lao People's Democratic Republic")]
            LA,
            [Description("Latvia")]
            LV,
            [Description("Lebanon")]
            LB,
            [Description("Lesotho")]
            LS,
            [Description("Liberia")]
            LR,
            [Description("Libya")]
            LY,
            [Description("Liechtenstein")]
            LI,
            [Description("Lithuania")]
            LT,
            [Description("Luxembourg")]
            LU,
            [Description("Macao")]
            MO,
            [Description("Macedonia (the former Yugoslav Republic of)")]
            MK,
            [Description("Madagascar")]
            MG,
            [Description("Malawi")]
            MW,
            [Description("Malaysia")]
            MY,
            [Description("Maldives")]
            MV,
            [Description("Mali")]
            ML,
            [Description("Malta")]
            MT,
            [Description("Marshall Islands")]
            MH,
            [Description("Martinique")]
            MQ,
            [Description("Mauritania")]
            MR,
            [Description("Mauritius")]
            MU,
            [Description("Mayotte")]
            YT,
            [Description("Mexico")]
            MX,
            [Description("Micronesia (Federated States of)")]
            FM,
            [Description("Moldova (Republic of)")]
            MD,
            [Description("Monaco")]
            MC,
            [Description("Mongolia")]
            MN,
            [Description("Montenegro")]
            ME,
            [Description("Montserrat")]
            MS,
            [Description("Morocco")]
            MA,
            [Description("Mozambique")]
            MZ,
            [Description("Myanmar")]
            MM,
            [Description("Namibia")]
            NA,
            [Description("Nauru")]
            NR,
            [Description("Nepal")]
            NP,
            [Description("Netherlands")]
            NL,
            [Description("New Caledonia")]
            NC,
            [Description("New Zealand")]
            NZ,
            [Description("Nicaragua")]
            NI,
            [Description("Niger")]
            NE,
            [Description("Nigeria")]
            NG,
            [Description("Niue")]
            NU,
            [Description("Norfolk Island")]
            NF,
            [Description("Northern Mariana Islands")]
            MP,
            [Description("Norway")]
            NO,
            [Description("Oman")]
            OM,
            [Description("Pakistan")]
            PK,
            [Description("Palau")]
            PW,
            [Description("Palestine, State of")]
            PS,
            [Description("Panama")]
            PA,
            [Description("Papua New Guinea")]
            PG,
            [Description("Paraguay")]
            PY,
            [Description("Peru")]
            PE,
            [Description("Philippines")]
            PH,
            [Description("Pitcairn")]
            PN,
            [Description("Poland")]
            PL,
            [Description("Portugal")]
            PT,
            [Description("Puerto Rico")]
            PR,
            [Description("Qatar")]
            QA,
            [Description("Réunion")]
            RE,
            [Description("Romania")]
            RO,
            [Description("Russian Federation")]
            RU,
            [Description("Rwanda")]
            RW,
            [Description("Saint Barthélemy")]
            BL,
            [Description("Saint Helena, Ascension and Tristan da Cunha")]
            SH,
            [Description("Saint Kitts and Nevis")]
            KN,
            [Description("Saint Lucia")]
            LC,
            [Description("Saint Martin (French part)")]
            MF,
            [Description("Saint Pierre and Miquelon")]
            PM,
            [Description("Saint Vincent and the Grenadines")]
            VC,
            [Description("Samoa")]
            WS,
            [Description("San Marino")]
            SM,
            [Description("Sao Tome and Principe")]
            ST,
            [Description("Saudi Arabia")]
            SA,
            [Description("Senegal")]
            SN,
            [Description("Serbia")]
            RS,
            [Description("Seychelles")]
            SC,
            [Description("Sierra Leone")]
            SL,
            [Description("Singapore")]
            SG,
            [Description("Sint Maarten (Dutch part)")]
            SX,
            [Description("Slovakia")]
            SK,
            [Description("Slovenia")]
            SI,
            [Description("Solomon Islands")]
            SB,
            [Description("Somalia")]
            SO,
            [Description("South Africa")]
            ZA,
            [Description("South Georgia and the South Sandwich Islands")]
            GS,
            [Description("South Sudan")]
            SS,
            [Description("Spain")]
            ES,
            [Description("Sri Lanka")]
            LK,
            [Description("Sudan")]
            SD,
            [Description("Suriname")]
            SR,
            [Description("Svalbard and Jan Mayen")]
            SJ,
            [Description("Swaziland")]
            SZ,
            [Description("Sweden")]
            SE,
            [Description("Switzerland")]
            CH,
            [Description("Syrian Arab Republic")]
            SY,
            [Description("Taiwan, Province of China")]
            TW,
            [Description("Tajikistan")]
            TJ,
            [Description("Tanzania, United Republic of")]
            TZ,
            [Description("Thailand")]
            TH,
            [Description("Timor-Leste")]
            TL,
            [Description("Togo")]
            TG,
            [Description("Tokelau")]
            TK,
            [Description("Tonga")]
            TO,
            [Description("Trinidad and Tobago")]
            TT,
            [Description("Tunisia")]
            TN,
            [Description("Turkey")]
            TR,
            [Description("Turkmenistan")]
            TM,
            [Description("Turks and Caicos Islands")]
            TC,
            [Description("Tuvalu")]
            TV,
            [Description("Uganda")]
            UG,
            [Description("Ukraine")]
            UA,
            [Description("United Arab Emirates")]
            AE,
            [Description("United Kingdom of Great Britain and Northern Ireland")]
            GB,
            [Description("United States of America")]
            US,
            [Description("United States Minor Outlying Islands")]
            UM,
            [Description("Uruguay")]
            UY,
            [Description("Uzbekistan")]
            UZ,
            [Description("Vanuatu")]
            VU,
            [Description("Venezuela (Bolivarian Republic of)")]
            VE,
            [Description("Viet Nam")]
            VN,
            [Description("Virgin Islands (British)")]
            VG,
            [Description("Virgin Islands (U.S.)")]
            VI,
            [Description("Wallis and Futuna")]
            WF,
            [Description("Western Sahara")]
            EH,
            [Description("Yemen")]
            YE,
            [Description("Zambia")]
            ZM,
            [Description("Zimbabwe")]
            ZW
        }
        public enum IsoCountry3Char
        {
            [Description("Afghanistan")]
            AFG,
            [Description("Åland Islands")]
            ALA,
            [Description("Albania")]
            ALB,
            [Description("Algeria")]
            DZA,
            [Description("American Samoa")]
            ASM,
            [Description("Andorra")]
            AND,
            [Description("Angola")]
            AGO,
            [Description("Anguilla")]
            AIA,
            [Description("Antarctica")]
            ATA,
            [Description("Antigua and Barbuda")]
            ATG,
            [Description("Argentina")]
            ARG,
            [Description("Armenia")]
            ARM,
            [Description("Aruba")]
            ABW,
            [Description("Australia")]
            AUS,
            [Description("Austria")]
            AUT,
            [Description("Azerbaijan")]
            AZE,
            [Description("Bahamas")]
            BHS,
            [Description("Bahrain")]
            BHR,
            [Description("Bangladesh")]
            BGD,
            [Description("Barbados")]
            BRB,
            [Description("Belarus")]
            BLR,
            [Description("Belgium")]
            BEL,
            [Description("Belize")]
            BLZ,
            [Description("Benin")]
            BEN,
            [Description("Bermuda")]
            BMU,
            [Description("Bhutan")]
            BTN,
            [Description("Bolivia (Plurinational State of)")]
            BOL,
            [Description("Bonaire, Sint Eustatius and Saba")]
            BES,
            [Description("Bosnia and Herzegovina")]
            BIH,
            [Description("Botswana")]
            BWA,
            [Description("Bouvet Island")]
            BVT,
            [Description("Brazil")]
            BRA,
            [Description("British Indian Ocean Territory")]
            IOT,
            [Description("Brunei Darussalam")]
            BRN,
            [Description("Bulgaria")]
            BGR,
            [Description("Burkina Faso")]
            BFA,
            [Description("Burundi")]
            BDI,
            [Description("Cambodia")]
            KHM,
            [Description("Cameroon")]
            CMR,
            [Description("Canada")]
            CAN,
            [Description("Cabo Verde")]
            CPV,
            [Description("Cayman Islands")]
            CYM,
            [Description("Central African Republic")]
            CAF,
            [Description("Chad")]
            TCD,
            [Description("Chile")]
            CHL,
            [Description("China")]
            CHN,
            [Description("Christmas Island")]
            CXR,
            [Description("Cocos (Keeling) Islands")]
            CCK,
            [Description("Colombia")]
            COL,
            [Description("Comoros")]
            COM,
            [Description("Congo")]
            COG,
            [Description("Congo (Democratic Republic of the)")]
            COD,
            [Description("Cook Islands")]
            COK,
            [Description("Costa Rica")]
            CRI,
            [Description("Côte d'Ivoire")]
            CIV,
            [Description("Croatia")]
            HRV,
            [Description("Cuba")]
            CUB,
            [Description("Curaçao")]
            CUW,
            [Description("Cyprus")]
            CYP,
            [Description("Czech Republic")]
            CZE,
            [Description("Denmark")]
            DNK,
            [Description("Djibouti")]
            DJI,
            [Description("Dominica")]
            DMA,
            [Description("Dominican Republic")]
            DOM,
            [Description("Ecuador")]
            ECU,
            [Description("Egypt")]
            EGY,
            [Description("El Salvador")]
            SLV,
            [Description("Equatorial Guinea")]
            GNQ,
            [Description("Eritrea")]
            ERI,
            [Description("Estonia")]
            EST,
            [Description("Ethiopia")]
            ETH,
            [Description("Falkland Islands (Malvinas)")]
            FLK,
            [Description("Faroe Islands")]
            FRO,
            [Description("Fiji")]
            FJI,
            [Description("Finland")]
            FIN,
            [Description("France")]
            FRA,
            [Description("French Guiana")]
            GUF,
            [Description("French Polynesia")]
            PYF,
            [Description("French Southern Territories")]
            ATF,
            [Description("Gabon")]
            GAB,
            [Description("Gambia")]
            GMB,
            [Description("Georgia")]
            GEO,
            [Description("Germany")]
            DEU,
            [Description("Ghana")]
            GHA,
            [Description("Gibraltar")]
            GIB,
            [Description("Greece")]
            GRC,
            [Description("Greenland")]
            GRL,
            [Description("Grenada")]
            GRD,
            [Description("Guadeloupe")]
            GLP,
            [Description("Guam")]
            GUM,
            [Description("Guatemala")]
            GTM,
            [Description("Guernsey")]
            GGY,
            [Description("Guinea")]
            GIN,
            [Description("Guinea-Bissau")]
            GNB,
            [Description("Guyana")]
            GUY,
            [Description("Haiti")]
            HTI,
            [Description("Heard Island and McDonald Islands")]
            HMD,
            [Description("Holy See")]
            VAT,
            [Description("Honduras")]
            HND,
            [Description("Hong Kong")]
            HKG,
            [Description("Hungary")]
            HUN,
            [Description("Iceland")]
            ISL,
            [Description("India")]
            IND,
            [Description("Indonesia")]
            IDN,
            [Description("Iran (Islamic Republic of)")]
            IRN,
            [Description("Iraq")]
            IRQ,
            [Description("Ireland")]
            IRL,
            [Description("Isle of Man")]
            IMN,
            [Description("Israel")]
            ISR,
            [Description("Italy")]
            ITA,
            [Description("Jamaica")]
            JAM,
            [Description("Japan")]
            JPN,
            [Description("Jersey")]
            JEY,
            [Description("Jordan")]
            JOR,
            [Description("Kazakhstan")]
            KAZ,
            [Description("Kenya")]
            KEN,
            [Description("Kiribati")]
            KIR,
            [Description("Korea (Democratic People's Republic of)")]
            PRK,
            [Description("Korea (Republic of)")]
            KOR,
            [Description("Kuwait")]
            KWT,
            [Description("Kyrgyzstan")]
            KGZ,
            [Description("Lao People's Democratic Republic")]
            LAO,
            [Description("Latvia")]
            LVA,
            [Description("Lebanon")]
            LBN,
            [Description("Lesotho")]
            LSO,
            [Description("Liberia")]
            LBR,
            [Description("Libya")]
            LBY,
            [Description("Liechtenstein")]
            LIE,
            [Description("Lithuania")]
            LTU,
            [Description("Luxembourg")]
            LUX,
            [Description("Macao")]
            MAC,
            [Description("Macedonia (the former Yugoslav Republic of)")]
            MKD,
            [Description("Madagascar")]
            MDG,
            [Description("Malawi")]
            MWI,
            [Description("Malaysia")]
            MYS,
            [Description("Maldives")]
            MDV,
            [Description("Mali")]
            MLI,
            [Description("Malta")]
            MLT,
            [Description("Marshall Islands")]
            MHL,
            [Description("Martinique")]
            MTQ,
            [Description("Mauritania")]
            MRT,
            [Description("Mauritius")]
            MUS,
            [Description("Mayotte")]
            MYT,
            [Description("Mexico")]
            MEX,
            [Description("Micronesia (Federated States of)")]
            FSM,
            [Description("Moldova (Republic of)")]
            MDA,
            [Description("Monaco")]
            MCO,
            [Description("Mongolia")]
            MNG,
            [Description("Montenegro")]
            MNE,
            [Description("Montserrat")]
            MSR,
            [Description("Morocco")]
            MAR,
            [Description("Mozambique")]
            MOZ,
            [Description("Myanmar")]
            MMR,
            [Description("Namibia")]
            NAM,
            [Description("Nauru")]
            NRU,
            [Description("Nepal")]
            NPL,
            [Description("Netherlands")]
            NLD,
            [Description("New Caledonia")]
            NCL,
            [Description("New Zealand")]
            NZL,
            [Description("Nicaragua")]
            NIC,
            [Description("Niger")]
            NER,
            [Description("Nigeria")]
            NGA,
            [Description("Niue")]
            NIU,
            [Description("Norfolk Island")]
            NFK,
            [Description("Northern Mariana Islands")]
            MNP,
            [Description("Norway")]
            NOR,
            [Description("Oman")]
            OMN,
            [Description("Pakistan")]
            PAK,
            [Description("Palau")]
            PLW,
            [Description("Palestine, State of")]
            PSE,
            [Description("Panama")]
            PAN,
            [Description("Papua New Guinea")]
            PNG,
            [Description("Paraguay")]
            PRY,
            [Description("Peru")]
            PER,
            [Description("Philippines")]
            PHL,
            [Description("Pitcairn")]
            PCN,
            [Description("Poland")]
            POL,
            [Description("Portugal")]
            PRT,
            [Description("Puerto Rico")]
            PRI,
            [Description("Qatar")]
            QAT,
            [Description("Réunion")]
            REU,
            [Description("Romania")]
            ROU,
            [Description("Russian Federation")]
            RUS,
            [Description("Rwanda")]
            RWA,
            [Description("Saint Barthélemy")]
            BLM,
            [Description("Saint Helena, Ascension and Tristan da Cunha")]
            SHN,
            [Description("Saint Kitts and Nevis")]
            KNA,
            [Description("Saint Lucia")]
            LCA,
            [Description("Saint Martin (French part)")]
            MAF,
            [Description("Saint Pierre and Miquelon")]
            SPM,
            [Description("Saint Vincent and the Grenadines")]
            VCT,
            [Description("Samoa")]
            WSM,
            [Description("San Marino")]
            SMR,
            [Description("Sao Tome and Principe")]
            STP,
            [Description("Saudi Arabia")]
            SAU,
            [Description("Senegal")]
            SEN,
            [Description("Serbia")]
            SRB,
            [Description("Seychelles")]
            SYC,
            [Description("Sierra Leone")]
            SLE,
            [Description("Singapore")]
            SGP,
            [Description("Sint Maarten (Dutch part)")]
            SXM,
            [Description("Slovakia")]
            SVK,
            [Description("Slovenia")]
            SVN,
            [Description("Solomon Islands")]
            SLB,
            [Description("Somalia")]
            SOM,
            [Description("South Africa")]
            ZAF,
            [Description("South Georgia and the South Sandwich Islands")]
            SGS,
            [Description("South Sudan")]
            SSD,
            [Description("Spain")]
            ESP,
            [Description("Sri Lanka")]
            LKA,
            [Description("Sudan")]
            SDN,
            [Description("Suriname")]
            SUR,
            [Description("Svalbard and Jan Mayen")]
            SJM,
            [Description("Swaziland")]
            SWZ,
            [Description("Sweden")]
            SWE,
            [Description("Switzerland")]
            CHE,
            [Description("Syrian Arab Republic")]
            SYR,
            [Description("Taiwan, Province of China")]
            TWN,
            [Description("Tajikistan")]
            TJK,
            [Description("Tanzania, United Republic of")]
            TZA,
            [Description("Thailand")]
            THA,
            [Description("Timor-Leste")]
            TLS,
            [Description("Togo")]
            TGO,
            [Description("Tokelau")]
            TKL,
            [Description("Tonga")]
            TON,
            [Description("Trinidad and Tobago")]
            TTO,
            [Description("Tunisia")]
            TUN,
            [Description("Turkey")]
            TUR,
            [Description("Turkmenistan")]
            TKM,
            [Description("Turks and Caicos Islands")]
            TCA,
            [Description("Tuvalu")]
            TUV,
            [Description("Uganda")]
            UGA,
            [Description("Ukraine")]
            UKR,
            [Description("United Arab Emirates")]
            ARE,
            [Description("United Kingdom of Great Britain and Northern Ireland")]
            GBR,
            [Description("United States of America")]
            USA,
            [Description("United States Minor Outlying Islands")]
            UMI,
            [Description("Uruguay")]
            URY,
            [Description("Uzbekistan")]
            UZB,
            [Description("Vanuatu")]
            VUT,
            [Description("Venezuela (Bolivarian Republic of)")]
            VEN,
            [Description("Viet Nam")]
            VNM,
            [Description("Virgin Islands (British)")]
            VGB,
            [Description("Virgin Islands (U.S.)")]
            VIR,
            [Description("Wallis and Futuna")]
            WLF,
            [Description("Western Sahara")]
            ESH,
            [Description("Yemen")]
            YEM,
            [Description("Zambia")]
            ZMB,
            [Description("Zimbabwe")]
            ZWE
        }
        public enum IsoLanguage
        {
            [Description("Abkhaz")]
            AB,
            [Description("Afar")]
            AA,
            [Description("Afrikaans")]
            AF,
            [Description("Akan")]
            AK,
            [Description("Albanian")]
            SQ,
            [Description("Amharic")]
            AM,
            [Description("Arabic")]
            AR,
            [Description("Aragonese")]
            AN,
            [Description("Armenian")]
            HY,
            [Description("Assamese")]
            AS,
            [Description("Avaric")]
            AV,
            [Description("Avestan")]
            AE,
            [Description("Aymara")]
            AY,
            [Description("Azerbaijani")]
            AZ,
            [Description("Bambara")]
            BM,
            [Description("Bashkir")]
            BA,
            [Description("Basque")]
            EU,
            [Description("Belarusian")]
            BE,
            [Description("Bengali")]
            BN,
            [Description("Bihari")]
            BH,
            [Description("Bislama")]
            BI,
            [Description("Bosnian")]
            BS,
            [Description("Breton")]
            BR,
            [Description("Bulgarian")]
            BG,
            [Description("Burmese")]
            MY,
            [Description("Catalan")]
            CA,
            [Description("Chamorro")]
            CH,
            [Description("Chechen")]
            CE,
            [Description("Chichewa")]
            NY,
            [Description("Chinese")]
            ZH,
            [Description("Chuvash")]
            CV,
            [Description("Cornish")]
            KW,
            [Description("Corsican")]
            CO,
            [Description("Cree")]
            CR,
            [Description("Croatian")]
            HR,
            [Description("Czech")]
            CS,
            [Description("Danish")]
            DA,
            [Description("Divehi")]
            DV,
            [Description("Dutch")]
            NL,
            [Description("Dzongkha")]
            DZ,
            [Description("English")]
            EN,
            [Description("Esperanto")]
            EO,
            [Description("Estonian")]
            ET,
            [Description("Ewe")]
            EE,
            [Description("Faroese")]
            FO,
            [Description("Fijian")]
            FJ,
            [Description("Finnish")]
            FI,
            [Description("French")]
            FR,
            [Description("Fula")]
            FF,
            [Description("Galician")]
            GL,
            [Description("Georgian")]
            KA,
            [Description("German")]
            DE,
            [Description("Greek")]
            EL,
            [Description("Guaraní")]
            GN,
            [Description("Gujarati")]
            GU,
            [Description("Haitian")]
            HT,
            [Description("Hausa")]
            HA,
            [Description("Hebrew")]
            HE,
            [Description("Herero")]
            HZ,
            [Description("Hindi")]
            HI,
            [Description("Hiri Motu")]
            HO,
            [Description("Hungarian")]
            HU,
            [Description("Interlingua")]
            IA,
            [Description("Indonesian")]
            ID,
            [Description("Interlingue")]
            IE,
            [Description("Irish")]
            GA,
            [Description("Igbo")]
            IG,
            [Description("Inupiaq")]
            IK,
            [Description("Ido")]
            IO,
            [Description("Icelandic")]
            IS,
            [Description("Italian")]
            IT,
            [Description("Inuktitut")]
            IU,
            [Description("Japanese")]
            JA,
            [Description("Javanese")]
            JV,
            [Description("Kalaallisu")]
            KL,
            [Description("Kannada")]
            KN,
            [Description("Kanuri")]
            KR,
            [Description("Kashmiri")]
            KS,
            [Description("Kazakh")]
            KK,
            [Description("Khmer")]
            KM,
            [Description("Kikuyu")]
            KI,
            [Description("Kinyarwanda")]
            RW,
            [Description("Kyrgyz")]
            KY,
            [Description("Komi")]
            KV,
            [Description("Kongo")]
            KG,
            [Description("Korean")]
            KO,
            [Description("Kurdish")]
            KU,
            [Description("Kwanyama")]
            KJ,
            [Description("Latin")]
            LA,
            [Description("Luxembourgish")]
            LB,
            [Description("Ganda")]
            LG,
            [Description("Limburgish")]
            LI,
            [Description("Lingala")]
            LN,
            [Description("Lao")]
            LO,
            [Description("Lithuanian")]
            LT,
            [Description("Luba-Katanga")]
            LU,
            [Description("Latvian")]
            LV,
            [Description("Manx")]
            GV,
            [Description("Macedonian")]
            MK,
            [Description("Malagasy")]
            MG,
            [Description("Malay")]
            MS,
            [Description("Malayalam")]
            ML,
            [Description("Maltese")]
            MT,
            [Description("Māori")]
            MI,
            [Description("Marathi")]
            MR,
            [Description("Marshallese")]
            MH,
            [Description("Mongolian")]
            MN,
            [Description("Nauru")]
            NA,
            [Description("Navajo")]
            NV,
            [Description("Northern Ndebele")]
            ND,
            [Description("Nepali")]
            NE,
            [Description("Ndonga")]
            NG,
            [Description("Norwegian Bokmål")]
            NB,
            [Description("Norwegian Nynorsk")]
            NN,
            [Description("Norwegian")]
            NO,
            [Description("Nuosu")]
            II,
            [Description("Southern Ndebele")]
            NR,
            [Description("Occitan")]
            OC,
            [Description("Ojibwe")]
            OJ,
            [Description("Old Church Slavonic")]
            CU,
            [Description("Oromo")]
            OM,
            [Description("Oriya")]
            OR,
            [Description("Ossetian")]
            OS,
            [Description("Panjabi")]
            PA,
            [Description("Pāli")]
            PI,
            [Description("Persian (Farsi)")]
            FA,
            [Description("Polish")]
            PL,
            [Description("Pashto")]
            PS,
            [Description("Portuguese")]
            PT,
            [Description("Quechua")]
            QU,
            [Description("Romansh")]
            RM,
            [Description("Kirundi")]
            RN,
            [Description("Romanian")]
            RO,
            [Description("Russian")]
            RU,
            [Description("Sanskrit")]
            SA,
            [Description("Sardinian")]
            SC,
            [Description("Sindhi")]
            SD,
            [Description("Northern Sami")]
            SE,
            [Description("Samoan")]
            SM,
            [Description("Sango")]
            SG,
            [Description("Serbian")]
            SR,
            [Description("Scottish Gaelic")]
            GD,
            [Description("Shona")]
            SN,
            [Description("Sinhala")]
            SI,
            [Description("Slovak")]
            SK,
            [Description("Slovene")]
            SL,
            [Description("Somali")]
            SO,
            [Description("Southern Sotho")]
            ST,
            [Description("Spanish")]
            ES,
            [Description("Sundanese")]
            SU,
            [Description("Swahili")]
            SW,
            [Description("Swati")]
            SS,
            [Description("Swedish")]
            SV,
            [Description("Tamil")]
            TA,
            [Description("Telugu")]
            TE,
            [Description("Tajik")]
            TG,
            [Description("Thai")]
            TH,
            [Description("Tigrinya")]
            TI,
            [Description("Tibetan")]
            BO,
            [Description("Turkmen")]
            TK,
            [Description("Tagalog")]
            TL,
            [Description("Tswana")]
            TN,
            [Description("Tongan")]
            TO,
            [Description("Turkish")]
            TR,
            [Description("Tsonga")]
            TS,
            [Description("Tatar")]
            TT,
            [Description("Twi")]
            TW,
            [Description("Tahitian")]
            TY,
            [Description("Uyghur")]
            UG,
            [Description("Ukrainian")]
            UK,
            [Description("Urdu")]
            UR,
            [Description("Uzbek")]
            UZ,
            [Description("Venda")]
            VE,
            [Description("Vietnamese")]
            VI,
            [Description("Volapük")]
            VO,
            [Description("Walloon")]
            WA,
            [Description("Welsh")]
            CY,
            [Description("Wolof")]
            WO,
            [Description("Western Frisian")]
            FY,
            [Description("Xhosa")]
            XH,
            [Description("Yiddish")]
            YI,
            [Description("Yoruba")]
            YO,
            [Description("Zhuang")]
            ZA,
            [Description("Zulu")]
            ZU
        }
        public enum UsaStates
        {
            [Description("Alabama")]
            AL,
            [Description("Alaska")]
            AK,
            [Description("Arizona")]
            AZ,
            [Description("Arkansas")]
            AR,
            [Description("California")]
            CA,
            [Description("Colorado")]
            CO,
            [Description("Connecticut")]
            CT,
            [Description("Delaware")]
            DE,
            [Description("Florida")]
            FL,
            [Description("Georgia")]
            GA,
            [Description("Hawaii")]
            HI,
            [Description("Idaho")]
            ID,
            [Description("Illinois")]
            IL,
            [Description("Indiana")]
            IN,
            [Description("Iowa")]
            IA,
            [Description("Kansas")]
            KS,
            [Description("Kentucky")]
            KY,
            [Description("Louisiana")]
            LA,
            [Description("Maine")]
            ME,
            [Description("Maryland")]
            MD,
            [Description("Massachusetts")]
            MA,
            [Description("Michigan")]
            MI,
            [Description("Minnesota")]
            MN,
            [Description("Mississippi")]
            MS,
            [Description("Missouri")]
            MO,
            [Description("Montana")]
            MT,
            [Description("Nebraska")]
            NE,
            [Description("Nevada")]
            NV,
            [Description("New Hampshire")]
            NH,
            [Description("New Jersey")]
            NJ,
            [Description("New Mexico")]
            NM,
            [Description("New York")]
            NY,
            [Description("North Carolina")]
            NC,
            [Description("North Dakota")]
            ND,
            [Description("Ohio")]
            OH,
            [Description("Oklahoma")]
            OK,
            [Description("Oregon")]
            OR,
            [Description("Pennsylvania")]
            PA,
            [Description("Rhode Island")]
            RI,
            [Description("South Carolina")]
            SC,
            [Description("South Dakota")]
            SD,
            [Description("Tennessee")]
            TN,
            [Description("Texas")]
            TX,
            [Description("Utah")]
            UT,
            [Description("Vermont")]
            VT,
            [Description("Virginia")]
            VA,
            [Description("Washington")]
            WA,
            [Description("West Virginia")]
            WV,
            [Description("Wisconsin")]
            WI,
            [Description("Wyoming")]
            WY
        }

        // properties
        public DateTime EpochDate => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // convert enum from string
        public static T ParseEnum<T>(string value) where T : struct
        {
            T result;
            return Enum.TryParse(value, true, out result) ? result : default(T);
        }

        public static string CombineFirstLastName(string firstName, string lastName)
        {
            if (firstName == null && lastName == null) return null;
            var first = firstName ?? string.Empty;
            var last = lastName ?? string.Empty;
            return $"{first.Trim()} {last.Trim()}".Trim();
        }

        // date methods
        public DateTime DateFromString(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? DateTime.MinValue : DateTime.Parse(value);
        }
        public DateTime FromEpochSeconds(long seconds)
        {
            return EpochDate.AddSeconds(seconds);
        }
        public DateTime FromEpochSeconds(float seconds)
        {
            return EpochDate.AddSeconds(seconds);
        }
        public DateTime FromEpochMilliseconds(long milliseconds)
        {
            return EpochDate.AddMilliseconds(milliseconds);
        }
        public DateTime FromEpochMilliseconds(float milliseconds)
        {
            return EpochDate.AddMilliseconds(milliseconds);
        }
        public string DayOfWeek(DateTime? value)
        {
            return value?.DayOfWeek.ToString() ?? string.Empty;
        }
        public long ToEpochSeconds(DateTime value)
        {
            var duration = value - EpochDate;
            return (long)duration.TotalSeconds;
        }
        public long ToEpochMilliseconds(DateTime value)
        {
            var duration = value - EpochDate;
            return (long)duration.TotalMilliseconds;
        }

        // address methods
        public string BuildAddress(params string[] addressFields)
        {
            if (addressFields.Length == 0) return string.Empty;
            var address = new StringBuilder();
            foreach (var addressField in addressFields.Where(addressField => !string.IsNullOrWhiteSpace(addressField))) address.Append($" {addressField}");
            return address.ToString().Trim().Replace("  ", " ");
        }
        
        // list methods
        public string CommaSeparatedValues(List<string> values, bool removeBlanks = true)
        {
            return string.Join(", ", values.Where(value => !string.IsNullOrWhiteSpace(value) && removeBlanks).OrderBy(value => value));
        }

        // temperature conversion methods
        public static double ConvertCelsiusToFahrenheit(double c)
        {
            return ((9.0 / 5.0) * c) + 32;
        }
        public static double ConvertFahrenheitToCelsius(double f)
        {
            return (5.0 / 9.0) * (f - 32);
        }

        // weight conversion methods
        public static double ConvertKilogramsToPounds(double k)
        {
            return k / 1000 * 453.59237;
        }
        public static double ConvertPoundsToKilograms(double p)
        {
            return p / 453.59237 * 1000;
        }

        // length conversion methods
        public static double ConvertKilometersToMiles(double k)
        {
            return k * 0.62137;
        }
        public static double ConvertMilesToKilometers(double m)
        {
            return m / 0.62137;
        }
        public static double ConvertYardsToMiles(double y)
        {
            return y / 1760;
        }
        public static double ConvertMilesToYards(double m)
        {
            return m * 1760;
        }
        public static double ConvertInchesToCentimeters(double i)
        {
            return i * 2.54;
        }
        public static double ConvertCentimetersToInches(double c)
        {
            return c / 2.54;
        }
        public static double ConvertFeetToInches(double f)
        {
            return f * 12;
        }
        public static double ConvertInchesToFeet(double i)
        {
            return i / 12;
        }
        public static double ConvertYardToFoot(double y)
        {
            return y * 3;
        }
        public static double ConvertFootToYard(double f)
        {
            return f / 3;
        }

    }
}
