using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Plate : MonoBehaviour
{
    public int id;
    public string namePlate;
    public int price;
    public float cookTime;
    private float cookTimer;
    public BoxCollider2D collider2D;
    public int kitchenSlot;
    public bool isEmpty = false;
    public Sprite emptyFood;

    private PlayerController player;
    private bool playerNear;


    private void Update()
    {
        if (playerNear && Input.GetButtonDown("Jump"))
        {
            GivePlate();
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

    public void GivePlate()
    {
        if (player.TakePlate(gameObject))
        {
            collider2D.enabled = false;
            gameObject.SetActive(false);
            FindObjectOfType<Kitchen>().SetSlotToFalse(kitchenSlot);
        }
    }

    public void EmptyPlate()
    {
        GetComponent<SpriteRenderer>().sprite = emptyFood;
    }

}


