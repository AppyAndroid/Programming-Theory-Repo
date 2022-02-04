using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour  // INHERITANCE - parent class
{
    public GameObject pickUp;
    public bool hasPickUp;
    public float y;
    public Transform player;
    //public Rigidbody2D playerRB;
    public GameObject chestOpen;
    public GameObject chestClosed;
    public ParticleSystem explosionParticle;
    public AudioClip openChestSound;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        hasPickUp = false;
        //playerRB = GetComponent<Rigidbody2D>();
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
        pickUp.transform.SetParent(parent);
        transform.Translate(0, -y, 0);
        hasPickUp = true;
    }

    protected virtual void DropPickup()
    {
        pickUp.transform.SetParent(null);
        transform.Translate(0, 1f, 0);
        hasPickUp = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Chest"))
        {
            pickUp.SetActive(false);
            chestClosed.SetActive(false);
            chestOpen.SetActive(true);
            Debug.Log("Collided with " + other.gameObject.name);
            explosionParticle.Play();
            playerAudio.PlayOneShot(openChestSound, 1.0f);
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
