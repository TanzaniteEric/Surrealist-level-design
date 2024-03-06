using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : MonoBehaviour
{
    private Coroutine bulletTimeCoroutine;
    
    /// <summary>
    /// Default timescale is 0
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="timeScale"></param>
    public void SlowForDuration(float duration, float timeScale = 0)
    {
        if (bulletTimeCoroutine != null)
            StopCoroutine(bulletTimeCoroutine);
        
        bulletTimeCoroutine = StartCoroutine(slowRoutine(duration, timeScale));
    }

    private IEnumerator slowRoutine(float duration, float timeScale)
    {
        Time.timeScale = timeScale;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }
    
}
