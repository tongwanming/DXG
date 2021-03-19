﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildTools : EditorWindow
{
    private bool isRelease;
    private bool isUpBigVersion;
    private bool isSetting;
    private string strCompanyName;
    private string strProductName;
    private string strApplicationIdentifier;
    private string strName;
    private string keyPass;
    private enum VersionType
    {
        One,
        Two,
        Three,
    }
    private VersionType type = VersionType.Three;

    private List<EditorBuildSettingsScene> levels = new List<EditorBuildSettingsScene>( );
    private Color [ ] GetColors
    {
        get
        {
            return new Color [ ] { new Color( 0 , 0.75f , 1 , 1 ) , new Color( 1 , 1 , 0.5f , 1 ) };
        }
    }

    private void OnGUI()
    {
        GUI.backgroundColor = GetColors [ 0 ];
        GUI.contentColor = GetColors [ 1 ];
        GUILayout.Label( "包名：" + PlayerSettings.applicationIdentifier );
        GUILayout.Label( "版本：" + PlayerSettings.bundleVersion );
        GUILayout.Label( "Code：" + PlayerSettings.Android.bundleVersionCode );
        GUILayout.Label( "宏：" + PlayerSettings.GetScriptingDefineSymbolsForGroup( BuildTargetGroup.Android ) );
        GUILayout.BeginVertical( );
        isRelease = EditorGUILayout.Toggle( "正式包" , isRelease );
        if ( isUpBigVersion )
            type = ( VersionType ) EditorGUILayout.EnumPopup( "升级版本类型" , type );
        if ( GUILayout.Button( "设置初始物料" ) )
            isSetting = !isSetting;
        strCompanyName = isSetting ? EditorGUILayout.TextField( "公司名" , strCompanyName ) : PlayerSettings.companyName;
        strProductName = isSetting ? EditorGUILayout.TextField( "产品名" , strProductName ) : PlayerSettings.productName;
        strApplicationIdentifier = isSetting ? EditorGUILayout.TextField( "包名" , strApplicationIdentifier ) : PlayerSettings.applicationIdentifier;
        if ( isSetting )
        {
            if ( GUILayout.Button( "保存物料" ) )
            {
                PlayerSettings.companyName = strCompanyName;
                PlayerSettings.productName = strProductName;
                PlayerSettings.applicationIdentifier = strApplicationIdentifier;
                PlayerSettings.bundleVersion = PlayerSettings.bundleVersion == "0.1" ? "1.0.0" : PlayerSettings.bundleVersion;
            }
        }
        if ( GUILayout.Button( "升级版本号" ) )
            UpVersion( );
        strName = EditorGUILayout.TextField( "后缀名" , strName );
        GUI.backgroundColor = Color.green;
        GUI.contentColor = Color.red;
        GUIStyle gUIStyle = new GUIStyle( GUI.skin.button );
        gUIStyle.fixedHeight = 50;
        gUIStyle.fontSize = 40;
        gUIStyle.fontStyle = FontStyle.Bold;

        if ( GUILayout.Button( "打       包" , gUIStyle ) )
            StartBuild( );
        GUILayout.EndVertical( );

    }

    private void UpVersion()
    {
        string [ ] strVersions = PlayerSettings.bundleVersion.Split( '.' );
        string version ="";
        switch ( type )
        {
            case VersionType.One:
                version = (int.Parse( strVersions [ 0 ] ) + 1) + ".0.0";
                break;
            case VersionType.Two:
                version = strVersions [ 0 ] + "."+( int.Parse( strVersions [ 1 ] ) + 1) + ".0";
                break;
            case VersionType.Three:
                version = strVersions [ 0 ] + "." + strVersions [ 1 ] + "."+( int.Parse( strVersions [ 2 ] ) + 1);
                break;
        }
        PlayerSettings.bundleVersion = version;
        PlayerSettings.Android.bundleVersionCode += 1;

    }

    private void StartBuild()
    {
        foreach ( EditorBuildSettingsScene scene in EditorBuildSettings.scenes )
        {
            if ( !scene.enabled ) continue;
            levels.Add( scene );
        }
        string buildPath = Application.dataPath + "/../Build/";
        string APKName = buildPath + strProductName + ( isRelease ? "_release_" : "_" ) + DateTime.Now.ToString( "MMdd_HHmm" ) + "_"
            + PlayerSettings.Android.bundleVersionCode + "_" + PlayerSettings.bundleVersion +
            ( string.IsNullOrEmpty( strName ) ? "" : "_" + strName ) + ".apk";
        BuildPipeline.BuildPlayer( levels.ToArray( ) , APKName , BuildTarget.Android , BuildOptions.None );
        EditorUtility.RevealInFinder(buildPath);
    }

    [MenuItem( "Tools/打包工具" )]
    public static void showBuildPanel()
    {
        BuildTools bt = GetWindow<BuildTools>( "游戏打包工具" );
        bt.Show( );
    }
    
    [MenuItem( "Tools/PlayerPrefs清除" )]
    public static void deletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
