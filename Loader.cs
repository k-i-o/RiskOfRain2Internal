using MelonLoader;
using UnityEngine;
using RoR2;

namespace RiskOfRain2Internal
{
    public class Loader: MelonMod
    {

        public override void OnGUI()
        {
            int buttonStart = 30;
            int buttonBase = 40;

            GUI.Box(new Rect(25, 25, 200, buttonStart + (buttonBase * 5)), "Cheat by kio");
            if(GUI.Button(new Rect(27, buttonStart + (buttonBase), 196, 40), "Give 9999999999999 Exp"))
            {
                Hacks.GiveExp();
            }
            if (GUI.Button(new Rect(27, buttonStart + (buttonBase * 2), 196, 40), "Give 999999999 Money"))
            {
                Hacks.GiveMoney();
            }
            if (GUI.Button(new Rect(27, buttonStart + (buttonBase * 3), 196, 40), "Give Random Items"))
            {
                Hacks.GiveRandomItems();
            }
            if (GUI.Button(new Rect(27, buttonStart + (buttonBase * 4), 196, 40), "Give Random Equipment"))
            {
                Hacks.GiveRandomEquipment();
            }
            if (GUI.Button(new Rect(27, buttonStart + (buttonBase * 5), 196, 40), "Teleport Finder"))
            {
                Hacks.EnableTeleportFinder();
            }
            if (GUI.Button(new Rect(27, buttonStart + (buttonBase * 6), 196, 40), "NPCs ESP"))
            {
                Hacks.EnableNPCsESP();
            }            
            if (GUI.Button(new Rect(27, buttonStart + (buttonBase * 7), 196, 40), "Chests ESP"))
            {
                Hacks.EnableChestsESP();
            }
            if (GUI.Button(new Rect(27, buttonStart + (buttonBase * 8), 196, 40), "All chests free"))
            {
                Hacks.AllChestFree();
            }
            if (GUI.Button(new Rect(27, buttonStart + (buttonBase * 9), 196, 40), "Chests Tier3 Chance 100%"))
            {
                Hacks.ChestsTier3Chance();
            }

            if (Hacks.teleportFinderEnabled)
            {
                GUIHelper.DrawESPLine(Hacks.teleportPosition, Color.green, 2f);
            }

            if(Hacks.npcESPEnabled)
            {
                GameObject[] NPCs = Hacks.GetGameObjectsByName("Body");
                
                foreach (GameObject NPC in NPCs) {
                    if (NPC.CompareTag("Player")) continue;

                    GUIHelper.DrawESPBox(NPC.transform.position, Color.red, 1f);
                    GUIHelper.DrawESPLine(NPC.transform.position, Color.red, 1f);
                }
            }

            if(Hacks.chestsESPEnabled)
            {
                GameObject[] chests = Hacks.GetGameObjectsByName("Chest");
                
                foreach (GameObject chest in chests) {
                    ChestBehavior chestBehavior = chest.GetComponent<ChestBehavior>();
                    if (!chestBehavior) continue;

                    PurchaseInteraction purchaseInteraction = chest.GetComponent<PurchaseInteraction>();
                    if (!purchaseInteraction.available) continue;

                    GUIHelper.DrawESPBox(chest.transform.position, Color.blue, 1f);
                    GUIHelper.DrawESPLine(chest.transform.position, Color.blue, 1f);
                }
            }
        }

    }
}
