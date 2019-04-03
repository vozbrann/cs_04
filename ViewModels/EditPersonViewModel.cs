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
    class EditPersonViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public EditPersonViewModel()
        {
            StationManager.EditVM = this;
        }
        #region Fields
        
        private Person _person = StationManager.CurrentPerson;

        //Person object, which fields will be altered not to allow bad fields, when smth happens
        private Person _editedPerson = StationManager.TestPerson;

        private RelayCommand<object> _confirmCommand;
        private RelayCommand<object> _cancelCommand;
        #endregion

        #region Properties
        public Person EditedPerson
        {
            get { return _editedPerson; }
            set
            {
                _editedPerson = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<Object> ConfirmCommand
        {
            get
            {
                return _confirmCommand ?? (_confirmCommand = new RelayCommand<object>(
                           ConfirmImplementation, CanExecuteProceed));
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

        private async void ConfirmImplementation(object obj)
        {
            LoaderManeger.Instance.ShowLoader();
            bool res = await Task.Run(() => {
                try
                {
                    _editedPerson.ValidatePerson();
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
                _person.Name = _editedPerson.Name;
                _person.Surname = _editedPerson.Surname;
                _person.Email = _editedPerson.Email;
                _person.Birthday = _editedPerson.Birthday;
                StationManager.TestPerson = null;
                StationManager.DataStorage.ApplyChanges();
                StationManager.DataVM.UpdateInfo();
                NavigationManager.Instance.Navigate(ViewType.DataView);
            }
        }

        private bool CanExecuteProceed(Object obj)
        {
            return !String.IsNullOrWhiteSpace(EditedPerson.Email) &&
                   !String.IsNullOrWhiteSpace(EditedPerson.Name) &&
                   !String.IsNullOrWhiteSpace(EditedPerson.Surname);
        }

        private async void CancelImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.DataView);
        }

        public void updateAll()
        {
            EditedPerson = StationManager.TestPerson;
            _person = StationManager.CurrentPerson;
            OnPropertyChanged("EditedPerson");
        }
    }
}
