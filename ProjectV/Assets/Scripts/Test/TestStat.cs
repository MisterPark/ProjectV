using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum TestType
{
    AAA,
    BBB,
    CCC
}

[System.Serializable]
public class TestStatElement
{
    public string name;
    public TestType type;
    public float origin;
    public float growth;
    public float final;
}
public class TestStat : MonoBehaviour
{
    [SerializeField]List<TestStatElement> elements = new List<TestStatElement>();

}
