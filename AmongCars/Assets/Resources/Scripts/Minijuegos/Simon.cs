using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Simon : MonoBehaviour 
{   
	public AmongCars controls;

    public GameObject panel;
	public GameObject texto;
	public GameObject textoGanar;

	public GameObject player;

	public int startingSequenceCount = 5;
	public List<int> simonSequence;
	public List<int> userSequence;

	public bool animatingSequence = false;

	private List<Button> buttons;
	public Button botonRojo;
	public Button botonAmarillo;
	public Button botonVerde;
	
	protected void Awake() => controls = new AmongCars();
	protected void OnEnable() => controls?.Enable();
    protected void OnDisable() => controls?.Disable();


	void Start () 
	{
		simonSequence = new List<int> ();
		userSequence = new List<int> ();

        buttons = new List<Button>();
        buttons.Add(botonRojo);
        buttons.Add(botonAmarillo);
        buttons.Add(botonVerde);
	}

	public void ResetSimon ()
	{
		simonSequence.Clear ();

		for (int i = 0; i < startingSequenceCount; i++)
		{
			AddToSimonSequence ();
		}

		StartCoroutine (AnimateButtonSequence ());
	}

	public void ResetUser () {
		userSequence.Clear();
	}

	IEnumerator AnimateButtonSequence ()
	{
		animatingSequence = true;
		print("Simon empieza");
		foreach (int id in simonSequence)
		{
			yield return new WaitForSeconds (1.0f);

			Button button = GetButtonFromID (id);

			button.GetComponent<Image>().color = button.GetComponent<Button>().colors.pressedColor;
			yield return new WaitForSeconds (1.0f);
			button.GetComponent<Image>().color = button.GetComponent<Button>().colors.normalColor;
		}
		print("Simon acaba");
		animatingSequence = false;
		StartCoroutine(AddToUserSequence());
	}

	public void AddToSimonSequence ()
	{
		int randomColour = Random.Range (0, buttons.Count);
        print(randomColour);

		simonSequence.Add (randomColour);
	}

	IEnumerator GameLoop ()
	{	
		bool lost = false;
		int level = 0;
		print("GAME LOOP");
		do
		{
			if (EqualSequenceLength ())
			{
				if (CompareSequences ())
				{
					print ("Has ganado!");
					level ++;
					if(level == 4){
						textoGanar.SetActive(true);
						StopAllCoroutines();
						panel.SetActive(false);
					}
					ResetUser ();

					AddToSimonSequence ();

					yield return new WaitForSeconds (1.5f);

					StartCoroutine (AnimateButtonSequence ());
				}
				else
				{
					print ("Has perdido :(");
					lost = true;
					player.SendMessage("quitarCordura");
					StopAllCoroutines();

					texto.SetActive(true);
					panel.SetActive(false);

					//yield return new WaitForSeconds (1.5f);
				}
			}
			else if (userSequence.Count > simonSequence.Count)
			{
				yield return new WaitForSeconds (1.5f);

				StopAllCoroutines();

				ResetSimon ();
				ResetUser ();
			}
			//print("Continuo esperando a que el user introduzca");
			yield return new WaitForSeconds (1.0f);

		} while (!lost && level < 4);
	}

	public bool CompareSequences ()
	{
		for (int i = 0; i < simonSequence.Count; i++)
		{
			int simonID = simonSequence[i];
			int userID = userSequence[i];

			if (simonID != userID)
				return false;
		}
		return true;
	}

	public bool EqualSequenceLength ()
	{
		return (simonSequence.Count == userSequence.Count);
	}

	private Button GetButtonFromID (int id)
	{
		if(id == 0){
			return botonRojo;
		}
		if(id == 1){
			return botonAmarillo;
		}
		if(id == 2){
			return botonVerde;
		}
		return null;
	}

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            panel.SetActive(true);
			
			ResetSimon ();
			ResetUser();

			StartCoroutine (GameLoop ());
        }
    }

    void OnTriggerExit(Collider other){
        if (other.CompareTag("Player")){
            panel.SetActive(false);
			texto.SetActive(false);
			textoGanar.SetActive(false);
			StopAllCoroutines();
        }
    }

	IEnumerator AddToUserSequence(){
		if(!animatingSequence){
			print("Entra en user sequence");
			yield return new WaitForSeconds (1.0f);
			userSequence.Add(0);
			botonRojo.GetComponent<Image>().color = botonRojo.GetComponent<Button>().colors.pressedColor;
			yield return new WaitForSeconds (1.0f);
			botonRojo.GetComponent<Image>().color = botonRojo.GetComponent<Button>().colors.normalColor;
			yield return new WaitForSeconds (0.5f);
			userSequence.Add(1);
			botonAmarillo.GetComponent<Image>().color = botonAmarillo.GetComponent<Button>().colors.pressedColor;
			yield return new WaitForSeconds (1.0f);
			botonAmarillo.GetComponent<Image>().color = botonAmarillo.GetComponent<Button>().colors.normalColor;
			yield return new WaitForSeconds (0.5f);
			userSequence.Add(1);
			botonAmarillo.GetComponent<Image>().color = botonAmarillo.GetComponent<Button>().colors.pressedColor;
			yield return new WaitForSeconds (1.0f);
			botonAmarillo.GetComponent<Image>().color = botonAmarillo.GetComponent<Button>().colors.normalColor;
			
			// if (controls.Player.Option1.ReadValue<float>() == 1)
			// {
			// 	userSequence.Add(0);
			// 	print("HAS PULSADO ROJO");
			// 	botonRojo.GetComponent<Image>().color = botonRojo.GetComponent<Button>().colors.pressedColor;
			// 	yield return new WaitForSeconds (0.5f);
			// 	botonRojo.GetComponent<Image>().color = botonRojo.GetComponent<Button>().colors.normalColor;
			// }
			// if (controls.Player.Option2.ReadValue<float>() == 1)
			// {
			// 	userSequence.Add(1);
			// 	print("HAS PULSADO AMARILLO");
			// 	botonAmarillo.GetComponent<Image>().color = botonAmarillo.GetComponent<Button>().colors.pressedColor;
			// 	yield return new WaitForSeconds (0.5f);
			// 	botonAmarillo.GetComponent<Image>().color = botonAmarillo.GetComponent<Button>().colors.normalColor;
			// }
			// if (controls.Player.Option3.ReadValue<float>() == 1)
			// {
			// 	userSequence.Add(2);
			// 	print("HAS PULSADO Verde");
			// 	botonVerde.GetComponent<Image>().color = botonVerde.GetComponent<Button>().colors.pressedColor;
			// 	yield return new WaitForSeconds (0.5f);
			// 	botonVerde.GetComponent<Image>().color = botonVerde.GetComponent<Button>().colors.normalColor;
			// }
		}
	}
}
