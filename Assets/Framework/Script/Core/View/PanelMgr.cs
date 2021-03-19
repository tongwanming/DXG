using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Common.Utils;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 面板管理
/// </summary>
public class PanelMgr
{
    #region 初始化

    protected static PanelMgr mInstance;

    public static bool hasInstance
    {
        get { return mInstance != null; }
    }

    public static PanelMgr GetInstance
    {
        get
        {
            if (!hasInstance)
            {
                mInstance = new PanelMgr();
            }

            return mInstance;
        }
    }

    private PanelMgr()
    {
        panels = new Dictionary<PanelName, PanelBase>();
        panelsDethList = new List<PanelBase>();
    }

    public Action<object> ShowAction;
    public Action<object> HideAction;

    #endregion

    #region 数据定义

    /// <summary>
    /// 面板显示方式
    /// </summary>
    public enum PanelShowStyle
    {
        /// <summary>
        /// //正常出现--
        /// </summary>
        Nomal,

        /// <summary>
        /// //中间变大--
        /// </summary>
        CenterScaleBigNomal,

        /// <summary>
        /// //上往中滑动--
        /// </summary>
        UpToSlide,

        /// <summary>
        /// //下往中滑动
        /// </summary>
        DownToSlide,

        /// <summary>
        /// //左往中--
        /// </summary>
        LeftToSlide,

        /// <summary>
        /// //右往中--
        /// </summary>
        RightToSlide,

        /// <summary>
        /// //从某点由小到大往中--
        /// </summary>
        SomeplaceToSlide
    }

    /// <summary>
    /// 面板遮罩
    /// </summary>
    public enum PanelMaskSytle
    {
        /// <summary>
        /// 无背景
        /// </summary>
        None,

        /// <summary>
        /// 不透明背景，周围不可关闭
        /// </summary>
        OpacityNone,

        /// <summary>
        /// 不透明背景，周围点击可关闭
        /// </summary>
        Opacity,

        /// <summary>
        /// 半透明，周围不可关闭
        /// </summary>
        TranslucenceNone,

        /// <summary>
        /// 半透明，周围点击可关闭
        /// </summary>
        Translucence,

        /// <summary>
        /// 透明，周围不可关闭
        /// </summary>
        LucencyNone,

        /// <summary>
        /// 透明，周围点击可关闭
        /// </summary>
        Lucency,
    }

    /// <summary>
    /// 存储当前已经实例化的面板
    /// </summary>
    public Dictionary<PanelName, PanelBase> panels;

    /// <summary> 深度列表 </summary>
    public List<PanelBase> panelsDethList;

    #endregion


    /// <summary>
    /// 当前面板
    /// </summary>
    public PanelBase current;

    public Queue<PannelModel> PannelQueue = new Queue<PannelModel>();


    public void Destroy()
    {
    }

    /// <summary>
    /// 打开指定弹框
    /// </summary>
    /// <param name="sceneType"></param>
    /// <param name="panelArgs">场景参数</param>
    public void ShowPanel(PanelName panelName, params object[] panelArgs)
    {
        MainPanel.isXIALUO = false;
        if (panels.ContainsKey(panelName))
        {
            LogUtils.Log(LogUtils.LogColor.Red, panelName, " 该面板已打开！");
            current = panels[panelName];
        }
        else
        {
            GameObject go = new GameObject(panelName.ToString());
            Type mType = Type.GetType(panelName.ToString());
            PanelBase pb = go.AddComponent(mType) as PanelBase; //sceneType.tostring等于该场景的classname
            pb.OnInit(panelArgs);
            pb.isShow = true;
            panels.Add(pb.type, pb);
            MaskStyle(pb);
            panelsDethList.Add(pb);
            ChangePanelDeth(pb);
            current = pb;
            pb.OnShowing();
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.identity;
            StartShowPanel(current, current.PanelShowStyle, true);
        }
    }

    public void AddPanel(PanelName panelName, params object[] panelArgs)
    {
        PannelModel pm = new PannelModel();
        pm.pn = panelName;
        pm.po = panelArgs;
        PannelQueue.Enqueue(pm);
        //if (PannelQueue.Count <= 1) 
        //{
        //    var p = PannelQueue.Dequeue();
        //    ShowPanel(p.pn, p.po);
        //}
        TestingPannel();
    }

    public void TestingPannel()
    {
        if (PannelQueue.Count >= 1)
        {
            var p = PannelQueue.Dequeue();
            ShowPanel(p.pn, p.po);
        }
    }

