using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class OnHitFX : MonoBehaviour
{
    [SerializeField] private GameObject red_vfx;
    [SerializeField] private GameObject blue_vfx;
    [SerializeField] private EventReference angryHitSound;
    [SerializeField] private EventReference calmHitSound;

    public void playFX(bool isBlue, float x)
    {
        if (!isBlue)
        {
            GameObject rvfx = Instantiate(red_vfx);

            rvfx.transform.position = new Vector3(x, transform.position.y, transform.position.z);
            Destroy(rvfx, 2f);
            AudioManager.instance.PlayOneShot(angryHitSound, this.transform.position);
        }
        else
        {
            GameObject bvfx = Instantiate(blue_vfx);

            bvfx.transform.position = this.transform.position;
            Destroy(bvfx, 2f);
            AudioManager.instance.PlayOneShot(calmHitSound, this.transform.position);
        }
    }


}

