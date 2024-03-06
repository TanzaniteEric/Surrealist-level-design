using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject healthPipAngry;
    [SerializeField] private GameObject healthPipCalm;
    [SerializeField] private float heartWidthSpan = 0.4f;

    public GameObject shieldSprite;

    public BlueBar blueBar;
    
    private GameObject[] healthPips;
    
    public int maxHealth;
    public int currentHealth;

    public int criticalHealthCalm;
    public int criticalHealthAngry;

    private Sprite spriteAngry;
    private Sprite spriteCalm;

    public bool invincible;

    public int blueSumThisWave;
    
    // Start is called before the first frame update
    void Awake()
    {
        spriteAngry = healthPipAngry.GetComponent<SpriteRenderer>().sprite;
        spriteCalm = healthPipCalm.GetComponent<SpriteRenderer>().sprite;
        
        healthPips = new GameObject[maxHealth];
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                healthPips[i] = Instantiate(healthPipCalm, transform);
            }
            else
            {
                healthPips[i] = Instantiate(healthPipAngry, transform);
            }
            
        }
        
        
        
        // Space the health pips evenly left to right (down to up)
        float pipWidth = healthPipAngry.GetComponent<SpriteRenderer>().bounds.size.y - heartWidthSpan;
        float totalWidth = pipWidth * maxHealth;
        float startY = transform.position.y - (totalWidth / 2) + (pipWidth / 2);
        for (int i = 0; i < maxHealth; i++)
        {
            healthPips[i].transform.Rotate(0, 0, -90);
            healthPips[i].transform.position = new Vector2(transform.position.x, startY + (pipWidth * i));
        }
        
        UpdateHealth();
    }

    public void SetInvincible(bool invincible)
    {
        this.invincible = invincible;
        
        shieldSprite.SetActive(invincible);
    }

    public void GetHit(bool isBlue)
    {
        if (invincible) return; 
        
        if (isBlue)
        {
            blueSumThisWave += 3;
        }
        else
        {
            blueSumThisWave -= 1;
        }
        
        blueBar.SetBarPosition(blueSumThisWave);
        
        
    }

    public int WaveEndHealthDiff()
    {
        int val = 0;
        
        if (blueSumThisWave > 0)
        {
            IncrementHealth();
            val = 1;
        }
        else if (blueSumThisWave < 0)
        {
            DecrementHealth();
            val = -1;
        }
        
        blueSumThisWave = 0;
        blueBar.SetBarPosition(0);
        return val;

    }
    
    public void IncrementHealth()
    {
        currentHealth++;
        UpdateHealth();
    }
    
    public void DecrementHealth()
    {
        currentHealth--;
        UpdateHealth();
    }
    
    void UpdateHealth()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if (i < currentHealth)
            {
                healthPips[i].GetComponent<SpriteRenderer>().sprite = spriteCalm;
            }
            else
            {
                healthPips[i].GetComponent<SpriteRenderer>().sprite = spriteAngry;
            }
        }
    }

    
}
