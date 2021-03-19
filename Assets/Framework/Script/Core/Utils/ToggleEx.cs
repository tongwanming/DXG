using UnityEngine;
using UnityEngine. UI;

public class ToggleEx : Toggle
{
    public bool InChildOn
    {
        get { return isOn; }
        set
        {
            isOn = value;
            graphic. GetComponentInChildren<Transform>(true). gameObject. SetActive(isOn);
        }
    }

    public bool CheckItemShow = false;//是否开启 点击选项按钮总是回调

    public void SetChildSprite (Sprite _image)
    {
        transform. Find("Background/Checkmark/Image"). GetComponent<Image>(). sprite = _image;
    }
#if UNITY_EDITOR
    protected override void OnValidate ()
    {
        if (CheckItemShow)
        {
            graphic. GetComponentInChildren<Transform>(true). gameObject. SetActive(isOn);
        }
    }
#endif
}
