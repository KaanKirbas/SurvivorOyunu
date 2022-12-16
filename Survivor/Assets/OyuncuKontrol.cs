using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OyuncuKontrol : MonoBehaviour
{
    public AudioClip atisSesi, olmeSesi, canAlmaSesi, yaralanmaSesi;
    public Transform mermiPos; //mermini çıkacağı pozisyon belirlemek için
    public GameObject mermi;
    public Image canImaji;
    private float canDegeri = 100f;
    public OyunKontrol oKontrol;
    private AudioSource aSource;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            aSource.PlayOneShot(atisSesi, 1f);
            
            GameObject go = Instantiate(mermi, mermiPos.position, mermiPos.rotation) as GameObject;
            go.GetComponent<Rigidbody>().velocity = mermiPos.transform.forward * 10f;
            Destroy (go.gameObject,2f); //sonsuza kadar gitmesin. 2 saniye sonra yok etsin.
        }
        
    }
    private void OnCollisionEnter(Collision c) //çarpışma testi mermi()
    {
        //if (c.collider.gameObject.tag.Equals("mermi")) 
        if (c.gameObject.tag.Equals("zombi"))
        {
            aSource.PlayOneShot(yaralanmaSesi, 1f);
            Debug.Log("Zombi Saldırdı");
            canDegeri -= 10f;
            float x = canDegeri / 100f;
            canImaji.fillAmount = x;
            canImaji.color = Color.Lerp(Color.red, Color.green, x);//renk değişimi yapma.
            
           
        }
    }
    private void OnTriggerEnter(Collider c) //içinden geçilen objelerin testi.
    {
        if(c.gameObject.tag.Equals("kalp"))
        {
            canDegeri += 10f;         
            float x = canDegeri / 100f;
            canImaji.fillAmount = x;
            canImaji.color = Color.Lerp(Color.red, Color.green, x);
        }
    }
}
