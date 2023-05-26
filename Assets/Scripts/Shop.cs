using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class Shop : MonoBehaviour
{

    private PlayerController player;
    private Client client;
    public ScoreManager scoreManager;
    public GameObject shop;
    public TextMeshProUGUI textMoney;
    public bool playerNear;
    public bool shopOpen = false;
    public Sprite soldOut;
    public AudioSource buyAudio;

    public GameObject table3;
    public GameObject table3Sprite;
    public Image table3Button;
    public bool table3Bought;

    public GameObject table4;
    public GameObject table4Sprite;
    public Image table4Button;
    public bool table4Bought;

    public GameObject table5;
    public GameObject table5Sprite;
    public Image table5Button;
    public bool table5Bought;

    public GameObject tableclothStarted;
    public GameObject tablecloth3;
    public GameObject tablecloth4;
    public GameObject tablecloth5;
    public Image tableclothButton;
    public bool tableclothBought;

    public GameObject decoration;
    public Image decorationButton;
    public bool decorationBought;

    public Image speedButton;
    public bool speedBought;



    private void Update()
    {
        textMoney.text = scoreManager.yourMoney.ToString();

        if (playerNear && Input.GetButtonDown("Jump") && shopOpen == false)
        {
            Time.timeScale = 0f;
            player.isPaused = true;
            shopOpen = true;
            shop.SetActive(true);

        }
        else if (playerNear && Input.GetButtonDown("Jump") && shopOpen == true)
        {
            Time.timeScale = 1f;
            player.isPaused = false;
            shopOpen = false;
            shop.SetActive(false);
        }

        if(tableclothBought == true)
        {
            if (table3Bought == true)
            {
                tablecloth3.SetActive(true);
            }

            if (table4Bought == true)
            {
                tablecloth4.SetActive(true);
            }

            if (table5Bought == true)
            {
                tablecloth5.SetActive(true);
            }
        }

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


    public void BuyTable3()
    {
        if(scoreManager.yourMoney >= 250 && table3Bought == false)
        {
            scoreManager.yourMoney -= 250;
            table3Sprite.SetActive(true);
            table3.SetActive(true);
            table3Button.sprite = soldOut;
            buyAudio.Play();
            table3Bought = true;

        }
    }

    public void BuyTable4()
    {
        if (scoreManager.yourMoney >= 500 && table4Bought == false)
        {
            scoreManager.yourMoney -= 500;
            table4Sprite.SetActive(true);
            table4.SetActive(true);
            table4Button.sprite = soldOut;
            buyAudio.Play();
            table4Bought = true;

        }
    }

    public void BuyTable5()
    {
        if (scoreManager.yourMoney >= 750 && table5Bought == false)
        {
            scoreManager.yourMoney -= 750;
            table5Sprite.SetActive(true);
            table5.SetActive(true);
            table5Button.sprite = soldOut;
            buyAudio.Play();
            table5Bought = true;

        }
    }

    public void BuyTablecloth()
    {
        if (scoreManager.yourMoney >= 200 && tableclothBought == false)
        {
            scoreManager.yourMoney -= 200;
            tableclothStarted.SetActive(true);
            tableclothButton.sprite = soldOut;
            buyAudio.Play();
            tableclothBought = true;
        }
    }

    public void BuyDecoration()
    {
        if (scoreManager.yourMoney >= 300 && decorationBought == false)
        {
            scoreManager.yourMoney -= 300;
            decoration.SetActive(true);
            decorationButton.sprite = soldOut;
            buyAudio.Play();
            decorationBought = true;
        }
    }

    public void BuySpeed()
    {
        if (scoreManager.yourMoney >= 400 && speedBought == false)
        {
            scoreManager.yourMoney -= 400;
            speedButton.sprite = soldOut;
            player.playerSpeed += 1;
            buyAudio.Play();
            speedBought = true;
        }
    }



}
