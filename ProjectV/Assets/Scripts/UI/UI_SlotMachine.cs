using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UI_SlotMachine : UI
{
    public static UI_SlotMachine instance;

    public int lineCount = 3;
    public float spacing = 10f;
    public RectTransform[] originImage;
    public Sprite goldSprite;
    public AudioClip rillSound;
    public AudioClip jackpotSound;
    public AudioClip bangSound;

    private bool isPlayMachine = false;
    private bool isOnceTime = false;
    private int lineStack = 0;
    private int maxRange;
    private int sameContents;
    private int jackpotGrade;
    private int[] jackpotNum;
    private float slotSize;
    private float maxSpeed;
    private float minSpeed;
    private float[] lineSpeed;
    private float[] startPosX;
    private float startPosY;
    private float centerPosY;
    private float lineLength;
    private RectTransform rectTransform;
    private RectTransform linePanel;
    private RectTransform buttonPanel;
    private RectTransform rewardPanel;
    private RectTransform[][] contents;
    private RectTransform[] rewardsRT;
    private Image[] rewardsImage;
    private Coroutine[] playCoroutine;
    private Coroutine[] stopCoroutine;
    private AudioSource audioSource;
    

    private SkillKind[] result;
    
    

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Init();
        ResetSize();
        Hide();
        audioSource.volume = DataManager.Instance.Settings.SoundVolume;
    }

    public override void Show()
    {
        GameManager.Instance.Pause();
        base.Show();
        if(!isPlayMachine)
            isOnceTime = true;
    }

    public override void Hide()
    {
        base.Hide();
        rewardPanel.gameObject.SetActive(false);
        audioSource.enabled = false;
        GameManager.Instance.Resume();
    }

    private void Play()
    {
        if (isPlayMachine == false && isOnceTime)
        {
            isPlayMachine = true;
            isOnceTime = false;
            audioSource.enabled = true;
            audioSource.clip = rillSound;
            audioSource.loop = true;
            audioSource.Play();
            for (int i = 0; i < playCoroutine.Length; i++)
            {
                if (playCoroutine[i] != null)
                    StopCoroutine(playCoroutine[i]);
                lineSpeed[i] = maxSpeed * Random.Range(1.5f, 4f);
                playCoroutine[i] = StartCoroutine(PlaySlotMachine(contents[i], i, lineSpeed[i]));
            }
        }
    }

    private void Stop()
    {
        if (isPlayMachine == true && lineStack == 0)
        {
            Result();
            for (int i = 0; i < playCoroutine.Length; i++)
            {
                StopCoroutine(playCoroutine[i]);
                stopCoroutine[i] = StartCoroutine(StopSlotMachine(contents[i],i, jackpotNum[i], lineSpeed[i]));

            }
            
        }
    }

    private void Result()
    {
        int percent = Random.Range(1, 100);
        if (1 <= percent && percent < 50)
        {
            sameContents = 3;
        }
        else if (50 <= percent && percent < 75)
        {
            sameContents = 2;
        }
        else if (75 <= percent && percent <= 100)
        {
            sameContents = 1;
        }
        else
            sameContents = 1;

        int numA, numB, numC;

        var exclude = new HashSet<int>();
        var range = Enumerable.Range(0, maxRange).Where(i => !exclude.Contains(i));
        var rand = new System.Random();
        int index = rand.Next(0, maxRange - exclude.Count);
        numA = range.ElementAt(index);

        exclude.Add(numA);
        range = Enumerable.Range(0, maxRange).Where(i => !exclude.Contains(i));
        rand = new System.Random();
        index = rand.Next(0, maxRange - exclude.Count);
        numB = range.ElementAt(index);

        exclude.Add(numB);
        range = Enumerable.Range(0, maxRange).Where(i => !exclude.Contains(i));
        rand = new System.Random();
        index = rand.Next(0, maxRange - exclude.Count);
        numC = range.ElementAt(index);

        jackpotGrade = numA;

        switch (sameContents)
        {
            case 1:
                jackpotNum[0] = numA;
                jackpotNum[1] = numB;
                jackpotNum[2] = numC;
                break;
            case 2:
                jackpotNum[0] = numA;
                jackpotNum[1] = numA;
                jackpotNum[2] = numB;
                break;
            case 3:
                jackpotNum[0] = numA;
                jackpotNum[1] = numA;
                jackpotNum[2] = numA;
                break;
            default:
                jackpotNum[0] = numA;
                jackpotNum[1] = numB;
                jackpotNum[2] = numC;
                break;
        }

    }

    private void Reward(int rewardQuantity)
    {
        for(int i =0; i < rewardQuantity;)
        {
            if (Player.Instance.IsAllSkillMaxLevel())
            {
                DataManager.Instance.currentGameData.gold += 100f * Player.Instance.stat.Get_FinalStat(StatType.Greed);
                rewardsImage[i].sprite = goldSprite;
                i++;
                continue;
            }
            SkillKind kind = Player.Instance.GetRandomSkill();
            Skill skill = Player.Instance.FindSkill(kind);
            if(skill == null || skill.IsMaxLevel)
                continue;
            result[i] = kind;
            rewardsImage[i].sprite = DataManager.Instance.skillDatas[(int)(kind)].skillData.icon;
            Player.Instance.AddOrIncreaseSkill(kind);
            i++;
        }
        ShowReward(rewardQuantity);
    }

    private void ShowReward(int rewardQuantity)
    {
        for(int i =0; i< rewardsRT.Length; i++)
        {
            if(i < rewardQuantity)
            {
                if(rewardQuantity == 1)
                {
                    rewardsRT[i].anchoredPosition = new Vector2(0f, 0f);
                }
                else if(rewardQuantity % 2 == 0)
                {
                    rewardsRT[i].anchoredPosition = new Vector2((300f * i) - (300f * (int)(rewardQuantity / 2) - 150f), 0f);
                }
                else
                {
                    rewardsRT[i].anchoredPosition = new Vector2((300f * i) - (300f * (int)(rewardQuantity/2)), 0f);
                }
                rewardsRT[i].gameObject.SetActive(true);
            }
            else
            {
                if (rewardsRT[i].gameObject.activeSelf)
                {
                    rewardsRT[i].gameObject.SetActive(false);
                }
            }
        }
    }


    IEnumerator PlaySlotMachine(RectTransform[] contents, int lineNum, float rotationSpeed)
    {
        while (true)
        {
            contents[0].anchoredPosition -= new Vector2(0f, rotationSpeed * Time.unscaledDeltaTime);
            if (contents[0].anchoredPosition.y <= -slotSize)
            {
                float gap = contents[0].anchoredPosition.y + slotSize;
                contents[0].anchoredPosition = new Vector2(startPosX[lineNum], startPosY + gap);
            }
            for (int i = 1; i < contents.Length; i++)
            {
                contents[i].anchoredPosition = contents[0].anchoredPosition + new Vector2(0f, (slotSize * i ) - lineLength);
                if(contents[i].anchoredPosition.y <= -slotSize)
                {
                    contents[i].anchoredPosition += new Vector2(0f, lineLength);
                }
            }
            yield return null;
        }
    }

    IEnumerator StopSlotMachine(RectTransform[] contents,int lineNum, int slotNum, float rotationSpeed)
    {
        float curSpeed = rotationSpeed;
        lineStack++;
        while (true)
        {
            curSpeed -= maxSpeed * 0.5f * Time.unscaledDeltaTime;
            if (curSpeed <= minSpeed)
            {
                curSpeed = minSpeed;
            }
            else
            {
                audioSource.pitch -= 0.005f * Time.unscaledDeltaTime;
            }
            contents[0].anchoredPosition -= new Vector2(0f, curSpeed * Time.unscaledDeltaTime);

            if (contents[0].anchoredPosition.y <= -slotSize)
            {
                float gap = contents[0].anchoredPosition.y + slotSize;
                contents[0].anchoredPosition = new Vector2(startPosX[lineNum], startPosY + gap);

            }
            for (int i = 1; i < contents.Length; i++)
            {
                contents[i].anchoredPosition = contents[0].anchoredPosition + new Vector2(0f, (slotSize * i) - lineLength);
                if (contents[i].anchoredPosition.y <= -slotSize)
                {
                    contents[i].anchoredPosition += new Vector2(0f, lineLength);
                }
            }

            if (curSpeed == minSpeed && contents[slotNum].anchoredPosition.y <= centerPosY && contents[slotNum].anchoredPosition.y >= centerPosY * 0.5f)
            {
                while (contents[slotNum].anchoredPosition.y < centerPosY)
                {
                    //천천히 위로 이동
                    contents[0].anchoredPosition += new Vector2(0f, slotSize * 0.2f * Time.unscaledDeltaTime);
                    if(contents[slotNum].anchoredPosition.y >= centerPosY)
                    {
                        break;
                    }
                    if (contents[0].anchoredPosition.y <= -slotSize)
                    {
                        float gap = contents[0].anchoredPosition.y + slotSize;
                        contents[0].anchoredPosition = new Vector2(startPosX[lineNum], startPosY + gap);
                    }
                    for (int i = 1; i < contents.Length; i++)
                    {
                        contents[i].anchoredPosition = contents[0].anchoredPosition + new Vector2(0f, (slotSize * i) - lineLength);
                        if (contents[i].anchoredPosition.y <= -slotSize)
                        {
                            contents[i].anchoredPosition += new Vector2(0f, lineLength);
                        }
                    }
                    yield return null;
                }

                //갑자기 이동
                contents[0].anchoredPosition = new Vector2(startPosX[lineNum], centerPosY - (slotSize * slotNum) );
                if (contents[0].anchoredPosition.y <= -slotSize)
                {
                    float gap = contents[0].anchoredPosition.y + slotSize;
                    contents[0].anchoredPosition = new Vector2(startPosX[lineNum], startPosY + gap);
                }
                for (int i = 1; i < contents.Length; i++)
                {
                    contents[i].anchoredPosition = contents[0].anchoredPosition + new Vector2(0f, (slotSize * i) - lineLength);
                    if (contents[i].anchoredPosition.y <= -slotSize)
                    {
                        contents[i].anchoredPosition += new Vector2(0f, lineLength);
                    }
                }

                lineStack--;
                if(lineStack == 0)
                {
                    audioSource.Stop();
                    audioSource.pitch = 1f;
                    audioSource.loop = false;
                    isPlayMachine = false;
                    yield return new WaitForSecondsRealtime(0.15f);
                    rewardPanel.gameObject.SetActive(true);
                    if (sameContents == 3)
                    {
                        audioSource.clip = jackpotSound;
                        audioSource.Play();
                        switch (jackpotGrade)
                        {
                            case 0:
                                Reward(1);
                                break;
                            case 1:
                                Reward(2);
                                break;
                            case 2:
                                Reward(3);
                                break;
                            case 3:
                                Reward(4);
                                break;
                            case 4:
                                Reward(5);
                                break;
                            case 5:
                                audioSource.clip = bangSound;
                                audioSource.Play();
                                Reward(0);
                                break;
                            default:
                                Reward(0);
                                break;
                        }
                    }
                    else
                    {
                        audioSource.clip = bangSound;
                        audioSource.Play();
                        Reward(0);
                    }
                }
                break;
            }
            yield return null;
        }
        yield return null;
    }

    private void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        linePanel = transform.GetChild(0).GetComponent<RectTransform>();
        buttonPanel = transform.GetChild(1).GetComponent<RectTransform>();
        rewardPanel = transform.GetChild(3).GetComponent<RectTransform>();
        contents = new RectTransform[lineCount][];
        for(int i = 0; i < contents.Length; i++)
        {
            contents[i] = new RectTransform[originImage.Length];
            for(int j = 0; j< originImage.Length;j++)
            {
                if (i == 0)
                {
                    contents[i][j] = originImage[j];
                }
                else
                {
                    GameObject temp = Instantiate(originImage[j].gameObject, transform.GetChild(0));
                    contents[i][j] = temp.GetComponent<RectTransform>();
                }
            }
        }
        playCoroutine = new Coroutine[contents.Length];
        stopCoroutine = new Coroutine[contents.Length];
        lineSpeed = new float[lineCount];

        result = new SkillKind[5];
        jackpotNum = new int[contents.Length];
        maxRange = contents[0].Length -1;

        rewardsRT = new RectTransform[rewardPanel.GetChild(0).childCount];
        rewardsImage = new Image[rewardPanel.GetChild(0).childCount];
        for(int i =0; i< rewardPanel.GetChild(0).childCount; i++)
        {
            rewardsRT[i] = rewardPanel.GetChild(0).GetChild(i).GetComponent<RectTransform>();
            rewardsImage[i] = rewardsRT[i].GetComponent<Image>();
            rewardsRT[i].gameObject.SetActive(false);
        }
        rewardPanel.gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    private void ResetSize()
    {
        Vector2 parentSize = transform.parent.GetComponent<RectTransform>().sizeDelta;
        Vector2 ratio = new Vector2(0.4f, 0.8f);
        float width = parentSize.x * ratio.x;
        float height = parentSize.y * ratio.y;

        rectTransform.sizeDelta = new Vector2(width, height);
        slotSize = (width - (spacing * (lineCount + 1))) / lineCount;
        linePanel.sizeDelta = new Vector2(0f, slotSize * 2);
        buttonPanel.sizeDelta = new Vector2(0f, (height - linePanel.sizeDelta.y) * 0.5f);
        for(int i =0; i < buttonPanel.childCount; i++)
        {
            RectTransform buttonRT = buttonPanel.GetChild(i).GetComponent<RectTransform>();
            buttonRT.sizeDelta = new Vector2(width * 0.3f, buttonPanel.sizeDelta.y * 0.5f);
            float buttonPosX = (width * 0.1f) + (buttonRT.sizeDelta.x * 0.5f);
            buttonRT.anchoredPosition = new Vector2((buttonPosX * 2f * i) - buttonPosX, 0f);
        }

        startPosX = new float[contents.Length];
        startPosY = slotSize * (contents[0].Length - 1);
        centerPosY = slotSize * 0.5f;
        lineLength = slotSize * contents[0].Length;
        float endPosY = slotSize * -0.5f;
        for (int i =0; i < contents.Length; i++)
        {
            startPosX[i] = (slotSize * i) + (spacing * (i+1));
            for(int j = 0; j< contents[i].Length; j++)
            {
                contents[i][j].sizeDelta = new Vector2(slotSize, slotSize);
                contents[i][j].anchoredPosition = new Vector2(startPosX[i], endPosY + (slotSize * j));
            }
        }
        maxSpeed = slotSize * contents[0].Length;
        minSpeed = maxSpeed * 0.5f;
    }

    public void OnClickPlay()
    {
        Play();
    }

    public void OnClickStop()
    {
        Stop();
    }

    public void OnClickConfirm()
    {
        Hide();
    }
}
