using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
