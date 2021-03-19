using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeUtils 
{
    // DateTime --> long
    public static long DateTimeToLong (this DateTime dt)
    {
        DateTime dtStart = TimeZone. CurrentTimeZone. ToLocalTime(new DateTime(1970, 1, 1));
        TimeSpan toNow = dt. Subtract(dtStart);
        long timeStamp = toNow. Ticks;
        timeStamp = long. Parse(timeStamp. ToString(). Substring(0, timeStamp. ToString(). Length - 4));
        return timeStamp;
    }


    // long --> DateTime
    public static DateTime LongToDateTime (this long d)
    {
        DateTime dtStart = TimeZone. CurrentTimeZone. ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long. Parse(d + "0000");
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime dtResult = dtStart. Add(toNow);
        return dtResult;
    }

    /// <summary>
    /// 获取当前本地时间戳
    /// </summary>
    /// <returns></returns>      
    public static long GetCurrentTimeUnix()
    {
        TimeSpan cha = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long t = (long)cha.TotalMilliseconds;
        return t;
    }

    public static long GetCurrentTimeSeconds()
    {
        TimeSpan cha = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long t = (long)cha.TotalSeconds;
        return t;
    }

    /// <summary>
    /// 时间戳转换为本地时间对象
    /// </summary>
    /// <returns></returns>      
    public static DateTime GetUnixDateTime(long unix)
    {
        //long unix = 1500863191;
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        DateTime newTime = dtStart.AddMilliseconds(unix);
        return newTime;
    }

    public static string SecondFormatHhMmSs2(long second)
    {
        string str = "";
        long hour = second / 3600;
        long min = second % 3600 / 60;
        long sec = second % 60;
        if (hour < 10)
        {
            str += "0" + hour.ToString();
        }
        else
        {
            str += hour.ToString();
        }
        str += "h ";
        if (min < 10)
        {
            str += "0" + min.ToString();
        }
        else
        {
            str += min.ToString();
        }
        str += "m ";
        if (sec < 10)
        {
            str += "0" + sec.ToString();
        }
        else
        {
            str += sec.ToString();
        }
        str += "s";
        return str;
    }

    public static string SecondFormatDdHhMm(long second)
    {
        string str = "";
        long day = second / 86400;
        long hour = second % 86400 / 3600;
        long min = second % 86400 % 3600 / 60;
        if (day < 10)
        {
            str += "0" + day.ToString();
        }
        else
        {
            str += day.ToString();
        }
        str += ":";
        if (hour < 10)
        {
            str += "0" + hour.ToString();
        }
        else
        {
            str += hour.ToString();
        }
        str += ":";
        if (min < 10)
        {
            str += "0" + min.ToString();
        }
        else
        {
            str += min.ToString();
        }
        return str;
    }

    public static string SecondFormatHhMmSs(long second)
    {
        string str = "";
        long hour = second / 3600;
        long min = second % 3600 / 60;
        long sec = second % 60;
        if (hour < 10)
        {
            str += "0" + hour.ToString();
        }
        else
        {
            str += hour.ToString();
        }
        str += ":";
        if (min < 10)
        {
            str += "0" + min.ToString();
        }
        else
        {
            str += min.ToString();
        }
        str += ":";
        if (sec < 10)
        {
            str += "0" + sec.ToString();
        }
        else
        {
            str += sec.ToString();
        }
        return str;
    }

    public static string SecondFormatMmSs(long second)
    {
        string str = "";
        long min = second / 60;
        long sec = second % 60;
        if (min < 10)
        {
            str += "0" + min.ToString();
        }
        else
        {
            str += min.ToString();
        }
        str += ":";
        if (sec < 10)
        {
            str += "0" + sec.ToString();
        }
        else
        {
            str += sec.ToString();
        }
        return str;
    }

    public static string SecondFormatDdHhMmSs(long second)
    {
        string str = "";
        long day = second / 86400;
        long hour = second % 86400 / 3600;
        long min = second % 86400 % 3600 / 60;
        long sec = second % 60;
        if (day < 10)
        {
            str += "0" + day.ToString();
        }
        else
        {
            str += day.ToString();
        }
        str += "d ";
        if (hour < 10)
        {
            str += "0" + hour.ToString();
        }
        else
        {
            str += hour.ToString();
        }
        str += "h ";
        if (min < 10)
        {
            str += "0" + min.ToString();
        }
        else
        {
            str += min.ToString();
        }
        str += "m ";
        if (sec < 10)
        {
            str += "0" + sec.ToString();
        }
        else
        {
            str += sec.ToString();
        }
        str += "s";
        return str;
    }

    public static long GetDayRemainingTime()
    {
        //获取当前时间
        DateTime DateTime1 = DateTime.Now;
        //第二天的0点00分00秒
        DateTime DateTime2 = DateTime.Now.AddDays(1).Date;
        //把2个时间转成TimeSpan,方便计算
        TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
        TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
        //时间比较，得出差值
        TimeSpan ts = ts1.Subtract(ts2).Duration();
        //结果
        return (long)ts.TotalSeconds;
    }
    
        /// <summary>
    /// 获取当前DateTime时间
    /// </summary>
    /// <returns></returns>
    public static DateTime GetNowDateTime()
    {
        return DateTime.Now;
    }

    /// <summary>
    /// 获取当前Unix时间(秒)
    /// </summary>
    /// <returns></returns>
    public static int GetNowUnixTime()
    {
        return DateTime2Unix(GetNowDateTime());
    }

    /// <summary>
    /// 检测DateTime是否是当日
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static bool isToday(DateTime dt)
    {
        if (GetDateFirst(dt).Equals(GetNowDateTime()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 检测Unix时间是否是当日(秒)
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <returns></returns>
    public static bool isToday(string timeStamp)
    {
        DateTime dt = Unix2DateTime(timeStamp);
        if (GetDateFirst(dt).Equals(GetDateFirst(GetNowDateTime())))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>  
    /// 时间戳转为C#格式时间  
    /// </summary>  
    /// <param name="timeStamp">Unix时间戳格式(秒)</param>  
    /// <returns>C#格式时间(DateTime)</returns>  
    public static DateTime Unix2DateTime(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }

    /// <summary>  
    /// DateTime时间格式转换为Unix时间戳格式(秒)
    /// </summary>  
    /// <param name="time"> DateTime时间格式</param>  
    /// <returns>Unix时间戳格式(秒)</returns>  
    public static int DateTime2Unix(System.DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        return (int)(time - startTime).TotalSeconds;
    }

    /// <summary>
    /// 获取当前日期的星期几(星期天为7)
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentDayOfWeek()
    {
        int dayOfWeek = Convert.ToInt32(DateTime.Now.DayOfWeek) < 1 ? 7 : Convert.ToInt32(DateTime.Now.DayOfWeek);
        return dayOfWeek;
    }

    /// <summary>
    /// 获取本周一
    /// </summary>
    /// <returns></returns>
    public static DateTime GetCurrentMonday()
    {
        DateTime monday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1 - GetCurrentDayOfWeek());
        return monday;
    }

    /// <summary>
    /// 获取本周日(星期天)
    /// </summary>
    /// <returns></returns>
    public static DateTime GetCurrentSunday()
    {
        DateTime sunday = GetCurrentMonday().AddDays(6);
        return sunday;
    }


    /// <summary>
    /// 获取DateTime的日期部分：2005-06-07
    /// </summary>
    /// <param name="T"></param>
    /// <returns></returns>
    public static string GetDateFirst(DateTime T,int a=0)
    {
        if (a == 1)
        return T.Year + "." + TwoChar(T.Month) + "." + TwoChar(T.Day);

        return T.Year + "-" + TwoChar(T.Month) + "-" + TwoChar(T.Day);
    }


    /// <summary>
    /// 获取DateTime的时间部分：12:23:34
    /// </summary>
    /// <param name="T"></param>
    /// <returns></returns>
    public static string GetDateLast(DateTime T)
    {
        return TwoChar(T.Hour) + ":" + TwoChar(T.Minute) + ":" + TwoChar(T.Second);

    }

    /// <summary>
    /// 获取两个日期之间的差值
    /// </summary>
    /// <param name="howtocompare">比较的方式可为：year month day hour minute second</param>
    /// <param name="startDate">开始日期</param>
    /// <param name="endDate">结束日期</param>
    /// <returns>时间差</returns>
    public static double GetDateDiff(string howtocompare, DateTime startDate, DateTime endDate)
    {
        double diff = 0;
        try
        {
            TimeSpan TS = new TimeSpan(endDate.Ticks - startDate.Ticks);

            switch (howtocompare.ToLower())
            {
                case "year":
                    diff = Convert.ToDouble(TS.TotalDays / 365);
                    break;
                case "month":
                    diff = Convert.ToDouble((TS.TotalDays / 365) * 12);
                    break;
                case "day":
                    diff = Convert.ToDouble(TS.TotalDays);
                    break;
                case "hour":
                    diff = Convert.ToDouble(TS.TotalHours);
                    break;
                case "minute":
                    diff = Convert.ToDouble(TS.TotalMinutes);
                    break;
                case "second":
                    diff = Convert.ToDouble(TS.TotalSeconds);
                    break;
            }
        }
        catch (Exception)
        {
            diff = 0;
        }
        return diff;
    }
    /// <summary>
    /// 比较时间
    /// </summary>
    /// <param name="date1">小的时间</param>
    /// <param name="date2">大的时间</param>
    /// <returns></returns>
    public static TimeSpan ComparisonTime(DateTime date1,DateTime date2)
    {
        TimeSpan inter=new TimeSpan();
        try
        {
             inter = date1 - date2;
        }
        catch (Exception)
        { 
        }
        return inter;
    }

    /// <summary>
    /// 将n转化为2位符号数， 0-99 转化为00-99
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    private static string TwoChar(int n)
    {
        return (n < 10 ? "0" : "") + n;
    }
}
