using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectByMouse : Detector
{
    //==========================================Variable==========================================
    [Header("By Mouse")]
    [SerializeField] private InterfaceReference<IDetectByMouse> user1;
    [SerializeField] protected float detectRange;
    [SerializeField] protected Transform target;

    //==========================================Get Set===========================================
    public IDetectByMouse User1 { get => user1.Value; set => user1.Value = value; }
    public float DetectRange { get => detectRange; set => detectRange = value; }
    public override Transform Target => this.target;

    //===========================================Unity============================================
    protected virtual void Update()
    {
        this.Detecting();
    }

    //==========================================Override==========================================
    public override void ResetTarget()
    {
        this.target = null;
    }

    //===========================================Method===========================================
    protected virtual void Detecting()
    {
        if (!this.user1.Value.CanDetect(this)) return;
        this.Detect();
    }
    
    protected virtual void Detect()
    {
        IDetectByMouse tempUser = this.user1.Value;
        Vector2 mainObjPos = tempUser.GetMainObj(this).position;
        Vector2 targetPos = InputManager.Instance.MousePos;
        float distance = Vector2.Distance(mainObjPos, targetPos);
        
        if (distance > this.detectRange) return;
        Collider2D col = Physics2D.OverlapCircle(targetPos, 0.1f);
            
        if (col == null) return;
        foreach (string tag in this.tags)
        {
            if (!col.gameObject.CompareTag(tag)) continue;
            this.target = col.transform;
            return;
        }
    }
}
