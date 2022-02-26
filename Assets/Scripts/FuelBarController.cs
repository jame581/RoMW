using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts;

/// <summary>
/// Skript pro spravu paliva hrace
/// </summary>
public class FuelBarController : MonoBehaviour
{

    public Image fuel;
    public float min;
    public float timeToMinus;

    private Animator anim;
    private GameController gameController;
    private bool fuelEmpty;
    private float fuelValue;

    // Use this for initialization
    void Start()
    {
        fuelEmpty = false;
        fuelValue = 1.0f;
        GameObject controllerGameObject = GameObject.FindWithTag("GameController");
        if (controllerGameObject != null)
        {
            gameController = controllerGameObject.GetComponent<GameController>();
        }

        GameObject animObjectP = GameObject.FindWithTag("FuelPortrait");
        GameObject animObjectL = GameObject.FindWithTag("FuelLandscape");

        if (animObjectL != null)
        {
            anim = animObjectL.GetComponent<Animator>();
        }
        else
        {
            anim = animObjectP.GetComponent<Animator>();
        }

       

        fuel.fillAmount = 1;        
    }

    public void StartFuelBar()
    {
        Start();
        StartCoroutine(MinusFuel());
    }
    
    /// <summary>
    /// Odecitam palivo hraci po uplynuti stanovene doby
    /// </summary>
    /// <returns></returns>
    IEnumerator MinusFuel()
    {
        while (!fuelEmpty)
        {
            if (gameController.GameOver) // pokud je konec hry
            {
                yield break;
            }
            float value = fuel.fillAmount;  // aktualni stav paliva
            anim.SetFloat("FuelState", value);
            if (value <= 0) // pokud nezbyva palivo
            {
                fuelEmpty = true;
                yield return new WaitForSeconds(2.0f);
                if (value <= 0)
                {
                    gameController.SetGameOver(true);   // game over
                }
            }
            else  // jinak odecti palivo
            {
                value -= min;
                fuelValue -= min;
                fuel.fillAmount = value;
                if (value <= 0)
                {
                    fuel.enabled = false;
                }             
            }         
            yield return new WaitForSeconds(timeToMinus); // cekej cas timeToMinus
        }        
    }

    /// <summary>
    /// Metoda slouzi k pricteni paliva hraci
    /// </summary>
    /// <param name="value">Hodnota k pricteni</param>
    public void PlusFuel(float value)
    {
        float plus = fuel.fillAmount;
        plus += value;

        if (plus >= 1)
        {
            fuel.fillAmount = 1;
            fuelValue = 1;
        }
        else
        {
            fuel.fillAmount = plus;
        }
        fuelValue = fuel.fillAmount;
    }
}
