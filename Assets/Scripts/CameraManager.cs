using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Обьект головы змеи")]
    private GameObject snakeHeadObj;

    [SerializeField]
    [Tooltip("Позиция головы змеи")]
    private Vector3 snakeHeadPos;

    //[SerializeField]
    //[Tooltip("Позиция камеры")]
    //private Vector3 camPos;

    [SerializeField]
    [Tooltip("Отступ от головы змеи до камеры")]
    private Vector3 camOffset;

    //[SerializeField]
    //[Tooltip("Угол поворота головы змеи")]
    //private float snakeHeadRot;

    void LateUpdate()
    {
        //Получить текущую позицию головы змеи
        snakeHeadObj = GameObject.FindGameObjectWithTag("Head");
        snakeHeadPos = snakeHeadObj.transform.position;

        //Получить текущуее значение поворота головы змеи
        //snakeHeadRot = snakeHeadObj.transform.eulerAngles.y;

        //Установить значение поворота камеры от поворота головы змеи
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, snakeHeadObj.transform.eulerAngles.y, transform.eulerAngles.z);

        //Установить отступ камеры от головы змеи исходя с того в какую из четырех сторон повернута голова
        SetCameraOffset(snakeHeadObj.transform.eulerAngles.y);

        //Получить текущую позицию камеры
        //camPos = transform.position;

        //Установить позицию камеры с некоторым отступлением по координатам от позиции головы змеи
        transform.position = snakeHeadPos + camOffset;
    }

    void SetCameraOffset(float snakeRot)
    {
        switch (snakeRot)
        {
            case 270:
                camOffset = new Vector3(10.5f, 5.5f, 0);
                break;
            case 90:
                camOffset = new Vector3(-10.5f, 5.5f, 0);
                break;
            case 0:
                camOffset = new Vector3(0, 5.5f, -10.5f);
                break;
            case 180:
                camOffset = new Vector3(0, 5.5f, 10.5f);
                break;
        }
    }

}
