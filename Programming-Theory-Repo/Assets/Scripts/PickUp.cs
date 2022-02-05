using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour  // INHERITANCE - parent class
{
    public GameObject pickUp;
    protected bool hasPickUp;
    public float y;
    public static float x { get; private set; }  //Encapsulation
    public Transform player;
    //public Rigidbody2D playerRB;
    public GameObject chestOpen;
    public GameObject chestClosed;
    public GameObject endingText;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        hasPickUp = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {        
        if (other.CompareTag("Player")&& Input.GetKey(KeyCode.DownArrow))
        {
            HoldPickup(player);
        }
    }

    protected virtual void HoldPickup(Transform parent)  //POLYMORPHISM
    {
        y = 0.3f; // Variable to be changed in child class
        x = 0;  // Property cannot be changed
        pickUp.transform.SetParent(parent);
        transform.Translate(x, -y, 0);
        hasPickUp = true;
    }

    protected virtual void DropPickup()  //ABSTRACTION - This method has been taken out of UPDATE
    {
        y = 1f;
        x = 0;
        pickUp.transform.SetParent(null);
        transform.Translate(x, y, 0);
        hasPickUp = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Chest"))
        {
            pickUp.SetActive(false);
            chestClosed.SetActive(false);
            chestOpen.SetActive(true);
            endingText.SetActive(true);
            Debug.Log("Collided with " + other.gameObject.name);
            explosionParticle.Play();
            hasPickUp = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && hasPickUp)
        {
            DropPickup();
        }
    }
}
