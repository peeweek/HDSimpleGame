using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [Header("Text")]
    public string Title = "Note Title";
    [Multiline(8)]
    public string Body = @"Lorem Ipsum is simply dummy text of the
printing and typesetting industry. Lorem 
Ipsum has been the industry's standard 
dummy text ever since the 1500s, when 
an unknown printer took a galley of type 
and scrambled it to make a type specimen 
book. It has survived not only five centuries, 
but also the leap into electronic typesetting, 
remaining essentially unchanged.";

    [Header("Colors")]
    public Color TitleColor = Color.white;
    public Color BodyColor = Color.gray;

    [Header("Objects")]
    public TextMesh TitleTM;
    public TextMesh BodyTM;

    private void Start()
    {
        UpdateText();
    }

    private void OnValidate()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if (TitleTM != null)
        {
            TitleTM.text = Title;
            TitleTM.fontSize = 0;
            TitleTM.fontStyle = FontStyle.Normal;
            TitleTM.color = TitleColor;
        }

        if (BodyTM != null)
        {
            BodyTM.text = Body;
            BodyTM.fontSize = 0;
            BodyTM.fontStyle = FontStyle.Normal;
            BodyTM.color = BodyColor;
        }
    }

}
