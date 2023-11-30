using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vinh;

public class Goal : MonoBehaviour
{
    public UnityEvent OnLose;
    public UnityEvent OnWin;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                //Lose
                //enemy.stateMachine.ChangeState(StateID.Goal);
                OnLose.Invoke();
            }
        }

        if (other.CompareTag("Player"))
        {
            // Trigger win
            OnWin.Invoke();
        }
    }
}
