using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickEx : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public bool IsPaused { get { return isPaused; } }

    [SerializeField] private float handleRange = 1;

    [SerializeField] protected RectTransform background = null;
    [SerializeField] private RectTransform handle = null;
    private RectTransform baseRect = null;
    private RectTransform canvasRect = null;

    private Canvas canvas;
    private Camera cam;

    private Vector2 input = Vector2.zero;
    private bool isPaused = false;

    void Start()
    {
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            Debug.LogError("The Joystick is not placed inside a canvas");

        canvasRect = canvas.transform.GetComponent<RectTransform>();

        baseRect.sizeDelta = canvasRect.sizeDelta;

        background.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isPaused) return;
        OnDrag(eventData);
        background.gameObject.SetActive(true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            cam = canvas.worldCamera;

        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        Vector2 radius = background.sizeDelta / 2;
        input = (eventData.position - position) / (radius * canvas.scaleFactor);

        handle.anchoredPosition = input * radius * handleRange;
    }

    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
        {
            Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
            return localPoint - (background.anchorMax * baseRect.sizeDelta) + pivotOffset;
        }
        return Vector2.zero;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
    }

    public void Show()
    {
        background.gameObject.SetActive(true);
    }

    public void Hide()
    {
        background.gameObject.SetActive(false);
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }
}
