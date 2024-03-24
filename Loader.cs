using MelonLoader;
using UnityEngine;
using RoR2;
using System.Collections.Generic;

namespace RiskOfRain2Internal
{
    public class GUIManager : MonoBehaviour
    {
        private Rect windowRect = new Rect(30, 30, 200, 300);
        private Vector2 buttonSize = new Vector2(180, 30);

        private List<ButtonData> buttons = new List<ButtonData>();

        public void AddButton(string label, System.Action onClick)
        {
            buttons.Add(new ButtonData(label, onClick));
        }

        public void Update()
        {
            GUI.Window(0, windowRect, DrawWindow, "Mono Internal Cheats by Kio");
        }

        void DrawWindow(int windowID)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (GUI.Button(new Rect(10, 30 * (i + 1), buttonSize.x, buttonSize.y), buttons[i].label))
                {
                    buttons[i].onClick?.Invoke();
                }
            }

            GUI.DragWindow(new Rect(0, 0, 200, 40)); 
        }

    }

    public class ButtonData
    {
        public string label;
        public System.Action onClick;

        public ButtonData(string label, System.Action onClick)
        {
            this.label = label;
            this.onClick = onClick;
        }
    }
    public class Loader: MelonMod
    {

        private GUIManager guiManager;
        private bool show = true;

        public override void OnApplicationStart()
        {
            GameObject guiManagerObject = new GameObject("GUIManager");
            guiManager = guiManagerObject.AddComponent<GUIManager>();

            guiManager.AddButton("Give 9999999999999 Exp", () =>
            {
                Hacks.GiveExp();
            });

            guiManager.AddButton("Give 999999999 Money", () =>
            {
                Hacks.GiveMoney();
            });

            guiManager.AddButton("Give Random Items", () =>
            {
                Hacks.GiveRandomItems();
            });

            guiManager.AddButton("Give Random Equipment", () =>
            {
                Hacks.GiveRandomEquipment();
            });

            guiManager.AddButton("Teleport Finder", () =>
            {
                Hacks.EnableTeleportFinder();
            });

            guiManager.AddButton("NPCs ESP", () =>
            {
                Hacks.EnableNPCsESP();
            });

            guiManager.AddButton("Chests ESP", () =>
            {
                Hacks.EnableChestsESP();
            });

            guiManager.AddButton("All chests free", () =>
            {
                Hacks.AllChestFree();
            });

            guiManager.AddButton("Chests Tier3 Chance 100%", () =>
            {
                Hacks.ChestsTier3Chance();
            });
        }

        public override void OnUpdate()
        {
            if(Input.GetKeyDown(KeyCode.F6))
            {
                show = !show;
                Cursor.visible = show;
            }
        }

        public override void OnGUI()
        {

            if (show) { 
                guiManager.Update();
            }

            { // ESPs

                if (Hacks.teleportFinderEnabled)
                {
                    GUIHelper.DrawESPLine(Hacks.teleportPosition, Color.green, 2f);
                }

                if (Hacks.npcESPEnabled)
                {
                    GameObject[] NPCs = Hacks.GetGameObjectsByName("Body");

                    foreach (GameObject NPC in NPCs)
                    {
                        if (NPC.CompareTag("Player")) continue;

                        GUIHelper.DrawESPBox(NPC.transform.position, Color.red, 1f);
                        GUIHelper.DrawESPLine(NPC.transform.position, Color.red, 1f);
                    }
                }

                if (Hacks.chestsESPEnabled)
                {
                    GameObject[] chests = Hacks.GetGameObjectsByName("Chest");

                    foreach (GameObject chest in chests)
                    {
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
}
