using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace InstallerTest
{
    [TestClass]
    public class WixTDDInstallerShould
    {
        
        [AssemblyInitialize]
        public static void Startup(TestContext testContext)
        {
            String msiFile = GetFullPathToMsiFile();
            Process.Start("MSIEXEC", @"/i " + msiFile + " /passive").WaitForExit();
        }

        private static String GetFullPathToMsiFile()
        {
            String currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            String msiPath = Regex.Split(currentPath, "WixTDD").First() + @"WixTDD\WixTDDInstaller\bin\Debug\";
            String msiFile = msiPath + "WixTDDInstaller.msi";
            return msiFile;
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            String msiFile = GetFullPathToMsiFile();
            Process.Start("MSIEXEC", @"/x " + msiFile + " /passive").WaitForExit();
        }

        [TestMethod]
        public void CopiesWixTddExeToInstallFolder()
        {
            String fileName = @"C:\Program Files (x86)\WixTDDInstaller\WixTDD.exe";
            String errorMessage = fileName + " was not found";
            Assert.IsTrue(File.Exists(fileName), errorMessage);
        }

        [TestMethod]
        public void CopiesWixTddPdbToInstallFolder()
        {
            String fileName = @"C:\Program Files (x86)\WixTDDInstaller\WixTDD.pdb";
            String errorMessage = fileName + " was not found";
            Assert.IsTrue(File.Exists(fileName), errorMessage);
        }
    }
}
