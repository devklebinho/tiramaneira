using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Breath : MonoBehaviour
{
    //A barra de f�lego (pulm�o) s� pode ser preenchida com valores entre 0 e 1
    [SerializeField] public Image breathBar;
    private PlayerScript playerScript;

    void Start()
    {
        playerScript = FindFirstObjectByType<PlayerScript>();
        breathBar.fillAmount = 1f;//Pulm�o cheio
    }
    /// <summary>
    /// Incrementa o fillAmount da imagem a cada chamada
    /// </summary> 
    /// <param name="amount"></param>
    public void IncreaseBreath(float amount)
    {
        breathBar.fillAmount += amount;
    }
    /// <summary>
    /// Decrementa o fillAmount da imagem a cada chamada
    /// </summary>
    /// <param name="amount"></param>
    public void DecreaseBreath(float amount)
    {
        breathBar.fillAmount -= amount;
        
        if (breathBar.fillAmount <= 0)
        {
            playerScript.DeathState();
        }
    }
}
