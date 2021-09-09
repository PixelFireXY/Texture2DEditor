using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Texture2DEditor
{
    public static Texture2D CloneTexture(Texture2D originalTexture)
    {
        //Save the image to the Texture2D
        Texture2D cloneTexture = new Texture2D(originalTexture.width, originalTexture.height);
        Color32[] colorArray = originalTexture.GetPixels32();

        cloneTexture.SetPixels32(colorArray);
        cloneTexture.Apply();

        return cloneTexture;
    }

    public static Texture2D RotateTexture(Texture2D originalTexture, bool clockwise)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;

        for (int textureHeight = 0; textureHeight < h; ++textureHeight)
        {
            for (int textureWidth = 0; textureWidth < w; ++textureWidth)
            {
                iRotated = (textureWidth + 1) * h - textureHeight - 1;
                iOriginal = !clockwise ? original.Length - 1 - (textureHeight * w + textureWidth) : textureHeight * w + textureWidth;
                rotated[iRotated] = original[iOriginal];
            }
        }

        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;
    }

    public static Texture2D ResizeTexture(Texture2D texture2D, int targetX, int targetY)
    {
        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;
    }

    /// <summary>
    /// Flip the texture vertically.
    /// </summary>
    /// <see cref="https://stackoverflow.com/questions/54623187/unity-flip-texture-using-c-sharp"/>
    /// <param name="original">The texture to flip.</param>
    /// <returns>The texture flipped.</returns>
    public static Texture2D FlipTextureVertically(Texture2D original)
    {
        var originalPixels = original.GetPixels32();

        Color32[] newPixels = new Color32[originalPixels.Length];

        int width = original.width;
        int height = original.height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                newPixels[x + y * width] = originalPixels[x + (height - y - 1) * width];
            }
        }

        Texture2D verticalFlippedTexture = new Texture2D(width, height);
        verticalFlippedTexture.SetPixels32(newPixels);
        verticalFlippedTexture.Apply();

        return verticalFlippedTexture;
    }

    /// <summary>
    /// Flip the texture horizontally.
    /// </summary>
    /// <see cref="https://stackoverflow.com/questions/54623187/unity-flip-texture-using-c-sharp"/>
    /// <param name="original">The texture to flip.</param>
    /// <returns>The texture flipped.</returns>
    public static Texture2D FlipTextureHorizontaly(Texture2D original)
    {
        var originalPixels = original.GetPixels32();

        Color32[] newPixels = new Color32[originalPixels.Length];

        int width = original.width;
        int height = original.height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                newPixels[x + y * width] = originalPixels[(width - x - 1) + y * width];
            }
        }

        Texture2D verticalFlippedTexture = new Texture2D(width, height);
        verticalFlippedTexture.SetPixels32(newPixels);
        verticalFlippedTexture.Apply();

        return verticalFlippedTexture;
    }
}
