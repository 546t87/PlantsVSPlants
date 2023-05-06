using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicManage : MonoBehaviour
{
    //����
    public static MenuMusicManage instance;
    public AudioSource audioSourcs;//����Դ
    public AudioClip bgm;//BGM
    public float musicVolume;//BGM������С
    public float soundEffectVolume;//��Ч������С

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //���ó�ʼ����
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.7f);
        soundEffectVolume = PlayerPrefs.GetFloat("soundEffectVolume", 0.7f);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("soundEffectVolume", soundEffectVolume);
        //����bgm
        audioSourcs.clip = bgm;
        audioSourcs.volume = musicVolume;
        audioSourcs.Play();
    }
}
