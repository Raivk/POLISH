using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour {

    public Transform m_Start;
    public Transform m_End;
    public TextMeshPro m_ReactText;
    public TextMeshPro m_Text;

    public float m_WriteSpeed;

    IEnumerator TextWriterReact(string text, TextMeshPro tmp)
    {
        string other = m_Text.text;
        m_Text.text = "";
        tmp.text = "";
        for(int i = 0; i < text.Length; i++)
        {
            tmp.text += text[i];
            yield return new WaitForSeconds(m_WriteSpeed);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(TextWriter(other, m_Text));
    }

    IEnumerator TextWriter(string text, TextMeshPro tmp)
    {
        tmp.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            tmp.text += text[i];
            yield return new WaitForSeconds(m_WriteSpeed);
        }
    }

    public void Write(string text, TextMeshPro tmp, bool react)
    {
        if (react)
        {
            StartCoroutine(TextWriterReact(text, tmp));
        } else
        {
            StartCoroutine(TextWriter(text, tmp));
        }
    }

}
