using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public enum States
    {
        empty, full
    }

    public States currentState;

    private PlayerController player;
    private bool playerNear;
    public Transform chairPosition;
    public Transform foodPosition;

    private void Update()
    {
        if (playerNear && Input.GetButtonDown("Jump"))
        {
            switch (currentState)
            {
                case States.empty:
                    player.DropClient(this);
                    break;
                case States.full:
                    break;
                default:
                    break;
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
}
