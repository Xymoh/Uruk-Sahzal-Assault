using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    [SerializeField] int amountToIncreaseScore = 15;

    ScoreBoard scoreBoard;

    void Start() 
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();    
    }

    void OnParticleCollision(GameObject other)
    {
        KillEnemy();
        ProcessHit();
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }

    void ProcessHit()
    {
        scoreBoard.IncreaseScore(amountToIncreaseScore);
    }
}
