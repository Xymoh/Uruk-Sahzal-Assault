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


    bool isDebugMode = false;
    PlayerController playerController;

    void Update()
    {
        TurnOffCollision();    
    }

    void Start() 
    {
        playerController = FindObjectOfType<PlayerController>();    
    }

    void OnCollisionEnter(Collision other)
    {
        if (isDebugMode) { return; }

        Debug.Log(this.name + " --bumped into-- " + other.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDebugMode) { return; }

        Debug.Log(this.name + " --bumped into-- " + other.gameObject.name);
        StartCrashSequence();
    }

    void StartCrashSequence() 
    {
        crashParticles.Play();
        ShipColliders.SetActive(false);
        playerController.SetLasersActive(false);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        Invoke("RestartLevel", levelLoadDelay);
    }

    void RestartLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }

    // Debug option
    void TurnOffCollision()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isDebugMode = !isDebugMode;

            if (isDebugMode == false)
            {
                Debug.Log("Debug Mode Disabled");
            }
            else 
            {
                Debug.Log("Debug Mode Enabled");
            }
        }
    }
}
