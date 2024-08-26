using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionUI : MonoBehaviour
{
    public TextMeshProUGUI QuestionLabel;

    public TextMeshProUGUI Answer1Label;

    public TextMeshProUGUI Answer2Label;

    public TextMeshProUGUI Answer3Label;

    public TextMeshProUGUI Answer4Label;

    public void PopulateQuestion(MultipleChoiceQuestionWithFiveOptions questionModel){

        QuestionLabel.text = questionModel.GetCaption();
        Answer1Label.text = questionModel.GetOptionByIndex(1).Value();
        Answer2Label.text = questionModel.GetOptionByIndex(2).Value();
        Answer3Label.text = questionModel.GetOptionByIndex(3).Value();
        Answer4Label.text = questionModel.GetOptionByIndex(4).Value();
    }
}
