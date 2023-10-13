using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    float speed = 50.0f;
    float jumpHeight = 100f;
    bool grounded = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(speed * inputX, 0, 0);
        if (Input.GetKeyDown("space") && grounded)
        {
            // Debug.Log("jumped");
            grounded = false;
            //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(0, jumpHeight);
        }
        transform.Translate(move * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("floor"))
        {
            grounded = true;
        }
    }
}
