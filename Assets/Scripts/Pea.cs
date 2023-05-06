using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pea : MonoBehaviour
{
    public int player;//所属玩家
    public int ACK;//伤害
    public float speed;//速度
    public bool isHurt;//是否已经造成伤害
    public bool isFire;//是否已经被点燃
    public int direction;//子弹移动方向，向前为0，向上为1，向下为-1
    public float init_y;//初始Y值，用于判断是否移动到其他行
    public Animator anim;
    public AudioSource audioSourcs;//声音源
    public AudioClip peaBroken;//豌豆碎裂音效
    public AudioClip firePeaBroken;//火豌豆碎裂音效

    // Start is called before the first frame update
    void Start()
    {
        isHurt = false;
        isFire = false;
        ACK = 40;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if(player == 1 && isHurt == false)
        {
            x = x + speed;
        }
        else if(player == 2 && isHurt == false)
        {
            x = x - speed;
        }
        if (y - init_y < 1.35 && y - init_y > -1.35)//还没换行
            y = y + direction * speed;
        transform.position = new Vector3(x, y, 0);
        if (transform.position.x < -7f || transform.position.x > 7f)
        {
            Dead();
        }
    }

    public void Init(int play, int dir)//设置所属玩家等属性
    {
        direction = dir;
        player = play;
        init_y = transform.position.y;
        if(player == 2)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)// 碰撞触发器
    {
        int p0;
        if (player == 1)
            p0 = 2;
        else
            p0 = 1;
        // 碰撞到己方火焰
        if (collision.tag == "Player" + player.ToString() + "_fire" && isHurt == false && isFire == false)
        {
            isFire = true;
            ACK = ACK * 2;//经过火伤害加倍
            anim.SetInteger("peaState", 1);//更新动画
        }
        // 碰撞到非己方单位
        else if (isHurt == false && (collision.tag == "Player" + p0.ToString() || collision.tag == "Player" + p0.ToString() + "_fire" || collision.tag == "Player0"))
        {
            isHurt = true;//设置已经造成伤害
            collision.gameObject.GetComponent<Plant>().Hurt(ACK);//造成伤害
            anim.SetInteger("peaState", 2);//更新动画
            //播放音效
            if (isFire)
            {
                audioSourcs.volume = SoundManage.instance.soundEffectVolume;//更新音量
                audioSourcs.clip = firePeaBroken;
                audioSourcs.Play();
            }
            else
            {
                audioSourcs.volume = SoundManage.instance.soundEffectVolume;//更新音量
                audioSourcs.clip = peaBroken;
                audioSourcs.Play();
            }
            Invoke("Dead", 0.2f);
        }
    }

    public void Dead()//销毁豌豆
    {
        Destroy(gameObject);
    }
}
