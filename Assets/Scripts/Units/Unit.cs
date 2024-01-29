using UnityEngine;

/// <summary>
/// Base abstract class for all units.
/// </summary>
public abstract class Unit : GridObject, ISelectable
{
    [field: SerializeField] public int Id { get; set; }
    protected GameObject _unitBody;
    protected Animator _animator;

    protected virtual void Start()
    {
        _unitBody = transform.Find("Body").gameObject;
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string animationName)
    {
        _animator.Play(animationName);
    }

    public void OnDeselect() { }
    public void OnSelect() { }
}