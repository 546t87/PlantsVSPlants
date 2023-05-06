using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jalapeno : Plant
{
    public Collider2D coll; // ��ɫ��ײ��
    public int ACK;
    public Animator anim;//����������

    private void Start()
    {
        ACK = 4000;
        HP = maxHP = 9999;
    }

    public override void Init(int i, int x0, int y0)//����ֲ����������ң���λ����Ϣ
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
        //����λ�úʹ�С
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        transform.localScale = new Vector3(1.3f, transform.localScale.y, transform.localScale.z);
        //�л�������������ײ������
        coll.enabled = true;
        shadow.enabled = false;//��ʾ������ҵ�Ȧ��ʧ
        anim.SetBool("isFire", true);
        //������Ч
        audioSourcs.volume = SoundManage.instance.soundEffectVolume;//��������
        audioSourcs.clip = attackSound;
        audioSourcs.Play();
    }

    // ��ײ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��ײ���Ǽ�����λ
        if (collision.tag != "Player" + player.ToString() && collision.tag != "Player" + player.ToString() + "_fire")
        {
            collision.gameObject.GetComponent<Plant>().Hurt(ACK);//����˺�
        }
    }
}
