using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squash : Plant
{
    public Collider2D coll; // ��ɫ��ײ��
    public int ACK;
    public Animator anim;//����������
    public float moveToX;//�����ƶ�
    public int moveNum;//�ƶ�����
    public bool isHurt;//�Ƿ��Ѿ�����˺�

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
        if(moveNum == -1)//��δ��������
        {
            if(player == 1)
            {
                if (x - 1 >= 0 && GameManager.instance.battlefield[y, x - 1] <= 0 && GameManager.instance.battlefield[y, x - 1] > -100)//����е���
                {
                    moveToX = -1.2f;
                    Attack();
                }
                else if (x + 1 < 9 && GameManager.instance.battlefield[y, x + 1] <= 0 && GameManager.instance.battlefield[y, x + 1] > -100)//�Ҳ��е���
                {
                    moveToX = 1.2f;
                    Attack();
                }
            }
            else if(player == 2)
            {
                if (x + 1 < 9 && GameManager.instance.battlefield[y, x + 1] >= 0 && GameManager.instance.battlefield[y, x + 1] < 100)//�Ҳ��е���
                {
                    moveToX = 1.2f;
                    Attack();
                }
                else if (x - 1 >= 0 && GameManager.instance.battlefield[y, x - 1] >= 0 && GameManager.instance.battlefield[y, x - 1] < 100)//����е���
                {
                    moveToX = -1.2f;
                    Attack();
                }
            }
        }
        else if(moveNum <10)//��������ʱ�����ƶ�
        {
            if(transform.position.x != moveToX)
            {
                transform.position = new Vector3(transform.position.x + 0.1f * moveToX, transform.position.y + transform.position.z);
                moveNum++;
            }
        }
    }

    public override void Init(int i, int x0, int y0)//����ֲ����������ң���λ����Ϣ
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
        //�л�������������ײ������
        coll.enabled = true;
        shadow.enabled = false;//��ʾ������ҵ�Ȧ��ʧ
        anim.SetBool("isAttack", true);
        //������Ч
        Invoke("playAudio",0.15f);
    }

    // ������Ч
    public void playAudio()
    {
        audioSourcs.volume = SoundManage.instance.soundEffectVolume;//��������
        audioSourcs.clip = attackSound;
        audioSourcs.Play();
    }

    // ��ײ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int p0;
        if (player == 1)
            p0 = 2;
        else
            p0 = 1;
        // ��ײ���Ǽ�����λ
        if (isHurt == false && (collision.tag == "Player" + p0.ToString() || collision.tag == "Player" + p0.ToString() + "_fire" || collision.tag == "Player0"))
        {
            isHurt = true;
            collision.gameObject.GetComponent<Plant>().Hurt(ACK);//����˺�
        }
    }
}
