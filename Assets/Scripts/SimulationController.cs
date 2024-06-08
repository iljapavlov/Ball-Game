using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
    public GameObject ball;
    public Button startButton;
    public Button restartButton;
    public Text timerText;
    public Text levelText;
    public GameObject[] draggableBlocks;

    private Vector3 initialBallPosition;
    private float startTime;
    private bool isSimulating = false;
    private float completionTime;
    private CameraController cameraController;

    void Start()
    {
        startButton.onClick.AddListener(StartSimulation);
        restartButton.onClick.AddListener(RestartSimulation);
        // restartButton.gameObject.SetActive(false);

        initialBallPosition = ball.transform.position;
        cameraController = FindObjectOfType<CameraController>();

        // Ensure blocks are draggable at the start
        SetBlocksDraggable(true);

        UpdateLevelText();
    }

    void Update()
    {
        if (isSimulating)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
        }
    }

    void StartSimulation()
    {
        startButton.interactable = false;
        restartButton.gameObject.SetActive(true);
        startTime = Time.time;
        isSimulating = true;
        SetBlocksDraggable(false);

        // Add force to the ball to start the simulation
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.simulated = true;

        // Enable camera follow
        cameraController.SetFollowBall(true);
    }

    public void EndSimulation()
    {
        isSimulating = false;
        completionTime = Time.time - startTime;
        startButton.interactable = true;
        SetBlocksDraggable(true);

        // Disable camera follow
        cameraController.SetFollowBall(false);
        cameraController.ResetCameraPosition();
    }

    void RestartSimulation()
    {
        EndSimulation();

        // Reset the ball's position and velocity
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.simulated = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        ball.transform.position = initialBallPosition;
        timerText.text = "0:00.00";
    }
    void UpdateLevelText()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        levelText.text = "Level: " + currentLevel;
    }


    private void SetBlocksDraggable(bool draggable)
    {
        foreach (var block in draggableBlocks)
        {
            var dragComponent = block.GetComponent<DraggableBlock>();
            if (dragComponent != null)
            {
                dragComponent.SetDraggable(draggable);
            }
        }
    }
}
