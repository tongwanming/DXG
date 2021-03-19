using System. Collections. Generic;
using UnityEngine;

public class LogicTips : LogicBase
{
    protected override void Awake ()
    {
        mTipsQueue = new List<string>();
    }

    protected override void OnDestroy ()
    {
        mTipsQueue. Clear();
        mTipsQueue = null;
    }
    private List<string> mTipsQueue;

    private GameObject LastTips;

    public void AddTips (string content)
    {
        GameObject tipsObj = ResourceMgr. GetInstance. CreateGameObject("PanelTips", true);
        LayerMgr. GetInstance. SetLayer(tipsObj, LayerType. Tips);
        Vector3 originPos = new Vector3(0, 0, 0);
        if (LastTips != null)
        {
            float uiHigh = LastTips. transform. Find("TipsSprite"). GetComponent<RectTransform>(). sizeDelta. y;
            if (LastTips. transform. localPosition. y < uiHigh)
            {
                originPos = LastTips. transform. localPosition - new Vector3(0, uiHigh * 1.2f, 0);
                //Debug.Log(originPos + "===" + uiHigh * 2);
                //Debug.Log(tipsObj.transform.localPosition);
            }
        }
        tipsObj. transform. localPosition = originPos;
        tipsObj. transform. localScale = Vector3. one;
        tipsObj. transform. localEulerAngles = Vector3. zero;
        TweenPosition tp = tipsObj. GetComponent<TweenPosition>();
        tp. from = originPos;
        TipsView tv = tipsObj. GetComponent<TipsView>();
        tv. StartTips(content);
        LastTips = tipsObj;
    }
}
