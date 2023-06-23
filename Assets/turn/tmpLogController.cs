using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tmpLogController : MonoBehaviour
{
    public float moveSpeed = 40f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Translate(Vector3.up * moveSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(Vector3.down * moveSpeed);
        }
    }
}
