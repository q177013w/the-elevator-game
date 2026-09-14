using UnityEngine;
using UnityEngine.SceneManagement;

// Повесить на пустой GameObject в первой игровой сцене (не в меню).
// Он "переживает" переходы между сценами и запоминает последнюю открытую.
public class GameProgress : MonoBehaviour
{
    private static GameProgress instance;

    [SerializeField] private string menuSceneName = "MainMenu"; // имя вашей сцены меню

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Не сохраняем как "последнюю", если это само меню
        if (scene.name != menuSceneName)
        {
            PlayerPrefs.SetString("LastScene", scene.name);
            PlayerPrefs.Save();
        }
    }
}