    /// <summary> 关闭所有面板 </summary>
    public void CloseAllPanel()
    {
        Dictionary<PanelName, PanelBase>.ValueCollection vs = panels.Values;
        foreach (PanelBase item in vs)
        {
            StartShowPanel(item, item.PanelShowStyle, false);
        }

        panelsDethList.Clear();
    }

    /// <summary> 打开关闭面板效果 </summary>
    private void StartShowPanel(PanelBase go, PanelShowStyle showStyle, bool isOpen)
    {
        switch (showStyle)
        {
            case PanelShowStyle.Nomal:
                ShowNomal(go, isOpen);
                break;
            case PanelShowStyle.CenterScaleBigNomal:
                CenterScaleBigNomal(go, isOpen);
                break;
            case PanelShowStyle.LeftToSlide:
                LeftAndRightToSlide(go, false, isOpen);
                break;
            case PanelShowStyle.RightToSlide:
                LeftAndRightToSlide(go, true, isOpen);
                break;
            case PanelShowStyle.UpToSlide:
                TopAndDownToSlide(go, true, isOpen);
                break;
            case PanelShowStyle.DownToSlide:
                TopAndDownToSlide(go, false, isOpen);
                break;
            case PanelShowStyle.SomeplaceToSlide:
                SomeplaceToSlide(go, isOpen);
                break;
        }
    }

    #region 显示方式

    /// <summary> 默认显示 </summary>
    private void ShowNomal(PanelBase go, bool isOpen)
    {
        if (isOpen)
        {
            current.gameObject.SetActive(true);
            current.OnShowed();
        }
        else
        {
            DestroyPanel(go.type);
        }
    }

    /// <summary> 中间变大 </summary>
    private void CenterScaleBigNomal(PanelBase go, bool isOpen)
    {
        TweenScale ts = go.gameObject.GetComponent<TweenScale>();
        if (ts == null)
        {
            ts = go.gameObject.AddComponent<TweenScale>();
        }

        if (isOpen)
        {
            ts.Reset();
        }

        ts.from = Vector3.zero;
        ts.to = Vector3.one;
        ts.duration = go.OpenDuration;
        ts.ease = Ease.InOutSine;
        ts.onFinished = () =>
        {
            if (isOpen)
            {
                go.OnShowed();
            }
            else
            {
                DestroyPanel(go.type);
            }
        };
        go.gameObject.SetActive(true);
        ts.Play(isOpen);
    }

    /// <summary> 左右往中 </summary>
    private void LeftAndRightToSlide(PanelBase go, bool isRight, bool isOpen)
    {
        TweenPosition tp = go.gameObject.GetComponent<TweenPosition>();
        TweenAlpha ta = go.gameObject.GetComponent<TweenAlpha>();
        if (tp == null)
        {
            tp = go.gameObject.AddComponent<TweenPosition>();
        }

        if (ta == null)
        {
            ta = go.gameObject.AddComponent<TweenAlpha>();
        }

        if (isOpen)
        {
            ta.Reset();
            tp.Reset();
        }

        tp.from = isRight == true ? new Vector3(1000, 0, 0) : new Vector3(-1000, 0, 0);
        tp.to = Vector3.zero;
        ta.from = 0;
        ta.to = 1;
        ta.duration = go.OpenDuration;
        tp.duration = go.OpenDuration;
        tp.ease = Ease.InOutSine;
        tp.onFinished = () =>
        {
            if (isOpen)
            {
                go.OnShowed();
            }
            else
            {
                DestroyPanel(go.type);
            }
        };
        go.gameObject.SetActive(true);
        tp.Play(isOpen);
        ta.Play(isOpen);
    }

    /// <summary> 上下往中 </summary>
    private void TopAndDownToSlide(PanelBase go, bool isTop, bool isOpen)
    {
        TweenPosition tp = go.gameObject.GetComponent<TweenPosition>();
        if (tp == null)
        {
            tp = go.gameObject.AddComponent<TweenPosition>();
        }

        if (isOpen)
        {
            tp.Reset();
        }

        tp.from = isTop == true ? new Vector3(0, 1000, 0) : new Vector3(0, -1000, 0);
        tp.to = Vector3.zero;
        tp.duration = go.OpenDuration;
        tp.ease = Ease.InOutSine;
        tp.onFinished = () =>
        {
            if (isOpen)
            {
                go.OnShowed();
            }
            else
            {
                DestroyPanel(go.type);
            }
        };
        go.gameObject.SetActive(true);
        tp.Play(isOpen);
    }

