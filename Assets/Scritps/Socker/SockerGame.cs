using System.Collections.Generic;
using Engine.AxGridUnityTools.Base;
using Engine.AxGridUnityTools.FSM;
using log4net.Core;
using Scritps.Main;
using Scritps.States;

namespace Scritps.Socker
{
    public class SockerGame: Starter
    {
        private Inputs _inputs;
             
        [OnStart]
        void Init()
        {
            _inputs = new Inputs();
        }
        protected override BaseModel CreateModel()
        {
            return new BaseModel(new Dictionary<string, object>()
            {
                {"SwipeMode", false},
                {"LevelIndex", 0},
                {"Levels", new []{0}},
                {"Level_0", new [] { "SwipeUp", "SwipeDown", "DoubleTap" } },
                {"Level_1", new [] { "DoubleTap", "SwipeDown", "LongTap", "LongTap" } },
            });
        }

        protected override FSM CreateFSM()
        {
            var fsm = base.CreateFSM();
            fsm.Add(new MainScreenState());
            fsm.Add(new SwipeGameState());
            return fsm;
        }
    }
}