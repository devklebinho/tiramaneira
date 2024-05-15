using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Breath : MonoBehaviour
{
    //A barra de fôlego (pulmão) só pode ser preenchida com valores entre 0 e 1
    [SerializeField] Image breathBar;

    //Singleton
    public static Breath breathInstance;

    void Awake()
    {
        if(Breath.breathInstance == null)
        {
            breathInstance = this;
        }
        else
        {
            Destroy(Breath.breathInstance);
        }
    }
    //end Singleton

    void Start()
    {
        breathBar.fillAmount = 1f;//Pulmão cheio
    }

    private void Update()
    {
        if(breathBar.fillAmount <= 0f)
        {
            Debug.Log("Cabou o ar!");
        }
    }

    /// <summary>
    /// Incrementa o fillAmount da imagem a cada chamada
    /// </summary> 
    /// <param name="amount">float de 0 a 10</param>
    public void IncreaseBreath(float amount)
    {
        amount = amount / 100;
        breathBar.fillAmount += amount;
    }

    /// <summary>
    /// Decrementa o fillAmount da imagem a cada chamada
    /// </summary>
    /// <param name="amount">float de 0 a 10</param>
    public void DecreaseBreath(float amount)
    {
        amount = amount / 100;
        breathBar.fillAmount -= amount;
    }
}
