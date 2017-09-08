using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MenuPanelManager : MonoBehaviour
{
    #region Para
    private Vector3 _hidePosition;
    private bool _isMenuMoveing;
    public bool IsMenuMoving { get { return _isMenuMoveing; } }
    public Canvas HudCanvas;
    public Transform MenuTransform;


    #endregion

    #region UnityInternalcall

    // Use this for initialization
    void Start()
    {
        float positionY = HudCanvas.GetComponent<RectTransform>().sizeDelta.y +
                          MenuTransform.GetComponent<RectTransform>().sizeDelta.y;

        _hidePosition = new Vector3(0, positionY, 0);
        MenuTransform.DOLocalMove(_hidePosition, 0);
        _isMenuMoveing = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion


    #region Interface

    public void Pause()
    {
        _isMenuMoveing = true;
        MenuTransform.DOLocalMove(Vector3.zero, 1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                _isMenuMoveing = false;
            });
    }

    public void Continue()
    {
        _isMenuMoveing = true;
        MenuTransform.DOLocalMove(_hidePosition, 1f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                _isMenuMoveing = false;
            });
    }

    #endregion

    #region InternalCall
    


    #endregion
}
