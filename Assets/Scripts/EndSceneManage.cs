using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneManage : MonoBehaviour
{
    //结束界面
    public GameObject endPanel;//结束界面

    public AudioSource audioSourcs;//音效声音源
    public AudioClip endMusic;//结束的音效

    public AudioSource BGMAudioSourcs;//音乐声音源
    public AudioClip BGM;//结束场景的音乐

    void Start()
    {
        Time.timeScale = 1;
        EndMusic();//播放音乐
        endPanel.SetActive(true);//展示结束界面
        if (PlayerPrefs.GetInt("win") == 1)//玩家1获胜
        {
            endPanel.transform.GetChild(3).GetComponent<Text>().text = "玩家1获得胜利!";//修改文本
            endPanel.transform.GetChild(6).localPosition = new Vector3(-350, 0, 0);//设置金奖杯
            endPanel.transform.GetChild(7).localPosition = new Vector3(350, 0, 0);//设置银奖杯
            endPanel.transform.GetChild(7).localScale = new Vector3(-1.5f, 1.5f, 1f);
        }
        else
        {
            endPanel.transform.GetChild(3).GetComponent<Text>().text = "玩家2获得胜利!";//修改文本
            endPanel.transform.GetChild(6).localPosition = new Vector3(350, 0, 0);//设置金奖杯
            endPanel.transform.GetChild(6).localScale = new Vector3(-1.5f, 1.5f, 1f);
            endPanel.transform.GetChild(7).localPosition = new Vector3(-350, 0, 0);//设置银奖杯
        }
    }

    //播放结算界面音乐
    public void EndMusic()
    {
        audioSourcs.clip = endMusic;
        audioSourcs.volume = PlayerPrefs.GetFloat("soundEffectVolume");
        audioSourcs.Play();

        BGMAudioSourcs.clip = BGM;
        BGMAudioSourcs.volume = PlayerPrefs.GetFloat("musicVolume");
        BGMAudioSourcs.Play();
    }

    //重新开始游戏
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    //返回主菜单
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
