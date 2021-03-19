using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelPlayerInfo
{
    public int level; //等级
    public int exp; //经验值
    public int maxScore; //最高分数
    public int levelScore; //关卡数
    public int score; //当前分数
    public List<SphereInfo> listSphereInfo; //球的位置
}

[Serializable]
public class SphereInfo
{
    public int id;
    public double[] pos;
    public double num;
}