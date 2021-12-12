using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("Amount of seconds to invoke reload level upon the death")] [SerializeField] 
    int levelLoadDelay = 3;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] GameObject ShipColliders;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(this.name + " --bumped into-- " + other.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.name + " --bumped into-- " + other.gameObject.name);
        StartCrashSequence();
    }

    void StartCrashSequence() 
    {
        crashParticles.Play();
        ShipColliders.SetActive(false);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        Invoke("RestartLevel", levelLoadDelay);
    }

    void RestartLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }
}
