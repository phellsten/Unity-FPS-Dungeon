// Paul Hellsten - 758077
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public Button controls;

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
        controls.onClick.AddListener(Controls);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("level1");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void Controls()
    {
        SceneManager.LoadScene("ControlsMenu");
    }
}