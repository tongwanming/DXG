#region PanelShowStyle 窗口打开方式
/// <summary>
/// 窗口打开方式
/// </summary>
public enum PanelShowStyle
{
    /// <summary>
    /// 正常打开
    /// </summary>
    Normal,

    /// <summary>
    /// 从中间放大
    /// </summary>
    CenterToBig,

    /// <summary>
    /// 从中间旋转放大
    /// </summary>
    CenterToBigRotate,

    /// <summary>
    /// 透明度渐变
    /// </summary>
    AlphaFade,

    /// <summary>
    /// 从上往下
    /// </summary>
    FromTop,

    /// <summary>
    /// 从下往上
    /// </summary>
    FromDown,

    /// <summary>
    /// 从左向右
    /// </summary>
    FromLeft,

    /// <summary>
    /// 从右向左
    /// </summary>
    FromRight,

    /// <summary>
    /// 从上往下旋转
    /// </summary>
    FromTopRotate,

    /// <summary>
    /// 从下往上旋转
    /// </summary>
    FromDownRotate,

    /// <summary>
    /// 从左向右旋转
    /// </summary>
    FromLeftRotate,

    /// <summary>
    /// 从右向左旋转
    /// </summary>
    FromRightRotate,

    /// <summary>
    /// 自定义个性化
    /// </summary>
    Custom,
}
#endregion

#region TweenStyle 动画方式
/// <summary>
/// 动画方式
/// </summary>
public enum TweenStyle
{
    /// <summary>
    /// 一次
    /// </summary>
    Once,

    /// <summary>
    /// 循环
    /// </summary>
    Loop,

    /// <summary>
    /// 一次来回
    /// </summary>
    Repeatedly,

    /// <summary>
    /// 循环来回
    /// </summary>
    PingPong,
}
#endregion