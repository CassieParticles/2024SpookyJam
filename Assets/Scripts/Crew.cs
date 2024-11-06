using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crew : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int timer;
    [SerializeField] private float accelerationTime = 2f;
    [SerializeField] private float maxSpeed = 5f;
    private Vector2 movement;
    private float timeLeft;
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        DestroyObjectDelayed();
    }


    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timeLeft += accelerationTime;
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * maxSpeed);
    }

    void DestroyObjectDelayed()
    {
        
        Destroy(gameObject, timer);
    }
}


