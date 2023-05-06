using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject sun;//阳光预制体
    public int timeState;//计时器状态，0为未开始，1为计时中，2为暂停中，3为计时完成
    public long startTime;//开始时间
    public long nowTime;//当前时间
    public long stopTime;//暂停时间
    public long endTime;//结束时间
    public long lastSunTime;//上次阳光生成时间
    public GameObject slot;//卡槽
    public int[] P1_CD = new int[6];//植物冷却所需时间
    public int[] P2_CD = new int[6];//植物冷却所需时间
    public long[] P1_plantCanPlantTime = new long[6];//植物能再次种植的时间
    public long[] P2_plantCanPlantTime = new long[6];//植物能再次种植的时间

    //一秒钟为10000000  Time = System.DateTime.Now.Ticks;
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
            //自动生产阳光
            nowTime = System.DateTime.Now.Ticks;
            if (nowTime - lastSunTime > 100000000)//每隔10秒自动生成一个阳光
            {
                lastSunTime = nowTime;
                float x = Random.Range(-6.5f, 6.5f);
                float y = Random.Range(-3.5f, 2.3f);
                GameObject s1 = Instantiate(sun, new Vector3(x, y, 0), Quaternion.identity);//生成阳光
                s1.GetComponent<Sun>().SetPlayer(1);
                x = Random.Range(-6.5f, 6.5f);
                y = Random.Range(-3.5f, 2.3f);
                GameObject s2 = Instantiate(sun, new Vector3(x, y, 0), Quaternion.identity);//生成阳光
                s2.GetComponent<Sun>().SetPlayer(2);
            }

            //植物冷却
            for (int i = 0; i < 6; i++)
            {
                if (nowTime < P1_plantCanPlantTime[i])//未冷却完成，修改黑色幕布大小
                    slot.transform.GetChild(0).GetChild(i + 1).GetChild(2).GetComponent<Transform>().localScale
                        = new Vector3(1, (float)(P1_plantCanPlantTime[i] - nowTime) / (float)(P1_CD[i] * 10000000f), 1);
                if (nowTime < P2_plantCanPlantTime[i])//未冷却完成，修改黑色幕布大小
                    slot.transform.GetChild(1).GetChild(i + 1).GetChild(2).GetComponent<Transform>().localScale
                        = new Vector3(1, (float)(P2_plantCanPlantTime[i] - nowTime) / (float)(P2_CD[i] * 10000000f), 1);
            }
        }
    }

    //开始计时
    public void TimeStart()
    {
        timeState = 1;
        startTime = System.DateTime.Now.Ticks;
        lastSunTime = System.DateTime.Now.Ticks;
        //获取所选择植物的冷却时间
        List<int> p1 = GameManager.instance.P1_plant;
        List<int> p2 = GameManager.instance.P2_plant;
        for (int i = 0; i < 6; i++)
        {
            P1_CD[i] = GameManager.instance.CD[p1[i]];
            P2_CD[i] = GameManager.instance.CD[p2[i]];
        }
    }

    //停止计时
    public void TimeEnd()
    {
        timeState = 3;
        endTime = System.DateTime.Now.Ticks;
    }

    //暂停计时
    public void TimeStop()
    {
        timeState = 2;
        stopTime = System.DateTime.Now.Ticks;
    }

    //继续计时
    public void TimeContinue()
    {
        timeState = 1;
        nowTime = System.DateTime.Now.Ticks;
        //延长时间
        lastSunTime = lastSunTime + (nowTime - stopTime);
        for (int i = 0; i < 6; i++)
        {
            P1_plantCanPlantTime[i] = P1_plantCanPlantTime[i] + (nowTime - stopTime);
            P2_plantCanPlantTime[i] = P2_plantCanPlantTime[i] + (nowTime - stopTime);
        }
    }


    //种植植物，计算冷却时间
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

    //查看冷却是否完成
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
            if (now >= P1_plantCanPlantTime[i])//已经冷却完成
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
            if (now >= P2_plantCanPlantTime[i])//已经冷却完成
                return true;
        }
        return false;
    }

    //得到游戏运行时间
    public void OutputRunTime()
    {
        Debug.Log("对战已开始 " + ((System.DateTime.Now.Ticks - startTime) / 10000) + " ms");
    }
}
