using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Net.Mime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text questionPanel;
    [SerializeField]
    private Text prizeField;
    [SerializeField]
    private Text option1;
    [SerializeField]
    private Text option2;
    [SerializeField]
    private Text option3;
    [SerializeField]
    private Text option4;
    [SerializeField]
    private Button fifty;
    [SerializeField]
    private Button callAFriend;
    [SerializeField]
    private Button publicVote;
    [SerializeField]
    private Text hint;

    private string correctAnswer;
    private int correctButton;
    
    public static string prize;

    TextAsset xmlTextAsset;
    XmlDocument xmlDoc;

    private int difficulty; 
    private int question; 

    void Start()
    {
        // Load the XML file
        xmlTextAsset = Resources.Load<TextAsset>("questions");
        xmlDoc = new XmlDocument();
        
        xmlDoc.LoadXml(xmlTextAsset.text);

        difficulty = 0;
        question = 0;
        
        
        IncreaseLevel();
    }

    void IncreaseLevel() {
        hint.text = "";
        System.Random rnd = new System.Random();
        question = rnd.Next(0, 3);

        XmlNodeList difficultyList = xmlDoc.GetElementsByTagName("difficulty");
        // Get the text of the question
        XmlAttributeCollection attList = difficultyList[difficulty].ChildNodes[question].Attributes;
        string questionText = attList[0].InnerText;
        
        prize = difficultyList[difficulty].Attributes["prize"].InnerText;

        questionPanel.text = questionText;
        // Get the answer choices for the question
        XmlNodeList answerList = difficultyList[difficulty].ChildNodes[question].ChildNodes;

        if (answerList[0].Attributes["score"].InnerText == "1") {
            correctAnswer = answerList[0].InnerText;
            correctButton = 1;
        }
        if (answerList[1].Attributes["score"].InnerText == "1") {
            correctAnswer = answerList[1].InnerText;
            correctButton = 2;
        }
        if (answerList[2].Attributes["score"].InnerText == "1") {
            correctAnswer = answerList[2].InnerText;
            correctButton = 3;
        }
        if (answerList[3].Attributes["score"].InnerText == "1") {
            correctAnswer = answerList[3].InnerText;
            correctButton = 4;
        }
        option1.text = answerList[0].InnerText;
        option2.text = answerList[1].InnerText;
        option3.text = answerList[2].InnerText;
        option4.text = answerList[3].InnerText;
        
        UnityEngine.Debug.Log(correctAnswer);

        if (difficulty <= 13) {
            difficulty++;
        } else {
            SceneManager.LoadScene("EndScene");
        }
    }

    void Update()
    {
        
    }
    public void onOption1Click() {
        if (option1.text == correctAnswer) {
            prizeField.text = "Печалба: " + prize;
            IncreaseLevel();
        } else {
            SceneManager.LoadScene("EndScene");
        }
    }
    public void onOption2Click() {

        if (option2.text == correctAnswer) {
            prizeField.text = "Печалба: " + prize;
            IncreaseLevel();
        } else {
            SceneManager.LoadScene("EndScene");
        }
    }
    public void onOption3Click() {
        if (option3.text == correctAnswer) {
            prizeField.text = "Печалба: " + prize;
            IncreaseLevel();
        } else {
            SceneManager.LoadScene("EndScene");
        }
    }
    public void onOption4Click() {
        if (option4.text == correctAnswer) {
            prizeField.text = "Печалба: " + prize;
            IncreaseLevel();
        } else {
            SceneManager.LoadScene("EndScene");
        }
    }
    public void onFiftyFiftyPressed() {
        if (correctButton == 1) {
            option2.text = "";
            option4.text = "";
        }
        if (correctButton == 2) {
            option1.text = "";
            option3.text = "";
        }
        if (correctButton == 3) {
            option1.text = "";
            option2.text = "";
        }
        if (correctButton == 4) {
            option2.text = "";
            option3.text = "";
        }
        fifty.gameObject.SetActive(false);
    }
    public void onCallAFriendPressed() {
        hint.text = "Мисля, че отговорът е " + correctAnswer;
        callAFriend.gameObject.SetActive(false);
    }
    public void onPublicVotePressed() {
        System.Random rnd = new System.Random();
        int correct = rnd.Next(60, 70);
        int rest = 100 - correct;
        int rest1 = 0;
        int rest2 = 0;
        int rest3 = 0;
        for (int i = 0; i < rest; i ++) {
            int k = rnd.Next(0, 3);
            if (k == 0) rest1 ++;
            if (k == 1) rest2 ++;
            if (k == 2) rest3 ++;
        }
        if (correctButton == 1) {
            hint.text = "A: " + correct.ToString() + " B: " + rest1.ToString() + " C: " + rest2.ToString() + " D: " + rest3.ToString();
        }
        if (correctButton == 2) {
            hint.text = "A: " + rest1.ToString() + " B: " + correct.ToString() + " C: " + rest2.ToString() + " D: " + rest3.ToString();
        }
        if (correctButton == 3) {
            hint.text = "A: " + rest2.ToString() + " B: " + rest1.ToString() + " C: " + correct.ToString() + " D: " + rest3.ToString();
        }
        if (correctButton == 4) {
            hint.text = "A: " + rest3.ToString() + " B: " + rest1.ToString() + " C: " + rest2.ToString() + " D: " + correct.ToString();
        }
        publicVote.gameObject.SetActive(false);
    }
} 