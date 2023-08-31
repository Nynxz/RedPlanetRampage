using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(ShopManager))]
[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(MissionManager))]
public class GameManager : MonoBehaviour {


    // Singleton Instance
    public static GameManager Instance;


    // Components Needed
    public UIManager UIManager { get; private set; }
    public InputManager InputManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    public ShopManager ShopManager { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public MissionManager MissionManager { get; private set; }

    public static string previousScene;
    public static string currentScene;


    protected void Awake() {
        DontDestroyOnLoad(this);

        // Set Singleton Instance to First Created Instance
        if (Instance != null) {
            Debug.LogError("Multiple instances of GameManager.cs found, Please only use one. Maybe managerscene is being loaded twice?");
        } else {
            Debug.Log("Starting GameManager");
            Instance = this;
            UIManager = GetComponent<UIManager>();
            InputManager = GetComponent<InputManager>();
            PlayerManager = GetComponent<PlayerManager>();
            ShopManager = GetComponent<ShopManager>();
            AudioManager = GetComponent<AudioManager>();
            MissionManager = GetComponent<MissionManager>();

            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }
    }
    protected void Start() {
        DontDestroyOnLoad(PlayerManager.Player.gameObject);
        DontDestroyOnLoad(UIManager.UICanvas.gameObject);
        DontDestroyOnLoad(UIManager.EventSystem.gameObject);

        Instance.PlayerManager.Player.RefreshPlayerEvents();
        Instance.UIManager.SetupInventoryUI(GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>().PlayerInventorySO);
        Instance.UIManager.SetupInventoryUpgradeList(GameManager.Instance.PlayerManager.Player.GetComponent<WeaponManager>().PlayerInventorySO);

    }

    private void SceneManager_activeSceneChanged(Scene current, Scene next) {
        if (current.name == null) {
            Debug.Log("Current Scene deleted");
            previousScene = currentScene;
            currentScene = next.name;
        } else {
            previousScene = currentScene;
            currentScene = next.name;

        }
        Debug.Log(previousScene + " --> " + currentScene);
    }
}
