// Oz
using UnityEngine;
public class LevelGenerator : MonoBehaviour
{
    public Texture2D mapTexture;
    public PixelToObject[] pixelColorMappings;
    private Color pixelColor;
    private float prefabSize = 2.4f;
    void Start()
    {
        GenerateLevel();
    }
    void GenerateLevel()
    {
        // Scan whole Texture and get positions of objects
        for (int i = 0; i < mapTexture.width; i++)
        {
            for (int j = 0; j < mapTexture.height; j++)
            {
                GenerateObject(i, j);
            }
        }
    }
    void GenerateObject(int x, int y)
    {
        // Read pixel color
        pixelColor = mapTexture.GetPixel(x, y);

        if (pixelColor.a == 0) return; // Pass that pixel

        foreach (PixelToObject pixelColorMapping in pixelColorMappings)
        {
            // Search for the matching color
            if (pixelColorMapping.pixelColor.Equals(pixelColor))
            {
                Vector3 pos = new Vector3(0, (y * prefabSize) + prefabSize / 2, (x * prefabSize) + prefabSize / 2);
                Instantiate(pixelColorMapping.prefab, pos, Quaternion.identity);
            }
        }
    }
}
