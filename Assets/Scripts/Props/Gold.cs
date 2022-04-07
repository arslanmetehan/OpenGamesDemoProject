using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gold : MonoBehaviour
{
    Vector3 customScale = new Vector3(1,0.27f,0.27f);
    private void OnEnable()
    {
        transform.parent = GameManager.Instance.backPack;
        transform.localPosition = new Vector3(0, GameManager.Instance.nextSlotPosY, 0);
        GameManager.Instance.nextSlotPosY += 0.3f;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.DOScale(customScale, 0.15f).SetEase(Ease.OutBack);
    }
}
