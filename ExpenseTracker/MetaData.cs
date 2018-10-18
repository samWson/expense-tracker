namespace ExpenseTracker
{
    class MetaData
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public MetaData(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}