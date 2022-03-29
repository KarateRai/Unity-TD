using UnityEngine;
using UnityEngine.UI;

public class AdditionalInfo : MonoBehaviour
{

    //TODO TA BORT!

    public GameObject infoUi;
    public Animator anim;
    public Button canvas;

    private void Start()
    {
        anim = infoUi.GetComponent<Animator>();
        canvas = infoUi.GetComponent<Button>();
    }

    private void OnMouseEnter()
    {
        Debug.Log("Entering!");
        anim.Play("SlideIn");
    }
}
