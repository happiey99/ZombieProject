using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }


    CharStateManager CharState = new CharStateManager();
    CharInfoManager CharInfo = new CharInfoManager();
    InventoryManager Inven = new InventoryManager();
    EquipManager Equip = new EquipManager();
    ItemList Item = new ItemList();

    public static CharStateManager _charState { get { return Instance.CharState; } }
    public static CharInfoManager _charInfo { get { return Instance.CharInfo; } }
    public static ItemList _item { get { return Instance.Item; } }
    public static InventoryManager _inven { get { return Instance.Inven; } }
    public static EquipManager _equip { get { return Instance.Equip; } }

    private void Update()
    {
        Init();
    }


    static void Init()
    {

        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Manager");

            if (go == null)
            {
                go = new GameObject { name = "@Manager" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);

            s_instance = go.GetComponent<Managers>();

            s_instance.CharInfo.Init();
            s_instance.CharState.Init();
            s_instance.Inven.Init();
            s_instance.Item.Init();
        }
    }


}
