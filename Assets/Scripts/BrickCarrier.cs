using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickCarrier : MonoBehaviour
{
    [SerializeField] Transform brickContainer;
    List<CarriedBrick> bricks = new();
    [SerializeField] GameUnit brickPrefab;
    [SerializeField] Bricks holdBrick;
    [SerializeField] Transform raycastOrigin;
    [SerializeField] LayerMask stairLayer;

    BrickSpawner spawner;

    int brickCount = 0;
    bool canGetBlock = true;
    bool isOnBridge = false;

    public bool CanGetBlock { get => canGetBlock; set => canGetBlock = value; }
    public BrickType GetId() => holdBrick.brickType;
    public BrickSpawner Spawner { get => spawner; set => spawner = value; }
    public Bricks GetBricks() => holdBrick;
    public int GetBrickCount() => brickCount;

    public void AddBrick()
    {
        var pos = brickContainer.position + new Vector3(0f, brickCount * 0.5f, 0f);
        CarriedBrick brick = SimplePool.Spawn<CarriedBrick>(PoolType.CarriedBrick, Vector3.zero, Quaternion.identity);
        brick.transform.SetParent(brickContainer);
        brick.transform.rotation = brickContainer.rotation;
        brick.SetBrickType(holdBrick);
        brick.transform.position = pos;
        bricks.Add(brick);
        brickCount++;
    }

    private bool IsBrickEmpty()
    {
        return brickCount == 0;
    }

    public void DropBrick()
    {
        if (bricks.Count > 0)
        {
            var brick = bricks[^1];
            bricks.RemoveAt(bricks.Count - 1);
            brickCount--;
            SimplePool.Despawn(brick);
        }
    }

    private void Update()
    {
        GetOnStair();
    }

    private void OnEnable()
    {
        ResetCharacter();
    }

    private void GetOnStair()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, transform.forward, out hit, 1.5f, stairLayer))
        {
   
            if (hit.collider.CompareTag("Stair"))
            {
                var stair = hit.collider.GetComponent<StairBrick>();
                if (stair.GetBricks().brickType != holdBrick.brickType)
                {
                    if (IsBrickEmpty())
                    {
                        stair.NoBrickHandle();
                        return;
                    }
                    stair.SetBricks(holdBrick);
                    DropBrick();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<BrickCarrier>(out var brickCarrier))
        {
            Debug.Log("Hit");
            if (GetBrickCount() < brickCarrier.GetBrickCount())
            {
                canGetBlock = false;
                brickCarrier.canGetBlock = false;
                StartCoroutine(RenewGetBlock(1f));
                StartCoroutine(brickCarrier.RenewGetBlock(.5f));
                OnFalling();
            }
        }
    }

    private void OnFalling()
    {
        canGetBlock = false;
        switch (this.tag)
        {
            case "Player":
                StartCoroutine(GetComponent<PlayerMovement>().Fall());
                break;
            case "Enemy":
                GetComponent<Enemy>().ChangeState(Vinh.StateID.Falling);
                break;
        }
        foreach (var brick in bricks)
        {
            SimplePool.Despawn(brick);
            spawner.SpawnWhenFall(transform.position);
        }
        brickCount = 0;
        bricks.Clear();
    }

    public IEnumerator RenewGetBlock(float countDownTime)
    {
        yield return new WaitForSeconds(countDownTime);
        canGetBlock = true;
    }

    private void ResetCharacter()
    {
        foreach (var brick in bricks)
        {
            SimplePool.Despawn(brick);
        }
        brickCount = 0;
        bricks.Clear ();
    }
}
