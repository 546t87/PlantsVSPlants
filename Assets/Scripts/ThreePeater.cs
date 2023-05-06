using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreePeater : Plant
{
    public GameObject pea;//�㶹Ԥ����

    public void Attack()
    {
        bool attack = false;
        //�ж��Ƿ���Ҫ���𹥻�
        if (player == 1)
        {
            for (int i = x + 1; i < 9; i++)
            {
                if (GameManager.instance.battlefield[y, i] <= 0 && GameManager.instance.battlefield[y, i] > -100)//���д���Ŀ��
                {
                    attack = true;
                }
                else if (y - 1 >= 0 && GameManager.instance.battlefield[y - 1, i] <= 0 && GameManager.instance.battlefield[y - 1, i] > -100)//��һ���д���Ŀ��
                {
                    attack = true;
                }
                else if (y + 1 < 5 && GameManager.instance.battlefield[y + 1, i] <= 0 && GameManager.instance.battlefield[y + 1, i] > -100)//��һ���д���Ŀ��
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
                else if (y - 1 >= 0 && GameManager.instance.battlefield[y - 1, i] >= 0 && GameManager.instance.battlefield[y - 1, i] < 100)
                {
                    attack = true;
                }
                else if (y + 1 < 5 && GameManager.instance.battlefield[y + 1, i] >= 0 && GameManager.instance.battlefield[y + 1, i] < 100)
                {
                    attack = true;
                }
            }
        }
        if (attack)
        {
            //�����㶹
            GameObject p1 = Instantiate(pea, new Vector3(transform.position.x, transform.position.y + 0.3f, 0), Quaternion.identity);
            p1.GetComponent<Pea>().Init(player, 0);
            if (y - 1 >= 0)//���ϵ��㶹
            {
                GameObject p2 = Instantiate(pea, new Vector3(transform.position.x, transform.position.y + 0.3f, 0), Quaternion.identity);
                p2.GetComponent<Pea>().Init(player, 1);
            }
            if(y + 1 < 5)//���µ��㶹
            {
                GameObject p3 = Instantiate(pea, new Vector3(transform.position.x, transform.position.y + 0.3f, 0), Quaternion.identity);
                p3.GetComponent<Pea>().Init(player, -1);
            }
            //������Ч
            audioSourcs.volume = SoundManage.instance.soundEffectVolume;//��������
            audioSourcs.clip = attackSound;
            audioSourcs.Play();
        }
    }
}
