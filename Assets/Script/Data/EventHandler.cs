using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler
{
    public static event Action<InventoryLocation, List<InventoryItem>> UpdateInventoryUI;
    public static void CallUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
    {
        UpdateInventoryUI?.Invoke(location,list);
    }
    public static event Action<string, Vector3> TranslationEvent;
    public static void CallTranslationEvent(string sceneName,Vector3 pos)
    {
        TranslationEvent?.Invoke(sceneName, pos);
    }

    public static event Action BeforeSceneUnLoadEvent;
    public static void CallBeforeSceneUnLoadEvent()
    {
        BeforeSceneUnLoadEvent?.Invoke();
    }

    public static event Action AfterSceneUnLoadEvent;
    public static void CallAfterSceneUnLoadEvent()
    {
        AfterSceneUnLoadEvent?.Invoke();
    }

    public static event Action<Vector3> MoveToPosition;
    public static void CallMoveToPosition(Vector3 targetPosition)
    {
        MoveToPosition?.Invoke(targetPosition);
    }

    public static event Action<SlotType, InventoryBag_SO> BagOpenEvent;
    public static void CallBagOpenEvent(SlotType slotType,InventoryBag_SO bag_SO)
    {
        BagOpenEvent?.Invoke(slotType, bag_SO);
    }

    public static event Action<SlotType, InventoryBag_SO> BagCloseEvent;
    public static void CallBagCloseEvent(SlotType slotType, InventoryBag_SO bag_SO)
    {
        BagCloseEvent?.Invoke(slotType, bag_SO);
    }

    public static event Action<ItemDetails> ShowTradeUI;
    public static void CallShowTradeUI(ItemDetails item)
    {
        ShowTradeUI?.Invoke(item);
    }
}
