using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public AudioSource audioSource;  // Drag the AudioSource you want to toggle here
    public Toggle soundToggle;

    private void Start()
    {
        soundToggle.onValueChanged.AddListener(OnToggleChanged);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnToggleChanged(bool isOn)
    {
        audioSource.mute = !isOn;  // Mute if isOn is false, unmute if true
    }
}
