using UnityEngine;
using System.Collections;

/// <summary>
/// Skript pro ovladani strelby nepratel
/// </summary>
public class EnemyShooting : MonoBehaviour {

    public GameObject shot; // prefab strely
    public Transform shotSpawn; // misto odkud se vystreli
    public float fireRate = 0.5f;

    private AudioSource audioSource;
    private float nextFire;

    // Use this for initialization
    void Start ()
    {
        nextFire = 0.5f;
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextFire)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        fireRate = 0.35f;
        shot.transform.localScale = new Vector3(1.3f, 1.3f, 1.0f);  // zvetsim strely o 130%
        nextFire = Time.time + Random.Range(0.35f, 0.85f);  // kdy znova vystreli
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        if (PlayerPrefs.GetInt("Music") == 1) // pokud jsou zaple zvuky
        {
            audioSource.Play(); // prehraj zvuk
        }
    }
}
