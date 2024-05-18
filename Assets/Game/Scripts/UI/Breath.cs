using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Breath : MonoBehaviour
{
    //A barra de f�lego (pulm�o) s� pode ser preenchida com valores entre 0 e 1
    [SerializeField] public Image breathBar;
    //Singleton
    public static Breath breathInstance;
    void Awake()
    {
        if (Breath.breathInstance == null)
        {
            breathInstance = this;
        }
        else
        {
            Destroy(Breath.breathInstance);
        }
    }
    public static Breath InstanceBreath { get { return Breath.breathInstance; } }
    //end Singleton
    void Start()
    {
        breathBar.fillAmount = 1f;//Pulm�o cheio
    }
    private void Update()
    {
        //if (breathBar.fillAmount <= 0f)
        //{
        //    Debug.Log("Cabou o ar!");
        //}
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
    }
}
