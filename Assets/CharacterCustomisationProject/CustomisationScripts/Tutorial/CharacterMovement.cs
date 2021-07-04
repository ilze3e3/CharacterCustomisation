using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController _charC;

    public Vector3 moveDir = Vector3.zero;

    public float turnSmoothTime = 0.2f;
    private float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    private float speedSmoothVelocity;
    private float currentSpeed;
    
    private Transform cameraT;

    private float horizontal = 0;
    private float vertical = 0;
    public float jumpSpeed = 8.0f;
    public float maxSpeed = 6.0f;
    public float currSpeed;
    public float runSpeedMultiplier = 2.5f;
    public float crouchSpeedDivider = 2.5f;
    public CharacterStatistics charStats;

    public List<GameObject> itemInRangeList = new List<GameObject>();

    public InventoryComponent inventory;
    public GameObject inventoryPanel;

    public QuestHandler questHandler;
    public GameObject questPanel;

    public GameObject pickupTooltip;
    public CanvasGroup damageIndicator;

    [SerializeField] AudioSource bgMusic;

    [SerializeField] Animator charAnimator;

    KeyCode moveForward;
    KeyCode moveLeft;
    KeyCode moveRight;
    KeyCode moveBackward;

    IEnumerator FlashDamage()
    {
        while(damageIndicator.alpha > 0)
        {
            damageIndicator.alpha -= Time.deltaTime;
            yield return null;
        }
    }

    public HotkeyRebinder hotkey;
    // Start is called before the first frame update
    void Start()
    {
        hotkey = FindObjectOfType<HotkeyRebinder>();
        hotkey.LoadKeys();

        _charC = this.GetComponent<CharacterController>();
        currSpeed = maxSpeed;
        charStats = this.GetComponentInChildren<CharacterStatistics>();
        moveForward = hotkey.GetKey("ForwardKeybind");
        moveBackward = hotkey.GetKey("BackwardKeybind");
        moveLeft = hotkey.GetKey("LeftKeybind");
        moveRight = hotkey.GetKey("RightKeybind");

        Debug.Log("ForwardKey: " + moveForward.ToString());
        Debug.Log("BackwardKey: " + moveBackward.ToString());
        Debug.Log("LeftKey: " + moveLeft.ToString());
        Debug.Log("RightKey: " + moveRight.ToString());

        inventory = this.GetComponent<InventoryComponent>();

        cameraT = Camera.main.transform;
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(moveForward))
        {
            vertical = 1.0f;
            charAnimator.SetFloat("vertical", vertical);
 
        }
        if(Input.GetKeyUp(moveForward))
        {
            vertical = 0;
            charAnimator.SetFloat("vertical", vertical);

        }
        if(Input.GetKeyDown(moveLeft))
        {
            horizontal = -1.0f;
            charAnimator.SetFloat("horizontal", horizontal);
      
        }
        if(Input.GetKeyUp(moveLeft))
        {
            horizontal = 0;
            charAnimator.SetFloat("horizontal", horizontal);
     
        }
        if(Input.GetKeyDown(moveRight))
        {
            horizontal = 1.0f;
            charAnimator.SetFloat("horizontal", horizontal);
        
        }
        if(Input.GetKeyUp(moveRight))
        {
            horizontal = 0;
            charAnimator.SetFloat("horizontal", horizontal);
         
        }
        if(Input.GetKeyDown(moveBackward))
        {
            vertical = -1.0f;
            charAnimator.SetFloat("vertical", vertical);
    
        }
        if(Input.GetKeyUp(moveBackward))
        {
            vertical = 0;
            charAnimator.SetFloat("vertical", vertical);
        }
        
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(false);
                inventory.EmptyDisplay();
            }
            else
            {
                inventoryPanel.SetActive(true);
                inventory.DisplayAllItem();
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (questPanel.activeSelf)
            {
                questPanel.SetActive(false);
                questHandler.ResetQuestDisplay();
            }
            else
            {
                questPanel.SetActive(true);
                questHandler.DisplayActiveQuest();
            }
        }

        PlayerPrefs.SetString("LoadFrom", "GameScene");
        
        if(itemInRangeList.Count > 0)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //Pickup Item
                GameObject itemToPickUp = itemInRangeList[0];
                inventory.PickUpItem(itemToPickUp);
                itemInRangeList.RemoveAt(0);
                pickupTooltip.SetActive(false);
            }
        }


        if (_charC.isGrounded)
        {
           
            moveDir = new Vector3(horizontal, 0, vertical);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= currSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpSpeed;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                if (charStats.currentStamina > 0)
                {
                    currSpeed *= runSpeedMultiplier;
                    charStats.isPlayerRunning = true;
                }

            }
            if(Input.GetKeyDown(KeyCode.LeftControl) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                currSpeed /= crouchSpeedDivider;
            }
            if(Input.GetKeyUp(KeyCode.LeftControl))
            {
                currSpeed *= crouchSpeedDivider;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if (currSpeed > maxSpeed) currSpeed /= runSpeedMultiplier;
                charStats.isPlayerRunning = false;
            }
            

        }
        moveDir.y = -1;
        
        if (moveDir != Vector3.zero)
        {
            this.transform.rotation = Quaternion.Euler(0, cameraT.eulerAngles.y, 0);
        }

        _charC.Move(moveDir * Time.deltaTime);

        charStats.RefreshStat();

        if(damageIndicator.alpha < 0.2f)
        {
            damageIndicator.gameObject.SetActive(false);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            itemInRangeList.Add(collision.gameObject);
            pickupTooltip.SetActive(true);
        }

        if (collision.gameObject.tag.Contains("Enemy"))
        {
            damageIndicator.gameObject.SetActive(true);
            // Make damage indicator appear
            charStats.TakeDamage(10);
            damageIndicator.alpha = 1;
            StartCoroutine(FlashDamage());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            itemInRangeList.Remove(collision.gameObject);
            pickupTooltip.SetActive(false);
        }
    }
}
