using Cainos.PixelArtPlatformer_VillageProps;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.Progress;

public class PlayerControl : MonoBehaviour
{
    public enum ActionState { Normal, Stunned, Walk_Attack, Jump, Attack, Dead, special_Attack }
    public ActionState actionState;
    public float playerSpeed;

    public float horizontalInput;
    public Vector3 jump;
    public bool isOnGround = true;
    private bool allowJump = false;
    public float jumpForce;
    public bool isInteracting = false;
    public bool inStun = false;
    public bool isUsingItem = false;
    public bool isUsingFood = false;
    public bool isInMenu = false;

    public PlayerInput playerInput;
    public Rigidbody2D rb;
    public Collider2D foot;
    public GameObject TutUI;
    public GameObject ReadText;
    //public NewBehaviourScript npcDial;
    //public ProjectileBehaviour projectilePrefabe;
    //public Transform launchOffset;
    private Vector3 rawInputMovement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jump = new Vector3(0.0f, 6.0f, 0.0f);

        actionState = ActionState.Normal;
        allowJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(horizontalInput, 0) * playerSpeed * Time.deltaTime);

        switch (actionState)
        {
            case ActionState.Normal:
                break;
            case ActionState.Stunned:
                break;
            case ActionState.Attack:
                break;
            case ActionState.special_Attack:
                break;
            case ActionState.Jump:
                rb.AddForce(jump, ForceMode2D.Impulse);
                isOnGround = false;
                break;
            case ActionState.Walk_Attack:
                break;
            case ActionState.Dead:
                break;
        }

        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            actionState = ActionState.Jump;
        }
        else if (isOnGround == true && Input.GetMouseButtonDown(0))
        {

            if (Mathf.Abs(horizontalInput) > 0)
            {
                actionState = ActionState.Walk_Attack;
            }
            else
            {
                actionState = ActionState.Attack;

            }
        }
        else if (isOnGround == true && Input.GetKeyDown(KeyCode.X))
        {
            actionState = ActionState.special_Attack;
        }
        else if (inStun == true)
        {
            actionState = ActionState.Stunned;
        }
        else
        {
            actionState = ActionState.Normal;
        }

        UseItems();
        //OpenMenuButton();
       // movementStop();
    }
    public void InteractButton()
    {
        if (Input.GetKey(KeyCode.F))
        {
            isInteracting = true;
            StartCoroutine(NoLongerInteracting());
        }
    }
    public void UseItems()
    {
        if (Input.GetKey(KeyCode.I))
        {
            isUsingItem = true; // use ability
        }
        if (Input.GetKey(KeyCode.P))
        {
            isUsingFood = true;
        }
    }
  
    private void OnTriggerEnter2D(Collider2D collision) // fix to work with feet not entire player collider
    {
        // get the direction of the collision
        Vector3 direction = transform.position - collision.gameObject.transform.position;
        // see if the obect is futher left/right or up down
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {

            if (direction.x > 0) { print("collision is to the right"); }
            else { print("collision is to the left"); }

        }
        else
        {
            if (isOnGround == false && isFalling())
            {
                if (direction.y > 0)
                {
                    print("collision is up");
                    allowJump = true;
                    if (allowJump == true & collision.gameObject.tag == "BouncePad")
                    {
                        rb.AddForce(jump * jumpForce, ForceMode2D.Impulse);
                    }
                    allowJump = false;
                }
            }
          
            //if (isOnGround == false & collision.gameObject.tag == "BouncePad")
            //{
            //    rb.AddForce(jump * jumpForce, ForceMode2D.Impulse);
            //}
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Board")
        {
            if (Input.GetKey(KeyCode.Q))
            {
                ReadText.gameObject.SetActive(false);
                TutUI.gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Board")
        {
            TutUI.gameObject.SetActive(false);
            ReadText.SetActive(true);
        }
    }
    public bool isFalling()
    {
        return rb.velocity.y < 0f;
    }
    public void OpenText(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            TutUI.gameObject.SetActive(false);
            ReadText.SetActive(true);
        }
    }
    //
    //CODE BELOW IS FOR CONTROLLER USE
    //
    public void PadUseItem(InputAction.CallbackContext value)
    {
        if(value.started)
        {
            isUsingItem = true; // use ability
        }
    }
    public void PadUseFood(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            isUsingFood = true; // use ability
        }
    }
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }
    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            //actionState = ActionState.Attack;
            if (Mathf.Abs(horizontalInput) > 0)
            {
                actionState = ActionState.Walk_Attack;
            }
            else
            {
                actionState = ActionState.Attack;

            }
        }
    }
           
    public void OnJump(InputAction.CallbackContext value)
    {
        if (isOnGround == true)
        {
            actionState = ActionState.Jump;
        }
    }
    public void SpecialAttack(InputAction.CallbackContext value)
    {
        if(value.started)
        {
            actionState = ActionState.special_Attack;
        }
    }
    public void InteractButton(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            isInteracting = true;
            StartCoroutine(NoLongerInteracting());
        }
    }
    private IEnumerator changeStateToNormal()
    {
        yield return new WaitForSeconds(.5f);
        inStun = false;
        actionState = PlayerControl.ActionState.Normal;
    }
    private IEnumerator NoLongerInteracting()
    {
        yield return new WaitForSeconds(0.5f);
        isInteracting = false;
    }

}
