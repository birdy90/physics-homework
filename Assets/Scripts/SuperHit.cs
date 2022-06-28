using UnityEngine;

public class SuperHit : MonoBehaviour
{
    public float Force = 300;
    
    private void OnCollisionEnter(Collision collision)
    {
        var appliedForce = (collision.transform.position - transform.position) * Force;
        collision.gameObject.GetComponent<Rigidbody>().AddForce(appliedForce);
    }
}
