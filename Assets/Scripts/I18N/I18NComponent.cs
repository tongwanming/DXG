using System;
using Common.Utils;
using I18N;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class I18NComponent : MonoBehaviour
{
    private string LuafileName = "I18N";

    public enum i18NType
    {
        TextMeshProUGUI,
        Text
    }

    public i18NType CurrentType = i18NType.TextMeshProUGUI;


    [SerializeField] private string _key = "";

    private TextMeshProUGUI _text;
    private Text _uiText;
    
    void Awake()
    {
        Search();
    }

    void Start()
    {
        Refresh();
    }

    private void Search()
    {
        if (CurrentType == i18NType.TextMeshProUGUI)
        {
            _text = GetComponent<TextMeshProUGUI>();
            try
            {
                if (_text.text != "" && _key == "") _key = _text.text;
            }
            catch (Exception e)
            {
                LogUtils.Log(e);
            }
        }
        else if (CurrentType == i18NType.Text)
        {
            _uiText = GetComponent<Text>();
            try
            {
                if (_uiText.text != "" && _key == "") _key = _uiText.text;
            }
            catch (Exception e)
            {
                LogUtils.Log(e);
            }
        }
    }

    private void Refresh()
    {
        if (Language.Instance.GetLanguage().ContainsKey(_key))
        {
            string value = Language.Instance.GetLanguage()[_key];
            if (value != "Unknown")
            {
                try
                {
                    if (CurrentType == i18NType.TextMeshProUGUI)
                    {
                        _text.text = value;
                    }
                    else if (CurrentType == i18NType.Text)
                    {
                        _uiText.text = value;
                    }
                }
                catch (Exception e)
                {
                    LogUtils.Log(e);
                }
            }
        }
    }

// #if UNITY_EDITOR
//     void Update()
//     {
//         Search();
//         Refresh();
//     }
// #endif
}