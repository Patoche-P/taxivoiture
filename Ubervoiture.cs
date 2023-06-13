using System;
using System.Collections.Generic;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace TestRessource
{
    public class Class1 : BaseScript
    {
        public Class1()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string TestRessource)
        {
            if (GetCurrentResourceName() != TestRessource) return;

            RegisterCommand("car", new Action<int, List<object>, string>((source, args, raw) =>
            {
                if (args.Count == 0)
                {
                    // Le joueur n'a pas spécifié de nom de véhicule
                    TriggerEvent("chat:addMessage", new
                    {
                        color = new[] {255, 0, 0},
                        args = new[] {"[UberVoiture]", "Votre chauffeur ne sait pas quel véhicule vous apporter, veuillez préciser un nom."}
                    });
                    return;
                }

                // Récupérer les coordonnées du joueur
                Vector3 playerPos = Game.PlayerPed.Position;

                // Créer le véhicule à la position du joueur
                Vehicle vehicle = new Vehicle(API.GetHashKey(args[0].ToString()), playerPos, 0.0f, true, false);

                // Envoyer un message de confirmation dans le chat
                TriggerEvent("chat:addMessage", new
                {
                    color = new[] {255, 0, 0},
                    args = new[] {"[UberVoiture]", $"Vous avez commandé une {args[0]} !"}
                });

            }), false);

        
        

        }
    }
}



