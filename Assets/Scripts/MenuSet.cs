using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSet : MonoBehaviour
{
    public GameObject music;//���ֻ������
    public GameObject soundEffect;//��Ч�������
    public Slider musicSlider;//���ֻ�����
    public Slider soundEffectSlider;//��Ч������

    private void Start()
    {
        //�����ı���Ϣ�ͻ�����λ��
        music.transform.GetChild(4).GetComponent<Text>().text = (MenuMusicManage.instance.musicVolume * 100).ToString();
        musicSlider.value = MenuMusicManage.instance.musicVolume * 100;
        soundEffect.transform.GetChild(4).GetComponent<Text>().text = (MenuMusicManage.instance.soundEffectVolume * 100).ToString();//�����ı���Ϣ
        soundEffectSlider.value = MenuMusicManage.instance.soundEffectVolume * 100;
    }

    //�����������
    public void updateMusic()
    {
        music.transform.GetChild(4).GetComponent<Text>().text = musicSlider.value.ToString();//�����ı���Ϣ
        MenuMusicManage.instance.audioSourcs.volume = musicSlider.value / 100.0f;//��������
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value / 100.0f);
    }

    //��Ч�������
    public void updateSoundRffect()
    {
        soundEffect.transform.GetChild(4).GetComponent<Text>().text = soundEffectSlider.value.ToString();//�����ı���Ϣ
        PlayerPrefs.SetFloat("soundEffectVolume", soundEffectSlider.value / 100.0f);//��������
    }
}
