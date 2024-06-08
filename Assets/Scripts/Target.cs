using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    private SimulationController simulationController;
    public GameObject explosionEffect; // Drag your explosion effect prefab here

    void Start()
    {
        simulationController = FindObjectOfType<SimulationController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Target Reached!");
            UnlockNewLevel();
            ShowExplosion(collision.gameObject);
            simulationController.EndSimulation();
            Invoke("LoadNextLevel", 1.3f);
        }
    }
    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    private void ShowExplosion(GameObject ball)
    {
        // Instantiate the explosion effect at the ball's position
        // Instantiate(explosionEffect, ball.transform.position, Quaternion.identity);
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        // Activate and start all particle systems within the explosion prefab
        ParticleSystem[] particleSystems = explosion.GetComponentsInChildren<ParticleSystem>();
        foreach (var ps in particleSystems)
        {
            ps.Play();
        }

        // Optionally, destroy the ball
        Destroy(ball);
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if nextSceneIndex is within the available scenes
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels to load. You have completed all levels!");
            // Optionally load a main menu or end screen here?
        }
    }
}
