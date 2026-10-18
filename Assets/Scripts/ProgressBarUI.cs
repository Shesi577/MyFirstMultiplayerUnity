using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasObjectGameObject;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasObjectGameObject.GetComponent<IHasProgress>();

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (Mathf.Approximately(e.progressNormalized, 0f) || Mathf.Approximately(e.progressNormalized, 1f))
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Hide() => gameObject.SetActive(false);
    private void Show() => gameObject.SetActive(true);

    private void OnDestroy()
    {
        if (hasProgress != null)
        {
            hasProgress.OnProgressChanged -= HasProgress_OnProgressChanged;
        }
    }
}