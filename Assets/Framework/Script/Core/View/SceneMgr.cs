using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr
{
    #region 初始化

    protected static SceneMgr mInstance;

    public static bool hasInstance
    {
        get { return mInstance != null; }
    }

    public static SceneMgr GetInstance
    {
        get
        {
            if (!hasInstance)
            {
                mInstance = new SceneMgr();
            }

            return mInstance;
        }
    }

    #endregion

    public delegate void OnSwitchingScene(SceneType type);

    /// <summary>
    /// 当更换场景时委派
    /// </summary>
    public OnSwitchingScene OnSwitchingSceneHandler;

    /// <summary>
    /// 存储当前已经实例化的场景
    /// </summary>
    public Dictionary<SceneType, SceneBase> scenes;

    /// <summary>
    /// 当前场景
    /// </summary>
    public SceneBase current;

    /// <summary>
    /// 记录切换数据
    /// </summary>
    private List<SwitchRecorder> switchRecoders;

    /// <summary>
    /// 主场景
    /// </summary>
    private const SceneType mainSceneType = SceneType.MainPanel;

    private SceneMgr()
    {
        scenes = new Dictionary<SceneType, SceneBase>();
        switchRecoders = new List<SwitchRecorder>();
    }

    public void Destroy()
    {
        OnSwitchingSceneHandler = null;

        switchRecoders.Clear();
        switchRecoders = null;

        scenes.Clear();
        scenes = null;
    }

    /// <summary>
    /// 场景切换
    /// </summary>
    /// <param name="sceneType"></param>
    /// <param name="sceneArgs">场景参数</param>
    public void SwitchingScene(SceneType sceneType, params object[] sceneArgs)
    {
        if (current != null)
        {
            if (sceneType == current.type)
            {
                Debug.LogWarning("试图切换场景至当前场景：" + sceneType.ToString());
                return;
            }
        }

        if (sceneType == mainSceneType) //进入主场景，把切换场景记录清空
        {
            switchRecoders.Clear();
        }

        switchRecoders.Add(new SwitchRecorder(sceneType, sceneArgs)); //切换记录
        HideCurrentScene();
        ShowScene(sceneType, sceneArgs);
        if (OnSwitchingSceneHandler != null)
        {
            OnSwitchingSceneHandler(sceneType);
        }
    }

    /// <summary>
    /// 切换至上一个场景
    /// </summary>
    public void SwitchingToPrevScene()
    {
        if (switchRecoders.Count < 2)
        {
            Debug.LogWarning("切换至上一个场景时，没有上一个场景记录！请检查逻辑!");
            return;
        }

        SwitchRecorder sr = switchRecoders[switchRecoders.Count - 2];
        switchRecoders.RemoveRange(switchRecoders.Count - 2, 2); //切换至上一个场景后，记录请除最后一个场景（即当前场景）和上一场景
        SwitchingScene(sr.sceneType, sr.sceneArgs);
    }

    /// <summary>
    /// 打开指定场景
    /// </summary>
    /// <param name="sceneType"></param>
    /// <param name="sceneArgs">场景参数</param>
    private void ShowScene(SceneType sceneType, params object[] sceneArgs)
    {
        if (scenes.ContainsKey(sceneType))
        {
            current = scenes[sceneType];
            current.OnShowing();
            current.OnResetArgs(sceneArgs);
            current.gameObject.SetActive(true);
            current.OnShowed();
        }
        else
        {
            if (sceneType == SceneType.None)
            {
                current = null;
                return;
            }

            GameObject go = new GameObject(sceneType.ToString());
            Type mType = Type.GetType(sceneType.ToString());
            current = go.AddComponent(mType) as SceneBase;
            current.OnInit(sceneArgs);
            scenes.Add(current.type, current);
            current.OnShowing();
            LayerMgr.GetInstance.SetLayer(current.gameObject, LayerType.Scene);
            go.transform.localPosition(Vector3.zero).localRotation(Quaternion.identity).localScale(1);
            current.OnShowed();
        }
    }

    /// <summary>
    /// 关闭当前场景
    /// </summary>
    private void HideCurrentScene()
    {
        if (current != null)
        {
            current.OnHiding();
            //NGUITools.SetActive(current.gameObject, false);
            current.OnHided();
            if (!current.cache)
            {
                scenes.Remove(current.type);
                GameObject.Destroy(current.gameObject);
            }
        }
    }

    /// <summary>
    /// 记录
    /// </summary>
    internal struct SwitchRecorder
    {
        internal SceneType sceneType;
        internal object[] sceneArgs;

        internal SwitchRecorder(SceneType sceneType, params object[] sceneArgs)
        {
            this.sceneType = sceneType;
            this.sceneArgs = sceneArgs;
        }
    }
}