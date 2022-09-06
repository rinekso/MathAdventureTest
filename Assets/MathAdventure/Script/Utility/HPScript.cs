using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPScript : MonoBehaviour
{
    [Header("UI Element")]
    [SerializeField]
    RectTransform barValue;

    [Space]
    public float maxValue;
    float currentValue;
    float widthUI;
    private void Start() {
        currentValue = maxValue;
        widthUI = barValue.rect.width;
        SetUIHP();
    }
    // set value
    public void SetValue(float _val) {
        if(_val > 0 && _val < maxValue) {
            currentValue = _val;
        }
        SetUIHP();
    }
    void SetUIHP() {
        barValue.sizeDelta = new Vector2(widthUI*(currentValue/maxValue),barValue.sizeDelta.x);
        if(currentValue == 0) GameOver();
    }
    void GameOver(){
        GetComponent<UIFollowObject>().target.GetComponentInParent<CharPropTemp>().GameOver();
    }
    public void DecreaseVal(float _val) {
        if((currentValue-_val) < 0)
            currentValue = 0;
        else
            currentValue -= _val;
        SetUIHP();
    }
    public void IncreaseVal(float _val) {
        if((currentValue+_val) > maxValue)
            currentValue = maxValue;
        else
            currentValue += _val;
        SetUIHP();
    }
    public float GetCurrentValueHP(){
        return currentValue;
    }
}
