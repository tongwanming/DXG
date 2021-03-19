using System.Collections.Generic;
using UnityEngine;

//public class ObjectPool : SingletonGetMono<ObjectPool>
//{
//    //金币对象池
//    private Queue<GameObject> ObjPool = new Queue<GameObject>();
//    private Queue<GameObject> ObjPool2 = new Queue<GameObject>();
//    ////特效对象池
//    //private Queue<GameObject> IMjPool = new Queue<GameObject>();
//    //private Queue<GameObject> IMjPool2 = new Queue<GameObject>();

//    public GameObject Fat2;
//    public GameObject showPre;
//    public GameObject showPre2;

//    //特效
//    public GameObject ImshowPre;
//    public GameObject ImshowPre2;
//    public int PoolCount = 10;
//    private Transform m_Transform;

//    private void Start ()
//    {
//        m_Transform = showPre. transform;
//    }

//    void Inte ()
//    {
//        for (int i = 0 ; i < PoolCount ; i++)
//        {
//            var shadwos = Instantiate(showPre);
//            shadwos. GetComponent<Transform>(). SetParent(transform);
//            //shadwos.transform.SetParent(transform);

//            BackPool(shadwos);
//        }
//    }
//    void Inte2 ()
//    {
//        for (int i = 0 ; i < PoolCount ; i++)
//        {
//            var shadwos = Instantiate(showPre2);
//            shadwos. GetComponent<Transform>(). SetParent(transform);
//            //shadwos.transform.SetParent(transform);
//            BackPool2(shadwos);
//        }
//    }
//    //void Inte3()
//    //{

//    //    for (int i = 0; i < PoolCount; i++)
//    //    {
//    //        Fat2 = GameObject.Find("Scene_Main(Clone)");
//    //        var shadwos = Instantiate(ImshowPre);
//    //        shadwos.GetComponent<Transform>().SetParent(Fat2.GetComponent<Transform>());
//    //        //shadwos.transform.SetParent(transform);

//    //        BackPool(shadwos);
//    //    }
//    //}
//    //void Inte4()
//    //{
//    //    Fat2 = GameObject.Find("Scene_Main(Clone)");
//    //    for (int i = 0; i < PoolCount; i++)
//    //    {
//    //        var shadwos = Instantiate(ImshowPre2);
//    //        shadwos.GetComponent<Transform>().SetParent(Fat2.GetComponent<Transform>());
//    //        //shadwos.transform.SetParent(transform);

//    //        BackPool2(shadwos);
//    //    }

//    //}

//    /// <summary>
//    /// 金币
//    /// </summary>
//    /// <param name="obj"></param>
//    public void BackPool (GameObject obj)
//    {

//        obj. transform. position = m_Transform. transform. position;
//        //obj. transform. localEulerAngles = new Vector3(90, 0, 0);
//        obj. GetComponent<Rigidbody>(). velocity = new Vector3(0, 0, 0);
//        obj. transform. rotation = Quaternion. Euler(90, 0, 0);
//        obj. SetActive(false);
//        ObjPool. Enqueue(obj);
//    }

//    public GameObject ExitPool ()
//    {
//        if (ObjPool. Count == 0)
//        {
//            Inte();
//        }
//        var output = ObjPool. Dequeue();
//        output. SetActive(true);
//        return output;
//    }


//    /// <summary>
//    /// 美元
//    /// </summary>
//    /// <param name="obj"></param>
//    public void BackPool2 (GameObject obj)
//    {
//        obj. transform. position = m_Transform. transform. position;
//        //obj. transform. localEulerAngles = new Vector3(90, 0, 0);
//        obj. GetComponent<Rigidbody>(). velocity = new Vector3(0, 0, 0);
//        obj. transform. rotation = Quaternion. Euler(90, 0, 0);
//        obj. SetActive(false);
//        ObjPool2. Enqueue(obj);
//    }

//    public GameObject ExitPool2 ()
//    {
//        if (ObjPool2. Count == 0)
//        {
//            Inte2();
//        }
//        var output = ObjPool2. Dequeue();
//        output. SetActive(true);
//        return output;
//    }
//    /// <summary>
//    /// 
//    /// </summary>
//    /// <param name="obj"></param>
//    //public void BackPool3(GameObject obj)
//    //{
//    //    obj.SetActive(false);
//    //    ObjPool.Enqueue(obj);
//    //}

