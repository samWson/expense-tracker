using Android.App;
using Android.Widget;
using Android.OS;
using MarcelloDB.Platform;
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

            // Get the platform object for the database
            IPlatform platform = new MarcelloDB.netfx.Platform();

            // Getting the Personal Special Folder. The database will go here.
            var dataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            // Creating a database session
            var session = new MarcelloDB.Session(platform, dataPath);

            // Getting a collections file. Each collection in the file is a database table
            var expensesFile = session["expenses.dat"];

            // Getting the expenses table and mapping how it will be given an ID (The expense time stamp)
            var expensesColleciton = expensesFile.Collection<Expense, System.DateTime>("expenses", e => e.TimeStamp);

            submitExpense.Click += delegate {
                Expense expense = new Expense(float.Parse(amount.Text), payee.Text, tags.Text);

                // Saving the expense to the database table
                expensesColleciton.Persist(expense);
            };

            viewExpenses.Click += delegate {
                List<string> expenses = new List<string>();

                foreach (Expense expense in expensesColleciton.All) {
                    expenses.Add(expense.ToString());
                }

                var intent = new Intent(this, typeof(AllExpenses));
                intent.PutStringArrayListExtra("expenses" ,expenses);
                StartActivity(intent);
            };
        }
    }
}

