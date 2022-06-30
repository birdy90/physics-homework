using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private List<GameObject> _affectedObjects;
    public float ExplosionMultiplier = 200f;
    public float ExplosionRadius = 6f;

    public bool UseLayer = true;
    public int LayerNumber = 6; // layer for bricks
    public ParticleSystem ParticleSystem;

    private float _timeout = 2f;
    private bool _exploded = false;

    void Start()
    {
        _affectedObjects = new List<GameObject>();
        var colliders = new Collider[100];

        var length = UseLayer 
            ? Physics.OverlapSphereNonAlloc(Vector3.zero, ExplosionRadius, colliders, 1 << 6) 
            : Physics.OverlapSphereNonAlloc(Vector3.zero, ExplosionRadius, colliders);

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
        foreach (var brick in _affectedObjects)
        {
            var vector = brick.transform.position - transform.position;
            var body = brick.GetComponent<Rigidbody>();
            if (!body) continue;
            var forceToApply = vector.normalized * ExplosionMultiplier / Mathf.Pow(vector.magnitude, 2);
            body.AddForce(forceToApply);
        }
        
        _exploded = true;
        gameObject.SetActive(false);
        ParticleSystem.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
