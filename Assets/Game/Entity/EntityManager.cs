using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
  public static EntityManager Inst { get; private set; }
  void Awake() => Inst = this;

  [SerializeField] GameObject entityPrefab;
  [SerializeField] List<Entity> SelfEntities;
  [SerializeField] List<Entity> otherEntities;
  [SerializeField] Entity SelfEmptyEntity;
  [SerializeField] Entity SelfEntity;
  [SerializeField] Entity OtherEntity;

  const int MAX_ENTITY_COUNT = 3;
  public bool IsFullSelfEntities => SelfEntities.Count >= MAX_ENTITY_COUNT && !ExistSelfEmptyEntity;
  bool IsFullOtherEntities => otherEntities.Count >= MAX_ENTITY_COUNT;
  bool ExistSelfEmptyEntity => SelfEntities.Exists(x => x == SelfEmptyEntity);
  int SelfEmptyEntityIndex => SelfEntities.FindIndex(x => x == SelfEmptyEntity);

  void EntityAlignment(bool isMine)
  {
    float targetY = isMine ? 0f : 2f;
    var targetEntities = isMine ? SelfEntities : otherEntities;

    for (int i = 0; i < targetEntities.Count; i++)
    {
      float targetX = (targetEntities.Count - 1) * -1.5f + i * 3.0f;

      var targetEntity = targetEntities[i];
      targetEntity.originPos = new Vector3(targetX, targetY, 0);
      targetEntity.MoveTransform(targetEntity.originPos, true, 0.5f);
      targetEntity.GetComponent<Order>()?.SetOriginOrder(i);
    }
  }

  public void InsertSelfEmptyEntity(float xPos)
  {
    if (IsFullSelfEntities)
      return;

    if (!ExistSelfEmptyEntity)
      SelfEntities.Add(SelfEmptyEntity);

    Vector3 emptyEntityPos = SelfEmptyEntity.transform.position;
    emptyEntityPos.x = xPos;
    SelfEmptyEntity.transform.position = emptyEntityPos;

    int _emptyEntityIndex = SelfEmptyEntityIndex;
    SelfEntities.Sort((entity1, entity2) => entity1.transform.position.x.CompareTo(entity2.transform.position.x));

    if (SelfEmptyEntityIndex != _emptyEntityIndex)
      EntityAlignment(true);
  }

  public void RemoveSelfEmptyEntity()
  {
    if (!ExistSelfEmptyEntity)
      return;

    SelfEntities.RemoveAt(SelfEmptyEntityIndex);
    EntityAlignment(true);
  }

  public bool SpawnEntity(bool isMine, CardItem carditem, Vector3 spawnPos)
  {
    if (isMine)
    {
      if (IsFullSelfEntities || !ExistSelfEmptyEntity)
        return false;
    }
    else
    {
      if (IsFullOtherEntities)
        return false;
    }

    var entityObject = Instantiate(entityPrefab, spawnPos, Utils.QI);
    var entity = entityObject.GetComponent<Entity>();

    if (isMine)
      SelfEntities[SelfEmptyEntityIndex] = entity;
    else
      otherEntities.Insert(Random.Range(0, otherEntities.Count), entity);

    entity.isMine = isMine;
    entity.Setup(carditem);
    EntityAlignment(isMine);

    return true;
  }
}
