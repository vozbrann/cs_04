using System.Windows;
using System.Windows.Controls;
using cs_Vozbrannyi_04.Tools.DataStorage;
using cs_Vozbrannyi_04.Tools.Managers;
using cs_Vozbrannyi_04.Tools.Navigation;
using cs_Vozbrannyi_04.ViewModels;

namespace cs_Vozbrannyi_04.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContentOwner
    {
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            StationManager.Initialize(new SerializedDataStorage());
            NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.DataView);

        }
    }
}
