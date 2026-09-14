using UnityEngine;
using UnityEngine.SceneManagement;

// Повесить на любой пустой GameObject в сцене (например "SceneTimer").
// Через заданное время автоматически загрузит указанную сцену.
public class SceneTimer : MonoBehaviour
{
    [Header("Настройки перехода")]
    [SerializeField] private string sceneToLoad = "NextScene"; // точное имя сцены, как в Build Settings
    [SerializeField] private float delaySeconds = 5f;          // через сколько секунд переключать

    [Header("Поведение")]
    [SerializeField] private bool startOnAwake = true;   // запускать таймер сразу при старте сцены
    [SerializeField] private bool useUnscaledTime = false; // true — таймер идёт даже если Time.timeScale = 0 (пауза)

    private float timer;
    private bool isRunning;

    private void Start()
    {
        if (startOnAwake)
            StartTimer();
    }

    private void Update()
    {
        if (!isRunning) return;

        timer += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

        if (timer >= delaySeconds)
        {
            isRunning = false;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // Можно вызывать вручную, например по событию или из другого скрипта
    public void StartTimer()
    {
        timer = 0f;
        isRunning = true;
    }

    public void CancelTimer()
    {
        isRunning = false;
    }
}
