using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BricksGenerator : MonoBehaviour
{
    
    public GameObject BrickPrefab;

    public float BrickCreationInterval = 1f;

    private float _nextBrickCreationTime;
    
    private List<GameObject> _bricks = new List<GameObject>();

    void Start()
    {
        _nextBrickCreationTime = Time.time + BrickCreationInterval;
    }
    
    void Update()
    {
        if (Time.time > _nextBrickCreationTime)
        {
            _nextBrickCreationTime += BrickCreationInterval;
            var brick = Instantiate(BrickPrefab);
            brick.transform.position = new Vector3(Random.Range(-1f, 1f), transform.position.y, transform.position.z);
            brick.transform.rotation = new Quaternion(
                Random.Range(-1f, 1f), 
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f), 
                Random.Range(-1f, 1f)
            );
            _bricks.Add(brick);
        }

        var itemsToDestroy = _bricks.Where(t => t.transform.position.y < -20).ToList();
        if (itemsToDestroy.Count > 0)
        {
            _bricks = _bricks.Where(t => t.transform.position.y >= -20).ToList();
            foreach (var item in itemsToDestroy)
            {
                Destroy(item);
            }
        }
    }
}
