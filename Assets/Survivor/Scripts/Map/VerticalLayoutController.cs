using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLayoutController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] MainElements;

    [SerializeField]
    private SpriteRenderer[] SideElements;

    public void SetMapSprite(Sprite mainSprite, Sprite sideSprite)
    {
        foreach (var element in MainElements)
        {
            element.sprite = mainSprite;
        }

        foreach (var element in SideElements)
        {
            element.sprite = sideSprite;
        }
    }
}
