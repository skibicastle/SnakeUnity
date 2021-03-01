using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject loadBar;
    public GameObject snake;
    public Transform pointGrid;
    public AudioClip soundCrunch;
    public AudioClip soundAtePig;
    public Sprite[] spriteFood = new Sprite[16];
    public bool colorMode = false;
    private IEnumerator SpawnBigPoint;
    private IEnumerator GetXYSnake;
    int snakeLenght;
    int scoreBigPoint = 100;
    bool taimerEnd = false;
    bool didHaveTime = false;
    List<float> xPositionSnake;
    List<float> yPositionSnake;
    public bool startAnimation { get; set; } = false;
    public bool startAnimationBigPoint { get; set; } = false;
    public int score { get; set; } = 0;

    int antiBug = 0;

    private void Awake()
    {
        loadBar.SetActive(false);
        gameObject.transform.Translate(Random.Range(-10,10), Random.Range(-8,8),0);
        GetXYSnake = GetCordinateSnake();
        StartCoroutine(GetXYSnake);
    }
    void Update()
    {
        if (taimerEnd == true)
        {
            taimerEnd = false;
            loadBar.SetActive(false);
            SpawnFood();
        }
        if (score == scoreBigPoint && didHaveTime != true)
        {
            startAnimationBigPoint = true;
            colorMode = true;
            // Spawn point
            const int TIME = 5;
            scoreBigPoint += 180;
            startAnimation = true;
            SpawnBigPoint = Taimer(TIME);
            StartCoroutine(SpawnBigPoint);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "snake")
        {
            
            if (didHaveTime)
            {
                // Ate big point
                gameObject.GetComponent<AudioSource>().PlayOneShot(soundAtePig);
                colorMode = false;
                startAnimationBigPoint = false;
                didHaveTime = false;
                StopCoroutine(SpawnBigPoint);
                const int scoreUpdate = 100;
                score += scoreUpdate;
                loadBar.SetActive(false);
                startAnimation = false;
                SpawnFood();
            }
            else
            {
                // Spawn point
                gameObject.GetComponent<AudioSource>().PlayOneShot(soundCrunch);
                const int scoreUpdate = 10;
                score += scoreUpdate;
                SpawnFood();
            }
            
        }

    }
    private void SpawnFood()
    {
        int xFoodDiapasonGrid = Random.Range(-16, 16);
        int yFoodDiapasonGrid = Random.Range(-6, 6);
        bool spawnfood = CheckPositionSnake(xFoodDiapasonGrid, yFoodDiapasonGrid);

        if (spawnfood)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteFood[Random.Range(0, 16)];
            gameObject.transform.localScale = new Vector3(1f, 1f, 0);
            gameObject.transform.position = new Vector3(xFoodDiapasonGrid, yFoodDiapasonGrid, 0);
        }
        else
        {
            SpawnFood();
        }
    }
    private void SpawnBigFood()
    {
        int xFoodDiapasonGrid = Random.Range(-16, 16);
        int yFoodDiapasonGrid = Random.Range(-5, 5);
        bool spawnfood = CheckPositionSnake(xFoodDiapasonGrid, yFoodDiapasonGrid);

        if (spawnfood)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteFood[3];
            gameObject.transform.localScale = new Vector3(3, 3, 0);
            gameObject.transform.position = new Vector3(xFoodDiapasonGrid, yFoodDiapasonGrid, 0);
        }
        else
        {
            SpawnBigFood();
        }
    }
    private IEnumerator Taimer(int time)
    {
        didHaveTime = true;
        loadBar.SetActive(true);
        SpawnBigFood();
        yield return new WaitForSeconds(time);
        colorMode = false;
        startAnimationBigPoint = false;
        taimerEnd = true;
        didHaveTime = false;
    }
    private IEnumerator GetCordinateSnake()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            getPositionSnake();
        }
    }
    private bool CheckPositionSnake(float xFood,float yFood)
    {
        if (antiBug == 300)
        {
            Debug.Log("END GAME, YOU WIN");
        }
        for (int i = 0; i < snakeLenght - 1; i++)
        {
            if (xFood == xPositionSnake[i] && yFood == yPositionSnake[i])
            {
                Debug.Log("Работает");
                antiBug += 1;
                return false;
            }
        }
        antiBug = 0;
        return true;
    }
    private void getPositionSnake()
    {
        snakeLenght = snake.GetComponent<Tail>().snakeLenght;
        for (int i = 0; i < snakeLenght - 1 ; i++)
        {
                xPositionSnake.Add(snake.GetComponent<Tail>().xPositionSnake[i]);
                yPositionSnake.Add(snake.GetComponent<Tail>().yPositionSnake[i]);
        }
    }
}

