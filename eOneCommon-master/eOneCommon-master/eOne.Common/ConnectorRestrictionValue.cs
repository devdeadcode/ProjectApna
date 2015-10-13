using System;
using System.Xml.Linq;
using eOne.Common.DataConnectors;
using eOne.Common.Helpers;

namespace eOne.Common
{
    
    public class ConnectorRestrictionValue
    {

        private ConnectorRestrictionDateValueType _dateValue;

        public enum ConnectorRestrictionValueType { Constant, Field, Calculation, DateTimeValue }
        public enum ConnectorRestrictionDateValueType
        {
            Today,
            Yesterday,
            Tomorrow,
            StartOfWeek,
            EndOfWeek,
            StartOfMonth,
            EndOfMonth,
            StartOfQuarter,
            EndOfQuarter,
            StartOfYear,
            EndOfYear,
            StartOfPeriod,
            EndOfPeriod
        }

        public ConnectorRestrictionValue() { }
        public ConnectorRestrictionValue(string value)
        {
            Type = ConnectorRestrictionValueType.Constant;
            Constant = value;
        }
        public ConnectorRestrictionValue(DateTime value)
        {
            Type = ConnectorRestrictionValueType.Constant;
            DateConstant = value;
        }
        public ConnectorRestrictionValue(DataConnectorField field)
        {
            Type = ConnectorRestrictionValueType.Field;
            Field = field;
        }
        public ConnectorRestrictionValue(ConnectorRestrictionDateValueType value)
        {
            Type = ConnectorRestrictionValueType.DateTimeValue;
            DateValue = value;
        }
        public ConnectorRestrictionValue(XElement xmlNode, DataConnectorEntity entity)
        {
            Type = (ConnectorRestrictionValueType)Enum.Parse(typeof(ConnectorRestrictionValueType), xmlNode.Name.ToString());
            switch (Type)
            {
                case ConnectorRestrictionValueType.Calculation:
                    //todo
                    break;
                case ConnectorRestrictionValueType.Constant:
                    Constant = xmlNode.Value;
                    break;
                case ConnectorRestrictionValueType.DateTimeValue:
                    DateValue = (ConnectorRestrictionDateValueType)Enum.Parse(typeof(ConnectorRestrictionDateValueType), xmlNode.Value);
                    break;
                case ConnectorRestrictionValueType.Field:
                    var fieldId = int.Parse(xmlNode.Value);
                    Field = entity.FindField(fieldId);
                    break;
            }
        }

        public ConnectorRestrictionValueType Type { get; set; }
        public string Constant { get; set; }
        public ConnectorRestrictionDateValueType DateValue
        {
            get
            {
                return _dateValue;
            }
            set
            {
                _dateValue = value;
                DateConstant = DateHelper.GetDateValue(_dateValue);
            }
        }
        public DataConnectorField Field { get; set; }
        public string CalculationName { get; set; }
        public DateTime DateConstant { get; set; }

    }
}
