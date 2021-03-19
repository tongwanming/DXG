using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorUtil 
{
    public static Color getHaxColor(this string hax){
        Color nowColor;
        ColorUtility. TryParseHtmlString(hax, out nowColor);
        return nowColor;
    } 
}
