using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchWood : Plant
{
    public override void Init(int i, int x0, int y0)//设置植物所属的玩家，与位置信息
    {
        player = i;
        x = x0;
        y = y0;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z);
        if (player == 1)
        {
            shadow.sprite = shadow1;
            gameObject.tag = "Player1_fire";
        }
        else if (player == 2)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            shadow.sprite = shadow2;
            gameObject.tag = "Player2_fire";
        }
    }
}

