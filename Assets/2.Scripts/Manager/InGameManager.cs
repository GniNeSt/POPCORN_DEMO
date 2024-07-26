using System.Collections.Generic;
using UnityEngine;

public class InGameManager : TSingleTon<InGameManager>
{
    [SerializeField]List<int> _binaryNum;
    int _binaryCellCount = 8;

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
    }
}
