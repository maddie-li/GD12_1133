using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Dictionary<Direction, int> _rotationByDirection = new()
    {
        { Direction.north, 0 },
        { Direction.east, 90 },
        { Direction.south, 180 },
        { Direction.west, 270 },
    };

    private Direction _facingDirection;

    public void Setup()
    {
        _facingDirection = Direction.north;
        SetFacingDirection();
    }

    private void SetFacingDirection()
    {
        Vector3 facing = transform.rotation.eulerAngles;
        // set y
        facing.y = _rotationByDirection[_facingDirection];

        // save rotation as quaternion
        transform.rotation = Quaternion.Euler(facing);
    }

    private void Update()
    {
        bool rotateLeft = Input.GetKeyDown(KeyCode.A);
        bool rotateRight = Input.GetKeyDown(KeyCode.D);

        if (rotateLeft && !rotateRight)
        {
            Debug.Log("Left");
            TurnLeft();
        }
        else if (rotateRight && !rotateLeft)
        {
            Debug.Log("Right");
            TurnRight();
        }
        SetFacingDirection();
    }

    private void TurnLeft()
    {
        switch (_facingDirection)
        {
            case Direction.north:
                _facingDirection = Direction.west;
                break;
            case Direction.south:
                _facingDirection = Direction.east;
                break;
            case Direction.east:
                _facingDirection = Direction.north;
                break;
            case Direction.west:
                _facingDirection = Direction.south;
                break;
        }
    }

    private void TurnRight()
    {
        switch (_facingDirection)
        {
            case Direction.north:
                _facingDirection = Direction.east;
                break;
            case Direction.south:
                _facingDirection = Direction.west;
                break;
            case Direction.east:
                _facingDirection = Direction.south;
                break;
            case Direction.west:
                _facingDirection = Direction.north;
                break;
        }
    }

}
