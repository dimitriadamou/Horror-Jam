using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaLerp : MonoBehaviour
{
    [SerializeField] SharedInt playerHealth;
    [SerializeField] GameObject target;

    [SerializeField] Animator animator;
    private float startXPos = 0;
    private float destXPos = 0;
    private float direction  = 0;
    void Start()
    {
        destXPos = startXPos = this.transform.position.x;
        playerHealth.Callback += CalculateNewPosition;
    }

    void CalculateNewPosition(int value)
    {
        //max is 100
        var percentage = ((float)value/100f);

        destXPos = Mathf.Lerp(
            target.transform.position.x,
            startXPos,
            percentage
        );

        direction = destXPos <= this.transform.position.x ? -1 : 1;


        animator.SetBool("IsMoving", true);
    }
    private void Update() {

        if(Mathf.Approximately(
            this.transform.position.x, 
            destXPos)
        ) {
            animator.SetBool("IsMoving", false);
            return;
        }

        this.transform.position = new Vector3(
            Mathf.Clamp(this.transform.position.x + (direction * Time.deltaTime * 5), destXPos, this.transform.position.x),
            this.transform.position.y,
            this.transform.position.z
        );
    }
}
