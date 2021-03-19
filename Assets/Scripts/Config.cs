using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Config : SingletonGetMono<Config>
{
    public int m_Result;
    public bool isMerga;
    public bool isRangeResult;
    internal Transform SphereParent { get; set; }
    internal Transform SphereResultParent { get; set; }
    internal Transform SphereCreateParent { get; set; }

    internal Action<int> QQMergaFront;
    internal Action QQMergaDone;
    internal Action LevelUp;
    internal Action AddLevelFront;
    internal Action AddLevelDone;
    internal Action JudgeLevelUp;
    internal Action SetInfo;
    internal Action<double> RefreshMoney;
    internal Action RetryEvent;
    internal Action<bool> isWipes;
    internal Action CreatRed;
    internal Action showRedBag;
    internal bool isDown { get; set; }

    internal bool isExistsFile => IOperate.Instance.isExistsFile("Config.txt");


    internal bool hasUniversal = false;

    internal bool m_First => PlayerPrefs.GetInt("First") == 1;

    internal void SetFirst()
    {
        PlayerPrefs.SetInt("First", PlayerPrefs.GetInt("First") + 1);
    }

    internal int getLevelScore => (int) Mathf.Pow(2, DataManager.getLevelScore + 8);

    internal bool isCanSave { get; set; }

    private int[] m_StartRange = {40, 30, 20, 10};

    private int[] m_GetStartIndex
    {
        get
        {
            int min = DataManager.getLevelScore - 2;
            min = min < 1 ? 1 : min;
            List<int> temp = new List<int>();
#if UNITY_EDITOR
            for (int i = 0; i < 10; i++)
#else
            for (int i = 0 ; i<4 ; i++)
#endif
                temp.Add(getPowBy(min + i));
            return temp.ToArray();
        }
    }

    private int getPowBy(int temp)
    {
        return (int) Mathf.Pow(2, temp);
    }

    internal void SetFreeZeAll(bool isTrue)
    {
        foreach (Transform item in SphereParent)
        {
            if (item.gameObject.activeSelf)
            {
                Rigidbody2D rigidbody = item.GetComponent<Rigidbody2D>();
                if (rigidbody != null)
                {
                    rigidbody.constraints =
                        isTrue ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
    }

    internal bool isOpenLottery { get; set; }
    internal bool isOpenCheckpoint { get; set; }

    internal bool isLevelShow { get; set; }

    internal bool isVIbrate = true;

    internal void CreateDefute()
    {
        int value = m_GetStartIndex[isRangeResult ? getResult(m_StartRange, 100) : m_Result];
        LevelSphere levelSphere;
        if (SphereCreateParent.childCount != 0)
        {
            levelSphere = SphereCreateParent.GetChild(0).GetComponent<LevelSphere>();
            levelSphere.anchoredPosition3D(new Vector3(0, -100, 0)).localScale(1);
        }
        else
            levelSphere = CreateSphere(SphereCreateParent, value, new Vector3(0, -100, 0));
        
        float temp = Mathf.Log(levelSphere.SphereNum, 2);
        levelSphere.anchoredPosition3D(new Vector3(0, temp > 7 ? -100 - (temp - 7) * 10 : -100, 0));
        levelSphere.m_Circle.enabled = false;
    }

    internal void CreateTypeSphere(LevelSphere.Type type = LevelSphere.Type.nomal)
    {
        LevelSphere levelSphere;
        if (SphereCreateParent.childCount != 0)
        {
            levelSphere = SphereCreateParent.GetChild(0).GetComponent<LevelSphere>();
            levelSphere.SphereType = type;
            levelSphere.anchoredPosition3D(new Vector3(0, -100, 0)).localScale(1);
        }
        else
            levelSphere = CreateSphere(SphereCreateParent, 0, new Vector3(0, -100, 0), type);

        levelSphere.m_Circle.enabled = false;
    }


    private int id;

    internal LevelSphere CreateSphere(Transform parent, double value, Vector2 pos, LevelSphere.Type type = LevelSphere.Type.nomal)
    {
        LevelSphere Clone = ObjectPool.Instance.Spawn<LevelSphere>("m_Sphere", parent);

        Clone.m_Rigidbody.constraints = ~RigidbodyConstraints2D.FreezePositionX;
        Clone.SphereType = type;
        if (type == LevelSphere.Type.nomal)
            Clone.SphereNum = (float) value;
        id++;
        Clone.id = id;

        Clone.anchoredPosition3D(pos).localScale(1);
        Clone.name = "Sphere_" + value;

        return Clone;
    }

    internal void CreateLevel()
    {
        LevelSphere sphere;
        if (SphereResultParent.childCount != 0)
        {
            sphere = SphereResultParent.GetChild(0).GetComponent<LevelSphere>();
            sphere.SphereNum = getLevelScore;
        }
        else
        {
            sphere = CreateSphere(SphereResultParent, getLevelScore, new Vector3(0, -100, 0));
            Destroy(sphere.GetComponent<Rigidbody2D>());
            Destroy(sphere.GetComponent<CircleCollider2D>());
        }

        sphere.m_Rect.sizeDelta = Vector2.one * 150;
    }

    internal void Save()
    {
        if (isCanSave)
        {
            DataManager.getSphereInfo = new List<SphereInfo>();
            foreach (Transform item in SphereParent)
            {
                if (item.gameObject.activeSelf)
                {
                    SphereInfo info = new SphereInfo();
                    info.id = item.transform.GetComponent<LevelSphere>().id;
                    info.pos = new double[] {item.anchoredPosition3D().x, item.anchoredPosition3D().y};
                    info.num = item.transform.GetComponent<LevelSphere>().SphereNum;
                    DataManager.addSphereInfo(info);
                }
            }
        }
    }

    internal int getResult(int[] rate, int total)
    {
        int r = Random.Range(1, total + 1);
        int t = 0;
        for (int i = 0; i < rate.Length; i++)
        {
            t += rate[i];
            if (r < t) return i;
        }

        return 0;
    }

    internal List<string> mList = new List<string>();
}