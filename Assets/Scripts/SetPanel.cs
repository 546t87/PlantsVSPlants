using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel : MonoBehaviour
{
    public GameObject music;//���ֻ������
    public GameObject soundEffect;//��Ч�������
    public Slider musicSlider;//���ֻ�����
    public Slider soundEffectSlider;//��Ч������

    private void Start()
    {
        //�����ı���Ϣ�ͻ�����λ��
        music.transform.GetChild(4).GetComponent<Text>().text = (SoundManage.instance.musicVolume * 100).ToString();
        musicSlider.value = SoundManage.instance.musicVolume * 100;
        soundEffect.transform.GetChild(4).GetComponent<Text>().text = (SoundManage.instance.soundEffectVolume * 100).ToString();//�����ı���Ϣ
        soundEffectSlider.value = SoundManage.instance.soundEffectVolume * 100;
    }

    //�����������
    public void updateMusic(float value)
    {
        music.transform.GetChild(4).GetComponent<Text>().text = value.ToString();//�����ı���Ϣ
        SoundManage.instance.audioSourcs.volume = value/100.0f;//��������
        SoundManage.instance.musicVolume = value/100.0f;
        PlayerPrefs.SetFloat("musicVolume", value / 100.0f);
    }

    //��Ч�������
    public void updateSoundRffect(float value)
    {
        soundEffect.transform.GetChild(4).GetComponent<Text>().text = value.ToString();//�����ı���Ϣ
        SoundManage.instance.P1_aduio.volume = value / 100.0f;//��������
        SoundManage.instance.P2_aduio.volume = value / 100.0f;//��������
        SoundManage.instance.soundEffectVolume = value/100.0f;
        PlayerPrefs.SetFloat("soundEffectVolume", value / 100.0f);
    }
}
