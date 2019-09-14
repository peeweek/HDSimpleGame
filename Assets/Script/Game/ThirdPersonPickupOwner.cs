using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonController;
using GameplayIngredients.Pickup;
using UnityEngine.VFX;

[RequireComponent(typeof(Character))]
public class ThirdPersonPickupOwner : PickupOwnerBase
{
    public VisualEffectAsset speedEffectVFX;

    public Character character { get { return gameObject.GetComponent<Character>(); } }

    private Coroutine m_SpeedCoroutine;

    public void ApplySpeedEffect(float duration, float multiplier)
    {
        if(m_SpeedCoroutine == null)
        {
            m_SpeedCoroutine = StartCoroutine(ApplySpeedCoroutine(duration, multiplier));
        }
    }

    GameObject SpawnEffectOnPlayer(VisualEffectAsset template)
    {
        GameObject effect = new GameObject(template.name);
        effect.transform.position = character.gameObject.transform.position;
        effect.transform.rotation = character.gameObject.transform.rotation;
        effect.transform.parent = character.gameObject.transform;
        var vfx = effect.AddComponent<VisualEffect>();
        vfx.visualEffectAsset = template;
        return effect;
    }

    IEnumerator ApplySpeedCoroutine(float duration, float multiplier)
    {
        var m = character.MovementSettings;

        GameObject effect = null;

        if(speedEffectVFX != null)
        {
            effect = SpawnEffectOnPlayer(speedEffectVFX);
        }

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

        if(effect != null)
            Destroy(effect);

        m_SpeedCoroutine = null;
    }
}
