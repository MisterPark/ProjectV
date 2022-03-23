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

    private bool isPlayMachine = false;
    private int lineStack = 0;
    private int maxRange;
    private int sameContents;
    private int[] jackpotNum;
    private float slotSize;
    private float maxSpeed;
    private float[] startPosX;
    private float startPosY;
    private RectTransform rectTransform;
    private RectTransform linePanel;
    private RectTransform buttonPanel;
    private RectTransform[][] contents;
    private Coroutine[] playCoroutine;
    private Coroutine[] stopCoroutine;

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
    }

    private void Play()
    {
        if (isPlayMachine == false)
        {
            isPlayMachine = true;
            for (int i = 0; i < playCoroutine.Length; i++)
            {
                if (playCoroutine[i] != null)
                    StopCoroutine(playCoroutine[i]);
                playCoroutine[i] = StartCoroutine(PlaySlotMachine(contents[i], i, maxSpeed * Random.Range(1f, 3f)));
            }
        }
    }

    private void Stop()
    {
        if (isPlayMachine == true && lineStack == 0)
        {
            KingCrimson();
            for (int i = 0; i < playCoroutine.Length; i++)
            {
                StopCoroutine(playCoroutine[i]);
                stopCoroutine[i] = StartCoroutine(StopSlotMachine(contents[i],i, jackpotNum[i], maxSpeed));

            }
            
        }
    }

    private void KingCrimson()
    {
        int percent = Random.Range(1, 100);
        if (1 <= percent && percent < 70)
        {
            sameContents = 1;
        }
        else if (70 <= percent && percent < 95)
        {
            sameContents = 2;
        }
        else if (95 <= percent && percent <= 100)
        {
            sameContents = 3;
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

        Debug.Log(sameContents.ToString() + "개 당첨");
    }

    private void Reward(int rewardQuantity)
    {
        //당첨 확률 1 = 70%, 2 = 25; 3 = 5;
        //당첨확률에 따라 어떤그림이 1~3개가 뜰지 정하고 뜬 숫자에 따라 결과를 전송 후
        // 완전히 멈췄을때 획득한 스킬을 보여준 다음 끝;
        for(int i =0; i < rewardQuantity;)
        {
            SkillKind kind = Player.Instance.GetRandomSkill();
            Skill skill = Player.Instance.FindSkill(kind);
            if(skill == null || skill.IsMaxLevel)
                continue;
            result[i] = kind;
            Player.Instance.AddOrIncreaseSkill(kind);
            i++;
        }
    }


    IEnumerator PlaySlotMachine(RectTransform[] contents, int lineNum, float rotationSpeed)
    {
        while (true)
        {
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i].anchoredPosition -= new Vector2(0f, rotationSpeed * Time.unscaledDeltaTime);
                if (contents[i].anchoredPosition.y <= -slotSize)
                {
                    float gap = contents[i].anchoredPosition.y + slotSize;
                    contents[i].anchoredPosition = new Vector2(startPosX[lineNum], startPosY + gap);
                }
            }
            yield return null;
        }
    }

    IEnumerator StopSlotMachine(RectTransform[] contents,int lineNum, int slotNum, float rotationSpeed)
    {
        bool isRotationOneLap = false;
        int rotationCount = 0;
        int lapCount = 0;
        float curSpeed = rotationSpeed;
        lineStack++;
        while (true)
        {
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i].anchoredPosition -= new Vector2(0f, curSpeed * Time.unscaledDeltaTime);
                if (contents[i].anchoredPosition.y <= -slotSize)
                {
                    float gap = contents[i].anchoredPosition.y + slotSize;
                    contents[i].anchoredPosition = new Vector2(startPosX[lineNum], startPosY + gap);
                    rotationCount++;
                    if(rotationCount >= contents.Length)
                    {
                        isRotationOneLap = true;
                        lapCount++;
                        rotationCount = 0;
                        if (lapCount >= 3)
                            break;
                    }
                }
            }
            if(lapCount >= 3 && contents[slotNum].anchoredPosition.y <= (slotSize * 0.5f))
            {
                int slot = slotNum - 1;
                int sort = 0;
                if(slot < 0)
                {
                    slot += contents.Length;
                }
                for (int i = slot; i < contents.Length + slot; i++)
                {
                    if(i >=  contents.Length)
                    {
                        contents[i - contents.Length].anchoredPosition = new Vector2(startPosX[lineNum], (slotSize * sort) - (slotSize * 0.5f));
                    }
                    else
                    {
                        contents[i].anchoredPosition = new Vector2(startPosX[lineNum], (slotSize * sort) - (slotSize * 0.5f));
                    }
                    sort++;
                }

                lineStack--;
                if(lineStack == 0)
                {
                    isPlayMachine = false;
                    switch(sameContents)
                    {
                        case 1: Reward(1);
                            break;
                        case 2: Reward(3);
                            break;
                        case 3: Reward(5);
                            break;
                        default: Reward(1);
                            break;
                    }
                }
                break;
            }
            if(isRotationOneLap)
            {
                isRotationOneLap = false;
                curSpeed *= 0.7f;
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

        result = new SkillKind[5];
        jackpotNum = new int[contents.Length];
        maxRange = contents[0].Length -1;
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
    }

    public void OnClickPlay()
    {
        Play();
    }

    public void OnClickStop()
    {
        Stop();
    }
}
