using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("=====Entity=====")]
    [SerializeField] protected string id;
    [SerializeField] protected SpriteRenderer model;
    [SerializeField] protected Animator animator;

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponent(ref this.model, transform.Find("Model"), "LoadModel()");
        this.LoadComponent(ref this.animator, transform.Find("Model"), "LoadAnimator()");
    }
}
