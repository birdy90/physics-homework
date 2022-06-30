using UnityEngine;

public class BallHit : MonoBehaviour
{
    public Vector3 Direction;
    public float Strength;
    
    private float _timeout = 1f;
    private bool _isHitDone;

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Direction);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isHitDone && Time.timeSinceLevelLoad > _timeout)
        {
            _isHitDone = true;
            GetComponent<Rigidbody>().AddForce(Direction.normalized * Strength);
        }
    }
}
