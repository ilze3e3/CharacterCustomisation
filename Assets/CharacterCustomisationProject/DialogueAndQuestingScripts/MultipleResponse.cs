using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Multiple Response", menuName = "Response/New Multiple Response")]
public class MultipleResponse : Response
{
    // Example Dialogue Do you like apples? Yes, I knew it / No, How could you not like apples
    public string baseResponse;
    public List<MultipleChoice> multipleChoiceResponse;
    public override bool IsMultiple()
    {
        return true;
    }

}
