using System;

namespace ExpenseTracker
{
    public class Expense
    {
        public float Amount
        { get; set; }

        public string Payee
        { get; set; }

        public string Tags
        { get; set; }

        public DateTime TimeStamp
        { get; set; }

        public int ID
        { get; set; }

        public Expense(float amount, string payee, string tags)
        {
            Amount = amount;
            Payee = payee;
            Tags = tags;
            TimeStamp = DateTime.Now;
        }

        public override string ToString()
        {
            return String.Format("${0}, {1}, {2}, {3}", Amount, Payee, Tags, TimeStamp);
        }
    }
}
