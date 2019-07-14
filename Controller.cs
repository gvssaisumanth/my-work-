using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{


    private bool walking = true;
    private Vector3 spawnPoint;
    public float damage = 10;
   public healthbar healthbarobject;
    Vector3 position;
 //   Vector3 scale;

    // Use this for initialization
    void Start()
    {
      
        spawnPoint = transform.position;
        healthbarobject = (healthbar)this.gameObject.GetComponent("healthbar");//is this it?? ok i'm back
        healthbarobject.ratiotext.color = new Color(0, 0, 0, 255);
        position = healthbarobject.ratiotext.gameObject.transform.position;
      //  scale = healthbarobject.ratiotext.gameObject.transform.localScale;
    }
    
    // Update is called once per frame
    public void Update()
    {
        healthbarobject.ratiotext.gameObject.transform.position = position;
   //     healthbarobject.ratiotext.gameObject.transform.localScale = scale;

        if (walking)
        {

            transform.position = transform.position + Camera.main.transform.forward * 10f * Time.deltaTime;
        }


        if (transform.position.y < -1f)
        {

            transform.position = spawnPoint;
        }
        if(transform.position.y>18)
        {
            transform.position = spawnPoint;
        }
  

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(5f, 5f, 5f));
        RaycastHit hit;

       if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.name.Contains("Plane"))
            {
                walking = false;
            }
            else
            {

                walking = true;
            }

        }
         if (Physics.Raycast(ray, out hit))
         {
             if (hit.collider.name.Contains("Zombie"))
             {
                 healthbarobject.TakeDamage(damage * Time.deltaTime*1);

            }
         }
    }
}