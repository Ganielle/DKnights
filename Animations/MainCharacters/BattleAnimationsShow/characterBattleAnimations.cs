using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterBattleAnimations : MonoBehaviour
{
    
    [SerializeField]private GameObject Weapon;

    [Header("Particle System")]
    [SerializeField] private ParticleSystem particlesRing;
    [SerializeField] private ParticleSystem particlesBurst, showWeaponShine;

    [Header("Audio Source")]
    [SerializeField] private AudioSource particleRingSfx;
    [SerializeField] private AudioSource showWeaponSfx;

    public void showWeapon()
    {
        Weapon.SetActive(true);
    }

    public void playParticleRingSfx()
    {
        particleRingSfx.Play();
    }

    public void playShowWeaponSfx()
    {
        showWeaponSfx.Play();
    }

    public void showParticlesRing()
    {
        particlesRing.gameObject.SetActive(true);   
        particlesRing.Play();
    }

    public void showWeaponShineParticle()
    {
        showWeaponShine.gameObject.SetActive(true);
        showWeaponShine.Play();
    }

    public void showParticleBurst()
    {
        particlesBurst.gameObject.SetActive(true);
        particlesBurst.Play();
    }

    public void hideEffects()
    {
        particlesRing.gameObject.SetActive(false);
        particlesBurst.gameObject.SetActive(false);
        showWeaponShine.gameObject.SetActive(false);
    }

    public void hideStaff()
    {
        Weapon.SetActive(false);
    }
}
