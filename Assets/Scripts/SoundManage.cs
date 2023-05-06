using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManage : MonoBehaviour
{
    //音乐
    public static SoundManage instance;
    public AudioSource audioSourcs;//声音源
    public AudioClip chooseMusic;//选择植物的BGM
    public AudioClip readyMusic;//准备战斗的BGM
    public AudioClip gameMusic;//战斗的BGM
    public AudioClip endMusic;//结束的BGM
    public float musicVolume;//BGM音量大小
    public float soundEffectVolume;//音效音量大小

    //音效
    public AudioSource P1_aduio;//玩家1操作的音效播放器
    public AudioSource P2_aduio;//玩家2操作音效播放器
    //植物选择界面
    public AudioClip chooseing_move;//选择植物界面，移动选择框的音效
    public AudioClip chooseing_choose;//选择植物界面，确定选择的音效
    public AudioClip chooseing_lock;//选择植物界面，锁定植物的音效
    //对战界面
    public AudioClip P1_game_choose;//对战界面，玩家1选择植物的音效
    public AudioClip P1_game_plant;//对战界面，玩家1种植植物的音效
    public AudioClip P2_game_choose;//对战界面，玩家2选择植物的音效
    public AudioClip P2_game_plant;//对战界面，玩家2种植植物的音效
    public AudioClip game_remove_plant;//对战界面，玩家铲除植物的音效
    public AudioClip game_collect_sun;//对战界面，玩家收集阳光的音效

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        soundEffectVolume = PlayerPrefs.GetFloat("soundEffectVolume");
        audioSourcs.volume = musicVolume;
        P1_aduio.volume = soundEffectVolume;
        P2_aduio.volume = soundEffectVolume;
    }

    //播放选择植物界面音乐
    public void ChooseingMusic()
    {
        audioSourcs.clip = chooseMusic;
        audioSourcs.volume = musicVolume;
        audioSourcs.Play();
    }

    //播放准备界面音乐
    public void ReadyMusic()
    {
        audioSourcs.clip = readyMusic;
        audioSourcs.volume = soundEffectVolume;
        audioSourcs.Play();
    }

    //播放对战界面音乐
    public void GammingMusic()
    {
        audioSourcs.clip = gameMusic;
        audioSourcs.volume = musicVolume;
        audioSourcs.Play();
    }

    //播放结算界面音乐
    public void EndMusic()
    {
        P1_aduio.clip = endMusic;
        P1_aduio.volume = musicVolume;
        P1_aduio.Play();
    }
    
    //播放选择植物界面，选择植物音效
    public void ChooseingMove(int player)
    {
        if(player == 1)
        {
            P1_aduio.volume = soundEffectVolume;//更新音量
            P1_aduio.clip = chooseing_move;
            P1_aduio.Play();
        }
        else if(player == 2)
        {
            P2_aduio.volume = soundEffectVolume;//更新音量
            P2_aduio.clip = chooseing_move;
            P2_aduio.Play();
        }
    }

    //播放选择植物界面，选中植物音效
    public void ChooseingChoose(int player)
    {
        if (player == 1)
        {
            P1_aduio.volume = soundEffectVolume;//更新音量
            P1_aduio.clip = chooseing_choose;
            P1_aduio.Play();
        }
        else if (player == 2)
        {
            P2_aduio.volume = soundEffectVolume;//更新音量
            P2_aduio.clip = chooseing_choose;
            P2_aduio.Play();
        }
    }

    //播放选择植物界面，锁定植物音效
    public void ChooseingLock(int player)
    {
        if (player == 1)
        {
            P1_aduio.volume = soundEffectVolume;//更新音量
            P1_aduio.clip = chooseing_lock;
            P1_aduio.Play();
        }
        else if (player == 2)
        {
            P2_aduio.volume = soundEffectVolume;//更新音量
            P2_aduio.clip = chooseing_lock;
            P2_aduio.Play();
        }
    }
    
    //播放对战界面，选择植物音效
    public void GameChoose(int player)
    {
        if (player == 1 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P1_aduio.volume = soundEffectVolume;//更新音量
            P1_aduio.clip = P1_game_choose;
            P1_aduio.Play();
        }
        else if (player == 2 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P2_aduio.volume = soundEffectVolume;//更新音量
            P2_aduio.clip = P2_game_choose;
            P2_aduio.Play();
        }
    }

    //播放对战界面，种植植物音效
    public void GamePlant(int player)
    {
        if (player == 1 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P1_aduio.volume = soundEffectVolume;//更新音量
            P1_aduio.clip = P1_game_plant;
            P1_aduio.Play();
        }
        else if (player == 2 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P2_aduio.volume = soundEffectVolume;//更新音量
            P2_aduio.clip = P2_game_plant;
            P2_aduio.Play();
        }
    }

    //播放对战界面，铲除植物音效
    public void GameRemove(int player)
    {
        if (player == 1 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P1_aduio.volume = soundEffectVolume;//更新音量
            P1_aduio.clip = game_remove_plant;
            P1_aduio.Play();
        }
        else if (player == 2 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P2_aduio.volume = soundEffectVolume;//更新音量
            P2_aduio.clip = game_remove_plant;
            P2_aduio.Play();
        }
    }

    //播放对战界面，收集阳光音效
    public void GameCollectSun(int player)
    {
        if (player == 1 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P1_aduio.volume = soundEffectVolume;//更新音量
            P1_aduio.clip = game_collect_sun;
            P1_aduio.Play();
        }
        else if (player == 2 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P2_aduio.volume = soundEffectVolume;//更新音量
            P2_aduio.clip = game_collect_sun;
            P2_aduio.Play();
        }
    }
}
