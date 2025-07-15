using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Engine.AxGridUnityTools.Base;
using Engine.AxGridUnityTools.Model;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimPool : MonoBehaviourExtBind
{
    [SerializeField] private AnimPoint winRef;
    [SerializeField] private AnimPoint loseRef;
    [SerializeField]private List<AnimPoint> _winPool;
    [SerializeField]private List<AnimPoint> _losePool;

    [Bind]
    private void WinPoint()
    {
        var animPoint = _winPool.FirstOrDefault(i => !i.IsAnimating);
        if(animPoint == null) animPoint = Instantiate(winRef, winRef.transform.position, Quaternion.identity, winRef.transform.parent);
        _winPool.Add(animPoint);
        animPoint.Show();
    }
    
    [Bind]
    private void LosePoint()
    {
        var animPoint = _losePool.FirstOrDefault(i => !i.isActiveAndEnabled);
        if(animPoint == null) animPoint = Instantiate(loseRef, loseRef.transform.position, Quaternion.identity, loseRef.transform.parent);
        _losePool.Add(animPoint);
        animPoint.Show();
    }
}
