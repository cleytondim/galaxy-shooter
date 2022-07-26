using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _anim;
    // Start is called before the first frame update

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        string pname = _anim.GetComponentInParent<Component>().name;

        if (pname=="Player_1" || pname == "Player")
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _anim.SetBool("Turn_Left", true);
                _anim.SetBool("Turn_Right", false);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                _anim.SetBool("Turn_Left", false);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _anim.SetBool("Turn_Left", false);
                _anim.SetBool("Turn_Right", true);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                _anim.SetBool("Turn_Right", false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _anim.SetBool("Turn_Left", true);
                _anim.SetBool("Turn_Right", false);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                _anim.SetBool("Turn_Left", false);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _anim.SetBool("Turn_Left", false);
                _anim.SetBool("Turn_Right", true);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                _anim.SetBool("Turn_Right", false);
            }
        }
        
    }
}
