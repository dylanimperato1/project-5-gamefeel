using UnityEngine;

public enum MovementState
{
    Left,
    Right,
    Up,
    Down
}
public class MovingSpike : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3 direction = Vector3.left;


    private bool firstCollisionOccured = false;
    public MovementState currentState;

    void Start()
    {
   
    }

    void Update()
    {
        MoveSpike();
    }

    private void MoveSpike()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!firstCollisionOccured)
        {
            firstCollisionOccured = true;
        }
        else
        {
            if (other.gameObject.CompareTag("platforms"))
            {
                // Rotate
                transform.rotation *= Quaternion.Euler(0, 0, -90f);
                switch (currentState)
                {
                    case MovementState.Right:
                        transform.position = new Vector3(8.21f, 3.9f, 0f);
                        currentState = MovementState.Down;
                        break;
                    case MovementState.Down:
                        transform.position = new Vector3(7.9f, -4.21f, 0f);
                        currentState = MovementState.Left;
                        break;
                    case MovementState.Left:
                        transform.position = new Vector3(-8.21f, -3.9f, 0f);
                        currentState = MovementState.Up;
                        break;
                    case MovementState.Up:
                        transform.position = new Vector3(-7.9f, 4.21f, 0f);
                        currentState = MovementState.Right;
                        break;
                }
            }
        }
            
    }
}
