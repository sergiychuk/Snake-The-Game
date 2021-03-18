using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    //[SerializeField]
    //[Tooltip("Обьект интерфейса где отображается режим качества картинки")]
    //private GameObject qualityText;

    //[Space(30)]

    [SerializeField]
    [Tooltip("Набранные очки")]
    public int points;

    [SerializeField]
    [Tooltip("Обьект интерфейса где отображается количество очок")]
    private GameObject scoreText;

    [Space(30)]

    [SerializeField]
    [Tooltip("Показать FPS?")]
    public bool showFPS = true;

    [SerializeField]
    [Tooltip("Показать FPS")]
    private GameObject fps_text;

    [Space(30)]

    [SerializeField]
    [Tooltip("Префаб головы змеи")]
    private GameObject snakeHeadPrefab;

    [SerializeField]
    [Tooltip("Стартовая позиция головы змеи после загрузки уровня")]
    private Vector3 snakeHeadStartPos = new Vector3(2.5f, 1.0f, -27.5f);

    [Space(30)]

    // материал стен
    public Material wallMaterial;
    
    // количество стен в уровне
    public int countWals = 10;

    float maxFPS = 0;

    // генерируем уровень при загрузке сцены
    public void Awake()
    {
        SetQualityMode(0);

        // обнуляем очки
        points = 0;

        // генерируем уровень
        //GenerateLevel();
    }

    public void Start()
    {
        // Спавним в начале голову без хвоста в начальной позиции
        CreateSnakeHead();
    }

    public void Update()
    {
        ShowScore();
        ShowFPS();
        InputControlls();
    }

    // отрисовка FPS
    public void ShowFPS()
    {
        float currentFPS = (int)(1f / Time.unscaledDeltaTime);
        if(currentFPS > maxFPS)
        {
            maxFPS = currentFPS;
        }
        fps_text.GetComponent<Text>().text = "FPS: " + currentFPS.ToString() + "\nMax: " + maxFPS.ToString();
        fps_text.SetActive(showFPS);
    }

    // отрисовка набранных очков
    public void ShowScore()
    {
        scoreText.GetComponent<Text>().text = "Score: " + points.ToString();
    }

    // функция генерации уровня
    private void GenerateLevel()
    {
        for (int i = 0; i < countWals; i++)
        {
            // создаем куб
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // называем его "Wall"
            wall.name = "Wall";
            // увеличиваем его габариты
            wall.transform.localScale = new Vector3(2, 2, 2);
            // расставляем его так, чтобы координаты были не в центре поля
            var pos = new Vector3(Random.Range(-40, 41), 0, Random.Range(-40, 41));
            while (Mathf.Abs(pos.x) < 10 || Mathf.Abs(pos.z) < 10)
            {
                pos = new Vector3(Random.Range(-40, 41), 0, Random.Range(-40, 41));
            }
            wall.transform.position = pos;
            // и назначаем материал
            //wall.renderer.material = wallMaterial;
            wall.GetComponent<Renderer>().material = wallMaterial;
        }
    }

    // функция создания головы змеи сразу после запуска игры
    private void CreateSnakeHead()
    {
        GameObject snakeHead = Instantiate(snakeHeadPrefab, snakeHeadStartPos, Quaternion.identity);
        snakeHead.name = "Head_Cube";
    }
    public void SetQualityMode(int mode)
    {
        QualitySettings.SetQualityLevel(mode);
    }

    public void InputControlls()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            showFPS = !showFPS;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            RestartLevel();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            //Debug.Log(SceneManager.sceneCount);
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //Debug.Log("Screen Width : " + Screen.);
        }
        if (Input.GetKeyUp(KeyCode.F3))
        {
            //SetQualityMode(1);
        }
        
        if (Input.GetKeyUp(KeyCode.F5))
        {
            //SetQualityMode(4);
        }
        if (Input.GetKeyUp(KeyCode.F6))
        {
            //SetQualityMode(5);
        }
    }

    //Restarts the level
    void RestartLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}