using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class CameraTriggerController : MonoBehaviour
{
    public CustomerInspectorObject _customInspectorObject;
    private Collider2D collider2d;

    private void Start()
    {
        collider2d= GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            if (_customInspectorObject.panCameraOnContact)
            {
                // pan the camera
             //   CameraMananger.instance._currentCamera.m_Lens.FieldOfView = 125f;
                CameraMananger.instance.PanCameraOnContact(_customInspectorObject.panDistance, _customInspectorObject.panTime
                                                          , _customInspectorObject.panDirection, false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 exitDirection = (collision.transform.position - collider2d.bounds.center).normalized;
            if (_customInspectorObject.swapCamera && _customInspectorObject._virtualCameraLeft != null && _customInspectorObject._virtualCameraRight != null) 
            {
               CameraMananger.instance.SwapCamera(_customInspectorObject._virtualCameraLeft,_customInspectorObject._virtualCameraRight,exitDirection);
            }
            if (_customInspectorObject.panCameraOnContact)
            {
                // pan the camera
                CameraMananger.instance.PanCameraOnContact(_customInspectorObject.panDistance, _customInspectorObject.panTime
                                                      , _customInspectorObject.panDirection, true);
            }
        }
    }
}
[System.Serializable]
public class CustomerInspectorObject
{
    public bool swapCamera = false;
    public bool panCameraOnContact = false;
    [HideInInspector] public CinemachineVirtualCamera _virtualCameraLeft;
    [HideInInspector] public CinemachineVirtualCamera _virtualCameraRight;

    [HideInInspector] public Pandirection panDirection;
    [HideInInspector] public float panDistance = 10f;
    [HideInInspector] public float panTime = 0.35f;

 
}
public enum Pandirection 
{
  Up,
  Down,
  Left,
  Right,
}
// Make a custom  spector type from cametriggercontroller same as custominspectecObject
[CustomEditor(typeof(CameraTriggerController))]
public class MyScriptEditor : Editor 
{
    CameraTriggerController _cameraTriggerController;
    private void OnEnable()
    {
        // turn on target this class to spector same as custominspectorobject because  cameracontrollertriger ref it
        _cameraTriggerController = (CameraTriggerController)target;
    }
    // override in spector on this script 
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(_cameraTriggerController._customInspectorObject.swapCamera) 
        {
            _cameraTriggerController._customInspectorObject._virtualCameraLeft = EditorGUILayout.
             ObjectField("Camera on Left", _cameraTriggerController._customInspectorObject._virtualCameraLeft,
             typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
            
            _cameraTriggerController._customInspectorObject._virtualCameraRight = EditorGUILayout.
              ObjectField("Camera on Right", _cameraTriggerController._customInspectorObject._virtualCameraRight,
              typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
        }
        if(_cameraTriggerController._customInspectorObject.panCameraOnContact) 
        {
            _cameraTriggerController._customInspectorObject.panDirection = (Pandirection)EditorGUILayout.EnumPopup
            ("Camera Pan Direction", _cameraTriggerController._customInspectorObject.panDirection);

            _cameraTriggerController._customInspectorObject.panDistance = EditorGUILayout.FloatField("Pan Distance",
            _cameraTriggerController._customInspectorObject.panDistance);

            _cameraTriggerController._customInspectorObject.panTime = EditorGUILayout.FloatField("Pan Time",
             _cameraTriggerController._customInspectorObject.panTime);
        }
        if(GUI.changed) 
        {
         EditorUtility.SetDirty(_cameraTriggerController);
        }
    }
}

