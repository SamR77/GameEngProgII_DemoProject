using System.Security.Cryptography;
using UnityEngine;

public class interactableDemoBall : BaseInteractable
{



    private void Awake()
    {
        // Ensure the base Awake logic is called
        base.Awake();
    }

    public override void OnInteract()
    {
        Debug.Log("Using Logic from Interactable Demo Ball");        

    }


}







