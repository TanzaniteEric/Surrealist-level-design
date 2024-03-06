using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ProjectileSpawner projectileSpawner;
    public DialogueDisplayer dialogueDisplayer;
    public ProgressBar progressBar;
    public EnemyHealth enemyHealth;

    public GameObject blueBar;

    public GameState gameState;

    public float waveDuration = 10f;

    public WaveController currentWave;

    private bool[] healthValuesReached;

    [Header("Dialogue Sequences")] 
    [SerializeField] private DialogueSeq prologueDialogue;
    [SerializeField] private DialogueSeq goodEndingDialogue;
    [SerializeField] private DialogueSeq badEndingDialogue;
    [SerializeField] private ProjectileWave prologueWave;
    [SerializeField] private ProjectileWave goodEndingWave;
    [SerializeField] private ProjectileWave badEndingWave;
    [Description("The length of this array should equal the number of health stages the dragon has")]
    [SerializeField] private DialogueSeq[] mainDialogue;
    [SerializeField] private DialogueSeq[] fillerDialogue;
    [SerializeField] private ProjectileWave[] projectileWaves;
    
    [Header("Endings")] 
    [SerializeField] private Ending ending;

    public enum GameState
    {
        Dialogue,
        Gameplay
    }

    public void SwitchToDialogue()
    {
        blueBar.SetActive(false);
        gameState = GameState.Dialogue;
        enemyHealth.SetInvincible(true);

    }

    public void SwitchToGameplay()
    {
        blueBar.SetActive(true);
        gameState = GameState.Gameplay;
        enemyHealth.SetInvincible(false);

    }

    public void OnDragonHit(bool wasBlue)
    {
    }

    public void GetNewWave()
    {
        Debug.Log("ENEMY HEALTH" + enemyHealth.currentHealth);
        // This is also where we check for a win or lose
        if (enemyHealth.currentHealth == 0)
        {
            PlayBadEndDialogue();
            return;
        }
        else if (enemyHealth.currentHealth == enemyHealth.maxHealth)
        {
            PlayGoodEndDialogue();
            return;
        }
        
        if (healthValuesReached[enemyHealth.currentHealth])
        {
            currentWave = GetFillerWave(enemyHealth.currentHealth);
        }
        else
        {
            currentWave = GetWaveForHealth(enemyHealth.currentHealth);
        }
        
        currentWave.Step();
    }
    

    private void PlayGoodEndDialogue()
    {
        ending.HumanTransformation();
        currentWave = new WaveController(goodEndingDialogue, goodEndingWave, waveDuration);    
        currentWave.currentState = WaveController.WaveState.goodEnding;
        currentWave.Step();
    }
    
    private void PlayBadEndDialogue()
    {
        currentWave = new WaveController(badEndingDialogue, goodEndingWave, waveDuration);
        currentWave.currentState = WaveController.WaveState.badEnding;
        currentWave.Step();
    }
    
    public void PrologueAnimation()
    {
        // TODO: Play animation here
        ending.DragonTransformation();
    }

    public void GoodEndingAnimation()
    {
        ending.GoodEnding();   
    }

    public void BadEndingAnimation()
    {
        ending.BadEnding();   
    }

    private WaveController GetWaveForHealth(int health)
    {
        Debug.Log("Getting wave for health " + health);
        healthValuesReached[health] = true;
        WaveController controller = new WaveController(mainDialogue[health], projectileWaves[health], waveDuration);
        return controller;
    }

    private WaveController GetFillerWave(int health)
    {
        Debug.Log("Getting filler wave for health " + health);
        WaveController controller = new WaveController(GetFiller(), projectileWaves[health], waveDuration);
        return controller;
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        healthValuesReached = new bool[enemyHealth.maxHealth];
        currentWave = new WaveController(prologueDialogue, prologueWave, waveDuration);
        currentWave.currentState = WaveController.WaveState.prologue;
        currentWave.Step();

    }

    void Update()
    {
        currentWave.UpdateTimer(Time.deltaTime);
    }

    public void StepDialogue()
    {
        currentWave.Step();
    }

    public DialogueSeq GetFiller()
    {
        return GetRandomEntryFromArray(fillerDialogue);
    }

    private T GetRandomEntryFromArray<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}