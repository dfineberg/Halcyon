using UnityEngine;

public class TrailController : MonoBehaviour {

	public int TrailCount;
    public float FastestSpeed;
    public float SlowestSpeed;

    private Trail _trailPrefab;
    private TrailGroup _trailGroup;

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

            if (!_trailGroup)
                InitTrailGroup(followPos);

            _trailGroup.FollowPos = FollowPos;
        }
        else
            _trailGroup = null;
    }

    private void InitTrailGroup(Vector3 startPos)
    {
        _trailGroup = new GameObject("TrailGroup").AddComponent<TrailGroup>();

        _trailGroup.Init(_trailPrefab, startPos, TrailCount, FastestSpeed, SlowestSpeed);
    }
}
