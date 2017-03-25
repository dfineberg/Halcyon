using UnityEngine;

public class TrailController : MonoBehaviour {

	public int TrailCount;
    public float FastestSpeed;
    public float SlowestSpeed;

    private Trail _trailPrefab;
    private Trail[] _trails;

    private float SpeedDifference
    {
        get { return FastestSpeed - SlowestSpeed; }
    }

    private float StepDifference
    {
        get { return SpeedDifference / (TrailCount - 1); }
    }

    private static bool IsFollowing
    {
        get { return Input.GetMouseButton(0); }
    }

    private static Vector3 FollowPos
    {
        get
        {
            var screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f);

            return Camera.main.ScreenToWorldPoint(screenPos);
        }
    }

    private void Awake()
    {
        _trailPrefab = Resources.Load<Trail>("Trail");
    }

    private void Update()
    {
        if (IsFollowing)
        {
            var followPos = FollowPos;

            if (_trails == null)
                InitTrails(followPos);

            foreach (var trail in _trails)
                trail.Follow.FollowPos = followPos;
        }
        else
            _trails = null;
    }

    private void InitTrails(Vector3 startPos)
    {
        _trails = new Trail[TrailCount];

        for (var i = 0; i < TrailCount; i++)
        {
            var trail = Instantiate(_trailPrefab, startPos, Quaternion.identity);
            trail.Follow.Speed = FastestSpeed - StepDifference * i;

            _trails[i] = trail;
        }
    }
}
