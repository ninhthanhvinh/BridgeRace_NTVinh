using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] BrickSpawner nextCompSpawner;
    [SerializeField] int nextCompID;
    [SerializeField]
    Transform[] goalPositions;
    [SerializeField] GameObject gate;

    private void OnTriggerExit(Collider other)
    {
        
        if (other.TryGetComponent<BrickCarrier>(out var brickCarrier))
        {
            nextCompSpawner.gameObject.SetActive(true);
            gate.SetActive(true);
            nextCompSpawner.AddBrick(brickCarrier.GetBricks());
        }
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.CurrentComponent = nextCompID;
            enemy.BrickSpawner = nextCompSpawner;
            enemy.GoalPosition = GetRandomGoal();
        }
    }

    private Vector3 GetRandomGoal()
    {
        var randomIndex = Random.Range(0, goalPositions.Count());
        return goalPositions[randomIndex].position;
    }

    private void OnDisable()
    {
        gate.SetActive(false);
    }
}
