using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public int x;//λ��
    public int y;
    public int player;//�������
    public int maxHP;//�������ֵ
    public int HP;//��ǰ����ֵ
    public SpriteRenderer shadow;//��Ӱ
    public Sprite shadow1;//��ҿ�
    public Sprite shadow2;
    public AudioSource audioSourcs;//����Դ
    public AudioClip attackSound;//������Ч

    private void Start()
    {
        HP = maxHP;
    }

    public virtual void Init(int p0, int x0, int y0)//����ֲ����������ң���λ����Ϣ
    {
        player = p0;
        x = x0;
        y = y0;
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

    public virtual void Hurt(int i)//�ܵ��˺�
    {
        HP -= i;
        if (HP <= 0)
            Dead();
    }

    public virtual bool CanAttack()//�ж��ܷ��𹥻�
    {
        if(player == 1)
        {
            for (int i = x + 1; i < 9; i++)
            {
                if (GameManager.instance.battlefield[y, i] <= 0 && GameManager.instance.battlefield[y, i] > -100)
                {
                    return true;
                }
            }
        }
        else if(player == 2)
        {
            for (int i = x - 1; i >= 0; i--)
            {
                if (GameManager.instance.battlefield[y, i] >= 0 && GameManager.instance.battlefield[y, i] < 100)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void Dead()//����
    {
        GameManager.instance.battlefield[y, x] = 999;//�ճ�λ��
        Destroy(gameObject);
    }
}
