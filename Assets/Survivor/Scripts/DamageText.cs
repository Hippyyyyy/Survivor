using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshPro text;
    [SerializeField] private Animator animator;

    [Header(" Movement ")]
    [SerializeField] private float moveAmplitude;
    [SerializeField] private float moveSpeed;

    
    private void Start()
    {
        
    }

    [NaughtyAttributes.Button]
    public void Configure(string textString, bool isCriticalHit)
    {
        text.text = textString;
        text.color = isCriticalHit ? Color.yellow : Color.white;
        animator.Play("animate");
    }
}
