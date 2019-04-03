using System.Windows.Controls;
using cs_Vozbrannyi_04.Tools.Navigation;
using cs_Vozbrannyi_04.ViewModels;

namespace cs_Vozbrannyi_04.Views
{
    public partial class AddPersonView : UserControl, INavigatable
    {
        public AddPersonView()
        {
            InitializeComponent();
            DataContext = new AddPersonViewModel();
        }
    }
}
