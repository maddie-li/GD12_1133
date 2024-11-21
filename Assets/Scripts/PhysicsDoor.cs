using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PhysicsDoor : MonoBehaviour
{
    new Rigidbody rigidbody;
    CinemachineCollisionImpulseSource impulse;

    [SerializeField] bool startLockedState;

    public bool isLocked;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        impulse = GetComponent<CinemachineCollisionImpulseSource>();

        Lock(startLockedState);
    }

    public void Lock(bool doorLock)
    {
        isLocked = doorLock;
        rigidbody.freezeRotation = doorLock;
        impulse.enabled = doorLock;
    }
}
