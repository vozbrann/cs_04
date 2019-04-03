namespace cs_Vozbrannyi_04.Tools.Navigation
{
    internal enum ViewType
    {
        DataView,
        AddPersonView,
        EditPersonView
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}