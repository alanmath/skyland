using UnityEngine;

public class CylinderController : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation; 

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (transform.position.y < 0)
        {
            transform.position = startPosition;
            
            transform.rotation = startRotation;

            
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; 
                rb.angularVelocity = Vector3.zero; 
            }
        }
    }
}
