using System;
using System. Collections. Generic;
using UnityEngine;
using UnityEngine. UI;

public class LayerMgr : MonoBehaviour
{
    private static LayerMgr mInstance;

    /// <summary>
    /// 获取资源加载实例
    /// </summary>
    /// <returns></returns>
    public static LayerMgr GetInstance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new GameObject("_LayerMgr"). AddComponent<LayerMgr>();
            }
            return mInstance;
        }
    }

    private LayerMgr ()
    {
        mLayerDic = new Dictionary<LayerType, GameObject>();
    }

    public Dictionary<LayerType, GameObject> mLayerDic;
    private GameObject mParent;

    public void LayerInit ()
    {
        mParent = GameObject. Find("Canvas");
        Transform mParentT = mParent. transform;
        if (mParent == null)
        {
            Debug. LogError("场景中不存在Canvas ,请创建！");
        }
        //获取一个枚举的个数长度
        int nums = Enum. GetNames(typeof(LayerType)). Length;
        for (int i = 0 ; i < nums ; i++)
        {
            //获取枚举的索引位置的值
            object obj = Enum. GetValues(typeof(LayerType)). GetValue(i);
            mLayerDic. Add((LayerType)obj, CreateLayerGameObject(obj. ToString(), (LayerType)obj));
        }
    }

    private GameObject CreateLayerGameObject (string name, LayerType type)
    {
        GameObject layer = new GameObject(name);
        layer. transform. parent = mParent. transform;
        Canvas canvas = layer. GetOrAddComponent<Canvas>();
        canvas. overrideSorting=true;
        canvas. sortingOrder=(int)type;
        layer. GetOrAddComponent<GraphicRaycaster>();
        layer. transform. localPosition(Vector3. zero). localEulerAngles(Vector3. zero). localScale(1);
        return layer;
    }

    public void SetLayer (GameObject current, LayerType type)
    {
        if (mLayerDic. Count < Enum. GetNames(typeof(LayerType)). Length)
        {
            LayerInit();
        }
        current. transform. SetParent(mLayerDic [ type ]. transform);
        //Debug.Log(current.name + "===" + current.transform.localScale + "===" + "");

        //Canvas canvas = current. GetOrAddComponent<Canvas>();
        //canvas. overrideSorting=true;
        //canvas. sortingOrder=(int)type;
        //current. GetOrAddComponent<GraphicRaycaster>();

        Canvas [] panelArr = current. GetComponentsInChildren<Canvas>(true);
        foreach (Canvas panel in panelArr)
        {
            panel. sortingOrder+=(int)type;
            Renderer renderer = panel. GetComponent<Renderer>();
            if (renderer!=null)
            {
                renderer. sortingOrder=panel. sortingOrder;
            }
        }


        //UIPanel[] panelArr = current.GetComponentsInChildren<UIPanel>(true);
        //foreach (UIPanel panel in panelArr)
        //{
        //    panel.depth += (int)type;
        //}
    }

    /// <summary>根据面板数组先后顺序设置深度 最后一个panel深度最高</summary>
    public void SetPanelsLayer (List<PanelBase> pbList)
    {
        for (int i = 0 ; i < pbList. Count ; i++)
        {
            pbList [ i ]. transform. SetAsLastSibling();
            pbList [ i ]. skinTrs. SetAsLastSibling();

            //Transform [] panelArr = pbList [ i ]. skin. GetComponentsInChildren<Transform>(true);
            //for (int f = 0 ; f < panelArr. Length ; f++)
            //{
            //    LogUtil. Error("panelArr= ", panelArr [f].name);

            //    panelArr [ f ]. SetAsLastSibling();
            //}
        }
    }
}
/// <summary>
/// 分层类型
/// </summary>
public enum LayerType
{
    /// <summary>场景</summary>
    Scene = 50,
    /// <summary>弹框</summary>
    Panel = 200,
    /// <summary>提示</summary>
    Tips = 400,
    /// <summary>公告层</summary>
    Notice = 1000,
}
