using System.Collections.Generic;

/**
 * 金钱改变需要通知的方法
 */
public class MoneyManager
{
    private static List<OnMoneyChangeListener> mList = new List<OnMoneyChangeListener>();


    public static void addListener(OnMoneyChangeListener listener)
    {
        if (!mList.Contains(listener))
        {
            mList.Add(listener);
        }
    }

    public static void removeListener(OnMoneyChangeListener listener)
    {
        if (mList.Contains(listener))
        {
            mList.Remove(listener);
        }
    }

    public static void sendChange(float value)
    {
        foreach (OnMoneyChangeListener listener in mList)
        {
            listener.onMoneyChange(value);
        }
    }
}