using UnityEngine;
using System.Collections.Generic;

public class Enemy : Character
{

    
    

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CharState.Walk:
                WalkUpdate();
                break;
        }
    }
}
