using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;
public class healthbar : MonoBehaviour
{
    public Image currenthealthbar;
    public Text ratiotext;
     

    public float hitpoint = 150;
    public float maxhitpoint=150;
    
    public void Start()
    {
        updatehealth();
       
    }
	public void Setup()
	{
		//updatehealth();
		
	}
    public void updatehealth()
    {
       float hitratio=hitpoint/maxhitpoint;
        currenthealthbar.rectTransform.localScale = new Vector3(hitratio, 1, 1);
		ratiotext.text = (hitratio * 100).ToString("0")+"%";


    }
    public void  TakeDamage(float damage)
    {
        hitpoint -= damage;
        if (hitpoint < 0)
        {
            hitpoint = 0;
			Debug.Log("dead!");
            SceneManager.LoadScene(1);
        }
        updatehealth();
    }

    
}
