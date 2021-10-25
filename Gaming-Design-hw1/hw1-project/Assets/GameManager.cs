using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int Height = 18;
    public static int Width = 10;
    [SerializeField] public GameObject[] Tetrominoes;
    [SerializeField] public GameObject _resultBlock;
    [SerializeField] public Text _elapsedTimeFromStartText;
    [SerializeField] public Text _currentScoreText;
    [SerializeField] public Text _totalScoreText;
    [SerializeField] public Button _playButton;
    [SerializeField] public AudioSource _removeSource;
    [SerializeField] public AudioSource _endSource;
    private Transform[,] grid = new Transform[Width, Height];
    private float _totalElapseTime = 60.00f;
    private int _score = 0;
    private bool gameover = false;

    public float ElapsedTimeFromStart
    {
        get => _totalElapseTime;

        set
        {
            _totalElapseTime = value;
            _elapsedTimeFromStartText.text = string.Format("{0} s" , (_totalElapseTime).ToString("F2"));
        }
    }

    public int TotalScore
    {
        get => _score;

        set
        {
            _score = value;
            _currentScoreText.text = string.Format("{0:0000}", _score);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        CreateNewTetromino();
    }

    // Update is called once per frame
    private void Update()
    {
        ElapsedTimeFromStart -= Time.deltaTime;

        if(IsGameOver())
        {
            GameOver();
        }
    }

    public void CreateNewTetromino()
    {
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
    }

    public void AddToGrid(Transform transform)
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            if (grid[roundX, roundY] == null) grid[roundX, roundY] = children;
        }
    }

    public bool IsGridFilled(int x, int y)
    {
        if (grid[x, y] != null) return true;
        return false;
    }

    public void CheckForLines()
    {
        for(int i = Height - 1;i >= 0; i--)
        {
            if (IsLineFilled(i))
            {
                RemoveLine(i);
                Rowdown(i);
            }
        }
    }

    private bool IsLineFilled(int line)
    {
        for(int i = 0;i < Width; i++)
        {
            if (grid[i, line] == null) return false;
        }

        return true;
    }

    private void RemoveLine(int line) {
        for(int i = 0;i < Width; i++)
        {
            Destroy(grid[i, line].gameObject);
            grid[i, line] = null;
        }
        TotalScore++;
        _removeSource.Play();
    }

    private void Rowdown(int line) {
        for(int i = line; i < Height; i++)
        {
            for(int j = 0;j < Width; j++)
            {
                if (grid[j, i] != null)
                {
                    grid[j, i - 1] = grid[j, i];
                    grid[j, i] = null;
                    grid[j, i - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    public bool IsGameOver()
    {
        if (ElapsedTimeFromStart <= 0)
        {
            ElapsedTimeFromStart = 0;
            return true;
        }
        return gameover || false;
    }

    public bool IsGameOver(Transform transform)
    {
        foreach (Transform children in transform)
        {
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            if (roundY >= 15) return true;
        }
        return false;
    }

    public void hitTop()
    {
        gameover = true;
    }

    public void GameOver()
    {
        _endSource.Play();
        this.enabled = false;
        for (int i = 0;i < Height; i++)
        {
            for(int j = 0;j < Width; j++)
            {
                if (grid[j, i] != null)
                {
                    Destroy(grid[j, i].gameObject);
                    grid[j, i] = null;
                }
            }
        }
        _totalScoreText.text = string.Format("{0:0000}", _score);
        _resultBlock.gameObject.SetActive(true);
    }
}
