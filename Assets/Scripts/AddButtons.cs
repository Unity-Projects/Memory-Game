using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject btn;

    // Start is called before the first frame update
    void Awake() {

        int cardNumbers = LevelParameters.cardsNumber;

        if (cardNumbers <= 0) {
            cardNumbers = 8;
        }

        for (int i=0; i<cardNumbers; i++) {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(puzzleField, false);

        }
    }
}
