using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Annoucements : MonoBehaviour
{
    private Text failMessage;
    private Text successMessage;
    public static Annoucements annoucements;

    private void Awake()
    {
        if (annoucements == null)
            annoucements = this;
        failMessage = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        successMessage = gameObject.transform.GetChild(0).GetChild(1).GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        failMessage.enabled = false;
        successMessage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fail()
    {
        failMessage.enabled = true;
    }

    public void Success()
    {
        successMessage.enabled = true;
    }
}
