using UnityEngine;
using System.Collections;

public class Moveliftcar : MonoBehaviour {



		public Transform target;
		public float speed = 0.0f;
		public KeyCode Key1;
		public KeyCode Key2;
		public Vector3 forwardPos;
		public Vector3 rearPos;



		void Update () {


			if (Input.GetKey(Key1))

			{



				target.transform.localPosition = Vector3.MoveTowards(target.transform.localPosition, forwardPos, speed  * Time.deltaTime);

			}
			if (Input.GetKey(Key2))
			{
				target.transform.localPosition = Vector3.MoveTowards (target.transform.localPosition, rearPos, speed * Time.deltaTime);
			}
		}
	}