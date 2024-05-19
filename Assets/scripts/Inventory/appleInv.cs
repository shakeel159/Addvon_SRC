using Cainos.PixelArtPlatformer_VillageProps;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class appleInv : MonoBehaviour
{
    public List<GameObject> apples = new List<GameObject>();

    public Chest foodChest;
    public Text appleText;
    private int appleInInventory = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (foodChest.canCollect)
        {
            if (foodChest.lootSprite.tag == "Apple" && foodChest.collect > 0)
            {
                Debug.Log("Adding Apple to inventory");
                AppleToInventory(foodChest.lootSprite.gameObject);
                foodChest.collect = 0; // Reset collect count after adding one item
            }

        }
    }
    public void AppleToInventory(GameObject item)
    {
        apples.Add(item);
        appleInInventory = apples.Count;
        appleText.text = appleInInventory.ToString();
        // You can implement additional logic here, like updating UI or handling other effects.
    }
}
