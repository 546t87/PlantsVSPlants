using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneManage : MonoBehaviour
{
    //��������
    public GameObject endPanel;//��������

    public AudioSource audioSourcs;//��Ч����Դ
    public AudioClip endMusic;//��������Ч

    public AudioSource BGMAudioSourcs;//��������Դ
    public AudioClip BGM;//��������������

    void Start()
    {
        Time.timeScale = 1;
        EndMusic();//��������
        endPanel.SetActive(true);//չʾ��������
        if (PlayerPrefs.GetInt("win") == 1)//���1��ʤ
        {
            endPanel.transform.GetChild(3).GetComponent<Text>().text = "���1���ʤ��!";//�޸��ı�
            endPanel.transform.GetChild(6).localPosition = new Vector3(-350, 0, 0);//���ý𽱱�
            endPanel.transform.GetChild(7).localPosition = new Vector3(350, 0, 0);//����������
            endPanel.transform.GetChild(7).localScale = new Vector3(-1.5f, 1.5f, 1f);
        }
        else
        {
            endPanel.transform.GetChild(3).GetComponent<Text>().text = "���2���ʤ��!";//�޸��ı�
            endPanel.transform.GetChild(6).localPosition = new Vector3(350, 0, 0);//���ý𽱱�
            endPanel.transform.GetChild(6).localScale = new Vector3(-1.5f, 1.5f, 1f);
            endPanel.transform.GetChild(7).localPosition = new Vector3(-350, 0, 0);//����������
        }
    }

    //���Ž����������
    public void EndMusic()
    {
        audioSourcs.clip = endMusic;
        audioSourcs.volume = PlayerPrefs.GetFloat("soundEffectVolume");
        audioSourcs.Play();

        BGMAudioSourcs.clip = BGM;
        BGMAudioSourcs.volume = PlayerPrefs.GetFloat("musicVolume");
        BGMAudioSourcs.Play();
    }

    //���¿�ʼ��Ϸ
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    //�������˵�
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
