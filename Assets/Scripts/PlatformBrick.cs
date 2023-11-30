using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlatformBrick : GameUnit
{
    public UnityEvent<PlatformBrick> OnDespawn;

    private void Awake()
    {
        OnDespawn.AddListener(Despawn);
    }

    public void Despawn(PlatformBrick _brick)
    {
        SimplePool.Despawn(_brick);
    }

    [SerializeField] Bricks brick;


    private void OnTriggerEnter(Collider other)
    {
        BrickCarrier brickCarrier = other.gameObject.GetComponent<BrickCarrier>();
        if (brick.brickType.Equals(BrickType.Drop) || brickCarrier.GetId().Equals(brick.brickType))
        {
            if (brickCarrier.CanGetBlock)
            {
                OnDespawn.Invoke(this);
                brickCarrier.AddBrick();
            }
        }
    }
    //Cần kiểm tra hiệu năng của trigger này
    private void OnTriggerStay(Collider other)
    {
        BrickCarrier brickCarrier = other.gameObject.GetComponent<BrickCarrier>();
        if (brick.brickType.Equals(BrickType.Drop) || brickCarrier.GetId().Equals(brick.brickType))
        {
            if (brickCarrier.CanGetBlock)
            {
                OnDespawn.Invoke(this);
                brickCarrier.AddBrick();
            }
        }
    }


    public void SetBrick(Bricks _brick)
    {
        brick = _brick;
        GetComponent<MeshRenderer>().material = brick.material;
    }

    public BrickType GetId()
    {
        return brick.brickType;
    }
}
