using UnityEngine;

public static class ComponentEx
{

    public static T SeachTrs<T>(this Transform transform, string name) where T : Component
    {
        T[] trs = transform.GetComponentsInChildren<T>(true);
        for (int i = 0; i<trs.Length; i++)
        {
            if (trs[i].name==name)
            {
                return trs[i];
            }
        }

        return null;
    }

    public static T GetOrAddComponent<T>(this Transform transform) where T : Component
    {
        T t = transform.GetComponent<T>();
        if (t==null)
        {
            t=transform.gameObject.AddComponent<T>();
        }

        return t;
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        T t = gameObject.GetComponent<T>();
        if (t==null)
        {
            t=gameObject.AddComponent<T>();
        }

        return t;
    }

    public static T position<T>(this T selfComponent, Vector3 position) where T : Component
    {
        selfComponent.transform.position=position;
        return selfComponent;
    }
    public static Vector3 position<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.position;
    }

    public static T rotation<T>(this T selfComponent, Quaternion rotation) where T : Component
    {
        selfComponent.transform.rotation=rotation;
        return selfComponent;
    }
    public static Quaternion rotation<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.rotation;
    }

    public static T localScale<T>(this T selfComponent, float xyz) where T : Component
    {
        selfComponent.transform.localScale=Vector3.one*xyz;
        return selfComponent;
    }

    public static T localScale<T>(this T selfComponent, float x, float y, float z) where T : Component
    {
        selfComponent.transform.localScale=new Vector3(x, y, z);
        return selfComponent;
    }

    public static T localScale<T>(this T selfComponent, Vector3 scale) where T : Component
    {
        selfComponent.transform.localScale=scale;
        return selfComponent;
    }

    public static T localPosition<T>(this T selfComponent, Vector3 position) where T : Component
    {
        selfComponent.transform.localPosition=position;
        return selfComponent;
    }

    public static Vector3 localPosition<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.localPosition;
    }

    public static T localRotation<T>(this T selfComponent, Quaternion rotation) where T : Component
    {
        selfComponent.transform.localRotation=rotation;
        return selfComponent;
    }

    public static Quaternion localRotation<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.localRotation;
    }

    public static T localEulerAngles<T>(this T selfComponent, Vector3 eulerAngles) where T : Component
    {
        selfComponent.transform.localEulerAngles=eulerAngles;
        return selfComponent;
    }

    public static T localEulerAngles<T>(this T selfComponent, float x, float y, float z) where T : Component
    {
        selfComponent.transform.localEulerAngles=new Vector3(x, y, z);
        return selfComponent;
    }

    public static Vector3 localEulerAngles<T>(this T selfComponent) where T : Component
    {
        return selfComponent.transform.localEulerAngles;
    }

    public static T anchoredPosition3D<T>(this T selfComponent, Vector3 position) where T : Component
    {
        selfComponent.GetComponent<RectTransform>().anchoredPosition3D=position;
        return selfComponent;
    }

    public static T anchoredPosition3D<T>(this T selfComponent, float x, float y, float z) where T : Component
    {
        selfComponent.GetComponent<RectTransform>().anchoredPosition3D=new Vector3(x, y, z);
        return selfComponent;
    }

    public static Vector3 anchoredPosition3D<T>(this T selfComponent) where T : Component
    {
        return selfComponent.GetComponent<RectTransform>().anchoredPosition3D;
    }

    public static T anchoredPosition<T>(this T selfComponent, Vector2 position) where T : Component
    {
        selfComponent.GetComponent<RectTransform>().anchoredPosition=position;
        return selfComponent;
    }

    public static Vector2 anchoredPosition<T>(this T selfComponent) where T : Component
    {
        return selfComponent.GetComponent<RectTransform>().anchoredPosition;
    }
}
