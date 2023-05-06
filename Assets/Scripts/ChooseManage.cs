using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseManage : MonoBehaviour
{
    public static ChooseManage instance;

    //游戏控制
    public List<int> P1_plant;//玩家1选择的植物
    public List<int> P2_plant;//玩家2选择的植物

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

    //定义常量
    //游戏状态
    [HideInInspector] public int CHOOSE_OK = 10;//可进行操作
    [HideInInspector] public int CHOOSE_MOVING_OUT = 11;//切换植物中，植物正在移出
    [HideInInspector] public int CHOOSE_MOVING_IN = 12;//切换植物中，植物正在移入
    [HideInInspector] public int CHOOSE_READY = 13;//已确认选择完毕
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
    //选择植物中常量
    [HideInInspector] public float CHOOSE_X = (float)-1.53;//选卡框基准X位置
    [HideInInspector] public float CHOOSE_Y = (float)1.33;//选卡框基准Y位置
    [HideInInspector] public float CHOOSE_X_DETA = (float)1.02;//选卡框X位移量
    [HideInInspector] public float CHOOSE_Y_DETA = (float)-1.46;//选卡框Y位移量
    public Sprite[] PLANT;//植物图片
    public Sprite[] PLANT_CARD;//植物卡片图片

    private void Awake()
    {
        instance = this;
        //设置分辨率，不全屏
        Screen.SetResolution(1920, 1080, false);
        //初始化
        P1_plant = new List<int>();
        P2_plant = new List<int>();
    }

    void Start()
    {
        //场景初始化
        Initialized();
    }

    // Update is called once per frame
    void Update()
    {
        ChooseKeyResponse();
    }

    //初始化
    private void Initialized()
    {
        //播放音乐
        SoundManage.instance.ChooseingMusic();
        //显示选卡界面
        choosePanel.SetActive(true);
        //初始化选卡位置
        P1_X = 0;
        P1_Y = 0;
        P2_X = 3;
        P2_Y = 0;
        //隐藏空白卡片
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
        else if (Input.GetKeyDown(KeyCode.Space) && P1_choose_state != CHOOSE_READY)
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
            else if (P1_plant.Count == 6)//卡牌已经选满 且 卡牌未选中，确认卡牌
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
            SoundManage.instance.ChooseingMove(2);
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
            SoundManage.instance.ChooseingMove(2);
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
            SoundManage.instance.ChooseingMove(2);
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
            SoundManage.instance.ChooseingMove(2);
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
                SoundManage.instance.ChooseingChoose(2);
            }
            else if (P2_plant.Count == 6)//卡牌已经选满 且 卡牌未选中，确认卡牌
            {
                if (P1_choose_state != CHOOSE_READY)//对手还未准备完成
                {
                    P2_choose_state = CHOOSE_READY;
                    P2_lock.gameObject.SetActive(true);
                    //播放音效
                    SoundManage.instance.ChooseingLock(2);
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
                SoundManage.instance.ChooseingChoose(2);
            }
        }
    }

    //准备开始对战
    public void GameReady()
    {
        //保存选择的植物
        for(int i = 0; i < 6; i++)
        {
            PlayerPrefs.SetInt("P1_" + i, P1_plant[i]);
            PlayerPrefs.SetInt("P2_" + i, P2_plant[i]);
        }
        SceneManager.LoadScene(2);//加载对战场景
    }
}
