using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : SuperManager {
    public GameObject tile = null;
    public TextAsset textmapinfo = null;
    private float rightGrounds = 0;
    private float leftGrounds = 0;
    
    public void SetupScene(TextAsset text)
    {
        
    }

    private void MonoDebugMapping()
    {
        if (gameManager.maincamera.transform.position.x >= rightGrounds * tile.transform.localScale.x)
        {
            rightGrounds++;
            tile = Instantiate(tile, new Vector2(rightGrounds * tile.transform.localScale.x, tile.transform.position.y), Quaternion.identity);

        }
        else if (-(gameManager.player.transform.position.x + 10) >= leftGrounds * tile.transform.localScale.x)
        {
            leftGrounds++;
            tile = Instantiate(tile, new Vector2(leftGrounds * -tile.transform.localScale.x, tile.transform.position.y), Quaternion.identity);

        }
    }

    private void TileParentAdd(GameObject tile)
    {
        tile.transform.SetParent(gameObject.transform);
    }
    void Update () {
        if (textmapinfo!=null)
        {
            return;
        }
        MonoDebugMapping();
	}
}
