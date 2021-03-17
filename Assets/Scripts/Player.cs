using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // скорость перемещения - 7.5 единиц в секунду по умолчанию в редакторе можно поменять
    [SerializeField]
    [Tooltip("Скорость перемещения головы")]
    private float speed = 7.5f;

    [Space(30)]

    [SerializeField]
    [Tooltip("Создать хвост при первой загрузке?")]
    private bool createTail = true;

    [SerializeField]
    [Tooltip("Префаб хвоста")]
    private GameObject tailPrefab;

    [Space(30)]


    [SerializeField]
    [Tooltip("Префаб хвоста")]
    private Vector3 nextMovePosition;


    /*------------
    1 - rotY: 0
    2 - rotY: 90
    3 - rotY: 180;
    4 - rotY: 270;
    ------------*/
    //[SerializeField]
    //[Tooltip("Направление движения(1-4)")]
    //private int moveSideDir = 1;

    public void Start()
    {
        if (createTail)
        {
            CreateTail();
        }
    }

    public void Update()
    {
        InputControlls();

        //Узнаем следующую позицию куда двигать
        nextMovePosition = GetNextMovePosition(transform.position);

        // Передвижение головы
        //transform.Translate(Vector3.forward * Time.deltaTime * speed);
        

    }

    // Функция управления с клавиатуры
    public void InputControlls()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Debug.Log("LEFT");
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Debug.Log("RIGHT");
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            Debug.Log("UP");
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            Debug.Log("Current pos: " + transform.position);
            Debug.Log("Next move pos: " + nextMovePosition);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            MoveSnakeHeadToNextPosition(nextMovePosition);
            //if (speed != 0)
            //{
            //    speed = 0;
            //}
            //else
            //{
            //    speed = 7.5f;
            //}
        }
        if (Input.GetKey(KeyCode.R))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Создает хвост
    public void CreateTail()
    {
        // current - текущая цель элемента хвоста, начинаем с головы
        Transform current = transform;
        for (int i = 0; i < 3; i++)
        {
            // помещаем "хвост" за "хозяином"
            Vector3 nextTailPos = new Vector3(current.transform.position.x, current.transform.position.y, current.transform.position.z - 5.0f);

            // создаем куб хвоста
            GameObject tailObj = Instantiate(tailPrefab, nextTailPos, Quaternion.identity);
            Tail tail = tailObj.GetComponent<Tail>();
            
            // ориентация хвоста как ориентация хозяина
            tailObj.transform.rotation = transform.rotation;

            // элемент хвоста должен следовать за хозяином, поэтому передаем ему ссылку на его
            tail.target = current.transform;

            // дистанция между элементами хвоста - 2 единицы
            tail.targetDistance = 5f;
            
            //удаляем с хвоста коллайдер, так как он не нужен
            //Destroy(tail.collider);
            //Destroy(tail.GetComponent<Collider>());
            
            // следующим хозяином будет новосозданный элемент хвоста
            current = tailObj.transform;
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Cell")
        {
            //Debug.Log(target.transform.name);
        }
    }

    // Функция определения направления движения с которой узнаем следующую точку куда двигаться
    public Vector3 GetNextMovePosition(Vector3 currentPos)
    {
        return currentPos + (transform.forward * 5.0f);
    }

    //Функция движения головы
    public void MoveSnakeHeadToNextPosition(Vector3 nextPos)
    {
        while(transform.position != nextPos)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }



}