using DG.Tweening;
using Engine.AxGridUnityTools.Base;
using Engine.AxGridUnityTools.Path;
using UnityEngine;
using UnityEngine.UI;


public class AnimPoint : MonoBehaviourExt
{
    [SerializeField] private Image _img;
    [SerializeField] private RectTransform _rectTransform;
    public bool IsAnimating{get; private set;}
    
    
    public void Show()
    {
        var start = _rectTransform.anchoredPosition;
        IsAnimating = true;
        gameObject.SetActive(true);
        _img.color = Color.white;
        Sequence seq = DOTween.Sequence();
        Tween scale = _rectTransform.DOScale(2f, 0.3f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
        Tween up = _rectTransform.DOAnchorPosY(100f, 1f).SetEase(Ease.Linear).OnComplete(()=>_rectTransform.anchoredPosition = start);
        Tween fade = _img.DOFade(0, 1f).SetEase(Ease.Linear);
        seq.Append(scale);
        seq.Append(up);
        seq.Join(fade);
        seq.Play().OnComplete(() =>
        {
            gameObject.SetActive(false);
            IsAnimating = false;
        });
    }
}
