using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyParticle : MonoBehaviour {

    Vector3 target;
    private Color particleColor;
    bool isMoving = false;
    float maxDistanceDelta = 0.2f;
    float waitToDestroyTime = 0.7f;

	void Update ()
    {
		if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, maxDistanceDelta);
            if (transform.position.Equals(target))
            {
                StartCoroutine(WaitToStop(waitToDestroyTime));
            }
        }
	}

    public void CreateParticle(string color, GameObject passedTarget)
    {
        target = new Vector3(passedTarget.transform.position.x, passedTarget.transform.position.y + Random.Range(-3, 3), passedTarget.transform.position.z);

        switch (color)
        {
            default:
            case "Red":
                particleColor = new Color(255f, 0f, 0f);
                break;
            case "Blue":
                particleColor = new Color(0f, 0f, 255f);
                break;
            case "Yellow":
                particleColor = new Color(255f, 255f, 0f);
                break;
            case "Green":
                particleColor = new Color(0f, 255f, 0f);
                break;
        }
        var main = GetComponent<ParticleSystem>().main;
        main.startColor = particleColor;
        
        isMoving = true;
    }

    IEnumerator WaitToStop(float time)
    {
        yield return new WaitForSeconds(time);
        var emission = GetComponent<ParticleSystem>().emission;
        emission.enabled = false;
        StartCoroutine(WaitToDestroy(time));
    }

    IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
