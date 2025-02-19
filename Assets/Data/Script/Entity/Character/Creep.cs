using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : Character, IMoveRandomly, IDespawnByHealth, IShootSkill, IDetectOnStay, 
    IObjHolder
{
    //==========================================Variable==========================================
    [Header("=====Creep=====")]
    [SerializeField] protected Movement movement;
    [SerializeField] protected List<Despawner> despawners;
    [SerializeField] protected Skill skill;
    [SerializeField] protected Detector detector;
    [SerializeField] protected ObjHolder objHolder;

    //===========================================Unity============================================
    public override void LoadComponents()
    {
        base.LoadComponents();
        // Component
        this.LoadComponent(ref this.movement, transform.Find("Move"), "LoadMovement()");
        this.movement.User = this;
        this.LoadComponent(ref this.despawners, transform.Find("Despawn"), "LoadDespawners()");
        foreach (Despawner despawner in this.despawners) despawner.User = this;
        this.LoadComponent(ref this.skill, transform.Find("Skill"), "LoadSkill()");
        this.skill.User = this;
        this.LoadComponent(ref this.detector, transform.Find("Detector"), "LoadDetector()");
        this.detector.User = this;
        this.LoadComponent(ref this.objHolder, transform.Find("Hold"), "LoadObjHolder()");
        this.objHolder.User = this;

        // MoveRandomly
        if (this.movement is MoveRandomly moveRandomly) moveRandomly.User1 = this;

        // ShootSkill
        if (this.skill is ShootSkill shootSkill) shootSkill.User1 = this;

        // DespawnByHealth
        foreach (Despawner despawner in this.despawners)
        {
            if (despawner is not DespawnByHealth despawnByHealth) continue; 
            despawnByHealth.User1 = this;
        }

        // DetectByCollide
        if (this.detector is DetectByCollide detectByCollide) detectByCollide.User1 = this;

        // DetectOnStay
        if (this.detector is DetectOnStay detectOnStay) detectOnStay.User2 = this;
    }



    //============================================================================================
    //==========================================Movement==========================================
    //============================================================================================

    //=========================================IMovement==========================================
    bool IMovement.CanMove(Movement component)
    {
        return this.damageRecv.IsDamage == false;
    }

    Rigidbody2D IMovement.GetRb(Movement component)
    {
        return this.rb;
    }

    //=======================================IMoveRandomly========================================
    bool IMoveRandomly.CanFinishMove(MoveRandomly component)
    {
        return this.damageRecv.IsDamage;
    }



    //============================================================================================
    //=========================================Despawners=========================================
    //============================================================================================

    //=========================================IDespawner=========================================
    bool IDespawner.CanDespawn(Despawner component)
    {
        return true;
    }

    Transform IDespawner.GetDespawnObj(Despawner component)
    {
        return transform;
    }

    Spawner IDespawner.GetSpawner(Despawner component)
    {
        return CreepSpawner.Instance;
    }

    //======================================IDespawnByHealth======================================
    int IDespawnByHealth.GetCurrHealth(DespawnByHealth component)
    {
        return this.health;
    }



    //============================================================================================
    //===========================================Skill============================================
    //============================================================================================

    //===========================================ISkill===========================================
    bool ISkill.CanUseSkill(Skill component)
    {
        if (this.detector.Target == null) return false;
        if (this.health <= 0) return false;
        return true;
    }

    bool ISkill.CanRechargeSkill(Skill component)
    {
        return true;
    }

    void ISkill.ConsumePower(Skill component)
    {
        this.health -= component.HealthCost;
    }

    //========================================IShootSkill=========================================
    int IShootSkill.GetShootState(ShootSkill component)
    {
        // Normal Shot
        if (this.skill is NormalShot normalShot)
        {
            if (this.detector.Target != null) return 1;
            return 0;
        }

        // Charge Shot
        if (this.skill is ChargeShot chargeShot)
        {
            foreach (Chargement chargement in component.Bullet.Chargements)
            {
                if (chargement.IsFullyCharge) continue;
                return 2;
            }

            return 0;
        }

        return 0;
    }



    //============================================================================================
    //==========================================Detector==========================================
    //============================================================================================

    //=========================================IDetector==========================================
    bool IDetector.CanDetect(Detector component)
    {
        return true;
    }

    //======================================IDetectByCollide======================================
    Transform IDetectByCollide.GetOwner(DetectByCollide component)
    {
        return transform;
    }

    //=======================================IDetectOnStay========================================
    bool IDetectOnStay.CanRecharge(DetectOnStay component)
    {
        return true;
    }



    //============================================================================================
    //=========================================ObjHolder==========================================
    //============================================================================================

    //=========================================IObjHolder=========================================
    bool IObjHolder.CanHold(ObjHolder component)
    {
        return true;
    }

    Vector2 IObjHolder.GetMainObjPos(ObjHolder component)
    {
        return transform.position;
    }

    Vector2 IObjHolder.GetTargetPos(ObjHolder component)
    {
        if (this.detector.Target == null) return Vector2.zero;
        return this.detector.Target.transform.position;
    }

    Transform IObjHolder.GetHoldObj(ObjHolder component)
    {
        if (this.skill is not ShootSkill shootSkill) return null;
        return shootSkill.FirePoint;
    }
}
