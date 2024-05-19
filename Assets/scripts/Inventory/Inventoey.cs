using Cainos.PixelArtPlatformer_VillageProps;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventoey : MonoBehaviour
{

    //public static Inventoey instance;
    public List<GameObject> apples = new List<GameObject>();
    public List<GameObject> potion = new List<GameObject>();

    public Chest[] chests;

    public Text appleText;
    public Text potionText;

    [SerializeField] public int potionInInventory = 0;
    [SerializeField] public int appleInInventory = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //foreach (var chest in chests)
        //{
        //    if(chest.canCollect)
        //    {
        //        if (chest.gameObject.tag == "appleChest")
        //        {
        //            if (chest.lootSprite.tag == "Apple" && chest.collect > 0)
        //            {
        //                Debug.Log("Adding Apple to inventory");
        //                AppleToInventory(chest.lootSprite.gameObject);
        //                chest.collect = 0; // Reset collect count after adding one item
        //            }

        //        }
        //        else if (chest.gameObject.tag == "potionChest")
        //        {
        //            if (chest.lootSprite.tag == "Potion" && chest.collect > 0)
        //            {
        //                Debug.Log("Adding Potion to inventory");
        //                PotionToInventory(chest.lootSprite.gameObject);
        //                chest.collect = 0; // Reset collect count after adding one item
        //            }
        //        }
        //    }

        //}
            


    }
    public void AppleToInventory(GameObject item)
    {
        apples.Add(item);
        appleInInventory = apples.Count;
        appleText.text = appleInInventory.ToString();
        // You can implement additional logic here, like updating UI or handling other effects.
    }
    public void PotionToInventory(GameObject item)
    {
        potion.Add(item);
        potionInInventory = potion.Count;
        potionText.text = potionInInventory.ToString();
        // You can implement additional logic here, like updating UI or handling other effects.
    }

}
