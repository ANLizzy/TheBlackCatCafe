using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int scoreAux;
    public int scoreDay;
    public int yourMoney;
    public int scoreTotal;
    public TextMeshProUGUI scoreText;
    public AudioSource pointsAudio;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int price)
    {
        scoreAux += price;
        UpdateScoreText();
    }


    public void UpdateScoreText()
    {
        pointsAudio.Play();
        scoreText.text = "Money: " + scoreAux.ToString();
    }

    public void TotalScore()
    {
        scoreDay = scoreAux;
        yourMoney += scoreAux;
        scoreTotal += scoreAux;

        scoreAux = 0;
        scoreText.text = "Money: " + scoreAux.ToString();
    }
}
