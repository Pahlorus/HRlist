using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HRList_BL.Test
{
    [TestClass]
    public class HRBuisnessLogicTest
    {
        public DataTable table;
        public DataRow row;
        public string name;

        [TestMethod]
        public void ReportCreateTest()
        {
            //arrange
            Dictionary<string, string> report;
            table = new DataTable();
            table.Columns.Add("FullName", typeof(String));
            table.Columns.Add("Supervisor", typeof(Boolean));
            table.Columns.Add("Position", typeof(String));
            table.Columns.Add("Name_Unit", typeof(String));
            table.Columns.Add("Name_SubUnit", typeof(String));
            table.Columns.Add("CurrencyCode", typeof(String));
            table.Columns.Add("BaseSalary", typeof(Double));
            table.Columns.Add("DataStart", typeof(DateTime));
            table.Columns.Add("BonusRate_1", typeof(Double));
            table.Columns.Add("BonusRate_2", typeof(Double));
            table.Columns.Add("ExpBonusLimit", typeof(Double));

            row = table.NewRow();
            name = "Name";
            row["FullName"] = "Name";
            row["Supervisor"] = true;
            row["Position"] = "Saleman";
            row["Name_Unit"] = "Division1";
            row["CurrencyCode"] = "$";
            row["Name_SubUnit"] = string.Empty;
            row["BaseSalary"] = 2500.0;
            row["DataStart"] = DateTime.Today;
            row["BonusRate_1"] = 5.0;
            row["BonusRate_2"] = 0.5;
            row["ExpBonusLimit"] = 40.0;
            table.Rows.Add(row);
            string expectedName = name;
            string expectedPosition = "Saleman";
            int expectedCountSubbordinates = 0;
            int expectedExperience = 0;
            double expectedBaseRate = 2500.0;
            double expectedBonusRate1 = 5.0;
            double expectedBonusRate2 = 0.5;
            double expectedSalary = 2500.0;

            //act
            HRBuisnessLogic hRBuisnessLogic = new HRBuisnessLogic();
            report = hRBuisnessLogic.ReportCreate(name, table);
            string actualName = report["ФИО Работника: "];
            string actualPosition = report["Должность: "];
            int actualCountSubbordinates = Convert.ToInt32(report["Количество подчиненых: "]);
            int actualExperience = Convert.ToInt32(report["Стаж, лет: "]);
            double actualBaseRate = Convert.ToDouble(report["Оклад, " + table.Rows[0]["CurrencyCode"] + ": "]);
            double actualBonusRate1 = Convert.ToDouble(report["Надбавка за стаж, %: "]);
            double actualBonusRate2 = Convert.ToDouble(report["Надбавка за подчиненных, %: "]);
            double actualSalary = Convert.ToDouble(report["Заработная плата, начисленная, " + table.Rows[0]["CurrencyCode"] + ": "]);

            //accert
            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedPosition, actualPosition);
            Assert.AreEqual(expectedCountSubbordinates, actualCountSubbordinates);
            Assert.AreEqual(expectedExperience, actualExperience);
            Assert.AreEqual(expectedBaseRate, actualBaseRate);
            Assert.AreEqual(expectedBonusRate1, actualBonusRate1);
            Assert.AreEqual(expectedBonusRate2, actualBonusRate2);
            Assert.AreEqual(expectedSalary, actualSalary);
        }
    }
}
