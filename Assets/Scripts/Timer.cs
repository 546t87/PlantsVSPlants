using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject sun;//����Ԥ����
    public int timeState;//��ʱ��״̬��0Ϊδ��ʼ��1Ϊ��ʱ�У�2Ϊ��ͣ�У�3Ϊ��ʱ���
    public long startTime;//��ʼʱ��
    public long nowTime;//��ǰʱ��
    public long stopTime;//��ͣʱ��
    public long endTime;//����ʱ��
    public long lastSunTime;//�ϴ���������ʱ��
    public GameObject slot;//����
    public int[] P1_CD = new int[6];//ֲ����ȴ����ʱ��
    public int[] P2_CD = new int[6];//ֲ����ȴ����ʱ��
    public long[] P1_plantCanPlantTime = new long[6];//ֲ�����ٴ���ֲ��ʱ��
    public long[] P2_plantCanPlantTime = new long[6];//ֲ�����ٴ���ֲ��ʱ��

    //һ����Ϊ10000000  Time = System.DateTime.Now.Ticks;
    private void Start()
    {
        timeState = 0;
        for (int i = 0; i < 2; i++)
        {
            P1_CD[i] = 0;
            P2_CD[i] = 0;
            P1_plantCanPlantTime[i] = 0;
            P2_plantCanPlantTime[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeState == 1)
        {
            //�Զ���������
            nowTime = System.DateTime.Now.Ticks;
            if (nowTime - lastSunTime > 100000000)//ÿ��10���Զ�����һ������
            {
                lastSunTime = nowTime;
                float x = Random.Range(-6.5f, 6.5f);
                float y = Random.Range(-3.5f, 2.3f);
                GameObject s1 = Instantiate(sun, new Vector3(x, y, 0), Quaternion.identity);//��������
                s1.GetComponent<Sun>().SetPlayer(1);
                x = Random.Range(-6.5f, 6.5f);
                y = Random.Range(-3.5f, 2.3f);
                GameObject s2 = Instantiate(sun, new Vector3(x, y, 0), Quaternion.identity);//��������
                s2.GetComponent<Sun>().SetPlayer(2);
            }

            //ֲ����ȴ
            for (int i = 0; i < 6; i++)
            {
                if (nowTime < P1_plantCanPlantTime[i])//δ��ȴ��ɣ��޸ĺ�ɫĻ����С
                    slot.transform.GetChild(0).GetChild(i + 1).GetChild(2).GetComponent<Transform>().localScale
                        = new Vector3(1, (float)(P1_plantCanPlantTime[i] - nowTime) / (float)(P1_CD[i] * 10000000f), 1);
                if (nowTime < P2_plantCanPlantTime[i])//δ��ȴ��ɣ��޸ĺ�ɫĻ����С
                    slot.transform.GetChild(1).GetChild(i + 1).GetChild(2).GetComponent<Transform>().localScale
                        = new Vector3(1, (float)(P2_plantCanPlantTime[i] - nowTime) / (float)(P2_CD[i] * 10000000f), 1);
            }
        }
    }

    //��ʼ��ʱ
    public void TimeStart()
    {
        timeState = 1;
        startTime = System.DateTime.Now.Ticks;
        lastSunTime = System.DateTime.Now.Ticks;
        //��ȡ��ѡ��ֲ�����ȴʱ��
        List<int> p1 = GameManager.instance.P1_plant;
        List<int> p2 = GameManager.instance.P2_plant;
        for (int i = 0; i < 6; i++)
        {
            P1_CD[i] = GameManager.instance.CD[p1[i]];
            P2_CD[i] = GameManager.instance.CD[p2[i]];
        }
    }

    //ֹͣ��ʱ
    public void TimeEnd()
    {
        timeState = 3;
        endTime = System.DateTime.Now.Ticks;
    }

    //��ͣ��ʱ
    public void TimeStop()
    {
        timeState = 2;
        stopTime = System.DateTime.Now.Ticks;
    }

    //������ʱ
    public void TimeContinue()
    {
        timeState = 1;
        nowTime = System.DateTime.Now.Ticks;
        //�ӳ�ʱ��
        lastSunTime = lastSunTime + (nowTime - stopTime);
        for (int i = 0; i < 6; i++)
        {
            P1_plantCanPlantTime[i] = P1_plantCanPlantTime[i] + (nowTime - stopTime);
            P2_plantCanPlantTime[i] = P2_plantCanPlantTime[i] + (nowTime - stopTime);
        }
    }


    //��ֲֲ�������ȴʱ��
    public void Planting(int player, int plant)
    {
        if (player == 1)
        {
            int i;
            for (i = 0; i < 6; i++)
            {
                if (GameManager.instance.P1_plant[i] == plant)
                {
                    break;
                }
            }
            int cd = P1_CD[i];
            P1_plantCanPlantTime[i] = System.DateTime.Now.Ticks + cd * 10000000;
            slot.transform.GetChild(0).GetChild(i + 1).GetChild(2).GetComponent<Transform>().localScale
                         = new Vector3(1, 1, 1);
        }
        else if (player == 2)
        {
            int i;
            for (i = 0; i < 6; i++)
            {
                if (GameManager.instance.P2_plant[i] == plant)
                {
                    break;
                }
            }
            int cd = P2_CD[i];
            P2_plantCanPlantTime[i] = System.DateTime.Now.Ticks + cd * 10000000;
            slot.transform.GetChild(1).GetChild(i + 1).GetChild(2).GetComponent<Transform>().localScale
                         = new Vector3(1, 1, 1);
        }
    }

    //�鿴��ȴ�Ƿ����
    public bool IsCD(int player, int plant)
    {
        if (player == 1)
        {
            int i;
            for (i = 0; i < 6; i++)
            {
                if (GameManager.instance.P1_plant[i] == plant)
                {
                    break;
                }
            }
            long now = System.DateTime.Now.Ticks;
            if (now >= P1_plantCanPlantTime[i])//�Ѿ���ȴ���
                return true;
        }
        else if (player == 2)
        {
            int i;
            for (i = 0; i < 6; i++)
            {
                if (GameManager.instance.P2_plant[i] == plant)
                {
                    break;
                }
            }
            long now = System.DateTime.Now.Ticks;
            if (now >= P2_plantCanPlantTime[i])//�Ѿ���ȴ���
                return true;
        }
        return false;
    }

    //�õ���Ϸ����ʱ��
    public void OutputRunTime()
    {
        Debug.Log("��ս�ѿ�ʼ " + ((System.DateTime.Now.Ticks - startTime) / 10000) + " ms");
    }
}
