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
        InstantiateBody();
        InstantiateBody();
        StepAlwaysMove = AlwaysMove(1f);
        StartCoroutine(StepAlwaysMove);
    }

    // Update is called once per frame
    void Update()
    {
        bodyLenght = food.GetComponent<SpawnPoint>().score;
        bodyLenght /= 10;
        if (bodyLenght != startLenght)
        {
            InstantiateBody();
            startLenght = bodyLenght;
        }    
    }
    private IEnumerator AlwaysMove(float WaitTime)
    {
        while (true)
        {
            float distance = ((Vector2)head.position - positions[0]).magnitude;
            if (distance > 0.5f)
            {
                positions.Insert(0, head.position);
                positions.RemoveAt(positions.Count - 1);
            }

            for (int i = 0; i < bodySnake.Count; i++)
            {
                bodySnake[i].position = Vector2.Lerp(positions[i + 1], positions[i], distance/1);
            }
            yield return new WaitForSeconds(0);
        }
    }
    void InstantiateBody()
    {
        Transform body = Instantiate(head,positions[positions.Count-1],Quaternion.identity, transform);
        bodySnake.Add(body);
        positions.Add(body.position);
    }  
}

