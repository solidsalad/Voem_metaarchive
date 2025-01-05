using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class StartAnim : MonoBehaviour
{
    public bool start = false;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    public void startAnimation()
    {
        anim.SetBool("start", true);

    }
    public void stopAnimation()
    {
        StartCoroutine(StopAnimationAfterDelay());
    }

    private IEnumerator StopAnimationAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Now stop the animation
        anim.SetBool("start", false);
    }
}
