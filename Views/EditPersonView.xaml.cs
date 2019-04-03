using System.Windows.Controls;
using cs_Vozbrannyi_04.Tools.Navigation;
using cs_Vozbrannyi_04.ViewModels;

namespace cs_Vozbrannyi_04.Views
{
    /// <summary>
    /// Interaction logic for EditPersonView.xaml
    /// </summary>
    public partial class EditPersonView : UserControl, INavigatable
    {
        public EditPersonView()
        {
            InitializeComponent();
            DataContext = new EditPersonViewModel();
        }
    }
}
