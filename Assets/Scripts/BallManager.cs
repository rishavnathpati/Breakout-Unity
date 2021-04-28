using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    
    #region Singleton

    private static BallManager _instance;
    public static BallManager Instance => _instance;

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

    [SerializeField]
    private Ball ballPrefab;
    private Ball initialBall;
    private Rigidbody2D initialBallRb;
    public float initialBallSpeed=250f;
    public List<Ball> Balls { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        InitialBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            Vector3 paddlePos = Paddle.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(paddlePos.x, paddlePos.y + .50f, 0);
            initialBall.transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0))
            {
                initialBallRb.isKinematic = false;
                initialBallRb.AddForce(new Vector2(0,initialBallSpeed), ForceMode2D.Impulse);
                GameManager.Instance.IsGameStarted = true;
            }

        }
    }
    private void InitialBall()
    {
        Vector3 paddlePos = Paddle.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePos.x, paddlePos.y + .50f, 0);
        initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        initialBallRb = initialBall.GetComponent<Rigidbody2D>();

        this.Balls = new List<Ball>
        {
            initialBall
        };
    }

}
