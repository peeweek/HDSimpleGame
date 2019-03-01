using GameplayIngredients;
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

    public int currentLevel { get { return m_CurrentLevel; } }
    public int currentSaveProgress {
        get { return Manager.Get<GameSaveManager>().GetInt("Game.Progress", GameSaveManager.Location.User);  }
        set { Manager.Get<GameSaveManager>().SetInt("Game.Progress", GameSaveManager.Location.User, value);  }
    }

    // Not Serialized
    int m_CurrentLevel = -2;

    public void Start()
    {
        m_CurrentLevel = int.MinValue;
        Callable.Call(OnGameStart);
        Manager.Get<GameSaveManager>().LoadUserSave(0);
    }

    public void LoadMainMenu(bool showUI = false, Callable[] onComplete = null)
    {
        if (MainMenuGameLevel != null && MainMenuGameLevel.StartupScenes != null && MainMenuGameLevel.StartupScenes.Where(o => o == null).Count() == 0)
        {
            string[] levels = MainMenuGameLevel.StartupScenes;
            Manager.Get<LevelStreamingManager>().LoadScenes(LevelStreamingManager.StreamingAction.Load, levels, levels[0], showUI, onComplete);
            m_CurrentLevel = -1;
        }
    }

    public void UnloadMainMenu(bool showUI = false, Callable[] onComplete = null)
    {
        if (MainMenuGameLevel != null && MainMenuGameLevel.ScenesToUnload != null && MainMenuGameLevel.ScenesToUnload.Where(o => o == null).Count() == 0)
        {
            string[] levels = MainMenuGameLevel.ScenesToUnload;
            Manager.Get<LevelStreamingManager>().LoadScenes(LevelStreamingManager.StreamingAction.Unload, levels, null, showUI, onComplete);
            m_CurrentLevel = -1;
        }
    }



    public void LoadLevel(int index, bool showUI = true, Callable[] onComplete = null)
    {
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
            m_CurrentLevel = index;
        }
    }

    public void UnloadLevel(int index, bool showUI = false, Callable[] onComplete = null)
    {
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
