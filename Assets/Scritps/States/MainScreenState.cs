using Engine.AxGridUnityTools;
using Engine.AxGridUnityTools.FSM;
using Engine.AxGridUnityTools.Model;

namespace Scritps.States
{
    [State("MainScreen")]
    public class MainScreenState: FSMState
    {

        [Bind]
        public virtual void OnBtn(string name)
        {
            switch (name)
            {
                case "Play":
                {
                    Log.Debug("Play");
                    Parent.Change("SwipeGame");
                        break;
                }
                case "MiniGame":
                {
                    Log.Debug("MiniGame");
                    break;
                }
                case "Training":
                {
                    Log.Debug("Training");
                    break;
                }
                case "Settings":
                {
                    Log.Debug("Settings");
                    break;
                }
            }
        }
    }
}