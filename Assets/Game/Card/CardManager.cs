using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
  public static CardManager Inst { get; private set; }
  void Awake() => Inst = this;

  [SerializeField] ItemSO itemSO;
  [SerializeField] GameObject CardPrefab;
  [SerializeField] List<Card> myCards;
  [SerializeField] Transform cardSpawnPoint;
  [SerializeField] Transform PlayerCardLeft;
  [SerializeField] Transform PlayerCardRight;
  [SerializeField] ECardState eCardState;

  List<CardItem> itemBuffer;
  Card selectCard;
  bool isMyCardDrag;
  bool onMyCardArea;
  enum ECardState { Nothing, CanMouseOver, CanMouseDrag }

  public CardItem PopCardItem()
  {
    if (itemBuffer.Count == 0)
      SetupCardItemBuffer();

    CardItem card = itemBuffer[0];
    itemBuffer.RemoveAt(0);
    return card;
  }

  void SetupCardItemBuffer()
  {
    itemBuffer = new List<CardItem>(100);

    for (int i = 0; i < itemSO.carditems.Length; i++)
    {
      CardItem card = itemSO.carditems[i];

      for (int j = 0; j < card.percent; j++)
        itemBuffer.Add(card);

    }

    for (int i = 0; i < itemBuffer.Count; i++)
    {
      int rand = Random.Range(i, itemBuffer.Count);
      CardItem temp = itemBuffer[i];
      itemBuffer[i] = itemBuffer[rand];
      itemBuffer[rand] = temp;
    }
  }

  void Start()
  {
    SetupCardItemBuffer();
    TurnManager.OnAddCard += AddCard;
  }

  void OnDestroy()
  {
    TurnManager.OnAddCard -= AddCard;
  }

  void Update()
  {
    if (isMyCardDrag)
      CardDrag();

    DetectCardArea();
    SetECardState();
  }

  void AddCard(bool isMine)
  {
    var cardObject = Instantiate(CardPrefab, cardSpawnPoint.position, Utils.QI);
    var card = cardObject.GetComponent<Card>();

    card.Setup(PopCardItem());
    myCards.Add(card);

    setOriginOrder(true);
    CardAlignment(true);
  }

  void setOriginOrder(bool isMine)
  {
    int count = isMine ? myCards.Count : 0;

    for (int i = 0; i < count; i++)
    {
      var targetCard = myCards[i];
      targetCard?.GetComponent<Order>().SetOriginOrder(i);
    }
  }

  void CardAlignment(bool isMine)
  {
    List<PRS> originCardPRSs = new List<PRS>();
    originCardPRSs = RoundAlignment(PlayerCardLeft, PlayerCardRight, myCards.Count, 0.5f, Vector3.one * 1.5f);

    var targetCards = isMine ? myCards : null;

    for (int i = 0; i < targetCards.Count; i++)
    {
      var targetCard = targetCards[i];

      targetCard.originPRS = originCardPRSs[i];
      targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
    }
  }

  List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
  {
    float[] objLerps = new float[objCount];
    List<PRS> results = new List<PRS>(objCount);

    switch (objCount)
    {
      case 1: objLerps = new float[] { 0.5f }; break;
      case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
      case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
      default:
        float interval = 1f / (objCount - 1);
        for (int i = 0; i < objCount; i++)
          objLerps[i] = interval * i;
        break;
    }

    for (int i = 0; i < objCount; i++)
    {
      var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
      var targetRot = Utils.QI;

      if (objCount >= 4)
      {
        float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
        targetPos.y += curve;
        targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
      }

      results.Add(new PRS(targetPos, targetRot, scale));
    }

    return results;
  }

  #region MyCard

  public void CardMouseOver(Card card)
  {
    if (eCardState == ECardState.Nothing)
      return;

    selectCard = card;
    EnlargeCard(true, card);
  }
  public void CardMouseExit(Card card)
  {
    EnlargeCard(false, card);
  }
  public void CardMouseDown()
  {
    if (eCardState != ECardState.CanMouseDrag)
      return;

    isMyCardDrag = true;
  }
  public void CardMouseUp()
  {
    isMyCardDrag = false;

    if (eCardState != ECardState.CanMouseDrag)
      return;
  }
  void CardDrag()
  {
    if (!onMyCardArea)
    {
      selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale), false);
    }
  }

  void DetectCardArea()
  {
    RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
    int layer = LayerMask.NameToLayer("MyCardArea");
    onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
  }
  void EnlargeCard(bool isEnlarge, Card card)
  {
    if (isEnlarge)
    {
      Vector3 enlargePos = new Vector3(card.originPRS.pos.x, -2.8f, -10f);
      card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * 3.0f), false);
    }
    else
      card.MoveTransform(card.originPRS, false);

    card.GetComponent<Order>().SetMostFrontOrder(isEnlarge);
  }

  void SetECardState()
  {
    if (TurnManager.Inst.isLoading)
      eCardState = ECardState.Nothing;
    else if (!TurnManager.Inst.myTurn)
      eCardState = ECardState.CanMouseOver;
    else if (TurnManager.Inst.myTurn)
      eCardState = ECardState.CanMouseDrag;
  }

  #endregion
}
