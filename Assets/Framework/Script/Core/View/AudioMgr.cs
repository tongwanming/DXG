using System;
using Common.Utils;
using UnityEngine;

public class AudioMgr : Singleton<AudioMgr>
{
    public static event Action<bool> SetMusic;

    public Action<bool> SetSound;

    public bool isMute;

    private AudioSource[] m_AllAudio;

    public void GetAudioByName(Transform go, string path, bool isloop = false)
    {
        AudioSource audio = go.GetOrAddComponent<AudioSource>();
        if (isMute)
        {
            audio.Stop();
        }
        else
        {
            path = "Audio/" + path;
            AudioClip clip = Resources.Load<AudioClip>(path);
            audio.clip = clip;
            audio.Play();
            audio.loop = isloop;
        }
    }

    public void SetAudioSound(bool isPlay)
    {
        SetSound(isPlay);
        //SetSound(go, isPlay);
    }

    public void SetAudioMusic(bool isPlay)
    {
        SetMusic(isPlay);
    }

    public void SetStop(Transform go)
    {
        AudioSource audio = go.GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Stop();
        }
    }

    public void SetStop(GameObject go)
    {
        SetStop(go.transform);
    }

    public void GetAudioByName(GameObject go, string path)
    {
        GetAudioByName(go.transform, path);
    }

    public void PlayAudios(string audio = null, float vo = 1)
    {
        // try
        // {
        //     m_AllAudio = Camera.main.GetComponents<AudioSource>();
        //     if (vo != 1)
        //     {
        //         PlayerPrefs.SetFloat("Vo", vo);
        //     }
        //
        //     isMute = PlayerPrefs.GetInt("isMute", 0) == 1 ? true : false;
        //
        //     //Debug.LogError("------>" + Convert.ToBoolean(PlayerPrefs.GetString("isMute")));
        //
        //     if (isMute)
        //     {
        //         for (int i = 1; i < m_AllAudio.Length; i++)
        //         {
        //             m_AllAudio[i].mute = true;
        //         }
        //     }
        //
        //     else
        //     {
        //         for (int i = 1; i < m_AllAudio.Length; i++)
        //         {
        //             m_AllAudio[i].mute = false;
        //             if (m_AllAudio[i].clip.name == audio)
        //             {
        //                 m_AllAudio[i].volume = vo == 1 ? 1 : PlayerPrefs.GetFloat("vo");
        //                 m_AllAudio[i].Play();
        //             }
        //         }
        //     }
        // }
        // catch (Exception e)
        // {
        //     LogUtils.Log(LogUtils.LogColor.Red, e);
        // }
    }

    public void PlayBGAudios(bool isBGMute, float vo = 1)
    {
        m_AllAudio = GameObject.Find("Main Camera").GetComponents<AudioSource>();
        if (isBGMute)
        {
            m_AllAudio[0].mute = PlayerPrefs.GetInt("BGMute") == 1 ? true : false;
            m_AllAudio[0].volume = vo == 1 ? 1 : PlayerPrefs.GetFloat("vo");
            m_AllAudio[0].Play();
        }
        else
        {
            m_AllAudio[0].Stop();
        }
    }
}