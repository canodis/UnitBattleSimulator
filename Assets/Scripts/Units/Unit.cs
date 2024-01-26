using UnityEngine;

public abstract class Unit : GridObject, IMovable, ISelectable, IAttackable
{
    [field: SerializeField] public int Id { get; set; }
    [field: SerializeField] protected float attackDamage;
    [field: SerializeField] public float attackSpeed;
    [field: SerializeField] protected float speed;

    private UnitStateManager _stateManager = new();
    private Animator _animator;
    private GameObject _unitBody;

    protected void Start()
    {
        _animator = GetComponent<Animator>();
        _unitBody = transform.Find("Body").gameObject;
    }

    public bool Attack(Vector3Int targetPosition)
    {
        GridObject target = GameManager.Instance.objectManager.GetGridObjectWithPosition(targetPosition);
        if (target != null)
        {
            RotateToMoveDirection(gridPosition, targetPosition);
            _animator.Play("Attack");
            target.TakeDamage(attackDamage);
            return true;
        }
        return false;
    }

    public void DestroySelf()
    {
        GameManager.Instance.gridData.DestroyObject(gridPosition, objectData.Size);
        GameManager.Instance.objectManager.DestroyObject(index);
    }


    public void Move(Vector3Int targetPosition)
    {
        _stateManager.GenerateNewState(gridPosition, targetPosition, this);
    }

    public void Update()
    {
        _stateManager.UpdateState(gridPosition);
    }

    public void OnDeselect() { }
    public void OnSelect() { }

    public void RotateToMoveDirection(Vector3Int oldPosition, Vector3Int newPosition)
    {
        float xScale = (newPosition.x > oldPosition.x) ? 1f : -1f;
        _unitBody.transform.localScale = new Vector3(xScale, 1f, 1f);
    }

    public void PlayAnimation(string animationName)
    {
        _animator.Play(animationName);
    }
}