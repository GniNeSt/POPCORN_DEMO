using System.Collections.Generic;
using UnityEngine;

public class InGameManager : TSingleTon<InGameManager>
{
    [SerializeField]int _result;
    [SerializeField]List<int> _binaryNum;
    List<int> _cardsNum;
    int[] _binarySetting;
    int _binaryCellCount = 8;
    public enum InGameStatus
    {
        InGame,

        None
    }
    public int _curResult
    {
        get { return _result; }
    }
    public void setBinarySetting(int place, bool isOn = true)
    {
        if(isOn)
            _binarySetting[place] = 1;
        else
            _binarySetting[place] = 0;

        _result = 0;
        for(int i = 0; i < _binarySetting.Length; i++)
        {
            _result += _binarySetting[i] * (int)Mathf.Pow(2, i);
        }
    }
    public void AddCardList(int num, bool reset = false)
    {
        if(reset)_cardsNum = new List<int>();
        else
            _cardsNum.Add(num);
    }
    public int GetNextRandomBinary()
    {
        int order = Random.Range(0, _binaryNum.Count);
        int result = _binaryNum[order];
        _binaryNum.Remove(result);
        return result;
    }
    public void SetBinaryNum()
    {
        _binaryNum = new List<int>();
        for (int i = 0; i < (int)Mathf.Pow(2, _binaryCellCount); i++)
        {
            _binaryNum.Add(i);
        }
    }
    protected override void Init()
    {
        SetBinaryNum();
        _cardsNum = new List<int>();
        _binarySetting = new int[_binaryCellCount];
        _result = 0;
    }
    private void Update()
    {
        
    }
}
