using System;
using System.Collections.Generic;
using UnityEngine;
public class BrickManager : MonoBehaviour
{
    private static BrickManager _instance;

    public static BrickManager Instance => _instance;

    public Sprite[] Sprites;

    private int maxRows = 6, maxCols = 12;

    private GameObject brickContainer;

    public int currrentLevel;

    private readonly float initialBrickPositionX = -7.7f;

    private readonly float initialBrickPositionY = 4.15f;

    public Brick brickPrefab;

    public object brickColor;

    private float shiftAmountY = 0.73f;

    private float shiftAmountX = 1.4f;

    public Color[] BrickColors;

    public int InitialBrickCount { get; set; }

    private List<Brick> remainingBricks { get; set; }

    public List<int[,]> LevelData { get; set; }

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
        this.brickContainer = new GameObject("Bricks Container");
        this.remainingBricks = new List<Brick>();
        this.LevelData = this.LoadLevelsData();
        this.GenerateBricks();
    }

    private void GenerateBricks()
    {
        int[,] currentLevelData = this.LevelData[this.currrentLevel];
        float currentSpawnX = initialBrickPositionX;
        float currentSpawnY = initialBrickPositionY;
        float zShift = 0;

        for (int row = 0; row < this.maxRows; row++)
        {
            for (int col = 0; col < this.maxCols; col++)
            {
                int brickType = currentLevelData[row, col];

                if (brickType > 0)
                {
                    Brick newBrick = Instantiate(brickPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f - zShift), Quaternion.identity) as Brick;
                    newBrick.Init(brickContainer.transform, this.Sprites[brickType - 1], this.BrickColors[brickType], brickType);

                    this.remainingBricks.Add(newBrick);
                    zShift += 0.0001f;
                }
                currentSpawnX += shiftAmountX;
                if (col + 1 == this.maxCols)
                {
                    currentSpawnX = initialBrickPositionX;
                }
            }
            currentSpawnY -= shiftAmountY;
        }
        this.InitialBrickCount = this.remainingBricks.Count;
    }

    private List<int[,]> LoadLevelsData()
    {
        TextAsset text = Resources.Load("Level1") as TextAsset;
        string[] rows = text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        List<int[,]> levelData = new List<int[,]>();
        int[,] currentlevel = new int[maxRows, maxCols];
        int currentRow = 0;
        for (int row = 0; row < rows.Length; row++)
        {
            string line = rows[row];
            if (line.IndexOf("--") == -1)
            {
                string[] bricks = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < bricks.Length; col++)
                {
                    currentlevel[currentRow, col] = int.Parse(bricks[col]);
                }
                currentRow++;
            }
            else
            {
                currentRow = 0;
                levelData.Add(currentlevel);
                currentlevel = new int[maxRows, maxCols];
            }
        }
        return levelData;
    }

    private void Update()
    {
    }
}
