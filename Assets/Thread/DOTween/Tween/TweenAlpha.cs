using DG. Tweening;
using UnityEngine;

/// <summary>
/// 动画之透明度类
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class TweenAlpha : UITweener
{
    /// <summary>
    /// 起始透明度
    /// </summary>
    [Range(0, 1)]
    public float from;

    /// <summary>
    /// 结束透明度
    /// </summary>
    [Range(0, 1)]
    public float to;
    private GameObject cacheGameObject;

    /// <summary>
    /// 缓存gameObject
    /// </summary>
    private GameObject CacheGameObject
    {
        get
        {
            if (cacheGameObject == null)
            {
                cacheGameObject = gameObject;
            }

            return cacheGameObject;
        }
    }

    private CanvasGroup ugui;

    /// <summary>
    /// 画布组，用来控制UI组的透明度
    /// </summary>
    private CanvasGroup UGUI
    {
        get
        {
            if (ugui == null)
            {
                ugui = gameObject. GetComponent<CanvasGroup>();
            }

            return ugui;
        }
    }

    /// <summary>
    /// 获取当前透明度
    /// </summary>
    public float GetAlpha
    {
        get
        {
            return UGUI. alpha;
        }
    }

    /// <summary>
    /// 顺序播放动画
    /// </summary>
    public override void PlayForward ()
    {
        base. PlayForward();
    }

    /// <summary>
    /// 顺序播放动画 延迟
    /// </summary>
    public override void PlayForwardDelay ()
    {
        CacheGameObject. SetActive(true);
        Play(from, to);
    }

    /// <summary>
    /// 倒序播放动画
    /// </summary>
    public override void PlayReverse ()
    {
        base. PlayReverse();
    }

    /// <summary>
    /// 倒序播放动画 延迟
    /// </summary>
    public override void PlayReverseDelay ()
    {
        CacheGameObject. SetActive(true);
        Play(to, from);
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    private void Play (float from, float to)
    {
        switch (style)
        {
            case TweenStyle. Once:
                Once(from, to);
                break;
            case TweenStyle. Loop:
                Loop(from, to);
                break;
            case TweenStyle. Repeatedly:
                Repeatedly(from, to);
                break;
            case TweenStyle. PingPong:
                PingPong(from, to);
                break;
        }
    }

    /// <summary>
    /// 一次
    /// </summary>
    private void Once (float from, float to)
    {
        UGUI. alpha = from;
        DOTween. To(() => UGUI. alpha, x => UGUI. alpha = x, to, duration). OnComplete(() => onFinished());
    }

    /// <summary>
    /// 循环
    /// </summary>
    private void Loop (float from, float to)
    {
        UGUI. alpha = from;
        DOTween. To(() => UGUI. alpha, x => UGUI. alpha = x, to, duration). OnComplete(() => Loop(from, to));
    }

    /// <summary>
    /// 一次来回
    /// </summary>
    private void Repeatedly (float from, float to)
    {
        UGUI. alpha = from;
        DOTween. To(() => UGUI. alpha, x => UGUI. alpha = x, to, duration). OnComplete(() => DOTween. To(() => UGUI. alpha, x => UGUI. alpha = x, from, duration));
    }

    /// <summary>
    /// 循环来回
    /// </summary>
    private void PingPong (float from, float to)
    {
        DOTween. To(() => UGUI. alpha, x => UGUI. alpha = x, to, duration). OnComplete(() => PingPong(to, from));
    }

    /// <summary>
    /// 起始值
    /// </summary>
    protected override void StartValue ()
    {
        if (UGUI)
        {
            from = UGUI. alpha;
        }
    }

    /// <summary>
    /// 结束值
    /// </summary>
    protected override void EndValue ()
    {
        if (UGUI)
        {
            to = UGUI. alpha;
        }
    }
}