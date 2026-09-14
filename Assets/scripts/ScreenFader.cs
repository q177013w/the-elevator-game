using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage; // Ссылка на наш черный UI Image
    public float fadeSpeed = 1f; // Скорость исчезновения черного экрана

    void Start()
    {
        // Проверяем, привязана ли картинка, и запускаем корутину
        if (fadeImage != null)
        {
            StartCoroutine(FadeToClear());
        }
    }

    IEnumerator FadeToClear()
    {
        // Устанавливаем начальный цвет как полностью черный
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        // Постепенно уменьшаем прозрачность до 0
        while (fadeImage.color.a > 0f)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            fadeImage.color = color;
            yield return null; // Ждем следующего кадра
        }

        // Выключаем объект, чтобы он не перехватывал клики мыши
        fadeImage.gameObject.SetActive(false);
    }
}
