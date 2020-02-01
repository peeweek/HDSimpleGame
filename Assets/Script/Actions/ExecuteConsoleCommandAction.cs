using GameplayIngredients.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteConsoleCommandAction : ActionBase
{
    public string command = "screenshot";

    public override void Execute(GameObject instigator = null)
    {
        ConsoleUtility.Console.ExecuteCommand(command);
    }

    public override string GetDefaultName()
    {
        return $"Console : '{command}'";
    }
}
