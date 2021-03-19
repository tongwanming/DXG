using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WithdrawPanel : PanelBase, OnMoneyChangeListener
{
    private Image adBcImage;

    #region 界面加载

    protected override void OnInitFront()
    {
        base.OnInitFront();
        _type = PanelName.WithdrawPanel;
        _openDuration = 0.5f;
        _showStyle = PanelMgr.PanelShowStyle.Nomal;
        _maskStyle = PanelMgr.PanelMaskSytle.TranslucenceNone;
        _cache = false;
    }

    protected override void OnInitSkinFront()
    {
        base.OnInitSkinFront();
        SetMainSkinPath("Panel/WithdrawPanel");
    }

    public override void OnInit(params object[] sceneArgs)
    {
        base.OnInit(sceneArgs);
        skinTrs.GetComponent<RectTransform>().sizeDelta = M_Canvas.sizeDelta;
        InitData();
        MoneyManager.addListener(this);
    }

    #endregion

    #region 数据定义

    private GameObject Tip;
    private TMP_Text _MTvTotalMoney;

    #endregion

    #region 逻辑

    /// <summary>初始化</summary>
    private void InitData()
    {
        FindObj();
        InitUi();
        AudioMgr.Instance.PlayBGAudios(true);
    }

    /// <summary>查找物体</summary>
    private void FindObj()
    {
        Tip = skinTrs.SeachTrs<Transform>("money_tip").gameObject;
        _MTvTotalMoney = skinTrs.SeachTrs<TMP_Text>("tv_total_money");
    }


    private void InitUi()
    {
        _MTvTotalMoney.text = DataManager.getCurrentMoney().ToString("F2");
    }

    public override void OnShowing()
    {
        base.OnShowing();
#if !UNITY_EDITOR
        // AdManager.Instance.showNativeAd((int) adBcImage.transform.position.y);
#endif
        AudioMgr.Instance.PlayBGAudios(true);
    }

    /// <summary>按钮点击事件</summary>
    protected override void OnClick(Transform target)
    {
        switch (target.name)
        {
            case "Btn_Back":
                Close();
                break;
            case "Btn_Withdraw":
                if (!Tip.activeSelf)
                {
                    Tip.transform.GetChild(0).GetComponent<TMP_Text>().text = "you are not satisfied with the cash requirement";
                    Tip.SetActive(true);
                    Invoke(nameof(HideTip), 2f);
                }

                break;
        }
    }


    private void HideTip()
    {
        Tip.SetActive(false);
    }

    #endregion

    public void onMoneyChange(float value)
    {
        _MTvTotalMoney.text = value.ToString();
    }


    public override void OnHideDone()
    {
        base.OnHideDone();
        MoneyManager.removeListener(this);
    }
}