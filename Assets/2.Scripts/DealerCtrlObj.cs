using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DealerCtrlObj : MonoBehaviour
{
    List<CardCtrlObj> _cards;
    [SerializeField]List<Vector2> _spreadPos;

    [SerializeField] Vector2Int _maxCount;
    [SerializeField]int _maxSpawnCardNum = 3;

    IEnumerator SpreadCard()
    {
        _cards = new List<CardCtrlObj>();
        for (int i = 0; i < _maxSpawnCardNum; i++)
        {
            GameObject go = Resources.Load("Prefabs/CardObj") as GameObject;
            go = Instantiate(go, GetComponent<RectTransform>());

            _cards.Add(go.GetComponent<CardCtrlObj>());
            Debug.Log(go.name);
            //카드 배치
            RectTransform rectTransform = go.GetComponent<RectTransform>();
            
            int num = Random.Range(0, _spreadPos.Count);
            //do
            //{
            //    rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, _spreadPos[num], 0.1f * Time.deltaTime);
            //    yield return new WaitForSeconds(1f);
            //}
            //while (Vector2.Distance(rectTransform.anchoredPosition, _spreadPos[num]) < 0.001f);
            rectTransform.anchoredPosition = _spreadPos[num];
            _spreadPos.Remove(_spreadPos[num]);
            yield return new WaitForSeconds(1f);    
        }
    }
    private void Awake()
    {
        RectTransform ATrans = transform.GetChild(0) as RectTransform;
        RectTransform BTrans = transform.GetChild(1) as RectTransform;
        float xDistance = (BTrans.anchoredPosition.x - ATrans.anchoredPosition.x)/_maxCount.x;
        float yDistance = (BTrans.anchoredPosition.y - ATrans.anchoredPosition.y)/ _maxCount.y;
        _spreadPos = new List<Vector2>();
        float posX = ATrans.anchoredPosition.x, posY = ATrans.anchoredPosition.y;
        for (int i = 0; i < _maxCount.x; i++)
        {
            for (int j = 0; j < _maxCount.y; j++)
            {
                _spreadPos.Add(new Vector2(posX, posY));
                posY += yDistance;
            }
            posX += xDistance;
        }
    }



    public void RemoveCards()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards.Remove(_cards[i]);
        }
        _cards = new List<CardCtrlObj>();
    }
    private void OnGUI()
    {
        if(GUI.Button(new Rect(0,0, 120, 40), "Make Card"))
        {
            StartCoroutine("SpreadCard");
            Debug.Log("card");
        }

        if (GUI.Button(new Rect(0, 40, 120, 40), "Make Card"))
        {
            StartCoroutine("RemoveCards");
        }
    }
}
