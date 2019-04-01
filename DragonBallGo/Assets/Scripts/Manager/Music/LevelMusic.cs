using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour {

    // Update is called once per frame
    void Start()
    {
            if (AudioManager.shared.randomPlay)
            {
                AudioManager.shared.CrossfadeRandomMusic();
            }
            else
            {
                AudioManager.shared.CrossfadeNextMusic();
            }
    }
}
