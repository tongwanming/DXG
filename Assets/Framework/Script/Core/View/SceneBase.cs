using UnityEngine;

public class SceneBase : ViewBase
{
    /// <summary>如果缓存为true，面板在发起关闭的时候为隐藏而不是直接删除</summary>
    public bool cache = false;

    protected SceneType _type;

    /// <summary>
    /// 场景ID
    /// </summary>
    public SceneType type
    {
        get { return _type; }
    }

    protected object[] _sceneArgs;

    /// <summary>
    /// 记录场景init时参数
    /// </summary>
    public object[] sceneArgs
    {
        get { return _sceneArgs; }
    }

    protected override void OnInitSkin()
    {
        base.OnInitSkin();
        skin.GetComponent<RectTransform>().sizeDelta = M_Canvas.sizeDelta;
    }

    /// <summary>
    /// 重值数据
    /// </summary>
    /// <param name="sceneArgs"></param>
    public virtual void OnResetArgs(params object[] sceneArgs)
    {
        _sceneArgs = sceneArgs;
    }

    /// <summary>
    /// 初始化场景
    /// </summary>
    /// <param name="sceneArgs">场景参数</param>
    public virtual void OnInit(params object[] sceneArgs)
    {
        _sceneArgs = sceneArgs;
        Init();
    }

    /// <summary>
    /// 开始显示
    /// </summary>
    public virtual void OnShowing()
    {
    }

    /// <summary>
    /// 显示后
    /// </summary>
    public virtual void OnShowed()
    {
    }


    /// <summary>
    /// 开始隐藏
    /// </summary>
    public virtual void OnHiding()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 隐藏后
    /// </summary>
    public virtual void OnHided()
    {
    }
}

/// <summary>
/// 场景定义
/// 有一个特殊要求，SceneType.tostring等于该场景的classname。原因是：在切换场景时，参数为scenetype,此时需要根据string反射得到该className对应的对象。
/// </summary>
public enum SceneType
{
    None,
    MainPanel,
    SplashPanel,
}