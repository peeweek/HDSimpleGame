using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayIngredients.LevelStreaming;
using NaughtyAttributes;
using GameplayIngredients;

public class GameLevel : ScriptableObject
{
    [ReorderableList, Scene]
    public string[] StartupScenes;

    [ReorderableList, Scene]
    public string[] ScenesToUnload;

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Assets/Create/Game Level")]
    static void CreateGameLevel()
    {
        GameplayIngredients.Editor.AssetFactory.CreateAssetInProjectWindow<GameLevel>("", "New Game Level.asset");
    }
#endif
}
