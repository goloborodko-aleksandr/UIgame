using Engine.AxGridUnityTools;
using Engine.AxGridUnityTools.FSM;


namespace Scritps.States
{
    [State("Init")]
    public class InitState: FSMState
    {
        [Enter]
        void InitEnter()
        {
            Parent.Change("MainScreen");
        }
    }
}