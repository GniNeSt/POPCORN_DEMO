using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CardCtrlObj : MonoBehaviour
{
    TextMeshProUGUI _numTMP;
    Animator _animator;
    Image _img;

    [SerializeField] Vector2 _moveTarget;
    [SerializeField] float _speed = 4;
    [SerializeField] Sprite[] _cardImg;
    Vector3 _randomRotateValue;
    RectTransform _rectTrans;
    CardState _curState;
    public enum CardState
    {
        Spawn,
        Flip,
        Idle,



        REMOVE
    }
    public int _cardNum
    {
        get { return int.Parse(_numTMP.text); }
    }
    public void InitSet(string numText, Vector2 dir)
    {
        _curState = CardState.Spawn;
        _numTMP = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        _numTMP.text = numText;
        _img = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        _img.sprite = _cardImg[0];
        _rectTrans = GetComponent<RectTransform>();
        _animator = GetComponent<Animator>();
        _numTMP.enabled = false;

        _randomRotateValue = new Vector3(0, 0, Random.Range(-10f, 10f));

        _moveTarget = dir;
    }
    private void Update()
    {
        switch (_curState)
        {
            case CardState.Spawn:
                if (Vector2.Distance(_rectTrans.anchoredPosition, _moveTarget) > 0.5f)
                {
                    _rectTrans.anchoredPosition = Vector2.Lerp(_rectTrans.anchoredPosition, _moveTarget, _speed * Time.deltaTime);
                    _rectTrans.rotation = Quaternion.Lerp(_rectTrans.rotation, Quaternion.Euler(_randomRotateValue), _speed * Time.deltaTime);
                }
                break;
            case CardState.Flip:
                break;
            case CardState.Idle:
                break;
            case CardState.REMOVE:
                Color color = _img.color;
                if(color.a > 0.001f)
                {
                    color.a -=Time.deltaTime * 2f;
                    _img.color = color;
                    _numTMP.color = color;
                }
                else Destroy(gameObject);
                break;
        }
    }
    public void CardFlip()
    {
        _curState = CardState.Flip;
        _animator.SetTrigger("Flip");
    }
    public void CardFlipToFront()
    {
        _img.sprite = _cardImg[1];
        _numTMP.enabled = true;
    }
    public void SetState(int stateNum)
    {
        _curState = (CardState)stateNum;
        
    }
}
