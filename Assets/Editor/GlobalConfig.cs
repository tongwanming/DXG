﻿﻿using UnityEditor;


[InitializeOnLoad]
public class GlobalConfig
{
    static GlobalConfig()
    {
        PlayerSettings.Android.keystorePass = "dxg2021";
        PlayerSettings.Android.keyaliasPass = "dxg2021";
        PlayerSettings.Android.keyaliasName = "dxg2021";
        PlayerSettings.SplashScreen.showUnityLogo = false;
    }
}