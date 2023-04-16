using System.Collections.Generic;
using UnityEngine;

public class DiceMaterialChanger : MonoBehaviour
{
    public string diceTag = "dice"; // The tag used to identify the dice objects
    public Material[] materials; // An array of materials to apply to the dice objects
    public Material[] groundMaterials; // An array of materials to apply to objects with the "Ground" tag

    private int currentMaterialIndex = 0; // The current index of the material array for dice objects
    private int currentGroundMaterialIndex = 0; // The current index of the material array for objects with the "Ground" tag
    private List<GameObject> diceObjects; // A list of all the dice objects in the scene
    private List<GameObject> groundObjects; // A list of all the objects with the "Ground" tag in the scene

    private void Start()
    {
        diceObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag(diceTag));
        groundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Ground"));
        ApplyMaterialToDiceObjects(materials[currentMaterialIndex]);
        ApplyMaterialToGroundObjects(groundMaterials[currentGroundMaterialIndex]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;
            ApplyMaterialToDiceObjects(materials[currentMaterialIndex]);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            currentGroundMaterialIndex = (currentGroundMaterialIndex + 1) % groundMaterials.Length;
            ApplyMaterialToGroundObjects(groundMaterials[currentGroundMaterialIndex]);
        }
    }

    private void ApplyMaterialToDiceObjects(Material material)
    {
        foreach (GameObject diceObject in diceObjects)
        {
            Renderer renderer = diceObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = material;
            }
        }
    }

    private void ApplyMaterialToGroundObjects(Material material)
    {
        foreach (GameObject groundObject in groundObjects)
        {
            Renderer renderer = groundObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = material;
            }
        }
    }
}
