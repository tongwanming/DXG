using System;
using UnityEngine;

namespace Common.Utils
{
    public class UnityUtils : Singleton<UnityUtils>
    {
        private const int MIN_CLICK_DELAY_TIME = 400;
        private long mLastClickTime;
        
        public bool IsFastClick()
        {
            return IsFastClick(MIN_CLICK_DELAY_TIME);
        }
        
        /// <summary>
        /// 判断是否是快速点击
        /// </summary>
        /// <returns><c>true</c>, if fast click was ised, <c>false</c> otherwise.</returns>
        /// <param name="interval">Interval.</param>
        public bool IsFastClick(long interval)
        {
            long time = TimeUtils.GetCurrentTimeUnix();
            if (mLastClickTime < time && time - mLastClickTime < interval)
            {
                return true;
            }
            mLastClickTime = time;
            return false;
        }
        
        /// <summary>
        /// 移动物体到某节点
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="parent"></param>
        public void Transform2Parent(Transform tran, Transform parent)
        {
            tran.SetParent(parent);
            tran.localPosition = Vector3.zero;
            tran.localRotation = Quaternion.identity;
            tran.localScale = Vector3.one;
        }
        
        /// <summary>
        /// color 转换hex
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorToHex(Color color)
        {
            int r = Mathf.RoundToInt(color.r * 255.0f);
            int g = Mathf.RoundToInt(color.g * 255.0f);
            int b = Mathf.RoundToInt(color.b * 255.0f);
            int a = Mathf.RoundToInt(color.a * 255.0f);
            string hex = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
            return hex;
        }


        /// <summary>
        /// hex转换到color
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Color HexToColor(string hex)
        {
            byte br = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte bg = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte bb = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte cc = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            float r = br / 255f;
            float g = bg / 255f;
            float b = bb / 255f;
            float a = cc / 255f;
            return new Color(r, g, b, a);
        }
    }
}