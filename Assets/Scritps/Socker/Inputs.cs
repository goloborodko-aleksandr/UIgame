using System;
using System.Linq;
using Engine.AxGridUnityTools;
using R3;
using UnityEngine;


namespace Scritps.Socker
{
    public class Inputs : IDisposable
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public Inputs()
        {
            bool swipeDetected = false;

            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Select(_ => Input.mousePosition)
                .Subscribe(start =>
                {
                    Observable.EveryUpdate()
                        .Where(_ => Input.GetMouseButtonUp(0))
                        .Select(_ => Input.mousePosition)
                        .Take(1)
                        .Subscribe(end =>
                        {
                            if(!Settings.Model.GetBool("SwipeMode")) return;
                            var delta = end - start;
                            if (delta.magnitude > 50f)
                            {
                                swipeDetected = true;
                                if (Math.Abs(delta.y) > Math.Abs(delta.x))
                                {
                                    if(delta.y > 0) Settings.Invoke("FingerDone", "SwipeUp");
                                    else Settings.Invoke("FingerDone", "SwipeDown");
                                }
                                swipeDetected = false;
                            }
                        })
                        .AddTo(_disposables);
                })
                .AddTo(_disposables);


            
            Observable
                .EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ =>
                {
                    Observable
                        .Timer(TimeSpan.FromSeconds(2))
                        .TakeUntil(Observable
                            .EveryUpdate()
                            .Where(_ => Input.GetMouseButtonUp(0)))
                        .Subscribe(_ =>
                        {
                            if(!Settings.Model.GetBool("SwipeMode")) return;
                            Settings.Invoke("FingerDone", "LongTap");
                        })
                        .AddTo(_disposables);
                })
                .AddTo(_disposables);
                


            
            Observable
                .EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .TimeInterval()
                .Select(t => t.Interval.TotalMilliseconds)
                .Chunk(2, 1)
                .Where(list => list[0] > 250d) 
                .Where(list => list[1] <= 250d) 
                .Subscribe(_ =>
                {
                    if(swipeDetected) return;
                    if(!Settings.Model.GetBool("SwipeMode")) return;
                    Settings.Invoke("FingerDone", "DoubleTap");
                })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}

