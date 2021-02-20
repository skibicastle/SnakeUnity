using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    public int bodyLenght;
    public List<Transform> bodySnake = new List<Transform>();
    public List<Vector2> positions = new List<Vector2>();
    public Transform head;
    private IEnumerator StepAlwaysMove;
    public GameObject food;
    int startLenght;
    void Start()
    {
        startLenght = food.GetComponent<SpawnPoint>().score;
        positions.Add(head.position);
        //InstantiateBody();
        //InstantiateBody();
    }

    // Update is called once per frame
    void Update()
    {
        const int bodySpawnNum = 10;
        bodyLenght = food.GetComponent<SpawnPoint>().score;
        bodyLenght /= bodySpawnNum;
        if (bodyLenght != startLenght)
        {
            InstantiateBody();
            startLenght = bodyLenght;
        }

        SnakeMove();
    }
    /*private IEnumerator AlwaysMove(float WaitTime)
    {
        while (true)
        {

            yield return new WaitForSeconds(0);
        }
    }*/
    void SnakeMove()
    {
        float distance = ((Vector2)head.position - positions[0]).magnitude;
        float diameterSquare = 1f; //diameter of the inscribed circle
        if (distance > diameterSquare)
        {
            Vector2 directionBeforeHeadToNewPosition = ((Vector2)head.position - positions[0]);

            positions.Insert(0, positions[0] + directionBeforeHeadToNewPosition);
            positions.RemoveAt(positions.Count - 1);

            distance -= diameterSquare;
        }

        for (int i = 0; i < bodySnake.Count; i++)
        {
            bodySnake[i].position = Vector2.Lerp(positions[i + 1], positions[i], distance / diameterSquare);
            Debug.Log($" переменная x ={bodySnake[i].position.x} переменная y ={bodySnake[i].position.y}");
        }
    }
    void InstantiateBody()
    {
        Transform body = Instantiate(head,positions[positions.Count-1],Quaternion.identity,transform);
        bodySnake.Add(body);
        positions.Add(body.position);
    }  
}

