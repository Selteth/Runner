using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTableLogic : MonoBehaviour {
    private List<int> ScoreTable = new List<int>();
    public void Add(int res)
    {
        ScoreTable.Add(res);
    }

    public System.Collections.ObjectModel.ReadOnlyCollection<int>GetResults()
    {
        ScoreTable.Sort();
        return ScoreTable.AsReadOnly();
    }
}
