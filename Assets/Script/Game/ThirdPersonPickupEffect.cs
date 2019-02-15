using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonController;
using GameplayIngredients.Pickup;

public class ThirdPersonPickupEffect : PickupEffectBase
{
    public float SpeedMultiplier = 2.0f;

    public override void ApplyPickupEffect(PickupOwnerBase owner)
    {
        var playerOwner = (ThirdPersonPickupOwner)owner; 
    }
}
