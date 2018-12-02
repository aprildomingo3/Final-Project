using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APDOPlayer_Controller : MonoBehaviour
{

    public float speed;   
    public Text countText;
    public Text winText;
    public AudioSource fishCollect;
    public AudioSource animalSaved;

    private Rigidbody2D rb2d;  
    private int count;

    private float timer;

    public Text endText;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        fishCollect = GetComponent<AudioSource>();
        animalSaved = GetComponent<AudioSource>();

        count = 0;
        winText.text = "";
        endText.text = "";
        SetCountText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb2d.AddForce(movement * speed);

        //transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(movement.y - transform.position.y, movement.x - transform.position.x) * Mathf.Rad2Deg - 90);

        timer = timer + Time.deltaTime;
        if (timer >= 10)
        {
            endText.text = "You Lose!";
            StartCoroutine(ByeAfterDelay(2));

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FishCollect"))
        {
            other.gameObject.SetActive(false);
            fishCollect.Play();
            count = count + 1;
            //GameLoader.AddScore(1);
            SetCountText();
        }

        if (other.gameObject.CompareTag("AnimalSave"))
        {
            other.gameObject.SetActive(false);
            animalSaved.Play();
            count = count + 5;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            winText.text = "You Saved The Animal!";
            StartCoroutine(ByeAfterDelay(2));

        }
    }

    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        //GameLoader.gameOn = false;

    }
}