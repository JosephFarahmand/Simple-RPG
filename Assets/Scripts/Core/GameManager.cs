using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [Header("Prefab")]
    [SerializeField] private CharacterPreview previewPrefab;

    [Header("Component")]
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameData gameData;
    [SerializeField] private SaveOrLoadManager saveOrLoad;

    public static EnemySpawner Spawner => instance.spawner;
    public static GameData GameData => instance.gameData;
    public static SaveOrLoadManager SaveOrLoad => instance.saveOrLoad;

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
        if (FindObjectOfType<CharacterPreview>() == null)
        {
            Instantiate(previewPrefab);
        }

        var player = FindObjectOfType<PlayerManager>();
        if(player == null)
        {
            Debug.LogError("Initialization ERROR!!\nPlayer Not Found!!");
            return;
        }
        player.Initialization();

        //preview.Initialization();

        cameraController.Initialization();

        UI_Manager.instance.Initialization();
    }
}
