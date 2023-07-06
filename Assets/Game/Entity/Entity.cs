using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class Entity : MonoBehaviour
{
  [SerializeField] CardItem cardItem;
  [SerializeField] SpriteRenderer entity;
  [SerializeField] SpriteRenderer character;
  [SerializeField] TMP_Text nameTMP;
  [SerializeField] TMP_Text attackTMP;
  [SerializeField] TMP_Text actionTMP;

  public int attack;
  public int action;
  public bool isMine;
  public bool isBossOrEmpty;
  public Vector3 originPos;

  public void Setup(CardItem cardItem)
  {
    attack = cardItem.attack;
    action = cardItem.action;

    this.cardItem = cardItem;
    character.sprite = this.cardItem.sprite;
    nameTMP.text = this.cardItem.name;
    attackTMP.text = attack.ToString();
    actionTMP.text = action.ToString();
  }

  public void MoveTransform(Vector3 pos, bool useDotween, float dotweenTime = 0)
  {
    if (useDotween)
      transform.DOMove(pos, dotweenTime);
    else
      transform.position = pos;
  }
}
