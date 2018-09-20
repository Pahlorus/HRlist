using System;
using System.Data;

namespace HRList_BL
{
    internal class Worker
    {
        #region Fields
        private string _name;
        private int _age;
        // Должность.
        private string _officePosition;
        // Дата трудоустройства.
        private static DateTime _startTime;
        // Стаж.
        private int _experience; 
        private int _countSubbordinates;
        // Оклад.
        private double _baserate;
        // Персональная надбавка. Не реализовано.
        private double _personalbonus;
        // Процент премии за стаж.
        private double _bonuspercent_1;
        // Процент премии за подчиненных.
        private double _bonuspercent_2; 
        #endregion
        
        public Worker(string name)
        {
            _name = name;
        }


        #region Properties
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Experience
        {
            get { return _experience; }
            set { _experience = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
         public string OfficePosition
        {
            get { return _officePosition; }
            set { _officePosition = value; }
        }
        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        public int CountSubbordinates
        {
            get { return _countSubbordinates; }
            set { _countSubbordinates = value; }
        }

        public double BaseRate
        {
            get { return _baserate; }
            set { _baserate = value; }
        }
        public double PersonalBonus
        {
            get { return _personalbonus; }
            set { _personalbonus = value; }
        }
        public double BonusPercent_1
        {
            get { return _bonuspercent_1; }
            set { _bonuspercent_1 = value; }
        }
        public double BonusPercent_2
        {
            get { return _bonuspercent_2; }
            set { _bonuspercent_2 = value; }
        }
        #endregion

    }
}
