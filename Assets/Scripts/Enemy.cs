using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int amountToIncreaseScore = 15;
    [SerializeField] int enemyHitPoints = 3;

    GameObject parentGameObject;

    Rigidbody rb;
    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        AddRigidbody();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void AddRigidbody()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        
        if (enemyHitPoints < 1)
        {
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        --enemyHitPoints;
        scoreBoard.IncreaseScore(amountToIncreaseScore);
    }
}
