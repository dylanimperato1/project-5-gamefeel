using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShake : MonoBehaviour
{

    public bool start = false;
    public AnimationCurve curve;
    public float duration;
    public Toggle _toggle;

    public static ScreenShake Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start && _toggle.isOn)
        {
            start = false;
            StartCoroutine(Shaking());
        }

    }

    IEnumerator Shaking()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPos;
    }
}
