using SpaceConsole.ConsoleApp.Controls;
using SpaceConsole.ConsoleApp.ViewModel;

namespace SpaceConsole.ConsoleApp.UserControls
{
    public sealed class IngameControl : UserControl<IControl>
    {
        public IngameModel ViewModel { get; }

        private ShipOverviewControl ShipOverview { get; }

        private CargoBayControl CargoBay { get; }

        private MarketplaceControl Marketplace { get; }

        private NavigationRoomControl NavigationRoom { get; }

        private EngineControl Engine { get; }

        public IngameControl(IngameModel viewModel)
        {
            ViewModel = viewModel;

            ShipOverview = new ShipOverviewControl(viewModel.Ship);
            CargoBay = new CargoBayControl(viewModel.Ship);
            NavigationRoom = new NavigationRoomControl(ViewModel.NavigationRoom);
            Marketplace = new MarketplaceControl(ViewModel);
            Engine = new EngineControl(viewModel.Ship);

            Box.Padding = 1;
            Content = ShipOverview;
        }

        protected override void DoUpdate()
        {
            SetActiveContent();

            base.DoUpdate();
        }

        private void SetActiveContent()
        {
            Content = GetActiveContent(ViewModel.UserInterfaceRoute);
        }

        private IControl GetActiveContent(UserInterfaceRoute route)
        {
            switch (route)
            {
                case UserInterfaceRoute.ShipOverview:
                    return ShipOverview;
                case UserInterfaceRoute.CargoBay:
                    return CargoBay;
                case UserInterfaceRoute.NavigationRoom:
                    return NavigationRoom;
                case UserInterfaceRoute.Marketplace:
                    return Marketplace;
                case UserInterfaceRoute.Engine:
                    return Engine;
                default:
                    return ShipOverview;
            }
        }
    }
}
