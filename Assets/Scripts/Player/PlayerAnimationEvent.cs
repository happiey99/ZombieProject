using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
  public void ReloadTrue()
    {
        Managers._charState.isReload = false;
    }
}
