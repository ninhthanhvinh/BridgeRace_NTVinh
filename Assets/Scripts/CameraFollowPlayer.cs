using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] Vector3 offset;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mainCamera.transform.position = Vector3.Slerp(mainCamera.transform.position, player.position + offset, 0.1f);
    }
}
