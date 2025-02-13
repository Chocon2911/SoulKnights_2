using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectByMouse : Detector
{
    //==========================================Variable==========================================
    [Header("By Mouse")]
    [SerializeField] private InterfaceReference<IDetectByMouse> user1;
    [SerializeField] protected float detectRange;

    //==========================================Get Set===========================================
    public IDetectByMouse User1 { get => user1.Value; set => user1.Value = value; }
    public float DetectRange { get => detectRange; set => detectRange = value; }

    //===========================================Method===========================================
    protected override void Detect()
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
