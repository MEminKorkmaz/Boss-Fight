using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcebenderFreezingGroundTriangle : MonoBehaviour
{
    public int Damage;
    void Start()
    {
        Invoke(nameof(DestroyFunction) , 0.75f);
    }


    void DestroyFunction()
    {
        GameObject goPS = transform.GetChild(0).gameObject;
        goPS.GetComponent<ParticleSystem>().Stop(true);
        goPS.transform.parent = null;
        goPS.transform.localScale = new Vector3(1f , 1f , 0f);
        Destroy(goPS , 3f);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Firebender"))
        {
            col.gameObject.GetComponent<TakeDamageAlly>().TakeDamage(Damage);
        }
    }
}
