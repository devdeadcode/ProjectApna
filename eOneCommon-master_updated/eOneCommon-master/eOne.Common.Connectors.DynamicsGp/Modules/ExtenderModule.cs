using System;

namespace eOne.Common.Connectors.DynamicsGp.Modules
{
    public class ExtenderModule : DynamicsGpModule
    {

        const short ExtenderFieldTypeIdShortString = 2;
        const short ExtenderFieldTypeIdLongString = 1;
        const short ExtenderFieldTypeIdDate = 3;
        const short ExtenderFieldTypeIdCurrency = 4;
        const short ExtenderFieldTypeIdNumber = 5;
        const short ExtenderFieldTypeIdAccount = 15;
        const short ExtenderFieldTypeIdCalculated = 13;
        const short ExtenderFieldTypeIdCheckbox = 7;
        const short ExtenderFieldTypeIdQuantity = 10;
        const short ExtenderFieldTypeIdPercentage = 9;
        const short ExtenderFieldTypeIdEmail = 17;
        const short ExtenderFieldTypeIdFile = 11;
        const short ExtenderFieldTypeIdFolder = 12;
        const short ExtenderFieldTypeIdLabel = 20;
        const short ExtenderFieldTypeIdLinkedLookup = 18;
        const short ExtenderFieldTypeIdList = 6;
        const short ExtenderFieldTypeIdLookup = 8;
        const short ExtenderFieldTypeIdPhone = 16;
        const short ExtenderFieldTypeIdTime = 14;

        public ExtenderModule(DynamicsGpConnector connector) : base(connector)
        {
            Id = 949;
            Name = "Extender";
            Installed = true;
            ParentConnector = connector;
        }

        public override void AddEntities()
        {
            throw new NotImplementedException();
        }
    }
}
