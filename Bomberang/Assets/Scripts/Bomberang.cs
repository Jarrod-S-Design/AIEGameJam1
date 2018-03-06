using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Bomberang : MonoBehaviour
{
    [HideInInspector]
    public GameObject currentPlayer;
    [HideInInspector]
    public bool isExploded;

    public TrailColour trailColour;

    [SerializeField] float startTime = 10.0f;
    [SerializeField] float intialOffset = 0.0f;
    [SerializeField] float minVelocityToReturn = 2.0f;
    [SerializeField] float normalDrag = 0.5f;
    [SerializeField] float returnDrag = 10f;

    Rigidbody rb;
    Collider collider;

    [HideInInspector]
    public bool isHeld;
    bool onReturnFlight;

    [HideInInspector]
    public float timer = 10;

    float flightTime;

    void Awake()
    {
        //currentPlayer = null;
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        isExploded = false;
        isHeld = false;
        onReturnFlight = false;
        flightTime = 0;
        timer = startTime;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            isExploded = false;
        }

        if (isHeld)
        {
            //transform.position = currentPlayer.transform.position;

            if (timer <= 0)
            {
                // BOOM!!!
                isExploded = true;
            }
        }
        else
        {
            flightTime += Time.deltaTime;
            if (onReturnFlight)
            {
                // Add force towards the player
                Vector3 vecBetween = (currentPlayer.transform.position - transform.position);
                Vector3 dirToPlayer = vecBetween.normalized;
                rb.AddForce(dirToPlayer * flightTime * flightTime, ForceMode.Impulse);
            }
            else
            {
                if (flightTime > 0.5f && rb.velocity.magnitude < minVelocityToReturn)
                {
                    StartReturn();
                    flightTime = 0;
                }
            }
        }
    }

    public void Shoot(Vector3 force)
    {
        transform.Translate(force.normalized * intialOffset);
        rb.drag = normalDrag;
        rb.AddForce(force, ForceMode.VelocityChange);
        isHeld = false;
        onReturnFlight = false;
        collider.isTrigger = false;
        flightTime = 0;
        rb.velocity = Vector3.zero;
    }

    void StartReturn()
    {
        onReturnFlight = true;
        rb.drag = returnDrag;
        collider.isTrigger = true;
    }

    public void HitPlayer(GameObject player)
    {
        currentPlayer = player;
        PlayerController hitPlayer = player.GetComponent<PlayerController>();
        trailColour.SetColour(hitPlayer.color);
        // Make that player the player holding this bomberang
        hitPlayer.Hit(gameObject);
        isHeld = true;
        rb.drag = normalDrag;
        onReturnFlight = false;
        collider.isTrigger = true;
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isHeld == false && collision.collider.CompareTag("Player"))
        {
            HitPlayer(collision.collider.gameObject);
        }
        // Set the velocity to zero, making it start the return flight
        else if (onReturnFlight == false)
        {
            rb.velocity = Vector3.zero;
            StartReturn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHeld == false && other.CompareTag("Player"))
        {
            HitPlayer(other.gameObject);
        }
    }
}
