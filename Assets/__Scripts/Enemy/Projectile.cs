using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Projectile : MonoBehaviour
{
    public Vector2 moveDirection;
    public float moveSpeed;

    public Spring2D spring;

    public bool isGrabbed;
    public bool isLaunched;

    public bool isBlue;

    public Color blueColor;
    public Sprite blueSprite;
    
    private LayerMask dragonLayerMask;
    private LayerMask playerLayerMask;

    [SerializeField] private GameObject outerRing;

    [SerializeField] private float minOuterRingScale;
    [SerializeField] private float maxOuterRingScale;
    
    public float timeToTurnBlue;
    private float blueTimer;

    public float bottomY;

    public bool startAsBlue = false;
    public float conversionBoost;
    public bool canGrab;
    
    [field: SerializeField] public EventReference chargingCompleteAudio { get; private set; }
    [field: SerializeField] public EventReference blueHitPlayerAudio { get; private set; }

    void Start()
    {
        InitSpring();    
        dragonLayerMask = LayerMask.GetMask("Dragon");
        playerLayerMask = LayerMask.GetMask("Player");
        
        if (startAsBlue)
        {
            BlueHeartInitialization();
        }
        else
        {
            canGrab = true;
        }
            
    }
    
    private void BlueHeartInitialization()
    {
        GetComponent<SpriteRenderer>().sprite = blueSprite;
        conversionBoost = 0.1f;

        //transform.localScale = new Vector3(transform.localScale.x * 15, transform.localScale.y * 15, transform.localScale.z * 15);
        //transform.rotation = Quaternion.identity;
        
        canGrab = false;
    }
    private void InitSpring()
    {
        spring.equilibriumPosition = transform.position;
        spring.position = spring.equilibriumPosition;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Deallocate();
        }
        
        if (!isGrabbed)
        {
            spring.equilibriumPosition =
                spring.equilibriumPosition + (moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
        else if (isGrabbed && !isLaunched)
        {
            UpdateColor();
            UpdateOuterRingScale();
            GameManager.Instance.progressBar.SetFill(blueTimer / timeToTurnBlue);
        }

        transform.position = spring.position;
        
        if (isLaunched)
        {
            CheckDragonCollision();
        }
        
        if (!isGrabbed && !isLaunched)
        {
            CheckPlayerCollision();
        }
    }

    void Deallocate()
    {
        Destroy(gameObject);
    }
    
    private void UpdateColor()
    {
        blueTimer += Time.deltaTime;
        if (!isBlue && blueTimer >= timeToTurnBlue)
        {
            blueTimer = timeToTurnBlue;
            SetBlue();
        }
    }

    private void UpdateOuterRingScale()
    {
        float scale = Mathf.Lerp(minOuterRingScale, maxOuterRingScale, (blueTimer / timeToTurnBlue));
        outerRing.transform.localScale = new Vector3(scale, scale, 1);
    }

    private void CheckDragonCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, 0.1f, dragonLayerMask);
        if (hit.collider != null)
        {
            EnemyHealth enemyHealth = hit.collider.gameObject.GetComponentInChildren<EnemyHealth>();
            if (enemyHealth != null)
            {
                //on hit fx
                OnHitFX fx = hit.collider.gameObject.GetComponentInChildren<OnHitFX>();
                fx.playFX(isBlue, transform.position.x);

                enemyHealth.GetHit(isBlue);
                Destroy(gameObject);
            }
            
            ProjectileGrabber projectileGrabber = hit.collider.gameObject.GetComponentInParent<ProjectileGrabber>();
            if (projectileGrabber != null)
            {
                projectileGrabber.Release();
                Destroy(gameObject);
            }

        }       
    }

    private void CheckPlayerCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, 0.1f, playerLayerMask);
        
        if (hit.collider != null)
        {
            ProjectileGrabber projectileGrabber = hit.collider.gameObject.GetComponentInParent<ProjectileGrabber>();

            if (projectileGrabber != null)
            {
                if (startAsBlue)
                {
                    //if this is blue projectile from dragon
                    AudioManager.instance.PlayOneShot(blueHitPlayerAudio, this.transform.position);
                    projectileGrabber.BoostConversion(conversionBoost);
                    Deallocate();
                }
                else
                {
                    //if this is red projectile from dragon
                    projectileGrabber.Release();

                    Launch(Vector2.up, projectileGrabber.projectileLaunchSpeed);
                }
                
            }
        }       
    }

    public void Launch(Vector2 direction, float launchSpeed)
    {
        isLaunched = true;
        isGrabbed = false;
        moveDirection = direction;
        moveSpeed = launchSpeed;
        GameManager.Instance.progressBar.SetFill(0);
    }

    public void SetGrabbed()
    {
        isGrabbed = true;
        blueTimer = 0;
    }

    public void SetBlue()
    {
        AudioManager.instance.PlayOneShot(chargingCompleteAudio, this.transform.position);
        isBlue = true;
        GetComponent<SpriteRenderer>().color = blueColor;
    }


    public void SpeedupBlueTimer(float boostAmount)
    {
        blueTimer += boostAmount;
    }
}
