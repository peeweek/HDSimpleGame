using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonController;
using GameplayIngredients.Pickup;

public class ThirdPersonSpeedupPickupEffect : PickupEffectBase
{
    public float Duration = 3.0f;
    public float SpeedMultiplier = 2.0f;

    public override void ApplyPickupEffect(PickupOwnerBase owner)
    {
        var playerOwner = (ThirdPersonPickupOwner)owner;
        playerOwner.ApplySpeedEffect(Duration, SpeedMultiplier);
    }


}
