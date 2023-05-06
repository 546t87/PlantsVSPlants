using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ready : MonoBehaviour
{
    //动画播放完成，开始对战
    public void GameStart()
    {
        GameManager.instance.GameStart();
        Destroy(gameObject);
    }
}
