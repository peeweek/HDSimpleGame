using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonController;
using GameplayIngredients.Pickup;

[RequireComponent(typeof(Character))]
public class ThirdPersonPickupOwner : PickupOwnerBase
{
    private Character m_Character;

    private void OnEnable()
    {
        m_Character = GetComponent<Character>();
    }
}
