using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLayoutController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] LayoutElements;

    public void SetMapSprite(Sprite mapSprite)
    {
        foreach (var element in LayoutElements)
        {
            element.sprite = mapSprite;
        }
    }
}
