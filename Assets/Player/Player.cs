using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  Transform transform;
  public float PSpeed = 0.5f;
  void Awake() { transform = GetComponent<Transform>(); }

  // Update is called once per frame
  void Update()
  {
    float delta = Time.deltaTime * 10;
    FMove(delta);
  }

  void FMove(float delta)
  {
    float Speed = PSpeed * delta;
    
    if (Input.GetKey(KeyCode.D)) { transform.position += new Vector3(Speed, 0.0f, 0.0f); }
    if (Input.GetKey(KeyCode.A)) { transform.position -= new Vector3(Speed, 0.0f, 0.0f); }
    if (Input.GetKey(KeyCode.W)) { transform.position += new Vector3(0.0f, Speed, 0.0f); }
    if (Input.GetKey(KeyCode.S)) { transform.position -= new Vector3(0.0f, Speed, 0.0f); }
  }
}