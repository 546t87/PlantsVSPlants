using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManage : MonoBehaviour
{
    public GameObject MenuPanel;//主界面
    public GameObject setPanel;//设置界面
    public GameObject helpPanelBG;//帮助界面背景
    public GameObject[] helpPanel;//帮助界面
    public int nowHelpPanel;//当前停留在帮助界面哪一页

    private void Awake()
    {
        //设置分辨率，不全屏
        Screen.SetResolution(1920, 1080, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(true);
        setPanel.SetActive(false);
        for (int i = 0; i < helpPanel.Length; i++)
            helpPanel[i].SetActive(false);
        helpPanelBG.SetActive(false);
        nowHelpPanel = 0;
    }

    //开始游戏
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    //打开帮助界面
    public void Help()
    {
        helpPanelBG.SetActive(true);
        helpPanel[nowHelpPanel].SetActive(true);
    }

    //打开帮助界面下一页
    public void HelpNext()
    {
        if(nowHelpPanel + 1 < helpPanel.Length)
        {
            helpPanel[nowHelpPanel].SetActive(false);
            nowHelpPanel++;
            helpPanel[nowHelpPanel].SetActive(true);
        }
    }

    //打开帮助界面上一页
    public void HelpLast()
    {
        if (nowHelpPanel - 1 >= 0)
        {
            helpPanel[nowHelpPanel].SetActive(false);
            nowHelpPanel--;
            helpPanel[nowHelpPanel].SetActive(true);
        }
    }

    //打开设置界面
    public void Set()
    {
        setPanel.SetActive(true);
    }

    //返回主界面
    public void BackMenu()
    {
        setPanel.SetActive(false);
        helpPanel[nowHelpPanel].SetActive(false);
        helpPanelBG.SetActive(false);
    }

    //退出游戏
    public void Quit()
    {
        Application.Quit();
    }
}
