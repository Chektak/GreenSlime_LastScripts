using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Stick : Item {
    
    public float attackAngle = 2500f;
    public float attackTime = 1f;

    protected override void UseItem()
    {
        
            StartCoroutine(Attack_Stick());
        
    }

    IEnumerator Attack_Stick()
    {
        int canSoundPlayCount = 13;
        float istime = 0;
        while (istime < attackTime)
        {
            gameObject.tag = "Weapon";
            istime += Time.deltaTime;
            PlayerManager.Player.ReturnInventoryscript().itemHold.transform.Rotate(0, 0, -(attackAngle * Time.deltaTime));

            if(canSoundPlayCount==13)
            {
                SuperManager superManager;
                GameManager.Instance.managers.TryGetValue("SoundManager", out superManager);//GameManager에서 사운드매니저에 대한 정보를 얻는다.
                SoundManager soundManager = superManager.gameObject.GetComponent<SoundManager>();
                soundManager.soundEffectSource.PlayOneShot(itemUseSoundEffect);
                canSoundPlayCount = 0;
            }
            canSoundPlayCount++;
            yield return null;
        }
        PlayerManager.Player.Setupp_A_S(Unit.UnitActionState.IDLE);
        OriginalEulerChange();//슈퍼클래스의 본래 상태로 돌아가는 함수를 실행한다.
        
        yield break;
    }
}
