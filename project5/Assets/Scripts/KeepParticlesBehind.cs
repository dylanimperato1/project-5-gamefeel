using UnityEngine;

public class KeepParticlesBehind : MonoBehaviour
{
    public GameObject player;
    public float distanceBehind;
    public float distanceDown;
    public float rotationTime;


    Rigidbody2D rb;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector3 particlePosition = player.transform.position -
                (Vector3.right * distanceBehind) - (Vector3.up * distanceDown);
        transform.position = Vector3.Lerp(transform.position, particlePosition, rotationTime);
        if (rb.velocity.y > 0.2)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 45f), rotationTime);
        }
        //else if (rb.velocity.y < -0.2)
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -45f), rotationTime);
        //}   
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotationTime);
        }
    }
}
