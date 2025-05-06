using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class NewBehaviourScript : MonoBehaviour
{
    public List<Slot_MenuBox> lsSlots;


    //Odin
    [Button("Btn SetUp", ButtonSizes.Large)]
    void SetUp()
    {
        for(int i = 1; i <= lsSlots.Count; i++)
        {
            lsSlots[i-1].txtLevel.text = i.ToString();
            lsSlots[i - 1].iD = i;
        }
    }

}
