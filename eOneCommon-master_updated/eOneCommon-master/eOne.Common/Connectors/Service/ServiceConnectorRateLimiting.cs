namespace eOne.Common.Connectors.Service
{
    public class ServiceConnectorRateLimiting
    {

        public ServiceConnectorRateLimiting()
        {
            NumberOfPeriods = 1;
        }
        public ServiceConnectorRateLimiting(LimitAppliedTo appliedTo, int requests, LimitPeriod period, int numberOfPeriods = 1)
        {
            AppliedTo = appliedTo;
            Requests = requests;
            Period = period;
            NumberOfPeriods = numberOfPeriods;
        }

        public enum LimitPeriod
        {
            None,
            Second,
            Minute,
            Hour,
            Day
        }
        public enum LimitAppliedTo
        {
            None,
            Endpoint,
            User,
            Account
        }

        public int Requests { get; set; }
        public LimitPeriod Period { get; set; }
        public int NumberOfPeriods { get; set; }
        public LimitAppliedTo AppliedTo { get; set; }
        public string Endpoint { get; set; }
        public int NumberOfSeconds
        {
            get
            {
                switch (Period)
                {
                    case LimitPeriod.Second:
                        return NumberOfPeriods;
                    case LimitPeriod.Minute:
                        return NumberOfPeriods * 60;
                    case LimitPeriod.Hour:
                        return NumberOfPeriods * 60 * 60;
                    case LimitPeriod.Day:
                        return NumberOfPeriods * 60 * 60 * 24;
                    default:
                        return 0;
                }
            }
        }

    }
}
