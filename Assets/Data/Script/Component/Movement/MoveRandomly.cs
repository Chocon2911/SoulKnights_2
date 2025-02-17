using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveRandomly : IMovement
{
    bool CanFinishMove(MoveRandomly component);
}


public class MoveRandomly : Movement
{
    //==========================================Variable==========================================
    [Header("Randomly")]
    [SerializeField] protected InterfaceReference<IMoveRandomly> user1;
    [SerializeField] protected Cooldown randomCD;
    [SerializeField] protected float randomRadMax;
    [SerializeField] protected float randomRadMin;
    [SerializeField] protected Vector2 goalPos;
    [SerializeField] protected bool isReachGoal;

    //==========================================Get Set===========================================
    public IMoveRandomly User1 { set => user1.Value = value; }

    //===========================================Unity============================================
    protected virtual void Update()
    {
        this.FinishingImediately();
        this.RandomizingGoal();
        this.Finishing();
    }
    
    protected override void FixedUpdate()
    {
        this.Recharging();
        base.FixedUpdate();
    }

    //=======================================Randomize Goal=======================================
    protected virtual void RandomizingGoal()
    {
        if (!this.randomCD.IsReady) return;
        this.RandomizeGoal();
    }

    protected virtual void RandomizeGoal()
    {
        this.GetRandomPos();
        this.isReachGoal = false;
    }

    protected virtual void GetRandomPos()
    {
        int[] randomArr = { -1, 1 };
        int horizontal = randomArr[Random.Range(0, randomArr.Length)];
        int vertical = randomArr[Random.Range(0, randomArr.Length)];
        float randomRad = Random.Range(this.randomRadMin, this.randomRadMax);
        
        float xPos = horizontal * randomRad + transform.position.x;
        float yPos = vertical * randomRad + transform.position.y;
        this.goalPos = new Vector2(xPos, yPos);
    }

    //==========================================Recharge==========================================
    protected virtual void Recharging()
    {
        if (!this.isReachGoal) return;
        if (!this.user1.Value.CanMove(this)) return;
        this.Recharge();
    }

    protected virtual void Recharge()
    {
        this.randomCD.CoolingDown();
    }

    //===========================================Finish===========================================
    protected virtual void Finishing()
    {
        if (this.isReachGoal) return;

        if (transform.position.x > this.goalPos.x + 0.1
            && transform.position.x < this.goalPos.x - 0.1) return;
        
        if (transform.position.y > this.goalPos.y + 0.1
            && transform.position.y < this.goalPos.y - 0.1) return;
        
        this.Finish();
    }

    protected virtual void FinishingImediately()
    {
        if (!this.user1.Value.CanFinishMove(this)) return;
        this.Finish();
    }
    
    protected virtual void Finish()
    {
        this.isReachGoal = true;
        this.randomCD.ResetStatus();
    }
}
