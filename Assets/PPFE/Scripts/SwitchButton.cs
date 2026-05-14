using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwitchButton : MonoBehaviour, IPointerClickHandler
{
    public RectTransform handle; // La pastille qui bouge
    public Color onColor;
    public Color offColor;
    public Image background;

    private GameManager GameManager;

    private bool isOn;
    private Vector2 offPos, onPos;

    void Start()
    {
        offPos = handle.anchoredPosition;
        onPos = offPos + new Vector2 (handle.rect.width, 0); // Ajustez selon la taille du bouton
        UpdateSwitch();
    }

    private void Awake()
    {
        UpdateSwitch();
    }

    public void LateStart(bool value, GameManager gameManager)
    {
        
        isOn = value;
        GameManager = gameManager;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isOn = !isOn;
        UpdateSwitch();
    }

    void UpdateSwitch()
    {
        handle.anchoredPosition = isOn ? onPos : offPos;
        background.color = isOn ? onColor : offColor;
        Debug.Log("Switch : " + (isOn ? "ON" : "OFF"));
    }

    public void ToggleSwitch()
    {
        GameManager.GameDatas.controllersToggle = !isOn;
        GameManager.ChangeControllersToggle(GameManager.GameDatas.controllersToggle);
    }

    public void SkipTutoSwitch()
    {
        GameManager.GameDatas.skipTutorial = !isOn;
    }
}
