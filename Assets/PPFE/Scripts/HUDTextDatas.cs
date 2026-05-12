using UnityEngine;

[System.Serializable]
public struct TextRow
{
    public int index;
    public string text;
    public int nextRowIndex;
    //public bool conditionEnabled;
    public bool IsFinished;
}

[CreateAssetMenu(fileName = "HUDTextDatas", menuName = "Scriptable Objects/HUDTextDatas")]
public class HUDTextDatas : ScriptableObject
{
    public TextRow[] textRow;
}
