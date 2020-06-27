using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "D-Knights/Questions")]
public class Question : ScriptableObject
{
    public enum QuestionTopic
    {
        NUMBERS,
        OPERATIONS,
        DECIMALNUMBERS,
        GEOMETRY,
        TIME,
        NUMBERTHEORY,
        FRACTIONS,
        ALGEBRA,
        DATAINTERCEPTION
    }

    public enum QuestionType
    {
        MULTIPLECHOICE,
        DEFINITION
    }

    public QuestionTopic questionTopic;
    public QuestionType questionType;
    [TextArea]
    public string question;
    public bool usePictureQuestion;
    [ConditionalField("usePictureQuestion")]public Sprite pictureQuestion;
    public string answer;
}