    /// <summary> 从某个点往中 </summary>
    private void SomeplaceToSlide(PanelBase go, bool isOpen)
    {
        TweenPosition tp = go.gameObject.GetComponent<TweenPosition>();
        TweenScale ts = go.gameObject.GetComponent<TweenScale>();
        if (tp == null)
        {
            tp = go.gameObject.AddComponent<TweenPosition>();
        }

        if (ts == null)
        {
            ts = go.gameObject.AddComponent<TweenScale>();
        }

        if (isOpen)
        {
            ts.Reset();
            tp.Reset();
        }

        tp.from = new Vector3(UnityEngine.Random.Range(0, 1920), UnityEngine.Random.Range(0, 1080));
        tp.to = Vector3.zero;
        ts.from = Vector3.zero;
        ts.to = Vector3.one;
        tp.duration = go.OpenDuration;
        ts.duration = go.OpenDuration;
        tp.ease = Ease.Linear;
        ts.ease = Ease.Linear;
        tp.onFinished = () =>
        {
            if (isOpen)
            {
                go.OnShowed();
            }
            else
            {
                DestroyPanel(go.type);
            }
        };
        go.gameObject.SetActive(true);
        tp.Play(isOpen);
        ts.Play(isOpen);
    }

    #endregion

    #region 遮罩方式

    private void MaskStyle(PanelBase go)
    {
        Transform mask = ResourceMgr.GetInstance.CreateTransform("PanelMask", true);
        mask.GetComponent<RectTransform>().sizeDelta =
            GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta;
        mask.GetComponent<ButtonEx>().onLeftClick = g =>
        {
            HidePanel((PanelName) Enum.Parse(typeof(PanelName), mask.transform.parent.name));
        };
        float alpha = 0;
        switch (go.PanelMaskStyle)
        {
            case PanelMaskSytle.None:
                alpha = 0;
                mask.GetComponent<ButtonEx>().onLeftClick = g => { };
                break;
            case PanelMaskSytle.OpacityNone:
                alpha = 1f;
                mask.GetComponent<ButtonEx>().onLeftClick = g => { };
                break;
            case PanelMaskSytle.Opacity:
                alpha = 1f;
                break;
            case PanelMaskSytle.TranslucenceNone:
                alpha = 0.9f;
                mask.GetComponent<ButtonEx>().onLeftClick = g => { };
                break;
            case PanelMaskSytle.Translucence:
                alpha = 0.9f;
                break;
            case PanelMaskSytle.LucencyNone:
                alpha = 0f;
                mask.GetComponent<ButtonEx>().onLeftClick = g => { };
                break;
            case PanelMaskSytle.Lucency:
                alpha = 0;
                break;
        }

        mask.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        mask.SetParent(go.gameObject.transform);
        mask.localPosition(Vector3.zero).localEulerAngles(Vector3.zero).localScale(1);
        LayerMgr.GetInstance.SetLayer(go.gameObject, LayerType.Panel);
    }

    #endregion

    /// <summary>
    /// 发起关闭
    /// </summary>
    public void HidePanel(PanelName type)
    {
        if (panels.ContainsKey(type))
        {
            PanelBase pb = null;
            pb = panels[type];
            pb.OnHideFront();
            StartShowPanel(pb, pb.PanelShowStyle, false);
            panelsDethList.Remove(pb);
        }
        else
        {
            Debug.LogError(type + " 关闭的面板不存在!");
        }
    }


    // <summary> 改变面板的深度 </summary>
    public void ChangePanelDeth(PanelBase type)
    {
        if (panelsDethList.Contains(type))
        {
            if (current == type)
            {
                return;
            }

            panelsDethList.Remove(type);
            panelsDethList.Add(type);
        }
        else
        {
            Debug.LogError("面板不存在");
            return;
        }

        LayerMgr.GetInstance.SetPanelsLayer(panelsDethList);
    }


    /// <summary>
    /// 强制摧毁面板
    /// </summary>
    /// <param name="panel"></param>
    public void DestroyPanel(PanelName type)
    {
        if (panels.ContainsKey(type))
        {
            PanelBase pb = panels[type];
            if (!pb.cache)
            {
                pb.isShow = false;
                pb.OnHideDone();
                GameObject.Destroy(pb.gameObject);
                panels.Remove(type);
                panelsDethList.Remove(pb);
            }
        }
    }
}

public class PannelModel
{
    public PanelName pn;
    public object[] po;
}