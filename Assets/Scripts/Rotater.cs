using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is used to rotate the cube
public class Rotater : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
