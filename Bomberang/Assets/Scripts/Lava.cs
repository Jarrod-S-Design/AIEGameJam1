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

    void Start()
    {
        lavaState = LavaState.Stagnate;
        timer = stagnateTime;

        warmUpParticle.time = warmUpTime;
        activeParticle.time = activeTime;
    }
	
	void Update ()
    {
        switch (lavaState)
        {
            case LavaState.Stagnate:
                if (timer < 0)
                {
                    lavaState = LavaState.WarmUp;
                    // Start Warmup particles
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
                    // Stop Warmup particles
                    // Start Active particles

                    // Activate the collider

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
                    // Stop Active particles

                    // Deactivate the collider

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
