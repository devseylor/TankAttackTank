using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int numberOfEnemies;
    private GameObject[] enemies;


    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numberOfEnemies = enemies.Length;
    }
    
    public void EnemyDecrease()
    {
        numberOfEnemies--;
        if(numberOfEnemies == 0)
        {
            StartCoroutine(ReloadSceneCoroutine());
        }
    }

    public void PlayerDeath()
    {
        StartCoroutine(ReloadSceneCoroutine());
    }

    IEnumerator ReloadSceneCoroutine()
    {
        yield return new WaitForSeconds(3);
        ReloadScene();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
