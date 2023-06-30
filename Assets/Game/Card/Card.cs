using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
  [SerializeField] SpriteRenderer card;
  [SerializeField] SpriteRenderer character;
  [SerializeField] TMP_Text nameTMP;
  [SerializeField] TMP_Text attackTMP;
  [SerializeField] TMP_Text actionTMP;

  public CardItem carditem;
  public PRS originPRS;

  public void Setup(CardItem carditem)
  {
    this.carditem = carditem;
    
    character.sprite = this.carditem.sprite;
    nameTMP.text = this.carditem.name;
    attackTMP.text = this.carditem.attack.ToString();
    actionTMP.text = this.carditem.action.ToString();
  }

  public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
  {
    if (useDotween)
    {
      transform.DOMove(prs.pos, dotweenTime);
      transform.DORotateQuaternion(prs.rot, dotweenTime);
      transform.DOScale(prs.scale, dotweenTime);
    }
    else
    {
      transform.position = prs.pos;
      transform.rotation = prs.rot;
      transform.localScale = prs.scale;
    }
  }

  void OnMouseOver()
  {
    CardManager.Inst.CardMouseOver(this);
  }

  void OnMouseExit()
  {
    CardManager.Inst.CardMouseExit(this);
  }

  void OnMouseDown()
  {
    CardManager.Inst.CardMouseDown();
  }

  void OnMouseUp()
  {
    CardManager.Inst.CardMouseUp();
  }
}
