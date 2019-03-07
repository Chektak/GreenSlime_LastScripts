using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectSlider : MonoBehaviour {
    public RectTransform panelRect;
    public Slider slider;
	public void SliderOfStageShow(float value)
    {
        panelRect.offsetMin = new Vector2(-value * 192, 0);
    }
}
