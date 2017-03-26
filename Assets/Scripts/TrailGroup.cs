using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrailGroup : MonoBehaviour
{
    public Vector3 FollowPos;

    private Trail[] _trails;

    private EdgeCollider2D _edge;
    private Vector2[] _points;

    public void Init(Trail prefab, Vector3 startPos, int trailCount, float fastestSpeed, float slowestSpeed)
    {
        FollowPos = startPos;

        var speedDifference = fastestSpeed - slowestSpeed;
        var stepDifference = speedDifference / (trailCount - 1);

        _trails = new Trail[trailCount];

        for (var i = 0; i < trailCount; i++)
        {
            var trail = Instantiate(prefab, startPos, Quaternion.identity);
            trail.Mover.Speed = fastestSpeed - stepDifference * i;
            trail.transform.SetParent(transform);

            _trails[i] = trail;
        }

        _trails[0].AddColliders();
        _trails.Last().AddColliders();

        _edge = gameObject.AddComponent<EdgeCollider2D>();
        _points = new Vector2[trailCount];
    }

    private void Update()
    {
        if (_trails == null)
            return;

        foreach (var trail in _trails)
            trail.Mover.FollowPos = FollowPos;

        for (var i = 0; i < _trails.Length; i++)
            _points[i] = _trails[i].transform.localPosition;

        _edge.points = _points;

        if (_trails.Any(t => t.IsVisible))
            return;

        Destroy(gameObject);
    }
}