//    //public GameObject ExitPool3()
//    //{
//    //    if (ObjPool.Count == 0)
//    //    {
//    //        Inte3();
//    //    }
//    //    var output = IMjPool.Dequeue();
//    //    output.SetActive(true);
//    //    return output;
//    //}
//    //public void BackPool4(GameObject obj)
//    //{
//    //    obj.SetActive(false);
//    //    ObjPool.Enqueue(obj);
//    //}


//    //public GameObject ExitPool4()
//    //{
//    //    if (ObjPool.Count == 0)
//    //    {
//    //        Inte4();
//    //    }
//    //    var output = IMjPool2.Dequeue();
//    //    output.SetActive(true);
//    //    return output;
//    //}
//}

public class ObjectPool : SingletonGetMono<ObjectPool> {
    public Dictionary<string, List<GameObject>> poolsDict = new Dictionary<string, List<GameObject>>();
    //取出物体
    //public T Spawn<T> (string name, Transform parent)
    //{
    //    //如果没有这个类型的池子就创建一个
    //    if (!poolsDict. ContainsKey(name))
    //    {
    //        poolsDict. Add(name, new List<GameObject>());
    //    }

    //    //得到池子
    //    List<GameObject> ObjList;
    //    poolsDict. TryGetValue(name, out ObjList);

    //    //在池子里寻找被隐藏的游戏物体
    //    GameObject go = null;
    //    foreach (var obj in ObjList)
    //    {
    //        if (!obj. activeSelf)
    //        {
    //            go=obj;
    //            LogUtil.Error("测试===",go.name);
    //        }
    //    }

    //    if (go==null)//不存在隐藏的游戏物体
    //    {
    //        go=Instantiate(Resources. Load<GameObject>("Prefabs/"+name));
    //        ObjList. Add(go);
    //    }
    //    else//存在隐藏的游戏物体
    //    {
    //        go. SetActive(true);
    //    }
    //    //go. transform. localEulerAngles(Vector3. zero);
    //    go. transform. SetParent(parent);
    //    go. name=go. name. Replace("(Clone)", "");
    //    return go. GetComponent<T>();
    //}

    public T Spawn<T>(string name, Transform parent) {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/" + name));
        go.transform.SetParent(parent);
        go.name = go.name.Replace("(Clone)", "");
        return go.GetComponent<T>();
    }

    //回收物体
    //public void Unspawn (GameObject go)
    //{
    //    foreach (List<GameObject> list in poolsDict. Values)
    //    {
    //        if (list. Contains(go)&&go. activeSelf)
    //        {
    //            go. SetActive(false);
    //            if (go.GetComponent<ClassSphere>()!=null)
    //            {
    //                go.GetComponent<RectTransform>().anchoredPosition3D(0,-100,0);
    //            }
    //        }
    //    }
    //}

    public void Unspawn(GameObject go) {
        Destroy(go);
    }
    //销毁物体
    //public void DestorySpawn (GameObject go)
    //{
    //    foreach (List<GameObject> list in poolsDict. Values)
    //    {
    //        if (list. Contains(go)&&go. activeSelf)
    //        {
    //            list. Remove(go);
    //            Destroy(go);
    //        }
    //    }
    //}

    public void DestorySpawn(GameObject go) {
        Destroy(go);
    }

    //销毁池子
    public void ClearPool(string name) {
        if (poolsDict.ContainsKey(name)) {
            foreach (GameObject go in poolsDict[name]) {
                Destroy(go);
            }

            poolsDict.Remove(name);
        }
    }

    public Queue<MergeEvent> MergeEvents = new Queue<MergeEvent>();

    internal bool Cando = true;


    private void Update() {
        if (MergeEvents.Count > 0 && Cando) {
            //Cando=false;
            MergeEvent _MergeEvent = MergeEvents.Dequeue();
            if (_MergeEvent.SphereA != null && _MergeEvent.SphereB != null) {
                _MergeEvent.SphereA.RefreshSphere(_MergeEvent.SphereB);
            }

            if (Config.Instance.mList.Contains(_MergeEvent.SphereA.id + "_" + _MergeEvent.SphereB.id)) {
                Config.Instance.mList.Remove(_MergeEvent.SphereA.id + "_" + _MergeEvent.SphereB.id);
            }

            if (Config.Instance.mList.Contains(_MergeEvent.SphereB.id + "_" + _MergeEvent.SphereA.id)) {
                Config.Instance.mList.Remove(_MergeEvent.SphereB.id + "_" + _MergeEvent.SphereA.id);
            }
        }
    }
}