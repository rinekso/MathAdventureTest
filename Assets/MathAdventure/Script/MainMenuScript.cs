using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public List<GameObject> panel;
    [SerializeField]
    GameObject buttonBack;

    private void Start() {
        GotoPanel(0);
    }
    public void GotoScene(int index){
        Application.LoadLevel(index);
    }
    public void GotoPanel(int index){
        DisableAllPanel();
        panel[index].SetActive(true);

        // Set button back active when not in main panel
        buttonBack.SetActive((index != 0) ? true : false);
    }
    void DisableAllPanel(){
        foreach (var item in panel)
        {
            item.SetActive(false);
        }
    }
    public void Quit(){
        Application.Quit();
    }
}
