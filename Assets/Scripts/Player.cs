using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // скорость перемещения - 7.5 единиц в секунду по умолчанию в редакторе можно поменять
    [SerializeField]
    [Tooltip("Скорость перемещения головы")]
    private float speed = 8.0f;

    [SerializeField]
    [Tooltip("Разрешение на перемещение")]
    private bool canMove = false;

    [Space(30)]

    [SerializeField]
    [Tooltip("Создать хвост?")]
    public Transform lastSnakeObject;
    private bool createTail = false;
    private bool tailCreated = false;


    [SerializeField]
    [Tooltip("Префаб хвоста")]
    private GameObject tailPrefab;

    [Space(30)]

    [SerializeField]
    [Tooltip("Направление движения сменилось?")]
    private int ChangeMoveDir = 0;

    //[SerializeField]
    //[Tooltip("Голова змеи перемещается сейчас?")]
    private bool headIsMoving = false;

    //[SerializeField]
    //[Tooltip("Голова змеи переместилась в след точку движения?")]
    private bool headIsMoved = true;

    [Space(30)]

    //[SerializeField]
    //[Tooltip("Следующая точка куда двигаться")]
    private Vector3 targetMovePosition;


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

        if (createTail)
        {
            CreateTail();
            createTail = false;
        }

        if (canMove)
        {
            speed = 8.0f;
        }
        

        //Узнаем следующую позицию куда двигать, если стоим на точке и не двигаемся
        if (headIsMoved && !headIsMoving)
        {
            if (ChangeMoveDir == 1)
            {
                ChangeMoveDir = 0;
                ChangeHeadRotation(90);
            }
            if (ChangeMoveDir == -1)
            {
                ChangeMoveDir = 0;
                ChangeHeadRotation(-90);
            }

            targetMovePosition = GetNextMovePosition(transform.position);

            headIsMoving = true;
            headIsMoved = false;
        }
        else
        {
            // Безграничный экран с переносом головы на противоположную сторону
            //if (targetMovePosition.z > 47.5f)
            //{
            //    Vector3 newPos = new Vector3(targetMovePosition.x, targetMovePosition.y, -47.5f);
            //    transform.position = newPos;
            //    targetMovePosition = GetNextMovePosition(transform.position);
            //}
            //if (targetMovePosition.z < -47.5f)
            //{
            //    Vector3 newPos = new Vector3(targetMovePosition.x, targetMovePosition.y, 47.5f);
            //    transform.position = newPos;
            //    targetMovePosition = GetNextMovePosition(transform.position);
            //}
            //if (transform.position.x > 47.5f)
            //{
            //    Vector3 newPos = new Vector3(-47.5f, targetMovePosition.y, targetMovePosition.z);
            //    transform.position = newPos;
            //    targetMovePosition = GetNextMovePosition(transform.position);
            //}
            //if (targetMovePosition.x < -53.5f)
            //{
            //    Vector3 newPos = new Vector3(47.5f, targetMovePosition.y, targetMovePosition.z);
            //    transform.position = newPos;
            //    targetMovePosition = GetNextMovePosition(transform.position);
            //}

            // Перемещаем голову змеи
            transform.position = Vector3.MoveTowards(transform.position, targetMovePosition, Time.deltaTime * speed);

            // Если голова змеи попала на конечную точку движения
            if (Vector3.Distance(transform.position, targetMovePosition) < 0.01f)
            {
                // Свою позицию прировнять к конечной позиции
                transform.position = targetMovePosition;

                headIsMoved = true;
                headIsMoving = false;
                if (!canMove)
                {
                    speed = 0.0f;
                }
            }
        }
    }

    // Функция управления с клавиатуры
    public void InputControlls()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            //Debug.Log("LEFT");
            ChangeMoveDir = -1;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            //Debug.Log("RIGHT");
            ChangeMoveDir = 1;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            //Debug.Log("UP");
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("Current pos: " + transform.position);
            Debug.Log("Next move pos: " + targetMovePosition);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            canMove = !canMove;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            transform.position = new Vector3(2.5f, 1.0f, -27.5f);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0.0f, transform.eulerAngles.z);
            GetNextMovePosition(transform.position);
            headIsMoved = true;
            headIsMoving = false;
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            createTail = true;
        }
    }

    // Создает хвост
    public void CreateTail()
    {
        if (!tailCreated)
        {
            // current - текущая цель элемента хвоста, начинаем с головы
            lastSnakeObject = transform;

            // помещаем "хвост" за "хозяином"
            Vector3 nextTailPos = new Vector3(lastSnakeObject.transform.position.x, lastSnakeObject.transform.position.y, lastSnakeObject.transform.position.z - 5.0f);

            // создаем куб хвоста
            GameObject tailObj = Instantiate(tailPrefab, nextTailPos, Quaternion.identity);
            Tail tail = tailObj.GetComponent<Tail>();

            // ориентация хвоста как ориентация хозяина
            tailObj.transform.rotation = transform.rotation;

            // элемент хвоста должен следовать за хозяином, поэтому передаем ему ссылку на его
            tail.target = lastSnakeObject.transform;

            // дистанция между элементами хвоста - 2 единицы
            tail.targetDistance = 5f;

            // следующим хозяином будет новосозданный элемент хвоста
            lastSnakeObject = tailObj.transform;

            tailCreated = true;
        }
        else
        {
            // помещаем "хвост" за "хозяином"
            Vector3 nextTailPos = new Vector3(lastSnakeObject.transform.position.x, lastSnakeObject.transform.position.y, lastSnakeObject.transform.position.z - 5.0f);

            // создаем куб хвоста
            GameObject tailObj = Instantiate(tailPrefab, nextTailPos, Quaternion.identity);
            Tail tail = tailObj.GetComponent<Tail>();

            // ориентация хвоста как ориентация хозяина
            tailObj.transform.rotation = transform.rotation;

            // элемент хвоста должен следовать за хозяином, поэтому передаем ему ссылку на его
            tail.target = lastSnakeObject.transform;

            // дистанция между элементами хвоста - 2 единицы
            tail.targetDistance = 5f;

            // следующим хозяином будет новосозданный элемент хвоста
            lastSnakeObject = tailObj.transform;
        }
        
        //удаляем с хвоста коллайдер, так как он не нужен
        //Destroy(tail.collider);
        //Destroy(tail.GetComponent<Collider>());

        

        //for (int i = 0; i < 3; i++)
        //{
        //    // помещаем "хвост" за "хозяином"
        //    Vector3 nextTailPos = new Vector3(current.transform.position.x, current.transform.position.y, current.transform.position.z - 5.0f);

        //    // создаем куб хвоста
        //    GameObject tailObj = Instantiate(tailPrefab, nextTailPos, Quaternion.identity);
        //    Tail tail = tailObj.GetComponent<Tail>();

        //    // ориентация хвоста как ориентация хозяина
        //    tailObj.transform.rotation = transform.rotation;

        //    // элемент хвоста должен следовать за хозяином, поэтому передаем ему ссылку на его
        //    tail.target = current.transform;

        //    // дистанция между элементами хвоста - 2 единицы
        //    tail.targetDistance = 5f;

        //    //удаляем с хвоста коллайдер, так как он не нужен
        //    //Destroy(tail.collider);
        //    //Destroy(tail.GetComponent<Collider>());

        //    // следующим хозяином будет новосозданный элемент хвоста
        //    current = tailObj.transform;
        //}
    }

    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Cell")
        {
            //Debug.Log(target.transform.name);
        }
        if (target.tag == "Food")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game>().points += 1;
            createTail = true;
            Destroy(target.gameObject);
        }
    }

    // Функция определения направления движения с которой узнаем следующую точку куда двигаться
    public Vector3 GetNextMovePosition(Vector3 currentPos)
    {
        return currentPos + (transform.forward * 5.0f);
    }

    // Сменить угол поворота головы змеи
    public void ChangeHeadRotation(float addRotAngle)
    {
        float currentEulerAngleY = transform.eulerAngles.y;
        float newEulerAngleY = currentEulerAngleY + addRotAngle;

        // Меняем угол поворота направления движения головы
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newEulerAngleY, transform.eulerAngles.z);
    }

    // Pass

}