using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXType
{
    DiskInserted = 0,
    MenuChoose = 1,
    Success = 2
}
public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] AudioClip diskInsertedClip;
    [SerializeField] AudioClip menuChooseClip;
    [SerializeField] AudioClip successClip;

    public void PlaySFX(SFXType type)
    {
        switch (type)
        {
            case SFXType.DiskInserted:
                AudioSource.PlayClipAtPoint(diskInsertedClip, Camera.main.transform.position);
                break;
            case SFXType.MenuChoose:
                AudioSource.PlayClipAtPoint(menuChooseClip, Camera.main.transform.position);
                break;
            case SFXType.Success:
                AudioSource.PlayClipAtPoint(successClip, Camera.main.transform.position);
                break;
            default:
                break;
        }
    }

    public void PlaySFX(int typeInt)
    {
        SFXType type = (SFXType)typeInt;
        switch (type)
        {
            case SFXType.DiskInserted:
                AudioSource.PlayClipAtPoint(diskInsertedClip, Camera.main.transform.position);
                break;
            case SFXType.MenuChoose:
                AudioSource.PlayClipAtPoint(menuChooseClip, Camera.main.transform.position);
                break;
            default:
                break;
        }
    }
}
