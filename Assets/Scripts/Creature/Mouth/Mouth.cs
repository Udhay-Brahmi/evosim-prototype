using UnityEngine;
using System.Collections;


/*
 *		Author: 	Craig Lomax
 *		Date: 		06.09.2011
 *		URL:		clomax.me.uk
 *		email:		craig@clomax.me.uk
 *
 */


public class Mouth : MonoBehaviour {

	public Foodbit fb;
	public Creature crt;
	private Transform _t;
	private LineRenderer lr;
	private Vector3 line_start;
	private float line_length = 0.5f;
	private Vector3 line_end;
	private float line_width = 0.5F;
	private int fb_detect_range = 40;
	private int fb_eat_range = 10;
	private Object[] fbits;
	private GameObject cf;
	private GameObject fb_detect_trigger;

	void Start () {
		this._t = transform;
		this.crt = (Creature)_t.parent.gameObject.GetComponent("Creature");
		this.lr = (LineRenderer)this.gameObject.AddComponent("LineRenderer");
		this.lr.material = (Material)Resources.Load("Materials/mouth_vector");
		this.lr.SetWidth(line_width, line_width);
		this.lr.SetVertexCount(2);
		this.lr.renderer.enabled = true;
		
		this.fb_detect_trigger = new GameObject("FB_Trigger");
		this.fb_detect_trigger.transform.parent = transform;
		this.fb_detect_trigger.transform.localPosition = Vector3.zero;
		SphereCollider sp = this.fb_detect_trigger.AddComponent<SphereCollider>();
		sp.isTrigger = true;
		sp.radius = this.fb_detect_range;
	}

	void Update () {
		/*this.cf = closestFoodbit();
		if(cf) {
			this.lr.useWorldSpace = true;
			this.line_end = new Vector3(cf.transform.position.x, cf.transform.position.y, cf.transform.position.z);
			this.line_start = _t.position;
			this.lr.SetPosition(1,line_end);
			this.resetStart();
		} else {
			this.lr.useWorldSpace = false;
			this.line_start = Vector3.zero;
			this.line_end = new Vector3(0,0,line_length);
			this.lr.SetPosition(0,line_start);
			this.lr.SetPosition(1,line_end);
		}
		*/
	}
	
	/*
	private GameObject closestFoodbit () {
		this.fbits = this.mr.getFoodbits();
		GameObject closest = null;
		float dist = fb_detect_range;
		Vector3 pos = transform.position;
		foreach(GameObject fbit in fbits) {
			if (null != fbit && fbit.tag == "Foodbit") {
				Vector3 diff = fbit.transform.position - pos;
				float curr_dist = diff.magnitude;
				if (curr_dist < dist) {
					closest = fbit;
					dist = curr_dist;
				}
				if (curr_dist < fb_eat_range && this.crt.state == Creature.State.hungry) {
					this.fb = fbit.GetComponent<Foodbit>();
					this.crt.eat(this.fb.getEnergy());
					this.fb.destroy();
				}
			}
		}
		return closest;	
	}*/
	
	private void resetStart () {
		this.line_start = new Vector3(_t.position.x,_t.position.y,_t.position.z);
		this.lr.SetPosition(0,line_start);
	}
	
	public float getDetectRadius() {
		return this.fb_detect_range;
	}

}
