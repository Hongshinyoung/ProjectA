using UnityEngine;

public class UIInventory : UIBase
{
    [SerializeField] private Transform uiTransformParent;
    private Inventory inventory;
    
    private void Awake()
    {
        inventory = Inventory.Instance;
        RefreshUI();
    }
    
    public void RefreshUI()
    {
        // 기존 슬롯 삭제
        foreach (Transform child in uiTransformParent)
        {
            Destroy(child.gameObject);
        }

        // 인벤토리에 있는 아이템 리스트로 슬롯 생성
        foreach (Item item in inventory.items)
        {
            GameObject inventorySlot = ResourceManager.Instance.LoadAsset<GameObject>("InventorySlot", eAssetType.UI);
            GameObject slot = Instantiate(inventorySlot, uiTransformParent); // 슬롯 생성
            var slotScript = slot.GetComponent<InventorySlot>();
            slotScript.SetSlot(item);

        }
    }
}
