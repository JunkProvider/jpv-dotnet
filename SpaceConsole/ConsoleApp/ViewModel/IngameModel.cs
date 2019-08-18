using SpaceConsole.ConsoleApp.Model;
using SpaceConsole.ConsoleApp.UserControls;

namespace SpaceConsole.ConsoleApp.ViewModel
{
    public sealed class IngameModel
    {
        public World World { get; }

        public Ship Ship => World.Ship;

        public NavigationRoom NavigationRoom { get; } = new NavigationRoom();


        public UserInterfaceRoute UserInterfaceRoute { get; set; }

        public string UserFeedback { get; private set; }

        public string UserInput { get; set; } = string.Empty;

        public bool IsUserTypingCommand => UserInput.StartsWith("#");


        public IngameModel(World world)
        {
            World = world;
        }


        public void Update()
        {
            NavigationRoom.Update(World.Stations, Ship);
        }

        public void SetMessage(string message)
        {
            UserFeedback = message;
        }

        public void ClearMessage()
        {
            UserFeedback = string.Empty;
        }
    }
}
