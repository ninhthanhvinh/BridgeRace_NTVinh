using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnTheBridge : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IOnStairs>(out var onStairs))
        {
            onStairs.SetOnStairs(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IOnStairs>(out var onStairs))
        {
            onStairs.SetOnStairs(false);
        }
    }
}
