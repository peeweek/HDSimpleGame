using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayIngredients.Actions;
using ThirdPersonController;

public class SetCharacterControlModeAction : ActionBase
{
    public Character TargetCharacter;
    public bool ApplyOnInstigator = true;
    public Character.ControlMode ControlMode = Character.ControlMode.Player;

    public override void Execute(GameObject instigator = null)
    {
        Character target = TargetCharacter;

        if (ApplyOnInstigator)
            target = instigator.GetComponent<Character>();

        if(target != null)
        {
            target.SetControlMode(ControlMode);
        }
        else
            Debug.LogWarning("SetCharacterControlModeAction : Character is null");
    }
}
