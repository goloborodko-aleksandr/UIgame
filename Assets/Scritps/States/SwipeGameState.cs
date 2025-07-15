using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Engine.AxGridUnityTools;
using Engine.AxGridUnityTools.FSM;
using Engine.AxGridUnityTools.Model;

namespace Scritps.States
{
    [State("SwipeGame")]
    public class SwipeGameState:FSMState
    {
        private Queue<string> _levelSwipe;
        private string _awaite;
        
        [Enter]
        void OnEnter()
        {
            _levelSwipe = new Queue<string>(Model.GetArrayOfString($"Level_{Model.Get("LevelIndex")}"));
            Log.Debug(_levelSwipe.Aggregate((a, b) => $"{a}, {b}"));
            Model.Set("SwipeGame", true);
            Model.Set("MainScreen", false);
            _ = AwaiteForStep();
        }
        
        void DoFinger()
        {
            Model.Set("SwipeMode", true);
            _awaite = _levelSwipe.Dequeue();
            Log.Debug(_awaite);
            Model.Set("ShowGame", _awaite);
        }

        [Bind]
        void FingerDone(string typetap)
        {
            Log.Debug(typetap);
            Model.Set("SwipeMode", false);
            if(_awaite == typetap) Invoke("WinPoint");
            else Invoke("LosePoint");
            if(_levelSwipe.Count>0)_ = AwaiteForStep();
            else LevelDone();
        }

        private async UniTaskVoid AwaiteForStep()
        {
            await UniTask.Delay(500);
            DoFinger();
        }
        


        private void LevelDone()
        {
            Log.Debug("Level done");
        }
    }
}