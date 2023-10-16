using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerJump : MonoBehaviour
{
    bool grounded = false;
    float jumpHeight  = 5f;

   // public Animator animator;
   // Update is called once per frame

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown("space") && grounded)
        {
            grounded = false;
            //animator.SetBool("Grounded", grounded);
            rb.velocity += new Vector2(0, jumpHeight);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("floor"))
        {
            grounded = true;
            //animator.SetBool("Grounded", grounded);
        }
    }
}
