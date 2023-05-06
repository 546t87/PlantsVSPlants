using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManage : MonoBehaviour
{
    //����
    public static SoundManage instance;
    public AudioSource audioSourcs;//����Դ
    public AudioClip chooseMusic;//ѡ��ֲ���BGM
    public AudioClip readyMusic;//׼��ս����BGM
    public AudioClip gameMusic;//ս����BGM
    public AudioClip endMusic;//������BGM
    public float musicVolume;//BGM������С
    public float soundEffectVolume;//��Ч������С

    //��Ч
    public AudioSource P1_aduio;//���1��������Ч������
    public AudioSource P2_aduio;//���2������Ч������
    //ֲ��ѡ�����
    public AudioClip chooseing_move;//ѡ��ֲ����棬�ƶ�ѡ������Ч
    public AudioClip chooseing_choose;//ѡ��ֲ����棬ȷ��ѡ�����Ч
    public AudioClip chooseing_lock;//ѡ��ֲ����棬����ֲ�����Ч
    //��ս����
    public AudioClip P1_game_choose;//��ս���棬���1ѡ��ֲ�����Ч
    public AudioClip P1_game_plant;//��ս���棬���1��ֲֲ�����Ч
    public AudioClip P2_game_choose;//��ս���棬���2ѡ��ֲ�����Ч
    public AudioClip P2_game_plant;//��ս���棬���2��ֲֲ�����Ч
    public AudioClip game_remove_plant;//��ս���棬��Ҳ���ֲ�����Ч
    public AudioClip game_collect_sun;//��ս���棬����ռ��������Ч

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

    //����ѡ��ֲ���������
    public void ChooseingMusic()
    {
        audioSourcs.clip = chooseMusic;
        audioSourcs.volume = musicVolume;
        audioSourcs.Play();
    }

    //����׼����������
    public void ReadyMusic()
    {
        audioSourcs.clip = readyMusic;
        audioSourcs.volume = soundEffectVolume;
        audioSourcs.Play();
    }

    //���Ŷ�ս��������
    public void GammingMusic()
    {
        audioSourcs.clip = gameMusic;
        audioSourcs.volume = musicVolume;
        audioSourcs.Play();
    }

    //���Ž����������
    public void EndMusic()
    {
        P1_aduio.clip = endMusic;
        P1_aduio.volume = musicVolume;
        P1_aduio.Play();
    }
    
    //����ѡ��ֲ����棬ѡ��ֲ����Ч
    public void ChooseingMove(int player)
    {
        if(player == 1)
        {
            P1_aduio.volume = soundEffectVolume;//��������
            P1_aduio.clip = chooseing_move;
            P1_aduio.Play();
        }
        else if(player == 2)
        {
            P2_aduio.volume = soundEffectVolume;//��������
            P2_aduio.clip = chooseing_move;
            P2_aduio.Play();
        }
    }

    //����ѡ��ֲ����棬ѡ��ֲ����Ч
    public void ChooseingChoose(int player)
    {
        if (player == 1)
        {
            P1_aduio.volume = soundEffectVolume;//��������
            P1_aduio.clip = chooseing_choose;
            P1_aduio.Play();
        }
        else if (player == 2)
        {
            P2_aduio.volume = soundEffectVolume;//��������
            P2_aduio.clip = chooseing_choose;
            P2_aduio.Play();
        }
    }

    //����ѡ��ֲ����棬����ֲ����Ч
    public void ChooseingLock(int player)
    {
        if (player == 1)
        {
            P1_aduio.volume = soundEffectVolume;//��������
            P1_aduio.clip = chooseing_lock;
            P1_aduio.Play();
        }
        else if (player == 2)
        {
            P2_aduio.volume = soundEffectVolume;//��������
            P2_aduio.clip = chooseing_lock;
            P2_aduio.Play();
        }
    }
    
    //���Ŷ�ս���棬ѡ��ֲ����Ч
    public void GameChoose(int player)
    {
        if (player == 1 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P1_aduio.volume = soundEffectVolume;//��������
            P1_aduio.clip = P1_game_choose;
            P1_aduio.Play();
        }
        else if (player == 2 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P2_aduio.volume = soundEffectVolume;//��������
            P2_aduio.clip = P2_game_choose;
            P2_aduio.Play();
        }
    }

    //���Ŷ�ս���棬��ֲֲ����Ч
    public void GamePlant(int player)
    {
        if (player == 1 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P1_aduio.volume = soundEffectVolume;//��������
            P1_aduio.clip = P1_game_plant;
            P1_aduio.Play();
        }
        else if (player == 2 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P2_aduio.volume = soundEffectVolume;//��������
            P2_aduio.clip = P2_game_plant;
            P2_aduio.Play();
        }
    }

    //���Ŷ�ս���棬����ֲ����Ч
    public void GameRemove(int player)
    {
        if (player == 1 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P1_aduio.volume = soundEffectVolume;//��������
            P1_aduio.clip = game_remove_plant;
            P1_aduio.Play();
        }
        else if (player == 2 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P2_aduio.volume = soundEffectVolume;//��������
            P2_aduio.clip = game_remove_plant;
            P2_aduio.Play();
        }
    }

    //���Ŷ�ս���棬�ռ�������Ч
    public void GameCollectSun(int player)
    {
        if (player == 1 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P1_aduio.volume = soundEffectVolume;//��������
            P1_aduio.clip = game_collect_sun;
            P1_aduio.Play();
        }
        else if (player == 2 && GameManager.instance.gameStatus != GameManager.instance.END)
        {
            P2_aduio.volume = soundEffectVolume;//��������
            P2_aduio.clip = game_collect_sun;
            P2_aduio.Play();
        }
    }
}
