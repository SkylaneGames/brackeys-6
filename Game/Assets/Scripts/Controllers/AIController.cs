using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : UnitController
{
    protected NavMeshAgent NavAgent { get; private set; }

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

    private IEnumerable<UnitController> VisibleAllies => _vision.VisibleUnits.Where(p => p?.Faction == Faction);
    private IEnumerable<AIController> NetworkedUnits => VisibleAllies.Where(p => p is AIController).Select(p => (AIController)p);
    public IEnumerable<UnitController> VisibleEnemies => _vision?.VisibleUnits?.Where(p => p?.Faction != Faction);

    public bool EnemyInSight { get; private set; }

    private bool _underfire;
    private Vector3 _lastAttackDirection;

    private UnitController ClosestEnemy => VisibleEnemies?.OrderBy(p => (p.transform.position - transform.position).sqrMagnitude).FirstOrDefault();

    protected override void Awake()
    {
        base.Awake();

        _vision = GetComponentInChildren<VisionSystem>();
        NavAgent = GetComponent<NavMeshAgent>();
    }

    protected override void Start()
    {
        base.Start();

        DamageSystem.Damaged += OnDamaged;
    }

    private void OnDamaged(object sender, DamagedEventArgs e)
    {
        _underfire = true;
        _lastAttackDirection = e.DirectionOfAttack;
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
            EnemyInSight = true;
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

                _aim.Target = transform.position + transform.forward * 3f;
                var target = ClosestEnemy.transform.position;

                NavAgent.isStopped = false;
                NavAgent.SetDestination(target);
            }
        }
        else
        {
            EnemyInSight = false;
            StopFiring();

            _aim.Target = transform.position + transform.forward * 3f;

            if (_underfire && _lastAttackDirection != Vector3.zero)
            {
                NavAgent.SetDestination(transform.position + _lastAttackDirection * 10f);
            }
            else
            {
                var target = NetworkedUnits.FirstOrDefault(p => p.EnemyInSight);

                if (target != null)
                {
                    NavAgent.isStopped = false;
                    NavAgent.SetDestination(target.transform.position);
                }
            }
        }
    }

    private void Attack(Vector3 target)
    {
        Fire();
    }

    private void OnDrawGizmos()
    {
        if (ClosestEnemy != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, ClosestEnemy.transform.position);
        }
        //Gizmos.color = Color.red;
        //foreach (var unit in VisibleEnemies)
        //{
        //    if (unit == null) continue;
        //    Gizmos.DrawLine(transform.position, unit.transform.position);
        //}

        //Gizmos.color = Color.cyan;
        //foreach (var unit in VisibleAllies)
        //{
        //    if (unit == null) continue;
        //    Gizmos.DrawLine(transform.position, unit.transform.position);
        //}

        //Gizmos.color = Color.green;
        //foreach (var unit in NetworkedUnits)
        //{
        //    if (unit == null) continue;
        //    Gizmos.DrawLine(transform.position, unit.transform.position);
        //}
    }
}
