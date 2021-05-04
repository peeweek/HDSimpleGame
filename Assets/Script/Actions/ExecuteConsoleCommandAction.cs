using GameplayIngredients.Actions;
using UnityEngine;
using GameplayIngredients;

[Callable("HDSimpleGame", "Misc/ic-callable.png")]
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
