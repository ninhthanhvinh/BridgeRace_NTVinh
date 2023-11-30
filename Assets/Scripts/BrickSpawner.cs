using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] int layerId;
    public int LayerId { get => layerId; set => layerId = value; }
    [SerializeField] int compId;
    public int CompId { get => compId; set => compId = value; }

    [SerializeField] int maxBrickColumns = 11;
    [SerializeField] int maxBrickRows = 15;

    [SerializeField] Transform brickParent;
    [SerializeField] GameUnit brickPrefab;
    [SerializeField] LayerMask brickLayer;
    [SerializeField] List<Bricks> brickTypes;
    [SerializeField] Bricks dropBrick;
    [SerializeField] float spawnInterval = 5f;
    List<PlatformBrick> bricksOnThisLayer = new List<PlatformBrick>();


    #region constants
    const float BRICK_SIZE_HORIZONTAL = 1.6f;
    const float BRICK_SIZE_VERTICAL = 2.4f;
    const float BRICK_WORLD_HEIGHT = 0.25f;
    #endregion

    float timer = 0f;

    private void Awake()
    {
        SimplePool.Preload(brickPrefab, 100, brickParent);
    }


    private void OnEnable()
    {
        LevelInit();
    }

    private void OnDisable()
    {
        ResetStage();
    }

    private void LevelInit()
    {
        for (int i = 0; i < maxBrickColumns; i++)
        {
            for (int j = 0; j < maxBrickRows; j++)
            {
                Vector3 brickPosition = transform.position +  new Vector3(i * BRICK_SIZE_VERTICAL, 0, j * BRICK_SIZE_HORIZONTAL);
                GenerateBrick(brickPosition);
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            var pos = transform.position + new Vector3(Random.Range(0, maxBrickColumns) * BRICK_SIZE_VERTICAL, 0, Random.Range(0, maxBrickRows) * BRICK_SIZE_HORIZONTAL);
            if (!CheckIfNothingInPosition(pos))
                return;
            if (bricksOnThisLayer.Count < maxBrickColumns * maxBrickRows)
            {
                GenerateBrick(pos);
            }

        }
    }


    private void GenerateBrick(Vector3 _position)
    {
        var brick = SimplePool.Spawn<PlatformBrick>(PoolType.PlatformBrick, Vector3.zero, Quaternion.identity);
        brick.SetBrick(GetRandomBrick());
        bricksOnThisLayer.Add(brick);
        brick.OnDespawn.AddListener(BrickGetDespawn);
        brick.transform.SetParent(this.transform);
        brick.transform.position = _position;
    }

    public bool CheckIfNothingInPosition(Vector3 _position)
    {
        Collider[] colliders = Physics.OverlapSphere(_position, 0.1f, brickLayer);
        if (colliders.Length > 0)
        {
            return false;
        }
        return true;
    }

    private Bricks GetRandomBrick()
    {
        var randomIndex = Random.Range(0, brickTypes.Count);
        return brickTypes[randomIndex];
    }

    public Vector3 GetRandomPosition()
    {
        Vector3 pos;
        do
        {
            pos = transform.position + new Vector3(Random.Range(0, maxBrickColumns) * BRICK_SIZE_VERTICAL, 0, Random.Range(0, maxBrickRows) * BRICK_SIZE_HORIZONTAL);
        } while (!CheckIfNothingInPosition(pos));
        return pos;
    }

    private void BrickGetDespawn(PlatformBrick _brick)
    {
        bricksOnThisLayer.Remove(_brick);
    }

    public Vector3 GetBrickPosition(BrickType _id, Vector3 position)
    {
        float minDistance = float.MaxValue;
        int minPosIndex = -1;

        foreach (PlatformBrick brick in bricksOnThisLayer)
        {
            if (brick.GetId() == _id)
            {
                if (Vector3.Distance(brick.transform.position, position) < minDistance)
                {
                    minDistance = Vector3.Distance(brick.transform.position, position);
                    minPosIndex = bricksOnThisLayer.IndexOf(brick);
                }
            }
        }

        if (minPosIndex >= 0) 
            return bricksOnThisLayer[minPosIndex].transform.position;

        var randomIndex = Random.Range(-10, 10);
        var randomIndex2 = Random.Range(-10, 10);

        return Vector3.zero + new Vector3(randomIndex, 0, randomIndex2);
    }

    public void SpawnWhenFall(Vector3 _position)
    {
        
        var brick = SimplePool.Spawn<PlatformBrick>(PoolType.PlatformBrick, Vector3.zero, Quaternion.identity);
        brick.SetBrick(dropBrick);
        bricksOnThisLayer.Add(brick);
        brick.OnDespawn.AddListener(BrickGetDespawn);
        brick.transform.SetParent(transform);
        var randomIndexX = Random.Range(-3, 3);
        var randomIndexZ = Random.Range(-3, 3);
        brick.transform.position = _position + new Vector3(randomIndexX, BRICK_WORLD_HEIGHT, randomIndexZ); 
    }

    public void AddBrick(Bricks _brick)
    {
        brickTypes.Add(_brick);
    }

    private void ResetStage()
    {
        foreach (PlatformBrick brick in bricksOnThisLayer)
        {
            SimplePool.Despawn(brick);
        }

        bricksOnThisLayer.Clear();
    }
}
