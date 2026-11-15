using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; private set; }

    [SerializeField] private SFXSO audioClipsSO;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
        Player.OnPickUp += Player_OnPickUp;
        Player.OnFootstep += Player_OnFootstep;
    }

    private void Player_OnFootstep(object sender, EventArgs e)
    {
        Player player = Player.Instance;
        PlaySound(audioClipsSO.footsteps[UnityEngine.Random.Range(0, audioClipsSO.footsteps.Length)], player.transform.position);
    }

    private void Player_OnPickUp(object sender, EventArgs e)
    {
        Player player = Player.Instance;
        PlaySound(audioClipsSO.pickUp[UnityEngine.Random.Range(0, audioClipsSO.pickUp.Length)], player.transform.position);
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipsSO.chop[UnityEngine.Random.Range(0, audioClipsSO.chop.Length)], trashCounter.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipsSO.chop[UnityEngine.Random.Range(0, audioClipsSO.chop.Length)], cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, EventArgs e)
    {
        DeliveryManager deliveryCounter = DeliveryManager.Instance;
        PlaySound(audioClipsSO.deliveryFail[UnityEngine.Random.Range(0, audioClipsSO.deliveryFail.Length)], deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, EventArgs e)
    {
        DeliveryManager deliveryCounter = DeliveryManager.Instance;
        PlaySound(audioClipsSO.deliverySuccess[UnityEngine.Random.Range(0, audioClipsSO.deliverySuccess.Length)], deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
