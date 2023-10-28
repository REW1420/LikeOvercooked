using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimator : MonoBehaviour
{
    private Animator animator;
    private bool isTurningHead = false;

    private float random;
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Start()
    {
        // Iniciar la secuencia de activación de animaciones
        StartCoroutine(AnimationSequence());
    }
    private void Update()
    {

        random = Random.Range(1f, 5f);

    }
    private IEnumerator AnimationSequence()
    {
        while (true)
        {
            // Activa "TurnHead" y espera 3 segundos
            animator.SetBool("Turn Head", true);
            yield return new WaitForSeconds(random);
            animator.SetBool("Turn Head", false);
            yield return new WaitForSeconds(random);
            // Activa "Eat" y espera 3 segundos
            animator.SetBool("Eat", true);
            yield return new WaitForSeconds(random);
            animator.SetBool("Eat", false);


        }
    }
}
