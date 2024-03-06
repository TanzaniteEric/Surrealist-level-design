using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ProjectileGrabber : MonoBehaviour
{
    public PlayerInputHandler inputHandler;
    public PlayerMovement playerMovement;
    private LayerMask projectileLayerMask;
    
    private Projectile grabbedProjectile;
    
    public float projectileRotationSpeed;
    private float currentProjectileRotation;

    public float projectileGrabRadius;
    
    public float projectileRotationRadius;
    
    private bool currentlyGrabbing;
    
    private bool clockwiseRotation;

    public float projectileLaunchSpeed;
    
    [field: Header("Bullet SFX")]
    [SerializeField] private EventReference releaseSound;
    [SerializeField] private EventReference bounceSound;

    [field: Header("Charging SFX")]
    [field: SerializeField] public EventReference grabbedAudio { get; private set; }
    [field: SerializeField] public EventReference chargingAudio { get; private set; }
    
    

    
    // Start is called before the first frame update
    void Start()
    {
        projectileLayerMask = LayerMask.GetMask("Projectile");
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.playerInputState.grab)
        {
            if (currentlyGrabbing)
            {
                UpdateProjectileRotation();
            }
            else
            {
                CheckForGrab();
            }
        }
        else
        {
            if (currentlyGrabbing)
            {
                Release();
            }
        }
    }

    public void CheckForGrab()
    {
        // Do an overlap circle check to see if there are any projectiles in range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, projectileGrabRadius, projectileLayerMask);
        
        // Find the closest projectile
        Projectile closestProjectile = null;
        float closestDistance = Mathf.Infinity;
        
        foreach (Collider2D collider in colliders)
        {
            Projectile projectile = collider.GetComponent<Projectile>();
            if (projectile != null && !projectile.isGrabbed && !projectile.isLaunched)
            {
                float distance = Vector2.Distance(transform.position, projectile.transform.position);
                if (distance < closestDistance)
                {
                    closestProjectile = projectile;
                    closestDistance = distance;
                }
            }
        }
        
        if (closestProjectile != null)
        {
            Grab(closestProjectile);
        }
    }

    private void Grab(Projectile projectile)
    {
        // Can't grab a launched projectile
        if (projectile.isLaunched ||!projectile.canGrab) return;
        
        AudioManager.instance.PlayOneShot(grabbedAudio, this.transform.position);
        playerMovement.SetSlow();
        
        // If projectile is to your left, rotate CCW
        // Else rotate CW
        if (projectile.transform.position.x < transform.position.x)
        {
            clockwiseRotation = false;
        }
        else
        {
            clockwiseRotation = true;
        }
        
        grabbedProjectile = projectile;
        
        grabbedProjectile.SetGrabbed();
        
        // Calculate initial rotation
        Vector2 direction = (Vector2)projectile.transform.position - (Vector2)transform.position;
        currentProjectileRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        currentlyGrabbing = true;
        
    }
    public void BoostConversion(float boostAmount)
    {
        if (currentlyGrabbing)
        {
            grabbedProjectile.SpeedupBlueTimer(boostAmount);
        }
    }
    public void Release()
    {
        if (!currentlyGrabbing)
        {
            AudioManager.instance.PlayOneShot(bounceSound, this.transform.position);
            return;
        }
        
        currentlyGrabbing = false;
        grabbedProjectile.Launch(Vector2.up, projectileLaunchSpeed);
        AudioManager.instance.PlayOneShot(releaseSound, this.transform.position);
        playerMovement.SetFast();
        
    }

    private void UpdateProjectileRotation()
    {
        if (clockwiseRotation)
        {
            currentProjectileRotation -= projectileRotationSpeed * Time.deltaTime;
            if (currentProjectileRotation < 0)
            {
                AudioManager.instance.PlayOneShot(chargingAudio, this.transform.position);
                currentProjectileRotation += 360;
            }
        }
        else
        {
            currentProjectileRotation += projectileRotationSpeed * Time.deltaTime;
            if (currentProjectileRotation > 360)
            {
                AudioManager.instance.PlayOneShot(chargingAudio, this.transform.position);
                currentProjectileRotation -= 360;
            }
        }
        
        Vector2 direction = new Vector2(Mathf.Cos(currentProjectileRotation * Mathf.Deg2Rad), Mathf.Sin(currentProjectileRotation * Mathf.Deg2Rad));
        
        Vector2 targetPosition = (Vector2)transform.position + (direction * projectileRotationRadius);
        
        grabbedProjectile.spring.equilibriumPosition = targetPosition;
    }
}
