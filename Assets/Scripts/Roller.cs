using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Roller
{
    System.Random random = new System.Random();
    public int Roll(int sides)
    {
        // because range should include highest number
        sides += 1;

        return random.Next(1, sides);

    }
}
