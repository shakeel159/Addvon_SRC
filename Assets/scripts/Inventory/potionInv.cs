using Cainos.PixelArtPlatformer_VillageProps;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class potionInv : MonoBehaviour
{
    public List<GameObject> potion = new List<GameObject>();

    public Chest potionChest;
    public Text potionText;

    private int potionInInventory = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (potionChest.canCollect)
        {
            if (potionChest.lootSprite.tag == "Potion" && potionChest.collect > 0)
            {
                Debug.Log("Adding Potion to inventory");
                PotionToInventory(potionChest.lootSprite.gameObject);
                potionChest.collect = 0; // Reset collect count after adding one item
            }

        }


    }
    public void PotionToInventory(GameObject item)
    {
        potion.Add(item);
        potionInInventory = potion.Count;
        potionText.text = potionInInventory.ToString();
        // You can implement additional logic here, like updating UI or handling other effects.
    }
}
