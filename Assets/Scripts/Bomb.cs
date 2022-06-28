using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private List<GameObject> _affectedObjects;
    public float ExplosionMultiplier = 200f;
    public float ExplosionRadius = 6f;

    public ParticleSystem _particleSystem;

    private float _timeout = 2f;
    private bool _exploded = false;

    void Start()
    {
        Debug.Log(Physics.gravity);
        
        _affectedObjects = new List<GameObject>();
        var colliders = new Collider[100];
        var length = Physics.OverlapSphereNonAlloc(Vector3.zero, ExplosionRadius, colliders, 1 << 6);

        for (var i = 0; i < length; i++)
        {
            _affectedObjects.Add(colliders[i].gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_exploded && Time.timeSinceLevelLoad > _timeout)
        {
            Explode();
        }
    }

    void Explode()
    {
        _exploded = true;
        foreach (var brick in _affectedObjects)
        {
            var vector = brick.transform.position - transform.position;
            var body = brick.GetComponent<Rigidbody>();
            var forceToApply = vector.normalized * ExplosionMultiplier / Mathf.Pow(vector.magnitude, 2);
            body.AddForce(forceToApply);
            gameObject.SetActive(false);
            _particleSystem.Play();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
