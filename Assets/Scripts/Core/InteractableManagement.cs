using UnityEngine;
using System.Collections.Generic;

public class InteractableManagement : MonoBehaviour,IController
{
    private List<Interactable> interactables = new List<Interactable>();

    public void AddInteractable(Interactable newInteractable)
    {
        interactables.Add(newInteractable);
    }

    public void Initialization()
    {
        foreach (var intrantable in interactables)
        {
            if (intrantable is InteractableChest chest)
            {
                var count = Random.Range(3, 24);
                for (int i = 0; i < count; i++)
                {
                    chest.AddItem(GameManager.GameData.GetEquipmentItems().RandomItem());
                }
            }
        }
    }
}