using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        
        DataManager.setCreateTime();
        DataManager.setLoginTime();
        Config.Instance.SetFirst();
        if (!Config.Instance.isExistsFile)
        {
            LevelPlayerInfo playerInfo = new LevelPlayerInfo();
            playerInfo.level = 0;
            playerInfo.score = 0;
            playerInfo.maxScore = 0;
            playerInfo.listSphereInfo = new List<SphereInfo>();
            DataManager.saveInfo(playerInfo);
        }

        SceneMgr.GetInstance.SwitchingScene(SceneType.SplashPanel);
    }

    private void OnApplicationPause(bool focus)
    {
        if (focus)
        {
            Config.Instance.Save();
        }
    }

    private void OnApplicationQuit()
    {
        Config.Instance.Save();
    }
}