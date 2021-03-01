using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private IEnumerator StepAlwaysMove;
    bool xNotKeyPressed = true;
    bool yNotKeyPressed = false;
    public int numeralDirection = 1;
    public GameObject food;
    public GameObject snakeTimeObject;
    public Vector2 directionSnake;
    int score;
    int scorePositionUp = 50;
    float time;
    float beforeScore;
    public float Time
    {
        get
        {
            return time;
        }
        set
        {
            const float MAXTIMELINE = 0.050F;
            if (score != beforeScore && time > MAXTIMELINE)
            {
                time -= value / 10;
            }
            else
            {
                time = value;
            }

        }
    }
    enum direction
    {
        Up = 1,
        Down = 2,
        Right = 3,
        Left = 4,
    }
    void Start()
    {
        time = 1;
        StepAlwaysMove = AlwaysMove();
        StartCoroutine(StepAlwaysMove);
    }
    void Update()
    {
        score = food.gameObject.GetComponent<SpawnPoint>().score;
        beforeScore = snakeTimeObject.GetComponent<Tail>().beforeScore;
        if (score % scorePositionUp == 0 && score !=0)
        {
            //calculate percentages and sum their with speed
            Time = time;
            scorePositionUp += 30;
        }

        if (Input.GetKeyUp(KeyCode.W) && yNotKeyPressed)
        {
            xNotKeyPressed = true;
            yNotKeyPressed = false;
            numeralDirection = (int)direction.Up;
        }
        if (Input.GetKeyUp(KeyCode.S) && yNotKeyPressed)
        {
            xNotKeyPressed = true;
            yNotKeyPressed = false;
            numeralDirection = (int)direction.Down;
        }
        if (Input.GetKeyUp(KeyCode.D) && xNotKeyPressed)
        {
            xNotKeyPressed = false;
            yNotKeyPressed = true;
            numeralDirection = (int)direction.Right;
        }
        if (Input.GetKeyUp(KeyCode.A) && xNotKeyPressed)
        {
            xNotKeyPressed = false;
            yNotKeyPressed = true;
            numeralDirection = (int)direction.Left;
        }
    }
    private IEnumerator AlwaysMove()
    {
        while (true)
        {
            MoveSnake();
            yield return new WaitForSeconds(time);
        }
    }
    void MoveSnake()
    {
        switch (numeralDirection)
        {
            case (int)direction.Up:
                directionSnake = Vector2.up;
                break;
            case (int)direction.Down:
                directionSnake = Vector2.down;
                break;
            case (int)direction.Right:
                directionSnake = Vector2.right;
                break;
            case (int)direction.Left:
                directionSnake = Vector2.left;
                break;
        }
    }
}