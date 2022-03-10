using LiveSplit.ComponentUtil;

namespace alyx_multiplayer
{
    class LuaUtils
    {
        private const string avatarScriptName = "\\move_avatar.lua";
        private const string headScriptName = "\\move_localHead.lua";
        private const string avatarEntityName = "avatar";

        // Account for the fact that the origin of info_player_start is below the tracked head, not at it. This value is in Hammer Units.
        private const int zOffset = 24;

        /// <summary>
        /// Write a Lua script for moving the avatar to a specific spot.
        /// </summary>
        /// <param name="scriptPath">Script path.</param>
        /// <param name="entPrefix">Prefix to add to avatarEntityName within the script.</param>
        /// <param name="pos">Position of the avatar.</param>
        /// <param name="ang">Angle of the avatar.</param>
        public static void WriteCoordsToScript(string scriptPath, string entPrefix, Vector3f pos, Vector3f ang)
        {
            System.Globalization.CultureInfo invariantCulture = new System.Globalization.CultureInfo("en-US");
            if (scriptPath.EndsWith("\\")) scriptPath.TrimEnd();

            try
            {
                string PosXstr = pos.X.ToString(invariantCulture);
                string PosYstr = pos.Y.ToString(invariantCulture);
                string PosZstr = (pos.Z+zOffset).ToString(invariantCulture);
                string PosVectorStr = PosXstr + "," + PosYstr + "," + PosZstr;

                string AngXstr = ang.X.ToString(invariantCulture);
                string AngYstr = ang.Y.ToString(invariantCulture);
                string AngZstr = ang.Z.ToString(invariantCulture);
                string AngVectorStr = PosXstr + "," + PosYstr + "," + PosZstr;

                System.IO.File.WriteAllText(scriptPath + avatarScriptName, "Entities:FindByName(nil, \"" + entPrefix + avatarEntityName + "\"):SetOrigin(Vector(" + PosVectorStr + "));\n" +
                "Entities:FindByName(nil, \"" + entPrefix + avatarEntityName + "\"):SetAngles(" + AngVectorStr + ")" );
            } catch
            {
                // That's fine, the file is being accessed by HL:A right now. Would normally throw System.IO.IOException.
            }

            WriteLocalHeadScript(scriptPath, entPrefix);
            
        }

        /// <summary>
        /// Write a Lua script for moving the avatar to a specific spot.
        /// </summary>
        /// <param name="scriptPath">Script path.</param>
        /// <param name="entPrefix">Prefix to add to headScriptName within the script.</param>
        private static void WriteLocalHeadScript(string scriptPath, string entPrefix)
        {
            try
            {
                System.IO.File.WriteAllText(scriptPath + headScriptName, "local playerCoords = Entities:FindByClassname(nil, \"player\"):GetCenter();\n" +
                    "local playerAngles = Entities:FindByClassname(nil, \"player\"):GetAnglesAsVector();\n" +
                    "(Entities: FindByName(nil, \"" + entPrefix + "localHead\")):SetOrigin(Vector(playerCoords[1], playerCoords[2], playerCoords[3]));\n" +
                    "(Entities:FindByName(nil, \"" + entPrefix + "localHead\")):SetAngles(playerAngles[1],playerAngles[2],playerAngles[3])");
            }
            catch
            {
                // That's fine, the file is being accessed by HL:A right now. Would normally throw System.IO.IOException.
            }
        }
    }
}
