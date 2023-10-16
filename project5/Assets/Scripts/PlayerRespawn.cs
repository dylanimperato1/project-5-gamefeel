using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    public Button respawnButton;   // Drag the UI button in the inspector
    public float immunityTime = 1.5f; // Time in seconds for which the player is immune
    public float flashDuration = 0.1f;
    public bool IsImmune;
    private Vector3 initialPosition;
    private Renderer currentPlayerRenderer;
    private GameObject PlayerCube;
    private GameObject _trail;
    private AudioSource _audioSource;

    public Toggle animations;
    public static PlayerRespawn Instance;

    private void Awake()
    {
        Instance = this;   
    }
    private void Start()
    {
        // Save the player's initial position for respawning
        initialPosition = transform.position;
        PlayerCube = transform.GetChild(1).gameObject;
        _trail = transform.GetChild(2).gameObject;
        currentPlayerRenderer = PlayerCube.GetComponent<Renderer>();

        _audioSource = GetComponent<AudioSource>(); 

        // Set up button click listener
        respawnButton.onClick.AddListener(Respawn);
    }
    private void Update()
    {
        if (!animations.isOn)
        {
            _trail.SetActive(false);
        }
    }

    private void Respawn()
    {
        transform.position = initialPosition; // Reset player position
        transform.rotation = Quaternion.identity;
        PlayerCube.SetActive(true);
        PlayerCube.transform.rotation = Quaternion.identity;
        if (animations.isOn) _trail.SetActive(true);
        //gameObject.SetActive(true);
        IsImmune = true;
        _audioSource.Play();
        StartFlashing();
        Invoke("StopFlashing", immunityTime);
    }

    private void StartFlashing()
    {
        InvokeRepeating("ToggleVisibility", 0, flashDuration);
    }

    private void StopFlashing()
    {
        IsImmune = false;
        CancelInvoke("ToggleVisibility");
        currentPlayerRenderer.enabled = true;  // Ensure the player is visible at the end
    }

    private void ToggleVisibility()
    {
        currentPlayerRenderer.enabled = !currentPlayerRenderer.enabled;
    }
} 
