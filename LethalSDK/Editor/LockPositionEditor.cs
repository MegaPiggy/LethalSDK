using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LockPosition))]
public class LockPositionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LockPosition lockPosition = (LockPosition)target;
        if (lockPosition.transform.position != lockPosition.initialPosition)
        {
            lockPosition.transform.position = lockPosition.initialPosition;
        }
    }
}
