using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //각 매니저들과 게임 내 오브젝트들을 연결시켜주는 책임을 가진다.
    //각 매니저들의 싱글턴과 공통 함수들을 부모클래스의 함수를 통해 실행하는 책임을 가진다.
    public GameObject player;
    public Camera maincamera;
    public List<SuperManager> managers;
    public static GameManager instance=null;
    
    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        
        foreach(var o in managers)
        {
            managers.Add(Instantiate(o, Vector2.zero, Quaternion.identity));
        }
       
    }

    void Start()
    {


    }

   

    void Update () {
		
	}
}
