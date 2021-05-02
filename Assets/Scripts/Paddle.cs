using UnityEngine;
public class Paddle : MonoBehaviour
{
    private static Paddle _instance;

    public static Paddle Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Camera mainCamera;

    private float paddlePosy;

    private SpriteRenderer sr;

    private Vector3 paddleLength;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        paddlePosy = this.transform.position.y;
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        PaddleMovement();
        //GetComponent.< Collider > ().bounds.size
        paddleLength = GetComponent<Collider2D>().bounds.size;
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

            //ballRb.velocity = Vector2.zero;
            float diff = (paddleCenter.x - pointOfContactWithPaddle.x) / paddleLength.x; // to normalise length
            Debug.Log(paddleLength.x + " " + diff);

            if (pointOfContactWithPaddle.x < paddleCenter.x)
            {
                //ballRb.AddForce(new Vector2(-Mathf.Sqrt((Mathf.Abs(diff))* BallManager.Instance.initialBallSpeed* BallManager.Instance.initialBallSpeed), Mathf.Sqrt(BallManager.Instance.initialBallSpeed*BallManager.Instance.initialBallSpeed * (1 - diff))), ForceMode2D.Impulse);
            }
            else
            {
                //ballRb.AddForce(new Vector2(Mathf.Sqrt((Mathf.Abs(diff)) * BallManager.Instance.initialBallSpeed * BallManager.Instance.initialBallSpeed), Mathf.Sqrt(BallManager.Instance.initialBallSpeed * BallManager.Instance.initialBallSpeed * (1 - diff))), ForceMode2D.Impulse);
            }
        }
    }
}
