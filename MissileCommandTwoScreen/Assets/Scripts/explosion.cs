using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    public float maxSize = 10.0F;
    public float growthTime = 2.0F;

    // Start is called before the first frame update
    void Start()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleOverTime(growthTime));
    }

    IEnumerator ScaleOverTime(float time) {
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3(maxSize, maxSize, maxSize);

        float currentTime = 0.0F;

        do {
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "missile") {
            missileCommand.changeStateTutorial();
            Destroy(other.gameObject);
        }
    }
}
