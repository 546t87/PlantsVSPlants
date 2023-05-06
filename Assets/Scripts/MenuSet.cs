using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSet : MonoBehaviour
{
    public GameObject music;//音乐滑动组件
    public GameObject soundEffect;//音效滑动组件
    public Slider musicSlider;//音乐滑动条
    public Slider soundEffectSlider;//音效滑动条

    private void Start()
    {
        //更新文本信息和滑动条位置
        music.transform.GetChild(4).GetComponent<Text>().text = (MenuMusicManage.instance.musicVolume * 100).ToString();
        musicSlider.value = MenuMusicManage.instance.musicVolume * 100;
        soundEffect.transform.GetChild(4).GetComponent<Text>().text = (MenuMusicManage.instance.soundEffectVolume * 100).ToString();//更新文本信息
        soundEffectSlider.value = MenuMusicManage.instance.soundEffectVolume * 100;
    }

    //音乐组件更新
    public void updateMusic()
    {
        music.transform.GetChild(4).GetComponent<Text>().text = musicSlider.value.ToString();//更新文本信息
        MenuMusicManage.instance.audioSourcs.volume = musicSlider.value / 100.0f;//更新音量
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value / 100.0f);
    }

    //音效组件更新
    public void updateSoundRffect()
    {
        soundEffect.transform.GetChild(4).GetComponent<Text>().text = soundEffectSlider.value.ToString();//更新文本信息
        PlayerPrefs.SetFloat("soundEffectVolume", soundEffectSlider.value / 100.0f);//更新音量
    }
}
