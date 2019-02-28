using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using GameplayIngredients;
using GameplayIngredients.Actions;
using NaughtyAttributes;

public class GameLevelLoadAction : ActionBase
{
    public enum Change
    {
        Load,
        Unload
    }

    public enum Target
    {
        MainMenu,
        First,
        Previous,
        Current,
        Next,
        Last,
        Specified,
        FromGameSave,
    }
    public bool ShowUI = true;
    public Change change = Change.Load;
    public Target level = Target.First;
    [NonNullCheck, ShowIf("isSpecified"), Tooltip("Which Level to Load/Unload, when selected 'Specified' level")]
    public GameLevel specifiedLevel;

    [ShowIf("isGameSave")]
    public int UserSaveIndex = 0;
    [ShowIf("isGameSave")]
    public string UserSaveName = "Progress";

    [ReorderableList]
    public Callable[] OnComplete;

    private bool isSpecified() { return level == Target.Specified; }
    private bool isGameSave() { return level == Target.FromGameSave; }

    public override void Execute(GameObject instigator = null)
    {
        int index = -2;
        var manager = Manager.Get<GameManager>();

        switch (level)
        {
            case Target.MainMenu: index = -1; break;
            case Target.First: index = 0; break;
            case Target.Last: index = manager.MainGameLevels.Length - 1; break;
            case Target.Current: index = manager.currentLevel; break;
            case Target.Previous: index = Mathf.Max(0, manager.currentLevel-1); break;
            case Target.Next: index = Mathf.Max(manager.MainGameLevels.Length - 1, manager.currentLevel + 1); break;
            case Target.Specified:
                if(specifiedLevel != null && manager.MainGameLevels.Contains(specifiedLevel))
                {
                    index = manager.MainGameLevels.ToList().IndexOf(specifiedLevel);
                }
                break;
        }

        if(level == Target.MainMenu)
        {
            switch (change)
            {
                case Change.Load:
                    manager.LoadMainMenu(ShowUI, OnComplete);
                    break;
                case Change.Unload:
                    manager.UnloadMainMenu(ShowUI, OnComplete);
                    break;
            }
        }
        else
        {
            switch(change)
            {
                case Change.Load: manager.LoadLevel(index, ShowUI, OnComplete);
                    break;
                case Change.Unload: manager.UnloadLevel(index, ShowUI, OnComplete);
                    break;
            }
        }

    }
}
