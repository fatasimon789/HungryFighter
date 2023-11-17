using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;


public class CameraMananger : MonoBehaviour
{
   public static CameraMananger instance;

    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCamera;

    [Header("Controls for lerping the Y damping during play falling")]
    [SerializeField] private float fallPanAmount = 0.25f;
    [SerializeField] private float fallYPanTime = 0.35f;
    public float _fallSpeedYDampingChangeThreshold = -15f;
 
    public bool IsLerpingYDamping { get; private set; }

    public bool LerpedFromPlayerFalling { get; set; }

    private Coroutine _LerpYPanCoroutine;
    private Coroutine _PanCameraCoroutine;

    [HideInInspector] public CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;

    private float normYPanAmount;

    private Vector3 _startingTrackedObjectOffset;
    private void Awake()
    {
        if (instance == null)//?
        {
            instance = this;
        }
        for (int i = 0; i < _allVirtualCamera.Length; i++) 
        {
            if (_allVirtualCamera[i].enabled) 
            {
               // set the current camera to active 
                _currentCamera = _allVirtualCamera[i];
                //set the framing transpoer
                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>(); //?
            }
        }
        //set the Y damping amount  so its base  on the ispector value
        normYPanAmount = _framingTransposer.m_YDamping;
     
        // set the  starting postion of the tracked from offset
        _startingTrackedObjectOffset = _framingTransposer.m_TrackedObjectOffset;
        
    }
    
    #region Lerp The Y damping
    public void LerpYDamping(bool isplayerFalling1) 
    {
        _LerpYPanCoroutine = StartCoroutine(LerpYAction(isplayerFalling1));
    }
    private IEnumerator LerpYAction(bool isplayerFalling) 
    {
        IsLerpingYDamping = true;
        // grab the  starting damping amount
        float startDampAmount = _framingTransposer.m_YDamping;
        float endDampAmount = 0f;

        //determind the end damping amount
        if(isplayerFalling) 
        {
            // if isplayerfalling endDamp = 0.25 , its meaning the end lerp from 2 to 0.25 END
            endDampAmount = fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else 
        {
            // if isplayerFalling = false  endDamp = defaul normYpan ( meaning 2)
            endDampAmount = normYPanAmount;
        }
        //lerp the  pan amount
        float elapstime = 0f;
        while(elapstime < fallYPanTime) 
        {
            // make time to deltatime for lerp from 2 to 0.25;
            elapstime += Time.deltaTime;
            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, elapstime/fallYPanTime);
            _framingTransposer.m_YDamping= lerpedPanAmount;
            yield return null;
        }
        
        IsLerpingYDamping= false;
    }
    #endregion

    #region Pan Camera
    public void PanCameraOnContact (float PanDistance,float PanTime,Pandirection panDirection,bool panToStartingPos) 
    {
        StartCoroutine(panCamera(PanDistance, PanTime, panDirection, panToStartingPos));
    }
    private IEnumerator panCamera (float PanDistance, float PanTime, Pandirection panDirection, bool panToStartingPos) 
    {
      Vector3 endPos = Vector2.zero;
      Vector3 startingPos = Vector2.zero;

      // handle pan from trigger
        if(!panToStartingPos) 
        {
            // set the  direction and distance 
             switch(panDirection) 
             {
                case Pandirection.Up:
                    endPos = new Vector3(0,1,0);
                    break;
                case Pandirection.Down:
                    endPos = new Vector3(0,-1,0);
                    break;
                case Pandirection.Left:
                    endPos = new Vector3(1, 0, 0);
                    break;
                case Pandirection.Right:
                    endPos = new Vector3(-1,0,0);
                    break;
                default:
                    break;
            }
            // add endpos = -10f  ( tùy vào lựa chọn hướng)
            endPos *= PanDistance;
            // add  startingpos =  vector pov camera ( now is 0 ,0 , -20)
            startingPos = _startingTrackedObjectOffset;
            // add Vector3 endpos = endPos Old ( meaning 0 , -10 , 0) + startingPos(meaning 0,0,-20)= 0 -10 -20
            endPos += startingPos;
        
        }
        // handle  the pan to back starting position
        else 
        {
            // 0 0 -20
            startingPos = _framingTransposer.m_TrackedObjectOffset;
            
            // 0 0 -20
            endPos = _startingTrackedObjectOffset;
            
        }
        // handle  the actual panning of the camera
        float elapstime = 0f;   
        while (elapstime < PanTime) 
        {
            elapstime += Time.deltaTime;
            // chạy từ startingpos đến endpos bằng time của elapstime / pantime
            Vector3 panLerp =  Vector3.Lerp(startingPos,endPos,(elapstime/PanTime));
            // pov góc nhìn vector 3 camera sẽ = panlerp tức là bị nghiêng theo hướng nào thì giữ y hướng đó
            // hoặc nếu else thì sẽ trở lại như cũ
            _framingTransposer.m_TrackedObjectOffset = panLerp;
           

            yield return null;
        }
    }
    #endregion
    #region Swap Camera
    public void SwapCamera(CinemachineVirtualCamera cameraFromLeft,CinemachineVirtualCamera cameraFromRight,Vector2 triggerExitDirection) 
    {
       // if curent camera  is on Left  and out trigger exit direction  was on right
        if(_currentCamera == cameraFromLeft && triggerExitDirection.x >0 ) 
        {
            // active the new camera
            cameraFromRight.enabled = true;
            // deactive the old camera
            cameraFromLeft.enabled = false;
            //set the new camera as the current camera
            _currentCamera = cameraFromRight;
            //update our composer variable
            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            Debug.Log("Transcamera NEW");
        }
        // if curent camera  is on Right  and out trigger exit direction  was on Left

        // give back camera when player go exit colider
        // 2 type to camera change ( 1 is  on trigger 2 gate type 69 , 2 on trigger change camera and back when out trigger)
        else if (_currentCamera == cameraFromRight && triggerExitDirection.x < 0) 
        {
            cameraFromRight.enabled = false;

            cameraFromLeft.enabled = true;

            _currentCamera = cameraFromLeft;

            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            Debug.Log("TransCamera BACK");
        }
    }
    
    #endregion
}
