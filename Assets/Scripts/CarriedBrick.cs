using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriedBrick : GameUnit
{
    [SerializeField] private Bricks brickType;

    public void SetBrickType(Bricks brickType)
    {
        this.brickType = brickType;
        GetComponent<MeshRenderer>().material = brickType.material;
    }
}
