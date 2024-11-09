using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Prompt : MonoBehaviour
{
    [SerializeField] public GameObject[] PromptTypes;

    public enum Prompts 
    { 
        None = 0,
        Door = 1 
    }

    // test
    public void Start()
    {
        DeactivatePrompt();
    }


    public void ShowPrompt(Prompts prompt)
    {
        for (int i = 0; i < PromptTypes.Length; i++)
        {
             PromptTypes[i].SetActive((int)prompt == i);
        }
        
    }

    public void ActivateDoorPrompt()
    {
        ShowPrompt(Prompts.Door);
    }

    public void DeactivatePrompt()
    {
        ShowPrompt(Prompts.None);
    }
}
