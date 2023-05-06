using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseManage : MonoBehaviour
{
    public static ChooseManage instance;

    //��Ϸ����
    public List<int> P1_plant;//���1ѡ���ֲ��
    public List<int> P2_plant;//���2ѡ���ֲ��

    //ѡ��ֲ��
    public GameObject choosePanel;//ѡ�����
    //���1
    public GameObject P1_choose_frame;//ѡ���
    public int P1_X;//ѡ�������λ��
    public int P1_Y;
    public GameObject P1_big_plant;//��ǰѡ�е�ֲ��
    public SpriteRenderer[] P1_plant_img = new SpriteRenderer[6];//��ѡ���ֲ��ͼƬ
    public int P1_choose_state;//ѡ��״̬
    public SpriteRenderer P1_lock;//��ͷ
    //���2
    public GameObject P2_choose_frame;//ѡ���
    public int P2_X;
    public int P2_Y;
    public GameObject P2_big_plant;//��ǰѡ�е�ֲ��
    public SpriteRenderer[] P2_plant_img = new SpriteRenderer[6];//��ѡ���ֲ��ͼƬ
    public int P2_choose_state;//ѡ��״̬
    public SpriteRenderer P2_lock;//��ͷ

    //���峣��
    //��Ϸ״̬
    [HideInInspector] public int CHOOSE_OK = 10;//�ɽ��в���
    [HideInInspector] public int CHOOSE_MOVING_OUT = 11;//�л�ֲ���У�ֲ�������Ƴ�
    [HideInInspector] public int CHOOSE_MOVING_IN = 12;//�л�ֲ���У�ֲ����������
    [HideInInspector] public int CHOOSE_READY = 13;//��ȷ��ѡ�����
    //ֲ������
    [HideInInspector] public int SUN_FLOWER = 1;//���տ�
    [HideInInspector] public int TWIN_SUN_FLOWER = 2;//˫�����տ�
    [HideInInspector] public int TALL_NUT = 3;//�߼��
    [HideInInspector] public int WALL_NUT = 4;//���ǽ
    [HideInInspector] public int PEA_SHOOTER = 5;//�㶹����
    [HideInInspector] public int REPEATER = 6;//˫������
    [HideInInspector] public int THREE_PEATER = 7;//��������
    [HideInInspector] public int CACTUS = 8;//������
    [HideInInspector] public int CHERRY_BOOM = 9;//ӣ��ը��
    [HideInInspector] public int JALAPENO = 10;//������
    [HideInInspector] public int SQUASH = 11;//����
    [HideInInspector] public int TORCH_WOOD = 12;//�����׮
    //ѡ��ֲ���г���
    [HideInInspector] public float CHOOSE_X = (float)-1.53;//ѡ�����׼Xλ��
    [HideInInspector] public float CHOOSE_Y = (float)1.33;//ѡ�����׼Yλ��
    [HideInInspector] public float CHOOSE_X_DETA = (float)1.02;//ѡ����Xλ����
    [HideInInspector] public float CHOOSE_Y_DETA = (float)-1.46;//ѡ����Yλ����
    public Sprite[] PLANT;//ֲ��ͼƬ
    public Sprite[] PLANT_CARD;//ֲ�￨ƬͼƬ

    private void Awake()
    {
        instance = this;
        //���÷ֱ��ʣ���ȫ��
        Screen.SetResolution(1920, 1080, false);
        //��ʼ��
        P1_plant = new List<int>();
        P2_plant = new List<int>();
    }

    void Start()
    {
        //������ʼ��
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        ChooseKeyResponse();
    }

    //��ʼ��
    private void Initialized()
    {
        //��������
        SoundManage.instance.ChooseingMusic();
        //��ʾѡ������
        choosePanel.SetActive(true);
        //��ʼ��ѡ��λ��
        P1_X = 0;
        P1_Y = 0;
        P2_X = 3;
        P2_Y = 0;
        //���ؿհ׿�Ƭ
        int num = P1_plant.Count;
        for (int i = num; i < 6; i++)
        {
            P1_plant_img[i].gameObject.SetActive(false);
        }
        num = P2_plant.Count;
        for (int i = num; i < 6; i++)
        {
            P2_plant_img[i].gameObject.SetActive(false);
        }
        //��ʼ��ѡ��״̬
        P1_choose_state = CHOOSE_OK;
        P2_choose_state = CHOOSE_OK;
    }

    //����ѡ��ֲ������У�������Ӧ�¼�
    private void ChooseKeyResponse()
    {
        //������Ӧ
        //���1
        if (Input.GetKeyDown(KeyCode.W) && P1_choose_state == CHOOSE_OK)
        {
            //����λ��
            P1_Y -= 1;
            if (P1_Y < 0)
                P1_Y = 2;
            P1_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P1_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P1_Y * CHOOSE_Y_DETA;
            P1_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //���Ŷ���
            P1_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //������Ч
            SoundManage.instance.ChooseingMove(1);
        }
        else if (Input.GetKeyDown(KeyCode.S) && P1_choose_state == CHOOSE_OK)
        {
            //����λ��
            P1_Y += 1;
            if (P1_Y > 2)
                P1_Y = 0;
            P1_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P1_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P1_Y * CHOOSE_Y_DETA;
            P1_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //���Ŷ���
            P1_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //������Ч
            SoundManage.instance.ChooseingMove(1);
        }
        else if (Input.GetKeyDown(KeyCode.A) && P1_choose_state == CHOOSE_OK)
        {
            //����λ��
            P1_X -= 1;
            if (P1_X < 0)
                P1_X = 3;
            P1_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P1_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P1_Y * CHOOSE_Y_DETA;
            P1_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //���Ŷ���
            P1_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //������Ч
            SoundManage.instance.ChooseingMove(1);
        }
        else if (Input.GetKeyDown(KeyCode.D) && P1_choose_state == CHOOSE_OK)
        {
            //����λ��
            P1_X += 1;
            if (P1_X > 3)
                P1_X = 0;
            P1_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P1_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P1_Y * CHOOSE_Y_DETA;
            P1_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //���Ŷ���
            P1_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //������Ч
            SoundManage.instance.ChooseingMove(1);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && P1_choose_state != CHOOSE_READY)
        {
            //���¿�������
            int index = P1_Y * 4 + P1_X;
            if (P1_plant.Contains(index))//����ûѡ�� �� ������ѡ�У��Ƴ�����
            {
                P1_plant.Remove(index);//�Ƴ�����
                //������ѡ����ͼƬ
                for (int i = 0; i < 6; i++)
                {
                    if (i < P1_plant.Count)
                    {
                        P1_plant_img[i].gameObject.SetActive(true);
                        P1_plant_img[i].sprite = PLANT_CARD[P1_plant[i]];
                    }
                    else if (i >= P1_plant.Count)
                    {
                        P1_plant_img[i].gameObject.SetActive(false);
                    }
                }
                //������Ч
                SoundManage.instance.ChooseingChoose(1);
            }
            else if (P1_plant.Count == 6)//�����Ѿ�ѡ�� �� ����δѡ�У�ȷ�Ͽ���
            {
                if (P2_choose_state != CHOOSE_READY)//���ֻ�δ׼�����
                {
                    P1_choose_state = CHOOSE_READY;
                    P1_lock.gameObject.SetActive(true);
                    //������Ч
                    SoundManage.instance.ChooseingLock(1);
                }
                else//�����Ѿ�ѡ����ɣ���Ϸ��ʼ
                {
                    GameReady();
                }
            }
            else//����δѡ�� �� ����δѡ�У�ѡ����
            {
                P1_plant.Add(index);//ѡ�п���
                //������ѡ����ͼƬ
                for (int i = 0; i < 6; i++)
                {
                    if (i < P1_plant.Count)
                    {
                        P1_plant_img[i].gameObject.SetActive(true);
                        P1_plant_img[i].sprite = PLANT_CARD[P1_plant[i]];
                    }
                    else if (i >= P1_plant.Count)
                    {
                        P1_plant_img[i].gameObject.SetActive(false);
                    }
                }
                //������Ч
                SoundManage.instance.ChooseingChoose(1);
            }
        }
        //���2
        if (Input.GetKeyDown(KeyCode.UpArrow) && P2_choose_state == CHOOSE_OK)
        {
            //����λ��
            P2_Y -= 1;
            if (P2_Y < 0)
                P2_Y = 2;
            P2_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P2_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P2_Y * CHOOSE_Y_DETA;
            P2_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //���Ŷ���
            P2_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //������Ч
            SoundManage.instance.ChooseingMove(2);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && P2_choose_state == CHOOSE_OK)
        {
            //����λ��
            P2_Y += 1;
            if (P2_Y > 2)
                P2_Y = 0;
            P2_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P2_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P2_Y * CHOOSE_Y_DETA;
            P2_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //���Ŷ���
            P2_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //������Ч
            SoundManage.instance.ChooseingMove(2);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && P2_choose_state == CHOOSE_OK)
        {
            //����λ��
            P2_X -= 1;
            if (P2_X < 0)
                P2_X = 3;
            P2_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P2_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P2_Y * CHOOSE_Y_DETA;
            P2_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //���Ŷ���
            P2_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //������Ч
            SoundManage.instance.ChooseingMove(2);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && P2_choose_state == CHOOSE_OK)
        {
            //����λ��
            P2_X += 1;
            if (P2_X > 3)
                P2_X = 0;
            P2_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P2_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P2_Y * CHOOSE_Y_DETA;
            P2_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //���Ŷ���
            P2_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //������Ч
            SoundManage.instance.ChooseingMove(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0) && P2_choose_state != CHOOSE_READY)
        {
            //���¿�������
            int index = P2_Y * 4 + P2_X;
            if (P2_plant.Contains(index))//����ûѡ�� �� ������ѡ�У��Ƴ�����
            {
                P2_plant.Remove(index);//�Ƴ�����
                //������ѡ����ͼƬ
                for (int i = 0; i < 6; i++)
                {
                    if (i < P2_plant.Count)
                    {
                        P2_plant_img[i].gameObject.SetActive(true);
                        P2_plant_img[i].sprite = PLANT_CARD[P2_plant[i]];
                    }
                    else if (i >= P2_plant.Count)
                    {
                        P2_plant_img[i].gameObject.SetActive(false);
                    }
                }
                //������Ч
                SoundManage.instance.ChooseingChoose(2);
            }
            else if (P2_plant.Count == 6)//�����Ѿ�ѡ�� �� ����δѡ�У�ȷ�Ͽ���
            {
                if (P1_choose_state != CHOOSE_READY)//���ֻ�δ׼�����
                {
                    P2_choose_state = CHOOSE_READY;
                    P2_lock.gameObject.SetActive(true);
                    //������Ч
                    SoundManage.instance.ChooseingLock(2);
                }
                else//�����Ѿ�ѡ����ɣ���Ϸ��ʼ
                {
                    GameReady();
                }
            }
            else//����δѡ�� �� ����δѡ�У�ѡ����
            {
                P2_plant.Add(index);//ѡ�п���
                //������ѡ����ͼƬ
                for (int i = 0; i < 6; i++)
                {
                    if (i < P2_plant.Count)
                    {
                        P2_plant_img[i].gameObject.SetActive(true);
                        P2_plant_img[i].sprite = PLANT_CARD[P2_plant[i]];
                    }
                    else if (i >= P2_plant.Count)
                    {
                        P2_plant_img[i].gameObject.SetActive(false);
                    }
                }
                //������Ч
                SoundManage.instance.ChooseingChoose(2);
            }
        }
    }

    //׼����ʼ��ս
    public void GameReady()
    {
        //����ѡ���ֲ��
        for(int i = 0; i < 6; i++)
        {
            PlayerPrefs.SetInt("P1_" + i, P1_plant[i]);
            PlayerPrefs.SetInt("P2_" + i, P2_plant[i]);
        }
        SceneManager.LoadScene(2);//���ض�ս����
    }
}
