using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    
    public GameObject player;

    private ParticleSystem _ParticleSystem;
    private GameObject _trail;

    private GameObject Parent;

    private AudioSource deathAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Parent = player.transform.parent.gameObject;
        _ParticleSystem = Parent.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        _trail = Parent.transform.GetChild(2).gameObject;
        deathAudioSource = GetComponent<AudioSource>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !PlayerRespawn.Instance.IsImmune)
        {

            _ParticleSystem.Play();
            ScreenShake.Instance.start = true;
            deathAudioSource.Play();
            player.SetActive(false);
            _trail.SetActive(false);

        }
    }


}
