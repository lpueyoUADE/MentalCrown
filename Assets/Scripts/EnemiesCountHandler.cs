using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesCountHandler : MonoBehaviour
{
    Text texto;

    void Start()
    {
        GameManager.onUpdateEnemyCount.AddListener(setCount);
        texto = GetComponent<Text>();
    }
    private void setCount()
    {
        int current = GameManager.instance.enemigosCount;
        int max = GameManager.instance.enemigosTotal;
        texto.text = current.ToString() + " / " + max.ToString();
    }
}
