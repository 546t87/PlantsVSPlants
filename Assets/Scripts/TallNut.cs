using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallNut : Plant
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
        if (HP <= (2 * maxHP) / 3.0f)
        {
            hurtState = 1;
            anim.SetInteger("HurtState", 1);
        }
        if (HP <= maxHP / 3.0f)
        {
            hurtState = 1;
            anim.SetInteger("HurtState", 2);
        }
        if (HP <= 0)
            Dead();
    }

    public override void Init(int i, int x0, int y0)
    {
        player = i;
        x = x0;
        y = y0;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.27f, transform.position.z);
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
}
