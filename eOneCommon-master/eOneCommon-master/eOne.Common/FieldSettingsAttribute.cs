using System;

namespace eOne.Common
{
    public class FieldSettingsAttribute : Attribute
    {

        public string DisplayName;
        public int FieldTypeId;
        public bool DefaultField;
        public Type EnumType;
        public string FieldsRequiredForCalculation;
        public int KeyNumber;
        public bool Created;
        public bool Modified;
        public int SearchPriority;
        public bool Hidden;

        public FieldSettingsAttribute(string displayName)
        {
            DisplayName = displayName;
            SearchPriority = 1;
            FieldTypeId = 1;    // default to string
            Hidden = false;
        }

    }
}
