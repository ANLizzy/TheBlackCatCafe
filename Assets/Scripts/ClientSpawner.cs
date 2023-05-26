using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnSlot
    {
        public Transform clientPosition;
        public bool thereIsClient;
    }

    public GameObject client;

    public Client[] allClients;
  
    private float timer;
    public float time;
    public float firsClientTime;
    public SpawnSlot Spawn1;
    public SpawnSlot Spawn2;
    public SpawnSlot Spawn3;

    public bool isActive;
    public int countClient;

    public AudioSource appearAudio;

    private void Start()
    {
        
        timer = firsClientTime;
    }



    private void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                if (Spawn1.thereIsClient == false)
                {
                    Client aux1 = Instantiate(allClients[UnityEngine.Random.Range(0, allClients.Length)], Spawn1.clientPosition.position, Quaternion.identity).GetComponent<Client>();
                    Spawn1.thereIsClient = true;
                    aux1.spawnerSlot = 1;
                    countClient++;
                    appearAudio.Play();
                }
                else if (Spawn2.thereIsClient == false)
                {
                    Client aux2 = Instantiate(allClients[UnityEngine.Random.Range(0, allClients.Length)], Spawn2.clientPosition.position, Quaternion.identity).GetComponent<Client>();
                    Spawn2.thereIsClient = true;
                    aux2.spawnerSlot = 2;
                    countClient++;
                    appearAudio.Play();
                }
                else if (Spawn3.thereIsClient == false)
                {
                    Client aux3 = Instantiate(allClients[UnityEngine.Random.Range(0, allClients.Length)], Spawn3.clientPosition.position, Quaternion.identity).GetComponent<Client>();
                    Spawn3.thereIsClient = true;
                    aux3.spawnerSlot = 3;
                    countClient++;
                    appearAudio.Play();
                }

                timer = time;
            }
        }
    }


    public void SetSlotToFalse(int slot)
    {
        if (slot == 1)
        {
            Spawn1.thereIsClient = false;
        }
        else if(slot ==2)
        {
            Spawn2.thereIsClient = false;
        }
        else
        {
            Spawn3.thereIsClient = false;
        }
    }


}
