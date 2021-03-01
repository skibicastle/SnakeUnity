using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public GameObject tailPrefab;
    public GameObject food;
    public Vector2 direction;
    private IEnumerator AlwaysMoveSnake;
    public List<GameObject> tailColor = new List<GameObject>();
    List<Transform> tail = new List<Transform>();
    bool ateFood = false;
    bool paintToWhite = false;
    public int snakeLenght;
    float time;
    public float score;
    public float beforeScore = 0;
    public List<float> xPositionSnake;
    public List<float> yPositionSnake;

    void Start()
    {

        AlwaysMoveSnake = AlwaysMove();
        StartCoroutine(AlwaysMoveSnake);

        time = tailPrefab.GetComponent<KeyController>().Time;
        const int startLenght = 2;
        for (int i = 0; i < startLenght; i++)
        {
            ateFood = true;
            Move();
        }
        getPositionTail();
    }
    void Update()
    {
        time = tailPrefab.GetComponent<KeyController>().Time;
        snakeLenght = tail.Count;
        score = food.GetComponent<SpawnPoint>().score;
        if (score != beforeScore)
        {
            beforeScore = score;
            ateFood = true;
        }
        bool colorMode = food.GetComponent<SpawnPoint>().colorMode;
        if (colorMode)
        {
            for (int i = 0; i < tailColor.Count - 1; i++)
            {
                tailColor[i].GetComponent<SpriteRenderer>().color = new Color(Random.Range(-255, 255), Random.Range(-255, 255), Random.Range(-255, 255));
            }
            paintToWhite = true;
        }
        else if (colorMode != true && paintToWhite)
        {
            for (int i = 0; i < tailColor.Count - 1; i++)
            {
                tailColor[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            }
            paintToWhite = false;
        }
    }
    void FixedUpdate()
    {
        getPositionTail();
    }
    void Move()
    {
        // Save current position 
        Vector2 beforePosition = tailPrefab.transform.position;

        // Move head into new direction 
        direction = tailPrefab.GetComponent<KeyController>().directionSnake;
        tailPrefab.transform.Translate(direction);

        // If put food - set new body element
        if (ateFood)
        {
            // Load Prefab into the world
            GameObject tailGameObject = Instantiate(tailPrefab, beforePosition, Quaternion.identity);
            tailGameObject.tag = "border";
            // Keep track of it in our tail list
            tail.Insert(0, tailGameObject.transform);
            tailColor.Add(tailGameObject);

            // Reset the flag
            ateFood = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = beforePosition;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }
    private IEnumerator AlwaysMove()
    {
        while (true)
        {
            Move();
            yield return new WaitForSeconds(time);
        }
    }
    private void getPositionTail()
    {
        for (int i = 0; i < tail.Count - 1; i++)
        {
            Debug.Log(tail[i]);
            xPositionSnake.Add(tail[i].position.x);
            yPositionSnake.Add(tail[i].position.y);
            Debug.Log(xPositionSnake[i]);
            Debug.Log(yPositionSnake[i]);
        }
    }
}