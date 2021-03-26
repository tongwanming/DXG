using System;
using System. Collections;
using System. IO;
using System. Text;
using UnityEngine;

public class IOperate : Singleton<IOperate>
{
    string path;
// #if UNITY_ANDROID
//     private string path = Application. persistentDataPath;
// #elif UNITY_STANDALONE_WIN 
//     private string path =Application.dataPath;
// #endif


    internal bool isExistsFile (string _name)
    {
        return File. Exists(path + "//" + _name);
    }

    #region 创建文件
    /// <summary>创建文件</summary>
    /// <param name="path">路径</param>
    /// <param name="name">文件名</param>
    /// <param name="Data">数据</param>
    public void CreateFile (string Data, string name = "",string _path = "")
    {
        try
        {
            path += _path;
            if (name != "")
            {
                if (!Directory. Exists(path))
                {
                    Directory. CreateDirectory(path);
                }
            }
            FileStream fs = new FileStream(name == "" ? path : path + "//" + name, FileMode. Create, FileAccess. Write);
            byte [] bs = Encoding. UTF8. GetBytes(Data);
            fs. Write(bs, 0, bs. Length);
            fs. Close();
        }
        catch (Exception ex)
        {
            Debug. Log(ex. Message);
        }
    }
    #endregion

    #region 读取文件
    /// <summary>读取文件</summary>
    /// <param name="path">路径</param>
    /// <param name="name">名称</param>
    public ArrayList ReadFile (string name = "", string _path = "")
    {
        StreamReader sr;
        path += _path;
        try
        {
            sr = File. OpenText(name == "" ? path : path + "//" + name);
        }
        catch (Exception ex)
        {
            Debug. Log(ex. Message);
            return null;
        }
        string line;
        ArrayList al = new ArrayList();
        while ((line = sr. ReadLine()) != null)
        {
            al. Add(line);
        }

        sr. Dispose();
        return al;
    }

    public ArrayList ResourcesFile (string name)
    {
        TextAsset text = Resources. Load(name) as TextAsset;
        ArrayList al = new ArrayList();
        string [] temp = text. ToString(). Replace("\r\n", "!"). Split('!');
        for (int i = 0 ; i < temp. Length ; i++)
        {
            al. Add(temp [ i ]);
        }
        return al;
    }
    #endregion

}
