using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterUsername : MonoBehaviour
{
    public GameObject UsernameInputField;
    public GameObject orangeBackground;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        UsernameInputField.SetActive(true);
        orangeBackground.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            UsernameInputField.SetActive(false);
            orangeBackground.SetActive(false);
        }
    }
}
