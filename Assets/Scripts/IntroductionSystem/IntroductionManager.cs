using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// On Game Start
// Load a welcome menu, explaining the situation and goal
// Once player continues, give them a basic layout of the controls
/*
 <TODO>
Alert... There is an outbreak on mars in one of the Griffith Science Labratorys....
They have destroyed the escape pod mechanism and you need to find the parts to repair it
They have been scattered on different floors of the lab
Continue deep to find the parts and be careful along the way
Come back to the Hub at any time to purchase ammo, weapons, upgrades and heal from the shop
Griffith was holding the tougher monsters further into the surface of mars, so be careful as you descend

Come back to the hub once you have all the parts to repair the escape pod
Good luck!
 */
public class IntroductionManager : MonoBehaviour {
    [Serializable]
    private struct PageInfo {
        public CurrentPage currentPage;
        public string PageHeader;
        [TextArea(5, 10)] public string PageText;
        public Sprite PageSprite;
        public string ConfirmButtonText;
    }

    [SerializeField] private TextMeshProUGUI headerTMP;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TextMeshProUGUI confirmButtonText;
    [SerializeField] private TextMeshProUGUI infoTextArea;
    [SerializeField] private Image imageArea;

    [SerializeField] private PageInfo introInfo;
    [SerializeField] private PageInfo controlsInfo;

    enum CurrentPage { None, Introduction, Controls };
    private CurrentPage currentPage = CurrentPage.None;

    void Start() {
        NextPage();
        confirmButton.onClick.AddListener(() => {
            NextPage();
        });
    }

    private void NextPage() {
        switch (currentPage) {
            case CurrentPage.None:
                SetupPage(introInfo);
                break;
            case CurrentPage.Introduction:
                SetupPage(controlsInfo);
                break;
            case CurrentPage.Controls:
                StartGame();
                break;
        }
    }

    private void SetupPage(PageInfo pageInfo) {
        currentPage = pageInfo.currentPage;
        headerTMP.text = pageInfo.PageHeader;
        confirmButtonText.text = pageInfo.ConfirmButtonText;
        infoTextArea.text = pageInfo.PageText;

        if(pageInfo.PageSprite != null) {
            imageArea.gameObject.SetActive(true);
            imageArea.sprite = pageInfo.PageSprite;
        } else {
            imageArea.gameObject.SetActive(false);
        }
    }

    private void StartGame() {
        confirmButton.onClick.RemoveAllListeners();
        Debug.Log("Start Game");
        string sceneToLoad = "Hub-H";
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        GameManager.currentScene = sceneToLoad;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }


}
