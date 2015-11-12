namespace FilterControl.Classes
{
    public class FilterValue
    {

        public Enums.ValueType Type { get; set; }
        public string ConstantValue { get; set; }
        public FilterColumn Column { get; set; }
        public Enums.ValueFunction Function { get; set; }

    }
}
