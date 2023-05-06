using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
    public int hurtState;//��ǰ״̬��0Ϊ��ã�1Ϊ���ˣ�Ϊ����
    public Animator anim;//����������

    private void Start()
    {
        hurtState = 0;
    }

    public override void Hurt(int i)//�ܵ��˺�
    {
        HP -= i;
        //�ж��Ƿ���Ҫ�л�����
        if(HP <= (2 * maxHP)/3.0f)
        {
            hurtState = 1;
            anim.SetInteger("HurtState", 1);
        }
        if(HP <= maxHP/3.0f)
        {
            hurtState = 2;
            anim.SetInteger("HurtState", 2);
        }
        if (HP <= 0)
            Dead();
    }

}
