using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel : MonoBehaviour
{
    public GameObject music;//音乐滑动组件
    public GameObject soundEffect;//音效滑动组件
    public Slider musicSlider;//音乐滑动条
    public Slider soundEffectSlider;//音效滑动条

    private void Start()
    {
        //更新文本信息和滑动条位置
        music.transform.GetChild(4).GetComponent<Text>().text = (SoundManage.instance.musicVolume * 100).ToString();
        musicSlider.value = SoundManage.instance.musicVolume * 100;
        soundEffect.transform.GetChild(4).GetComponent<Text>().text = (SoundManage.instance.soundEffectVolume * 100).ToString();//更新文本信息
        soundEffectSlider.value = SoundManage.instance.soundEffectVolume * 100;
    }

    //音乐组件更新
    public void updateMusic(float value)
    {
        music.transform.GetChild(4).GetComponent<Text>().text = value.ToString();//更新文本信息
        SoundManage.instance.audioSourcs.volume = value/100.0f;//更新音量
        SoundManage.instance.musicVolume = value/100.0f;
        PlayerPrefs.SetFloat("musicVolume", value / 100.0f);
    }

    //音效组件更新
    public void updateSoundRffect(float value)
    {
        soundEffect.transform.GetChild(4).GetComponent<Text>().text = value.ToString();//更新文本信息
        SoundManage.instance.P1_aduio.volume = value / 100.0f;//更新音量
        SoundManage.instance.P2_aduio.volume = value / 100.0f;//更新音量
        SoundManage.instance.soundEffectVolume = value/100.0f;
        PlayerPrefs.SetFloat("soundEffectVolume", value / 100.0f);
    }
}
