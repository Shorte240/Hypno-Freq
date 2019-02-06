using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_device : MonoBehaviour {

    public int defExpand = 5;
    public int maxSize = 15;

    int expandRate = 2;
    bool expanding = false;
    private Color startColor;
    AudioSource audSrc;
    private bool hold;
    public GameObject childSphere;

	// Use this for initialization
	void Start () {
        audSrc = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {
        SphereCollider influence = transform.GetComponent<SphereCollider>();
        ParticleSystem emmiter = GetComponent<ParticleSystem>();

        float scaleFac = influence.radius * 2;
        childSphere.transform.localScale = new Vector3(scaleFac, scaleFac, scaleFac);

        emmiter.startLifetime = influence.radius / emmiter.startSpeed;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (expanding)
        {
            expandRate = 6; //sphere of influence expands faster until it gets to default expanded value
            if (influence.radius < maxSize)
            {
                influence.radius += expandRate * Time.deltaTime;
            }
            if (influence.radius >= defExpand)
            {
                expanding = false;
            }
        }
        else
        {
            expandRate = 2;

            if (Input.GetMouseButtonUp(0)) { hold = false; }

            if ((Physics.Raycast(ray, out hit) && Input.GetMouseButton(0) && hit.collider == this.GetComponent<Collider>()) || hold )
            {
                if (!hold) { hold = true; }
                if (influence.radius < defExpand)
                {
                    expanding = true;
                    audSrc.Play();
                    emmiter.Play();
                }
                if (influence.radius < maxSize)
                {
                    influence.radius += expandRate * Time.deltaTime;
                }
            }
            else if (influence.radius > 0.5)
            {
                if (influence.radius > defExpand) { expandRate = 8; }
                influence.radius -= expandRate * Time.deltaTime;
                audSrc.Stop();
                emmiter.Stop();
            }
        }
	}

    void OnMouseEnter()
    {

        Renderer renderer = GetComponent<Renderer>();

        startColor = renderer.material.color;
        renderer.material.color = Color.yellow;
    }

    void OnMouseExit()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = startColor;
    }
}
