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
    [SerializeField] private ShopController shopController;
    [SerializeField] private PlayerManager player;

    public static EnemyManager Spawner => instance.spawner;
    public static GameData GameData => instance.gameData;
    public static SaveOrLoadManager SaveOrLoad => instance.saveOrLoad;
    public static ErrorController ErrorController => instance.errorController;
    public static ShopController ShopController => instance.shopController;

    public static bool IsRun { get; set; } = false;

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
            cameraController.Initialization();
        });

        LoadingController.AddAction(() =>
        {
            UI_Manager.instance.Initialization();
        });

        LoadingController.AddAction(() =>
        {
            shopController.Initialization();
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
}
