using UnityEngine;
using System.Collections;

public class BoomerangLauncher : MonoBehaviour
{
    [SerializeField]
    private Boomerang boomerangPrefab;

    public void LaunchBoomerang()
    {
        Boomerang b =  Instantiate(boomerangPrefab, transform.position, transform.parent.rotation) as Boomerang;
        b.Launch(transform.parent.gameObject);
    }
}
