using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using cs_Vozbrannyi_04.Tools.Exceptions;

namespace cs_Vozbrannyi_04.Models
{
    [Serializable]
    internal class Person
    {
        #region Fields

        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthday = DateTime.Today;


        private int _age = -1;
        private string _sunSign;
        private string _chineseSign;

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                _age = CalcAge();
                _sunSign = CalcSunSign();
                _chineseSign = CalcChineseSign();

                OnPropertyChanged();
                OnPropertyChanged("IsAdult");
                OnPropertyChanged("IsBirthday");
                OnPropertyChanged("BirthdayString");
                OnPropertyChanged("SunSign");
                OnPropertyChanged("ChineseSign");
            }
        }

        #endregion

        #region Constructors

        private Person(string name, string surname)
        {
            _name = name;
            _surname = surname;
        }

        public Person(string name, string surname, string email, DateTime birthday) :
            this(name, surname)
        {
            _email = email;
            _birthday = birthday;
        }

        public Person(string name, string surname, string email) :
            this(name, surname)
        {
            _email = email;
        }

        public Person(string name, string surname, DateTime birthday) :
            this(name, surname)
        {
            _birthday = birthday;
        }

        #endregion

        #region ReadOnlyProps

        private int Age
        {
            get { return (_age == -1) ? (_age = CalcAge()) : _age; }
        }

        public bool IsAdult
        {
            get { return (_age != -1) ? 18 <=_age : 18 <= (_age = CalcAge()); }
        }

        public string SunSign
        {
            get { return _sunSign ?? (_sunSign = CalcSunSign()); }
        }

        public string ChineseSign
        {
            get { return _chineseSign ?? (_chineseSign = CalcChineseSign()); }
        }

        public bool IsBirthday
        {
            get { return (_birthday.Day == DateTime.Today.Day) && (_birthday.Month == DateTime.Today.Month); }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public void ValidatePerson()
        {
            if (Age < 0)
            {
                throw new PersonNotBornException("Person is not born yet");
            }

            if (Age > 135)
            {
                throw new PersonTooOldException("Peron can't be over 135 years old");
            }

            const string pattern =
                @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            if (!Regex.IsMatch(_email, pattern, RegexOptions.IgnoreCase))
            {
                //throw new InvalidEmailException(_email);
                throw new InvalidEmailException("Invalid email!");
            }
        }

        private int CalcAge()
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            DateTime today = DateTime.Today;
            TimeSpan span = today - _birthday;
            int age = (zeroTime + span).Year - 1;

            return age;
        }

        private string CalcSunSign()
        {
            switch (_birthday.Month)
            {
                case 1: return ((_birthday.Day <= 20) ? WesternNames.Козерог : WesternNames.Водолей).ToString(); break;
                case 2: return ((_birthday.Day <= 19) ? WesternNames.Водолей : WesternNames.Рыбы).ToString(); break;
                case 3: return ((_birthday.Day <= 21) ? WesternNames.Рыбы : WesternNames.Овен).ToString(); break;
                case 4: return ((_birthday.Day <= 20) ? WesternNames.Овен : WesternNames.Телец).ToString(); break;
                case 5: return ((_birthday.Day <= 21) ? WesternNames.Телец : WesternNames.Близнецы).ToString(); break;
                case 6: return ((_birthday.Day <= 22) ? WesternNames.Близнецы : WesternNames.Рак).ToString(); break;
                case 7: return ((_birthday.Day <= 23) ? WesternNames.Рак : WesternNames.Лев).ToString(); break;
                case 8: return ((_birthday.Day <= 23) ? WesternNames.Лев : WesternNames.Дева).ToString(); break;
                case 9: return ((_birthday.Day <= 24) ? WesternNames.Дева : WesternNames.Весы).ToString(); break;
                case 10: return ((_birthday.Day <= 23) ? WesternNames.Весы : WesternNames.Скорпион).ToString(); break;
                case 11: return ((_birthday.Day <= 23) ? WesternNames.Скорпион : WesternNames.Стрелец).ToString(); break;
                case 12: return ((_birthday.Day <= 22) ? WesternNames.Стрелец : WesternNames.Козерог).ToString(); break;
                default: return "Error";
            }
        }

        private string CalcChineseSign()
        {
            int year = _birthday.Year;
            int resultNumber = (year - 4) % 12;

            switch (resultNumber)
            {
                case 0: return ChineseNames.Крыса.ToString();
                case 1: return ChineseNames.Бык.ToString();
                case 2: return ChineseNames.Тигр.ToString();
                case 3: return ChineseNames.Заяц.ToString();
                case 4: return ChineseNames.Дракон.ToString();
                case 5: return ChineseNames.Змея.ToString();
                case 6: return ChineseNames.Лошадь.ToString();
                case 7: return ChineseNames.Овца.ToString();
                case 8: return ChineseNames.Обезьяна.ToString();
                case 9: return ChineseNames.Петух.ToString();
                case 10: return ChineseNames.Собака.ToString();
                case 11: return ChineseNames.Кабан.ToString();
                default: return "Error";
            }
        }
    }
}