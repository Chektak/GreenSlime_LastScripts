using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    
    public GameObject itemHold;
    public AudioClip itemEatSoundEffect;
    public Image selectItemShowImage;
    public Image[] itemslot = new Image[5];
    public Text itemTooltip = null;

    private Item[] inventory = new Item[5];
    private int nowSelectItem = 0;
    private int pastSelectItem=0;

    /// <summary>
    /// 아이템을 넣을 칸이 있는지 확인하고 아이템을 리스트에 추가한다. 실패시 out형으로 2번째 인잣값의 값을 바꾼다.
    /// </summary>
    /// <param name="Item">추가할 아이템</param>
    public void AddInventory(Item item)
    {
        for(int i = 0; i<itemslot.Length; i++) {
            if (inventory[i] == null)//인벤토리중 빈칸이 있는지 확인
            {
                SuperManager superManager;
                GameManager.Instance.managers.TryGetValue("SoundManager", out superManager);//GameManager에서 사운드매니저에 대한 정보를 얻는다.
                SoundManager soundManager = superManager.gameObject.GetComponent<SoundManager>();
                soundManager.soundEffectSource.PlayOneShot(itemEatSoundEffect);
                inventory[i] = item;
                itemslot[i].sprite = inventory[i].RendererInfo().sprite;
                
                if (GameObject.FindGameObjectWithTag("NowItem") == null || PlayerManager.Player.U_A_S!=PlayerManager.UnitActionState.PATTERN)
                    //셀렉중인 아이템이 없거나 아이템 사용중이 아닐경우(아이템이 존재할경우) 렌더러를 바꾼다.
                    ChangeItem();
                break;
            }
        } 
    }

    /// <summary>
    /// 아이템을 버릴 수 있다면, 아이템을 버리고 인벤토리에서 아이템을 삭제한다.
    /// </summary>
    /// <param name="item"></param>
    public void RemoveInventory(Item item)
    {
        if (itemslot[nowSelectItem]!=null&& PlayerManager.Player.U_A_S != Unit.UnitActionState.PATTERN)
        {//인벤토리에 셀렉중인 아이템이 존재하고 플레이어가 아이템을 사용중이지 않을때
            item.ReturnObjItem().EnableObjItem(true);
            itemslot[nowSelectItem].sprite = null;
            inventory[nowSelectItem].transform.parent = null;
            inventory[nowSelectItem].gameObject.tag = "Item";
            inventory[nowSelectItem] = null;
            ChangeItem();
        }
    }

    /// <summary>
    /// 현재 셀렉중인 아이템의 정보를 받아 아이템상태를 변경할 수 있는지 확인하고 실행해주는 함수이다.
    /// </summary>
    public void UseItem()
    {
            inventory[nowSelectItem].UsingTest();
    }

    /// <summary>
    /// 셀렉중인 아이템에 대해 유저에게 시각매체를 보여주는 함수다.
    /// </summary>
    public void ChangeItem()
    {
        bool inventoryisnull = true;//인벤토리가 비었는지를 계산하는걸 반복할 필요가 없기 때문에, 함수값으로 이 변수를 사용할 것이다.
        if (inventory[nowSelectItem] != null)
        {
            inventoryisnull = false;
        }
        ChangeItemTooltip(inventoryisnull);
        ChangeItemtransform(inventoryisnull);

    }

    /// <summary>
   /// 인벤토리홀더에 자식으로 아이템을 추가한다.(내부알고리즘은 아이템오브젝트의 포지션이동과 자식오브젝트 관련으로 되어있다)
   /// </summary> 
    private void ChangeItemtransform(bool selectItemisnull)
    {
        if (!selectItemisnull)//셀렉 중인 아이템이 null이 아니라면
        {
            inventory[nowSelectItem].transform.parent = itemHold.transform;
            inventory[nowSelectItem].tag = "NowItem";
            inventory[nowSelectItem].transform.localPosition = new Vector2();

            if (PlayerManager.Player.P_S_S == PlayerManager.PlayerSeeState.SEERIGHT)//아이템이 거꾸로 되는것을 방지한다.
                inventory[nowSelectItem].transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                inventory[nowSelectItem].transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (inventory[pastSelectItem] != null)//셀렉 전의 아이템이 있다면
        {
            inventory[pastSelectItem].transform.parent = null;
            inventory[pastSelectItem].tag = "Item";
            inventory[pastSelectItem].transform.localPosition = GameManager.Instance.outerWorld;//그 전에 셀렉했던 아이템을 숨긴다.*/
        }
    }

    /// <summary>
    /// 아이템의 툴팁을 변경한다. 셀렉아이템이 null일 경우 아이템의 툴팁은 변경되지 않는다.
    /// </summary>
    private void ChangeItemTooltip(bool selectItemisnull)
    {
        if (selectItemisnull)
        {
            itemTooltip.text = "";
            return;
        }
        itemTooltip.text = inventory[nowSelectItem].itemTooltipText;
    }

    /// <summary>
    /// 마우스 스크롤러에 따라 무엇이 셀렉되고있는지를 위한 '별' 이미지를 이동한다.
    /// </summary>
    /// <param name="isScrollUp"></param>
    public void ChangeSelectItemRenderer(bool isScrollUp)
    {
        if (PlayerManager.Player.U_A_S == Unit.UnitActionState.PATTERN)//아이템을 사용중이라면 다른아이템을 셀렉할 수 없다.
            return;
        pastSelectItem = nowSelectItem;
        if (isScrollUp)
        {
            nowSelectItem++;
            
            if (nowSelectItem > 4)
            {//표시를 인벤토리칸에 고정
                nowSelectItem = 0;
            }
        }
        else
        {
            nowSelectItem--;
            
            if (nowSelectItem < 0)
            {//표시를 인벤토리칸에 고정
                nowSelectItem = 4;
            }
       

        }
        selectItemShowImage.rectTransform.position = itemslot[nowSelectItem].rectTransform.position;
        
    }
    
    /// <summary>
    /// 현재 셀렉되고있는 아이템을 반환한다.
    /// </summary>
    /// <returns></returns>
    public Item ReturnSelectItem()
    {
        return inventory[nowSelectItem];
    }

    /// <summary>
    /// 아이템들의 정보를 담고있는 인벤토리를 반환한다.
    /// </summary>
    /// <returns></returns>
    public Item[] ReturnInventory()
    {
        return inventory;
    }
    
}
