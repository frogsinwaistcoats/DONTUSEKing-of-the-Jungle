using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 5f;
    public float turnspeed = 10f;
    private Rigidbody rb;
    public string horizontalInputAxis = "Horizontal";
    public string verticalInputAxis = "Vertical";

    void Start ()
    {
        rb = GetComponent<Rigidbody>();

    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * movespeed);

    }
}
