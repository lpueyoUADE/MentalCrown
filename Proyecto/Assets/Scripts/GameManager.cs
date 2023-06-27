using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Personaje personaje;

    public GameObject enemigosContainer;

    public int enemigosCount;
    public int enemigosTotal;

    public static UnityEvent onUpdateEnemyCount = new UnityEvent();

    public int currentLevel;

    private void Awake()
    {
        if (instance)
            Destroy(this.gameObject);
        else
            instance = this; 
    }

    private void Start()
    {
        Enemigo.onDieEnemy.AddListener(modifyEnemiesCount);
        enemigosCount = enemigosContainer.transform.childCount;
        enemigosTotal = enemigosCount;
        onUpdateEnemyCount.Invoke();
    }
    IEnumerator NextLevelCorutine(float time)
    {
        yield return new WaitForSeconds(time);

        if(currentLevel == 1)
        {
            SceneManager.LoadScene("Level2");

        } else if (currentLevel == 2)
        {
            SceneManager.LoadScene("Victory");
        }
    }
    private void modifyEnemiesCount()
    {
        enemigosCount--;
        onUpdateEnemyCount.Invoke();

        if (enemigosCount <= 0)
        {
            StartCoroutine(NextLevelCorutine(1f));
        }
    }

}
