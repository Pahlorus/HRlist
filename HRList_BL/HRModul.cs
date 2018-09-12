using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRList_BL
{
    class HRModul
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
    }
}
