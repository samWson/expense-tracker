
using Android.App;
using Android.OS;
using Android.Widget;

namespace ExpenseTracker
{
    [Activity(Label = "AllExpenses")]
    public class AllExpenses : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AllExpenses);

            var expenses = Intent.Extras.GetStringArrayList("expenses");

            this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, expenses);
        }
    }
}
