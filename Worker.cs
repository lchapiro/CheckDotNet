using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace CheckDotNet
{
    class AxWorker
    {
        private Logger _oLog;

        const string GSzNetfx10RegKeyName = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\.NETFramework\\Policy\\v1.0";
        const string GSzNetfx10RegKeyValue = "3705";
        //const string GSzNetfx10SpxMsiRegKeyName = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Active Setup\\Installed Components\\{78705f0d-e8db-4b2d-8193-982bdda15ecd}";
        //const string GSzNetfx10SpxOcmRegKeyName = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Active Setup\\Installed Components\\{FDC11A6F-17D1-48f9-9EA3-9051954BAA24}";
        const string GSzNetfx11RegKeyName = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\NET Framework Setup\\NDP\\v1.1.4322";
        const string GSzNetfx20RegKeyName = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\NET Framework Setup\\NDP\\v2.0.50727";
        const string GSzNetfx30RegKeyName = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0\\Setup";
        const string GSzNetfx30SpRegKeyName = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0";
        const string GSzNetfx30RegValueName = "InstallSuccess";
        const string GSzNetfx35RegKeyName = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\NET Framework Setup\\NDP\\v3.5";
        const string GSzNetfx40ClientRegKeyName = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Client";
        const string GSzNetfx40FullRegKeyName = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full";
        const string GSzNetfx40SPxRegValueName = "Servicing";
        const string GSzNetfx45RegKeyName = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full";
        const string GSzNetfx45RegValueName = "Release";
        const string GSzNetfxStandardRegValueName = "Install";
        const string GSzNetfxStandardSPxRegValueName = "SP";
        const string GSzNetfxStandardVersionRegValueName = "Version";

        /* Version information for final release of .NET Framework 3.0 */
        const int GiNetfx30VersionMajor = 3;
        const int GiNetfx30VersionMinor = 0;
        const int GiNetfx30VersionBuild = 4506;
        const int GiNetfx30VersionRevision = 26;

        /* Version information for final release of .NET Framework 3.5 */
        const int GiNetfx35VersionMajor = 3;
        const int GiNetfx35VersionMinor = 5;
        const int GiNetfx35VersionBuild = 21022;
        const int GiNetfx35VersionRevision = 8;

        /* Version information for final release of .NET Framework 4 */
        const int GiNetfx40VersionMajor = 4;
        const int GiNetfx40VersionMinor = 0;
        const int GiNetfx40VersionBuild = 30319;
        const int GiNetfx40VersionRevision = 0;

        /* Version information for final release of .NET Framework 4.5 */
        const int GDwNetfx45ReleaseVersion = 378389;

        /* Version information for final release of .NET Framework 4.5.1 */
        const int GDwNetfx451ReleaseVersion = 378675;

        /* Version information for final release of .NET Framework 4.5.2 */
        const int GDwNetfx452ReleaseVersion = 379893;

        /* Version information for final release of .NET Framework 4.6 */
        const int GDwNetfx46ReleaseVersion = 393295;

        /* Version information for final release of .NET Framework 4.6.1 */
        const int GDwNetfx461ReleaseVersion = 394254;

        // Leo, 16.08.2018 .NET Framework 4.6.2
        /* Windows Server 2016: 394802  */
        const int GDwNetfx462ServerVersion = 394254;

        /* On all other OS versions: 394806	*/
        const int GDwNetfx462ReleaseVersion = 394806;

        // Leo, 16.08.2018 .NET Framework 4.7
        /* Windows 10 Creators Update: 460798 */
        const int GDwNetfx47UpdateVersion = 460798;

        /* On all other OS versions: 460805	*/
        const int GDwNetfx47ReleaseVersion = 460805;

        // Leo, 16.08.2018 .NET Framework 4.7.1
        /* Windows 10 Creators Update: 461308 */
        const int GDwNetfx471UpdateVersion = 461308;

        /* On all other OS versions: 461310		.NET Framework 4.7.1 */
        const int GDwNetfx471ReleaseVersion = 461310;

        // Leo, 16.08.2018 .NET Framework 4.7.2
        /* Windows 10 April 2018: 461808 */
        const int GDwNetfx472UpdateVersion = 461808;

        /* On all other OS versions: 461814		.NET Framework 4.7.2 */
        const int GDwNetfx472ReleaseVersion = 461814;

        public AxWorker()
        {
            _oLog = new Logger();
        }

        public Logger GetLogger()
        {
            return _oLog;
        }

        public bool IsNetAfter40Installed()
        {
            return IsNetfx40ClientInstalled() || IsNetfx40FullInstalled() || IsNetfx45Installed() ||
                    IsNetfx451Installed() || IsNetfx452Installed() || IsNetfx46Installed() || IsNetfx461Installed() ||
                    IsNetfx462Installed() || IsNetfx47Installed() || IsNetfx471Installed() || IsNetfx472Installed();
        }

        public List<String> GetFoundedDotNet()
        {
            var list = new List<string>();

            bool bNetfx10Installed = IsNetfx10Installed();
            bool bNetfx11Installed = IsNetfx11Installed();
            bool bNetfx20Installed = IsNetfx20Installed();

            /* The .NET Framework 3.0 is an add-in that installs
            // on top of the .NET Framework 2.0.  For this version
            // check, validate that both 2.0 and 3.0 are installed. */
            bool bNetfx30Installed = (IsNetfx20Installed() && IsNetfx30Installed());

            /* The .NET Framework 3.5 is an add-in that installs
            // on top of the .NET Framework 2.0 and 3.0.  For this version
            // check, validate that 2.0, 3.0 and 3.5 are installed. */
            bool bNetfx35Installed = (IsNetfx20Installed() && IsNetfx30Installed() && IsNetfx35Installed());

            bool bNetfx40ClientInstalled = (IsNetfx40ClientInstalled());
            bool bNetfx40FullInstalled = (IsNetfx40FullInstalled());

            bool bNetfx45Installed = (IsNetfx45Installed());
            bool bNetfx451Installed = (IsNetfx451Installed());
            bool bNetfx452Installed = (IsNetfx452Installed());
            bool bNetfx46Installed = (IsNetfx46Installed());
            bool bNetfx461Installed = (IsNetfx461Installed());

            // Leo, 16.08.2018
            bool bNetfx462Installed = (IsNetfx462Installed());
            bool bNetfx47Installed = (IsNetfx47Installed());
            bool bNetfx471Installed = (IsNetfx471Installed());
            bool bNetfx472Installed = (IsNetfx472Installed());

            /* If .NET Framework 1.0 is installed, get the service pack level */
            if (bNetfx10Installed)
                list.Add(".NET Framework 1.0 is installed.");
            else
                list.Add(".NET Framework 1.0 is not installed.");

            /* If .NET Framework 1.1 is installed, get the  service pack level */
            if (bNetfx11Installed)
            {
                int iNetfx11SpLevel = GetNetfxSpLevel(GSzNetfx11RegKeyName, GSzNetfxStandardSPxRegValueName);

                if (iNetfx11SpLevel > 0)
                    list.Add(".NET Framework 1.1 with service pack is installed.");
                else
                    list.Add(".NET Framework 1.1 is installed with no service packs.");
            }
            else
            {
                list.Add(".NET Framework 1.1 is not installed.");
            }

            /* If .NET Framework 2.0 is installed, get the service pack level */
            if (bNetfx20Installed)
            {
                int iNetfx20SpLevel = GetNetfxSpLevel(GSzNetfx20RegKeyName, GSzNetfxStandardSPxRegValueName);

                if (iNetfx20SpLevel > 0)
                    list.Add(".NET Framework 2.0 with service pack is installed.");
                else
                    list.Add("NET Framework 2.0 is installed with no service packs.");
            }
            else
                list.Add(".NET Framework 2.0 is not installed.");

            /* If .NET Framework 3.0 is installed, get the service pack level */
            if (bNetfx30Installed)
            {
                int iNetfx30SpLevel = GetNetfxSpLevel(GSzNetfx30SpRegKeyName, GSzNetfxStandardSPxRegValueName);

                if (iNetfx30SpLevel > 0)
                {
                    string str = String.Format(".NET Framework 3.0 service pack {0} is installed.", iNetfx30SpLevel);
                    list.Add(str);
                }
                else
                    list.Add(".NET Framework 3.0 is installed with no service packs.");
            }
            else
                list.Add(".NET Framework 3.0 is not installed.");

            /* If .NET Framework 3.5 is installed, get the service pack level */
            if (bNetfx35Installed)
            {
                int iNetfx35SpLevel = GetNetfxSpLevel(GSzNetfx35RegKeyName, GSzNetfxStandardSPxRegValueName);

                if (iNetfx35SpLevel > 0)
                {
                    string str = String.Format(".NET Framework 3.5 service pack {0} is installed.", iNetfx35SpLevel);
                     list.Add(str);
                }
                else
                    list.Add(".NET Framework 3.5 is installed with no service packs.");
            }
            else
                list.Add(".NET Framework 3.5 is not installed.");

            /* If .NET Framework 4 Client is installed, get the service pack level */
            if (bNetfx40ClientInstalled)
            {
                int iNetfx40ClientSpLevel = GetNetfxSpLevel(GSzNetfx40ClientRegKeyName, GSzNetfx40SPxRegValueName);

                if (iNetfx40ClientSpLevel > 0)
                {
                    string str = String.Format(".NET Framework 4 client service pack {0} is installed.", iNetfx40ClientSpLevel);
                    list.Add(str);
                }
                else
                    list.Add(".NET Framework 4 client is installed with no service packs.");
            }
            else
            {
                list.Add(".NET Framework 4 client is not installed.");
            }

            /* If .NET Framework 4 Full is installed, get the service pack level */
            if (bNetfx40FullInstalled)
            {
                int iNetfx40FullSpLevel = GetNetfxSpLevel(GSzNetfx40FullRegKeyName, GSzNetfx40SPxRegValueName);

                if (iNetfx40FullSpLevel > 0)
                {
                    string str = String.Format(".NET Framework 4 full service pack {0} is installed.", iNetfx40FullSpLevel);
                    list.Add(str);   
                }
                else
                    list.Add(".NET Framework 4 full is installed with no service packs.");
            }
            else
            {
                list.Add(".NET Framework 4 full is not installed.");
            }

            /* If .NET Framework 4.5 is installed, get the service pack level */
            if (bNetfx45Installed)
            {
                var iNetfx45SpLevel = GetNetfxSpLevel(GSzNetfx45RegKeyName, GSzNetfx40SPxRegValueName);

                if (iNetfx45SpLevel > 0)
                {
                    string str = String.Format(".NET Framework 4.5 service pack {0} is installed.", iNetfx45SpLevel);
                    list.Add(str);
                }
                else
                    list.Add(".NET Framework 4.5 is installed with no service packs.");
            }
            else
                list.Add(".NET Framework 4.5 is not installed.");

            /* If .NET Framework 4.5.1 is installed, get the service pack level */
            if (bNetfx451Installed)
            {
                int iNetfx451SpLevel = GetNetfxSpLevel(GSzNetfx45RegKeyName, GSzNetfx40SPxRegValueName);

                if (iNetfx451SpLevel > 0)
                {
                    string str = String.Format(".NET Framework 4.5.1 service pack {0} is installed.", iNetfx451SpLevel);
                    list.Add(str);
                }
                else
                    list.Add("\n\n.NET Framework 4.5.1 is installed with no service packs.");
            }
            else
                list.Add("\n\n.NET Framework 4.5.1 is not installed.");

            /* If .NET Framework 4.5.2 is installed, get the service pack level */
            if (bNetfx452Installed)
            {
                int iNetfx452SpLevel = GetNetfxSpLevel(GSzNetfx45RegKeyName, GSzNetfx40SPxRegValueName);

                if (iNetfx452SpLevel > 0)
                {
                    string str = String.Format(".NET Framework 4.5.2 service pack {0} is installed.", iNetfx452SpLevel);
                    list.Add(str);
                }
                else
                    list.Add(".NET Framework 4.5.2 is installed with no service packs.");
            }
            else
                list.Add(".NET Framework 4.5.2 is not installed.");

            /* If .NET Framework 4.6 is installed, get the service pack level */
            if (bNetfx46Installed)
            {
                int iNetfx46SpLevel = GetNetfxSpLevel(GSzNetfx45RegKeyName, GSzNetfx40SPxRegValueName);

                if (iNetfx46SpLevel > 0)
                {
                    string str = String.Format(".NET Framework 4.6 service pack {0} is installed.", iNetfx46SpLevel);
                    list.Add(str);
                }
                else
                    list.Add(".NET Framework 4.6 is installed with no service packs.");
            }
            else
                list.Add(".NET Framework 4.6 is not installed.");

            /* If .NET Framework 4.6.1 is installed, get the service pack level */
            if (bNetfx461Installed)
            {
                int iNetfx461SpLevel = GetNetfxSpLevel(GSzNetfx45RegKeyName, GSzNetfx40SPxRegValueName);

                if (iNetfx461SpLevel > 0)
                {
                    string str = String.Format(".NET Framework 4.6.1 service pack {0} is installed.", iNetfx461SpLevel);
                    list.Add(str);
                }
                else
                    list.Add(".NET Framework 4.6.1 is installed with no service packs.");
            }
            else
                list.Add("\n\n.NET Framework 4.6.1 is not installed.");

            if (bNetfx462Installed)
            {

            }

            return list;
        }

        /******************************************************************
        Function Name:	IsNetfx10Installed
        Description:	Uses the detection method recommended at
        http://msdn.microsoft.com/library/ms994349.aspx
        to determine whether the .NET Framework 1.0 is
        installed on the machine
        Inputs:	        NONE
        Results:        true if the .NET Framework 1.0 is installed
        false otherwise
        ******************************************************************/
        public bool IsNetfx10Installed()
        {
	        var oRet = Registry.GetValue(GSzNetfx10RegKeyName, GSzNetfx10RegKeyValue, "");
            return (oRet != null);
        }


        /******************************************************************
        Function Name:	IsNetfx11Installed
        Description:	Uses the detection method recommended at
        http://msdn.microsoft.com/library/ms994339.aspx
        to determine whether the .NET Framework 1.1 is
        installed on the machine
        Inputs:	        NONE
        Results:        true if the .NET Framework 1.1 is installed
        false otherwise
        ******************************************************************/
        public bool IsNetfx11Installed()
        {
	        bool bRetValue = false;

            try
            {
                var oRet = Registry.GetValue(GSzNetfx11RegKeyName, GSzNetfxStandardRegValueName, "");
                if (oRet != null)
	            {
		            if ("1" == (string)oRet)
			            bRetValue = true;
	            }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }
            
	        return bRetValue;
        }


        /******************************************************************
        Function Name:	IsNetfx20Installed
        Description:	Uses the detection method recommended at
        http://msdn.microsoft.com/library/aa480243.aspx
        to determine whether the .NET Framework 2.0 is
        installed on the machine
        Inputs:	        NONE
        Results:        true if the .NET Framework 2.0 is installed
        false otherwise
        ******************************************************************/
        public bool IsNetfx20Installed()
        {
	        bool bRetValue = false;

            try
            {
                var oRet = Registry.GetValue(GSzNetfx20RegKeyName, GSzNetfxStandardRegValueName, "");
                if (oRet != null)
	            {
		            if (1 == (Int32)oRet)
			            bRetValue = true;
	            }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }
            

	        return bRetValue;
        }


        /******************************************************************
        Function Name:	IsNetfx30Installed
        Description:	Uses the detection method recommended at
        http://msdn.microsoft.com/library/aa964979.aspx
        to determine whether the .NET Framework 3.0 is
        installed on the machine
        Inputs:	        NONE
        Results:        true if the .NET Framework 3.0 is installed
        false otherwise
        ******************************************************************/
        public bool IsNetfx30Installed()
        {
	        bool bRetValue = false;

            try
            {
                /* Check that the InstallSuccess registry value exists and equals 1 */
                var oRet = Registry.GetValue(GSzNetfx30RegKeyName, GSzNetfx30RegValueName, "");
	            if (oRet != null)
	            {
		            if (1 == (Int32)oRet)
			            bRetValue = true;
	            }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }

	        /* A system with a pre-release version of the .NET Framework 3.0 can
	        have the InstallSuccess value.  As an added verification, check the
	        version number listed in the registry */
	        return (bRetValue && CheckNetfxBuildNumber(GSzNetfx30RegKeyName, GSzNetfxStandardVersionRegValueName, 
                GiNetfx30VersionMajor, GiNetfx30VersionMinor, GiNetfx30VersionBuild, GiNetfx30VersionRevision));
        }


        /******************************************************************
        Function Name:	IsNetfx35Installed
        Description:	Uses the detection method recommended at
        http://msdn.microsoft.com/library/cc160716.aspx
        to determine whether the .NET Framework 3.5 is
        installed on the machine
        Inputs:	        NONE
        Results:        true if the .NET Framework 3.5 is installed
        false otherwise
        ******************************************************************/
        public bool IsNetfx35Installed()
        {
	        bool bRetValue = false;

            try
            {
                /* Check that the Install registry value exists and equals 1 */
                var oRet = Registry.GetValue(GSzNetfx35RegKeyName, GSzNetfxStandardRegValueName, "");
	            if (oRet != null)
	            {
		            if (1 == (Int32)oRet)
			            bRetValue = true;
	            }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }
	        

	        /* A system with a pre-release version of the .NET Framework 3.5 can
	        have the Install value.  As an added verification, check the
	        version number listed in the registry */
	        return (bRetValue && CheckNetfxBuildNumber(GSzNetfx35RegKeyName, GSzNetfxStandardVersionRegValueName, 
                GiNetfx35VersionMajor, GiNetfx35VersionMinor, GiNetfx35VersionBuild, GiNetfx35VersionRevision));
        }


        /******************************************************************
        Function Name:	IsNetfx40ClientInstalled
        Description:	Uses the detection method recommended at
        http://msdn.microsoft.com/library/ee942965(v=VS.100).aspx
        to determine whether the .NET Framework 4 Client is
        installed on the machine
        Inputs:         NONE
        Results:        true if the .NET Framework 4 Client is installed
        false otherwise
        ******************************************************************/
        public bool IsNetfx40ClientInstalled()
        {
	        bool bRetValue = false;

            try
            {
                var oRet = Registry.GetValue(GSzNetfx40ClientRegKeyName, GSzNetfxStandardRegValueName, "");
                if (oRet != null)
	            {
		            if (1 == (Int32)oRet)
			            bRetValue = true;
	            }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }
            

	        /* A system with a pre-release version of the .NET Framework 4 can
	        have the Install value.  As an added verification, check the
	        version number listed in the registry */
	        return (bRetValue && CheckNetfxBuildNumber(GSzNetfx40ClientRegKeyName, GSzNetfxStandardVersionRegValueName,
                            GiNetfx40VersionMajor, GiNetfx40VersionMinor, GiNetfx40VersionBuild, GiNetfx40VersionRevision));
        }


        /******************************************************************
        Function Name:	IsNetfx40FullInstalled
        Description:	Uses the detection method recommended at
        http://msdn.microsoft.com/library/ee942965(v=VS.100).aspx
        to determine whether the .NET Framework 4 Full is
        installed on the machine
        Inputs:         NONE
        Results:        true if the .NET Framework 4 Full is installed
        false otherwise
        ******************************************************************/
        public bool IsNetfx40FullInstalled()
        {
	        bool bRetValue = false;

            try
            {
                var oRet = Registry.GetValue(GSzNetfx40FullRegKeyName, GSzNetfxStandardRegValueName, "");
                if (oRet != null)
	            {
		            if (1 == (Int32)oRet)
			            bRetValue = true;
	            }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);    
            }
            
	        /* A system with a pre-release version of the .NET Framework 4 can
	        have the Install value.  As an added verification, check the
	        version number listed in the registry */
	        return (bRetValue && CheckNetfxBuildNumber(GSzNetfx40FullRegKeyName, GSzNetfxStandardVersionRegValueName, 
                                GiNetfx40VersionMajor, GiNetfx40VersionMinor, GiNetfx40VersionBuild, GiNetfx40VersionRevision));
        }


        /******************************************************************
        Function Name:	IsNetfx45Installed
        Description:	Uses the detection method recommended at
        http://msdn.microsoft.com/en-us/library/ee942965(v=vs.110).aspx
        to determine whether the .NET Framework 4.5 is
        installed on the machine
        Inputs:         NONE
        Results:        true if the .NET Framework 4.5 is installed
        false otherwise
        ******************************************************************/
        public bool IsNetfx45Installed()
        {
	        bool bRetValue = false;

            try
            {
                var oRet = Registry.GetValue(GSzNetfx45RegKeyName, GSzNetfx45RegValueName, "");

                if (oRet != null)
	            {
		            if (GDwNetfx45ReleaseVersion <= (Int32)oRet)
			            bRetValue = true;
	            }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }
            

	        return bRetValue;
        }


        /******************************************************************
        Function Name:	IsNetfx451Installed
        Description:	Uses the detection method recommended at
        http://msdn.microsoft.com/en-us/library/ee942965(v=vs.110).aspx
        to determine whether the .NET Framework 4.5.1 is
        installed on the machine
        Inputs:         NONE
        Results:        true if the .NET Framework 4.5.1 is installed
        false otherwise
        ******************************************************************/
        public bool IsNetfx451Installed()
        {
	        bool bRetValue = false;

            try
            {
                var oRet = Registry.GetValue(GSzNetfx45RegKeyName, GSzNetfx45RegValueName, "");

                if (oRet != null)
	            {
		            if (GDwNetfx451ReleaseVersion <= (Int32)oRet)
			            bRetValue = true;
	            }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }
            
	        return bRetValue;
        }


        /******************************************************************
        Function Name:	IsNetfx452Installed
        Description:	Uses the detection method recommended at
        http://msdn.microsoft.com/en-us/library/ee942965(v=vs.110).aspx
        to determine whether the .NET Framework 4.5.2 is
        installed on the machine
        Inputs:         NONE
        Results:        true if the .NET Framework 4.5.2 is installed
        false otherwise
        ******************************************************************/
        public bool IsNetfx452Installed()
        {
	        bool bRetValue = false;

            try
            {
                var oRet = Registry.GetValue(GSzNetfx45RegKeyName, GSzNetfx45RegValueName, "");
                if (oRet != null)
	            {
		            if (GDwNetfx452ReleaseVersion <= (Int32)oRet)
			            bRetValue = true;
	            }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }
	        

	        return bRetValue;
        }


        /******************************************************************
        Function Name:	IsNetfx46Installed
        Description:	Uses the detection method recommended at
        http://msdn.microsoft.com/en-us/library/ee942965(v=vs.110).aspx
        to determine whether the .NET Framework 4.6 is
        installed on the machine
        Inputs:         NONE
        Results:        true if the .NET Framework 4.6 is installed
        false otherwise
        ******************************************************************/
        public bool IsNetfx46Installed()
        {
	        bool bRetValue = false;

            try
            {
                var oRet = Registry.GetValue(GSzNetfx45RegKeyName, GSzNetfx45RegValueName, "");
                if (oRet != null)
	            {
		            if (GDwNetfx46ReleaseVersion <= (Int32)oRet)
			            bRetValue = true;
	            }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }
	        

	        return bRetValue;
        }


        /******************************************************************
        Function Name:	IsNetfx461Installed
        Description:	Uses the detection method recommended at
        http://msdn.microsoft.com/en-us/library/ee942965(v=vs.110).aspx
        to determine whether the .NET Framework 4.6.1 is
        installed on the machine
        Inputs:         NONE
        Results:        true if the .NET Framework 4.6.1 is installed
        false otherwise
        ******************************************************************/
        public bool IsNetfx461Installed()
        {
	        bool bRetValue = false;

            try
            {
                var oRet = Registry.GetValue(GSzNetfx45RegKeyName, GSzNetfx45RegValueName, "");
                if (oRet != null)
	            {
                    if (GDwNetfx461ReleaseVersion <= (Int32)oRet)
			            bRetValue = true;
	            }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }
	        
	        return bRetValue;
        }

        // Leo, 16.08.2018
        public bool IsNetfx462Installed()
        {
            bool bRetValue = false;

            try
            {
                var oRet = Registry.GetValue(GSzNetfx45RegKeyName, GSzNetfx45RegValueName, "");
                if (oRet != null)
                {
                    if (GDwNetfx462ServerVersion <= (Int32)oRet)
                        bRetValue = true;
                }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }

            return bRetValue;
        }

        public bool IsNetfx47Installed()
        {
            bool bRetValue = false;

            try
            {
                var oRet = Registry.GetValue(GSzNetfx45RegKeyName, GSzNetfx45RegValueName, "");
                if (oRet != null)
                {
                    if (GDwNetfx47UpdateVersion <= (Int32)oRet)
                        bRetValue = true;
                }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }

            return bRetValue;
        }

        public bool IsNetfx471Installed()
        {
            bool bRetValue = false;

            try
            {
                var oRet = Registry.GetValue(GSzNetfx45RegKeyName, GSzNetfx45RegValueName, "");
                if (oRet != null)
                {
                    if (GDwNetfx471UpdateVersion <= (Int32)oRet)
                        bRetValue = true;
                }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }

            return bRetValue;
        }

        public bool IsNetfx472Installed()
        {
            bool bRetValue = false;

            try
            {
                var oRet = Registry.GetValue(GSzNetfx45RegKeyName, GSzNetfx45RegValueName, "");
                if (oRet != null)
                {
                    if (GDwNetfx472UpdateVersion <= (Int32)oRet)
                        bRetValue = true;
                }
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }

            return bRetValue;
        }

        /******************************************************************
        Function Name:	GetNetfxSPLevel
        Description:	Determine what service pack is installed for a
        version of the .NET Framework using registry
        based detection methods documented in the
        .NET Framework deployment guides.
        Inputs:         pszNetfxRegKeyName - registry key name to use for detection
        pszNetfxRegValueName - registry value to use for detection
        Results:        integer representing SP level for .NET Framework
        ******************************************************************/
        int GetNetfxSpLevel(string pszNetfxRegKeyName, string pszNetfxRegValueName)
        {
            try
            {
                var oRet = Registry.GetValue(pszNetfxRegKeyName, pszNetfxRegValueName, "");
                if (oRet != null)
                    return (Int32)oRet;
            }
            catch (Exception ex)
            {
                _oLog.Write(ex);
            }

            /* We can only get here if the .NET Framework is not
	        installed or there was some kind of error retrieving
	        the data from the registry */

	        return -1;
        }

        /******************************************************************
        Function Name:	CheckNetfxBuildNumber
        Description:	Retrieves the .NET Framework build number from
        the registry and validates that it is not a pre-release
        version number
        Inputs:         NONE
        Results:        true if the build number in the registry is greater
        than or equal to the passed in version; false otherwise
        ******************************************************************/
        bool CheckNetfxBuildNumber( string pszNetfxRegKeyName, string pszNetfxRegKeyValue, 
                                    int iRequestedVersionMajor, int iRequestedVersionMinor, 
                                    int iRequestedVersionBuild, int iRequestedVersionRevision)
        {
	        int iRegistryVersionMajor = 0;
	        int iRegistryVersionMinor = 0;
	        int iRegistryVersionBuild = 0;
	        int iRegistryVersionRevision = 0;

            /* Attempt to retrieve the build number registry value */
	        var oRet = Registry.GetValue(pszNetfxRegKeyName, pszNetfxRegKeyValue, "");

	        if (oRet != null)
	        {
		        /* This registry value should be of the format
		        #.#.#####.##.  Try to parse the 4 parts of
		        the version here */
		        //pszToken = _tcstok_s(szRegValue, _T("."), &pszNextToken);
	            var sRet = (string) oRet;
	            string[] arVersions = sRet.Split('.');

	            if (arVersions.Length > 0)
	            {
	                iRegistryVersionMajor = Convert.ToInt32(arVersions[0]);

                    if (arVersions.Length > 1)
                        iRegistryVersionMinor = Convert.ToInt32(arVersions[1]);

                    if (arVersions.Length > 2)
                        iRegistryVersionBuild = Convert.ToInt32(arVersions[2]);

                    if (arVersions.Length > 3)
                        iRegistryVersionRevision = Convert.ToInt32(arVersions[3]);
	            }
	        }

	        /* Compare the version number retrieved from the registry with
	        the version number of the final release of the .NET Framework
	        that we are checking */
	        if (iRegistryVersionMajor > iRequestedVersionMajor)
		        return true;
	        
            if (iRegistryVersionMajor == iRequestedVersionMajor)
            {
                if (iRegistryVersionMinor > iRequestedVersionMinor)
                    return true;

                if (iRegistryVersionMinor == iRequestedVersionMinor)
                {
                    if (iRegistryVersionBuild > iRequestedVersionBuild)
                        return true;

                    if (iRegistryVersionBuild == iRequestedVersionBuild)
                    {
                        if (iRegistryVersionRevision >= iRequestedVersionRevision)
                            return true;
                    }
                }
            }

            /* If we get here, the version in the registry must be less than the
	        // version of the final release of the .NET Framework we are checking,
	        // so return false */
	        return false;
        }
    }
}
