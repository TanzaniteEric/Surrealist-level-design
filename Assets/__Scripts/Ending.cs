using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public Animator dragon;
    public Animator screen;

    public void HumanTransformation()
    {
        //need to deactivate dragon talking sfx since now they turn into human
        dragon.SetTrigger("GoodEnding"); //dragon to human transformation
    }
    
    public void DragonTransformation()
    {
        dragon.SetTrigger("ToDragon"); //dragon to human transformation
    }
    
    public void GoodEnding()
    {
        //after the dialogue, canvas fade to black, show text in the middle
        screen.SetTrigger("GE");
        
    }

    public void BadEnding()
    {
        //disable dragon sprites one by one after their speech

        //canvas fade to black, show text in the middle
        screen.SetTrigger("BE");
    }
}
