using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairBrick : MonoBehaviour
{
    [SerializeField] Bricks brick;
    [SerializeField] Collider wayBlock;

    public Bricks GetBricks() => brick;
    public void SetBricks(Bricks bricks)
    {
        brick = bricks;
        GetComponent<MeshRenderer>().material = brick.material;
        wayBlock.gameObject.SetActive(false);
    }

    private void Start()
    {
        GetComponent<MeshRenderer>().material = brick.material;
    }

    public void NoBrickHandle()
    {
        wayBlock.gameObject.SetActive(true);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if ( other.TryGetComponent<BrickCarrier>(out var brickCarrier))
    //    {
    //        if (brick.id == brickCarrier.GetId())
    //        {
    //            return;
    //        }
    //        else
    //        {
    //            if (brickCarrier.GetBrickCount() > 0)
    //            {
    //                wayBlock.enabled = false;
    //                //brick.id = brickCarrier.GetId();
    //                brick = brickCarrier.GetBricks();
    //                GetComponent<MeshRenderer>().material = brick.material;
    //                brickCarrier.DropBrick();
    //            }
    //            else
    //            {
    //                wayBlock.enabled = true;
    //            }
    //        }
    //    }
    //}
}
