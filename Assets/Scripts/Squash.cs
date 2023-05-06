using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squash : Plant
{
    public Collider2D coll; // 角色碰撞体
    public int ACK;
    public Animator anim;//动画控制器
    public float moveToX;//横向移动
    public int moveNum;//移动次数
    public bool isHurt;//是否已经造成伤害

    private void Start()
    {
        ACK = 4000;
        moveToX = 0;
        coll.enabled = false;
        moveNum = -1;
        isHurt = false;
    }

    private void FixedUpdate()
    {
        if(moveNum == -1)//还未发动攻击
        {
            if(player == 1)
            {
                if (x - 1 >= 0 && GameManager.instance.battlefield[y, x - 1] <= 0 && GameManager.instance.battlefield[y, x - 1] > -100)//左侧有敌人
                {
                    moveToX = -1.2f;
                    Attack();
                }
                else if (x + 1 < 9 && GameManager.instance.battlefield[y, x + 1] <= 0 && GameManager.instance.battlefield[y, x + 1] > -100)//右侧有敌人
                {
                    moveToX = 1.2f;
                    Attack();
                }
            }
            else if(player == 2)
            {
                if (x + 1 < 9 && GameManager.instance.battlefield[y, x + 1] >= 0 && GameManager.instance.battlefield[y, x + 1] < 100)//右侧有敌人
                {
                    moveToX = 1.2f;
                    Attack();
                }
                else if (x - 1 >= 0 && GameManager.instance.battlefield[y, x - 1] >= 0 && GameManager.instance.battlefield[y, x - 1] < 100)//左侧有敌人
                {
                    moveToX = -1.2f;
                    Attack();
                }
            }
        }
        else if(moveNum <10)//发动攻击时进行移动
        {
            if(transform.position.x != moveToX)
            {
                transform.position = new Vector3(transform.position.x + 0.1f * moveToX, transform.position.y + transform.position.z);
                moveNum++;
            }
        }
    }

    public override void Init(int i, int x0, int y0)//设置植物所属的玩家，与位置信息
    {
        player = i;
        x = x0;
        y = y0;
        if (player == 1)
        {
            shadow.sprite = shadow1;
            gameObject.tag = "Player1";
        }
        else if (player == 2)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            shadow.sprite = shadow2;
            gameObject.tag = "Player2";
        }
    }

    public void Attack()
    {
        moveNum = 0;
        //切换动画，启用碰撞触发器
        coll.enabled = true;
        shadow.enabled = false;//提示所属玩家的圈消失
        anim.SetBool("isAttack", true);
        //播放音效
        Invoke("playAudio",0.15f);
    }

    // 播放音效
    public void playAudio()
    {
        audioSourcs.volume = SoundManage.instance.soundEffectVolume;//更新音量
        audioSourcs.clip = attackSound;
        audioSourcs.Play();
    }

    // 碰撞触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int p0;
        if (player == 1)
            p0 = 2;
        else
            p0 = 1;
        // 碰撞到非己方单位
        if (isHurt == false && (collision.tag == "Player" + p0.ToString() || collision.tag == "Player" + p0.ToString() + "_fire" || collision.tag == "Player0"))
        {
            isHurt = true;
            collision.gameObject.GetComponent<Plant>().Hurt(ACK);//造成伤害
        }
    }
}
