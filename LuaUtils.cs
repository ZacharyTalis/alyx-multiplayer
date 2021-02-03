using LiveSplit.ComponentUtil;

namespace alyx_multiplayer
{
    class LuaUtils
    {
        private const string scriptName = "\\move_avatar.lua";
        private const string avatarEntityName = "avatar";

        // Account for the fact that the origin of info_player_start is below the tracked head, not at it. This value is in Hammer Units.
        private const int zOffset = 24;

        /// <summary>
        /// Write a Lua script for moving the avatar to a specific spot.
        /// </summary>
        /// <param name="scriptPath"></param>
        /// <param name="pos">Position of the avatar.</param>
        /// <param name="ang">Angle of the avatar.</param>
        public static void WriteCoordsToScript(string scriptPath, Vector3f pos, Vector3f ang)
        {
            if (scriptPath.EndsWith("\\")) scriptPath.TrimEnd();

            try
            {
                System.IO.File.WriteAllText(scriptPath + scriptName, "Entities:FindByName(nil, \"" + avatarEntityName + "\"):SetOrigin(Vector(" + pos.X + "," + pos.Y + "," + (pos.Z + zOffset) + "));\n" +
                "Entities:FindByName(nil, \"" + avatarEntityName + "\"):SetAngles(" + ang.X + "," + ang.Y + "," + ang.Z + ")");
            } catch
            {
                // That's fine, the file is being accessed by HL:A right now. Would normally throw System.IO.IOException.
            }
            
        }
    }
}
