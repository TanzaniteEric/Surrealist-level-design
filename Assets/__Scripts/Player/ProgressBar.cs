using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image fillBar;

    private Color defaultColor;
    //public float maxXScale;

    void Awake()
    {        
        //fillBar.transform.localScale = new Vector3(0, 1, 1);
        defaultColor = fillBar.color;

        SetFill(0);

    }
    
    public void SetFill(float fill)
    {
        //fillBar.transform.localScale = new Vector3(Mathf.Clamp01(fill) * maxXScale, 1, 1);
        fillBar.fillAmount = fill;

        if (fillBar.fillAmount >= 0.98)
        {
            fillBar.color = Color.white;
        }
        else
            fillBar.color = defaultColor;
    }
    
}
