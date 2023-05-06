using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sun : MonoBehaviour
{
    public int player;//�������
    public int sunNum = 100;//������
    public Vector3 positon;//��ʼλ��

    // Start is called before the first frame update
    void Start()
    {
        positon = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player == 1)
        {
            //�򿨲����⴦�ƶ�
            Vector3 goTo = new Vector3(-6.5f, 3.5f, 0);
            float x = (goTo.x - positon.x) / 100.0f;
            float y = (goTo.y - positon.y) / 100.0f;
            transform.position = new Vector3(transform.position.x + x, transform.position.y + y, 0);
            //��������ʧ
            if( (transform.position.x + 6.5f) * (transform.position.x + 6.5f) + (transform.position.y - 3.5f) * (transform.position.y - 3.5f) < 1f)
            {
                GameManager.instance.P1_sun += sunNum;//��������
                GameManager.instance.slot.transform.GetChild(0).GetComponentsInChildren<Text>()[0].text = GameManager.instance.P1_sun.ToString();
                //������Ч
                SoundManage.instance.GameCollectSun(1);
                Destroy(gameObject);
            }
        }
        else if(player == 2)
        {
            Vector3 goTo = new Vector3(6.5f, 3.5f, 0);
            float x = (goTo.x - positon.x) / 100.0f;
            float y = (goTo.y - positon.y) / 100.0f;
            transform.position = new Vector3(transform.position.x + x, transform.position.y + y, 0);
            //��������ʧ
            if ((transform.position.x - 6.5f) * (transform.position.x - 6.5f) + (transform.position.y - 3.5f) * (transform.position.y - 3.5f) < 1f)
            {
                GameManager.instance.P2_sun += sunNum;//��������
                GameManager.instance.slot.transform.GetChild(1).GetComponentsInChildren<Text>()[0].text = GameManager.instance.P2_sun.ToString();
                //������Ч
                SoundManage.instance.GameCollectSun(1);
                Destroy(gameObject);
            }
        }
    }

    public void SetPlayer(int i)
    {
        player = i;
        positon = transform.position;
    }
}
