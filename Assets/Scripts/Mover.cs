using UnityEngine;

public class Mover : MonoBehaviour
{
    public Vector3 MoveDirection;

    private void Update()
    {
        transform.Translate(MoveDirection * Time.deltaTime);
    }
}
