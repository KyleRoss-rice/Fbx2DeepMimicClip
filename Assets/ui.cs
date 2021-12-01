using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ui : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OpenDialog()
    {
        string path = EditorUtility.OpenFilePanel("Select FBX File", "", "fbx");
        if (path.Length != 0)
        {
            if (AssetImporter.GetAtPath(path) is ModelImporter importer)
            {
                ModelImporterClipAnimation[] clips = importer.defaultClipAnimations;
            }
        }
    }

    public void StartAndRecord()
    {
        animator.enabled = true;
        
    }
}
