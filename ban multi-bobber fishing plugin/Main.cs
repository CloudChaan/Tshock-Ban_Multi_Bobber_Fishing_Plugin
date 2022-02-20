using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using OTAPI;

namespace ban_multi_bobber_fishing_plugin
{ 
    [ApiVersion(2, 1)]

    public class Ban : TerrariaPlugin
    {
        public override string Author => "云海酱";

        public override string Description => "检查PE多钓线钓鱼作弊";

        public override string Name => "Ban Multi-Boober";

        public override Version Version
        {
            get { return new Version(1, 0, 0, 0); }
        }
        public Ban(Main game) : base(game)
        {
        }
        public static List<int> pole = new List<int>() { 2289, 2291, 2293, 2421, 4442, 4325, 2292, 2295, 2296, 2422, 2294 };
        public static System.Timers.Timer timer = new System.Timers.Timer(1000);


        public override void Initialize()
        {
            timer.Elapsed += new System.Timers.ElapsedEventHandler(BobberCheck);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
        }
        public void BobberCheck(object source, System.Timers.ElapsedEventArgs e)
        {
            TSPlayer[] playerList = TShock.Players;
            foreach (TSPlayer iPlayer in playerList)
            {
                if (iPlayer != null)
                {
                    Player it = iPlayer.TPlayer;
                    int held = it.HeldItem.netID;
                    if (pole.Exists(p => p == held))
                    {
                        int bobberNum = it.ownedProjectileCounts[360]
                            + it.ownedProjectileCounts[361]
                            + it.ownedProjectileCounts[363]
                            + it.ownedProjectileCounts[381]
                            + it.ownedProjectileCounts[775]
                            + it.ownedProjectileCounts[760]
                            + it.ownedProjectileCounts[362]
                            + it.ownedProjectileCounts[365]
                            + it.ownedProjectileCounts[366]
                            + it.ownedProjectileCounts[382]
                            + it.ownedProjectileCounts[364];
                        Console.WriteLine(iPlayer.Name + "浮标数" + bobberNum);
                        if (bobberNum > 1)
                        {
                            Console.WriteLine("检测到作弊");
                            iPlayer.Kick("使用多钓线钓鱼作弊", force: true);
                        }
                    }
                }
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

    }
}
