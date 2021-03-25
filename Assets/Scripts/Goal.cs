using Random = UnityEngine.Random;
using UnityEngine;

public class Goal : MonoBehaviour {
    public bool isSpawned = false;

    public void Spawn() {
        if (!isSpawned) {
            isSpawned = true;

            gameObject.SetActive(true);
            transform.localPosition = new Vector3(Random.Range(-3f, 0f), 0.1f, Random.Range(-1.9f, +1.9f));
        }
    }

    public void Despawn() {
        if (isSpawned) {
            isSpawned = false;
            
            gameObject.SetActive(false);
        }
    }
}
