using UnityEngine;
public class Paddle : MonoBehaviour
{
    #region Singleton

    private static Paddle _instance;
    public static Paddle Instance => _instance;

    private void Awake()
    {
        //if (_instance == null)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        _instance = this;
        //}
    }

    #endregion
    private Camera mainCamera;

    private float paddlePosy;

    private SpriteRenderer sr;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        paddlePosy = this.transform.position.y;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        PaddleMovement();
    }

    private void PaddleMovement()
    {
        float leftClamp = 30 + this.sr.size.x / 2;
        float rightClamp = 1910 - this.sr.size.x / 2;
        float mousePosRange = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        float mousePosx = mainCamera.ScreenToWorldPoint(new Vector3(mousePosRange, 0, 0)).x;

        this.transform.position = new Vector3(mousePosx, paddlePosy, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 pointOfContactWithPaddle = collision.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballRb.velocity = Vector2.zero;
            float diff = paddleCenter.x - pointOfContactWithPaddle.x;
            if (pointOfContactWithPaddle.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(diff * 10)), BallManager.Instance.initialBallSpeed),ForceMode2D.Impulse);
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(diff * 10)), BallManager.Instance.initialBallSpeed), ForceMode2D.Impulse);
            }
        }
    }
}
