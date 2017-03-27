using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrailGroup : MonoBehaviour
{
    public Vector3 FollowPos;

    private Trail[] _trails;

    private EdgeCollider2D _edge;
    private Vector2[] _points;
    private bool _stopped;

    public Vector3[] Positions
    {
        get { return _trails == null ? null : _trails.Select(t => t.transform.position).ToArray(); }
    }

    public void Init(Trail prefab, Vector3 startPos, int trailCount, float fastestSpeed, float slowestSpeed, Vector3[] positions)
    {
        FollowPos = startPos;

        var speedDifference = fastestSpeed - slowestSpeed;
        var stepDifference = speedDifference / (trailCount - 1);

        _trails = new Trail[trailCount];

        for (var i = 0; i < trailCount; i++)
        {
            var trail = Instantiate(prefab, positions != null ? positions[i] : startPos, Quaternion.identity);
            trail.Follow.Speed = fastestSpeed - stepDifference * i;
            trail.transform.SetParent(transform);

            _trails[i] = trail;
        }

        _trails[0].AddColliders();
        _trails[0].TrailRenderer.numCornerVertices = 8;

        _trails.Last().AddColliders();
        _trails.Last().TrailRenderer.numCornerVertices = 8;

        _edge = gameObject.AddComponent<EdgeCollider2D>();
        _points = new Vector2[trailCount];
    }

    public void Stop()
    {
        _stopped = true;

        foreach (var trail in _trails)
            trail.Stop();
    }

    private void Update()
    {
        if (_trails == null || _stopped)
            return;

        foreach (var trail in _trails)
            trail.Follow.FollowPos = FollowPos;

        for (var i = 0; i < _trails.Length; i++)
            _points[i] = _trails[i].transform.localPosition;

        _edge.points = _points;

        if (_trails.Any(t => t.IsVisible))
            return;

        Destroy(gameObject);
    }
}
