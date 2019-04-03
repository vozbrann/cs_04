using System;
using cs_Vozbrannyi_04.Views;

namespace cs_Vozbrannyi_04.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {

        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.DataView:
                    ViewsDictionary.Add(viewType, new DataView());
                    break;
                case ViewType.AddPersonView:
                    ViewsDictionary.Add(viewType, new AddPersonView());
                    break;
                case ViewType.EditPersonView:
                    ViewsDictionary.Add(viewType, new EditPersonView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}