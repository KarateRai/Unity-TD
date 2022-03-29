using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModelMouseScript : MonoBehaviour
{

    public GameObject definedButton;
    public UnityEvent OnClick = new UnityEvent();
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        definedButton = this.gameObject;
        anim = GetComponent<Animator>();
    }

    private void OnMouseEnter()
    {
        if (null != anim)
        {
            anim.Play("Highlighted");
        }
    }
    private void OnMouseExit()
    {
        if (null != anim)
        {
            anim.Play("Normal");
        }
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Input.GetMouseButtonDown(0))
        {
            
            if (Physics.Raycast(ray, out Hit) && Hit.collider.gameObject == gameObject)
            {
                anim.Play("Pressed");
                if (anim.GetBool("BallMode"))
                {
                    anim.SetBool("BallMode", false);
                }
                else
                {
                    anim.SetBool("BallMode", true);
                }
                OnClick.Invoke();
            }
        }
    }
}

