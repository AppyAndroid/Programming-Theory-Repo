using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timber : PickUp // INHERITANCE - child class
{
    public GameObject bridge;
    // Start is called before the first frame update

    protected override void HoldPickup(Transform parent) //POLYMORPHISM
    {
        pickUp.transform.SetParent(parent);
        y = 0; // Variable Changed from Parent Class
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.DownArrow))
        {
            HoldPickup(player); 
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && hasPickUp)
        {
            DropPickup();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bridge"))
        {
            bridge.SetActive(true);
            pickUp.SetActive(false);
            hasPickUp = false;
        }
    }
}
