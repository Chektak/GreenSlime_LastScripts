using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyToSetTime : MonoBehaviour {
    //소모품아이템은 사용후 즉시 삭제되어 효과음이 도중에 끊기기 때문에 그것을 방지하기 위한 스크립트입니다.
    public float setTime = 1f;

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroySetTime(setTime));
	}
    IEnumerator DestroySetTime(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
        yield break;

    }
}
