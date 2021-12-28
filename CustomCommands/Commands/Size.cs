﻿using Synapse.Command;
using Synapse.Api;
using Synapse.Api.Plugin;

namespace CustomCommands.Commands
{

    [CommandInformation(
        Name = "size",
        Aliases = new string[] { },
        Description = "change the size of a player",
        Permission = "twin.size",
        Platforms = new[] { Platform.RemoteAdmin },
        Usage = "size x y z"
        )]

    public class Size : ISynapseCommand
    {
        public CommandResult Execute(CommandContext context)
        {

            var result = new CommandResult();
            if (context.Arguments.Count == 4)
            {
                var parg = context.Arguments.Array[1];
                var xarg = context.Arguments.Array[2];
                var yarg = context.Arguments.Array[3];
                var zarg = context.Arguments.Array[4];

                if (float.TryParse(xarg, out var x) && float.TryParse(yarg, out var y) && float.TryParse(zarg, out var z))
                {
                    bool exec = false;
                    foreach (Player players in SynapseController.Server.Players)
                    {
                        if (players.NickName.ToLower().Contains(parg.ToLower()))
                        {
                            players.Scale = new UnityEngine.Vector3(float.Parse(xarg), float.Parse(yarg), float.Parse(zarg));
                            exec = true;
                        }
                    }
                    if (exec)
                    {
                        result.Message = "La taille du/des joueur/s a été changé";
                        result.State = CommandResultState.Ok;

                    }
                    else
                    {
                        result.Message = "Le joueur n'a pas été trouvé";
                        result.State = CommandResultState.Error;

                    }

                }
                else
                {
                    result.Message = "La taille donné est inutilisable";
                    result.State = CommandResultState.Error;
                }
            }
            else
            {
                result.State = CommandResultState.Error;
                result.Message = "Vous n'avez pas entré le bon nombre d'arguments";
            }

            return result;
        }
    }
}
