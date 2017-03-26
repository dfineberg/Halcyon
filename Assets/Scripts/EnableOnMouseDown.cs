using UnityEngine;

public class EnableOnMouseDown : MonoBehaviour
{

    public Movement TextMover;
    public GameObject[] ParticleSystems;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        TextMover.enabled = true;

        foreach(var p in ParticleSystems)
            p.SetActive(true);
    }
}
