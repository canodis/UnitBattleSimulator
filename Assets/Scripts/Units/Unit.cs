using UnityEngine;

public abstract class Unit : GridObject
{
    [field : SerializeField] public int Id { get; set; }
    protected float attackDamage; 
}