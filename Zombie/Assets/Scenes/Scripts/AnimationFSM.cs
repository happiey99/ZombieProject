using UnityEngine;

public abstract class AnimationFSM : MonoBehaviour
{
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}
