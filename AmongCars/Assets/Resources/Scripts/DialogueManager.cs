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

    public GameObject dialoguePanel;
    public TMP_Text displayText;

    public GameObject optionsPanel;

    public int curResponseTracker = 0;

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
    public AudioClip speakSound;

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
            print("Prueba");
            player = other.gameObject;
            dialoguePanel.SetActive(true);
            npcName.text = npc.name;
            string url = "Sprites/" + npc.icon;
            npcImage.sprite = Resources.Load<Sprite>(url);
            StartDialogue();

        }
    }

    void StartDialogue()
    {
        sentences.Clear();
        foreach (string sentence in npc.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            displayText.text = activeSentence;
            return;
        }

        activeSentence = sentences.Dequeue();
        displayText.text = activeSentence;

        StopAllCoroutines();
        StartCoroutine(TypeTheSentence(activeSentence));
    }

    IEnumerator TypeTheSentence(string sentence)
    {
        displayText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            displayText.text += letter;
            //myAudio.PlayOneShot(speakSound);
            yield return new WaitForSeconds(typingSpeed);
        }

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
                //button2.AddListener(()=>ChooseOption(2));
            }
            if (controls.Player.Option3.ReadValue<float>() == 1)
            {
                ChooseOption(3);
                //button3.AddListener(()=>ChooseOption(3));
            }
            //button1.onClick.AddListener(() => ChooseOption(1));
            //button2.onClick.AddListener(() => ChooseOption(2));
            //button3.onClick.AddListener(() => ChooseOption(3));

            if (optionsPanel.activeInHierarchy == false)
            {
                btOut.onClick.AddListener(() => ClosePanel());
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
            ClosePanel();
            StopAllCoroutines();
        }
    }

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
        string[] options = npc.GetOptions();
        op1.text = options[0];
        op2.text = options[1];
        op3.text = options[2];
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
        dialoguePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }
}





