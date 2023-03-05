using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private float _timeBeforeDespawn;
    private float _timeSpawned;
    
    protected abstract void Collect();

    private void OnEnable()
    {
        _timeBeforeDespawn = 10f;
        _timeSpawned = Time.time;
    }

    private void FixedUpdate()
    {
        if (Time.time - _timeSpawned > _timeBeforeDespawn) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Superclass Collided");
        if (other.transform.CompareTag("BedCollider")) Collect();
    }
}
