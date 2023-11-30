using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public ButtonController buttonController;
    public void ClickedWithMouse(){
        buttonController.click();
    }
}
