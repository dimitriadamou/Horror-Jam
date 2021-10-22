using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaLerp : MonoBehaviour
{
    [SerializeField] SharedInt playerHealth;
    [SerializeField] GameObject target;
    [SerializeField] GameObject bringHimToMe;
    private float startCameraDollyYPos = 0;

    [SerializeField] Animator animator;
    [SerializeField] Animator kidAnimator;
    private float startXPos = 0;
    private float destXPos = 0;
    private float direction  = 0;
    void Start()
    {
        destXPos = startXPos = this.transform.position.x;
        startCameraDollyYPos = bringHimToMe.transform.localPosition.y; 
        playerHealth.Callback += CalculateNewPosition;
    }

    void CalculateNewPosition(int value)
    {
        //max is 100
        var percentage = ((float)value/100f);

        destXPos = Mathf.Lerp(
            target.transform.position.x + 2,
            startXPos,
            percentage
        );

        bringHimToMe.transform.localPosition = new Vector3(
            0,
            Mathf.Lerp(
                (transform.position.y + 10),
                startCameraDollyYPos,
                percentage
            ),
            0
        );

        direction = destXPos <= this.transform.position.x ? -1 : 1;


        animator.SetBool("IsMoving", true);
        kidAnimator.SetBool("IsGrandmaComing", true);
    }
    private void Update() {

        if(Mathf.Approximately(
            this.transform.position.x, 
            destXPos)
        ) {
            animator.SetBool("IsMoving", false);
            kidAnimator.SetBool("IsGrandmaComing", false);
            return;
        }

        this.transform.position = new Vector3(
            Mathf.Clamp(this.transform.position.x + (direction * Time.deltaTime * 5), destXPos, this.transform.position.x),
            this.transform.position.y,
            this.transform.position.z
        );
    }
}
