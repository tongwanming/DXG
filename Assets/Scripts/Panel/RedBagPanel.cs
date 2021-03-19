using System;
using System.Collections;
using System.Collections.Generic;
using Ads;
using Common.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RedBagPanel : PanelBase
{
    #region 界面加载

    protected override void OnInitFront()
    {
        base.OnInitFront();
        _type = PanelName.RedBagPanel;
        _openDuration = 0.5f;
        _showStyle = PanelMgr.PanelShowStyle.Nomal;
        _maskStyle = PanelMgr.PanelMaskSytle.TranslucenceNone;
        _cache = false;
    }

    protected override void OnInitSkinFront()
    {
        base.OnInitSkinFront();
        SetMainSkinPath("Panel/RedBagPanel");
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
        Config.Instance.isOpenLottery = false;
        Config.Instance.JudgeLevelUp();
    }

    #endregion

    #region 数据定义

    public const int INDEX_LOCK = 1;
    public const int INDEX_OPEN = 2;

    private int mCurrentIndex = 1;
    private Transform mLock;
    private Transform mOpen;
    private RewardInfo mReward;

    private TMP_Text _mOverAddMoney;
    private TMP_Text _mOverTotalMoney;

    private Image Btn_open;

    #endregion

    #region 逻辑

    /// <summary>初始化</summary>
    private void InitData()
    {
        Config.Instance.isOpenLottery = true;
        if (_panelArgs.Length != 0)
        {
            mCurrentIndex = (int)_panelArgs[0];
        }

        FindObj();
        if (DataManager.getCurrentMoney() == 0)
        {
            Btn_open.sprite = Resources.Load<Sprite>("red_open");
        }

        // AudioMgr.Instance.PlayAudios("06");
    }

    /// <summary>查找物体</summary>
    private void FindObj()
    {
        mLock = skinTrs.SeachTrs<Transform>("Lock");
        mOpen = skinTrs.SeachTrs<Transform>("open");
        _mOverAddMoney = skinTrs.SeachTrs<TMP_Text>("over_add_money");
        _mOverTotalMoney = skinTrs.SeachTrs<TMP_Text>("over_total_money");
        Btn_open = skinTrs.SeachTrs<Image>("Btn_open");
        showResult();
    }

    private void closePanel()
    {
        Close();
        Config.Instance.CreatRed();
#if UNITY_ANDROID
        DataManager.addGiveUpNum();
        int max = 2;
        if (DataManager.getGiveUpNum() % max == 0)
        {
            AdManager.Instance.ShowInterstitial();
        }
#endif
    }

    /// <summary>按钮点击事件</summary>
    protected override void OnClick(Transform target)
    {
        if (UnityUtils.Instance.IsFastClick(800))
        {
            return;
        }

        switch (target.name)
        {
            case "Btn_close":
                if (DataManager.getCurrentMoney() == 0)
                {
                    DataManager.addGiveUpNum();
                    mReward = RewardManager.getNextReward(DataManager.getCurrentMoney());
                    DataManager.addMoney((float)mReward.reward);
                    Close();
                    refreshMoney();
                }
                else
                {
                    closePanel();
                }

                break;
            case "Btn_open_close":
                Close();
                refreshMoney();
                break;
            case "Btn_continue":
                Close();
                refreshMoney();
                break;
            case "Btn_open":
#if UNITY_EDITOR
                mCurrentIndex = INDEX_OPEN;
                showResult();
#elif UNITY_ANDROID
                if (DataManager.getCurrentMoney() == 0)
                {
                    mCurrentIndex = INDEX_OPEN;
                    showResult();
                }
                else
                {
                    if (AdManager.Instance.IsRewardedAvailable())
                    {
                        AdManager.OnRewardedAdRewardedEvent -= null; 
                        AdManager.OnRewardedAdRewardedEvent += tag =>
                        {
                            if(tag=="RedBagPanel"){
                                ThreadManager.Instance.runOnMainThread(() =>
                                {
                                    mCurrentIndex = INDEX_OPEN;
                                    showResult();
                                });
                            }
                           
                        };
                        AdManager.Instance.ShowRewardedWithTag("RedBagPanel");
                    }
                    else
                    {
                        LogicMgr.GetInstance.GetLogic<LogicTips>().AddTips("Please try again later");
                    }
                }

#endif

                break;
            case "Btn_with_draw":
                Close();
                refreshMoney();
                //PanelMgr.GetInstance.ShowPanel(PanelName.Panel_Withdraw);
                break;
            case "Btn_fly_save":
                DataManager.addMoney((float)mReward.reward);
                Close();
                refreshMoney();
                break;
            case "Btn_fly_close":
                Close();
                Config.Instance.CreatRed();
                break;
        }
    }

    private void refreshMoney()
    {
        if (mReward != null && mReward.reward != 0)
        {
            Config.Instance.RefreshMoney(mReward.reward);
        }
    }


    private void showResult()
    {
        mLock.gameObject.SetActive(mCurrentIndex == INDEX_LOCK);
        mOpen.gameObject.SetActive(mCurrentIndex == INDEX_OPEN);
        if (mCurrentIndex == INDEX_OPEN)
        {
            //打开红包
            mReward = RewardManager.getNextReward(DataManager.getCurrentMoney());
            _mOverAddMoney.text = $"{mReward.reward:F2}usd";
            _mOverAddMoney.text = "<size=64>" + mReward.reward.ToString("F2") + "</size>" + "usd";
            DataManager.addMoney((float)mReward.reward);

            _mOverTotalMoney.GetComponent<TMP_Text>().text =
                $"{" BALANCE:" + DataManager.getCurrentMoney().ToString("F2") + "usd"}";
        }

        Invoke("showClose", 2f);
    }


    private void showClose()
    {
        if (mCurrentIndex == INDEX_LOCK)
        {
            mLock.SeachTrs<Transform>("Btn_close").gameObject.SetActive(true);
        }
        else if (mCurrentIndex == INDEX_OPEN)
        {
            mOpen.SeachTrs<Transform>("Btn_open_close").gameObject.SetActive(true);
        }
    }

    #endregion
}