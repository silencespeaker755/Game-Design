using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetries : MonoBehaviour
{
    public static int Height = GameManager.Height;
    public static int Width = GameManager.Width;
    private float MaxFallTime = 0.7f;
    private float previousTime = 0f;
    public Vector3 rotatePosition;
    [SerializeField] private AudioSource _hitSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GameManager>().IsGameOver())
        {
            foreach(Transform children in transform)
            {
                Destroy(children.gameObject);
            }
            this.enabled = false;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!ValidateMove())
                transform.position -= new Vector3(-1, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidateMove())
                transform.position -= new Vector3(1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotatePosition), new Vector3(0, 0, 1), 90);
            if(!ValidateMove())
                transform.RotateAround(transform.TransformPoint(rotatePosition), new Vector3(0, 0, 1), -90);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            while (ValidateMove()) transform.position += new Vector3(0, -1, 0);
            HitEffect();
            transform.position -= new Vector3(0, -1, 0);
            FindObjectOfType<GameManager>().AddToGrid(transform);
            if (FindObjectOfType<GameManager>().IsGameOver(transform))
            {
                FindObjectOfType<GameManager>().hitTop();
            }
            else
            {
                FindObjectOfType<GameManager>().CheckForLines();
                FindObjectOfType<GameManager>().CreateNewTetromino();
            }
            this.enabled = false;
            previousTime = Time.time;
        }

        if(Time.time - previousTime >= (Input.GetKey(KeyCode.DownArrow) ? MaxFallTime/10 : MaxFallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidateMove()) {
                HitEffect();
                transform.position -= new Vector3(0, -1, 0);
                FindObjectOfType<GameManager>().AddToGrid(transform);
                if (FindObjectOfType<GameManager>().IsGameOver(transform))
                {
                    FindObjectOfType<GameManager>().hitTop();
                }
                else
                {
                    FindObjectOfType<GameManager>().CheckForLines();
                    FindObjectOfType<GameManager>().CreateNewTetromino();
                }
                this.enabled = false;
            }
            previousTime = Time.time;
        }
    }

    bool ValidateMove()
    {
        foreach(Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            if (roundX < 0 || roundX >= Width || roundY < 0 || roundY >= Height)
            {
                return false;
            }

            if (FindObjectOfType<GameManager>().IsGridFilled(roundX, roundY))
            {
                return false;
            }
        }

        return true;
    }

    void HitEffect()
    {
        _hitSource.Play();
    }
}
