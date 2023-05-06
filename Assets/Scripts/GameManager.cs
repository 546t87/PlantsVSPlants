using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //定义常量
    //游戏状态，1为选卡中，2为准备开始，3为对战中，4为为暂停中，5结算中。
    /*
    [HideInInspector] public int CHOOSE = 1;
    [HideInInspector] public int CHOOSE_OK = 10;//可进行操作
    [HideInInspector] public int CHOOSE_MOVING_OUT = 11;//切换植物中，植物正在移出
    [HideInInspector] public int CHOOSE_MOVING_IN = 12;//切换植物中，植物正在移入
    [HideInInspector] public int CHOOSE_READY = 13;//已确认选择完毕*/
    [HideInInspector] public int READY = 2;
    [HideInInspector] public int GAME = 3;
    [HideInInspector] public int STOP = 4;
    [HideInInspector] public int END = 5;
    //植物类型
    [HideInInspector] public int SUN_FLOWER = 1;//向日葵
    [HideInInspector] public int TWIN_SUN_FLOWER = 2;//双子向日葵
    [HideInInspector] public int TALL_NUT = 3;//高坚果
    [HideInInspector] public int WALL_NUT = 4;//坚果墙
    [HideInInspector] public int PEA_SHOOTER = 5;//豌豆射手
    [HideInInspector] public int REPEATER = 6;//双发射手
    [HideInInspector] public int THREE_PEATER = 7;//三线射手
    [HideInInspector] public int CACTUS = 8;//仙人掌
    [HideInInspector] public int CHERRY_BOOM = 9;//樱桃炸弹
    [HideInInspector] public int JALAPENO = 10;//火爆辣椒
    [HideInInspector] public int SQUASH = 11;//倭瓜
    [HideInInspector] public int TORCH_WOOD = 12;//火炬树桩
    /*//选择植物中常量
    [HideInInspector] public float CHOOSE_X = (float)-1.53;//选卡框基准X位置
    [HideInInspector] public float CHOOSE_Y = (float)1.33;//选卡框基准Y位置
    [HideInInspector] public float CHOOSE_X_DETA = (float)1.02;//选卡框X位移量
    [HideInInspector] public float CHOOSE_Y_DETA = (float)-1.46;//选卡框Y位移量*/
    //对战中常量
    [HideInInspector] public float GAME_X = (float)-4.05;//选择框基准X位置
    [HideInInspector] public float GAME_Y = (float)2.33;//选择框基准Y位置
    [HideInInspector] public float GAME_X_DETA = (float)1.075;//选择框X位移量
    [HideInInspector] public float GAME_Y_DETA = (float)-1.35;//选择框Y位移量
    //植物信息
    public Sprite[] PLANT;//植物图片
    public Sprite[] PLANT_CARD;//植物卡片图片
    public int[] SUN;//种植植物所需阳光
    public int[] CD;//植物冷却时间
    public GameObject[] PLANT_PREFAB;//植物预制体

    //游戏控制
    public static GameManager instance;
    public int gameStatus;//当前游戏状态
    public List<int> P1_plant;//玩家1选择的植物
    public List<int> P2_plant;//玩家2选择的植物
    public GameObject timer;//计时器

    /*
    //选择植物
    public GameObject choosePanel;//选择界面
    //玩家1
    public GameObject P1_choose_frame;//选择框
    public int P1_X;//选择框所在位置
    public int P1_Y;
    public GameObject P1_big_plant;//当前选中的植物
    public SpriteRenderer[] P1_plant_img = new SpriteRenderer[6];//已选择的植物图片
    public int P1_choose_state;//选卡状态
    public SpriteRenderer P1_lock;//锁头
    //玩家2
    public GameObject P2_choose_frame;//选择框
    public int P2_X;
    public int P2_Y;
    public GameObject P2_big_plant;//当前选中的植物
    public SpriteRenderer[] P2_plant_img = new SpriteRenderer[6];//已选择的植物图片
    public int P2_choose_state;//选卡状态
    public SpriteRenderer P2_lock;//锁头
    */

    //准备开始
    public GameObject ready;//准备开始游戏的预制体

    //玩家对战
    public GameObject slot;//卡槽
    public GameObject stopPanel;//暂停界面
    public GameObject gamePanel;//对战界面
    public int P1_sun = 200;//阳光
    public int P2_sun = 200;//阳光
    public int[,] battlefield = new int[5,9];//战场上的植物，玩家1的为正数，玩家2的为负数
    public int win;//胜利的玩家
    //玩家1
    public GameObject P1_game_frame;//选择框
    public int P1_game_X;//选择的位置
    public int P1_game_Y;
    public int P1_choose_plant;//卡槽中选中的植物
    //玩家2
    public GameObject P2_game_frame;//选择框
    public int P2_game_X;//选择的位置
    public int P2_game_Y;
    public int P2_choose_plant;//卡槽中选中的植物

    //结束界面
    public GameObject endPanel;//结束界面
    /*
    public int test;
    public int testx;
    public int testy;*/

    private void Awake()
    {
        instance = this;
        //设置分辨率，不全屏
        Screen.SetResolution(1920, 1080, false);
        //初始化
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
        //获取选中的植物
        for (int i = 0; i < 6; i++)
        {
            P1_plant.Add(PlayerPrefs.GetInt("P1_" + i));
            P2_plant.Add(PlayerPrefs.GetInt("P2_" + i));
        }
        //准备开始
        GameReady();
    }

    void Update()
    {
        //test = battlefield[testx, testy];//测试，查看格子上的植物
        //timer.GetComponent<Timer>().OutputRunTime();//输出游戏已经运行的时间
        if (gameStatus == GAME || gameStatus == STOP)
        {
            //键盘响应
            GameKeyResponse();
        }
    }

    /*
    //初始化
    private void Initialized()
    {
        //播放音乐
        SoundManage.instance.ChooseingMusic();
        //显示选卡界面
        choosePanel.SetActive(true);
        //初始化游戏状态
        gameStatus = CHOOSE;
        //初始化选卡位置
        P1_X = 0;
        P1_Y = 0;
        P2_X = 3;
        P2_Y = 0;
        //隐藏空白卡片
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
        //初始化选卡状态
        P1_choose_state = CHOOSE_OK;
        P2_choose_state = CHOOSE_OK;
    }

    //处理选择植物过程中，键盘响应事件
    private void ChooseKeyResponse()
    {
        //按键响应
        //玩家1
        if (Input.GetKeyDown(KeyCode.W) && P1_choose_state == CHOOSE_OK)
        {
            //更新位置
            P1_Y -= 1;
            if (P1_Y < 0)
                P1_Y = 2;
            P1_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P1_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P1_Y * CHOOSE_Y_DETA;
            P1_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //播放动画
            P1_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //播放音效
            SoundManage.instance.ChooseingMove(1);
        }
        else if (Input.GetKeyDown(KeyCode.S) && P1_choose_state == CHOOSE_OK)
        {
            //更新位置
            P1_Y += 1;
            if (P1_Y > 2)
                P1_Y = 0;
            P1_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P1_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P1_Y * CHOOSE_Y_DETA;
            P1_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //播放动画
            P1_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //播放音效
            SoundManage.instance.ChooseingMove(1);
        }
        else if (Input.GetKeyDown(KeyCode.A) && P1_choose_state == CHOOSE_OK)
        {
            //更新位置
            P1_X -= 1;
            if (P1_X < 0)
                P1_X = 3;
            P1_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P1_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P1_Y * CHOOSE_Y_DETA;
            P1_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //播放动画
            P1_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //播放音效
            SoundManage.instance.ChooseingMove(1);
        }
        else if (Input.GetKeyDown(KeyCode.D) && P1_choose_state == CHOOSE_OK)
        {
            //更新位置
            P1_X += 1;
            if (P1_X > 3)
                P1_X = 0;
            P1_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P1_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P1_Y * CHOOSE_Y_DETA;
            P1_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //播放动画
            P1_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //播放音效
            SoundManage.instance.ChooseingMove(1);
        }
        else if(Input.GetKeyDown(KeyCode.Space) && P1_choose_state != CHOOSE_READY)
        {
            //更新卡牌数组
            int index = P1_Y * 4 + P1_X;
            if (P1_plant.Contains(index))//卡牌没选满 且 卡牌已选中，移除卡牌
            {
                P1_plant.Remove(index);//移出卡牌
                //更新已选择卡牌图片
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
                //播放音效
                SoundManage.instance.ChooseingChoose(1);
            }
            else if(P1_plant.Count == 6)//卡牌已经选满 且 卡牌未选中，确认卡牌
            {
                if (P2_choose_state != CHOOSE_READY)//对手还未准备完成
                {
                    P1_choose_state = CHOOSE_READY;
                    P1_lock.gameObject.SetActive(true);
                    //播放音效
                    SoundManage.instance.ChooseingLock(1);
                }
                else//对手已经选择完成，游戏开始
                {
                    GameReady();
                }
            }
            else//卡牌未选满 且 卡牌未选中，选择卡牌
            {
                P1_plant.Add(index);//选中卡牌
                //更新已选择卡牌图片
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
                //播放音效
                SoundManage.instance.ChooseingChoose(1);
            }
        }
        //玩家2
        if (Input.GetKeyDown(KeyCode.UpArrow) && P2_choose_state == CHOOSE_OK)
        {
            //更新位置
            P2_Y -= 1;
            if (P2_Y < 0)
                P2_Y = 2;
            P2_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P2_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P2_Y * CHOOSE_Y_DETA;
            P2_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //播放动画
            P2_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //播放音效
            SoundManage.instance.ChooseingMove(1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && P2_choose_state == CHOOSE_OK)
        {
            //更新位置
            P2_Y += 1;
            if (P2_Y > 2)
                P2_Y = 0;
            P2_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P2_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P2_Y * CHOOSE_Y_DETA;
            P2_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //播放动画
            P2_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //播放音效
            SoundManage.instance.ChooseingMove(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && P2_choose_state == CHOOSE_OK)
        {
            //更新位置
            P2_X -= 1;
            if (P2_X < 0)
                P2_X = 3;
            P2_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P2_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P2_Y * CHOOSE_Y_DETA;
            P2_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //播放动画
            P2_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //播放音效
            SoundManage.instance.ChooseingMove(1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && P2_choose_state == CHOOSE_OK)
        {
            //更新位置
            P2_X += 1;
            if (P2_X > 3)
                P2_X = 0;
            P2_choose_state = CHOOSE_MOVING_OUT;
            float x = CHOOSE_X + P2_X * CHOOSE_X_DETA;
            float y = CHOOSE_Y + P2_Y * CHOOSE_Y_DETA;
            P2_choose_frame.transform.localPosition = new Vector3(x, y, 0);
            //播放动画
            P2_big_plant.GetComponent<Animator>().SetInteger("move_state", 1);
            //播放音效
            SoundManage.instance.ChooseingMove(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0) && P2_choose_state != CHOOSE_READY)
        {
            //更新卡牌数组
            int index = P2_Y * 4 + P2_X;
            if (P2_plant.Contains(index))//卡牌没选满 且 卡牌已选中，移除卡牌
            {
                P2_plant.Remove(index);//移出卡牌
                //更新已选择卡牌图片
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
                //播放音效
                SoundManage.instance.ChooseingChoose(1);
            }
            else if (P2_plant.Count == 6)//卡牌已经选满 且 卡牌未选中，确认卡牌
            {
                if (P1_choose_state != CHOOSE_READY)//对手还未准备完成
                {
                    P2_choose_state = CHOOSE_READY;
                    P2_lock.gameObject.SetActive(true);
                    //播放音效
                    SoundManage.instance.ChooseingLock(1);
                }
                else//对手已经选择完成，游戏开始
                {
                    GameReady();
                }
            }
            else//卡牌未选满 且 卡牌未选中，选择卡牌
            {
                P2_plant.Add(index);//选中卡牌
                //更新已选择卡牌图片
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
                //播放音效
                SoundManage.instance.ChooseingChoose(1);
            }
        }
    }
    */

    //处理对战过程中，键盘响应事件
    private void GameKeyResponse()
    {
        //按键响应
        //暂停
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            if (gameStatus == STOP)//如果已暂停，则解除暂停
            {
                GameContinue();
            }
            else//未暂停，进入暂停
            {
                GameStop();
            }
        }
        if(gameStatus == GAME)
        {
            //玩家1
            if (Input.GetKeyDown(KeyCode.W))
            {
                //更新位置
                P1_game_Y -= 1;
                if (P1_game_Y < 0)
                    P1_game_Y = 0;
                float x = GAME_X + P1_game_X * GAME_X_DETA;
                float y = GAME_Y + P1_game_Y * GAME_Y_DETA;
                P1_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                //更新位置
                P1_game_Y += 1;
                if (P1_game_Y > 4)
                    P1_game_Y = 4;
                float x = GAME_X + P1_game_X * GAME_X_DETA;
                float y = GAME_Y + P1_game_Y * GAME_Y_DETA;
                P1_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                //更新位置
                P1_game_X -= 1;
                if (P1_game_X < 0)
                    P1_game_X = 0;
                float x = GAME_X + P1_game_X * GAME_X_DETA;
                float y = GAME_Y + P1_game_Y * GAME_Y_DETA;
                P1_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                //更新位置
                P1_game_X += 1;
                if (P1_game_X > 8)
                    P1_game_X = 8;
                float x = GAME_X + P1_game_X * GAME_X_DETA;
                float y = GAME_Y + P1_game_Y * GAME_Y_DETA;
                P1_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                P1_choose_plant = P1_plant[0];//选择植物
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameChoose(1);
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                P1_choose_plant = P1_plant[1];//选择植物
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameChoose(1);
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                P1_choose_plant = P1_plant[2];//选择植物
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameChoose(1);
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                P1_choose_plant = P1_plant[3];//选择植物
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameChoose(1);
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                P1_choose_plant = P1_plant[4];//选择植物
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameChoose(1);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                P1_choose_plant = P1_plant[5];//选择植物
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameChoose(1);
            }
            else if (Input.GetKeyDown(KeyCode.P))//移除当前位置上的植物
            {
                //查找当前位置上的植物
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
                        p[j].GetComponent<Plant>().Dead();//植物移除
                        break;
                    }
                }
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P1_game_Y, P1_game_X] > 100 || battlefield[P1_game_Y, P1_game_X] < -100)
                    img.sprite = PLANT[P1_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameRemove(1);
            }
            if (Input.GetKeyDown(KeyCode.Space) && CanPlant(1, P1_game_X, P1_game_Y))
            {
                //生成植物
                GameObject p1 = Instantiate(PLANT_PREFAB[P1_choose_plant], new Vector3(P1_game_frame.transform.position.x, P1_game_frame.transform.position.y, 0), Quaternion.identity);//生成植物
                p1.GetComponent<Plant>().Init(1, P1_game_X, P1_game_Y);
                battlefield[P1_game_Y, P1_game_X] = P1_choose_plant + 1;
                SpriteRenderer img = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                img.sprite = PLANT[12];//换成空白图
                timer.GetComponent<Timer>().Planting(1, P1_choose_plant);//进入冷却
                P1_sun -= SUN[P1_choose_plant];//阳光减少
                Text sun = slot.transform.GetChild(0).GetComponentsInChildren<Text>()[0];
                sun.text = P1_sun.ToString();
                //播放音效
                SoundManage.instance.GamePlant(1);
                //判断游戏是否结束
                if (P1_game_X == 8)
                {
                    PlayerPrefs.SetInt("win", 1);
                    Invoke("GameOver", 0.2f);
                }
            }
            //玩家2
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //更新位置
                P2_game_Y -= 1;
                if (P2_game_Y < 0)
                    P2_game_Y = 0;
                float x = GAME_X + P2_game_X * GAME_X_DETA;
                float y = GAME_Y + P2_game_Y * GAME_Y_DETA;
                P2_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //更新位置
                P2_game_Y += 1;
                if (P2_game_Y > 4)
                    P2_game_Y = 4;
                float x = GAME_X + P2_game_X * GAME_X_DETA;
                float y = GAME_Y + P2_game_Y * GAME_Y_DETA;
                P2_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //更新位置
                P2_game_X -= 1;
                if (P2_game_X < 0)
                    P2_game_X = 0;
                float x = GAME_X + P2_game_X * GAME_X_DETA;
                float y = GAME_Y + P2_game_Y * GAME_Y_DETA;
                P2_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //更新位置
                P2_game_X += 1;
                if (P2_game_X > 8)
                    P2_game_X = 8;
                float x = GAME_X + P2_game_X * GAME_X_DETA;
                float y = GAME_Y + P2_game_Y * GAME_Y_DETA;
                P2_game_frame.transform.localPosition = new Vector3(x, y, 0);
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                P2_choose_plant = P2_plant[0];//选择植物
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameChoose(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                P2_choose_plant = P2_plant[1];//选择植物
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameChoose(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                P2_choose_plant = P2_plant[2];//选择植物
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameChoose(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                P2_choose_plant = P2_plant[3];//选择植物
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameChoose(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                P2_choose_plant = P2_plant[4];//选择植物
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameChoose(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                P2_choose_plant = P2_plant[5];//选择植物
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameChoose(2);
            }
            else if (Input.GetKeyDown(KeyCode.Keypad9))//移除当前位置上的植物
            {
                //查找当前位置上的植物
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
                        p[j].GetComponent<Plant>().Dead();//植物死亡
                        break;
                    }
                }
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                if (battlefield[P2_game_Y, P2_game_X] > 100 || battlefield[P2_game_Y, P2_game_X] < -100)
                    img.sprite = PLANT[P2_choose_plant];//更新虚像
                else
                    img.sprite = PLANT[12];//换成空白图
                //播放音效
                SoundManage.instance.GameRemove(2);
            }
            if (Input.GetKeyDown(KeyCode.Keypad0) && CanPlant(2, P2_game_X, P2_game_Y))
            {
                //生成植物
                GameObject p2 = Instantiate(PLANT_PREFAB[P2_choose_plant], new Vector3(P2_game_frame.transform.position.x, P2_game_frame.transform.position.y, 0), Quaternion.identity);//生成植物
                p2.GetComponent<Plant>().Init(2, P2_game_X, P2_game_Y);
                battlefield[P2_game_Y, P2_game_X] = -1 * P2_choose_plant - 1;
                SpriteRenderer img = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
                img.sprite = PLANT[12];//换成空白图
                timer.GetComponent<Timer>().Planting(2, P2_choose_plant);//进入冷却
                P2_sun -= SUN[P2_choose_plant];//阳光减少
                Text sun = slot.transform.GetChild(1).GetComponentsInChildren<Text>()[0];
                sun.text = P2_sun.ToString();
                //播放音效
                SoundManage.instance.GamePlant(2);
                //判断游戏是否结束
                if (P2_game_X == 0)
                {
                    PlayerPrefs.SetInt("win", 1);
                    Invoke("GameOver", 0.2f);
                }
            }

        }
    }

    //准备开始对战
    public void GameReady()
    {
        //播放音效
        SoundManage.instance.ReadyMusic();
        //生成预制体
        GameObject readyPrefab = Instantiate(ready, new Vector3(0, 0, 0), Quaternion.identity);
    }

    //开始游戏
    public void GameStart()
    {
        //播放音乐
        SoundManage.instance.GammingMusic();

        gameStatus = GAME;//切换状态
        //choosePanel.SetActive(false);
        slot.SetActive(true);
        //更新卡槽
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
        //更新阳光
        Text sun1 = slot.transform.GetChild(0).GetComponentsInChildren<Text>()[0];
        sun1.text = P1_sun.ToString();
        Text sun2 = slot.transform.GetChild(1).GetComponentsInChildren<Text>()[0];
        sun2.text = P2_sun.ToString();
        //启动计时器
        timer.GetComponent<Timer>().TimeStart();
        //初始化对战时的选择框
        gamePanel.SetActive(true);
        P1_game_X = 0;
        P1_game_Y = 0;
        P1_choose_plant = P1_plant[0];
        SpriteRenderer img1 = P1_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
        img1.sprite = PLANT[P1_choose_plant];//更新虚像
        P2_game_X = 8;
        P2_game_Y = 0;
        P2_choose_plant = P2_plant[0];
        SpriteRenderer img2 = P2_game_frame.transform.GetChild(1).GetComponent<SpriteRenderer>();
        img2.sprite = PLANT[P2_choose_plant];//更新虚像
        //初始化对战场景
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

    //暂停游戏
    public void GameStop()
    {
        Time.timeScale = 0;
        gameStatus = STOP;
        stopPanel.SetActive(true);
        timer.GetComponent<Timer>().TimeStop();//计时器暂停
    }

    //继续游戏
    public void GameContinue()
    {
        Time.timeScale = 1;
        gameStatus = GAME;
        stopPanel.SetActive(false);
        timer.GetComponent<Timer>().TimeContinue();//计时器恢复
    }

    //判断能否种植
    public bool CanPlant(int player, int x, int y)
    {
        if (player == 1)
        {
            //冷却中 或 阳光不够，无法种植
            if (!timer.GetComponent<Timer>().IsCD(player, P1_choose_plant) || P1_sun < SUN[P1_choose_plant])
                return false;
            if(battlefield[y, x] < 100 && battlefield[y, x] > -100)//该位置已有植物，不能种植
                return false;
            if (x == 0)//靠近房屋，可以种植
                return true;
            //判断周围一圈是否有植物，有则可以种植
            else if(x - 1 >= 0 && y - 1 >= 0 && battlefield[y - 1, x - 1] > 0 && battlefield[y - 1, x - 1] < 100)//左上
                return true;
            else if(x - 1 >= 0 && battlefield[y, x - 1] > 0 && battlefield[y , x - 1] < 100)//左中
                return true;
            else if (x - 1 >= 0 && y + 1 < 5 && battlefield[y + 1, x - 1] > 0 && battlefield[y + 1, x - 1] < 100)//左下
                return true;
            else if (y - 1 >= 0 && battlefield[y - 1, x] > 0 && battlefield[y - 1, x] < 100)//上
                return true;
            else if (y + 1 < 5 && battlefield[y + 1, x] > 0 && battlefield[y + 1, x] < 100)//下
                return true;
            else if (x + 1 < 9 && y - 1 >= 0 && battlefield[y - 1, x + 1] > 0 && battlefield[y - 1, x + 1] < 100)//右上
                return true;
            else if (x + 1 < 9 && battlefield[y, x + 1] > 0 && battlefield[y, x + 1] < 100)//右中
                return true;
            else if (x + 1 < 9 && y + 1 < 5 && battlefield[y + 1, x + 1] > 0 && battlefield[y + 1, x + 1] < 100)//右下
                return true;
        }
        else if (player == 2)
        {
            //冷却中 或 阳光不够，无法种植
            if (!timer.GetComponent<Timer>().IsCD(player, P2_choose_plant) || P2_sun < SUN[P2_choose_plant])
                return false;
            if (battlefield[y, x] < 100 && battlefield[y, x] > -100)//该位置已有植物，不能种植
                return false;
            if (x == 8)//靠近房屋，可以种植
                return true;
            //判断周围一圈是否有植物，有则可以种植
            else if (x - 1 >= 0 && y - 1 >= 0 && battlefield[y - 1, x - 1] < 0 && battlefield[y - 1, x - 1] > -100)//左上
                return true;
            else if (x - 1 >= 0 && battlefield[y, x - 1] < 0 && battlefield[y, x - 1] > -100)//左中
                return true;
            else if (x - 1 >= 0 && y + 1 < 5 && battlefield[y + 1, x - 1] < 0 && battlefield[y + 1, x - 1] > -100)//左下
                return true;
            else if (y - 1 >= 0 && battlefield[y - 1, x] < 0 && battlefield[y - 1, x] > -100)//上
                return true;
            else if (y + 1 < 5 && battlefield[y + 1, x] < 0 && battlefield[y + 1, x] > -100)//下
                return true;
            else if (x + 1 < 9 && y - 1 >= 0 && battlefield[y - 1, x + 1] < 0 && battlefield[y - 1, x + 1] > -100)//右上
                return true;
            else if (x + 1 < 9 && battlefield[y, x + 1] < 0 && battlefield[y, x + 1] > -100)//右中
                return true;
            else if (x + 1 < 9 && y + 1 < 5 && battlefield[y + 1, x + 1] < 0 && battlefield[y + 1, x + 1] > -100)//右下
                return true;
        }
        return false;
    }

    //游戏结束
    public void GameOver()
    {
        gameStatus = END;//更新游戏状态
        timer.GetComponent<Timer>().TimeEnd();//停止计时
        Time.timeScale = 0;
        SceneManager.LoadScene(3);//进入结算页面
    }
    /*
    //重新开始游戏
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    //返回主菜单
    public void Back()
    {
        SceneManager.LoadScene(0);
    }*/

}

/*//清除所有保存的数据
PlayerPrefs.DeleteAll();*/