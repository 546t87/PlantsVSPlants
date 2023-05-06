using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pea : MonoBehaviour
{
    public int player;//�������
    public int ACK;//�˺�
    public float speed;//�ٶ�
    public bool isHurt;//�Ƿ��Ѿ�����˺�
    public bool isFire;//�Ƿ��Ѿ�����ȼ
    public int direction;//�ӵ��ƶ�������ǰΪ0������Ϊ1������Ϊ-1
    public float init_y;//��ʼYֵ�������ж��Ƿ��ƶ���������
    public Animator anim;
    public AudioSource audioSourcs;//����Դ
    public AudioClip peaBroken;//�㶹������Ч
    public AudioClip firePeaBroken;//���㶹������Ч

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
        if (y - init_y < 1.35 && y - init_y > -1.35)//��û����
            y = y + direction * speed;
        transform.position = new Vector3(x, y, 0);
        if (transform.position.x < -7f || transform.position.x > 7f)
        {
            Dead();
        }
    }

    public void Init(int play, int dir)//����������ҵ�����
    {
        direction = dir;
        player = play;
        init_y = transform.position.y;
        if(player == 2)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)// ��ײ������
    {
        int p0;
        if (player == 1)
            p0 = 2;
        else
            p0 = 1;
        // ��ײ����������
        if (collision.tag == "Player" + player.ToString() + "_fire" && isHurt == false && isFire == false)
        {
            isFire = true;
            ACK = ACK * 2;//�������˺��ӱ�
            anim.SetInteger("peaState", 1);//���¶���
        }
        // ��ײ���Ǽ�����λ
        else if (isHurt == false && (collision.tag == "Player" + p0.ToString() || collision.tag == "Player" + p0.ToString() + "_fire" || collision.tag == "Player0"))
        {
            isHurt = true;//�����Ѿ�����˺�
            collision.gameObject.GetComponent<Plant>().Hurt(ACK);//����˺�
            anim.SetInteger("peaState", 2);//���¶���
            //������Ч
            if (isFire)
            {
                audioSourcs.volume = SoundManage.instance.soundEffectVolume;//��������
                audioSourcs.clip = firePeaBroken;
                audioSourcs.Play();
            }
            else
            {
                audioSourcs.volume = SoundManage.instance.soundEffectVolume;//��������
                audioSourcs.clip = peaBroken;
                audioSourcs.Play();
            }
            Invoke("Dead", 0.2f);
        }
    }

    public void Dead()//�����㶹
    {
        Destroy(gameObject);
    }
}
