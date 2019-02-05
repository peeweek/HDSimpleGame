using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

static class HDSimpleGamePlayFromHere
{
    [InitializeOnLoadMethod]
    static void SetPlayFromHere()
    {
        GameplayIngredients.Editor.PlayFromHere.OnPlayFromHere += PlayFromHere_OnPlayFromHere;
    }

    private static void PlayFromHere_OnPlayFromHere(Vector3 position, Vector3 forward)
    {
        var initialPosition = position + forward;
        var initialForward = forward;
        initialForward.Scale(new Vector3(1, 0, 1));
        initialForward.Normalize();
        var initialRotation = Quaternion.LookRotation(initialForward, Vector3.up);

        var prefab = (GameObject)Resources.Load("DefaultPlayer");
        var Player = Object.Instantiate(prefab, initialPosition, initialRotation);
        Player.name = "(Play From Here)" + prefab.name;
    }
}
