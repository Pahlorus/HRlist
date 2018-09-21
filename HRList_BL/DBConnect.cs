using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections.Generic;

namespace HRList_BL
{
    // Необходимо вынести классы для работы с БД в отдельную сборку
    class DBConnect : IDb
    {
        // Имя отдела.
        private string nameunit;
        // Имя группы.
        private string namesubunit;
        // Выбор БД необходимо перенести в настройки.
        private string _connectionString = "Data Source = HR_DB.db; foreign keys=true; EnforceFKConstraints=Yes|True|1; Version=3";
        private SQLQueries _sqlQueries;
        private SQLiteConnection _dbaseConnection;
        SQLiteDataAdapter _dataAdapter;


        public DBConnect()
        {
            _sqlQueries = new SQLQueries();
        }

        event EventHandler DataSetError;
        event EventHandler DataGetError;
        event EventHandler ConnectError;


        private void GetSettings(Dictionary<string, string> dictionary, string SQLQueries)
        {
            try
            {
                DataTable table = new DataTable();
                GetData(SQLQueries, table);
                dictionary.Clear();
                foreach (DataRow row in table.Rows)
                {
                    dictionary.Add(row[1].ToString(), row[0].ToString());
                }
            }
            catch (Exception)
            {
                DataGetError?.Invoke(this, EventArgs.Empty);
            }
        }

        private void GetData(string sqlQuery, DataTable table)
        {
            try
            {
                ConnectDB(sqlQuery);
                _dataAdapter.Fill(table);
                _dbaseConnection.Close();
            }
            catch (Exception)
            {
                DataGetError?.Invoke(this, EventArgs.Empty);
            }
        }

        private void ConnectDB(string sqlQuery)
        {
            try
            {
                _dbaseConnection = new SQLiteConnection(_connectionString);
                _dbaseConnection.Open();
                _dataAdapter = new SQLiteDataAdapter(sqlQuery, _dbaseConnection);
            }
            catch (Exception)
            {
                ConnectError?.Invoke(this, EventArgs.Empty);
            }
        }

        public void GetSettingsAll(Dictionary<string, string> UnitList, Dictionary<string, string> subUnitList, Dictionary<string, string> positionList, Dictionary<string, string> accessRulesList)
        {
            GetSettings(UnitList, SQLQueries.Instance.UnitQuery);
            GetSettings(subUnitList, SQLQueries.Instance.SubunitQuery);
            GetSettings(positionList, SQLQueries.Instance.PositionQuery);
            GetSettings(accessRulesList, SQLQueries.Instance.AccessRulesQuery);
        }

        public void SetData(Dictionary<string, string> newUser, Dictionary<string, string> UnitList, Dictionary<string, string> subUnitList, Dictionary<string, string> positionList, Dictionary<string, string> accessRulesList, DataTable table)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand("INSERT INTO StaffList (ID_Human, FullName, DataStart, Password, Supervisor, ID_Position, ID_Unit, ID_Subunit, ID_Access) " +
                                                          "VALUES (@ID_Human, @FullName, @DataStart, @Password, @Supervisor, @ID_Position, @ID_Unit, @ID_Subunit, @ID_Access)",
                                                           _dbaseConnection);

                command.Parameters.AddWithValue("@ID_Human", null);
                command.Parameters.AddWithValue("@FullName", newUser["ФИО Работника: "]);
                command.Parameters.AddWithValue("@DataStart", DateTime.Today);
                command.Parameters.AddWithValue("@Password", 1);
                command.Parameters.AddWithValue("@Supervisor", Convert.ToBoolean(newUser["Начальник: "]));
                command.Parameters.AddWithValue("@ID_Position", Convert.ToInt32(positionList[newUser["Должность: "]]));
                command.Parameters.AddWithValue("@ID_Unit", Convert.ToInt32(UnitList[newUser["Отдел: "]]));
                command.Parameters.AddWithValue("@ID_Subunit", Convert.ToInt32(subUnitList[newUser["Группа: "]]));
                command.Parameters.AddWithValue("@ID_Access", Convert.ToInt32(accessRulesList[newUser["Должность: "]]));
                _dataAdapter.InsertCommand = command;
                _dataAdapter.Update(table);
               _dbaseConnection.Close();
            }
            catch (Exception e)
            {
                DataSetError?.Invoke(this, EventArgs.Empty);
                MessageBox.Show(e.Message);
            }
        }

        public bool AuthorizationDB(string login, string password, out string rules, out int idRules)
        {
            bool result = false;
            rules = string.Empty;
            idRules = 0;
            DataTable table = new DataTable();
            GetData(SQLQueries.Instance.UserListQuery, table);
            foreach (DataRow row in table.Rows)
            {
                if (login == row[0].ToString() && password == row[1].ToString())
                {
                    rules = row[3].ToString();
                    idRules = Convert.ToInt32(row[2]);
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void GetUserData(string login, int idRules, DataTable table)
        {
            GetData(SQLQueries.Instance.GeneralQuery, table);
            foreach (DataRow row in table.Rows)
            {
                if (login == row[1].ToString())
                {
                    nameunit = row[4].ToString();
                    namesubunit = row[5].ToString();
                    table.DefaultView.RowFilter = SQLQueries.Instance.UserRulesFilterCreate(idRules, login, nameunit, namesubunit);
                    break;
                }
            }
        }
    }
}
