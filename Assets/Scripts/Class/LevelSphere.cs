using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSphere : MonoBehaviour
{
    public float sphereNum;

    public Sprite[] SphereSprite;

    public float SphereNum
    {
        get { return sphereNum; }

        set
        {
            sphereNum = value;
            SetThisValue(value);
        }
    }

    internal enum Type
    {
        nomal,
        redPacket,
        gravity,
        universal,
        Puzzle
    }

    internal Type sphereType = Type.nomal;

    internal Type SphereType
    {
        get { return sphereType; }
        set
        {
            sphereType = value;
            switch (value)
            {
                case Type.nomal:
                    break;
                case Type.redPacket:
                    break;
                case Type.gravity:
                    SetThisValue();
                    m_Rigidbody.mass = 100000;
                    break;
                case Type.universal:
                    SetThisValue();
                    break;
                case Type.Puzzle:
                    SetThisValue();
                    break;
            }
        }
    }

    public int id;
    internal RectTransform m_Rect;

    internal Rigidbody2D m_Rigidbody;

    //[Header("X方向震动幅度")]
    private float X = 0.3f;

    //[Header("Y方向震动幅度")]
    private float Y = 0.2f;

    // [Header("力的大小")]
    //private float Li = 2000;
    internal CircleCollider2D m_Circle;
    private int m_Sizes = 120;
    private bool isCheckAddNum;

    private Image m_Image;

    private int Blood = 1;

    private void OnEnable()
    {
        m_Rect = transform.GetComponent<RectTransform>();
        m_Image = GetComponent<Image>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Circle = GetComponent<CircleCollider2D>();
        m_Rect.anchoredPosition(new Vector2(0, -100)).localEulerAngles(Vector3.zero);
        m_Circle.enabled = true;
        transform.GetComponent<Button>().onClick.AddListener(()=>{
            RemoveObj();
        });
    }

    private float[] whs = {78, 116, 162, 178, 226, 272, 296, 378, 456, 546, 602};

    private Vector2 numToV2
    {
        get
        {
            int index = log2(Convert.ToInt32(SphereNum)) - 1;
            var sp = SphereSprite[index];
            if (m_Circle != null)
            {
                SetCir(whs[index] / 2 - 1);
            }

            PlayerPrefs.SetInt("dijigeQQ", PlayerPrefs.GetInt("dijigeQQ") + 1);
            if (m_Rigidbody != null) m_Rigidbody.mass = index * 2 + 5;

            m_Image.sprite = sp;

            return new Vector2(whs[index], whs[index]);
        }
    }

    int log2(int n)
    {
        int count = 0;
        if (n == 1)
            return 0;

        return 1 + log2(n >> 1);
    }

    private void SetThisValue(float temp)
    {
        switch (sphereType)
        {
            case Type.nomal:
                m_Rect.sizeDelta = numToV2;
                break;
            case Type.redPacket:
                break;
        }
    }

    private void SetThisValue()
    {
        switch (sphereType)
        {
            case Type.gravity:
                m_Image.sprite = Resources.Load<Sprite>("gravity");
                m_Image.color = Color.white;
                m_Rect.sizeDelta = Vector2.one * 160;
                SetCir(160 / 2);
                break;
            case Type.universal:
                m_Image.sprite = Resources.Load<Sprite>("universal");
                m_Image.color = Color.white;
                m_Rect.sizeDelta = Vector2.one * 148;
                SetCir(148 / 2);
                break;
            case Type.Puzzle:
                m_Image.sprite = Resources.Load<Sprite>("sui");
                m_Image.color = Color.white;
                m_Rect.sizeDelta = Vector2.one * 148;
                SetCir(148 / 2);
                break;
        }
    }

    private void SetCir(float radius)
    {
        m_Circle.radius = radius;
        m_Circle.offset = Vector2.zero;
    }

    internal void RefreshSphere(LevelSphere _temp)
    {
        if (SphereNum != _temp.SphereNum)
        {
            ObjectPool.Instance.Cando = true;
            Config.Instance.isMerga = true;
            return;
        }

        SetThisValue(SphereNum *= 2);
        transform.name = transform.name.Split('_')[0] + "_" + SphereNum;

        ObjectPool.Instance.Unspawn(_temp.gameObject);

        ObjectPool.Instance.Cando = false;

        if (SphereNum == 2048)
        {
            AudioMgr.Instance.PlayAudios("08");
        }

        zhatexiao((int) SphereNum);

        if (gameObject != null && gameObject.activeSelf)
            StartCoroutine(Merga((int) SphereNum));
        else
        {
            ObjectPool.Instance.Cando = true;
        }
    }

    private void zhatexiao(int num)
    {
        transform.DOKill();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(0.0F, 0.0F));
        sequence.Append(transform.DOScale(1.1F, 0.4F));
        sequence.Append(transform.DOScale(1.0F, 0.2F));
    }

    IEnumerator Merga(int fen)
    {
        DataManager.compositeBall();
        Config.Instance.QQMergaFront(fen);
        m_Rigidbody.velocity = Vector2.zero;
        yield return new WaitForSecondsRealtime(0.3f);
        ObjectPool.Instance.Cando = true;
        Config.Instance.isMerga = true;
        if (Config.Instance.QQMergaDone != null)
        {
            Config.Instance.QQMergaDone();
        }
    }

    private IEnumerator UnspawnGravity()
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPool.Instance.Unspawn(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("m_Sphere"))
        {
            LevelSphere sphere = collision.transform.GetComponent<LevelSphere>();
            switch (SphereType)
            {
                case Type.nomal:
                    if (sphere != null && sphere.SphereNum == SphereNum && (int) SphereNum < 2048)
                    {
                        if (Config.Instance.mList == null)
                        {
                            return;
                        }
                        if (Config.Instance.mList.Contains(id + "_" + sphere.id) ||
                            Config.Instance.mList.Contains(sphere.id + "_" + id))
                        {
                            return;
                        }
                        if (id > sphere.id && Config.Instance.isMerga)
                        {
                            Config.Instance.isMerga = false;
                            Config.Instance.mList.Add(id + "_" + sphere.id);
                            RefreshSphere(sphere);
                            
                            if (Config.Instance.mList.Contains(id + "_" + sphere.id)) {
                                Config.Instance.mList.Remove(id + "_" + sphere.id);
                            }

                            if (Config.Instance.mList.Contains(sphere.id + "_" + id)) {
                                Config.Instance.mList.Remove(sphere.id + "_" + id);
                            }
                        }
                    }

                    break;
                case Type.redPacket:
                    break;
                case Type.gravity:
                    break;
                case Type.universal:
                    if (Config.Instance.isMerga)
                    {
                        ObjectPool.Instance.MergeEvents.Enqueue(new MergeEvent(this, sphere));
                    }

                    break;
                case Type.Puzzle:
                    //if (Config.Instance.isMerga)
                    //{
                    ObjectPool.Instance.MergeEvents.Enqueue(new MergeEvent(this, sphere));
                    //}
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("m_Sphere"))
        {
            LevelSphere sphere = collision.transform.GetComponent<LevelSphere>();
            switch (SphereType)
            {
                case Type.nomal:

                    //LogUtil.Error("stay  1", "名字-->" + SphereNum + "ID---->" + id + "名字2--->" + sphere.SphereNum + "ID2--------->" + sphere.id);
                    if (sphere.SphereNum == SphereNum && (int) SphereNum < 2048)
                    {
                        if (id >= sphere.id && Config.Instance.isMerga)
                        {
                            //Debug.LogError("接触进入");
                            //Debug.LogError("st222" + "名字1" + "<" + SphereNum + "--" + id + ">" + "名字2" + "<" + sphere.SphereNum + "--" + sphere.id + ">");
                            // ObjectPool.Instance.MergeEvents.Enqueue(new MergeEvent(this, sphere));
                            // ObjectPool.Instance.CheckMerge();
                            RefreshSphere(sphere);
                            
                            if (Config.Instance.mList.Contains(id + "_" + sphere.id)) {
                                Config.Instance.mList.Remove(id + "_" + sphere.id);
                            }

                            if (Config.Instance.mList.Contains(sphere.id + "_" + id)) {
                                Config.Instance.mList.Remove(sphere.id + "_" + id);
                            }
                        }
                    }

                    break;
                case Type.redPacket:
                    break;
                case Type.gravity:
                    break;
                case Type.universal:
                    if (sphere.sphereType == Type.universal)
                    {
                        Destroy(collision.gameObject);
                        Destroy(this);
                    }

                    if (Config.Instance.isMerga)
                    {
                        ObjectPool.Instance.MergeEvents.Enqueue(new MergeEvent(this, sphere));
                    }

                    break;
                case Type.Puzzle:
                    if (sphere.sphereType == Type.Puzzle)
                    {
                        Destroy(collision.gameObject);
                        Destroy(this);
                    }

                    if (Config.Instance.isMerga)
                    {
                        ObjectPool.Instance.MergeEvents.Enqueue(new MergeEvent(this, sphere));
                    }

                    break;
            }
        }

        else if (collision.transform.tag == "Border")
        {
            if (sphereType == Type.gravity)
            {
                StartCoroutine(UnspawnGravity());
            }
        }
        else if (collision.transform.tag == "Death")
        {
            Blood--;
            if (Blood <= 0)
            {
                PanelMgr.GetInstance.ShowPanel(PanelName.RestartPanel, false);
                Blood = 1;
            }
        }
    }


    private void RemoveObj()
    {
        if (MainPanel.isRemoveClassSphere && GetComponent<Collider2D>().isActiveAndEnabled)
        {
            Destroy(gameObject);
            MainPanel.isRemoveClassSphere = false;
        }
    }
}