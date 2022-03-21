using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SlotMachine : UI
{
    public static UI_SlotMachine instance;

    public int lineCount = 3;
    public float spacing = 10f;
    public RectTransform[] originImage;

    private bool isPlayMachine = false;
    private int lineStack = 0;
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
            for (int i = 0; i < playCoroutine.Length; i++)
            {
                StopCoroutine(playCoroutine[i]);
                stopCoroutine[i] = StartCoroutine(StopSlotMachine(contents[i],i, Random.Range(0, 5), maxSpeed));
            }
        }
    }

    public void OnClickPlay()
    {
        Play();
    }

    public void OnClickStop()
    {
        Stop();
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
}
