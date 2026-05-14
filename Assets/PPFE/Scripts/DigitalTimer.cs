using TMPro;
using UnityEngine;

public class DigitalTimer : MonoBehaviour
{
    private float timer = 300f;
    private bool startTimer;
    [SerializeField] private TextMeshProUGUI timerDisplay;
    private GameManager gameManager;

    private void Start()
    {
        DisplayTimeOnTimer();
    }

    public void StartTimer(GameManager gameManagerRef)
    {
        startTimer = true;
        gameManager = gameManagerRef;
    }

    public float StopTimer()
    {
        startTimer = false;
        return timer;
    }

    public float GetTimer() 
    {  
        return timer;
    }

    private void DisplayTimeOnTimer()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void Update()
    {
        if (startTimer)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else if (timer < 0)
            {
                startTimer = false;
                timer = 0;
                gameManager.EndGame();
            }
            DisplayTimeOnTimer();
        }
    }
}
