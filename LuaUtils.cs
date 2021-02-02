using LiveSplit.ComponentUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alyx_multiplayer
{
    class LuaUtils
    {
        private const string scriptName = "\\networking_io.lua";
        private const string avatarEntityName = "moveEnt";

        /// <summary>
        /// Write a Lua script for moving the avatar to a specific spot.
        /// </summary>
        /// <param name="scriptPath"></param>
        /// <param name="pos">Position of the avatar.</param>
        /// <param name="ang">Angle of the avatar.</param>
        public static void WriteCoordsToScript(string scriptPath, Vector3f pos, Vector3f ang)
        {
            if (scriptPath.EndsWith("\\")) scriptPath.TrimEnd();

            System.IO.File.WriteAllText(scriptPath + scriptName, "Entities:FindByName(nil, \"" + avatarEntityName + "\")):SetOrigin(Vector(" + pos.X + "," + pos.Y + "," + pos.Z + "));\n" +
                "Entities:FindByName(nil, \"" + avatarEntityName + "\")):SetAngles(" + ang.X + "," + ang.Y + "," + ang.Z + ")");
        }
    }
}
