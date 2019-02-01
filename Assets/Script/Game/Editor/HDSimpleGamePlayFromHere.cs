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
        var Player = Object.Instantiate((GameObject)Resources.Load("DefaultPlayer"));
        Player.transform.position = position + forward;
    }
}
