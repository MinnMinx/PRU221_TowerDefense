using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Button loadBtn;
    private void Start()
    {
        if (loadBtn != null && !GameManager.ExistSaveData())
        {
            loadBtn.interactable = false;
        }
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ContinueGame()
    {
        PlayerPrefs.SetString("load", string.Empty);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Gameplay");
    }

    public void HighScore()
    {
        SceneManager.LoadScene("HighScore");
    }

    public void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        print("Exiting game");
    }
}
