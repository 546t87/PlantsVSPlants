using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int x;//位置
    public int y;
    public int player;//所属玩家
    public int maxHP;//最高生命值
    public int HP;//当前生命值
    public SpriteRenderer shadow;//阴影
    public Sprite shadow1;//玩家框
    public Sprite shadow2;
    public AudioSource audioSourcs;//声音源
    public AudioClip attackSound;//攻击音效

    private void Start()
    {
        HP = maxHP;
    }

    public virtual void Init(int p0, int x0, int y0)//设置植物所属的玩家，与位置信息
    {
        player = p0;
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

    public virtual void Hurt(int i)//受到伤害
    {
        HP -= i;
        if (HP <= 0)
            Dead();
    }

    public virtual bool CanAttack()//判断能否发起攻击
    {
        if(player == 1)
        {
            for (int i = x + 1; i < 9; i++)
            {
                if (GameManager.instance.battlefield[y, i] <= 0 && GameManager.instance.battlefield[y, i] > -100)
                {
                    return true;
                }
            }
        }
        else if(player == 2)
        {
            for (int i = x - 1; i >= 0; i--)
            {
                if (GameManager.instance.battlefield[y, i] >= 0 && GameManager.instance.battlefield[y, i] < 100)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void Dead()//死亡
    {
        GameManager.instance.battlefield[y, x] = 999;//空出位置
        Destroy(gameObject);
    }
}
