using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class VisionSystem : MonoBehaviour
{
    public ISet<UnitController> VisibleUnits => _unitsInView;

    private ISet<UnitController> _unitsInView = new HashSet<UnitController>();
    private ISet<UnitController> _unitsInRange = new HashSet<UnitController>();

    private UnitController _parent;

    [SerializeField]
    private bool _showDebug;

    private void Awake()
    {
        _parent = GetComponentInParent<UnitController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        UpdateVisibleUnits();
    }

    private void UpdateVisibleUnits()
    {
        foreach (var unit in _unitsInRange)
        {
            //if (unit == null)
            //{
            //    continue;
            //}

            if (Physics.Raycast(transform.position, unit.transform.position - transform.position, out var hit, 300f, LayerMask.GetMask("Default", "Units")))
            {
                if (_showDebug)
                {
                    Debug.Log($"Checking line of sight for '{unit.name}'. Hit {hit.collider.name}");
                }

                var lineOfSight = hit.collider.GetComponent<UnitController>() == unit;

                if (lineOfSight)
                {
                    _unitsInView.Add(unit);
                }
                else
                {
                    _unitsInView.Remove(unit);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (var unit in _unitsInView)
        {
            Gizmos.DrawLine(transform.position, unit.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var unit = other.GetComponent<UnitController>();

        if (unit != null && unit != _parent)
        {
            //Debug.Log($"'{other.name}' entered Vision Sphere");
            _unitsInRange.Add(unit);
            unit.Destroyed += UnitDestroyed;
        }
    }

    private void UnitDestroyed(object sender, System.EventArgs e)
    {
        var unit = sender as UnitController;

        _unitsInRange.Remove(unit);
        _unitsInView.Remove(unit);
    }

    private void OnTriggerExit(Collider other)
    {
        var unit = other.GetComponent<UnitController>();

        if (unit != null)
        {
            _unitsInRange.Remove(unit);
            _unitsInView.Remove(unit);
            unit.Destroyed -= UnitDestroyed;
        }
    }
}
