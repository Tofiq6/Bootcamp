using TMPro;
using UnityEngine;

public class ItemIndex : MonoBehaviour
{
    public int index;
    public Transform startTransform;
    public CarryItems carryItems;
    public TextMeshProUGUI collectItemText;
    public GameObject Item;
    public bool isItemExist = true;
    public bool isCanRestart = true;
    public string collectItemString = "Press 'E' to Collect";

    private void Start()
    {
        startTransform.position = Item.transform.position;
    }

    private void Update()
    {
        if (carryItems.restartBool && isCanRestart)
        {
            RestartItems();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && carryItems.isItemCarriable && isItemExist)
        {
            collectItemText.text = collectItemString;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && carryItems.isItemCarriable && isItemExist)
        {
            if (Input.GetKeyDown(KeyCode.E) && isItemExist)
            {
                DestroyItem();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && carryItems.isItemCarriable && isItemExist)
        {
            collectItemText.text = "";
        }
    }

    void DestroyItem()
    {
        collectItemText.text = "";

        carryItems.isItemCarriable = false;
        carryItems.itemNumber = index;

        isItemExist = false;
        Item.SetActive(false);

        isCanRestart = true;
    }

    void RestartItems()
    {

        isItemExist = true;
        Item.SetActive(true);
        isCanRestart = false;
        carryItems.restartBool = false;
    }
}
