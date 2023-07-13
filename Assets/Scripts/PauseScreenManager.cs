using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;
    [SerializeField]
    private Button resumeBtn, loadSaveBtn, quitBtn, pauseBtn;
    [SerializeField]
    private Toggle saveProgressToggle;

    // Start is called before the first frame update
    void Start()
    {
        pauseBtn.onClick.AddListener(OnClickPauseBtn);
        resumeBtn.onClick.AddListener(OnResumeGame);
        loadSaveBtn.onClick.AddListener(GameManager.LoadGame);
        quitBtn.onClick.AddListener(OnQuitGame);
    }

    void OnClickPauseBtn()
    {
        parent.SetActive(true);
        loadSaveBtn.interactable = GameManager.ExistSaveData();
        bool allowSave = GameManager.instance.score > 0;
        saveProgressToggle.isOn = allowSave;
        saveProgressToggle.interactable = allowSave;
        Time.timeScale = 0f;
    }

    void OnResumeGame()
    {
        parent.SetActive(false);
        Time.timeScale = 1f;
    }

    void OnQuitGame()
    {
        OnResumeGame();
        if (saveProgressToggle.isOn)
        {
            // Save game
            GameManager.SaveGame();
        }
        GameUiEventManager.Instance.Clear();
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
