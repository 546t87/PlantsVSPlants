using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ready : MonoBehaviour
{
    //����������ɣ���ʼ��ս
    public void GameStart()
    {
        GameManager.instance.GameStart();
        Destroy(gameObject);
    }
}
