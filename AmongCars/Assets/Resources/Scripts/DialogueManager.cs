using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class DialogueManager : MonoBehaviour
{
    public AmongCars controls;

    public NPC npc;
    public TMP_Text npcName;
    public Image npcImage;

    public GameObject player;

    Queue<string> sentences;

    //public GameObject dialoguePanel;
    public TMP_Text displayText;

    public GameObject optionsPanel;

    public Button button1;
    public Button button2;
    public Button button3;

    public Button btOut;

    public Text op1;
    public Text op2;
    public Text op3;

    string activeSentence;
    public float typingSpeed;

    AudioSource myAudio;

    void Start()
    {
        sentences = new Queue<string>();
        myAudio = GetComponent<AudioSource>();
    }

    protected void Awake() => controls = new AmongCars();
    protected void OnEnable() => controls?.Enable();
    protected void OnDisable() => controls?.Disable();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            ObjectInteraction.ShowIcon(true);
            ObjectInteraction.ShowDialog(true);
            npcName.text = npc.name;
            string url = "Sprites/" + npc.icon;
            npcImage.sprite = Resources.Load<Sprite>(url);
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        print("Enter: " + npc.GetRound());
        sentences.Clear();
        if(npc.GetRound() == 1)
        {
            foreach (string sentence in npc.sentences1)
            {
                sentences.Enqueue(sentence);
            } 
            DisplayNextSentence();
        }
        else if(npc.GetRound() == 2)
        {
            print("entro ronda 2");
            foreach (string sentence in npc.sentences2)
            {
                sentences.Enqueue(sentence);
            } 
            DisplayNextSentence();
        } 
        
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            displayText.text = activeSentence;
            return;
        }

        activeSentence = sentences.Dequeue();
        //displayText.text = activeSentence;

        StopAllCoroutines();
        StartCoroutine(TypeTheSentence(activeSentence));
    }

    IEnumerator TypeTheSentence(string sentence)
    {
        displayText.text = "";
        myAudio.Play();
        foreach (char letter in sentence.ToCharArray())
        {
            ObjectInteraction.ShowDialog(true);
            displayText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        myAudio.Stop();
        ShowOptions();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (controls.Player.Option1.ReadValue<float>() == 1)
            {
                ChooseOption(1);
            }
            if (controls.Player.Option2.ReadValue<float>() == 1)
            {
                ChooseOption(2);
            }
            if (controls.Player.Option3.ReadValue<float>() == 1)
            {
                ChooseOption(3);
            }

            if (optionsPanel.activeInHierarchy == false)
            {
                btOut?.onClick.AddListener(() => ClosePanel());
                if (controls.Player.Salir.ReadValue<float>() == 1)
                {
                    ClosePanel();
                    StopAllCoroutines();
                }
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAudio.Stop();
            ClosePanel();
            if(npc.GetRound() == 1) {
                npc.SetRound(2);
            }
            else if(npc.GetRound() == 2) {
                npc.SetRound(1);
            }
            print("Exit de " + npc.name + ": " + npc.GetRound());
            StopAllCoroutines();
        }
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
        string[] options = npc.GetOptions();
         if(npc.GetRound() == 1) {
            op1.text = options[0];
            op2.text = options[1];
            op3.text = options[2];
        }
        else {
            op1.text = options[3];
            op2.text = options[4];
            op3.text = options[5];
        }
    }

    private void ChooseOption(int button)
    {
        switch (button)
        {
            case 1:
                NextSentence(1);
                break;
            case 2:
                NextSentence(2);
                break;
            case 3:
                NextSentence(3);
                break;
        }
    }

    private void NextSentence(int curResponseTracker)
    {
        displayText.text = npc.GetSentence(curResponseTracker);
        optionsPanel.SetActive(false);
    }

    private void ClosePanel()
    {
        ObjectInteraction.ShowDialog(false);
        optionsPanel.SetActive(false); 
    }

}





