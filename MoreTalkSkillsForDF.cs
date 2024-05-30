using MelonLoader;
using HarmonyLib;
using Il2Cpp;
using more_talk_skills_for_df;
using Il2Cppnewbattle_H;
[assembly: MelonInfo(typeof(MoreTalkSkillsForDF), "More talk skills for Demi-fiend", "1.0.0", "Matthiew Purple")]
[assembly: MelonGame("アトラス", "smt3hd")]

namespace more_talk_skills_for_df;
public class MoreTalkSkillsForDF : MelonMod
{
    [HarmonyPatch(typeof(nbCommSelProcess), nameof(nbCommSelProcess.DispCommandList2))]
    private class Patch
    {
        // Before displaying the battle command panel
        public static void Prefix(ref nbCommSelProcessData_t s)
        {
            // If it's Demi-fiend's turn
            if (s.my.formindex == 0)
            {
                // Create a list of the following skills:
                // Talk, Brainwash, Dark Pledge, Threaten, Begging, Trade, Loan and Stone Hunt
                var talkIndices = new List<ushort> { 32772, 388, 390, 398, 397, 400, 401, 399 };

                // Fill an array of 288 slots with those skills
                var talkCommands = new ushort[288];
                for (ushort i = 0; i < talkIndices.Count; i++)
                    talkCommands[i] = talkIndices[i];

                // Replace the command list by this list
                s.commlist[1] = talkCommands;

                // Tell the game how many skills are usable (here 8)
                s.commcnt[1] = talkIndices.Count;
            }
        }
    }
}
