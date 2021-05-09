using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    public float speed = 0.5f;
    Animator m_Animator;
    public CharacterController controller;

    public float turnSmoothTime = 3f;
    float turnSmoothVelocity;
    

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();

      
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = -Input.GetAxisRaw("Vertical");
        float horizontal = -Input.GetAxisRaw("Horizontal");
        var direction = new Vector3(horizontal, 0f, vertical).normalized;
         m_Animator = gameObject.GetComponent<Animator>();
        if(direction.magnitude >= 0.1)
        {
            m_Animator.SetBool("walking", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            controller.Move(direction * speed * Time.deltaTime);
        }
       
        if (direction.magnitude == 0)
        {
            m_Animator.SetBool("walking", false);
        }
        controller.Move(Physics.gravity * Time.deltaTime);
        //if (Input.GetKey(KeyCode.S) )
        //{
        //    Debug.Log("down");
        //    m_Animator.SetBool("walking", true);
        //}
        //if ( Input.GetKey(KeyCode.A))
        //{
        //    Debug.Log("down");
        //    m_Animator.SetBool("walking", true);
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    Debug.Log("down");
        //    m_Animator.SetBool("walking", true);
        //}
        //if ( Input.GetKey(KeyCode.D))
        //{
        //    Debug.Log("down");
        //    m_Animator.SetBool("walking", true);
        //}

        //if (Input.GetKeyUp(KeyCode.DownArrow) | Input.GetKeyDown(KeyCode.S))
        //{
        //    m_Animator.SetBool("walking", false);
        //}
        //if (Input.GetKeyUp(KeyCode.LeftArrow) | Input.GetKeyDown(KeyCode.A))
        //{
        //    m_Animator.SetBool("walking", false);
        //}
        //if (Input.GetKeyUp(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.W))
        //{
        //    m_Animator.SetBool("walking", false);
        //}
        //if (Input.GetKeyUp(KeyCode.RightArrow) | Input.GetKeyDown(KeyCode.D))
        //{
        //    m_Animator.SetBool("walking", false);
        //}

    }
}
