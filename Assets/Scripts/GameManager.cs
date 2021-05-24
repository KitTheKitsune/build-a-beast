using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //number of tiles that make up the level for background generation
    public Vector2 levelSize = new Vector2(20f, 15f);
    //list of sprites for background generation
    public Sprite[] wallTiles;
    //transform of background
    public Transform tileBase;
    //parent object that the background tiles are instantiated in
    public GameObject environment;
    //exit ladder object the scene is looking at
    //public ExitLadder ladder;
    //is the scene in the process of changing? This helps prevent multiple calls
    public boolean sceneChanging;
    //the player game object for use in scene transition
    public GameObject player;
    //the current level that is loaded 0 for testing >0 are actual levels in numeric order
    public int levelLoaded = 0;

    void Start() {
        //scene has started and thus scene is able to change
        scenechanging = false;
        
        //background generation [ignore]
        for(int i = 0; i < levelSize.x; i++) {
            for(int j = 0; j < levelSize.y; j++) {
                Transform tile = Instantiate(tileBase, new Vector3(i - levelSize.x/2, j - levelSize.y/2, 7f), Quaternion.identity);
                tile.GetComponent<SpriteRenderer>().sprite = wallTiles[(int)Mathf.Floor(Random.Range(0f, wallTiles.Length-float.Epsilon))];
                tile.transform.SetParent(environment.transform);
            }
        }
    }

    void Update() {
        if(player.levelEnd && !sceneChanging) {
            //ensures this is not called multiple times before scene change
            sceneChanging = true;
            
            //grab the current scene
            Scene currScene = SceneManager.GetActiveScene();
            
            //increment the level name
            levelLoaded++;
            string sceneName = "Level" + levelLoaded;
            
            //load next level scene
            SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.additive);
            
            //move player to new scene
            SceneManager.MoveGameObjectToScene(player,GameManager.GetSceneByName(sceneName));
            
            //unload current scene
            SceneManager.UnloadSceneAsync(currScene);

            
        }
    }
}
