using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private int lives;
    public int availableLives=3;
    public GameObject gameOverScreen;

    public static GameManager Instance => _instance;
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

    private void Start()
    {
        this.lives = this.availableLives;
        Ball.OnBallDeath += Ball_OnBallDeath;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Ball_OnBallDeath(Ball obj)
    {
        if (BallManager.Instance.Balls.Count <= 0)
        {
            this.lives--;
            if (this.lives < 1)
            {
                gameOverScreen.SetActive(true);
            }
            else
            {
                BallManager.Instance.ResetBalls();
                IsGameStarted = false;
                BrickManager.Instance.LoadLevel(BrickManager.Instance.currrentLevel);

            }
        }
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= Ball_OnBallDeath;
    }




    public bool IsGameStarted { get; set; }


}
