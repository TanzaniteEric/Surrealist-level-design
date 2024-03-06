using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBar : MonoBehaviour
{
    public GameObject arrow;
    public Spring1D spring;
    public float barWidth;
    public int numIncrements;

    public Sprite redHeart;
    public Sprite blueHeart;
    public Sprite whiteHeart;

    public GameObject flyHeart;

    // Update is called once per frame
    void Update()
    {
        UpdateArrowPosition();
    }

    public void SetBarPosition(int position)
    {
        if (position > numIncrements) position = numIncrements;
        if (position < -1 * numIncrements) position = -1 * numIncrements;
        
        spring.equilibriumPosition = ((float)position / (float)numIncrements) * barWidth;

        UpdateArrowColor(position);
    }

    void UpdateArrowPosition()
    {
        arrow.transform.localPosition = new Vector2(spring.position, arrow.transform.localPosition.y);        
    }

    void UpdateArrowColor(int position)
    {
        if (position < 0)
        {
            arrow.GetComponent<SpriteRenderer>().sprite = redHeart;
            flyHeart.GetComponent<SpriteRenderer>().sprite = redHeart;
        }
        else if (position == 0)
        {
            arrow.GetComponent<SpriteRenderer>().sprite = whiteHeart;
            flyHeart.GetComponent<SpriteRenderer>().sprite = whiteHeart;
        }
        else
        {
            arrow.GetComponent<SpriteRenderer>().sprite = blueHeart;
            flyHeart.GetComponent<SpriteRenderer>().sprite = blueHeart;
        }
   
    }
    
    
    void OnDisable()
    {
        if(flyHeart != null)
        {
            flyHeart.GetComponent<SpriteRenderer>().enabled = true;
            flyHeart.GetComponent<Animator>().SetTrigger("fly");
        }
        
        gameObject.SetActive(false);
    }
}
