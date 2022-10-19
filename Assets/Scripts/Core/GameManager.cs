using UnityEngine;

public interface IController
{
    void Initialization();
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [Header("Prefab")]
    [SerializeField] private CharacterPreview previewPrefab;

    [Header("Component")]
    [SerializeField] private EnemyManager spawner;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameData gameData;
    [SerializeField] private SaveOrLoadManager saveOrLoad;
    [SerializeField] private ErrorController errorController;
    [SerializeField] private InteractableManagement interactableManagement;
    [SerializeField] private PlayerManager player;

    public static EnemyManager Spawner => instance.spawner;
    public static GameData GameData => instance.gameData;
    public static SaveOrLoadManager SaveOrLoad => instance.saveOrLoad;
    public static ErrorController ErrorController => instance.errorController;
    public static PlayerManager Player => instance.player;
    public static InteractableManagement InteractableManagement => instance.interactableManagement;

    public static bool IsRun { get; private set; } = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CharacterPreview preview = FindObjectOfType<CharacterPreview>();
        if (preview == null)
        {
            preview = Instantiate(instance.previewPrefab);
        }
        preview.Initialization();

        errorController.Initialization();

        LoadingController.AddAction(() =>
        {
            DatabaseController.LoadItems();
        });

        LoadingController.AddAction(() =>
        {
            player.Initialization();
        });

        LoadingController.AddAction(() =>
        {
            interactableManagement.Initialization();
        });

        LoadingController.AddAction(() =>
        {
            cameraController.Initialization();
        });

        LoadingController.AddAction(() =>
        {
            UI_Manager.instance.Initialization();
        });

        LoadingController.AddAction(() =>
        {
            //DatabaseController.LoadProfile();
        });

        DatabaseController.Initialization();

        UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<EntryPage>());
        if (AccountController.CheckAccount())
        {
            UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<LoadingPage>());
        }
    }

    private Stats currentStats = Stats.None;
    private Stats lastStats = Stats.None;

    private void Update()
    {
        HandleLastStats();
    }

    private void HandleLastStats()
    {
        if (currentStats != lastStats)
        {
            switch (currentStats)
            {
                case Stats.None:
                    break;
                case Stats.InHome:
                    IsRun = false;
                    break;
                case Stats.PlayGame:
                    IsRun = true;
                    if (lastStats == Stats.InHome)
                    {
                        Spawner.ResetGame();
                    }
                    break;
                case Stats.PauseGame:
                    IsRun = false;
                    break;
            }

            lastStats = currentStats;
        }
    }

    //public static Stats GetCurrentStats() => instance.currentStats;
    public static void SetStats(Stats newStats) => instance.currentStats = newStats;

    public enum Stats
    {
        None,
        InHome,
        PlayGame,
        PauseGame
    }
}
