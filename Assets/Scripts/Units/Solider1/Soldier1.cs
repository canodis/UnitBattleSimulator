using UnityEngine;

public class Soldier1 : Unit, IMovable
{
    public void Move(Vector3Int targetPosition)
    {
        Debug.Log("I am too lazy to move");
    }
}