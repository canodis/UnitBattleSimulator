using UnityEngine;

public class Soldier : Unit, IMovable, IAttackable
{
    private SoldierStateManager _stateManager = new();
    [field: SerializeField] protected float attackDamage;
    [field: SerializeField] protected float attackSpeed;
    [field: SerializeField] protected float speed;

    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Attack target specified by targetPosition
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns>If target dead true otherwise false</returns>
    public bool Attack(Vector3Int targetPosition)
    {
        GridObject target = GameManager.Instance.objectManager.GetGridObjectWithPosition(targetPosition);
        if (target != null)
        {
            RotateToDirection(gridPosition, targetPosition);
            _animator.Play("Attack");
            target.TakeDamage(attackDamage);
            return true;
        }
        return false;
    }

    public void Move(Vector3Int targetPosition)
    {
        _stateManager.GenerateMovementState(gridPosition, targetPosition, this);
    }

    public void Update()
    {
        _stateManager.UpdateState(gridPosition);
    }

    public void RotateToDirection(Vector3Int oldPosition, Vector3Int newPosition)
    {
        float xScale = (newPosition.x > oldPosition.x) ? 1f : -1f;
        _unitBody.transform.localScale = new Vector3(xScale, 1f, 1f);
    }

    public void DestroySelf()
    {
        GameManager.Instance.gridData.DestroyObject(gridPosition, objectData.Size);
        GameManager.Instance.objectManager.DestroyObject(index);
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
}
