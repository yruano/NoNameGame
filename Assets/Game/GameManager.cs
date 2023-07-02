using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Inst { get; private set; }
  void Awake() => Inst = this;

  void Start()
  {
    StartGame();
  }

  void Update()
  {
#if UNITY_EDITOR
    InputCheatKey();
#endif
  }

  void InputCheatKey()
  {
    if (Input.GetKeyDown(KeyCode.Q))
      TurnManager.OnAddCard?.Invoke(true);

    if (Input.GetKeyDown(KeyCode.W))
      TurnManager.Inst.EndTurn();
  }

  public void StartGame()
  {
    StartCoroutine(TurnManager.Inst.StartGameCo());
  }
}
