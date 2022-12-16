using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombiHareket : MonoBehaviour
{
    private GameObject oyuncu;
    private int zombieCan = 3;
    private float mesafe;
    private int zombidenGelenPuan = 1;
    public GameObject kalp;
    private OyunKontrol oKontrol;
    private AudioSource aSource;
    private bool zombieOluyor = false;
    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        oyuncu = GameObject.Find("FPSController");
        oKontrol = GameObject.Find("_Scripts").GetComponent<OyunKontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().destination =oyuncu.transform.position;/*gideceginoktayanioyuncu*/ //oyuncunun bulunduğu noktaya yönledir. uptade içine yazma sebebi oynucunun yerinin sürekli değişiyor olması.
        mesafe = Vector3.Distance(transform.position, oyuncu.transform.position); //3B 2 nokta arasıdaki mesafeyi ölçer.
        if (mesafe < 10f)
        {
            if(!aSource.isPlaying)
                aSource.Play();
            if(!zombieOluyor)
            GetComponentInChildren<Animation>().Play("Zombie_Attack_01");
        }
        else
        {
            aSource.Stop();
        }
    }
    private void OnCollisionEnter(Collision c) //çarpışma testi mermi()
    {
        //if (c.collider.gameObject.tag.Equals("mermi")) 
        if (c.gameObject.tag.Equals("mermi"))
        {
        
            Debug.Log("Çarpışma Gerçekleşti");
            zombieCan--;
            if (zombieCan ==0)
            {
                zombieOluyor = true;
                oKontrol.PuanArtir(zombidenGelenPuan);
                Instantiate(kalp, transform.position, Quaternion.identity);
                GetComponentInChildren<Animation>().Play("Zombie_Death_01");
                Destroy(this.gameObject, 1.667f);
            }
        }
    }
}
