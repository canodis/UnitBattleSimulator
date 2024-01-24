using UnityEngine;

public class Soldier : Unit, IMovable, ISelectable
{
    public void Move(Vector3Int targetPosition)
    {
        Debug.Log("I am too lazy to move");
    }

    public void OnDeselect()
    {
    }

    public void OnSelect()
    {
    }
}