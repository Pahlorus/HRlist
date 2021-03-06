﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace HRList_BL
{
    class CountingModul
    {
        public double SalaryCalculation(string name, DataTable table)
        {
            return BaseSalary(name, table) + BonusExperience(name, table) + BonusSubbordinates(name, table);
        }

        public int Experience(string name, DataTable table)
        {
            string filter = string.Format("FullName= '{0}'", name);
            DataRow[] rows = table.Select(filter);
            return Convert.ToInt32((DateTime.Today.Year - rows[0].Field<DateTime>("DataStart").Year).ToString(), 10);
        }

        public int CountSubordinates(string name, DataTable table)
        {
            int countWorkerDivision = 0;
            int countWorkerGroup = 0;
            int countSubbordinates;
            string filter = string.Format("FullName='{0}'", name);
            DataRow[] row = table.Select(filter);
            bool supervisor = row[0].Field<Boolean>("Supervisor");
            string name_unit = row[0].Field<String>("Name_Unit");
            string name_subunit = row[0].Field<String>("Name_SubUnit");
            DataColumnCollection columns = table.Columns;

            if (!supervisor) countSubbordinates = 0;
            else
            {
                string filter_2 = string.Format("Name_Unit='{0}'", name_unit);
                DataRow[] row1 = table.Select(filter_2);

                // Eсловие начальника отдела (Saleman).
                if (supervisor && String.IsNullOrEmpty(name_subunit)) 
                {
                    foreach (DataRow line in row1)
                    {
                        if (line[columns.IndexOf("Name_Unit")].ToString() == name_unit) countWorkerDivision = countWorkerDivision + 1;
                    }
                    countSubbordinates = countWorkerDivision - 1;
                }

                // Условие начальника группы  (Manager).
                else
                {
                    foreach (DataRow line in row1)
                    {
                        if (line[columns.IndexOf("Name_Unit")].ToString() == name_unit && line[columns.IndexOf("Name_SubUnit")].ToString() == name_subunit) countWorkerGroup = countWorkerGroup + 1;
                    }
                    countSubbordinates = countWorkerGroup - 1;
                }
            }
            return countSubbordinates;
        }

        // Получение количества отработанных дней (Из табеля или указывается вручную).
        public int GetTabel()
        {
            // Функция не реализована принимается стандартное значение 1, т.е. отработан полный месяц.
            int Count = 1;
            return Count;
        }

        // Вычисление надбавки менеджера за подчиненных.
        public double BonusManagerSubbordinates(string name, DataTable table)
        {
            double bonus_s = 0;
            string filter = string.Format("FullName='{0}'", name);
            DataRow[] row = table.Select(filter);
            bool supervisor = row[0].Field<Boolean>("Supervisor");
            string name_unit = row[0].Field<String>("Name_Unit");
            string name_subunit = row[0].Field<String>("Name_SubUnit");
            DataColumnCollection columns = table.Columns;
            string filter_2 = string.Format("Name_Unit='{0}'", name_unit);
            DataRow[] row1 = table.Select(filter_2);
            foreach (DataRow line in row1)
            {
                if (line[columns.IndexOf("Supervisor")].ToString() != "True" && line[columns.IndexOf("Name_Unit")].ToString() == name_unit && line[columns.IndexOf("Name_SubUnit")].ToString() == name_subunit)
                    bonus_s = bonus_s + BaseSalary(line[columns.IndexOf("FullName")].ToString(), table) + BonusExperience(line[columns.IndexOf("FullName")].ToString(), table); ;
            }
            return bonus_s;
        }

        // Вычисление бонуса за подчиненных.
        public double BonusSubbordinates(string name, DataTable table)
        {
            double bonus_s = 0;
            string filter = string.Format("FullName='{0}'", name);
            DataRow[] row = table.Select(filter);
            bool supervisor = row[0].Field<Boolean>("Supervisor");
            string name_unit = row[0].Field<String>("Name_Unit");
            string name_subunit = row[0].Field<String>("Name_SubUnit");
            DataColumnCollection columns = table.Columns;
            double bonusrate = row[0].Field<Double>("BonusRate_2");

            if (!supervisor)
            {
                bonus_s = 0;
            }

            else
            {
                string filter_2 = string.Format("Name_Unit='{0}'", name_unit);
                DataRow[] row1 = table.Select(filter_2);


                double bonus_s1 = 0;
                double bonus_s2 = 0;
                double bonus_s3 = 0;

                foreach (DataRow line in row1)//сумма зарплат employee в отделе
                {

                    if (line[columns.IndexOf("Supervisor")].ToString() != "True" && line[columns.IndexOf("Name_Unit")].ToString() == name_unit && !String.IsNullOrEmpty(line[columns.IndexOf("Name_SubUnit")].ToString()))
                        bonus_s1 = bonus_s1 + BaseSalary(line[columns.IndexOf("FullName")].ToString(), table) + BonusExperience(line[columns.IndexOf("FullName")].ToString(), table);
                }

                foreach (DataRow line in row1)//сумма зарплат employee в группе
                {

                    if (line[columns.IndexOf("Supervisor")].ToString() != "True" && line[columns.IndexOf("Name_Unit")].ToString() == name_unit && line[columns.IndexOf("Name_SubUnit")].ToString() == name_subunit)
                        bonus_s2 = bonus_s2 + BaseSalary(line[columns.IndexOf("FullName")].ToString(), table) + BonusExperience(line[columns.IndexOf("FullName")].ToString(), table); ;
                }


                foreach (DataRow line in row1) //сумма зарплаты менеджеров отдела
                {

                    if (line[columns.IndexOf("Supervisor")].ToString() == "True" && line[columns.IndexOf("Name_Unit")].ToString() == name_unit && !String.IsNullOrEmpty(line[columns.IndexOf("Name_SubUnit")].ToString()))

                        bonus_s3 = bonus_s3 + BaseSalary(line[columns.IndexOf("FullName")].ToString(), table) + BonusExperience(line[columns.IndexOf("FullName")].ToString(), table) + BonusManagerSubbordinates(name, table);
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
        public double BaseSalary(string name, DataTable table)
        {
            string filter = string.Format("FullName='{0}'", name);
            DataRow[] row = table.Select(filter);
            double basesalary = row[0].Field<Double>("BaseSalary") * GetTabel();
            return basesalary;
        }

        // Вычисление бонуса за стаж.
        public double BonusExperience(string name, DataTable table)
        {
            string filter = string.Format("FullName='{0}'", name);
            DataRow[] row = table.Select(filter);
            double bonusrate = row[0].Field<Double>("BonusRate_1");
            double bonuslimit = row[0].Field<Double>("ExpBonusLimit");

            double bonus_e = (BaseSalary(name, table) / 100) * bonusrate * Experience(name, table);
            if (bonus_e >= bonuslimit)
            {
                bonus_e = (BaseSalary(name, table) / 100) * bonusrate;
            }
            return bonus_e;
        }
    }
}
