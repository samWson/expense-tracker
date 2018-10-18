using System;
using MarcelloDB.Platform;
using MarcelloDB.Collections;
using System.Collections.Generic;

namespace ExpenseTracker
{
    public class DBLayer
    {
        readonly Collection<Expense, int> _expensesCollection;
        readonly Collection<MetaData, string> _metaDataCollection;
        MetaData _expensesCount;

        public DBLayer()
        {
            // Get the platform object for the database
            IPlatform platform = new MarcelloDB.netfx.Platform();

            // Getting the Personal Special Folder. The database will go here.
            var dataPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            // Creating a database session
            var session = new MarcelloDB.Session(platform, dataPath);

            // Getting a collections file. Each collection in the file is a database table
            var expensesFile = session["expenses.dat"];

            // getting the meta data (count of records) from the meta data file
            var expensesMetaDataFile = session["expensesMetaData.dat"];

            // Getting the expenses table and mapping how it will be given an ID (The expense time stamp)
            _expensesCollection = expensesFile.Collection<Expense, int>("expenses", e => e.ID);

            // Getting the meta data table
            _metaDataCollection = expensesMetaDataFile.Collection<MetaData, String>("metaData", e => e.Name);

            // does this table have an auto incrementing key?
            if (!DataBaseInitialized()) {
                _expensesCount = new MetaData("expenses:count", 0);
                _metaDataCollection.Persist(_expensesCount);
            } 
        }

        public void Save(Expense expense)
        {
            expense.ID = NextId();
            _expensesCollection.Persist(expense);
        }

        public IEnumerable<Expense> All()
        {
            return _expensesCollection.All;
        }

        bool DataBaseInitialized()
        {
            _expensesCount = _metaDataCollection.Find("expenses:count");
            return _expensesCount != null;
        }

        int NextId()
        {
            return ++_expensesCount.Value;
        }
    }
}
