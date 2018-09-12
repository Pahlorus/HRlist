using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace HRList_BL
{
    class CountingModul
    {
        #region Fields
        private string name;
        private int age;
        private string officePosition; //Должность
        private static DateTime startTime; //Дата трудоустройства
        private int experience; // Стаж
        private int subbordinatecount_2; //Количество подчиненных Manager
        private int subbordinatecount_3; //Количество подчиненных Employee
        private double baserate; // Оклад
        private double personalbonus; // Персональная надбавка
        private double bonuspercent_1;// Процент премии за стаж
        private double bonuspercent_2; // Процент премии за подчиненных
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public string OfficePosition
        {
            get { return officePosition; }
            set { officePosition = value; }
        }
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        public int SubbordinateCount_2
        {
            get { return subbordinatecount_2; }
            set { subbordinatecount_2 = value; }
        }
        public int SubbordinateCount_3
        {
            get { return subbordinatecount_3; }
            set { subbordinatecount_3 = value; }
        }
        public double BaseRate
        {
            get { return baserate; }
            set { baserate = value; }
        }
        public double PersonalBonus
        {
            get { return personalbonus; }
            set { personalbonus = value; }
        }
        public double BonusPercent_1
        {
            get { return bonuspercent_1; }
            set { bonuspercent_1 = value; }
        }
        public double BonusPercent_2
        {
            get { return bonuspercent_2; }
            set { bonuspercent_2 = value; }
        }
        #endregion

        #region Methods
        // Вычисление стажа.
        public int Experience(string Name, DataSet ds)
        {
            string filter = string.Format("FullName='{0}'", Name);

            ds.Tables[0].Select(filter);
            startTime = ds.Tables[0].Rows[0].Field<DateTime>("DataStart");
            experience = Convert.ToInt32((DateTime.Today.Year - startTime.Year).ToString(), 10);
            return experience;
        }

        // Вычисление количества подчиненных.
        public int CountSubordinates(string Name, DataSet ds)
        {
            int count_div = 0;//количество сотрудников в отделе
            int count_gr = 0;//количество сотрудников в группе
            int count_s;
            string filter = string.Format("FullName='{0}'", Name);
            DataRow[] row = ds.Tables[0].Select(filter);
            bool supervisor = row[0].Field<Boolean>("Supervisor");
            string name_unit = row[0].Field<String>("Name_Unit");
            string name_subunit = row[0].Field<String>("Name_SubUnit");
            DataColumnCollection columns = ds.Tables[0].Columns;

            if (!supervisor) count_s = 0;//условие employee
            else
            {
                string filter_2 = string.Format("Name_Unit='{0}'", name_unit);
                DataRow[] row1 = ds.Tables[0].Select(filter_2);

                if (supervisor && String.IsNullOrEmpty(name_subunit)) // условие начальника отдела (saleman)
                {
                    foreach (DataRow line in row1)
                    {

                        if (line[columns.IndexOf("Name_Unit")].ToString() == name_unit) count_div = count_div + 1;
                    }
                    count_s = count_div - 1;
                }

                else // условие начальника группы  (manager)
                {
                    foreach (DataRow line in row1)
                    {

                        if (line[columns.IndexOf("Name_Unit")].ToString() == name_unit && line[columns.IndexOf("Name_SubUnit")].ToString() == name_subunit) count_gr = count_gr + 1;
                    }

                    count_s = count_gr - 1;
                }
            }
            return count_s;
        }

        // Получение количества отработанных дней (Из табеля или указывается вручную).
        public int Tabel()
        {
            int Count = 21;// функция не реализована принимается стандартное значение
            return Count;
        }

        // Вычисление надбавки менеджера за подчиненных.
        public double BonusManagerSubbordinates(string Name, DataSet ds)
        {
            double bonus_s = 0;
            string filter = string.Format("FullName='{0}'", Name);
            DataRow[] row = ds.Tables[0].Select(filter);
            bool supervisor = row[0].Field<Boolean>("Supervisor");
            string name_unit = row[0].Field<String>("Name_Unit");
            string name_subunit = row[0].Field<String>("Name_SubUnit");
            DataColumnCollection columns = ds.Tables[0].Columns;
            string filter_2 = string.Format("Name_Unit='{0}'", name_unit);
            DataRow[] row1 = ds.Tables[0].Select(filter_2);
            foreach (DataRow line in row1)
            {

                if (line[columns.IndexOf("Supervisor")].ToString() != "True" && line[columns.IndexOf("Name_Unit")].ToString() == name_unit && line[columns.IndexOf("Name_SubUnit")].ToString() == name_subunit)
                    bonus_s = bonus_s + BaseSalary(line[columns.IndexOf("FullName")].ToString(), ds) + BonusExperience(line[columns.IndexOf("FullName")].ToString(), ds); ;
            }

            return bonus_s;
        }

        // Вычисление бонуса за подчиненных.
        public double BonusSubbordinates(string Name, DataSet ds)

        {
            double bonus_s = 0;
            string filter = string.Format("FullName='{0}'", Name);
            DataRow[] row = ds.Tables[0].Select(filter);
            bool supervisor = row[0].Field<Boolean>("Supervisor");
            string name_unit = row[0].Field<String>("Name_Unit");
            string name_subunit = row[0].Field<String>("Name_SubUnit");
            DataColumnCollection columns = ds.Tables[0].Columns;
            double bonusrate = row[0].Field<Double>("BonusRate_2");

            if (!supervisor)
            {
                bonus_s = 0;
            }

            else
            {
                string filter_2 = string.Format("Name_Unit='{0}'", name_unit);
                DataRow[] row1 = ds.Tables[0].Select(filter_2);


                double bonus_s1 = 0;
                double bonus_s2 = 0;
                double bonus_s3 = 0;

                foreach (DataRow line in row1)//сумма зарплат employee в отделе
                {

                    if (line[columns.IndexOf("Supervisor")].ToString() != "True" && line[columns.IndexOf("Name_Unit")].ToString() == name_unit && !String.IsNullOrEmpty(line[columns.IndexOf("Name_SubUnit")].ToString()))
                        bonus_s1 = bonus_s1 + BaseSalary(line[columns.IndexOf("FullName")].ToString(), ds) + BonusExperience(line[columns.IndexOf("FullName")].ToString(), ds);
                }

                foreach (DataRow line in row1)//сумма зарплат employee в группе
                {

                    if (line[columns.IndexOf("Supervisor")].ToString() != "True" && line[columns.IndexOf("Name_Unit")].ToString() == name_unit && line[columns.IndexOf("Name_SubUnit")].ToString() == name_subunit)
                        bonus_s2 = bonus_s2 + BaseSalary(line[columns.IndexOf("FullName")].ToString(), ds) + BonusExperience(line[columns.IndexOf("FullName")].ToString(), ds); ;
                }


                foreach (DataRow line in row1) //сумма зарплаты менеджеров отдела
                {

                    if (line[columns.IndexOf("Supervisor")].ToString() == "True" && line[columns.IndexOf("Name_Unit")].ToString() == name_unit && !String.IsNullOrEmpty(line[columns.IndexOf("Name_SubUnit")].ToString()))

                        bonus_s3 = bonus_s3 + BaseSalary(line[columns.IndexOf("FullName")].ToString(), ds) + BonusExperience(line[columns.IndexOf("FullName")].ToString(), ds) + BonusManagerSubbordinates(Name, ds);
                }


                if (supervisor && String.IsNullOrEmpty(name_subunit)) // условие начальника отдела (saleman)
                {
                    bonus_s = (bonus_s1 + bonus_s3) * bonusrate;

                }

                if (supervisor && !String.IsNullOrEmpty(name_subunit)) // условие начальника группы  (manager)
                {
                    bonus_s = bonus_s2 * bonusrate;
                }
            }


            return bonus_s;

        }

        // Вычисление базовой зарплаты.
        public double BaseSalary(string Name, DataSet ds)
        {

            string filter = string.Format("FullName='{0}'", Name);
            DataRow[] row = ds.Tables[0].Select(filter);
            double basesalary = row[0].Field<Double>("BaseSalary") * Tabel();


            return basesalary;

        }

        // Вычисление бонуса за стаж.
        public double BonusExperience(string Name, DataSet ds)
        {

            string filter = string.Format("FullName='{0}'", Name);
            DataRow[] row = ds.Tables[0].Select(filter);
            double bonusrate = row[0].Field<Double>("BonusRate_1");
            double bonuslimit = row[0].Field<Double>("ExpBonusLimit");

            double bonus_e = (BaseSalary(Name, ds) / 100) * bonusrate * Experience(Name, ds);
            if (bonus_e >= bonuslimit)
            {
                bonus_e = (BaseSalary(Name, ds) / 100) * bonusrate;
            }

            return bonus_e;

        }
        #endregion
    }
}
