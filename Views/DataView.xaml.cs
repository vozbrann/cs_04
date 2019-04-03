using cs_Vozbrannyi_04.Tools.Managers;
using cs_Vozbrannyi_04.Tools.Navigation;
using cs_Vozbrannyi_04.ViewModels;
using UserControl = System.Windows.Controls.UserControl;

namespace cs_Vozbrannyi_04.Views
{
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class DataView : UserControl, INavigatable
    {

        public DataView()
        {
            InitializeComponent();
            DataContext = new DataViewModel();
            StationManager.PersonTable = DGname;
        }

    }
}
