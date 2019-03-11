using GameplayIngredients;
using GameplayIngredients.Actions;
using GameplayIngredients.LevelStreaming;
using NaughtyAttributes;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[ManagerDefaultPrefab("GameManager")]
public class GameManager : Manager
{
    [Header("Events")]
    [ReorderableList]
    public Callable[] OnGameStart;
    [ReorderableList]
    public Callable[] OnLevelLoaded;
    [ReorderableList]
    public Callable[] OnMainMenuLoaded;

    [Header("Levels"),NonNullCheck]
    public GameLevel MainMenuGameLevel;
    [ReorderableList,NonNullCheck]
    public GameLevel[] MainGameLevels;

    [Header("Save")]
    public string ProgressSaveName = "Progress";
    [ShowNativeProperty]
    public int currentLevel { get; private set; } = -2;

    [ShowNativeProperty]
    public int currentSaveProgress {
        get { Manager.Get<GameSaveManager>().LoadUserSave(0); return Manager.Get<GameSaveManager>().GetInt(ProgressSaveName, GameSaveManager.Location.User);  }
        set { Manager.Get<GameSaveManager>().SetInt(ProgressSaveName, GameSaveManager.Location.User, value); Manager.Get<GameSaveManager>().SaveUserSave(0); }
    }

    GameObject m_CurrentLevelSwitch;


    public void Start()
    {
        currentLevel = int.MinValue;
        Callable.Call(OnGameStart);
        Manager.Get<GameSaveManager>().LoadUserSave(0);
    }

    Callable GetCurrentLevelSwitch(int targetLevel, bool showUI = false, Callable[] onComplete = null)
    {
        GameObject go = new GameObject();
        go.name = $"LevelSwtich {currentLevel} -> {targetLevel}";
        go.transform.parent = this.transform;
        m_CurrentLevelSwitch = go;

        var cameraFade = go.AddComponent<FullScreenFadeAction>();
        var unloadLevel = go.AddComponent<GameLevelLoadAction>();
        var loadLevel = go.AddComponent<GameLevelLoadAction>();
        var sendMessage = go.AddComponent<SendMessageAction>();
        var destroy = go.AddComponent<DestroyObjectAction>();

        cameraFade.Fading = FullScreenFadeManager.FadeMode.ToBlack;
        cameraFade.Name = "Fade to Black";
        cameraFade.Duration = 1.0f;
        cameraFade.OnComplete = new Callable[]{ unloadLevel };

        unloadLevel.Name = "Unload Current";
        unloadLevel.change = GameLevelLoadAction.Change.Unload;
        unloadLevel.level = GameLevelLoadAction.Target.Current;
        unloadLevel.ShowUI = false;
        unloadLevel.OnComplete = new Callable[] { loadLevel };

        loadLevel.Name = $"Load {targetLevel}";
        loadLevel.change = GameLevelLoadAction.Change.Load;
        loadLevel.level = GameLevelLoadAction.Target.SpecifiedLevel;
        loadLevel.ShowUI = showUI;
        loadLevel.specifiedLevel = MainGameLevels[targetLevel];
        loadLevel.OnComplete = new Callable[] { sendMessage, destroy };

        sendMessage.Name = "Send GAME_START";
        sendMessage.MessageToSend = "GAME_START";

        destroy.ObjectsToDestroy = new GameObject[] { go };

        // Return first callable
        return cameraFade;

    }

    public void SwitchLevel(int nextLevel, bool showUI = false, Callable[] onComplete = null)
    {
        if (m_CurrentLevelSwitch == null)
        {
            var call = GetCurrentLevelSwitch(nextLevel, showUI, onComplete);
            call.Execute();
        }
        else
            Debug.LogWarning("SwitchLevel : an Operation was still in progress and switching level could not be done");
    }

    public void LoadMainMenu(bool showUI = false, Callable[] onComplete = null)
    {
        if (MainMenuGameLevel != null && MainMenuGameLevel.StartupScenes != null && MainMenuGameLevel.StartupScenes.Where(o => o == null).Count() == 0)
        {
            string[] levels = MainMenuGameLevel.StartupScenes;
            Manager.Get<LevelStreamingManager>().LoadScenes(LevelStreamingManager.StreamingAction.Load, levels, levels[0], showUI, onComplete);
            currentLevel = -1;
        }
    }

    public void UnloadMainMenu(bool showUI = false, Callable[] onComplete = null)
    {
        if (MainMenuGameLevel != null && MainMenuGameLevel.ScenesToUnload != null && MainMenuGameLevel.ScenesToUnload.Where(o => o == null).Count() == 0)
        {
            string[] levels = MainMenuGameLevel.ScenesToUnload;
            Manager.Get<LevelStreamingManager>().LoadScenes(LevelStreamingManager.StreamingAction.Unload, levels, null, showUI, onComplete);
            currentLevel = -1;
        }
    }



    public void LoadLevel(int index, bool showUI = true, Callable[] onComplete = null)
    {
        if (index == -1)
            LoadMainMenu(showUI, onComplete);

        if(index >= 0 
            && MainGameLevels != null 
            && index < MainGameLevels.Length 
            && MainGameLevels[index] != null
            && MainGameLevels[index].StartupScenes != null
            && MainGameLevels[index].StartupScenes.Length > 0)
        {
            string[] levels = MainGameLevels[index].StartupScenes;

            Callable[] nextCalls = (index >= 0 ? OnLevelLoaded : OnMainMenuLoaded);

            if (onComplete != null)
            {
                if (nextCalls == null)
                    nextCalls = onComplete;
                else
                    nextCalls = nextCalls.Concat(onComplete).ToArray();
            }

            Manager.Get<LevelStreamingManager>().LoadScenes(LevelStreamingManager.StreamingAction.Load, levels, levels[0], showUI, nextCalls);
            currentLevel = index;
            currentSaveProgress = index;
        }
    }

    public void UnloadLevel(int index, bool showUI = false, Callable[] onComplete = null)
    {
        if (index == -1)
            UnloadMainMenu(showUI, onComplete);

        if (index >= 0
            && MainGameLevels != null
            && index < MainGameLevels.Length
            && MainGameLevels[index] != null
            && MainGameLevels[index].ScenesToUnload != null
            && MainGameLevels[index].ScenesToUnload.Length > 0)
        {
            string[] levels = MainGameLevels[index].ScenesToUnload;
            Manager.Get<LevelStreamingManager>().LoadScenes(LevelStreamingManager.StreamingAction.Unload, levels, null, showUI, onComplete);
        }
    }
}
