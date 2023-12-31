using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class spikeMovement : MonoBehaviour
{
    float speed = 1000.0f;
    public float stabTime = 0.0f;
    public bool stabbing = false;
    public Vector2 movement = new Vector2(0, 0);
    public Toggle smoothstab;
    public Toggle vibrateToggle;
    public float waitTime = 0.0f;
    public bool waiting = false;

    public float intensity;
    public AudioClip vibrateClip;
    public AudioClip moveClip;
    Vector3 startPos;
    public AudioSource src;
    void Start()
    {
        startPos = transform.position;

    }

    void Update()
    {
        //every 5 seconds the spear will stab
        //all movement is managed by Vector2 movement
        //set movement = new Vector2(speed,0) to go right
        //set movement = new Vector2(-speed,0) to go left 

        if(vibrateToggle.isOn && stabTime < 5.0f)
        {
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * intensity;

            // Apply the offset to the sprite's position
            transform.position = startPos + randomOffset;
            src.PlayOneShot(vibrateClip);
        }
        else if (!vibrateToggle.isOn)
        {
            src.Stop();
        }
        if(stabTime >= 5.0f){
            src.Stop();
            src.PlayOneShot(moveClip);
            //first toggle, makes spear slow down slightly in flight
            if(smoothstab.isOn){
               speed -= 0.5f; 
            }
            movement = new Vector2(-speed,0);
            stabbing = true;
        }
        else if(!stabbing){
            stabTime += Time.deltaTime;
        }
        //makes the spear wait for a second when it hits the player/wall (toggled on or off)
        if(waiting){
            movement = new Vector2(0, 0);
            waitTime += Time.deltaTime;
            if(waitTime >= 1){
                waiting = false;
                waitTime = 0.0f;
                movement = new Vector2(speed, 0);
            }
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = movement;
    }
  



    private void OnCollisionEnter2D(Collision2D col)
    {
        //this if checks if it hit the left wall/player
        if((col.gameObject.CompareTag("platforms")||col.gameObject.CompareTag("Player")) && stabbing)
        {
            if(smoothstab.isOn){
                waiting = true;
                speed = 700;
            }
            movement = new Vector2(speed, 0);
            stabTime = 0;
            stabbing = false;
        }
        //this is the code for when the spike retracts, resetting for next stab
        else if((col.gameObject.CompareTag("platforms")) && !stabbing){
            movement = new Vector2(0, 0);
            if(smoothstab.isOn){
                speed = 1000;
            }
        }
    }
}
