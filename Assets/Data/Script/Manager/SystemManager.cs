using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILateFixedUpdate
{
    void LateFixedUpdate();
}

public class SystemManager : HuyMonoBehaviour
{
    //==========================================Variable==========================================
    [Header("System Manager")]
    [SerializeField] private static SystemManager instance;
    [SerializeField] protected List<ILateFixedUpdate> lateFUs = new List<ILateFixedUpdate>();

    //==========================================Get Set===========================================
    public static SystemManager Instance => instance;

    //===========================================Unity============================================
    protected override void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("One SystemManager Only", transform.gameObject);
            return;
        }

        instance = this;
    }

    //===========================================Method===========================================
    public void LateFixedUpdate()
    {
        if (this.lateFUs.Count == 0) return;
        foreach (ILateFixedUpdate child in this.lateFUs)
        {
            child.LateFixedUpdate();
        }
    }
    
    public void AddLateFU(ILateFixedUpdate child)
    {
        this.lateFUs.Add(child);
    }

    public void RemoveLateFU(ILateFixedUpdate child)
    {
        this.lateFUs.Remove(child);
    }
}
