using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�� ���ӿ����� �Ÿ��� ����� ������ ũ�⸦ ������ �ʿ䰡 ���⿡ �ϳ��� AudioSource�� AudioClip���� �������� �����ų ���̴�.
//��������� ������ AudioSource�� ȿ������ ������ AudioSource�� SoundManager�� �ڽ� ������Ʈ�� ����

[System.Serializable]
public enum SoundKind
{
    Hurt01,
    Hurt02,
    Hurt03,
    Death,
    End
}

[System.Serializable]
public class Sound
{
    public SoundKind kind;
    public AudioClip clip;
}

[System.Serializable]
public class AudioClipNode
{
    public string clipName;
    public AudioClip clip;
    public float volume = 1f;
}
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }

            return instance;
        }
    } // Sound�� �������ִ� ��ũ��Ʈ�� �ϳ��� �����ؾ��ϰ� instance������Ƽ�� ���� ��𿡼��� �ҷ��������� �̱��� ���

    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;

    public float masterVolumeBGM = 1f;
    public float masterVolumeSFX = 1f;

    [ArrayElementTitle("clipName")]
    [SerializeField]
    private AudioClipNode[] bgmAudioClips; //����ȭ�鿡�� ����� BGM
    //[SerializeField]
    //private AudioClip adventureBgmAudioClip; //��庥�ľ����� ����� BGM

    [ArrayElementTitle("clipName")]
    [SerializeField]
    private AudioClipNode[] sfxAudioClips; //ȿ������ ����

    Dictionary<string, AudioClipNode> sfxAudioClipsDic = new Dictionary<string, AudioClipNode>(); //ȿ���� ��ųʸ�
    Dictionary<string, AudioClipNode> bgmAudioClipsDic = new Dictionary<string, AudioClipNode>(); //����� ��ųʸ�
    // AudioClip�� Key,Value ���·� �����ϱ� ���� ��ųʸ� ���
    [SerializeField] private float newBgmInterval = 600f;
    private float newBgmTick = 0f;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject); //���� ������ ����� ��.

        bgmPlayer = transform.GetChild(0).GetComponent<AudioSource>();
        sfxPlayer = transform.GetChild(1).GetComponent<AudioSource>();

        foreach (AudioClipNode audioclip in sfxAudioClips)
        {
            AudioClipNode newNode = new AudioClipNode();
            newNode.clip = audioclip.clip;
            newNode.volume = audioclip.volume;
            sfxAudioClipsDic.Add(audioclip.clipName, newNode);
        }
        foreach (AudioClipNode audioclip in bgmAudioClips)
        {
            AudioClipNode newNode = new AudioClipNode();
            newNode.clip = audioclip.clip;
            newNode.volume = audioclip.volume;
            bgmAudioClipsDic.Add(audioclip.clipName, newNode);
        }
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    // ȿ�� ���� ��� : �̸��� �ʼ� �Ű�����, ������ ������ �Ű������� ����
    public void PlaySFXSound(string name)
    {
        if (sfxAudioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }
        sfxPlayer.PlayOneShot(sfxAudioClipsDic[name].clip, sfxAudioClipsDic[name].volume * masterVolumeSFX);
    }

    //BGM ���� ��� : ������ ������ �Ű������� ����
    public void PlayBGMSound(string name)
    {
        bgmPlayer.loop = true; //BGM �����̹Ƿ� ��������

        if (bgmAudioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }
        bgmPlayer.clip = bgmAudioClipsDic[name].clip;
        bgmPlayer.volume = bgmAudioClipsDic[name].volume * masterVolumeBGM;
        bgmPlayer.Play();

    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name.Contains("Stage"))
        {
            newBgmTick += Time.fixedDeltaTime;
            if(newBgmTick >= newBgmInterval)
            {
                newBgmTick = 0f;
                int random = Random.Range(0, 4);
                switch (random)
                {
                    case 0:
                        {
                            PlayBGMSound("8Bit Track2");
                            break;
                        }
                    case 1:
                        {
                            PlayBGMSound("8Bit Track5");
                            break;
                        }
                    case 2:
                        {
                            PlayBGMSound("8Bit Track8");
                            break;
                        }
                    case 3:
                        {
                            PlayBGMSound("8Bit Track9");
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }

    public void SetCurrentBgmVolume()
    {
        string currentBgmName = bgmPlayer.clip.name;
        bgmPlayer.volume = bgmAudioClipsDic[currentBgmName].volume * masterVolumeBGM;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "TitleScene":
                {
                    PlaySFXSound("GameStart");
                    PlayBGMSound("8Bit Track1");
                    break;
                }
            case "LoadingScene":
                {
                    PlaySFXSound("LongButton");
                    StopBGM();
                    newBgmTick = newBgmInterval;
                    break;
                }
            default:
                break;
        }
    }

}