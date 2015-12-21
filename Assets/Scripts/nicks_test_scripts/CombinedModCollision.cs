using UnityEngine;
using System;
using System.Collections;

public class CombinedModCollision : MonoBehaviour
{
    public float forceModifier = 0.8f;

    public void OnCollisionEnter(Collision collision)
    {
        VehicleController vc = collision.transform.GetComponent<VehicleController>();
        if (vc != null)
        {
            vc.OnModifyWheelForce += ApplyModifier;
            
            StartCoroutine(ModifierTimer(vc));
        }
    }

    public void ApplyModifier(object sender, CombinedModifierEventArgs e)
    {
        e.AddModifier(forceModifier);
    }

    private IEnumerator ModifierTimer(VehicleController vc)
    {
        yield return new WaitForSeconds(5);
        vc.OnModifyWheelForce -= ApplyModifier;
    }
}
