using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchPuzzle : MonoBehaviour
{
    private void OnMouseDown()//down 3la object wiz scrpt attached
    {
        if(!GameControl.youWin)
           transform.Rotate(0f,0f,90f);
    }

}
