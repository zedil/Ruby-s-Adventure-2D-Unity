using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    //OnTriggerEnter2D function on the first frame when it detects a new Rigidbody entering the Trigger. The parameter called other will contain the Collider that just entered the Trigger.
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Entered the trigger area: " + other); //other= çarpan objeyi tanımlar örneğin ruby
        RubyController controller = other.GetComponent<RubyController>();

        if(controller != null)
        {
            if(controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                Destroy(gameObject); //bu kod collectible healtın içinde olduğu için buradaki gameobject odur.

                controller.PlaySound(collectedClip);
            }

          
        }
    }

}
