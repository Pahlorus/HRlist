
using System.Data;
using System.Data.Common;
using System.Data.SQLite;


namespace HRList_BL
{
    public class DBConnect
    {
        private DataSet _dataset;
        private DataRow _datarow;
        private string connectionString = "Data Source = HR_DB.db; Version=3";


        public DataSet DataSet
        {
            get { return _dataset; }
        }


        public void ConnectToDB(string sqlQuery)
        {
            // ConnectionString = "Data Source = HR_DB.db; Version=3";// добавить выбор БД в настройки.
            _dataset = new DataSet();
            SQLiteConnection dbaseConnection = new SQLiteConnection(connectionString);
            dbaseConnection.Open();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlQuery, dbaseConnection);
            dataAdapter.Fill(_dataset);
            dbaseConnection.Close();
        }
    }
}
