using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : Plant
{
    public GameObject thorn;//尖刺预制体

    public void Attack()
    {
        bool attack = false;
        //判断是否需要发起攻击
        if (player == 1)
        {
            for (int i = x + 1; i < 9; i++)
            {
                if (GameManager.instance.battlefield[y, i] <= 0 && GameManager.instance.battlefield[y, i] > -100)
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
            }
        }
        if (attack)
        {
            GameObject p = Instantiate(thorn, new Vector3(transform.position.x, transform.position.y + 0.05f, 0), Quaternion.identity);//生成尖刺
            p.GetComponent<Thorn>().Init(player, 0);
            //播放音效
            audioSourcs.volume = SoundManage.instance.soundEffectVolume;//更新音量
            audioSourcs.clip = attackSound;
            audioSourcs.Play();
        }
    }
}
