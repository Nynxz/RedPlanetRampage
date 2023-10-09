using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField] private Button playButton;
    [SerializeField] private Button highscoreButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    protected void Start() {
        playButton.onClick.AddListener(PlayButtonClicked);
        highscoreButton.onClick.AddListener(HighscoreButtonClicked);
        settingsButton.onClick.AddListener(CreditsButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);
    }

    private void PlayButtonClicked() {
        Debug.Log("Start Cine");
        string sceneToLoad = "StartEndCineScene";
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        //SceneManager.LoadScene("ManagerScene", LoadSceneMode.Additive);
        GameManager.currentScene = sceneToLoad;
    }

    private void HighscoreButtonClicked() {
        Debug.Log("High Scores");
    }

    private void CreditsButtonClicked() {
        SceneManager.LoadScene("CreditsScene");
    }

    private void ExitButtonClicked() {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
