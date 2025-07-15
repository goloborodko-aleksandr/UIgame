using System;
using System.Collections.Generic;
using Engine.AxGridUnityTools;
using Engine.AxGridUnityTools.Base;
using Engine.AxGridUnityTools.FSM;
using Scritps.States;
using UnityEngine;

namespace Scritps.Main
{
    public class Starter : MonoBehaviourExt
    {
        protected virtual FSM CreateFSM()
        {
            var fsm = new FSM();
            fsm.Add(new InitState());
            return fsm;
        }
    
        protected virtual BaseModel CreateModel()
        {
            return new BaseModel(new Dictionary<string, object>()
            {
                
            });
        }
    
    
        [OnAwake(RunLevel.High)]
        protected virtual void __init()
        {
            Log.Info("Create Model and FSM...");
            Settings.Model = CreateModel();
            Settings.Fsm = CreateFSM();
            FSM.ShowFsmEnterState = true;
            FSM.ShowFsmExitState = true;
        }
        
        [OnUpdate]
        protected virtual void __update()
        {
            Settings.Model?.EventManager.Update(Time.deltaTime);
            Settings.Fsm?.Update(Time.deltaTime);
        }
        
        [OnAwake(RunLevel.Medium)]
        protected virtual void __start()
        {
            Log.Info("Starting fsm...");
            try
            {
                Settings.Fsm.Start("Init");
            }
            catch (Exception e)
            {
                Log.Error($"Error starting fsm {e.Message}\n{e.StackTrace}");
            }
        }
    
        [OnDestroy]
        protected virtual void __destroy()
        {
            Settings.Fsm = null;
        }
    }
}
