using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareLayoutController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer MainElement;

    [SerializeField]
    private SpriteRenderer[] SideElements;

    [SerializeField]
    private SpriteRenderer TopElement;

    [SerializeField]
    private SpriteRenderer BotElement;

    public void SetMapSprite(Sprite mainSprite, Sprite sideSprite, Sprite topSprite, Sprite botSprite)
    {
        MainElement.sprite = mainSprite;

        foreach (var element in SideElements)
        {
            element.sprite = sideSprite;
        }

        TopElement.sprite = topSprite;
        BotElement.sprite = botSprite;
    }
}
