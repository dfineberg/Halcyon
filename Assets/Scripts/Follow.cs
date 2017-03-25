using UnityEngine;

public class Follow : MonoBehaviour
{
    public float Speed;
    public Vector3 FollowPos;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, FollowPos, Speed * Time.deltaTime);
    }
}
