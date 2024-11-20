using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDoor : MonoBehaviour
{
    new Rigidbody rigidbody;

    [SerializeField] bool startLockedState;

    public bool isLocked;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        Lock(startLockedState);
    }

    public void Lock(bool doorLock)
    {
        isLocked = doorLock;
        rigidbody.freezeRotation = doorLock;
        //Debug.Log(doorLock);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
