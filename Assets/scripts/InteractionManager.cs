using Cainos.PixelArtPlatformer_VillageProps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    public Chest chest;
    public Transform interactionPoint;
    public float interactionRanf = 1f;
    public LayerMask ineractableLayer;

    
    Collider2D[] interactables;
    // Start is called before the first frame update
    void Start()
    {
        chest = GetComponentInChildren<Chest>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenChest()
    {
        interactables = Physics2D.OverlapCircleAll(interactionPoint.position, interactionRanf, ineractableLayer);

        foreach (var interacts in interactables)
        {
            //Debug.Log("Open Chest");
            chest.IsOpened = true;
            interacts.GetComponent<Chest>().Open();
        }
    }
   
    private void OnDrawGizmosSelected()
    {
        if (interactionPoint == null)
            return;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRanf);
    }

}
