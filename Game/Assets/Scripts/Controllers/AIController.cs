using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : UnitController
{
    protected NavMeshAgent NavAgent { get; private set;}

    private VisionSystem _vision;

    [SerializeField]
    private AimTarget _aim;

    [SerializeField]
    [Range(0f, 5f)]
    private float _reactionTime = 0.25f;

    [SerializeField]
    [Range(0f, 2f)]
    private float _targetLeadScale = 0.5f;
    
    private float _timeSinceLastAction;

    public override Vector3 Velocity => NavAgent.velocity;

    private IEnumerable<UnitController> VisibleAllies => _vision.VisibleUnits.Where(p => p.Faction == Faction);
    private IEnumerable<UnitController> VisibleEnemies => _vision.VisibleUnits.Where(p => p.Faction != Faction);
    private UnitController ClosestEnemy => VisibleEnemies.OrderBy(p => (p.transform.position - transform.position).sqrMagnitude).FirstOrDefault();

    protected override void Awake()
    {
        base.Awake();

        _vision = GetComponentInChildren<VisionSystem>();
        NavAgent = GetComponent<NavMeshAgent>();
    }

    protected override void Update()
    {
        base.Update();

        _timeSinceLastAction += Time.deltaTime;
        if (_timeSinceLastAction > _reactionTime)
        {
            _timeSinceLastAction = 0f;
            DecisionUpdate();
        }
    }

    protected virtual void DecisionUpdate()
    {
        if (ClosestEnemy != null)
        {
            // If within firing range and has weapons available
            var distanceTo = (ClosestEnemy.transform.position - transform.position).magnitude;
            if (distanceTo <= Weapons.MaxRange && distanceTo >= Weapons.MinRange)
            {
                NavAgent.isStopped = true;
                // Look at
                _aim.Target = ClosestEnemy.transform.position + ClosestEnemy.Velocity * _targetLeadScale;

                if (Weapons.CanFire && (_aim.transform.position - ClosestEnemy.transform.position).magnitude < 3f)
                {
                    Attack(ClosestEnemy.transform.position);
                }
            }
            else
            {
                StopFiring();

                var target = ClosestEnemy.transform.position;

                NavAgent.isStopped = false;
                NavAgent.SetDestination(target);
            }
        }
        else
        {
            StopFiring();
        }
    }

    private void Attack(Vector3 target)
    {
        Fire();
    }
}
