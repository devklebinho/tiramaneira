using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Toggle fullscreenToggle, vSyncToggle;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ApplyGraphics()
    {
        Debug.Log("Apply Graphics");
        Screen.fullScreen = fullscreenToggle.isOn;
        QualitySettings.vSyncCount = vSyncToggle.isOn ? 1 : 0;//operador ternário
    }
}
