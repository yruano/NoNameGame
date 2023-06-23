using UnityEngine;

public class spawner_test : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    GameObject clone;
    private void Awake()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 45);
        clone = Instantiate(prefab, new Vector3(3, 3, 0) ,rotation);
        Instantiate(prefab, new Vector3(-1, -2, 0), Quaternion.identity);
        clone.name = "tridiot";
        clone.GetComponent<SpriteRenderer>().color = Color.black;
        clone.transform.position = new Vector3 (2, 1, 0);
        clone.transform.localScale = new Vector3(3, 2, 1);

    }
}
