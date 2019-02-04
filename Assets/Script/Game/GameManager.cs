using GameplayIngredients;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ManagerDefaultPrefab("GameManager")]
public class GameManager : Manager
{
    [ReorderableList]
    public Callable[] OnGameStart;

    public void Start()
    {
        Callable.Call(OnGameStart);
    }
}
