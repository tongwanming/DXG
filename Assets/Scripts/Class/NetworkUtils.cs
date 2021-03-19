using UnityEngine;

public class NetworkHelper : MonoBehaviour
{
    /// <summary>
    /// 检测网络状态 true有网 false没网
    /// </summary>
    /// <returns></returns>
    public static bool checkNetwork()
    {
        return !(Application.internetReachability == NetworkReachability.NotReachable);
    }
}