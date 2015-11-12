using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using eOne.Common.Helpers;

namespace eOne.Common.Connectors
{
    public class ConnectorField
    {

        public enum ConnectorFieldFormatType
        {
            None, 
            Email, 
            Website, 
            Image, 
            Skype, 
            SkypePhone, 
            Twitter, 
            Color,
            Custom
        }

        #region Classes

        public class ConnectorFieldListItem
        {
            public ConnectorFieldListItem() { }
            public ConnectorFieldListItem(string item, string value)
            {
                Item = item;
                Value = value;
            }
            public string Item { get; set; }
            public string Value { get; set; }
            public override string ToString()
            {
                return Value;
            }
        }

        #endregion

        public ConnectorField()
        {
            ListItems = new List<ConnectorFieldListItem>();
            FieldsRequiredForCalculation = new List<string>();
        }

        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string Calculation { get; set; }
        public string DisplayName { get; set; }
        public string ApiName { get; set; }
        public string Table { get; set; }
        public ConnectorFieldType Type { get; set; }
        public List<ConnectorFieldListItem> ListItems { get; set; }
        public string StringMask { get; set; }
        public string MaskChar { get; set; }
        public bool DefaultField { get; set; }
        public bool Enabled { get; set; }
        public string SummaryMethod { get; set; }
        public int DecimalPlaces { get; set; }
        public ConnectorEntity ParentEntity { get; set; }
        public bool AllowRead { get; set; }
        public bool AllowWrite { get; set; }
        public ConnectorFieldFormatType FormatType { get; set; }
        public string CustomFormatString { get; set; }
        public int ImageWidth { get; set; }
        public List<string> FieldsRequiredForCalculation { get; set; }
        public int KeyNumber { get; set; }
        public bool CreateDate { get; set; }
        public bool CreateTime { get; set; }
        public bool ModifyDate { get; set; }
        public bool ModifyTime { get; set; }
        public int SearchPriority { get; set; }
        public bool Hidden { get; set; }
        public bool DisplayString { get; set; }
        public string XmlName => XmlHelper.GetXmlFieldName(DisplayName);
        public PropertyInfo propertyInfo { get; set; }

        public bool IsNumeric => Type.Type == typeof(decimal) || Type.Type == typeof(int);
        public bool IsString => Type.Type == typeof(string);
        public bool IsCalculation => FieldsRequiredForCalculation != null && FieldsRequiredForCalculation.Count > 0;

        #endregion

        #region Methods

        public string Format(string value, string description = "")
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            switch (FormatType)
            {
                case ConnectorFieldFormatType.Email:
                    return string.Format("<a href='mailto:{0}' target='_blank'>{0}</a>", value);
                case ConnectorFieldFormatType.Image:
                    return $"<img src='{value}' width='{ImageWidth}' />";
                case ConnectorFieldFormatType.Skype:
                    string chat = $"<a href='skype:{value}?chat'><i class='fa fa-weixin'></i> </a>";
                    string call = $"<a href='skype:{value}?call'><i class='fa fa-phone'></i></a>";
                    string video = $"<a href='skype:{value}?call&video=true'><i class='fa fa-video-camera'></i></a>";
                    return $"{value} {chat}{call}{video}";
                case ConnectorFieldFormatType.SkypePhone:
                    return string.Format("{0} <a href='skype:{0}?call'><i class='fa fa-skype'></i></a>", value);
                case ConnectorFieldFormatType.Website:
                    return $"<a href='{value}' target='_blank'>{(string.IsNullOrWhiteSpace(description) ? value : description)}</a>";
                case ConnectorFieldFormatType.Twitter:
                    return string.Format("<a href='https://twitter.com/{0}' target='_blank'>{0}</a>", value);
                case ConnectorFieldFormatType.Custom:
                    return string.Format(CustomFormatString, value, description);
            }
            return value;
        }
        public void AddListItems(int start, List<string> values)
        {
            var item = start;
            foreach (var value in values)
            {
                if (!string.IsNullOrWhiteSpace(value)) ListItems.Add(new ConnectorFieldListItem(item.ToString(CultureInfo.InvariantCulture), value));
                item++;
            }
        }
        public override string ToString()
        {
            return DisplayName;
        }
        public string GetListDescription(string item)
        {
            if (ListItems == null) return item;
            foreach (var listItem in ListItems.Where(listItem => listItem.Item == item)) return listItem.Value;
            return item;
        }

        #endregion

    }
}
