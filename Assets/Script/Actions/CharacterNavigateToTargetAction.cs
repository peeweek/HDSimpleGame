using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayIngredients.Actions;
using ThirdPersonController;
using GameplayIngredients;

public class CharacterNavigateToTargetAction : ActionBase
{
    public Character TargetCharacter;
    public bool ApplyOnInstigator = true;
    [NonNullCheck]
    public Transform Target;

    public override void Execute(GameObject instigator = null)
    {
        Character target = TargetCharacter;

        if (ApplyOnInstigator)
            target = instigator.GetComponent<Character>();

        if (target != null && Target != null)
        {
            target.SetControlMode(Character.ControlMode.NavMesh);
            target.navMeshAgent.SetDestination(Target.transform.position);
        }
        else
            Debug.LogWarning("CharacterNavigateToTargetAction : Character is null or Target is null");
    }
}
