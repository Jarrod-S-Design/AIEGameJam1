using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] ParticleSystem warmUpParticle;
    [SerializeField] ParticleSystem activeParticle;

    [SerializeField] float stagnateTime = 5.0f;
    [SerializeField] float warmUpTime = 1.0f;
    [SerializeField] float activeTime = 2.0f;

    Collider collider;

    enum LavaState
    {
        Stagnate,
        WarmUp,
        Active
    }

    LavaState lavaState;

    float timer;

    void Awake()
    {
        collider = GetComponent<Collider>();
    }

    void Start()
    {
        lavaState = LavaState.Stagnate;
        collider.isTrigger = true;
        collider.enabled = false;
        timer = stagnateTime;

        warmUpParticle.time = warmUpTime;
        activeParticle.time = activeTime;
    }
	
	void Update()
    {
        switch (lavaState)
        {
            case LavaState.Stagnate:
                if (timer < 0)
                {
                    lavaState = LavaState.WarmUp;
                    timer = warmUpTime;
                    // Start Warmup particles
                    var main = warmUpParticle.main;
                    main.duration = warmUpTime;
                    warmUpParticle.Play();
                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;
            case LavaState.WarmUp:
                if (timer < 0)
                {
                    lavaState = LavaState.Active;
                    timer = activeTime;
                    // Start Active particles
                    var main = activeParticle.main;
                    main.duration = activeTime;
                    activeParticle.Play();

                    // Activate the collider
                    collider.enabled = true;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;
            case LavaState.Active:
                if (timer < 0)
                {
                    lavaState = LavaState.Stagnate;
                    timer = stagnateTime;

                    // Deactivate the collider
                    collider.enabled = false;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;
            default:
                break;
        }
    }
}
