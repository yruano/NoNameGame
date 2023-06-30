using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
  [SerializeField] Renderer[] backRenderers;
  [SerializeField] Renderer[] middleRenderers;
  [SerializeField] string sortingLayerName;
  int originOrder;

  public void SetOriginOrder(int originOrder)
  {
    this.originOrder = originOrder;
    SetOrder(originOrder);
  }

  public void SetMostFrontOrder(bool isMostFront)
  {
    SetOrder(isMostFront ? 100 : originOrder);
  }

  private void Start()
  {
    SetOrder(0);
  }
  
  public void SetOrder(int order)
  {
    int mulOder = order * 10;
    
    foreach (var renderer in backRenderers)
    {
      renderer.sortingLayerName = sortingLayerName;
      renderer.sortingOrder = mulOder;
    }

    foreach (var renderer in middleRenderers)
    {
      renderer.sortingLayerName = sortingLayerName;
      renderer.sortingOrder = mulOder + 1;
    }
  }
}
