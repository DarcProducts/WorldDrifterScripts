using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundEffectScript : MonoBehaviour
{
    public SoundController soundController;

    public void PlayExplosion()
    {
        soundController.Explosion();
    }
}
