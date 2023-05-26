using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{

    public int day;
    [SerializeField]
    private float dayTimer;
    public float dayTime;
    public int level = 0;
    public bool dayFinished = true;
    public ClientSpawner spawner;
    public ScoreManager scoreManager;
    private PlayerController player;
    public bool playerNear;

    public GameObject tutorial2;
    public GameObject summary;
    public TextMeshProUGUI textScoreDay;
    public TextMeshProUGUI textYourMoney;
    public TextMeshProUGUI textTotalScore;

    public BoxCollider2D startDayCollider;
    public GameObject startDayText;

    private void Start()
    {
        spawner = FindObjectOfType<ClientSpawner>();
        scoreManager = FindObjectOfType<ScoreManager>();
        // StartDay();
        spawner.isActive = false;
        scoreManager.scoreDay = 0;
    }

    private void Update()
    {
        if (!dayFinished)
        {
            dayTimer -= Time.deltaTime;

            if (dayTimer <= 0)
            {
                spawner.isActive = false;
            }

            if (dayTimer <= 0 && spawner.countClient == 0 && !dayFinished)
            {
                FinishDay();
                dayFinished = true;
            }
        }


        if (playerNear && Input.GetButtonDown("Jump"))
        {
            StartDay();
        }

    }

    public void StartDay()
    {
        startDayCollider.enabled = false;
        startDayText.SetActive(false);

        dayFinished = false;
        dayTimer = dayTime + day * 5;

        switch(day){
            case 5:
                level = 1;
                break;
        }

        scoreManager.scoreDay = 0;
        spawner.isActive = true;

    }

    public void FinishDay()
    {
        Time.timeScale = 0f;
        spawner.isActive = false;
        day++;
        scoreManager.TotalScore();
        textScoreDay.text = "Earned: " + scoreManager.scoreDay.ToString();
        textYourMoney.text = "Your pockets: " + scoreManager.yourMoney.ToString();
        textTotalScore.text = "Total: " + scoreManager.scoreTotal.ToString();

        summary.SetActive(true);
    }

    public void CloseSummary()
    {
        summary.SetActive(false);
        Time.timeScale = 1f;
        startDayCollider.enabled = true;
        startDayText.SetActive(true);
    }

    public void SaveAndExit()
    {
        SceneManager.LoadScene("Menu");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerController>();
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
            playerNear = false;
        }
    }


    public  void StartFirstDay()
    {

        tutorial2.SetActive(false);
        StartDay();

    }

}
