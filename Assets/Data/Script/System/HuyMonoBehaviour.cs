using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuyMonoBehaviour : MonoBehaviour
{
    //===========================================Unity============================================
    protected virtual void Awake()
    {
        //this.LoadComponents();
    }

    protected virtual void OnEnable()
    {
        if (this is not ILateFixedUpdate lateFU) return;
        SystemManager.Instance.AddLateFU(lateFU);
    }

    protected virtual void OnDisable()
    {
        if (this is not ILateFixedUpdate lateFU) return;
        SystemManager.Instance.RemoveLateFU(lateFU);
    }

    //===========================================Method===========================================
    public virtual void LoadComponents()
    {
        //For override
    }

    protected void LoadComponent<T>(ref T component, Transform obj, string message)
    {
        if (obj == null) return;
        component = obj.GetComponent<T>();
        Debug.LogWarning(transform.name + ": " + message, transform.gameObject);
    }

    protected void LoadChildComponent<T>(ref T component, Transform obj, string message)
    {
        if (obj == null) return;
        component = obj.GetComponentInChildren<T>(true);
        Debug.LogWarning(transform.name + ": " + message, transform.gameObject);
    }

    protected virtual void LoadComponent<T>(ref List<T> components, Transform obj, string message)
    {
        components = new List<T>();
        if (obj == null) return;
        foreach (Transform child in obj) components.Add(child.GetComponent<T>());
        Debug.LogWarning(transform.name + ": " + message, transform.gameObject);
    }

    protected virtual void LoadSO<T>(ref T so, string filePath) where T : ScriptableObject
    {
        if (so == null || so.Equals(null))
        {
            so = Resources.Load<T>(filePath);
            Debug.LogWarning(transform.name + ": LoadSO()", transform.gameObject);
        }
    }
}