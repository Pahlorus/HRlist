
using System.Data;
using System.Data.Common;
using System.Data.SQLite;


namespace HRList_BL
{
    public class DBConnect
    {
        public DataSet dataset_1;
        public DataRow datarow_1;

        public void ConnectToDB(string ConnectionString, string SqlQuery )
        {
            // ConnectionString = "Data Source = HR_DB.db; Version=3";// добавить выбор БД в настройки.
            dataset_1 = new DataSet();
            SQLiteConnection DBase = new SQLiteConnection(ConnectionString);
            DBase.Open();
            SQLiteDataAdapter dataadapter = new SQLiteDataAdapter(SqlQuery, DBase);
            dataadapter.Fill(dataset_1);
            DBase.Close();
        }
    }
}
