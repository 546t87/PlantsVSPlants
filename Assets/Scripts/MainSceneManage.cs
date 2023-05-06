using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManage : MonoBehaviour
{
    public GameObject MenuPanel;//������
    public GameObject setPanel;//���ý���
    public GameObject helpPanelBG;//�������汳��
    public GameObject[] helpPanel;//��������
    public int nowHelpPanel;//��ǰͣ���ڰ���������һҳ

    private void Awake()
    {
        //���÷ֱ��ʣ���ȫ��
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

    //��ʼ��Ϸ
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    //�򿪰�������
    public void Help()
    {
        helpPanelBG.SetActive(true);
        helpPanel[nowHelpPanel].SetActive(true);
    }

    //�򿪰���������һҳ
    public void HelpNext()
    {
        if(nowHelpPanel + 1 < helpPanel.Length)
        {
            helpPanel[nowHelpPanel].SetActive(false);
            nowHelpPanel++;
            helpPanel[nowHelpPanel].SetActive(true);
        }
    }

    //�򿪰���������һҳ
    public void HelpLast()
    {
        if (nowHelpPanel - 1 >= 0)
        {
            helpPanel[nowHelpPanel].SetActive(false);
            nowHelpPanel--;
            helpPanel[nowHelpPanel].SetActive(true);
        }
    }

    //�����ý���
    public void Set()
    {
        setPanel.SetActive(true);
    }

    //����������
    public void BackMenu()
    {
        setPanel.SetActive(false);
        helpPanel[nowHelpPanel].SetActive(false);
        helpPanelBG.SetActive(false);
    }

    //�˳���Ϸ
    public void Quit()
    {
        Application.Quit();
    }
}
