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
    private static SystemManager instance;
    [SerializeField] protected List<ILateFixedUpdate> lateFUs;

    //==========================================Get Set===========================================
    public static SystemManager Instance
    {
        get
        {
            if (instance == null) instance = FindAnyObjectByType<SystemManager>();
            return instance;
        }
    }

    //===========================================Unity============================================
    private void FixedUpdate() // LateFixedUpdate()
    {
        foreach (ILateFixedUpdate child in this.lateFUs)
        {
            child.LateFixedUpdate();
        }
    }

    //===========================================Method===========================================
    public void AddLateFU(ILateFixedUpdate child)
    {
        this.lateFUs.Add(child);
    }

    public void RemoveLateFU(ILateFixedUpdate child)
    {
        this.lateFUs.Remove(child);
    }
}
