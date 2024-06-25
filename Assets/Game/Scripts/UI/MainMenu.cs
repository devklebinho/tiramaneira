using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject optionScreen;
    [SerializeField] GameObject buttonHolder;

    void Start()
    {
        optionScreen.SetActive(false);
        buttonHolder.SetActive(true);
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenOptions()
    {
        optionScreen.SetActive(true);
        buttonHolder.SetActive(false);
    }

    public void CloseOptions()
    {
        optionScreen.SetActive(false);
        buttonHolder.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
