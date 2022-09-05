using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void GotoScene(int index){
        Application.LoadLevel(index);
    }
}
