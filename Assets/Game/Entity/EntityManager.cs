using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
  public static EntityManager Inst {get; private set; }
  void Awake() => Inst = this;

  [SerializeField] GameObject entityPrefab;
  [SerializeField] List<Entity> myEntities;
  [SerializeField] List<Entity> otherEntities;
  [SerializeField] Entity PlayerEmptyEntity;
  [SerializeField] Entity PlayerEntity;
  [SerializeField] Entity OtherEntity;

  void EntityAlignment(bool isMine)
  {
    float targetY = isMine ? -4.35f : 4.15f;
    var targetEntities = isMine ? myEntities : otherEntities;

    for (int i = 0; i < targetEntities.Count; i++)
    {
      float targetX = (targetEntities.Count - 1) * -3.4f + i * 6.8f;

      var targetEntity = targetEntities[i];
      targetEntity.originPos = new Vector3(targetX, targetY, 0);
      targetEntity.MoveTransform(targetEntity.originPos, true, 0.5f);
      targetEntity.GetComponent<Order>().SetOriginOrder(i);
    }
  }

}
