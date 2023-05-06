using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicManage : MonoBehaviour
{
    //音乐
    public static MenuMusicManage instance;
    public AudioSource audioSourcs;//声音源
    public AudioClip bgm;//BGM
    public float musicVolume;//BGM音量大小
    public float soundEffectVolume;//音效音量大小

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //设置初始音量
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.7f);
        soundEffectVolume = PlayerPrefs.GetFloat("soundEffectVolume", 0.7f);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("soundEffectVolume", soundEffectVolume);
        //播放bgm
        audioSourcs.clip = bgm;
        audioSourcs.volume = musicVolume;
        audioSourcs.Play();
    }
}
