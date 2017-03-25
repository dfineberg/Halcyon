using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(Follow))]
public class Trail : MonoBehaviour
{
    public TrailRenderer TrailRenderer { get; private set; }
    public Follow Follow { get; private set; }

    private void Awake()
    {
        TrailRenderer = GetComponent<TrailRenderer>();
        Follow = GetComponent<Follow>();
    }
}
