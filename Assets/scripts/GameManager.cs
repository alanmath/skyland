using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private int count;
    
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool isTimerRunning = false;
    private int countLife = 8;
    public TextMeshProUGUI lifeText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);

        startTime = Time.time;
        isTimerRunning = true;
    }

    public void IncrementCount()
    {
        count++;
        SetCountText();
    }

    public void decrementLife()
    {
        countLife--;
        SetCountLifeText();
    }

    void SetCountLifeText()
    {
        lifeText.text = "Life: " + countLife.ToString();
        if(countLife <= 0)
        {
            isTimerRunning = false;
            
            string finalTime = timerText.text;
            SceneManager.LoadScene("EndGame");
            StartCoroutine(SetEndGameTime(finalTime));
        }
    }
    public int getLifes()
    {
        return countLife;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 8) 
        {
            isTimerRunning = false;
            string finalTime = timerText.text;
            SceneManager.LoadScene("EndGame");
            StartCoroutine(SetEndGameTime(finalTime));
        }
    }
    IEnumerator SetEndGameTime(string finalTime)
    {
        yield return null;
        
        EndGameController endGameController = FindObjectOfType<EndGameController>();
        if (endGameController != null)
        {
            endGameController.SetFinalTime(finalTime);
        }
    }


   public void UpdateTimer()
    {
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00");
        timerText.text = $"Time: {minutes}:{seconds}";
    }
    void Update()
    {
        if (isTimerRunning)
        {
            UpdateTimer();
        }
    }
}
