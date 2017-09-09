using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanelManager : MonoBehaviour
{
    #region Para
    private Vector3 _hidePosition;
    private bool _isMenuMoveing;
    public GameObject DataHelperGameObject;
    public bool IsMenuMoving { get { return _isMenuMoveing; } }
    public Canvas HudCanvas;
    public Transform MenuTransform;
    public Transform GameOverMenuTransform;


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

    public void GameOver()
    {
        GameOverMenuTransform.DOScale(1, 0.5f);
        foreach (var image in GameOverMenuTransform.GetComponentsInChildren<Image>())
        {
            image.DOFade(1, 0.5f);
        }
        foreach (var text in GameOverMenuTransform.GetComponentsInChildren<Text>())
        {
            text.DOFade(1, 0.5f);
        }
    }

    #endregion

    #region InternalCall



    #endregion

    #region OnClickBtnFunc

    public void OnClickReStartBtn()
    {
        DataHelperGameObject.GetComponent<DataTransmitHelper>().Reset();
        SceneManager.LoadScene(1);
    }

    public void OnClickReturnToStartBtn()
    {
        DataHelperGameObject.GetComponent<DataTransmitHelper>().Reset();
        SceneManager.LoadScene(0);
    }

    public void OnClickExitBtn()
    {
        Application.Quit();
    }

    public void OnClickQuitBtn()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<MainSceneGameManager>().GameOver();
    }

    public void OnClickContinueBtn()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<MainSceneGameManager>().Continue();
    }

    public void OnClickOKBtn()
    {
        SceneManager.LoadScene(2);
    }

    #endregion
}

