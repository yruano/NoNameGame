using UnityEngine;

public class tmpCameraController : MonoBehaviour
{
    public float moveSpeed = 40f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * moveSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * moveSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * moveSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * moveSpeed);
        }
    }
}