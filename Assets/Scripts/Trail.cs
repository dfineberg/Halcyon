using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(Follow))]
public class Trail : MonoBehaviour
{
    public TrailRenderer TrailRenderer { get; private set; }
    public Follow Follow { get; private set; }

    public bool HasColliders;

    private EdgeCollider2D _edge;

    private List<Vector2> _points;

    private bool _stopped;

    public bool IsVisible
    {
        get { return TrailRenderer.isVisible; }
    }

    private void Awake()
    {
        TrailRenderer = GetComponent<TrailRenderer>();
        Follow = GetComponent<Follow>();
    }

    public void AddColliders()
    {
        //var endCapCollider = gameObject.AddComponent<CircleCollider2D>();
        //endCapCollider.radius = TrailRenderer.startWidth / 2f;

        _edge = gameObject.AddComponent<EdgeCollider2D>();

        _points = new List<Vector2> { transform.position };

        StartCoroutine(ColliderRoutine(_edge, _points));
    }

    public void Stop()
    {
        Follow.FollowPos = transform.position;
        _stopped = true;
    }

    private IEnumerator ColliderRoutine(EdgeCollider2D edge, List<Vector2> points)
    {
        while (!_stopped)
        {
            yield return null;
            yield return null;

            points.Add(transform.position);

            edge.points = points.ToArray();
            edge.offset = -transform.position;
        }
    }
}
