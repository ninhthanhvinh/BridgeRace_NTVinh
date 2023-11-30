using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IOnStairs
{
    [SerializeField] FixedJoystick joystick;
    Animator animator;
    Vector2 input;
    [SerializeField] float speed = 5f;
    bool isOnStairs = false;
    float lastForwardPosition;
    const float BRIDGE_ANGLE = Mathf.PI / 6;

    bool isFalling = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        input.x = joystick.Horizontal;
        input.y = joystick.Vertical;

        transform.position += speed * Time.deltaTime * new Vector3(input.x, 0f, input.y);
        if (input.sqrMagnitude > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(input.x, 0, input.y)), 0.2f);

        var deltaZ = transform.position.z - lastForwardPosition;

        if (isOnStairs)
        {
            transform.position += new Vector3(0f, deltaZ * Mathf.Tan(BRIDGE_ANGLE), 0f);
        }

        lastForwardPosition = transform.position.z;
        
        if(isFalling)
        {
            input = Vector2.zero;
        }

    }

    private void LateUpdate()
    {
        if (input.sqrMagnitude > 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }


    public void SetOnStairs(bool onStairs)
    {
        this.isOnStairs = onStairs;
        GetComponent<Rigidbody>().useGravity = !onStairs;
    }

    public IEnumerator Fall()
    {
        isFalling = true;
        GetComponent<Animator> ().SetTrigger("Fall");
        yield return new WaitForSeconds(2f);
        isFalling = false;
    }
}
