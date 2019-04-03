using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using cs_Vozbrannyi_04.Models;
using cs_Vozbrannyi_04.Tools;
using cs_Vozbrannyi_04.Tools.Exceptions;
using cs_Vozbrannyi_04.Tools.Managers;
using cs_Vozbrannyi_04.Tools.Navigation;

namespace cs_Vozbrannyi_04.ViewModels
{
    class AddPersonViewModel:BaseViewModel, INotifyPropertyChanged
    {
        #region Fields

        private Person _person = StationManager.CurrentPerson;

        private RelayCommand<object> _proceedCommand;
        private RelayCommand<object> _cancelCommand;
        #endregion

        #region Properties
        public Person NewPerson
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<Object> ProceedCommand
        {
            get
            {
                return _proceedCommand ?? (_proceedCommand = new RelayCommand<object>(
                           ProceedImplementation, CanExecuteProceed));
            }
        }

        public RelayCommand<Object> CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(
                           CancelImplementation));
            }
        }

        #endregion

        private async void ProceedImplementation(object obj)
        {
            LoaderManeger.Instance.ShowLoader();
            bool res = await Task.Run(() => {
                try
                {
                    _person.ValidatePerson();
                }
                catch (ArgumentException e)
                {
                    MessageBox.Show($"Ошибка: {e.Message}");
                    return false;
                }
                return true;
            });
            LoaderManeger.Instance.HideLoader();
            if (res)
            {
                StationManager.DataStorage.AddPerson(_person);
                _person = new Person("", "", "");
                NewPerson = _person;

                //cancel window
                StationManager.DataVM.UpdateInfo();
                NavigationManager.Instance.Navigate(ViewType.DataView);
            }
        }

        private bool CanExecuteProceed(Object obj)
        {
            return !String.IsNullOrWhiteSpace(NewPerson.Email) && !String.IsNullOrWhiteSpace(NewPerson.Name) && !String.IsNullOrWhiteSpace(NewPerson.Surname);
        }

        private async void CancelImplementation(object obj)
        {
            StationManager.DataVM.UpdateInfo();
            NavigationManager.Instance.Navigate(ViewType.DataView);
        }
    }
}
