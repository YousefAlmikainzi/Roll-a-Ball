using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is used to make sure that the camera follows the player without rotating with them
public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = offset + player.transform.position;
    }
}
