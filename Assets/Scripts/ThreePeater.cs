using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreePeater : Plant
{
    public GameObject pea;//豌豆预制体

    public void Attack()
    {
        bool attack = false;
        //判断是否需要发起攻击
        if (player == 1)
        {
            for (int i = x + 1; i < 9; i++)
            {
                if (GameManager.instance.battlefield[y, i] <= 0 && GameManager.instance.battlefield[y, i] > -100)//本行存在目标
                {
                    attack = true;
                }
                else if (y - 1 >= 0 && GameManager.instance.battlefield[y - 1, i] <= 0 && GameManager.instance.battlefield[y - 1, i] > -100)//上一行行存在目标
                {
                    attack = true;
                }
                else if (y + 1 < 5 && GameManager.instance.battlefield[y + 1, i] <= 0 && GameManager.instance.battlefield[y + 1, i] > -100)//下一行行存在目标
                {
                    attack = true;
                }
            }
        }
        else if (player == 2)
        {
            for (int i = x - 1; i >= 0; i--)
            {
                if (GameManager.instance.battlefield[y, i] >= 0 && GameManager.instance.battlefield[y, i] < 100)
                {
                    attack = true;
                }
                else if (y - 1 >= 0 && GameManager.instance.battlefield[y - 1, i] >= 0 && GameManager.instance.battlefield[y - 1, i] < 100)
                {
                    attack = true;
                }
                else if (y + 1 < 5 && GameManager.instance.battlefield[y + 1, i] >= 0 && GameManager.instance.battlefield[y + 1, i] < 100)
                {
                    attack = true;
                }
            }
        }
        if (attack)
        {
            //生成豌豆
            GameObject p1 = Instantiate(pea, new Vector3(transform.position.x, transform.position.y + 0.3f, 0), Quaternion.identity);
            p1.GetComponent<Pea>().Init(player, 0);
            if (y - 1 >= 0)//向上的豌豆
            {
                GameObject p2 = Instantiate(pea, new Vector3(transform.position.x, transform.position.y + 0.3f, 0), Quaternion.identity);
                p2.GetComponent<Pea>().Init(player, 1);
            }
            if(y + 1 < 5)//向下的豌豆
            {
                GameObject p3 = Instantiate(pea, new Vector3(transform.position.x, transform.position.y + 0.3f, 0), Quaternion.identity);
                p3.GetComponent<Pea>().Init(player, -1);
            }
            //播放音效
            audioSourcs.volume = SoundManage.instance.soundEffectVolume;//更新音量
            audioSourcs.clip = attackSound;
            audioSourcs.Play();
        }
    }
}
