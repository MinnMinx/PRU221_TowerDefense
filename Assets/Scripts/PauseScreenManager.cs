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
        loadSaveBtn.onClick.AddListener(() => GameUiEventManager.Instance.Notify(TowerManager.LOAD_TOWER_EVT));
        quitBtn.onClick.AddListener(OnQuitGame);
    }

    void OnClickPauseBtn()
    {
        parent.SetActive(true);
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
            GameUiEventManager.Instance.Notify(TowerManager.SAVE_TOWER_EVT);
            Debug.Log("SaveGame");
        }
        GameUiEventManager.Instance.Clear();
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
