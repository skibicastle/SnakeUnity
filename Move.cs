using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private IEnumerator StepAlwaysMove;
    bool xNotKeyPressed = true;
    bool yNotKeyPressed = false;
    int numeralDirection = 1;
    public float speed;
    public float time = 1f;
    public GameObject food;
    enum direction
    {
        Up = 1,
        Down = 2,
        Right = 3,
        Left = 4,
    }
    // Start is called before the first frame update
    void Start()
    {
        StepAlwaysMove = AlwaysMove(time);
        StartCoroutine(StepAlwaysMove);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W) && yNotKeyPressed)
        {
            xNotKeyPressed = true;
            yNotKeyPressed = false;
            numeralDirection = (int)direction.Up;
        }
        if (Input.GetKeyDown(KeyCode.S) && yNotKeyPressed)
        {
            xNotKeyPressed = true;
            yNotKeyPressed = false;
            numeralDirection = (int)direction.Down;
        }
        if (Input.GetKeyDown(KeyCode.D) && xNotKeyPressed)
        {
            xNotKeyPressed = false;
            yNotKeyPressed = true;
            numeralDirection = (int)direction.Right;
        }
        if (Input.GetKeyDown(KeyCode.A) && xNotKeyPressed)
        {
            xNotKeyPressed = false;
            yNotKeyPressed = true;
            numeralDirection = (int)direction.Left;
        }
    }
    private IEnumerator AlwaysMove(float WaitTime)
    {
        while (true)
        {
            MoveSnake();
            yield return new WaitForSeconds(WaitTime / 3);
        }
    }
    void MoveSnake()
    {
        switch (numeralDirection)
        {
            case (int)direction.Up:
                transform.Translate(0, speed, 0);
                break;
            case (int)direction.Down:
                transform.Translate(0, -speed, 0);
                break;
            case (int)direction.Right:
                transform.Translate(speed, 0, 0);
                break;
            case (int)direction.Left:
                transform.Translate(-speed, 0, 0);
                break;
        }
    }
}
