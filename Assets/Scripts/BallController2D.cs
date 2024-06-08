using UnityEngine;

public class BallController2D : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 initialForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.AddForce(initialForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            GameManager2D.Instance.LevelComplete();
        }
    }
}
