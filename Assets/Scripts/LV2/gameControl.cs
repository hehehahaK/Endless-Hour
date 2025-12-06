using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControl : MonoBehaviour

// // --- GameControl.cs (COMPLETE) ---
// using UnityEngine;
// using UnityEngine.UI; // Required for the Text component

// public class GameControl : MonoBehaviour
// {
//     // The static variable used by your touchPuzzle script
//     public static bool youWin = false; 

//     // *** 1. ARRAY & UI REFERENCE ***
//     // Drag all 9 puzzle pieces from the Hierarchy into this slot in the Inspector
//     public Transform[] pieces; 

//     // Drag your UI Text object (the "YOU WIN" message) here
//     public Text winText; 

//     void Start()
//     {
//         youWin = false; 
//         if (winText != null) 
//         {
//             winText.gameObject.SetActive(false);
//         }
//     }

//     void Update()
//     {
//         if (!youWin)
//         {
//             CheckForWin();
//         }
//     }

//     void CheckForWin()
//     {
//         int correctPieces = 0;
//         foreach (Transform piece in pieces)
//         {
//             if (piece.rotation.eulerAngles.z == 0)
//             {
//                 correctPieces++;
//             }
//         }
//         if (correctPieces == pieces.Length)
//         {
//             youWin = true;
//             Debug.Log("YOU WIN!");
//             if (winText != null) 
//             {
//                 winText.gameObject.SetActive(true);
//             }
//         }
//     }
// }

{
    public static bool youWin = false; 

    // Drag all 9 puzzle pieces here in the Inspector
    public Transform[] pieces; 

    // Drag your "You Win!" UI Text object here
    public Text winText; 

    void Start()
    {
        youWin = false; 
        // Hide the win message at the start
        if (winText != null) 
        {
            winText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!youWin)
        {
            CheckForWin();
        }
    }

    void CheckForWin()
    {
        int correctPieces = 0;

        foreach (Transform piece in pieces)
        {
            // Checks if the Z-rotation is 0 (the initial, correct position)
            if (piece.rotation.eulerAngles.z == 0)
            {
                correctPieces++;
            }
        }

        if (correctPieces == pieces.Length)
        {
            youWin = true;
            Debug.Log("YOU WIN!");
            
            // Show the "YOU WIN" message
            if (winText != null) 
            {
                winText.gameObject.SetActive(true);
            }
        }
    }
}