using UnityEngine;
//point target
//�������� ��ũ��Ʈ���� Ÿ�� ����
public class MonsterTargetFinder : MonoBehaviour
{
    public GameObject Mtarget = null;
    // Start is called before the first frame update
    private void Awake()
    {
        //�÷��̾��� ���� ã�Ƽ� �����. �ٵ� �̰� �����ؼ� �� ����
        Mtarget = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //��ǥ ��ġ�� �ٽ� �⵵�� �ؾ��ϳ� �; �ϴ� �����
        if (Mtarget == null)
            Mtarget = GameObject.FindGameObjectWithTag("Player");
    }
}
