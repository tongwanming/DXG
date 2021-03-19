using System. Collections;
using System. Collections. Generic;
using System. Text;
using UnityEngine;

public static class IEnumerableUtil
{

    public enum dicType
    {
        key,
        value,
        keyandvalue
    }
    /// <summary>集合转字符串</summary>
    /// <param name="_list">集合或数组</param>
    /// <param name="_separator">分隔符</param>
    /// <returns></returns>
    public static string ListToString (this IEnumerable _list, string _separator, string _prefix = "", string _suffix = "")
    {
        StringBuilder value = new StringBuilder();
        if (_list == null)
        {
            return value. ToString();
        }

        foreach (object item in _list)
        {
            if (item is string)
            {
                string str = ((string)item). TrimEnding();
                value. Append(_prefix + str + _suffix + _separator);
            }
            else if (item is int)
            {
                string str = (((int)item). ToString()). TrimEnding();
                value. Append(_prefix + str + _suffix + _separator);
            }
            else if (item is Object)
            {
                string str = (((GameObject)item). name). TrimEnding();
                value. Append(_prefix + str + _suffix + _separator);
            }
            else
            {
                Debug. Log("item的类型=" + item. GetType());
                return null;
            }
        }
        return value. ToString() == "" ? "" : value. ToString(). Substring(0, value. ToString(). LastIndexOf(_separator));
    }

    public static string DicToString (this IEnumerable _list, string _separator, int _type, string _prefix = "", string _suffix = "")
    {
        StringBuilder value = new StringBuilder();
        if (_list == null)
        {
            return value. ToString();
        }

        string _str = "";
        foreach (object item in _list)
        {
            if (item is KeyValuePair<string, string>)
            {
                KeyValuePair<string, string> _item = (KeyValuePair<string, string>)item;
                switch ((dicType)_type)
                {
                    case dicType. key: _str = _prefix + _item. Key. TrimEnding() + _suffix; break;
                    case dicType. value: _str = _prefix + _item. Value. TrimEnding() + _suffix; break;
                    case dicType. keyandvalue: _str = _item. Key. TrimEnding() + " = " + _prefix + _item. Value. TrimEnding() + _suffix; break;
                }
                value. Append(_str + _separator);
            }
            else if (item is KeyValuePair<string, int>)
            {
                KeyValuePair<string, int> _item = (KeyValuePair<string, int>)item;
                switch ((dicType)_type)
                {
                    case dicType. key: _str = _prefix + _item. Key. TrimEnding() + _suffix; break;
                    case dicType. value: _str = _prefix + _item. Value. ToString(). TrimEnding() + _suffix; break;
                    case dicType. keyandvalue: _str = _item. Key. TrimEnding() + " = " + _prefix + _item. Value. ToString(). TrimEnding() + _suffix; break;
                }
                value. Append(_str + _separator);
            }
            else if (item is KeyValuePair<string, Object>)
            {
                KeyValuePair<string, Object> _item = (KeyValuePair<string, Object>)item;
                switch ((dicType)_type)
                {
                    case dicType. key: _str = _prefix + _item. Key. TrimEnding() + _suffix; break;
                    case dicType. value: _str = _prefix + _item. Value. name. TrimEnding() + _suffix; break;
                    case dicType. keyandvalue: _str = _item. Key. TrimEnding() + " = " + _prefix + _item. Value. name. TrimEnding() + _suffix; break;
                }
                value. Append(_str + _separator);
            }
            else
            {
                Debug. Log("item.value的类型不存在");
                return null;
            }
        }
        return value. ToString() == "" ? "" : value. ToString(). Substring(0, value. ToString(). LastIndexOf(_separator));
    }


    public static string TrimEnding (this string v)
    {
        if (v == "")
        {
            return "";
        }

        char [] buff = v. ToCharArray();
        if (buff [ buff. Length - 1 ] == '\0')
        {
            buff [ buff. Length - 1 ] = ' ';
        }

        return new string(buff). Trim();
    }
}