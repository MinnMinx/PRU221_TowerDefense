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
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private AudioSource bgmSrc;
    [SerializeField]
    private TowerManager towerMng;
    [SerializeField]
    private SlotChooserManager slotChooserManager;

    // Start is called before the first frame update
    void Start()
    {
        pauseBtn.onClick.AddListener(OnClickPauseBtn);
        resumeBtn.onClick.AddListener(OnResumeGame);
        loadSaveBtn.onClick.AddListener(OnLoadGame);
        quitBtn.onClick.AddListener(OnQuitGame);
        volumeSlider.value = bgmSrc.volume;
        volumeSlider.onValueChanged.AddListener(f => bgmSrc.volume = f);
    }

    void OnClickPauseBtn()
    {
        slotChooserManager.DisablePreviewTower();
        towerMng.DisableRemoving();
        towerMng.FinishLevelingUp();
        parent.SetActive(true);
        loadSaveBtn.interactable = GameManager.ExistSaveData();
        bool allowSave = GameManager.instance.score > 0;
        saveProgressToggle.isOn = allowSave;
        saveProgressToggle.interactable = allowSave;
        Time.timeScale = 0f;
        bgmSrc.Pause();
    }

    void OnResumeGame()
    {
        parent.SetActive(false);
        Time.timeScale = 1f;
        bgmSrc.Play();
    }

    void OnLoadGame()
    {
        GameManager.LoadGame();
        bool allowSave = GameManager.instance.score > 0;
        saveProgressToggle.isOn = allowSave;
        saveProgressToggle.interactable = allowSave;
    }

    void OnQuitGame()
    {
        OnResumeGame();
        if (saveProgressToggle.isOn)
        {
            // Save game
            GameManager.SaveGame();
            GameEventManager.Instance.Clear();
            Destroy(GameManager.instance.gameObject);
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Destroy(GameManager.instance.gameObject);
            GameManager.instance.GoToScoreScreen();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            OnClickPauseBtn();
        }
    }
}
