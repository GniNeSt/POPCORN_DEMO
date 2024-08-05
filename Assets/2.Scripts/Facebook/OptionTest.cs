using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OptionTest : MonoBehaviour
{
    [SerializeField] TMP_Dropdown _resolutionList;
    [SerializeField] Toggle _windowToggle;
    [SerializeField]Camera _mapCam;
    [SerializeField] Slider _speedSlider;
    string _debugMsg;

    [SerializeField] float _orthoZoomSpeed = 2.0f, _perspectiveZoomSpeed = 2.0f;

    bool _windowVisible = false;
    float _lastWidth, _lastHeight;
    TMP_Dropdown.OptionData _lastOptionData;
    


    private void Start()
    {
        //string msg = string.Format("�ػ� : {0} X {1}",Screen.width, Screen.height);
        //Debug.Log(msg);

        Resolution[] res = Screen.resolutions;
        for(int n = 0; n < res.Length; n++)
        {
            string msg = string.Format("�ػ� : {0} X {1}, {2}Hz", res[n].width, res[n].height, res[n].refreshRate);
            TMP_Dropdown.OptionData Data = new TMP_Dropdown.OptionData(msg);
            
            if(_lastWidth == res[n].width && _lastHeight == res[n].height)
            {
                _resolutionList.options.Remove(_lastOptionData);
            }
            _resolutionList.options.Add(Data);
            _lastWidth = res[n].width;
            _lastHeight = res[n].height;
            _lastOptionData = Data;
            if (Screen.width == res[n].width && Screen.height == res[n].height)
            {
                _resolutionList.value = n;
            }
            //Debug.Log(msg);

        }
    }

    private void Update()
    {
#if UNITY_ANDROID
        if(Input.touchCount > 1)
        {
            Touch first = Input.GetTouch(0);
            Touch second = Input.GetTouch(1);
            //Vector2 firstDirec = first.position - first.deltaPosition;
            //Vector2 secondDirec = second.position - second.deltaPosition;

            //float posMag = (firstDirec - secondDirec).magnitude;
            //float deltaPosMag = (first.deltaPosition - second.deltaPosition).magnitude;

            //float distanceMove = deltaPosMag - posMag;

            Vector2 touchFirstPrevPos = first.position - first.deltaPosition;
            Vector2 touchsecondPrevPos = second.position - second.deltaPosition;
            //�� �����ӿ��� ��ġ ������ ���� �Ÿ��� ����
            float prevTouchDeltaMag = (touchFirstPrevPos - touchsecondPrevPos).magnitude;
            float touchDeltaMag = (first.position - second.position).magnitude;
            //�Ÿ� ���� ����(�Ÿ��� ���� ���� ũ�� �հ����� ���� ����/���� ����)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if (_mapCam.orthographic)
            {
                //_debugMsg = "" + Input.touchCount + "orthographic" + distanceMove;
                //_mapCam.orthographicSize += distanceMove * _orthoZoomSpeed * Time.deltaTime;
                //_mapCam.orthographicSize = Mathf.Max(_mapCam.orthographicSize, 0.1f);

                _mapCam.orthographicSize += deltaMagnitudeDiff * _orthoZoomSpeed;
                _mapCam.orthographicSize = Mathf.Max(_mapCam.orthographicSize, 0.1f);
            }
            else
            {//perspective
                //_debugMsg = "" + Input.touchCount + "perspective" + distanceMove;
                //_mapCam.fieldOfView += distanceMove * _perspectiveZoomSpeed * Time.deltaTime;
                //_mapCam.fieldOfView = Mathf.Clamp(_mapCam.fieldOfView, 0.1f, 179.9f);
                _mapCam.fieldOfView += deltaMagnitudeDiff * _perspectiveZoomSpeed;
                _mapCam.fieldOfView = Mathf.Clamp(_mapCam.fieldOfView, 0.1f, 179.9f);
            }
        }
#endif
    }
    public void clickApplyButton()
    {
#if UNITY_STANDALONE
        int num = _resolutionList.value;
        Resolution res = Screen.resolutions[num];
        Screen.SetResolution(res.width, res.height, _windowVisible, res.refreshRate);
#endif
    }
    public void WindowToggle()
    {
        _windowVisible = !_windowToggle.isOn;
        Debug.Log(_windowVisible);
    }
    public void SliderSpeedChange()
    {
        _orthoZoomSpeed = _perspectiveZoomSpeed = _speedSlider.value;
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(0,0, 140,40),_debugMsg);
    }
}
