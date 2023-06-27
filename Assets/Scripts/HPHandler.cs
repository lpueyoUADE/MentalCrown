using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPHandler : MonoBehaviour
{
    Text texto;
    Slider slider;

    void Start()
    {
        Personaje.OnHit.AddListener(modifyHealth);
        texto = GetComponentInChildren<Text>();
        slider = GetComponent<Slider>();

        setHealth(GameManager.instance.personaje.vida, Personaje.maxVida);

        LifePackController.OnUsedMedikit.AddListener(modifyHealth);
    }

    private void setHealth(int current, int max)
    {
        texto.text = current.ToString() + " / " + max.ToString();
        slider.value = (float)current / max;
    }
    private void modifyHealth()
    {
        setHealth(GameManager.instance.personaje.vida, Personaje.maxVida);
    }
}