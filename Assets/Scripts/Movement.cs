using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 MoveDirection;

    private void Update()
    {
        transform.Translate(MoveDirection * Time.deltaTime);
    }
}
