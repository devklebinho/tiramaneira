using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Toggle fullscreenToggle, vSyncToggle;
    [SerializeField] TMP_Text txtResolutionFormat;
    [SerializeField] int selectedResolution = 0;

    public ResolutionItem[] resolutions;
    

    void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
        if(QualitySettings.vSyncCount == 0)
            vSyncToggle.isOn = false;
        else
            vSyncToggle.isOn = true;

        selectedResolution = 0;
        UpdateResolutionLabel();
    }

    void Update()
    {
        
    }

    public void ApplyGraphics()
    {
        Debug.Log("Apply Graphics");
        Screen.fullScreen = fullscreenToggle.isOn;
        QualitySettings.vSyncCount = vSyncToggle.isOn ? 1 : 0;//operador ternário
        Screen.SetResolution(resolutions[selectedResolution].hResolution, resolutions[selectedResolution].vResolution, fullscreenToggle.isOn);
    }

    public void UpdateResolutionLabel()
    {
        txtResolutionFormat.text = resolutions[selectedResolution].hResolution + "x" + resolutions[selectedResolution].vResolution; 
    }

    public void PreviousResolution()
    {
        selectedResolution--;
        if (selectedResolution < 0)
            selectedResolution = 0;
        UpdateResolutionLabel();
    }

    public void NextResolution()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Length -1)
            selectedResolution = resolutions.Length -1;
        UpdateResolutionLabel();
    }
}

[System.Serializable]
public class ResolutionItem
{
    public int hResolution, vResolution;
}