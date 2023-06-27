using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  Transform transform;
  public float PSpeed = 0.5f;
  void Awake() { transform = GetComponent<Transform>(); }

  // 플레이어의 움직임 텔타타임을이용하여 일정함을 유지
  void Update()
  {
    float delta = Time.deltaTime * 10;
    FMove(delta);
  }

  // 키에 따라 움직임 (키가 바뀔 수 있음)
  void FMove(float delta)
  {
    float Speed = PSpeed * delta;
    
    if (Input.GetKey(KeyCode.D)) { transform.position += new Vector3(Speed, 0.0f, 0.0f); }
    if (Input.GetKey(KeyCode.A)) { transform.position -= new Vector3(Speed, 0.0f, 0.0f); }
    if (Input.GetKey(KeyCode.W)) { transform.position += new Vector3(0.0f, Speed, 0.0f); }
    if (Input.GetKey(KeyCode.S)) { transform.position -= new Vector3(0.0f, Speed, 0.0f); }
  }
}