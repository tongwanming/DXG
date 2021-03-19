using DG. Tweening;
using UnityEngine;

/// <summary>
/// 动画基类
/// </summary>
public abstract class UITweener : MonoBehaviour
{
    /// <summary>
    /// 重写编辑器界面状态
    /// </summary>
    [HideInInspector]
    public bool isEditorDisabled = false;

    /// <summary>
    /// 动画方式
    /// </summary>
    public TweenStyle style = TweenStyle. Once;

    /// <summary>
    /// 动画曲线
    /// </summary>
    public Ease ease = Ease. OutQuad;

    /// <summary>
    /// 延迟时间
    /// </summary>
    public float delay = 0f;

    /// <summary>
    /// 持续时间
    /// </summary>
    public float duration = 1f;

    /// <summary>
    /// 是否开始运行
    /// </summary>
    public bool IsStartRun = false;

    public delegate void TweenCompleteHandle ();
    public TweenCompleteHandle onFinished;

    public delegate void TweenDelayFinishedHandle ();
    public TweenDelayFinishedHandle onDelayFinished;

    public void Reset ()
    {
        StartValue();
        EndValue();
    }

    private void Awake ()
    {
        if (onFinished == null)
        {
            onFinished = TweenAnim;
        }

        OnAwake();
    }

    private void Start ()
    {
        if (IsStartRun)
        {
            PlayForward();
        }
    }

    protected void TweenAnim () { }
    public virtual void OnAwake () { }
    public virtual void OnStart () { }

    public virtual void Play (bool IsForward)
    {
        DOTween. CompleteAll();
        if (IsForward)
        {
            Invoke("PlayForwardDelay", delay);
        }
        else
        {
            Invoke("PlayReverseDelay", delay);
        }
    }

    public virtual void PlayForward () { DOTween. CompleteAll(); Invoke("PlayForwardDelay", delay); }
    public virtual void PlayForwardDelay () { }
    public virtual void PlayReverse () { DOTween. CompleteAll(); Invoke("PlayReverseDelay", delay); }
    public virtual void PlayReverseDelay () { }
    protected virtual void StartValue () { }
    protected virtual void EndValue () { }
    public virtual void SetAlpha (float startalpha, float alpha, float animTime, TweenStyle style) { }
}