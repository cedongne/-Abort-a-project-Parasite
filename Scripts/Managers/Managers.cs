using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

    #region Contents
    GameManagerEx _game = new GameManagerEx();

    public static GameManagerEx Game { get { return Instance._game; } }
    #endregion

    #region Core
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    DataManager _data = new DataManager();
    SaveLoadManager _saveLoad = new SaveLoadManager();
    PoolManager _pool = new PoolManager();
    MonsterManager _monster = new MonsterManager();
    /*
    PoolManager _pool = new PoolManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();
    */
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static DataManager Data { get { return Instance._data; } }
    public static SaveLoadManager SaveLoad { get { return Instance._saveLoad; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static MonsterManager Monster { get { return Instance._monster; } }
    /*
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    */
    #endregion

    private void Awake()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            s_instance._data.Init();
            s_instance._saveLoad.Init();
            s_instance._pool.Init();
            s_instance._monster.Init();
            //           s_instance._sound.Init();


            NewGame();
        }
    }

    private void OnDisable()
    {
        Debug.Log("@Managers is disabled");
    }

    static void NewGame()
    {
        s_instance._saveLoad.NewGame();
        PlayerController.playerStat = s_instance._data.ItemList.Find(x => x.objectName.Equals("닭"));
    }

    public static void Clear()
    {
        Input.Clear();
        Pool.Clear();
        /*
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        */
    }
}
