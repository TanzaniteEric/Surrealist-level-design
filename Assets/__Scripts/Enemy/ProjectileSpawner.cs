using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;
    
    public ProjectileWave[] projectileWaves;
    
    public float waveCooldownTime;

    private bool WaveReady;
    
    private int currentWaveIndex;

    public int wavesBetweenDialogue;
    
    private int wavesSinceLastDialogue;

    public bool paused;

    void Start()
    {
        // StartCoroutine(WaveCooldown(waveCooldownTime));

        wavesSinceLastDialogue = wavesBetweenDialogue - 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        // if (WaveReady && !paused)
        // {
        //     SpawnWave();
        //     StartCoroutine(WaveCooldown(waveCooldownTime));
        // }
    }

    public void SpawnWave(ProjectileWave wavePrefab)
    {
        // Create wave
        ProjectileWave wave = Instantiate(wavePrefab);
        
        wave.Shoot();
        
        // Set wave to be at the same position as the spawner
        wave.transform.position = transform.position;
        
    }

    private IEnumerator WaveCooldown(float cooldownTime)
    {
        WaveReady = false;
        yield return new WaitForSeconds(cooldownTime);
        WaveReady = true;
    }
}
