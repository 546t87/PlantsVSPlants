using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{
    public GameObject sun;//����Ԥ����

    public void CreateSun()
    {
        GameObject s1 = Instantiate(sun, new Vector3(transform.position.x, transform.position.y + 0.15f, 0), Quaternion.identity);//��������
        s1.GetComponent<Sun>().SetPlayer(player);
    }
}
