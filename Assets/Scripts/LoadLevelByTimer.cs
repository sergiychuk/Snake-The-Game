using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelByTimer : MonoBehaviour
{
    // время до загрузки уровня
    public float delay = 3;
    // имя загружаемого уровня
    public string levelName;
    // типа IEnumerator из простр. имен System.Collections.
    // для поддержки функцией Start механизма сопрограмм
    public IEnumerator Start()
    {
        // задержка на заданное число секунд
        yield return new WaitForSeconds(delay);
        // загрузка уровня с указанным именем
        SceneManager.LoadScene(levelName);
    }
}