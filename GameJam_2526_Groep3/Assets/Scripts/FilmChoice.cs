using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FilmChoice : MonoBehaviour
{
    [SerializeField] GameObject film_1;
    [SerializeField] GameObject film_2;
    [SerializeField] GameObject film_3;
    Animator animator;
    bool isSelected = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnMouseOver()
    {
        animator.SetBool("onAim", true);
    }
    void OnMouseExit()
    {
        if (!isSelected)
        {
            animator.SetBool("onAim", false);
        }
    }
    private void OnMouseDown()
    {
        if (!isSelected)
        {
            isSelected = true;
            switch (gameObject.tag)
            {
                case "Film_1":
                    film_2.SetActive(false);
                    film_3.SetActive(false);
                    break;
                case "Film_2":
                    film_1.SetActive(false);
                    film_3.SetActive(false);
                    break;
                case "Film_3":
                    film_2.SetActive(false);
                    film_1.SetActive(false);
                    break;

            }
            gameObject.transform.position = new Vector3(0f, 0f, 0f);
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 1.5f, gameObject.transform.localScale.y * 1.5f, 0);
        }
    }
}