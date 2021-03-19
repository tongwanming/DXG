using UnityEditor;
using UnityEngine;

public class IronSourceMenu
{

   [MenuItem("IronSource/Documentation", false, 0)]
    public static void Documentation()
    {
        Application.OpenURL("https://developers.ironsrc.com/ironsource-mobile/unity/unity-plugin/");
    }

   
    [MenuItem("IronSource/SDK Change Log", false, 1)]
    public static void ChangeLog()
    {
        Application.OpenURL("https://developers.ironsrc.com/ironsource-mobile/unity/sdk-change-log/");
    }


    [MenuItem("IronSource/Integration Manager", false , 2)]
    public static void SdkManagerProd()
    {
        IronSourceDependenciesManager.ShowISDependenciesManager();
    }
}
