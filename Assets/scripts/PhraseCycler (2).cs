using UnityEngine;
using TMPro;

// Повесить на объект с компонентом TextMeshProUGUI (UI-текст) ИЛИ TextMeshPro (3D-текст в сцене) —
// TMP_Text это общий базовый класс для обоих, скрипт работает с любым из них.
public class PhraseCycler : MonoBehaviour
{
    [Header("Ссылка на текстовое поле")]
    [SerializeField] private TMP_Text textField;

    [Header("Фразы (заполняются в инспекторе)")]
    [SerializeField] private string[] phrases;

    [Header("Настройки")]
    [SerializeField] private float secondsPerPhrase = 0.5f;
    [SerializeField] private bool loop = true;
    [SerializeField] private bool playOnStart = true;

    private Coroutine cycleRoutine;

    private void Awake()
    {
        if (textField == null)
            textField = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (playOnStart)
            StartCycling();
    }

    public void StartCycling()
    {
        if (phrases == null || phrases.Length == 0)
        {
            Debug.LogWarning("PhraseCycler: массив phrases пуст.");
            return;
        }

        if (cycleRoutine != null)
            StopCoroutine(cycleRoutine);

        cycleRoutine = StartCoroutine(CycleRoutine());
    }

    public void StopCycling()
    {
        if (cycleRoutine != null)
        {
            StopCoroutine(cycleRoutine);
            cycleRoutine = null;
        }
    }

    private System.Collections.IEnumerator CycleRoutine()
    {
        int index = 0;
        var wait = new WaitForSeconds(secondsPerPhrase);

        do
        {
            textField.text = phrases[index];
            index = (index + 1) % phrases.Length;
            yield return wait;
        }
        while (loop || index != 0);
    }
}
