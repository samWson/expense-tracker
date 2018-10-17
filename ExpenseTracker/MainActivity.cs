using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;

namespace ExpenseTracker
{
    [Activity(Label = "Expense Tracker", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            EditText amount = FindViewById<EditText>(Resource.Id.Amount);
            EditText payee = FindViewById<EditText>(Resource.Id.Payee);
            EditText tags = FindViewById<EditText>(Resource.Id.Tags);
            Button submitExpense = FindViewById<Button>(Resource.Id.Submit);
            Button viewExpenses = FindViewById<Button>(Resource.Id.ViewExpenses);

            DBLayer dBLayer = new DBLayer();

            submitExpense.Click += delegate {
                Expense expense = new Expense(float.Parse(amount.Text), payee.Text, tags.Text);

                // Saving the expense to the database table
                dBLayer.Save(expense);
            };

            viewExpenses.Click += delegate {
                List<string> expenses = new List<string>();

                foreach (Expense expense in dBLayer.All()) {
                    expenses.Add(expense.ToString());
                }

                var intent = new Intent(this, typeof(AllExpenses));
                intent.PutStringArrayListExtra("expenses" ,expenses);
                StartActivity(intent);
            };
        }
    }
}

