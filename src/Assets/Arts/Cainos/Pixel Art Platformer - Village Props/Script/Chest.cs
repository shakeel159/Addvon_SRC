using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.LucidEditor;
using Unity.VisualScripting;
//using static UnityEditor.Progress;
using UnityEngine.UI;

namespace Cainos.PixelArtPlatformer_VillageProps
{
    public class Chest : MonoBehaviour
    {
        //[FoldoutGroup("Reference")]
        public Animator animate;
        public GameObject playerObject;

        private PlayerControl control;
        private Player player;
        public GameObject lootSprite;
        public Inventoey inv;
        //public string item;
        public bool canCollect;
        public int collect = 1;

        //[FoldoutGroup("Runtime"), ShowInInspector, DisableInEditMode]
        public bool IsOpened;


        private void Awake()
        {
            control = playerObject.GetComponent<PlayerControl>();
            player = playerObject.GetComponent<Player>();
        }
        private void Start()
        {

        }
        private void Update()
        {
            this.IsOpened = control.isInteracting;
        }
        //[FoldoutGroup("Runtime"),Button("Open"), HorizontalGroup("Runtime/Button")]
        public void Open()
        {
            //IsOpened = true;
            canCollect = true;
            animate.SetBool("IsOpened", IsOpened);
            //animate.SetTrigger("IsOpened");
        }

        //[FoldoutGroup("Runtime"), Button("Close"), HorizontalGroup("Runtime/Button")]
        public void Close()
        {
            //IsOpened = false;
            animate.SetBool("IsOpened", IsOpened);
        }
        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other.gameObject.tag == "Player")
        //    {
        //        Debug.Log("COLLISION WITH CHEST on chest Script");
        //        IsOpened = true;
        //        Open();
        //    }

        //}
        private void OnTriggerStay2D(Collider2D other)
        {
            if(other.gameObject.tag == "Player" && control.isInteracting)
            {
                //this.IsOpened = control.isInteracting;
                Debug.Log("COLLISION WITH CHEST on chest Script");
                IsOpened = true;
                if (IsOpened == true)
                {                  
                    Open();
                    if (gameObject.tag == "appleChest")
                    {
                        if (lootSprite.tag == "Apple" && collect > 0)
                        {
                            Debug.Log("Adding Apple to inventory");
                            inv.AppleToInventory(lootSprite.gameObject);
                            collect = 0; // Reset collect count after adding one item
                        }

                    }
                    else if (gameObject.tag == "potionChest")
                    {
                        if (lootSprite.tag == "Potion" && collect > 0)
                        {
                            Debug.Log("Adding Potion to inventory");
                            inv.PotionToInventory(lootSprite.gameObject);
                            collect = 0; // Reset collect count after adding one item
                        }
                    }
                    IsOpened = false;
                    canCollect = false;
                }
            }

        }
        private void OnTriggerExit2D(Collider2D other)
        {
            control.isInteracting = false;
            IsOpened = false;
            Close();
        }
    }
}
