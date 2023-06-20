using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private GameObject _highScoreTable;

    // Start is called before the first frame update
    void Start()
    {
        _highScoreTable = GameObject.Find("HighScoreTable");
        _highScoreTable.SetActive(false);
    }



    public void NewGame()
    {
        SceneManager.LoadScene("Gameplay");
    }




    public void ContinueGame()
    {
        SceneManager.LoadScene("");
    }





    public void HighScore()
    {
        _highScoreTable.SetActive(true);
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
