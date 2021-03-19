using System;

public class ModelBase
{
    /// <summary> 属性变更事件定义 </summary>
    public event EventHandler<ValueUpdateEventArgs> ValueUpdateEvent;

    /// <summary>
    /// 属性事件触发
    /// </summary>
    /// <param name="key">事件key</param>
    /// <param name="newValue">新值</param>
    protected void DispatchValueUpdateEvent (string key, object newValue)
    {
        EventHandler<ValueUpdateEventArgs> handler = ValueUpdateEvent;
        if (handler != null)
        {
            handler(this, new ValueUpdateEventArgs(key, newValue));
        }
    }

    /// <summary> 属性事件触发 </summary>
    protected void DispatchValueUpdateEvent (ValueUpdateEventArgs args)
    {
        EventHandler<ValueUpdateEventArgs> handler = ValueUpdateEvent;
        if (handler != null)
        {
            handler(this, args);
        }
    }

}

/// <summary>
/// 数据更新事件
/// </summary>
public class ValueUpdateEventArgs : EventArgs
{
    public string key { get; set; }

    public object newValue { get; set; }

    public ValueUpdateEventArgs (String key, object newValue)
    {
        this. key = key;
        this. newValue = newValue;
    }

    public ValueUpdateEventArgs ()
    {
    }
}
