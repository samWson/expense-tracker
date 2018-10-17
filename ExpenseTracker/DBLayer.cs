using System;
using MarcelloDB.Platform;
using MarcelloDB.Collections;
using System.Collections.Generic;

namespace ExpenseTracker
{
    public class DBLayer
    {
        Collection<Expense, DateTime> _expensesCollection;

        public DBLayer()
        {
            // Get the platform object for the database
            IPlatform platform = new MarcelloDB.netfx.Platform();

            // Getting the Personal Special Folder. The database will go here.
            var dataPath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            // Creating a database session
            var session = new MarcelloDB.Session(platform, dataPath);

            // Getting a collections file. Each collection in the file is a database table
            var expensesFile = session["expenses.dat"];

            // Getting the expenses table and mapping how it will be given an ID (The expense time stamp)
            _expensesCollection = expensesFile.Collection<Expense, DateTime>("expenses", e => e.TimeStamp);

        }

        public void Save(Expense expense)
        {
            _expensesCollection.Persist(expense);
        }

        public IEnumerable<Expense> All()
        {
            return _expensesCollection.All;
        }
    }
}
