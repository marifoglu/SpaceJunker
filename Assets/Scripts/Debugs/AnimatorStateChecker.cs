using UnityEngine;

public class AnimatorStateChecker : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator == null) return;

        // Print ALL parameters
        Debug.Log("=== ANIMATOR PARAMETERS ===");

        // Check bools
        string[] boolParams = { "Idle", "Move","MovePre" };
        foreach (string param in boolParams)
        {
            if (HasParameter(param, AnimatorControllerParameterType.Bool))
            {
                Debug.Log($"{param}: {animator.GetBool(param)}");
            }
            else
            {
                Debug.Log($"{param}: PARAMETER DOES NOT EXIST");
            }
        }

        // Check current state
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log($"State Hash: {state.fullPathHash}");
        Debug.Log($"State Name Hash: {state.shortNameHash}");
        Debug.Log($"Is Idle: {animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")}");
        Debug.Log($"Is MovePre: {animator.GetCurrentAnimatorStateInfo(0).IsName("MovePre")}");
        Debug.Log($"Is Move: {animator.GetCurrentAnimatorStateInfo(0).IsName("Move")}");
        Debug.Log($"Normalized Time: {state.normalizedTime}");

        // Try to get the actual state name
        if (animator.IsInTransition(0))
        {
            Debug.Log("IS IN TRANSITION");
        }
    }

    bool HasParameter(string paramName, AnimatorControllerParameterType type)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName && param.type == type)
                return true;
        }
        return false;
    }
}