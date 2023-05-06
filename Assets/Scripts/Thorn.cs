using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    public int player;//所属玩家
    public int ACK;//伤害
    public float speed;//速度
    public float delayTime;//延迟消失时间
    public AudioSource audioSourcs;//声音源
    public AudioClip peaBroken;//豌豆碎裂音效

    // Start is called before the first frame update
    void Start()
    {
        ACK = 40;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //尖刺移动
        float x = transform.position.x;
        float y = transform.position.y;
        if (player == 1)
        {
            x = x + speed;
        }
        else if (player == 2)
        {
            x = x - speed;
        }
        transform.position = new Vector3(x, y, 0);
        //出界删除
        if (transform.position.x < -7f || transform.position.x > 7f)
        {
            Dead();
        }
    }

    //设置所属玩家等属性
    public void Init(int play, int dir)
    {
        player = play;
        if (player == 2)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
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
        if (collision.tag == "Player" + p0.ToString() || collision.tag == "Player" + p0.ToString() + "_fire" || collision.tag == "Player0")
        {
            collision.gameObject.GetComponent<Plant>().Hurt(ACK);//造成伤害
            //播放音效
            audioSourcs.volume = SoundManage.instance.soundEffectVolume;//更新音量
            audioSourcs.clip = peaBroken;
            audioSourcs.Play();
            Invoke("Dead", delayTime);//额外飞行一格后消失
        }
    }

    //销毁豌豆
    public void Dead()
    {
        Destroy(gameObject);
    }
}
