using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    public int player;//�������
    public int ACK;//�˺�
    public float speed;//�ٶ�
    public float delayTime;//�ӳ���ʧʱ��
    public AudioSource audioSourcs;//����Դ
    public AudioClip peaBroken;//�㶹������Ч

    // Start is called before the first frame update
    void Start()
    {
        ACK = 40;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //����ƶ�
        float x = transform.position.x;
        float y = transform.position.y;
        if (player == 1)
        {
            x = x + speed;
        }
        else if (player == 2)
        {
            x = x - speed;
        }
        transform.position = new Vector3(x, y, 0);
        //����ɾ��
        if (transform.position.x < -7f || transform.position.x > 7f)
        {
            Dead();
        }
    }

    //����������ҵ�����
    public void Init(int play, int dir)
    {
        player = play;
        if (player == 2)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    // ��ײ������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int p0;
        if (player == 1)
            p0 = 2;
        else
            p0 = 1;
        // ��ײ���Ǽ�����λ
        if (collision.tag == "Player" + p0.ToString() || collision.tag == "Player" + p0.ToString() + "_fire" || collision.tag == "Player0")
        {
            collision.gameObject.GetComponent<Plant>().Hurt(ACK);//����˺�
            //������Ч
            audioSourcs.volume = SoundManage.instance.soundEffectVolume;//��������
            audioSourcs.clip = peaBroken;
            audioSourcs.Play();
            Invoke("Dead", delayTime);//�������һ�����ʧ
        }
    }

    //�����㶹
    public void Dead()
    {
        Destroy(gameObject);
    }
}
