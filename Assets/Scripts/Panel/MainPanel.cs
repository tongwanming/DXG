using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Ads;
using Common.Utils;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainPanel : SceneBase, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region 界面加载

    protected override void OnInitSkinFront()
    {
        base.OnInitSkinFront();
        SetMainSkinPath("Panel/MainPanel");
    }

    protected override void OnInitFront()
    {
        base.OnInitFront();
        _type = SceneType.MainPanel;
    }

    public override void OnInit(params object[] sceneArgs)
    {
        base.OnInit(sceneArgs);
        InitData();
    }

    public override void OnShowed()
    {
        base.OnShowed();
        Config.Instance.CreateDefute();
        Config.Instance.CreateLevel();
        if (Config.Instance.isExistsFile)
            GetSaveValue();
        else
        {
            DataManager.addLevel(1);
        }
    }

    public override void OnHiding()
    {
        base.OnHiding();
        Config.Instance.QQMergaFront -= QQMergaFront;
        Config.Instance.QQMergaDone -= QQMergaDone;
        Config.Instance.LevelUp -= LevelUp;
        Config.Instance.JudgeLevelUp -= JudgeLevelUp;
        Config.Instance.SetInfo -= SetInfo;
        Config.Instance.RefreshMoney -= RefreshMoney;
        Config.Instance.RetryEvent -= RetryEvent;
        Config.Instance.isWipes -= Isopen;
        Config.Instance.AddLevelFront -= AddLevelFront;
        Config.Instance.AddLevelDone -= AddLevelDone;
    }

    #endregion

    #region 数据定义

    private RectTransform m_Btn_DragArea;
    private RectTransform m_Center;
    private RectTransform m_DownBorder;
    private Transform m_SphereParent;
    private Transform m_SphereResultParent;
    private TMP_Text m_LevelLeft;
    private TMP_Text m_LevelRight;
    private Slider m_LevelSlider;
    private TMP_Text m_MaxScore;
    private Text m_Score;
    private Transform Finger;
    private Transform broadcast;
    private TMP_Text broadcast_Text;
    private string tips;
    private string ID;
    private float With;
    private RectTransform m_WithDraw;
    private int composite_ball_number;
    private Transform m_FlyEND;
    private GameObject m_CoinEffect;
    private Transform m_Left;
    private Transform m_Right;
    private Transform m_AllRed;
    private GameObject m_NONetwork;
    private GameObject m_LevelMoneyParent;
    private TMP_Text m_TotalMoney;
    private GameObject BorderDeath;
    private Transform RemoveObjTips;
    private bool isNetwork;
    private float utime = 1;//计时
    public Queue<Transform> Boom = new Queue<Transform>();
    private bool isOpenBoom;
    public static bool isShowRed = true;
    public static bool isOpenCreatMerge;
    public static bool isRemoveClassSphere;

    public static bool isXIALUO = true;

    #endregion

    #region 逻辑

    /// <summary>初始化</summary>
    private void InitData()
    {
        //StatisticUtil.autoTrack();
        //StatisticUtil.openUser();
        FindObj();
        AddEvent();
        Config.Instance.SphereCreateParent = m_Btn_DragArea;
        Config.Instance.SphereParent = m_SphereParent;
        Config.Instance.SphereResultParent = m_SphereResultParent;
        DataManager.resetGiveUpNum();
        LevelUp();
        SetInfo();

        SetFunctionNum("Btn_gravity", DataManager.getGravityNum);
        SetFunctionNum("Btn_universal", DataManager.getUniversalNum);

        Config.Instance.isCanSave = true;
        Tips();
        //WithDraw();




        OpenFinger(true); //non_organic 开启按钮点击导航
        AudioMgr.Instance.PlayBGAudios(true);
        m_DownBorder.sizeDelta = new Vector2(438, M_Canvas.sizeDelta.y / 2 - m_Center.sizeDelta.y / 2);
        StartCoroutine(OpenNetwork());
    }


    private void FindObj()
    {
        m_WithDraw = skinTrs.SeachTrs<RectTransform>("Btn_WithDraw");
        broadcast = skinTrs.SeachTrs<Transform>("broadcast");
        broadcast_Text = skinTrs.SeachTrs<TMP_Text>("broadcast_Text");
        RemoveObjTips = skinTrs.SeachTrs<Transform>("RemoveObjTips");
        Finger = skinTrs.SeachTrs<RectTransform>("Finger");
        m_Btn_DragArea = skinTrs.SeachTrs<RectTransform>("Btn_DragArea");
        m_SphereParent = skinTrs.SeachTrs<RectTransform>("SphereParent");
        m_SphereResultParent = skinTrs.SeachTrs<RectTransform>("SphereResultParent");
        BorderDeath = skinTrs.SeachTrs<Transform>("BorderDeath").gameObject;
        m_LevelLeft = skinTrs.SeachTrs<TMP_Text>("Txt_LevelLeft");
        m_LevelRight = skinTrs.SeachTrs<TMP_Text>("Txt_LevelRight");
        m_LevelSlider = skinTrs.SeachTrs<Slider>("LevelSlider");
        m_MaxScore = skinTrs.SeachTrs<TMP_Text>("Txt_MaxScore");
        m_Score = skinTrs.SeachTrs<Text>("Txt_Score");
        With = -2.5f * broadcast_Text.transform.parent.GetComponent<RectTransform>().rect.width;
        m_FlyEND = skinTrs.SeachTrs<Transform>("WithDraw_Text");
        skinTrs.SeachTrs<TMP_Text>("WithDraw_Text").text = DataManager.getCurrentMoney().ToString("F2");
        m_CoinEffect = skinTrs.SeachTrs<Transform>("CoinEffect").gameObject;
        m_Left = skinTrs.SeachTrs<Transform>("Left");
        m_Right = skinTrs.SeachTrs<Transform>("Right");
        m_AllRed = skinTrs.SeachTrs<Transform>("AllRedEnvelope");
        m_Center = skinTrs.SeachTrs<RectTransform>("Center");
        m_DownBorder = skinTrs.SeachTrs<RectTransform>("DownBoder");
        m_LevelMoneyParent = skinTrs.SeachTrs<Transform>("LevelMoney").gameObject;
        m_TotalMoney = skinTrs.SeachTrs<TMP_Text>("Tv_total_money");
        m_NONetwork = skinTrs.Find("NONetwork").gameObject;
    }

    private void AddEvent()
    {
        Config.Instance.QQMergaFront += QQMergaFront;
        Config.Instance.QQMergaDone += QQMergaDone;
        Config.Instance.LevelUp += LevelUp;
        Config.Instance.JudgeLevelUp += JudgeLevelUp;
        Config.Instance.SetInfo += SetInfo;
        Config.Instance.RefreshMoney += RefreshMoney;
        Config.Instance.RetryEvent += RetryEvent;
        Config.Instance.isWipes += Isopen;
        Config.Instance.AddLevelFront += AddLevelFront;
        Config.Instance.AddLevelDone += AddLevelDone;
        Config.Instance.CreatRed = CreatRed;
    }

    private IEnumerator OpenNetwork()
    {
        isNetwork = !NetworkHelper.checkNetwork();
        if (isNetwork)
            LogicMgr.GetInstance.GetLogic<LogicTips>().AddTips("Network error");
        m_NONetwork.SetActive(isNetwork);
        yield return new WaitForSeconds(20);
        StartCoroutine(OpenNetwork());
    }

    private void Isopen(bool obj)
    {
        m_CoinEffect.SetActive(obj);
    }

    private void OpenFinger(object _isShow)
    {
        bool isShow = (bool)_isShow;
        Finger.gameObject.SetActive(isShow);
        Finger.DOLocalMoveX(-243f, 2f).OnComplete(() =>
        {
            Finger.DOLocalMoveX(243f, 2f).OnComplete(() => { OpenFinger(isShow); }).SetId("Finger2");
        }).SetId("Finger");
    }

    private void GetSaveValue()
    {
        for (int i = 0; i < DataManager.getSphereInfo.Count; i++)
        {
            SphereInfo info = DataManager.getSphereInfo[i];
            LevelSphere sphere = Config.Instance.CreateSphere(m_SphereParent, info.num,
                new Vector2((float)info.pos[0], (float)info.pos[1]));
            sphere.id = -(info.id == 0 ? i : info.id);
            sphere.m_Rigidbody.constraints = RigidbodyConstraints2D.None;
        }
    }


    private void RefreshMoney(double value)
    {
        float temp = float.Parse(skinTrs.SeachTrs<TMP_Text>("WithDraw_Text").text);
        GameObject clone = (GameObject)Instantiate(Resources.Load("Prefabs/FlyNumber"), transform);
        //RewardInfo reward = RewardManager.getNextReward(DataManager.getCurrentMoney());
        //clone.GetComponent<TMP_Text>().text = value.ToString();
        clone.gameObject.transform.SeachTrs<TMP_Text>("money").text = value.ToString();
        clone.transform.DOScale(new Vector3(3, 3, 3), 0.5f).OnComplete(() =>
        {
            clone.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() =>
            {
                clone.transform.DOMove(m_FlyEND.position, 1f).OnComplete(() =>
                {
                    DOTween.To(() => float.Parse(m_FlyEND.GetComponent<TMP_Text>().text.ToString()), x => temp = x,
                        DataManager.getCurrentMoney(), 1).OnUpdate(() => { m_TotalMoney.text = temp.ToString("f2"); });
                    Destroy(clone);
                });
            });
        });
    }

    private void LevelUp()
    {
        m_LevelLeft.text = DataManager.getLevel.ToString();
        m_LevelRight.text = (DataManager.getLevel + 1).ToString();
        m_LevelSlider.maxValue = 50f;
        m_LevelSlider.value = DataManager.getExp;
    }

    private void QQMergaFront(int fen)
    {
        AudioMgr.Instance.PlayAudios("02");
        int i = 1;
        int di = 2;
        if (fen < 2048)
        {
            while (di != fen)
            {
                di *= 2;
                i++;
            }
        }
        else
        {
            i = 100;
        }

        DataManager.addScore(i);
        SetInfo();
    }

    private void QQMergaDone()
    {
        if (isShowRed)
        {
            if (DataManager.getCurrentMoney() == 0)
            {
                PanelMgr.GetInstance.ShowPanel(PanelName.RedBagPanel, RedBagPanel.INDEX_LOCK);
            }
            else
            {
                composite_ball_number++;
                if (composite_ball_number == 10)
                {
                    if (Config.Instance.isLevelShow)
                    {
                        Config.Instance.showRedBag += showRedBag;
                    }
                    else
                    {
                        if (RewardManager.getNextReward(DataManager.getCurrentMoney()).reward > 0f)
                        {
                            PanelMgr.GetInstance.ShowPanel(PanelName.RedBagPanel, RedBagPanel.INDEX_LOCK);
                        }
                    }

                    composite_ball_number = 0;
                }
            }

            JudgeLevelUp();
        }
    }

    private void showRedBag()
    {
        PanelMgr.GetInstance.ShowPanel(PanelName.RedBagPanel, RedBagPanel.INDEX_LOCK);
        Config.Instance.showRedBag -= showRedBag;
    }

    private void JudgeLevelUp()
    {
    }

    private void AddLevelFront()
    {
        float leftAngle = m_Left.localEulerAngles.z;
        DOTween.To(() => 0, x => leftAngle = x, 100, 0.2f).OnUpdate(() => m_Left.localEulerAngles(0, 0, leftAngle))
            .OnComplete(() =>
            {
                DOTween.To(() => 100, x => leftAngle = x, 90, 0.2f)
                    .OnUpdate(() => m_Left.localEulerAngles(0, 0, leftAngle));
            });

        float rightAngle = m_Right.localEulerAngles.z;
        DOTween.To(() => 0, x => rightAngle = x, 100, 0.2f).OnUpdate(() => m_Right.localEulerAngles(0, 0, rightAngle))
            .OnComplete(() =>
            {
                DOTween.To(() => 100, x => rightAngle = x, 90, 0.2f)
                    .OnUpdate(() => m_Right.localEulerAngles(0, 0, rightAngle));
            });
    }

    private void AddLevelDone()
    {
        m_Left.localEulerAngles(Vector3.zero);
        m_Right.localEulerAngles(Vector3.zero);
    }

    private void SetInfo()
    {
        m_LevelSlider.value = DataManager.getExp;
        m_MaxScore.text = DataManager.getMaxScore.addUnit();
        m_Score.text = "" + DataManager.getScore.addUnit();

        m_LevelMoneyParent.SetActive(true);
        m_TotalMoney.text = DataManager.getCurrentMoney().ToString("F2");
    }

    private void SetSphere(bool isShow)
    {
        if (m_Btn_DragArea.childCount == 0 || Config.Instance.isDown || !isXIALUO) return;

        Transform tempSphere = m_Btn_DragArea.GetChild(0);
        //  tempSphere.Find("Line").gameObject.SetActive(isShow);


        if (!isShow)
        {
            BorderDeath.SetActive(false);
            CloseFinger();
            Rigidbody2D rigidbody = tempSphere.GetComponent<Rigidbody2D>();
            rigidbody.constraints = RigidbodyConstraints2D.None;
            rigidbody.AddForce(new Vector2(UnityEngine.Random.Range(-1, 2) * 30, 0));
            tempSphere.SetParent(Config.Instance.SphereParent);
            tempSphere.GetComponent<LevelSphere>().m_Circle.enabled = true;
            StartCoroutine(create());
        }
    }

    IEnumerator create()
    {
        yield return new WaitForSeconds(1f);
        Config.Instance.CreateDefute();
        BorderDeath.SetActive(true);
    }

    private void CreatRed()
    {
        if (m_AllRed.childCount < 1)
        {
            ButtonEx m_Read = ObjectPool.Instance.Spawn<ButtonEx>("RedEnvelope", m_AllRed);
            m_Read.transform.localPosition(Vector3.zero).localScale(1);
            m_Read.onLeftClick = go =>
            {
                if (AdManager.Instance.IsRewardedAvailable())
                {
                    AdManager.OnRewardedAdRewardedEvent -= null;
                    AdManager.OnRewardedAdRewardedEvent += tag =>
                    {
                        ThreadManager.Instance.runOnMainThread(() =>
                        {
                            ObjectPool.Instance.DestorySpawn(m_Read.gameObject);
                            PanelMgr.GetInstance.ShowPanel(PanelName.RedBagPanel, RedBagPanel.INDEX_OPEN);
                        });
                    };
                    AdManager.Instance.ShowRewardedWithTag();
                }
                LogUtils.Log(LogUtils.LogColor.Yellow, "点击悬浮球");
            };
            StartCoroutine(DestroyRed(m_Read.gameObject));
        }
    }

    IEnumerator DestroyRed(GameObject m_Read)
    {
        yield return new WaitForSeconds(10f);
        if (m_Read != null)
        {
            ObjectPool.Instance.DestorySpawn(m_Read);
            //Destroy(m_Read);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    private float initPosX = 0;

    public void OnDrag(PointerEventData eventData)
    {
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isNetwork && isXIALUO)
        {
            initPosX = 0;
            SetSphere(false);
        }
    }

    private void RetryEvent()
    {
        DataManager.getLevelScore = 0;
        DataManager.getScore = 0;
        DataManager.getSphereInfo = new List<SphereInfo>();
        SetInfo();
        Config.Instance.CreateLevel();
        Config.Instance.CreateDefute();
        ObjectPool.Instance.MergeEvents = new Queue<MergeEvent>();
        ObjectPool.Instance.Cando = true;
        for (int i = 0; i < m_SphereParent.childCount; i++)
            ObjectPool.Instance.Unspawn(m_SphereParent.GetChild(i).gameObject);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetSphere(false);
        }
#endif

        if (utime < 0.3f)
        {
            utime += Time.deltaTime;
        }
        else
        {
            //判断是否点击
            if (Input.GetMouseButton(0))
            {
                if (!isNetwork)
                {
                    if (m_Btn_DragArea.childCount == 0) return;
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//获取点击位置
                    Vector3 old = m_Btn_DragArea.GetChild(0).position;

                    m_Btn_DragArea.GetChild(0).position = new Vector3(mousePosition.x, old.y, old.z);
                }
            }

            //判断是否完成点击
            if (Input.GetMouseButtonUp(0))
            {
                if (!isNetwork)
                {
                    if (m_Btn_DragArea.childCount == 0) return;
                    BorderDeath.SetActive(false);
                    SetSphere(false);

                    utime = 0;
                }
            }
        }

        if (isOpenBoom && Boom.Count > 0)
        {
            isOpenBoom = false;
            BoomEffet(Boom.Dequeue());
            if (Boom.Count == 0)
            {
                PanelMgr.GetInstance.ShowPanel(PanelName.RestartPanel, false);
            }
        }
    }

    /// <summary>按钮点击事件</summary>
    protected override void OnClick(Transform target)
    {
        switch (target.name)
        {
            case "Btn_WithDraw":
                PanelMgr.GetInstance.ShowPanel(PanelName.WithdrawPanel);
                break;
            case "Btn_Retry":
                // PanelMgr.GetInstance.ShowPanel(PanelName.Panel_Restart);
                RemoveClassSphere();
                break;
            case "Btn_DragArea":
                SetSphere(false);
                break;
            case "Btn_Setting":
                break;
            case "Btn_universal":
                if (DataManager.getUniversalNum > 0)
                {
                    if (Config.Instance.SphereCreateParent.GetChild(0).GetComponent<LevelSphere>().SphereType !=
                        LevelSphere.Type.nomal) return;
                    if (Config.Instance.hasUniversal)
                    {
                        return;
                    }

                    DataManager.setUniversalNum();
                    Config.Instance.CreateTypeSphere(LevelSphere.Type.universal);
                    Config.Instance.hasUniversal = true;
                }
                else
                {
#if UNITY_EDITOR
                    DataManager.setUniversalNum(true);
#elif UNITY_ANDROID
                    if (AdManager.Instance.IsRewardedAvailable())
                    {
                        if (AdManager.Instance.IsRewardedAvailable())
                        {
                            AdManager.OnRewardedAdRewardedEvent -= null; 
                            AdManager.OnRewardedAdRewardedEvent += tag =>
                            {
                                DataManager.setUniversalNum(true);
                                SetFunctionNum(target.name, DataManager.getGravityNum);
                            };
                            AdManager.Instance.ShowRewardedWithTag();
                        }
                    }
#endif
                }

                SetFunctionNum(target.name, DataManager.getUniversalNum);
                break;
            case "Btn_gravity":
                if (DataManager.getGravityNum > 0)
                {
                    if (Config.Instance.SphereCreateParent.GetChild(0).GetComponent<LevelSphere>().SphereType !=
                        LevelSphere.Type.nomal) return;
                    DataManager.setGravityNum();
                    Config.Instance.CreateTypeSphere(LevelSphere.Type.gravity);
                }
                else
                {
#if UNITY_EDITOR
                    DataManager.setGravityNum(true);
#elif UNITY_ANDROID
                    if (AdManager.Instance.IsRewardedAvailable())
                    {
                        if (AdManager.Instance.IsRewardedAvailable())
                        {
                            AdManager.OnRewardedAdRewardedEvent -= null; 
                            AdManager.OnRewardedAdRewardedEvent += tag =>
                            {
                                DataManager.setGravityNum(true);
                                SetFunctionNum(target.name, DataManager.getGravityNum);
                            };
                            AdManager.Instance.ShowRewardedWithTag();
                        }
                    }
#endif
                }

                SetFunctionNum(target.name, DataManager.getGravityNum);
                break;
            case "Btn_with_draw":
                PanelMgr.GetInstance.ShowPanel(PanelName.WithdrawPanel);
                break;
        }
    }

    protected override void OnDown(Transform target)
    {
        switch (target.name)
        {
            case "Btn_DragArea":
                SetSphere(true);
                break;
        }
    }

    protected override void OnUp(Transform target)
    {
        switch (target.name)
        {
            case "Btn_DragArea":
                //SetSphere(false);
                break;
        }
    }

    private void SetFunctionNum(string name, int num)
    {
        TMP_Text Num = skinTrs.Find("DownBoder/" + name + "/Num").GetComponent<TMP_Text>();
        Transform Video = skinTrs.Find("DownBoder/" + name + "/Video");
        bool isShow = num > 0;
        Num.text = num.ToString();
        Video.gameObject.SetActive(!isShow);
        Num.gameObject.SetActive(isShow);
    }

    private void CloseFinger()
    {
        OpenFinger(false);
        DOTween.Kill("Finger");
        DOTween.Kill("Finger2");
    }

    private void GenerateText()
    {
        int max = 6;
        for (int i = 0; i < max; i++)
        {
            ID += UnityEngine.Random.Range(0, 10).ToString();
        }

        tips = "恭喜ID<color=#62EE66>" + ID + "</color>用户成功提现100元";
    }

    private void Tips()
    {
        if (NetworkHelper.checkNetwork())
        {
            int time = UnityEngine.Random.Range(10, 30);
            GenerateText();
            broadcast.DOScaleY(1, 0.5f).OnComplete(() =>
            {
                broadcast_Text.text = tips;

                broadcast_Text.transform.DOLocalMoveX(With, 7f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    broadcast.DOScaleY(0, 0.5f);
                    broadcast_Text.transform.localPosition = new Vector3(-206, 0, 0);
                    ID = null;
                    Invoke("Tips", time);
                });
            });
        }
    }

    //红包抖动
    private void WithDraw()
    {
        m_WithDraw.DORotate(new Vector3(0, 0, 10), 0.1f).OnComplete(() =>
        {
            m_WithDraw.DORotate(new Vector3(0, 0, 0), 0.1f).OnComplete(() =>
            {
                m_WithDraw.DORotate(new Vector3(0, 0, -10), 0.1f).OnComplete(() =>
                {
                    m_WithDraw.DORotate(new Vector3(0, 0, 0), 0.1f).OnComplete(() =>
                    {
                        Invoke("WithDraw", 1.5f);
                    });
                });
            });
        });
    }

    private void RemoveClassSphere()
    {
        if (UnityUtils.Instance.IsFastClick(800))
        {
            return;
        }

        if (!PlayerPrefs.HasKey("diyiciqingchu"))
        {
            isRemoveClassSphere = true;
            PlayerPrefs.SetInt("diyiciqingchu", 1);
            RemoveObjTips.gameObject.SetActive(true);
            StartCoroutine(WaitTimeClose());
        }
        else
        {
#if UNITY_EDITOR
            isRemoveClassSphere = true;
#else
            if (AdManager.Instance.IsRewardedAvailable())
            {
                AdManager.OnRewardedAdRewardedEvent -= null;
                AdManager.OnRewardedAdRewardedEvent += tag =>
                {
                    if(tag=="MainPanel"){
                        ThreadManager.Instance.runOnMainThread(() =>
                        {
                            ThreadManager.Instance.runOnMainThread(() => { isRemoveClassSphere = true; });
                        });
                    }
                 
                };
                AdManager.Instance.ShowRewardedWithTag("MainPannel");
            }
            else
            {
                LogicMgr.GetInstance.GetLogic<LogicTips>().AddTips("Please try again later");
            }
#endif
        }
    }

    IEnumerator WaitTimeClose()
    {
        yield return new WaitForSeconds(2f);
        RemoveObjTips.gameObject.SetActive(false);
    }

    private void BoomEffet(Transform fen)
    {

        if (fen.GetComponent<LevelSphere>() != null)
        {

            int num = (int)fen.GetComponent<LevelSphere>().sphereNum;
            fen.Find("zha/" + num).GetComponent<ParticleSystem>().Play();
            StartCoroutine(WaitBoom(fen.gameObject));
        }
    }
    IEnumerator WaitBoom(GameObject ga)
    {
        yield return new WaitForSeconds(0.2f);
        int i = 1;
        int di = 2;
        if (ga.GetComponent<LevelSphere>() != null)
        {
            var fen = ga.GetComponent<LevelSphere>().SphereNum;
            if (fen < 2048)
            {

                while (di != fen)
                {
                    di *= 2;
                    i++;
                }
            }
            else
            {
                i = 10;
            }
            DataManager.addScore(i);
            m_Score.text = DataManager.getScore.addUnit() + "分";
            Destroy(ga);
            isOpenBoom = true;
        }
    }

    #endregion
}