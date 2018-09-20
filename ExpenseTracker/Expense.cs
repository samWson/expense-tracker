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

        public Expense(float amount, string payee, string tags)
        {
            this.Amount = amount;
            this.Payee = payee;
            this.Tags = tags;
            this.TimeStamp = DateTime.Now;
        }
    }
}
