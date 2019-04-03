using System.Windows.Controls;

namespace cs_Vozbrannyi_04.Tools.Navigation
{
    internal interface IContentOwner
    {
        ContentControl ContentControl { get; }
    }
}