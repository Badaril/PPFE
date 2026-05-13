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
    }

    public void LateStart(bool value, GameManager gameManager)
    {
        isOn = value;
        GameManager = gameManager;
        UpdateSwitch();
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
        GameManager.GameDatas.controllersToggleOff = isOn;
        GameManager.ChangeControllersToggle(GameManager.GameDatas.controllersToggleOff);
    }

    public void SkipTutoSwitch()
    {
        GameManager.GameDatas.skipTutorial = isOn;
    }
}
