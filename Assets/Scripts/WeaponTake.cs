using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTake : MonoBehaviour {
    /* Player부모클래스의 콜라이더이벤트에서 어떤 무기를 얻었는지 전달받아     *
     * 자식클래스Weapon(빈 오브젝트)에 Mesh와 Collider를 추가하는 스크립트이다.*/
    
    public float attackangle = 15f;
    public float attacktime = 3f;

    public GameObject weapon;

    private bool isattack = false;
    private Item weaponitem;//아이템 속성을 받을 아이템형 변수 선언
    
    private void Awake()
    {
        SetItem();
        weaponitem=weapon.GetComponent<Item>();
    }
    private void SetItem()
    {
        Debug.Log(weapon);
        
    }
    public void GetItemCode(Collision2D Item)
    {

    }
    public void Attack(float attackdelay)
    {
        StartCoroutine(AttackAnime(attackdelay));
    }
    IEnumerator AttackAnime(float attackdeleay)
    {
        if (isattack)//어택중일때 함수가 실행되지 않게함
            yield break;
        isattack = true;
        float istime = 0;
        while (istime < attacktime)
        {
            weapon.transform.Rotate(0, 0, -(attackangle*Time.deltaTime));
            istime+=Time.deltaTime;
            yield return null;
        }
        isattack = false;
        weapon.transform.rotation = Quaternion.Euler(0, 0, 0);//본래 상태로 돌아간다.
        yield break;
    }
}
