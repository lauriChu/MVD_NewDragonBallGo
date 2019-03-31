using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{     
    // Name of Button
    public string name;
    // Button Textures
    public Texture texture_pressed;
    public Texture texture_normal;

    // Components
    private RawImage rawImage;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rawImage = GetComponent<RawImage>();
        if (rawImage != null && texture_normal != null)
        {
            rawImage.texture = texture_normal;            
        }
    }

    void Update () {
        if (rawImage != null && texture_normal != null && texture_pressed != null) {
            rawImage.texture = Input.GetButtonDown(name) ? texture_pressed : texture_normal;
        }  
    }

    // Is called when button component is pressed
    /* public void onClick() {
        animator.SetBool(CLICK,true);
    }*/
}
