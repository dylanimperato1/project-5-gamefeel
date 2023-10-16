using UnityEngine;
using UnityEngine.UI;

public class SmoothJump : MonoBehaviour
{
    public bool grounded = false;
    public float jumpHeight = 5f;
    public float fallMultiplier = 1f;
    public float lowJumpMultiplier = 1f;
    public Toggle jumpAnimation;
    public Toggle smoothJump;
    public Toggle shortJump;
    public float jumpBufferLength = 0.2f; // max time between button press and jump
    private float jumpBufferCount;

    private bool isRotating = false;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float rotationTime = 0f;
    private float rotationDuration = 0.72f;

    private AudioSource _audioSource;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCount = jumpBufferLength;
        }


        // Allow jump if within coyote time or buffer time
        if (jumpBufferCount >= 0 && grounded)
        {
            grounded = false;
            rb.velocity += new Vector2(0, jumpHeight);
            isRotating = true;
            rotationTime = 0f;
            rotationDuration = 0.69f;
            startRotation = transform.rotation;
            targetRotation = startRotation * Quaternion.Euler(0, 0, -90f);
            jumpBufferCount = 0;
            _audioSource.Play();


        }

        jumpBufferCount -= Time.deltaTime;

        if (smoothJump.isOn)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }
        if (shortJump.isOn) {
                if (rb.velocity.y > float.Epsilon && !Input.GetButton("Jump") && !grounded)
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
                    rotationDuration = 0.34f;
                }
            }
        if (jumpAnimation.isOn)
        {
            if (isRotating)
            {
                RotateCube();
            }
        }
    }

    private void RotateCube()
    {
        rotationTime += Time.deltaTime / rotationDuration;
        transform.rotation = Quaternion.Slerp(startRotation, targetRotation, rotationTime);

        // If rotation is completed
        if (rotationTime >= 1f)
        {
            isRotating = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("platforms"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("platforms"))
        {
            grounded = false;
        }
    }
}
