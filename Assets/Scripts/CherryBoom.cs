using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBoom : Plant
{
    public Collider2D coll; // ��ɫ��ײ��
    public int ACK;

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
        coll.enabled = true;
        //������Ч
        audioSourcs.volume = SoundManage.instance.soundEffectVolume;//��������
        audioSourcs.clip = attackSound;
        audioSourcs.Play();
        Invoke("Dead", 0.7f);
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
