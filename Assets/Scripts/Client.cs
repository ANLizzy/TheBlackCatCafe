using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{

    public int spawnerSlot;
    public enum States
    {
        waitingTable, following, choosing, waitingTakeOrder, waitingFood, eating, waitingPay
    }

    public States currentState;

    public float waitTableTime;
    [SerializeField] private float waitTableTimer;
    public float chooseTime;
    private float chooseTimer;
    public float waitTakeOrderTime;
    private float waitTakeOrderTimer;
    public float waitFoodTime;
    private float waitFoodTimer;
    public float eatTime;
    private float eatTimer;
    public int level;

    private Shop shop;

    private PlayerController player;
    public bool playerNear;
    public Plate[] menu;
    private Plate order;
    public Table currentTable;
    public Plate currentPlate;
    private bool orderTaken = false;

    public BoxCollider2D clientCollider;
    public BoxCollider2D waitClient;
    public BoxCollider2D tableClient;
              
    public SpriteRenderer bubble;
    public SpriteRenderer food;
    public GameObject exclamation;
    public AudioSource exclamationAudio;
    public GameObject timerVisual;
    public Slider timer;


    public Animator animator;
    public bool isFollowing = false;
    public Transform followTarget;
    public float followSpeed;


    private void Start()
    {
        level = FindObjectOfType<DayManager>().level;
        currentState = States.waitingTable;



        waitTableTimer = waitTableTime - level;
        chooseTimer = chooseTime - level;
        waitTakeOrderTimer = waitTakeOrderTime - level;
        waitFoodTimer = waitFoodTime - level;
        eatTimer = eatTime - level;

    }



    private void Update()
    {
        if (playerNear && Input.GetButtonDown("Jump"))
        {
            switch (currentState)
            {
                case States.waitingTable:
                    PickClient(player);
                    break;
                case States.choosing:
                    break;
                case States.waitingTakeOrder:
                    if (!orderTaken)
                    {
                        orderTaken = true;
                        GiveOrder(player);
                    }
                    break;
                case States.waitingFood:
                    RecieveFood();
                    break;
                case States.eating:
                    break;
                case States.waitingPay:
                    Pay();
                    break;
                default:
                    break;
            }
        }

        switch (currentState)
        {
            case States.waitingTable:
                WaitingTable();
                break;
            case States.choosing:
                ChoosingPlate();
                break;
            case States.waitingTakeOrder:
                WaitingTakeOrder();
                break;
            case States.waitingFood:
                WaitingFood();
                break;
            case States.eating:
                Eating();
                break;
            case States.waitingPay:
                break;
            default:
                break;
        }

        if (isFollowing)
        {
            transform.position = Vector2.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
            Animations();
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



    public void WaitingTable()
    {
        timerVisual.SetActive(true);
        timer.maxValue = waitTableTime;
        waitTableTimer -= Time.deltaTime;
        

        if(waitTableTimer >= 0)
        {
            timer.value = waitTableTimer;
        }

        if (waitTableTimer <= 0)
        {
            FindObjectOfType<ClientSpawner>().SetSlotToFalse(spawnerSlot);
            FindObjectOfType<ClientSpawner>().countClient--;
            animator.SetTrigger("Out");
            Destroy(gameObject, 0.5f);
        }
    }

    public void PickClient(PlayerController player)
    { 
        if (player.TakeClient(gameObject))
        {
            timerVisual.SetActive(false);
            waitClient.enabled = false;
            isFollowing = true;
            FindObjectOfType<ClientSpawner>().SetSlotToFalse(spawnerSlot);
            
        }
    }

    public void ChoosingPlate()
    {
        chooseTimer -= Time.deltaTime;


        if (chooseTimer <= 0)
        {
            int num = UnityEngine.Random.Range(0, menu.Length);
            order = menu[num];
            currentState = Client.States.waitingTakeOrder;
            bubble.gameObject.SetActive(true);
            food.sprite = order.GetComponent<SpriteRenderer>().sprite;
            food.gameObject.SetActive(true);
            exclamation.SetActive(true);
            exclamationAudio.Play();
            tableClient.enabled = true;
        }
    }

    public void WaitingTakeOrder()
    {
        timerVisual.SetActive(true);
        timer.maxValue = waitTakeOrderTime;
        waitTakeOrderTimer -= Time.deltaTime;

        if (waitTakeOrderTimer >= 0)
        {
            timer.value = waitTakeOrderTimer;
        }

        if (waitTakeOrderTimer <= 0)
        {
            currentTable.currentState = Table.States.empty;
            FindObjectOfType<ClientSpawner>().countClient--;
            animator.SetTrigger("Out");
            Destroy(gameObject, 0.5f);
        }
    }

    private void GiveOrder(PlayerController player)
    {
        player.TakeOrder(order);
        currentState = Client.States.waitingFood;
        exclamation.SetActive(false);
        timerVisual.SetActive(false);
    }

    private void WaitingFood()
    {
        timerVisual.SetActive(true);
        timer.maxValue = waitFoodTime;
        waitFoodTimer -= Time.deltaTime;

        if (waitFoodTimer >= 0)
        {
            timer.value = waitFoodTimer;
        }

        if (waitFoodTimer <= 0)
        {
            currentTable.currentState = Table.States.empty;
            FindObjectOfType<ClientSpawner>().countClient--;
            animator.SetTrigger("Out");
            Destroy(gameObject);
        }
    }

    private void RecieveFood()
    {

        if (player.plateSlot1 != null && player.plateSlot1.GetComponent<Plate>().id == order.id)
        {
            timerVisual.SetActive(false);
            currentPlate = player.plateSlot1.GetComponent<Plate>();
            player.DropFood1(currentTable);
            bubble.gameObject.SetActive(false);
            food.gameObject.SetActive(false);
            currentState = Client.States.eating;
        }
        else if (player.plateSlot2 != null && player.plateSlot2.GetComponent<Plate>().id == order.id)
        {
            timerVisual.SetActive(false);
            currentPlate = player.plateSlot2.GetComponent<Plate>();
            player.DropFood2(currentTable);
            bubble.gameObject.SetActive(false);
            food.gameObject.SetActive(false);
            currentState = Client.States.eating;
        }

    }

    private void Eating()
    {
        eatTimer -= Time.deltaTime;

        if (eatTimer <= 0)
        {
            currentPlate.EmptyPlate();
            currentState = Client.States.waitingPay;

        }
    }

    private void Pay()
    {
        
        ScoreManager.instance.AddScore(currentPlate.price);
        currentTable.currentState = Table.States.empty;
        FindObjectOfType<ClientSpawner>().countClient--;
        Destroy(currentPlate.gameObject);
        Destroy(gameObject);

    }

    private void Animations()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetTrigger("Right");
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetTrigger("Left");
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            animator.SetTrigger("Up");
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            animator.SetTrigger("Down");
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            animator.SetTrigger("Idle");
        }
    }

}
