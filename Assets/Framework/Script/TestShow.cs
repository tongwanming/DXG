using LitJson;
using System;
using System. Collections;
using System. Collections. Generic;
using UnityEngine;

public class TestShow : MonoBehaviour
{
    private void Start ()
    {
        LogicMgr.GetInstance.GetLogic<LogicTips>().AddTips("添加一条提示");
        SceneMgr.GetInstance.SwitchingScene(SceneType.MainPanel);
    }

}
