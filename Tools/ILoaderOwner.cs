using System.ComponentModel;
using System.Windows;

namespace cs_Vozbrannyi_04.Tools
{
    internal interface ILoaderOwner : INotifyPropertyChanged
    {
        Visibility LoaderVisibility { get; set; }
        bool IsControlEnabled { get; set; }
    }
}