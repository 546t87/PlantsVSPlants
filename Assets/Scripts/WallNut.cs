using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
    public int hurtState;//当前状态，0为完好，1为受伤，为残破
    public Animator anim;//动画控制器

    private void Start()
    {
        hurtState = 0;
    }

    public override void Hurt(int i)//受到伤害
    {
        HP -= i;
        //判断是否需要切换动画
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
