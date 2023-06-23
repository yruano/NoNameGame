using UnityEngine;
//player대체 스크립트.
public class Entity : MonoBehaviour
{
    [SerializeField]
    public float HP;
    [SerializeField]
    public float Armor;

    private void Awake()
    {
        HP = 100;
        Armor = 0;
    }
    public void FHited(float damage)
    {
        Debug.Log("player Hited, damage: "+ damage);
        HP -= damage - Armor;
    }
}
