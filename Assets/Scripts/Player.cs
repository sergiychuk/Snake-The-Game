using UnityEngine.SceneManagement;
using UnityEngine;
// скрипту игрока необходим на объекте компонент CharacterController
// с помощью этого компонента будет выполняться движение
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // скорость перемещения - 6 единиц в секунду по умолчанию
    // в редакторе можно поменять
    public float speed = 6;
    // аналогично скорость вращения 60 градусов в секунду по умолчанию
    public float rotationSpeed = 60;
    // локальная переменная для хранения ссылки на компонент CharacterController
    private CharacterController _controller;
    public void Start()
    {
        // получаем компонент CharacterController и 
        // записываем его в локальную переменную
        _controller = GetComponent<CharacterController>();
        // создаем хвост
        // current - текущая цель элемента хвоста, начинаем с головы
        Transform current = transform;
        for (int i = 0; i < 3; i++)
        {
            // создаем примитив куб и добавляем ему компонент Tail
            Tail tail = GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<Tail>();
            // помещаем "хвост" за "хозяином"
            tail.transform.position = current.transform.position - current.transform.forward * 2;
            // ориентация хвоста как ориентация хозяина
            tail.transform.rotation = transform.rotation;
            // элемент хвоста должен следовать за хозяином, поэтому передаем ему ссылку на его
            tail.target = current.transform;
            // дистанция между элементами хвоста - 2 единицы
            tail.targetDistance = 2;
            // удаляем с хвоста коллайдер, так как он не нужен
            //Destroy(tail.collider);
            Destroy(tail.GetComponent<Collider>());
            // следующим хозяином будет новосозданный элемент хвоста
            current = tail.transform;
        }
    }
    //private bool _testing = false;
    public void Update()
    {
        /* 
        * Гибкий способ - использовать оси
        * Unity имеет набор предустановленных осей, которые можно использовать
        * следующий код будет работать как на клавиатуре (стрелки и WSAD), так и на геймпаде
        */
        // получаем значение вертикальной оси ввода
        /* float vertical = Input.GetAxis("Vertical"); */
        // получаем значение горизонтальной оси ввода
        float horizontal = Input.GetAxis("Horizontal");

        // вращаем трансформ вокруг оси Y 
        transform.Rotate(0, rotationSpeed * Time.deltaTime * horizontal, 0);
        // движение выполняем с помощью контроллера в сторону, куда смотрит трансформ игрока
        // двигаем змею постоянно
        _controller.Move(transform.forward * speed * Time.deltaTime /* * vertical*/);
    }
    GameObject food;
    // В данную функцию будут передаваться все объекты, с которыми
    // CharacterController вступает в столкновения
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.name == "Food")
        {
            // прибавляем очки еды к общему числу очков
            //Game.points += 10;
            //Врезались в еду, "съедаем" ее и создаем новую в пределах поля. 
            //На самом деле перемещаем еду в Random положение
            food = GameObject.Find("Food");
            //Destroy(food);
            var pos = new Vector3(Random.Range(-40, 41), 0, Random.Range(-40, 41));
            food.transform.position = pos;
        }
        //else
        //{
        //    //врезались не в еду
        //    SceneManager.LoadScene("GameOver");
        //}
    }
}