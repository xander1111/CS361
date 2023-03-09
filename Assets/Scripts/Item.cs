using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private const float FallSpeed = 2f;
    
    protected abstract void Collect();
    
    private void FixedUpdate()
    {
        Transform tsf = transform;
        tsf.position += new Vector3(0, -1 * FallSpeed, 0) * Time.deltaTime;
        
        // ReSharper disable once Unity.InefficientPropertyAccess
        float minVisibleY = tsf.position.y - tsf.lossyScale.y / 2;
        if (minVisibleY < GameManager.minVisibleY) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.transform.CompareTag("BedCollider"))
        {
            Collect();
            Destroy(gameObject);
        }
    }
}
