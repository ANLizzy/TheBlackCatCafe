using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour
{

    [System.Serializable]
    public struct PlateSlot
    {
        public GameObject plate;
        public Transform slotPosition;
        public int plateId;
    }

    public PlateSlot slot1;
    public PlateSlot slot2;
    private bool preparingSlot1;
    private bool preparingSlot2;
    public float slot1Timer;
    public float slot2Timer;
    [SerializeField]
    public Queue<Plate> plateOrders = new Queue<Plate>();
    public bool isCooking;
    private float cookingTimer;
    public Plate[] menu;
    private bool order1Created = false;
    private bool order2Created = false;

    public AudioSource appearAudio;

    public void SetSlotToFalse(int slot)
    {
        if (slot == 1)
        {
            preparingSlot1 = false;
        }
        else
        {
            preparingSlot2 = false;
        }
    }

    


    private void Update()
    {
        CheckQueue();
        PreparingOrders();
    }

    public void AddOrder(Plate order)
    {
        plateOrders.Enqueue(order);
    }

    public void CheckQueue()
    {
        if (plateOrders.Count > 0)
        {
            if (!preparingSlot1)
            {
                
                slot1.plateId = plateOrders.Peek().id;
                slot1Timer = plateOrders.Dequeue().cookTime;
                preparingSlot1 = true;
                order1Created = false;
                
            }
            else if (!preparingSlot2)
            {
                slot2.plateId = plateOrders.Peek().id;
                slot2Timer = plateOrders.Dequeue().cookTime;
                preparingSlot2 = true;
                order2Created = false;
            }
        }
    }

    public void PreparingOrders()
    {
        if (preparingSlot1)
        {
            slot1Timer -= Time.deltaTime;
            if(slot1Timer <= 0 && !order1Created)
            {
                order1Created = true;
                Plate aux = Instantiate(menu[slot1.plateId], slot1.slotPosition.position, Quaternion.identity);
                aux.kitchenSlot = 1;
                appearAudio.Play();
            }
        }

        if (preparingSlot2)
        {
            slot2Timer -= Time.deltaTime;
            if (slot2Timer <= 0 && !order2Created)
            {
                order2Created = true;
                Plate aux = Instantiate(menu[slot2.plateId], slot2.slotPosition.position, Quaternion.identity);
                aux.kitchenSlot = 2;
                appearAudio.Play();
            }
        }
    }



}
