using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDoor : MonoBehaviour
{
    Rigidbody rigidbody;

    [SerializeField] bool startLockedState;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        Lock(startLockedState);
    }

    private void Lock(bool doorLock)
    {
        rigidbody.freezeRotation = doorLock;

    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
