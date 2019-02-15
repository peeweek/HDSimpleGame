using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonController;
using GameplayIngredients.Pickup;

[RequireComponent(typeof(Character))]
public class ThirdPersonPickupOwner : PickupOwnerBase
{
    public Character character { get { return gameObject.GetComponent<Character>(); } }

    private Coroutine m_SpeedCoroutine;

    public void ApplySpeedEffect(float duration, float multiplier)
    {
        if(m_SpeedCoroutine == null)
        {
            m_SpeedCoroutine = StartCoroutine(ApplySpeedCoroutine(duration, multiplier));
        }
    }

    IEnumerator ApplySpeedCoroutine(float duration, float multiplier)
    {
        var m = character.MovementSettings;

        float walkSpeed = m.WalkSpeed;
        float jogSpeed = m.JogSpeed;
        float runSpeed = m.SprintSpeed;

        m.WalkSpeed *= multiplier;
        m.JogSpeed *= multiplier;
        m.SprintSpeed *= multiplier;

        yield return new WaitForSeconds(duration);

        m.WalkSpeed = walkSpeed;
        m.JogSpeed = jogSpeed;
        m.SprintSpeed = runSpeed;

        m_SpeedCoroutine = null;
    }
}
