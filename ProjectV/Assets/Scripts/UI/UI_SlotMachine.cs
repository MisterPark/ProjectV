using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SlotMachine : UI
{
    public static UI_SlotMachine instance;

    public RectTransform resultPanel;

    private bool isPlayMachine = false;
    private int slotStack = 0;
    private int maxSlotStack;
    private float maxSpeed = 1000f;
    private float startPosY;
    private float contentsWidth;
    private float contentsHeight;
    private RectTransform rectTransform;
    private RectTransform parent;
    private RectTransform[] contents;
    private LayoutElement[][] slotsElement;
    private Coroutine[] slotCoroutine;


    void Awake()
    {
        instance = this;
        Init();
    }

    void Start()
    {
        ResetSize();
    }

    private void Play()
    {
        if (isPlayMachine == false)
        {
            for (int i = 0; i < slotCoroutine.Length; i++)
            {
                if (slotCoroutine[i] != null)
                    StopCoroutine(slotCoroutine[i]);
                slotCoroutine[i] = StartCoroutine(PlaySlotMachine(contents[i]));
            }
            isPlayMachine = true;
        }
    }

    private void Stop()
    {
        if (isPlayMachine == true && slotStack >= maxSlotStack)
        {
            for (int i = 0; i < slotCoroutine.Length; i++)
            {
                StopCoroutine(slotCoroutine[i]);
                slotCoroutine[i] = StartCoroutine(StopSlotMachine(contents[i], Random.Range(1, 5)));
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

    IEnumerator PlaySlotMachine(RectTransform contents)
    {
        float curSpeed = maxSpeed;
        while (true)
        {
            contents.anchoredPosition -= new Vector2(0f, curSpeed * Time.unscaledDeltaTime);
            if (contents.anchoredPosition.y <= 0f)
            {
                contents.anchoredPosition = new Vector2(0f, startPosY);
            }
            yield return null;
        }
    }

    IEnumerator StopSlotMachine(RectTransform contents, int slotNum)
    {
        int rollRound = 0;
        float curSpeed = maxSpeed;
        slotStack--;
        while (true)
        {
            contents.anchoredPosition -= new Vector2(0f, curSpeed * Time.unscaledDeltaTime);
            if (contents.anchoredPosition.y <= 0f)
            {
                contents.anchoredPosition = new Vector2(0f, startPosY);
                rollRound++;
                curSpeed *= 0.7f;
            }
            if(rollRound >= 3)
            {
                if(contents.anchoredPosition.y <= slotNum * contentsHeight)
                {
                    contents.anchoredPosition = new Vector2(0f, slotNum * contentsHeight);
                    slotStack++;
                    if (slotStack >= maxSlotStack)
                        isPlayMachine = false;
                    break;
                }
            }
            yield return null;
        }
        yield return null;
    }

    private void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        //Init Contents
        contents = new RectTransform[resultPanel.childCount];
        for(int i = 0; i < resultPanel.childCount; i++)
        {
            contents[i] = resultPanel.GetChild(i).GetChild(0).GetComponent<RectTransform>();
        }
        //Init SlotsElement
        slotsElement = new LayoutElement[contents.Length][];
        for(int i =0; i < 3; i++)
        {
            slotsElement[i] = new LayoutElement[contents[i].childCount];
            for(int j = 0; j < contents[i].childCount; j++)
            {
                slotsElement[i][j] = contents[i].GetChild(j).GetComponent<LayoutElement>();
            }
        }
        //Init Coroutine
        slotCoroutine = new Coroutine[contents.Length];

        maxSlotStack = contents.Length;
        slotStack = maxSlotStack;

        parent = transform.parent.GetComponent<RectTransform>();
    }

    private void ResetSize()
    {
        Vector2 size = parent.sizeDelta;
        float ratioX = 0.4f;
        float ratioY = 0.8f;
        rectTransform.sizeDelta = new Vector2(size.x * ratioX, size.y * ratioY);
        float width = rectTransform.sizeDelta.x;
        HorizontalLayoutGroup temp = resultPanel.GetComponent<HorizontalLayoutGroup>();
        float interval = width * 0.1f;
        temp.padding.left = (int)(interval * 0.15f);
        temp.padding.right = (int)(interval * 0.15f);
        temp.spacing = (int)(interval * 0.35f);
        contentsWidth = width * 0.3f;
        contentsHeight = contentsWidth;
        resultPanel.sizeDelta = new Vector2(0f, contentsHeight);
        
        for (int i = 0; i < contents.Length; i++)
        {
            for(int j = 0; j < slotsElement[i].Length; j++)
            {
                slotsElement[i][j].minHeight = contentsHeight;
            }
        }
        startPosY = contentsHeight * (slotsElement[0].Length - 1);
        for (int i = 0; i < contents.Length; i++)
        {
            contents[i].anchoredPosition = new Vector2(0f, startPosY);
        }
        maxSpeed = startPosY;
    }
}
