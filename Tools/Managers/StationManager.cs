using System;
using System.Windows;
using System.Windows.Controls;
using cs_Vozbrannyi_04.Models;
using cs_Vozbrannyi_04.Tools.DataStorage;
using cs_Vozbrannyi_04.ViewModels;

namespace cs_Vozbrannyi_04.Tools.Managers
{
    internal static class StationManager
    {
        internal static Person CurrentPerson { get; set; }
        internal static Person TestPerson { get; set; }
        internal static DataGrid PersonTable { get; set; }

        internal static EditPersonViewModel EditVM { get; set; }
        internal static DataViewModel DataVM { get; set; }

        private static IDataStorage _dataStorage;

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");
            Environment.Exit(1);
        }

    }
}
