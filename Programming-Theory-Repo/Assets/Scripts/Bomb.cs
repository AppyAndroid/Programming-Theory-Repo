using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : PickUp
{
    public ParticleSystem explosion;
    public GameObject bolder;
    // Start is called before the first frame update

    protected override void HoldPickup(Transform parent) //POLYMORPHISM
    {
        pickUp.transform.SetParent(parent);
        y = 0; // Variable Changed from Parent Class
    }

    protected override void DropPickup() //POLYMORPHISM
    {
        pickUp.transform.SetParent(null);
        y = 0; // Variable Changed from Parent Class
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.DownArrow))
        {
            HoldPickup(player);
            hasPickUp = true;
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
            hasPickUp = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bolder"))
        {
            bolder.SetActive(false);
            pickUp.SetActive(false);
            explosion.Play();
            hasPickUp = false;
        }
    }
}
