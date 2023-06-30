using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카드 원본 위치
[System.Serializable]
public class PRS
{
  public Vector3 pos;
  public Quaternion rot;
  public Vector3 scale;

  public PRS(Vector3 pos, Quaternion rot, Vector3 scale)
  {
    this.pos = pos;
    this.rot = rot;
    this.scale = scale;
  }
}

public class Utils
{
  public static Quaternion QI => Quaternion.identity;
  public static Vector3 MousePos
  {
    get
    {
      Vector3 result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      result.z = -10;
      return result;
    }
  }
}
