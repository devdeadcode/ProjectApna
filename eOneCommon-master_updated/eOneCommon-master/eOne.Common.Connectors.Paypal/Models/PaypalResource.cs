using System;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalResource : ConnectorEntityModel
    {
        
        public enum PaypalResourceType
        {
            none,
            sale,
            authorization,
            capture,
            refund
        }

        public PaypalSale sale { get; set; }
        public PaypalAuthorization authorization { get; set; }
        public PaypalCapture capture { get; set; }
        public PaypalRefund refund { get; set; }

        public PaypalResourceType type
        {
            get
            {
                if (sale != null) return PaypalResourceType.sale;
                if (authorization != null) return PaypalResourceType.authorization;
                if (capture != null) return PaypalResourceType.capture;
                if (refund != null) return PaypalResourceType.refund;
                return PaypalResourceType.none;
            }
        }

        public string id
        {
            get
            {
                switch (type)
                {
                    case PaypalResourceType.sale:
                        return sale.id;
                    case PaypalResourceType.refund:
                        return refund.id;
                    case PaypalResourceType.capture:
                        return capture.id;
                    case PaypalResourceType.authorization:
                        return authorization.id;
                }
                return string.Empty;
            }
        }
        public decimal total
        {
            get
            {
                switch (type)
                {
                    case PaypalResourceType.sale:
                        return sale.amount.total;
                    case PaypalResourceType.refund:
                        return refund.amount.total;
                    case PaypalResourceType.capture:
                        return capture.amount.total;
                    case PaypalResourceType.authorization:
                        return authorization.amount.total;
                }
                return 0;
            }
        }
        public string currency
        {
            get
            {
                switch (type)
                {
                    case PaypalResourceType.sale:
                        return sale.amount.currency;
                    case PaypalResourceType.refund:
                        return refund.amount.currency;
                    case PaypalResourceType.capture:
                        return capture.amount.currency;
                    case PaypalResourceType.authorization:
                        return authorization.amount.currency;
                }
                return string.Empty;
            }
        }
        public DateTime create_time
        {
            get
            {
                switch (type)
                {
                    case PaypalResourceType.sale:
                        return sale.create_time;
                    case PaypalResourceType.refund:
                        return refund.create_time;
                    case PaypalResourceType.capture:
                        return capture.create_time;
                    case PaypalResourceType.authorization:
                        return authorization.create_time;
                }
                return DateTime.MinValue;
            }
        }
        public DateTime create_date
        {
            get
            {
                switch (type)
                {
                    case PaypalResourceType.sale:
                        return sale.create_time;
                    case PaypalResourceType.refund:
                        return refund.create_time;
                    case PaypalResourceType.capture:
                        return capture.create_time;
                    case PaypalResourceType.authorization:
                        return authorization.create_time;
                }
                return DateTime.MinValue;
            }
        }
        public DateTime update_time
        {
            get
            {
                switch (type)
                {
                    case PaypalResourceType.sale:
                        return sale.update_time;
                    case PaypalResourceType.refund:
                        return refund.update_time;
                    case PaypalResourceType.capture:
                        return capture.update_time;
                    case PaypalResourceType.authorization:
                        return authorization.update_time;
                }
                return DateTime.MinValue;
            }
        }
        public DateTime update_date
        {
            get
            {
                switch (type)
                {
                    case PaypalResourceType.sale:
                        return sale.update_time;
                    case PaypalResourceType.refund:
                        return refund.update_time;
                    case PaypalResourceType.capture:
                        return capture.update_time;
                    case PaypalResourceType.authorization:
                        return authorization.update_time;
                }
                return DateTime.MinValue;
            }
        }
        public string parent_payment
        {
            get
            {
                switch (type)
                {
                    case PaypalResourceType.sale:
                        return sale.parent_payment;
                    case PaypalResourceType.refund:
                        return refund.parent_payment;
                    case PaypalResourceType.capture:
                        return capture.parent_payment;
                    case PaypalResourceType.authorization:
                        return authorization.parent_payment;
                }
                return string.Empty;
            }
        }

    }
}
