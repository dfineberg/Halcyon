using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(Follow))]
public class Trail : MonoBehaviour
{
    public TrailRenderer TrailRenderer { get; private set; }
    public Follow Mover { get; private set; }

    public bool HasColliders;

    private EdgeCollider2D _edge1;
    private EdgeCollider2D _edge2;

    private List<Vector2> _points1;
    private List<Vector2> _points2;

    public bool IsVisible
    {
        get { return TrailRenderer.isVisible; }
    }

    private void Awake()
    {
        TrailRenderer = GetComponent<TrailRenderer>();
        Mover = GetComponent<Follow>();
    }

    public void AddColliders()
    {
        var endCapCollider = gameObject.AddComponent<CircleCollider2D>();
        endCapCollider.radius = TrailRenderer.startWidth / 2f;

        _edge1 = gameObject.AddComponent<EdgeCollider2D>();
        //_edge2 = gameObject.AddComponent<EdgeCollider2D>();

        _points1 = new List<Vector2> { transform.position };
        _points2 = new List<Vector2> { transform.position };

        StartCoroutine(ColliderRoutine(_edge1, _points1, 0f));
        //StartCoroutine(ColliderRoutine(_edge2, _points2, -(TrailRenderer.startWidth / 2f)));
    }

    private IEnumerator ColliderRoutine(EdgeCollider2D edge, List<Vector2> points, float offset)
    {
        var previousPos = points[0];

        while (true)
        {
            yield return null;

            var direction = transform.position - (Vector3)previousPos;
            var cross = Vector3.Cross(direction, Vector3.back).normalized;
            var thisPos = transform.position + cross * offset;

            points.Add(thisPos);

            edge.points = points.ToArray();
            edge.offset = -transform.position;

            previousPos = transform.position;
        }
    }
}
