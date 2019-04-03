using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using cs_Vozbrannyi_04.Models;
using cs_Vozbrannyi_04.Tools;
using cs_Vozbrannyi_04.Tools.Managers;
using cs_Vozbrannyi_04.Tools.Navigation;

namespace cs_Vozbrannyi_04.ViewModels
{
    internal class DataViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public DataViewModel()
        {
            StationManager.DataVM = this;
        }

        #region Fields

        private List<Person> _personList = StationManager.DataStorage.PersonsList;
        private string[] _sortByList = {"Name", "Surname", "Email", "Birthday", "SunSign", "ChineseSign"};
        private string[] _filterByList = {"Name", "Surname", "Email", "SunSign", "ChineseSign"};

        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _removePersonCommand;
        private RelayCommand<object> _filterCommand;

        private int _sortByIndex = 0;
        private int _filterByIndex = 0;

        #endregion

        #region Properties

        public string FilterQuery { get; set; }

        public int SelectedSortByIndex
        {
            get { return _sortByIndex; }
            set
            {
                _sortByIndex = value;
                OnPropertyChanged("PersonList");
            }
        }

        public int SelectedFilterByIndex
        {
            get { return _filterByIndex; }
            set
            {
                _filterByIndex = value;
                OnPropertyChanged("PersonList");
            }
        }


        public object SelectedPerson { get; set; }

        public IEnumerable<Person> PersonList
        {
            get
            {
                IEnumerable<Person> l = _personList;

                switch (SelectedSortByIndex)
                {
                    case 0:
                        l = l.OrderBy(x => x.Name);
                        break;
                    case 1:
                        l = l.OrderBy(x => x.Surname);
                        break;
                    case 2:
                        l = l.OrderBy(x => x.Email);
                        break;
                    case 3:
                        l = l.OrderBy(x => x.Birthday);
                        break;
                    case 4:
                        l = l.OrderBy(x => x.SunSign);
                        break;
                    case 5:
                        l = l.OrderBy(x => x.ChineseSign);
                        break;
                }

                if (String.IsNullOrWhiteSpace(FilterQuery))
                    return l;

                switch (SelectedFilterByIndex)
                {
                    case 0:
                        l = l.Where(x => x.Name.Contains(FilterQuery));
                        break;
                    case 1:
                        l = l.Where(x => x.Surname.Contains(FilterQuery));
                        break;
                    case 2:
                        l = l.Where(x => x.Email.Contains(FilterQuery));
                        break;
                    case 3:
                        l = l.Where(x => x.SunSign.Contains(FilterQuery));
                        break;
                    case 4:

                        l = l.Where(x => x.ChineseSign.Contains(FilterQuery));

                        break;
                }

                return l;
            }
        }

        public IEnumerable<string> SortByList
        {
            get { return _sortByList; }
        }

        public IEnumerable<string> FilterByList
        {
            get { return _filterByList; }
        }

        public RelayCommand<object> AddPersonCommand
        {
            get
            {
                return _addPersonCommand ?? (_addPersonCommand = new RelayCommand<object>(
                           AddPersonImplementation));
            }
        }

        public RelayCommand<object> EditPersonCommand
        {
            get
            {
                return _editPersonCommand ?? (_editPersonCommand =
                           new RelayCommand<object>(EditPersonImplementation, CanExecuteRemoveOrEdit));
            }
        }

        public RelayCommand<object> RemovePersonCommand
        {
            get
            {
                return _removePersonCommand ?? (_removePersonCommand =
                           new RelayCommand<object>(RemovePersonImplementation, CanExecuteRemoveOrEdit));
            }
        }

        public RelayCommand<object> FilterCommand
        {
            get
            {
                return _filterCommand ?? (_filterCommand = new RelayCommand<object>(
                           (o => { OnPropertyChanged("PersonList"); })));
            }
        }

        #endregion

        private void AddPersonImplementation(object obj)
        {
            StationManager.CurrentPerson = new Person("", "", "");
            NavigationManager.Instance.Navigate(ViewType.AddPersonView);
        }

        private async void RemovePersonImplementation(object obj)
        {
            LoaderManeger.Instance.ShowLoader();
           
            await Task.Run(() =>
            {
                Person personToRemove = (Person) SelectedPerson;

                DialogResult dr = MessageBox.Show(
                    "Are you sure to remove " + personToRemove.Name + " " + personToRemove.Surname + "?",
                    "Removing Person",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                    StationManager.DataStorage.RemovePerson(personToRemove);
                    OnPropertyChanged("PersonList");
                }
            });

            LoaderManeger.Instance.HideLoader();
        }

        private async void EditPersonImplementation(object obj)
        {
            LoaderManeger.Instance.ShowLoader();

            await Task.Run(() =>
            {
                StationManager.CurrentPerson = (Person) SelectedPerson;

                StationManager.TestPerson = new Person(
                    StationManager.CurrentPerson.Name,
                    StationManager.CurrentPerson.Surname,
                    StationManager.CurrentPerson.Email,
                    StationManager.CurrentPerson.Birthday
                );
            });
            LoaderManeger.Instance.HideLoader();
            if (StationManager.EditVM != null)
                StationManager.EditVM.updateAll();

            NavigationManager.Instance.Navigate(ViewType.EditPersonView);
        }

        private bool CanExecuteRemoveOrEdit(object obj)
        {
            return SelectedPerson != null;
        }

        public void UpdateInfo()
        {
            OnPropertyChanged("PersonList");
        }
    }
}