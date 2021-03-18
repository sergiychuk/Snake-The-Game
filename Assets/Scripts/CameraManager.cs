using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //[SerializeField]
    //[Tooltip("Обьект головы змеи")]
    private GameObject snakeHeadObj;


    private void Start()
    {
        snakeHeadObj = GameObject.FindGameObjectWithTag("Head");
        // Ставим камеру как дочерний головы змеи
        transform.SetParent(snakeHeadObj.transform);
    }    

}
