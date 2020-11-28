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

    Queue<AudioClip> audios;

    //public GameObject dialoguePanel;
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
    public AudioClip speakSound; //Crear de la misma manera que las frases, una cola de audios

    void Start()
    {
        sentences = new Queue<string>();
        audios = new Queue<AudioClip>();
        myAudio = GetComponent<AudioSource>();
    }

    protected void Awake() => controls = new AmongCars();
    protected void OnEnable() => controls?.Enable();
    protected void OnDisable() => controls?.Disable();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //print("Prueba");
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
        sentences.Clear();
        audios.Clear();
        foreach (string sentence in npc.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (AudioClip voz in npc.voces){
            audios.Enqueue(voz);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            displayText.text = activeSentence;
            myAudio.clip = speakSound;
            return;
        }

        activeSentence = sentences.Dequeue();
        displayText.text = activeSentence;
        speakSound = audios.Dequeue();
        myAudio.clip = speakSound;

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
            //myAudio.PlayOneShot(speakSound);
            yield return new WaitForSeconds(typingSpeed);
        }

        if (npc.GetOptions().Length > 0)
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
            ClosePanel();
            StopAllCoroutines();
            myAudio.Stop();
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
        myAudio.clip = npc.GetAudio(curResponseTracker);
        myAudio.Play();

        optionsPanel.SetActive(false);
    }

    private void ClosePanel()
    {
        ObjectInteraction.ShowDialog(false);
        optionsPanel.SetActive(false);
    }
}





