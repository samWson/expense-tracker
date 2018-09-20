using Android.App;
using Android.Widget;
using Android.OS;

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
            Button submit_expense = FindViewById<Button>(Resource.Id.Submit);

            submit_expense.Click += delegate 
            {
                Expense expense = new Expense(float.Parse(amount.Text), payee.Text, tags.Text);
                System.Console.WriteLine(expense);
            };
        }
    }
}

