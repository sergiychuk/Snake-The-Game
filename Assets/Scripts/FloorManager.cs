using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Родительський обьект земли")]
    private GameObject floorParent;

    [SerializeField]
    [Tooltip("Префаб клетки")]
    private GameObject cellPrefab;

    //[SerializeField]
    //[Tooltip("Количество клеток на сторону")]
    private int cellAmount = 20; // 20 шт. по X и 20 шт. по Z

    //[SerializeField]
    //[Tooltip("Стартовая позиция создания клеток")]
    private Vector3 startSpawnPos = new Vector3(-47.5f, 0, -47.5f); // -47.5; 47.5

    //[SerializeField]
    //[Tooltip("Спавнить ли клетки в начале игры")]
    private bool spawnCells = true;

    private Vector3 nextSpawnPos;

    private int cellCounter = 20;


    void Start()
    {
        nextSpawnPos = startSpawnPos;
        if (spawnCells)
        {
            for (int i = 0; i < cellAmount; i++)
            {
                for (int j = 0; j < cellAmount; j++)
                {
                    SpawnCell(nextSpawnPos);
                    nextSpawnPos = new Vector3(nextSpawnPos.x, nextSpawnPos.y, nextSpawnPos.z + 5);
                }
                nextSpawnPos = new Vector3(nextSpawnPos.x + 5, nextSpawnPos.y, -47.5f);
            }
        }
    }

    void SpawnCell(Vector3 pos)
    {
        cellCounter++;
        GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);
        cell.transform.parent = floorParent.transform;
        cell.name = ("Cell" + cellCounter.ToString());
    }

}
