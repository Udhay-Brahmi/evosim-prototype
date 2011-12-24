using UnityEngine;
using System.Collections;


/*
 *		Author: 	Craig Lomax
 *		Date: 		06.09.2011
 *		URL:		clomax.me.uk
 *		email:		crl9@aber.ac.uk
 *
 */


public class Genitalia : MonoBehaviour {
#pragma warning disable 0414
	public Creature crt;
	private Logger lg;
	private Spawner spw;
	private Transform _t;
	private LineRenderer lr;
	private Vector3 line_start;
	private float line_length = 0.5F;
	private Vector3 line_end;
	private float line_width = 0.5F;
	private int crt_detect_range = 3000;
#pragma warning restore 0414

	void Start () {
		this.gameObject.tag = "Genital";
		lg = Logger.getInstance();
		spw = Spawner.getInstance();
		
		_t = transform;
		lr = (LineRenderer)this.gameObject.AddComponent("LineRenderer");
		lr.material = (Material)Resources.Load("Materials/genital_vector");
		lr.SetWidth(line_width, line_width);
		lr.SetVertexCount(2);
		lr.castShadows = false;
		lr.receiveShadows = false;
		lr.renderer.enabled = true;
	}
	
	void Update () {
		GameObject cc = closestCreature();
		if(cc) {
			lr.useWorldSpace = true;
			line_end = new Vector3(cc.transform.position.x, cc.transform.position.y, cc.transform.position.z);
			line_start = _t.position;
			lr.SetPosition(1,line_end);
			resetStart();
		} else {
			lr.useWorldSpace = false;
			line_start = new Vector3(0,0,0);
			line_end = new Vector3(0,0,line_length);
			lr.SetPosition(0,line_start);
			lr.SetPosition(1,line_end);
		}
	}
	
	private GameObject closestCreature () {
		GameObject[] crts = GameObject.FindGameObjectsWithTag("Genital");
		GameObject closest = null;
		float dist = crt_detect_range;
		Vector3 pos = transform.position;
		foreach(GameObject crt in crts) {
			Vector3 diff = crt.transform.position - pos;
			float curr_dist = diff.sqrMagnitude;
			if (curr_dist < dist && crt != gameObject) {
				closest = crt;
				dist = curr_dist;
			}
		}
		return closest;	
	}
	
	private void resetStart () {
		line_start = new Vector3(_t.position.x,_t.position.y,_t.position.z);
		lr.SetPosition(0,line_start);
	}
	
	/*
	 * Determine the GameObject colliding with the genital
	 * radius. If it's the genitalia of another creature
	 * log event and pass genes of both creatues to
	 * a function yet undetermined.
	 *
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.name == "Genital") {
			other_crt = col.transform.parent.gameObject.GetComponent<Creature>();
			spw.spawn(col.transform.position, col.transform.localEulerAngles);
			string mesg = "CRTB" + " " + crt.getID() + " " +
				other_crt.getID() + " " + Time.realtimeSinceStartup; 
			Debug.Log(mesg);
			lg.write(mesg);
		 }
	}*/
}
