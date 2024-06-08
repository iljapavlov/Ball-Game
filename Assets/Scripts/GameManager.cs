using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2D : MonoBehaviour
{
    public static GameManager2D Instance;
    private BallController2D ball;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ball = FindObjectOfType<BallController2D>();
    }

    public void StartLevel()
    {
        ball.Launch();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LevelComplete()
    {
        Debug.Log("Level Complete!");
        // level complete logic here
    }
}
