using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vinh;

public class Enemy : MonoBehaviour
{ 
    [SerializeField] LayerMask brickLayer;

    [HideInInspector]
    public StateMachine stateMachine;
    [SerializeField]    
    private StateID initialState;

    public StateID currentState;

    BrickSpawner brickSpawner;
    BrickCarrier brickCarrier;
    Animator animator;
    NavMeshAgent navMeshAgent;

    private int bridgeLimit;
    private int currentComponent = 0;
    public int CurrentComponent
    {
        get => currentComponent;
        set => currentComponent = value;
    }
    public BrickSpawner BrickSpawner
    {
        get => brickSpawner;
        set => brickSpawner = value;
    }
    private Vector3 destination;
    [SerializeField]
    private Vector3 goalPosition;
    public Vector3 Destination
    {
        get => destination;
        set => destination = value;
    }
    public Vector3 GoalPosition
    {
        get => goalPosition;
        set => goalPosition = value;
    }
    public int BridgeLimit
    {
        get => bridgeLimit;
        set => bridgeLimit = value;
    }

    //private IState<Enemy> currentState;

    private void Awake()
    {

        brickCarrier = GetComponent<BrickCarrier>();
        //TEST

        navMeshAgent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //ChangeState(new IdleState());
        stateMachine = new StateMachine(this);
        stateMachine.RegisterState(new Vinh.IdleState());
        stateMachine.RegisterState(new Vinh.SeekingState());
        stateMachine.RegisterState(new GettingState());
        stateMachine.RegisterState(new Vinh.FallingState());
        stateMachine.RegisterState(new Vinh.BridgeBuildingState());

        bridgeLimit = Random.Range(10, 20);
    }

    private void OnEnable()
    {
        ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        //if (currentState != null)
        //{
        //    currentState.OnExecute(this);
        //}

        if (navMeshAgent.hasPath) 
            animator.SetBool("isRunning", true);
        else
            animator.SetBool("isRunning", false);

        stateMachine.Update();
        currentState = stateMachine.currentState;
    }

    //public void ChangeState(IState<Enemy> state)
    //{
    //    if (currentState != null)
    //    {
    //        currentState.OnExit(this);
    //    }

    //    currentState = state;

    //    if (currentState != null)
    //    {
    //        currentState.OnEnter(this);
    //    }
    //}

    public Vector3 GetBrickPosition()
    {
        var pos = brickSpawner.GetBrickPosition(brickCarrier.GetId(), transform.position);
        
        return pos;
    }

    public void ChangeState(StateID newState)
    {
        stateMachine.ChangeState(newState);
    }

    public int GetBrickCount()
    {
        return brickCarrier.GetBrickCount();
    }

    public void CollectBrickInTrigger()
    {

        Debug.Log("Collecting");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<PlatformBrick>(out var brick) && brick.GetId() == brickCarrier.GetId())
            {
                Debug.Log(brick.GetId());
                brickCarrier.AddBrick();
                brick.Despawn(brick);
            }
        }
    }

}
