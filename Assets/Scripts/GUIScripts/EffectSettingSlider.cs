using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectSettingSlider : MonoBehaviour {
    public Text text;
    public Image image;
    public Slider slider;
	public void SetValueOfText(float value)
    {
        int stateValue = (int)value;
        switch (stateValue)
        {
            case 1:
                text.text = "이펙트 퀄리티 : 낮음";
                break;
            case 2:
                text.text = "이펙트 퀄리티 : 중간";
                break;
            case 3:
                text.text = "이펙트 퀄리티 : 높음";
                break;
            default:
                break;
        }
    }

    public void SetValueOfImage(float value)
    {
        int stateValue = (int)value;
        SuperManager superManager;
        GameManager.Instance.managers.TryGetValue("GUIManager", out superManager);//딕셔너리를 활용해 GUIManager 키에 해당하는 값을 얻는다.
        GUIManager guiManager = superManager.gameObject.GetComponent<GUIManager>();
        switch (stateValue)
        {
            case 1:
                image.sprite = guiManager.effectSettingSprite_Low;
                break;
            case 2:
                image.sprite = guiManager.effectSettingSprite_Normal;
                break;
            case 3:
                image.sprite = guiManager.effectSettingSprite_High;
                break;
            default:
                break;
        }
    }
}
