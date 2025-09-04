using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping = 0.5f;

    public Transform target;
    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z;
        targetPosition.y = transform.position.y;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);

        if (target.position.y > transform.position.y - 5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        }

        if (target.position.y < transform.position.y - 5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
        }
    }
}
