using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plant
{
    public GameObject pea;//�㶹Ԥ����

    public void Attack()
    {
        bool attack = false;
        //�ж��Ƿ���Ҫ���𹥻�
        if(player == 1)
        {
            for(int i = x+1; i < 9; i++)
            {
                if(GameManager.instance.battlefield[y,i]<=0 && GameManager.instance.battlefield[y, i] > -100)
                {
                    attack = true;
                }
            }
        }
        else if(player == 2)
        {
            for (int i = x - 1; i >= 0; i--)
            {
                if (GameManager.instance.battlefield[y, i] >= 0 && GameManager.instance.battlefield[y, i] < 100)
                {
                    attack = true;
                }
            }
        }
        if (attack)
        {
            GameObject p = Instantiate(pea, new Vector3(transform.position.x, transform.position.y + 0.3f, 0), Quaternion.identity);//�����㶹
            p.GetComponent<Pea>().Init(player, 0);
            //������Ч
            audioSourcs.volume = SoundManage.instance.soundEffectVolume;//��������
            audioSourcs.clip = attackSound;
            audioSourcs.Play();
        }
    }
}
