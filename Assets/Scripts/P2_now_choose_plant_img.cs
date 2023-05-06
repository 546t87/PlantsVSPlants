using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_now_choose_plant_img : MonoBehaviour
{
    public SpriteRenderer image;//ͼƬ
    public Animator anim;//����������

    //�����ƶ����
    public void MoveOutEnd()
    {
        //�л�ֲ��ͼƬ
        int index = ChooseManage.instance.P2_Y * 4 + ChooseManage.instance.P2_X;
        image.sprite = ChooseManage.instance.PLANT[index];
        if (ChooseManage.instance.P2_choose_state != ChooseManage.instance.CHOOSE_READY)
            ChooseManage.instance.P2_choose_state = ChooseManage.instance.CHOOSE_MOVING_IN;
        anim.SetInteger("move_state", 2);
    }

    //�����ƶ����
    public void MoveInEnd()
    {
        //�л�״̬
        if (ChooseManage.instance.P2_choose_state != ChooseManage.instance.CHOOSE_READY)
            ChooseManage.instance.P2_choose_state = ChooseManage.instance.CHOOSE_OK;
        anim.SetInteger("move_state", 0);
    }
}
