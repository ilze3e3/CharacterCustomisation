using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Single Response", menuName = "Response/New Single Response")]
public class SingleResponse : Response
{
    public string sentence;
    public override bool IsMultiple()
    {
        return false;
    }
    public string GetSentence()
    {
        return sentence;
    }
}
