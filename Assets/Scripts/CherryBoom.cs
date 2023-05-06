using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBoom : Plant
{
    public Collider2D coll; // 角色碰撞体
    public int ACK;

    private void Start()
    {
        ACK = 4000;
        HP = maxHP = 9999;
    }

    public override void Init(int i, int x0, int y0)//设置植物所属的玩家，与位置信息
    {
        player = i;
        x = x0;
        y = y0;
        if (player == 1)
        {
            shadow.sprite = shadow1;
        }
        else if (player == 2)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            shadow.sprite = shadow2;
        }
    }

    public void Attack()
    {
        coll.enabled = true;
        //播放音效
        audioSourcs.volume = SoundManage.instance.soundEffectVolume;//更新音量
        audioSourcs.clip = attackSound;
        audioSourcs.Play();
        Invoke("Dead", 0.7f);
    }

    // 碰撞触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 碰撞到非己方单位
        if (collision.tag != "Player" + player.ToString() && collision.tag != "Player" + player.ToString() + "_fire")
        {
            collision.gameObject.GetComponent<Plant>().Hurt(ACK);//造成伤害
        }
    }
}
