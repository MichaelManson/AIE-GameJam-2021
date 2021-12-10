using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    [SerializeField] private Transform targetLimb;
    [SerializeField] private bool mirror;
    private ConfigurableJoint configJoint;

    private Quaternion targetInitialRotation;

    private void Awake()
    {
        configJoint = GetComponent<ConfigurableJoint>();
        Debug.Assert(configJoint != null, "configJoint is null!");
    }

    void Start()
    {
        targetInitialRotation = targetLimb.transform.localRotation;
    }

    private void FixedUpdate()
    {
        configJoint.targetRotation = CopyRotation();
    }

    private Quaternion CopyRotation()
    {
        return Quaternion.Inverse(targetLimb.localRotation) * targetInitialRotation;
    }
}
