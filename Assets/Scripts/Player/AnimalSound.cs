using Cysharp.Threading.Tasks;
using Player;
using System.Threading;
using System;
using UnityEngine;
using System.Collections;

public class AnimalSound : MonoBehaviour
{
    [SerializeField] AudioSource _meowStartSound;
    [SerializeField] AudioSource _meowDeathSound;
    [SerializeField] AudioClip _meowDeathClip;

    float _secondsInMS = 1000f;
   
    public void PlaySoundAtStart()
    {
        _meowStartSound.Play();
    }

    public async UniTask PlayDeathSound()
    {
        _meowDeathSound.Play();
        await UniTask.Delay((int)(_meowDeathClip.length * _secondsInMS));
    }

}

