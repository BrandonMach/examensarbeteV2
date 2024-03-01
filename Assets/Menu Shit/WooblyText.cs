using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WooblyText : MonoBehaviour
{

    [SerializeField] TMP_Text _textComponent;
    [SerializeField] GameObject ParentGO;
    [SerializeField] float time;

    // Update is called once per frame
    void Update()
    {
        _textComponent.ForceMeshUpdate();
        var textInfo = _textComponent.textInfo;

        //One loop for each character
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; j++)
            {
                var original = verts[charInfo.vertexIndex + j];

                verts[charInfo.vertexIndex + j] = original + new Vector3(0, Mathf.Sin(Time.time*5f + original.x*0.01f) * 20, 0);
            }
        }


        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            _textComponent.UpdateGeometry(meshInfo.mesh,i);
        }

        time += Time.deltaTime;
        if(time >= 4)
        {
            Destroy(ParentGO);
        }
        
    }
}
