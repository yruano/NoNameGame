using UnityEngine;
//point target
//전반적인 스크립트들의 타겟 지정
public class MonsterTargetFinder : MonoBehaviour
{
    public GameObject Mtarget = null;
    // Start is called before the first frame update
    private void Awake()
    {
        //플레이어의 값을 찾아서 기록함. 다들 이거 공유해서 쓸 것임
        Mtarget = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //목표 놓치면 다시 잡도록 해야하나 싶어서 일단 적어둠
        if (Mtarget == null)
            Mtarget = GameObject.FindGameObjectWithTag("Player");
    }
}
