using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//이 게임에서는 거리에 비례해 사운드의 크기를 조절할 필요가 없기에 하나의 AudioSource로 AudioClip들을 돌려가며 실행시킬 것이다.
//배경음악을 실행할 AudioSource와 효과음을 실행할 AudioSource를 SoundManager의 자식 오브젝트로 설정

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
    } // Sound를 관리해주는 스크립트는 하나만 존재해야하고 instance프로퍼티로 언제 어디에서나 불러오기위해 싱글톤 사용

    private AudioSource bgmPlayer;
    private AudioSource sfxPlayer;

    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;

    [SerializeField]
    private AudioClip mainBgmAudioClip; //메인화면에서 사용할 BGM
    //[SerializeField]
    //private AudioClip adventureBgmAudioClip; //어드벤쳐씬에서 사용할 BGM

    [ArrayElementTitle("clipName")]
    [SerializeField]
    private AudioClipNode[] sfxAudioClips; //효과음들 지정

    Dictionary<string, AudioClipNode> audioClipsDic = new Dictionary<string, AudioClipNode>(); //효과음 딕셔너리
    // AudioClip을 Key,Value 형태로 관리하기 위해 딕셔너리 사용

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject); //여러 씬에서 사용할 것.

        bgmPlayer = transform.GetChild(0).GetComponent<AudioSource>();
        sfxPlayer = transform.GetChild(1).GetComponent<AudioSource>();

        foreach (AudioClipNode audioclip in sfxAudioClips)
        {
            AudioClipNode newNode = new AudioClipNode();
            newNode.clip = audioclip.clip;
            newNode.volume = audioclip.volume;
            audioClipsDic.Add(audioclip.clip.name, newNode);
        }
        PlaySFXSound("GameStart");
    }

    // 효과 사운드 재생 : 이름을 필수 매개변수, 볼륨을 선택적 매개변수로 지정
    public void PlaySFXSound(string name)
    {
        if (audioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipsDic[name].clip, audioClipsDic[name].volume * masterVolumeSFX);
    }

    //BGM 사운드 재생 : 볼륨을 선택적 매개변수로 지정
    public void PlayBGMSound(float volume = 1f)
    {
        bgmPlayer.loop = true; //BGM 사운드이므로 루프설정
        bgmPlayer.volume = volume * masterVolumeBGM;

        //if (SceneManager.GetActiveScene().name == "Main")
        //{
            bgmPlayer.clip = mainBgmAudioClip;
            bgmPlayer.Play();
        //}
        //else if (SceneManager.GetActiveScene().name == "Adventure")
        //{
        //    bgmPlayer.clip = adventureBgmAudioClip;
        //    bgmPlayer.Play();
        //}
        //현재 씬에 맞는 BGM 재생
    }
}