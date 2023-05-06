using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //���峣��
    //��Ϸ״̬��1Ϊѡ���У�2Ϊ׼����ʼ��3Ϊ��ս�У�4ΪΪ��ͣ�У�5�����С�
    /*
    [HideInInspector] public int CHOOSE = 1;
    [HideInInspector] public int CHOOSE_OK = 10;//�ɽ��в���
    [HideInInspector] public int CHOOSE_MOVING_OUT = 11;//�л�ֲ���У�ֲ�������Ƴ�
    [HideInInspector] public int CHOOSE_MOVING_IN = 12;//�л�ֲ���У�ֲ����������
    [HideInInspector] public int CHOOSE_READY = 13;//��ȷ��ѡ�����*/
    [HideInInspector] public int READY = 2;
    [HideInInspector] public int GAME = 3;
    [HideInInspector] public int STOP = 4;
    [HideInInspector] public int END = 5;
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
    /*//ѡ��ֲ���г���
    [HideInInspector] public float CHOOSE_X = (float)-1.53;//ѡ�����׼Xλ��
    [HideInInspector] public float CHOOSE_Y = (float)1.33;//ѡ�����׼Yλ��
    [HideInInspector] public float CHOOSE_X_DETA = (float)1.02;//ѡ����Xλ����
    [HideInInspector] public float CHOOSE_Y_DETA = (float)-1.46;//ѡ����Yλ����*/
    //��ս�г���
    [HideInInspector] public float GAME_X = (float)-4.05;//ѡ����׼Xλ��
    [HideInInspector] public float GAME_Y = (float)2.33;//ѡ����׼Yλ��
    [HideInInspector] public float GAME_X_DETA = (float)1.075;//ѡ���Xλ����
    [HideInInspector] public float GAME_Y_DETA = (float)-1.35;//ѡ���Yλ����
    //ֲ����Ϣ
    public Sprite[] PLANT;//ֲ��ͼƬ
    public Sprite[] PLANT_CARD;//ֲ�￨ƬͼƬ
    public int[] SUN;//��ֲֲ����������
    public int[] CD;//ֲ����ȴʱ��
    public GameObject[] PLANT_PREFAB;//ֲ��Ԥ����

    //��Ϸ����
    public static GameManager instance;
    public int gameStatus;//��ǰ��Ϸ״̬
    public List<int> P1_plant;//���1ѡ���ֲ��
    public List<int> P2_plant;//���2ѡ���ֲ��
    public GameObject timer;//��ʱ��

    /*
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
    */

    //׼����ʼ
    public GameObject ready;//׼����ʼ��Ϸ��Ԥ����

    //��Ҷ�ս
    public GameObject slot;//����
    public GameObject stopPanel;//��ͣ����
    public GameObject gamePanel;//��ս����
    public int P1_sun = 200;//����
    public int P2_sun = 200;//����
    public int[,] battlefield = new int[5,9];//ս���ϵ�ֲ����1��Ϊ���������2��Ϊ����
    public int win;//ʤ�������
    //���1
    public GameObject P1_game_frame;//ѡ���
    public int P1_game_X;//ѡ���λ��
    public int P1_game_Y;
    public int P1_choose_plant;//������ѡ�е�ֲ��
    //���2
    public GameObject P2_game_frame;//ѡ���
    public int P2_game_X;//ѡ���λ��
    public int P2_game_Y;
    public int P2_choose_plant;//������ѡ�е�ֲ��

    //��������
    public GameObject endPanel;//��������
    /*
    public int test;
    public int testx;
    public int testy;*/

    private void Awake()
    {
        instance = this;
        //���÷ֱ��ʣ���ȫ��
        Screen.SetResolution(1920, 1080, false);
        //��ʼ��
        P1_plant = new List<int>();
        P2_plant = new List<int>();
        int[] sun = { 50, 100, 125, 50, 100, 200, 250, 125, 150, 125, 50, 175 };
        SUN = sun;
        //int[] cd = { 5, 5, 25, 25, 7, 7, 7, 10, 40, 40, 30, 10 };
        int[] cd = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        CD = cd;
    }

    void Start()
    {
        //Initialized();
        //��ȡѡ�е�ֲ��
        for (int i = 0; i < 6; i++)
        {
            P1_plant.Add(PlayerPrefs.GetInt("P1_" + i));
            P2_plant.Add(PlayerPrefs.GetInt("P2_" + i));
        }
        //׼����ʼ
        GameReady();
    }

    void Update()
    {
        //test = battlefield[testx, testy];//���ԣ��鿴�����ϵ�ֲ��
        //timer.GetComponent<Timer>().OutputRunTime();//�����Ϸ�Ѿ����е�ʱ��
        if (gameStatus == GAME || gameStatus == STOP)
        {
            //������Ӧ
            GameKeyResponse();
        }
    }

    /*
    //��ʼ��
    private void Initialized()
    {
        //��������
        SoundManage.instance.ChooseingMusic();
        //��ʾѡ������
        choosePanel.SetActive(true);
        //��ʼ����Ϸ״̬
        gameStatus = CHOOSE;
        //��ʼ��ѡ��λ��
        P1_X = 0;
        P1_Y = 0;
        P2_X = 3;
        P2_Y = 0;
        //���ؿհ׿�Ƭ
        int num = P1_plant.Count;
        for(int i = num; i < 6; i++)
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
        else if(Input.GetKeyDown(KeyCode.Space) && P1_choose_state != CHOOSE_READY)
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
            else if(P1_plant.Count == 6)//�����Ѿ�ѡ�� �� ����δѡ�У�ȷ�Ͽ���
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
            SoundManage.instance.ChooseingMove(1);
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
            SoundManage.instance.ChooseingMove(1);
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
            SoundManage.instance.ChooseingMove(1);
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
            SoundManage.instance.ChooseingMove(1);
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
                SoundManage.instance.ChooseingChoose(1);
            }
            else if (P2_plant.Count == 6)//�����Ѿ�ѡ�� �� ����δѡ�У�ȷ�Ͽ���
            {
                if (P1_choose_state != CHOOSE_READY)//���ֻ�δ׼�����
                {
                    P2_choose_state = CHOOSE_READY;
                    P2_lock.gameObject.SetActive(true);
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
                SoundManage.instance.ChooseingChoose(1);
            }
        }
    }
    */

    //�����ս�����У�������Ӧ�¼�
    private void GameKeyResponse()
    {
        //������Ӧ
        //��ͣ
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            if (gameStatus == STOP)//�������ͣ��������ͣ
            {
                GameContinue();
            }
            else//δ��ͣ��������ͣ
            {
                GameStop();
            }
        }
        if(gameStatus == GAME)
        {
            //���1
            if (Input.GetKeyDown(KeyCode.W))
            {
                //����λ��
                P1_game_Y -= 1;
                if (P1_game_Y < 0)
                    P1_game_Y = 0;
                float x = GAME_X + P1_game_X * GAME_X_DETA;
                float y = GAME_Y + P1_game_Y * GAME_Y_DETA;
                P1_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                //����λ��
                P1_game_Y += 1;
                if (P1_game_Y > 4)
                    P1_game_Y = 4;
                float x = GAME_X + P1_game_X * GAME_X_DETA;
                float y = GAME_Y + P1_game_Y * GAME_Y_DETA;
                P1_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                //����λ��
                P1_game_X -= 1;
                if (P1_game_X < 0)
                    P1_game_X = 0;
                float x = GAME_X + P1_game_X * GAME_X_DETA;
                float y = GAME_Y + P1_game_Y * GAME_Y_DETA;
                P1_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                //����λ��
                P1_game_X += 1;
                if (P1_game_X > 8)
                    P1_game_X = 8;
                float x = GAME_X + P1_game_X * GAME_X_DETA;
                float y = GAME_Y + P1_game_Y * GAME_Y_DETA;
                P1_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                P1_choose_plant = P1_plant[0];//ѡ��ֲ��
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameChoose(1);
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                P1_choose_plant = P1_plant[1];//ѡ��ֲ��
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameChoose(1);
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                P1_choose_plant = P1_plant[2];//ѡ��ֲ��
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameChoose(1);
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                P1_choose_plant = P1_plant[3];//ѡ��ֲ��
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameChoose(1);
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                P1_choose_plant = P1_plant[4];//ѡ��ֲ��
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameChoose(1);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                P1_choose_plant = P1_plant[5];//ѡ��ֲ��
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameChoose(1);
            }
            else if (Input.GetKeyDown(KeyCode.P))//�Ƴ���ǰλ���ϵ�ֲ��
            {
                //���ҵ�ǰλ���ϵ�ֲ��
                GameObject[] p = new GameObject[45];
                GameObject[] p0 = GameObject.FindGameObjectsWithTag("Player1");
                GameObject[] p1 = GameObject.FindGameObjectsWithTag("Player1_fire");
                int i = 0;
                for (i = 0; i < p0.Length; i++)
                {
                    p[i] = p0[i];
                }
                for (i = 0; i < p1.Length; i++)
                {
                    p[i + p0.Length] = p1[i];
                }
                for (int j = 0; j < i + p0.Length; j++)
                {
                    if (p[j].GetComponent<Plant>().x == P1_game_X && p[j].GetComponent<Plant>().y == P1_game_Y)
                    {
                        p[j].GetComponent<Plant>().Dead();//ֲ���Ƴ�
                        break;
                    }
                }
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameRemove(1);
            }
            if (Input.GetKeyDown(KeyCode.Space) && CanPlant(1, P1_game_X, P1_game_Y))
            {
                //����ֲ��
                GameObject p1 = Instantiate(PLANT_PREFAB[P1_choose_plant], new Vector3(P1_game_frame.transform.position.x, P1_game_frame.transform.position.y, 0), Quaternion.identity);//����ֲ��
                p1.GetComponent<Plant>().Init(1, P1_game_X, P1_game_Y);
                battlefield[P1_game_Y, P1_game_X] = P1_choose_plant + 1;
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                img.sprite = PLANT[12];//���ɿհ�ͼ
                timer.GetComponent<Timer>().Planting(1, P1_choose_plant);//������ȴ
                P1_sun -= SUN[P1_choose_plant];//�������
                Text sun = slot.transform.GetChild(0).GetComponentsInChildren<Text>()[0];
                sun.text = P1_sun.ToString();
                //������Ч
                SoundManage.instance.GamePlant(1);
                //�ж���Ϸ�Ƿ����
                if (P1_game_X == 8)
                {
                    PlayerPrefs.SetInt("win", 1);
                    Invoke("GameOver", 0.2f);
                }
            }
            //���2
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //����λ��
                P2_game_Y -= 1;
                if (P2_game_Y < 0)
                    P2_game_Y = 0;
                float x = GAME_X + P2_game_X * GAME_X_DETA;
                float y = GAME_Y + P2_game_Y * GAME_Y_DETA;
                P2_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //����λ��
                P2_game_Y += 1;
                if (P2_game_Y > 4)
                    P2_game_Y = 4;
                float x = GAME_X + P2_game_X * GAME_X_DETA;
                float y = GAME_Y + P2_game_Y * GAME_Y_DETA;
                P2_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //����λ��
                P2_game_X -= 1;
                if (P2_game_X < 0)
                    P2_game_X = 0;
                float x = GAME_X + P2_game_X * GAME_X_DETA;
                float y = GAME_Y + P2_game_Y * GAME_Y_DETA;
                P2_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //����λ��
                P2_game_X += 1;
                if (P2_game_X > 8)
                    P2_game_X = 8;
                float x = GAME_X + P2_game_X * GAME_X_DETA;
                float y = GAME_Y + P2_game_Y * GAME_Y_DETA;
                P2_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                P2_choose_plant = P2_plant[0];//ѡ��ֲ��
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameChoose(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                P2_choose_plant = P2_plant[1];//ѡ��ֲ��
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameChoose(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                P2_choose_plant = P2_plant[2];//ѡ��ֲ��
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameChoose(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                P2_choose_plant = P2_plant[3];//ѡ��ֲ��
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameChoose(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                P2_choose_plant = P2_plant[4];//ѡ��ֲ��
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameChoose(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                P2_choose_plant = P2_plant[5];//ѡ��ֲ��
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameChoose(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad9))//�Ƴ���ǰλ���ϵ�ֲ��
            {
                //���ҵ�ǰλ���ϵ�ֲ��
                GameObject[] p = new GameObject[45];
                GameObject[] p0 = GameObject.FindGameObjectsWithTag("Player2");
                GameObject[] p1 = GameObject.FindGameObjectsWithTag("Player2_fire");
                int i = 0;
                for (i = 0; i < p0.Length; i++)
                {
                    p[i] = p0[i];
                }
                for (i = 0; i < p1.Length; i++)
                {
                    p[i + p0.Length] = p1[i];
                }
                for (int j = 0; j < i + p0.Length; j++)
                {
                    if (p[j].GetComponent<Plant>().x == P2_game_X && p[j].GetComponent<Plant>().y == P2_game_Y)
                    {
                        p[j].GetComponent<Plant>().Dead();//ֲ������
                        break;
                    }
                }
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//��������
                else
                    img.sprite = PLANT[12];//���ɿհ�ͼ
                //������Ч
                SoundManage.instance.GameRemove(2);
            }
            if (Input.GetKeyDown(KeyCode.Keypad0) && CanPlant(2, P2_game_X, P2_game_Y))
            {
                //����ֲ��
                GameObject p2 = Instantiate(PLANT_PREFAB[P2_choose_plant], new Vector3(P2_game_frame.transform.position.x, P2_game_frame.transform.position.y, 0), Quaternion.identity);//����ֲ��
                p2.GetComponent<Plant>().Init(2, P2_game_X, P2_game_Y);
                battlefield[P2_game_Y, P2_game_X] = -1 * P2_choose_plant - 1;
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                img.sprite = PLANT[12];//���ɿհ�ͼ
                timer.GetComponent<Timer>().Planting(2, P2_choose_plant);//������ȴ
                P2_sun -= SUN[P2_choose_plant];//�������
                Text sun = slot.transform.GetChild(1).GetComponentsInChildren<Text>()[0];
                sun.text = P2_sun.ToString();
                //������Ч
                SoundManage.instance.GamePlant(2);
                //�ж���Ϸ�Ƿ����
                if (P2_game_X == 0)
                {
                    PlayerPrefs.SetInt("win", 1);
                    Invoke("GameOver", 0.2f);
                }
            }

        }
    }

    //׼����ʼ��ս
    public void GameReady()
    {
        //������Ч
        SoundManage.instance.ReadyMusic();
        //����Ԥ����
        GameObject readyPrefab = Instantiate(ready, new Vector3(0, 0, 0), Quaternion.identity);
    }

    //��ʼ��Ϸ
    public void GameStart()
    {
        //��������
        SoundManage.instance.GammingMusic();

        gameStatus = GAME;//�л�״̬
        //choosePanel.SetActive(false);
        slot.SetActive(true);
        //���¿���
        for(int i = 0; i < 6; i++)
        {
            Image t1 = slot.transform.GetChild(0).GetChild(i + 1).GetChild(0).GetComponent<Image>();
            t1.sprite = PLANT[P1_plant[i]];
            Text s1 = slot.transform.GetChild(0).GetChild(i + 1).GetChild(1).GetComponent<Text>();
            s1.text = SUN[P1_plant[i]].ToString();
            Image t2 = slot.transform.GetChild(1).GetChild(i + 1).GetChild(0).GetComponent<Image>();
            t2.sprite = PLANT[P2_plant[i]];
            Text s2 = slot.transform.GetChild(1).GetChild(i + 1).GetChild(1).GetComponent<Text>();
            s2.text = SUN[P2_plant[i]].ToString();
        }
        //��������
        Text sun1 = slot.transform.GetChild(0).GetComponentsInChildren<Text>()[0];
        sun1.text = P1_sun.ToString();
        Text sun2 = slot.transform.GetChild(1).GetComponentsInChildren<Text>()[0];
        sun2.text = P2_sun.ToString();
        //������ʱ��
        timer.GetComponent<Timer>().TimeStart();
        //��ʼ����սʱ��ѡ���
        gamePanel.SetActive(true);
        P1_game_X = 0;
        P1_game_Y = 0;
        P1_choose_plant = P1_plant[0];
        SpriteRenderer img1 = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
        img1.sprite = PLANT[P1_choose_plant];//��������
        P2_game_X = 8;
        P2_game_Y = 0;
        P2_choose_plant = P2_plant[0];
        SpriteRenderer img2 = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
        img2.sprite = PLANT[P2_choose_plant];//��������
        //��ʼ����ս����
        for (int i=0; i<5; i++)
        {
            for(int j=0; j<9; j++)
            {
                battlefield[i, j] = 999;
            }
        }
        battlefield[0, 4] = 0;
        battlefield[1, 4] = 0;
        battlefield[2, 4] = 0;
        battlefield[3, 4] = 0;
        battlefield[4, 4] = 0;
    }

    //��ͣ��Ϸ
    public void GameStop()
    {
        Time.timeScale = 0;
        gameStatus = STOP;
        stopPanel.SetActive(true);
        timer.GetComponent<Timer>().TimeStop();//��ʱ����ͣ
    }

    //������Ϸ
    public void GameContinue()
    {
        Time.timeScale = 1;
        gameStatus = GAME;
        stopPanel.SetActive(false);
        timer.GetComponent<Timer>().TimeContinue();//��ʱ���ָ�
    }

    //�ж��ܷ���ֲ
    public bool CanPlant(int player, int x, int y)
    {
        if (player == 1)
        {
            //��ȴ�� �� ���ⲻ�����޷���ֲ
            if (!timer.GetComponent<Timer>().IsCD(player, P1_choose_plant) || P1_sun < SUN[P1_choose_plant])
                return false;
            if(battlefield[y, x] < 100 && battlefield[y, x] > -100)//��λ������ֲ�������ֲ
                return false;
            if (x == 0)//�������ݣ�������ֲ
                return true;
            //�ж���ΧһȦ�Ƿ���ֲ����������ֲ
            else if(x - 1 >= 0 && y - 1 >= 0 && battlefield[y - 1, x - 1] > 0 && battlefield[y - 1, x - 1] < 100)//����
                return true;
            else if(x - 1 >= 0 && battlefield[y, x - 1] > 0 && battlefield[y , x - 1] < 100)//����
                return true;
            else if (x - 1 >= 0 && y + 1 < 5 && battlefield[y + 1, x - 1] > 0 && battlefield[y + 1, x - 1] < 100)//����
                return true;
            else if (y - 1 >= 0 && battlefield[y - 1, x] > 0 && battlefield[y - 1, x] < 100)//��
                return true;
            else if (y + 1 < 5 && battlefield[y + 1, x] > 0 && battlefield[y + 1, x] < 100)//��
                return true;
            else if (x + 1 < 9 && y - 1 >= 0 && battlefield[y - 1, x + 1] > 0 && battlefield[y - 1, x + 1] < 100)//����
                return true;
            else if (x + 1 < 9 && battlefield[y, x + 1] > 0 && battlefield[y, x + 1] < 100)//����
                return true;
            else if (x + 1 < 9 && y + 1 < 5 && battlefield[y + 1, x + 1] > 0 && battlefield[y + 1, x + 1] < 100)//����
                return true;
        }
        else if (player == 2)
        {
            //��ȴ�� �� ���ⲻ�����޷���ֲ
            if (!timer.GetComponent<Timer>().IsCD(player, P2_choose_plant) || P2_sun < SUN[P2_choose_plant])
                return false;
            if (battlefield[y, x] < 100 && battlefield[y, x] > -100)//��λ������ֲ�������ֲ
                return false;
            if (x == 8)//�������ݣ�������ֲ
                return true;
            //�ж���ΧһȦ�Ƿ���ֲ����������ֲ
            else if (x - 1 >= 0 && y - 1 >= 0 && battlefield[y - 1, x - 1] < 0 && battlefield[y - 1, x - 1] > -100)//����
                return true;
            else if (x - 1 >= 0 && battlefield[y, x - 1] < 0 && battlefield[y, x - 1] > -100)//����
                return true;
            else if (x - 1 >= 0 && y + 1 < 5 && battlefield[y + 1, x - 1] < 0 && battlefield[y + 1, x - 1] > -100)//����
                return true;
            else if (y - 1 >= 0 && battlefield[y - 1, x] < 0 && battlefield[y - 1, x] > -100)//��
                return true;
            else if (y + 1 < 5 && battlefield[y + 1, x] < 0 && battlefield[y + 1, x] > -100)//��
                return true;
            else if (x + 1 < 9 && y - 1 >= 0 && battlefield[y - 1, x + 1] < 0 && battlefield[y - 1, x + 1] > -100)//����
                return true;
            else if (x + 1 < 9 && battlefield[y, x + 1] < 0 && battlefield[y, x + 1] > -100)//����
                return true;
            else if (x + 1 < 9 && y + 1 < 5 && battlefield[y + 1, x + 1] < 0 && battlefield[y + 1, x + 1] > -100)//����
                return true;
        }
        return false;
    }

    //��Ϸ����
    public void GameOver()
    {
        gameStatus = END;//������Ϸ״̬
        timer.GetComponent<Timer>().TimeEnd();//ֹͣ��ʱ
        Time.timeScale = 0;
        SceneManager.LoadScene(3);//�������ҳ��
    }
    /*
    //���¿�ʼ��Ϸ
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    //�������˵�
    public void Back()
    {
        SceneManager.LoadScene(0);
    }*/

}

/*//������б��������
PlayerPrefs.DeleteAll();*/