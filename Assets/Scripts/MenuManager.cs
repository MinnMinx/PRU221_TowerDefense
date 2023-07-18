using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Button loadBtn;
    [SerializeField]
    private CanvasGroup loadingGroup;
    [SerializeField]
    private Slider loadingSlider;
    [SerializeField, Range(0, 1)]
    private float lerpMultipler = 0.5f;

    private void Start()
    {
        if (loadBtn != null && !GameManager.ExistSaveData())
        {
            loadBtn.interactable = false;
        }
    }
    public void NewGame()
    {
        StartCoroutine(LoadToScreen("Gameplay"));
    }

    public void ContinueGame()
    {
        PlayerPrefs.SetString("load", string.Empty);
        StartCoroutine(LoadToScreen("Gameplay"));
    }

    public void HighScore()
    {
        StartCoroutine(LoadToScreen("HighScore"));
    }

    public void About()
    {
         
    }

    public void QuitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    IEnumerator LoadToScreen(string sceneName)
    {
        loadingGroup.alpha = 1f;
        loadingGroup.blocksRaycasts = true;
        loadingSlider.value = 0;
        Time.fixedDeltaTime = 0.02f;
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;
        while (loadingSlider.value < 1f)
        {
            yield return null;
            loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, asyncOp.isDone ? 1f : asyncOp.progress / 0.9f, lerpMultipler * Time.deltaTime);
        }
        asyncOp.allowSceneActivation = true;
        loadingGroup.alpha = 0f;
        loadingGroup.blocksRaycasts = false;
    }
}
