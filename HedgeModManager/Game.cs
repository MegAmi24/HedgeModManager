﻿using HedgeModManager.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HedgeModManager
{
    public static class Games
    {
        public const uint CodeLoaderMinCodeVersionStringId = 101;
        public const string EmbeddedCPKREDIRVersion = "0.5.0.8";

        public static Game Unknown = new Game();
        public static Game SonicGenerations = new Game()
        {
            GameName = "Sonic Generations",
            ExecuteableName = "SonicGenerations.exe",
            HasCustomLoader = true,
            SupportsCPKREDIR = true,
            ModLoaderDownloadURL = Resources.URL_GCL_DL,
            ModLoaderData = EmbeddedLoaders.GenerationsCodeLoader,
            CustomLoaderName = "Generations Code Loader",
            CustomLoaderFileName = "d3d9.dll",
            AppID = "71340",
            DirectXVersion = 9,
            GBProtocol = "hedgemmgens",
            Is64Bit = false,
            CodesURL = Resources.URL_GCL_CODES
        };

        public static Game SonicLostWorld = new Game()
        {
            GameName = "Sonic Lost World",
            ExecuteableName = "slw.exe",
            HasCustomLoader = true,
            SupportsCPKREDIR = true,
            ModLoaderDownloadURL = Resources.URL_LCL_DL,
            ModLoaderData = EmbeddedLoaders.LostCodeLoader,
            CustomLoaderName = "Lost Code Loader",
            CustomLoaderFileName = "d3d9.dll",
            AppID = "329440",
            DirectXVersion = 9,
            GBProtocol = "hedgemmlw",
            Is64Bit = false,
            CodesURL = Resources.URL_LCL_CODES
        };

        public static Game SonicForces = new Game()
        {
            GameName = "Sonic Forces",
            ExecuteableName = "Sonic Forces.exe",
            HasCustomLoader = true,
            SupportsCPKREDIR = false,
            ModLoaderDownloadURL = Resources.URL_FML_DL,
            ModLoaderData = EmbeddedLoaders.ForcesModLoader,
            CustomLoaderName = "Forces Mod Loader",
            CustomLoaderFileName = "d3d11.dll",
            AppID = "637100",
            DirectXVersion = 11,
            GBProtocol = "hedgemmforces",
            Is64Bit = true,
            CodesURL = Resources.URL_FML_CODES
        };

        public static IEnumerable<Game> GetSupportedGames()
        {
            yield return SonicGenerations;
            yield return SonicLostWorld;
            yield return SonicForces;
        }
    }

    internal static class EmbeddedLoaders
    {
        public static byte[] GenerationsCodeLoader;
        public static byte[] ForcesModLoader;
        public static byte[] LostCodeLoader;

        static EmbeddedLoaders()
        {
            using (var stream = new MemoryStream(Resources.DAT_LOADERS_ZIP))
            using (var zip = new ZipArchive(stream))
            {
                GenerationsCodeLoader = GetFile("SonicGenerationsCodeLoader.dll");
                ForcesModLoader = GetFile("ForcesModLoader.dll");
                LostCodeLoader = GetFile("LostCodeLoader.dll");

                byte[] GetFile(string name)
                {
                    var entry = zip.GetEntry(name);
                    using (var file = entry.Open())
                    {
                        var buffer = new byte[entry.Length];
                        file.Read(buffer, 0, buffer.Length);
                        return buffer;
                    }
                }
            }
        }
    }

    public class Game
    {
        public string GameName = "Unnamed Game";
        public string ExecuteableName = string.Empty;
        public string ModLoaderDownloadURL = string.Empty;
        public byte[] ModLoaderData = null;
        public uint DirectXVersion = uint.MaxValue;
        public bool HasCustomLoader = false;
        public bool SupportsCPKREDIR = false;
        public string CustomLoaderName = "None";
        public string CustomLoaderFileName = string.Empty;
        public string AppID = "0";
        public string GBProtocol;
        public bool Is64Bit = false;
        public string CodesURL;

        public override string ToString()
        {
            return GameName;
        }
    }
}
