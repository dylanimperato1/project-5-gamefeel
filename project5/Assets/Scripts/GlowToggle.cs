using UnityEngine;
using UnityEngine.UI;

public class GlowToggle : MonoBehaviour
{
    public Toggle _GlowToggle;

    private void Start()
    {
        _GlowToggle.onValueChanged.AddListener(OnToggleChanged);
        
    }

    private void OnToggleChanged(bool isOn)
    {
        gameObject.SetActive(isOn);
    }
}
