using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_now_choose_plant_img : MonoBehaviour
{
    public SpriteRenderer image;//图片
    public Animator anim;//动画控制器

    //向外移动完成
    public void MoveOutEnd()
    {
        //切换植物图片
        int index = ChooseManage.instance.P2_Y * 4 + ChooseManage.instance.P2_X;
        image.sprite = ChooseManage.instance.PLANT[index];
        if (ChooseManage.instance.P2_choose_state != ChooseManage.instance.CHOOSE_READY)
            ChooseManage.instance.P2_choose_state = ChooseManage.instance.CHOOSE_MOVING_IN;
        anim.SetInteger("move_state", 2);
    }

    //向内移动完成
    public void MoveInEnd()
    {
        //切换状态
        if (ChooseManage.instance.P2_choose_state != ChooseManage.instance.CHOOSE_READY)
            ChooseManage.instance.P2_choose_state = ChooseManage.instance.CHOOSE_OK;
        anim.SetInteger("move_state", 0);
    }
}
