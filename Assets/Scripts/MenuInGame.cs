using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour
{
    private PlayerController player;
    public bool isPaused = false;
    public AudioSource music;
    public GameObject menu;
    public AudioSource click;
    public GameObject tutorial;


    private void Update()
    {
        Pause();
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void Pause()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                menu.SetActive(true);

                player.isPaused = true;
                music.Pause();
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1f;
                menu.SetActive(false);

                player.isPaused = false;
                music.Play();
                isPaused = false;
            }
            
        }
        
    }


    public void Resume()
    {
        click.Play();

        Time.timeScale = 1f;
        menu.SetActive(false);

        player.isPaused = false;
        music.Play();
        isPaused = false;
    }

    public void Restart()
    {
        click.Play();

        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
