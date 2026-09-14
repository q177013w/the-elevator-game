using UnityEngine;
using UnityEngine.SceneManagement;

// Повесить этот скрипт на пустой GameObject внутри Canvas (например "MenuManager")
public class MenuManager : MonoBehaviour
{
    [Header("Имена сцен (как в Build Settings)")]
    [SerializeField] private string newGameSceneName = "Scene1";
    [SerializeField] private string daySelectSceneName = "DaySelectMenu";
    [SerializeField] private string defaultSceneIfNoSave = "Scene1";

    [Header("Ссылки на UI")]
    [SerializeField] private GameObject settingsPanel; // панель настроек (изначально выключена)

    // === НОВАЯ ИГРА ===
    public void OnNewGameClicked()
    {
        // Сбрасываем сохранённый прогресс, если нужно начинать с нуля
        PlayerPrefs.DeleteKey("LastScene");
        SceneManager.LoadScene(newGameSceneName);
    }

    // === ПРОДОЛЖИТЬ ===
    public void OnContinueClicked()
    {
        string lastScene = PlayerPrefs.GetString("LastScene", defaultSceneIfNoSave);
        SceneManager.LoadScene(lastScene);
    }

    // === ВЫБРАТЬ ДЕНЬ ===
    public void OnSelectDayClicked()
    {
        SceneManager.LoadScene(daySelectSceneName);
    }

    // === НАСТРОЙКИ ===
    public void OnSettingsClicked()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void OnCloseSettingsClicked()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    // === ВЫЙТИ ===
    public void OnQuitClicked()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
