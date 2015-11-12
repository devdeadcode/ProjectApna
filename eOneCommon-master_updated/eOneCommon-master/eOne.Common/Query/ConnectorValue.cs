using System;
using System.Xml.Linq;
using eOne.Common.Connectors;
using eOne.Common.Helpers;

namespace eOne.Common.Query
{
    
    public class ConnectorValue
    {

        private ConnectorDateValueType _dateValue;

        public enum ConnectorValueType { Constant, Field, Calculation, DateTimeValue }
        public enum ConnectorDateValueType
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

        public ConnectorValue() { }
        public ConnectorValue(string value)
        {
            Type = ConnectorValueType.Constant;
            Constant = value;
        }
        public ConnectorValue(DateTime value)
        {
            Type = ConnectorValueType.Constant;
            DateConstant = value;
        }
        public ConnectorValue(ConnectorField field)
        {
            Type = ConnectorValueType.Field;
            Field = field;
        }
        public ConnectorValue(ConnectorDateValueType value)
        {
            Type = ConnectorValueType.DateTimeValue;
            DateValue = value;
        }
        public ConnectorValue(XElement xmlNode, ConnectorEntity entity)
        {
            Type = (ConnectorValueType)Enum.Parse(typeof(ConnectorValueType), xmlNode.Name.ToString());
            switch (Type)
            {
                case ConnectorValueType.Calculation:
                    //todo
                    break;
                case ConnectorValueType.Constant:
                    Constant = xmlNode.Value;
                    break;
                case ConnectorValueType.DateTimeValue:
                    DateValue = (ConnectorDateValueType)Enum.Parse(typeof(ConnectorDateValueType), xmlNode.Value);
                    break;
                case ConnectorValueType.Field:
                    var fieldId = int.Parse(xmlNode.Value);
                    Field = entity.FindField(fieldId);
                    break;
            }
        }

        public ConnectorValueType Type { get; set; }
        public string Constant { get; set; }
        public ConnectorDateValueType DateValue
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
        public ConnectorField Field { get; set; }
        public string CalculationName { get; set; }
        public DateTime DateConstant { get; set; }

    }
}
