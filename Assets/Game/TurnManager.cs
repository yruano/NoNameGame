using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class TurnManager : MonoBehaviour
{
  public static TurnManager Inst { get; private set; }
  void Awake() => Inst = this;

  [Header("Develop")]
  [SerializeField][Tooltip("시작 턴 모들를 정합니다")] ETurnMode eTurnMode;
  [SerializeField][Tooltip("카드 배분이 매우 빨라집니다.")] bool fastMode;
  [SerializeField][Tooltip("플레이어의 턴입니다.")] int startCardCount;

  [Header("Properties")]
  public bool isLoading;
  public bool myTurn;

  enum ETurnMode { Random, My, Other }
  WaitForSeconds delay05 = new WaitForSeconds(0.5f);
  WaitForSeconds delay07 = new WaitForSeconds(0.7f);

  public static Action<bool> OnAddCard;

  void GameSetup()
  {
    if (fastMode)
      delay05 = new WaitForSeconds(0.05f);
    switch (eTurnMode)
    {
      case ETurnMode.Random:
        myTurn = Random.Range(0, 2) == 0;
        break;

      case ETurnMode.My:
        myTurn = true;
        break;

      case ETurnMode.Other:
        myTurn = false;
        break;
    }
  }

  public IEnumerator StartGameCo()
  {
    GameSetup();
    isLoading = true;

    for (int i = 0; i < startCardCount; i++)
    {
      yield return delay05;
      OnAddCard?.Invoke(true);
    }
    StartCoroutine(StartTurnCo());
  }

  IEnumerator StartTurnCo()
  {
    isLoading = true;

    // yield return delay07;
    // OnAddCard?.Invoke(true);
    yield return delay07;
    isLoading = false;
  }
}
