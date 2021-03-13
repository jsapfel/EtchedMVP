 using System;
 using TMPro;
 using UnityEngine;
 using UnityEngine.SceneManagement;
 using UnityEngine.UI;

 public class SceneLoader : MonoBehaviour
 {

     public GameObject UnlockAll;
     public GameObject LockAll;
     public TextMeshProUGUI LivesText;

     private void Start()
     {
         LivesText.text = "Total Lives: " + GameManager.Instance.PlayerLives;
         UnlockAll.SetActive(!GameManager.Instance.AllUnlocked);
         LockAll.SetActive(GameManager.Instance.AllUnlocked);
     }

     public void LoadSceneOnClick(int sceneNo)
     {
         if (GameManager.Instance.AllUnlocked || GameManager.Instance.LevelUnlocked[sceneNo-1])
            SceneManager.LoadScene(sceneNo);
     }

     public void UnlockLockAll()
     {
         GameManager.Instance.AllUnlocked = !GameManager.Instance.AllUnlocked;
         UnlockAll.SetActive(!GameManager.Instance.AllUnlocked);
         LockAll.SetActive(GameManager.Instance.AllUnlocked);
     }
 
 }