using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public float playerSpeed;
    private Animator animator;
    private float moveX;
    private float moveY;

    public GameObject plateSlot1;
    public GameObject plateSlot2;
    public GameObject clientSlot;
    public Kitchen kitchen;
    public Transform followPoint;

    public AudioSource pickAudio;
    public AudioSource DropAudio;

    public Image inventory1;
    public Image inventory2;

    public bool isPaused = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isPaused)
        {
            // Movimiento del jugador.
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");

            // Velocidad del jugador.
            rb2d.velocity = new Vector2(moveX, moveY) * playerSpeed;

            // Animaciones del jugador.
            animator.SetFloat("MoveX", moveX);
            animator.SetFloat("MoveY", moveY);
        }


    }


        // Al intentar llevarte un cliente comprueba si el slot está vacío, si lo está, se guarda.
    public bool TakeClient(GameObject client)
    {

        if (clientSlot == null)
        {
            clientSlot = client;
            clientSlot.GetComponent<Client>().currentState = Client.States.following;
            clientSlot.GetComponent<Client>().followTarget = followPoint;
            clientSlot.GetComponent<Client>().clientCollider.enabled = false;
            pickAudio.Play();
            return true;
        }
        else
        {
            return false;
        }
    }

        /* Al intentar dejar el cliente en una mesa comprueba si el slot del cliente no está vacío y si la mesa está vacía, si ambas condiciones se cumplen: deja al cliente en la mesa,
        cambia el estado de este y vacía el slot. */
    public void DropClient(Table table)
    {
        if (clientSlot != null && table.currentState == Table.States.empty)
        {
            table.currentState = Table.States.full;
            clientSlot.GetComponent<Client>().isFollowing = false;
            clientSlot.transform.position = table.chairPosition.position;
            clientSlot.GetComponent<Client>().currentState = Client.States.choosing;
            clientSlot.GetComponent<Client>().currentTable = table;
            clientSlot.GetComponent<Client>().animator.SetTrigger("Sit");
            clientSlot = null;
            DropAudio.Play();

        }
    }

        // Cuando, estando el cliente en el estado "waitingTakeOrder",
    public void TakeOrder(Plate plate)
    {
        kitchen.AddOrder(plate);
        DropAudio.Play();
    }

    public bool TakePlate(GameObject plate)
    {
        if (plateSlot1 == null)
        {
            plateSlot1 = plate;
            pickAudio.Play();
            inventory1.sprite = plateSlot1.GetComponent<SpriteRenderer>().sprite;
            inventory1.enabled = true;
            return true;
        }
        else if (plateSlot2 == null)
        {
            plateSlot2 = plate;
            pickAudio.Play();
            inventory2.sprite = plateSlot2.GetComponent<SpriteRenderer>().sprite;
            inventory2.enabled = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DropFood1(Table table)
    {
        plateSlot1.SetActive(true);
        plateSlot1.transform.position = table.foodPosition.position;
        plateSlot1 = null;
        inventory1.enabled = false;
        DropAudio.Play();
    }

    public void DropFood2(Table table)
    {
        plateSlot2.SetActive(true);
        plateSlot2.transform.position = table.foodPosition.position;
        plateSlot2 = null;
        inventory2.enabled = false;
        DropAudio.Play();
    }

    public void EmptySlots()
    {
        plateSlot1 = null;
        plateSlot2 = null;
        inventory1.enabled = false;
        inventory2.enabled = false;
    }
}
