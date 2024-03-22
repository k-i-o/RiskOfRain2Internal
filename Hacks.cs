using RoR2;
using UnityEngine;
using System;
using System.Linq;

namespace RiskOfRain2Internal
{
    class Hacks
    {
        public static bool npcESPEnabled = false;
        public static bool chestsESPEnabled = false;

        public static bool teleportFinderEnabled = false;
        public static Vector3 teleportPosition;

        public static GameObject GetGameObjectComponentByName(string gameObjectName, string componentName)
        {
            GameObject gameObject = GameObject.Find(gameObjectName);
            if (gameObject != null)
            {
                Component component = gameObject.GetComponent(componentName);
                if (component != null)
                {
                    return component.gameObject;
                }
                else
                {
                    Debug.LogWarning("The specified component name was not found on the game object.");
                }
            }
            else
            {
                Debug.LogError("The specified game object name was not found in the scene.");
            }

            return null;
        }

        public static GameObject[] GetGameObjectsByName(string name)
        {
            GameObject[] allObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            GameObject[] parentObjects = allObjects.Where(obj => obj.name.Contains(name)).ToArray();

            if (parentObjects.Length > 0)
            {
                return parentObjects;
            }
            else
            {
                Debug.LogError("The specified game objects name was not found in the scene.");
            }

            return null;
        }

        public static void GiveExp()
        {
            GameObject result = GetGameObjectComponentByName("PlayerMaster(Clone)", "RoR2.CharacterMaster");

            if(result != null)
            {
                CharacterMaster component = result.GetComponent<CharacterMaster>();
                
                if(component != null)
                {
                    component.GiveExperience(9999999999999);
                }
            }

        }

        public static void GiveMoney()
        {
            GameObject result = GetGameObjectComponentByName("PlayerMaster(Clone)", "RoR2.CharacterMaster");

            if (result != null)
            {
                CharacterMaster component = result.GetComponent<CharacterMaster>();

                if (component != null)
                {
                    component.GiveMoney(999999999);
                }
            }

        }

        public static void GiveRandomEquipment()
        {
            GameObject result = GetGameObjectComponentByName("PlayerMaster(Clone)", "RoR2.CharacterMaster");

            if (result != null)
            {
                Inventory component = result.GetComponent<Inventory>();

                if (component != null)
                {
                    component.GiveRandomEquipment();
                }
            }
        }

        public static void GiveRandomItems()
        {
            GameObject result = GetGameObjectComponentByName("PlayerMaster(Clone)", "RoR2.CharacterMaster");

            if (result != null)
            {
                Inventory component = result.GetComponent<Inventory>();

                if (component != null)
                {
                    component.GiveRandomItems(99, true, true);
                }
            }
        }

        public static void EnableTeleportFinder()
        {
            teleportFinderEnabled = !teleportFinderEnabled;

            if (teleportFinderEnabled)
            {
                GameObject result = GetGameObjectComponentByName("Teleporter1(Clone)", "RoR2.TeleporterInteraction");

                if (result != null)
                {
                    teleportPosition = result.transform.position;
                }
            } else
            {
                teleportPosition = new Vector3();
            }
        }

        public static void EnableNPCsESP()
        {
            npcESPEnabled = !npcESPEnabled;
        }

        public static void EnableChestsESP()
        {
            chestsESPEnabled = !chestsESPEnabled;
        }

    }
}
