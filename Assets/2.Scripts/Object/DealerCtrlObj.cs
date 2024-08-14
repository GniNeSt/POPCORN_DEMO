using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DealerCtrlObj : MonoBehaviour
{
    [SerializeField] RectTransform ATrans, BTrans;
    [SerializeField] List<CardCtrlObj> _cards;
    [SerializeField] List<Vector2> _spreadPos;

    [SerializeField] Vector2Int _maxCount;
    [SerializeField] int _maxSpawnCardNum = 3;

    IEnumerator SpreadCard()
    {
        CreateRandomArea();
        _cards = new List<CardCtrlObj>();
        for (int i = 0; i < _maxSpawnCardNum; i++)
        {
            GameObject go = Resources.Load("Prefabs/CardObj") as GameObject;
            go = Instantiate(go, GetComponent<RectTransform>());
            CardCtrlObj cco = go.GetComponent<CardCtrlObj>();

            //카드 배치
            RectTransform rectTransform = go.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            int num = Random.Range(0, _spreadPos.Count);
            int cardNum = InGameManager._instance.GetNextRandomBinary();
            cco.InitSet(string.Format("{0}", cardNum), _spreadPos[num]);
            _spreadPos.Remove(_spreadPos[num]);


            _cards.Add(cco);

            yield return new WaitForSeconds(0.5f);
        }
        foreach (CardCtrlObj cco in _cards)
        {
            InGameManager._instance.AddCardList(cco);
            cco.CardFlip();
        }
        InGameManager._instance.SetGameStatus(InGameManager.InGameStatus.InGame);
    }
    public void CreateRandomArea()
    {
        float xDistance = (BTrans.anchoredPosition.x - ATrans.anchoredPosition.x) / _maxCount.x;
        float yDistance = (BTrans.anchoredPosition.y - ATrans.anchoredPosition.y) / _maxCount.y;
        Vector2 cardBoardPos = transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
        _spreadPos = new List<Vector2>();
        float posX = ATrans.anchoredPosition.x, posY = ATrans.anchoredPosition.y;
        for (int i = 0; i < _maxCount.x; i++)
        {
            for (int j = 0; j < _maxCount.y; j++)
            {
                _spreadPos.Add(cardBoardPos + new Vector2(posX, posY));
                posY += yDistance;
            }
            posX += xDistance;
            posY = ATrans.anchoredPosition.y;
        }
    }
    public void RemoveCards()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].SetState(3);
        }
        _cards = new List<CardCtrlObj>();
        InGameManager._instance.AddCardList(null, true);
    }
    public void SubmitBtnDown()
    {
        if(InGameManager._instance._InGameStatus == InGameManager.InGameStatus.InGame)
            InGameManager._instance.FindResult();
        else
        {
            Debug.Log("정답 제출 불가");
        }
    }
    public void TurnStart()
    {
        StartCoroutine("SpreadCard");
    }
    private void Awake()
    {
        InGameManager._instance._dialogClickEvent = true;
        InGameManager._instance.SetGameStatus(InGameManager.InGameStatus.Start);
    }
    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(0, 0, 120, 40), "Make Card"))
    //    {
    //        Debug.Log("card");
    //        StartCoroutine("SpreadCard");
    //    }
    //}
}
