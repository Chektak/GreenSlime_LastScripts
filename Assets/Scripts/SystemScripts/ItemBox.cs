using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour {

    public List<int> itemKeyList;
    public List<GameObject> itemObjects;

    [HideInInspector]
    public Dictionary<int, GameObject> itemList = new Dictionary<int, GameObject>();

    private void Awake()
    {
        int i=0;
        foreach(int index in itemKeyList)
        {
            itemList.Add(index, itemObjects[i]);
            i++;
         }
    }
}
