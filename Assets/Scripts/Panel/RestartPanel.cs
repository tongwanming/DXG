using System;
using System.Collections;
using System.Collections.Generic;
using Ads;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestartPanel : PanelBase
{
    private Image adBcImage;

    #region 界面加载

    protected override void OnInitFront()
    {
        base.OnInitFront();
        _type = PanelName.RestartPanel;
        _openDuration = 0.5f;
        _showStyle = PanelMgr.PanelShowStyle.Nomal;
        _maskStyle = PanelMgr.PanelMaskSytle.TranslucenceNone;
        _cache = false;
    }

    protected override void OnInitSkinFront()
    {
        base.OnInitSkinFront();
        SetMainSkinPath("Panel/RestartPanel");
    }

    public override void OnInit(params object[] sceneArgs)
    {
        base.OnInit(sceneArgs);
        skinTrs.GetComponent<RectTransform>().sizeDelta = M_Canvas.sizeDelta;
        InitData();
    }


    protected override void Close()
    {
        base.Close();
        Config.Instance.isOpenCheckpoint = false;
        Config.Instance.JudgeLevelUp();
    }

    #endregion

    #region 数据定义

    private bool isWin;

    #endregion

    #region 逻辑

    /// <summary>初始化</summary>
    private void InitData()
    {
        Config.Instance.isOpenCheckpoint = true;

        if (_panelArgs.Length != 0)
        {
            isWin = (bool)_panelArgs[0];
            FindObj(isWin);
        }
    }

    /// <summary>查找物体</summary>
    private void FindObj(bool isWin)
    {
        adBcImage = skinTrs.SeachTrs<Image>("ImageBc");
        skinTrs.SeachTrs<Transform>("Win").gameObject.SetActive(isWin);
        skinTrs.SeachTrs<Transform>("Lose").gameObject.SetActive(!isWin);
        if (isWin)
        {
            skinTrs.SeachTrs<TMP_Text>("Now_Text").text = DataManager.getScore.addUnit();
            skinTrs.SeachTrs<TMP_Text>("Data_Text").text = DataManager.getMaxScore.addUnit();
            AudioMgr.Instance.PlayAudios("03");
        }
        else
        {
            skinTrs.SeachTrs<TMP_Text>("Lose_Now_Text").text = DataManager.getScore.addUnit();
            skinTrs.SeachTrs<TMP_Text>("Lose_Data_Text").text = DataManager.getMaxScore.addUnit();
            AudioMgr.Instance.PlayAudios("05");
        }
    }

    /// <summary>按钮点击事件</summary>
    protected override void OnClick(Transform target)
    {
        switch (target.name)
        {
            case "Btn_Continue":
                Close();
                break;
            case "Btn_Lose_Continue":
                if (AdManager.Instance.IsRewardedAvailable())
                {
                    AdManager.Instance.ShowRewardedWithTag("RestartPanel");
                }
                else
                {
                    Config.Instance.RetryEvent();
                    Close();
                }

                break;
            case "Btn_Close":
                Close();

                break;
        }
    }

    private void OnEnable()
    {
        AdManager.OnRewardedAdRewardedEvent += OnRewardedAdRewarded;
    }

    private void OnDisable()
    {
        AdManager.OnRewardedAdRewardedEvent -= OnRewardedAdRewarded;
    }

    /// <summary>
    ///  Rewarded Ad Successful.see
    /// </summary>
    private void OnRewardedAdRewarded(string watchVidoTag)
    {
        if (watchVidoTag == "RestartPanel")
        {
            ThreadManager.Instance.runOnMainThread(() =>
            {
                Config.Instance.RetryEvent();
                Close();
            });
        }
    }

    #endregion
}