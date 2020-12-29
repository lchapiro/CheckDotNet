#include <stdio.h>
#include <windows.h>
#include <tchar.h>
#include <strsafe.h>

#include <Shellapi.h>

#pragma comment(lib, "AdvAPI32")
#pragma comment(lib, "User32")
#pragma comment(lib, "Shell32")

#ifndef SM_TABLETPC
#define SM_TABLETPC     86
#endif

#ifndef SM_MEDIACENTER
#define SM_MEDIACENTER  87
#endif

/* Leo, 20.05.2016 Needs Advapi32.dll to be linked! */

const TCHAR *g_szNetfx10RegKeyName = _T("Software\\Microsoft\\.NETFramework\\Policy\\v1.0");
const TCHAR *g_szNetfx10RegKeyValue = _T("3705");
const TCHAR *g_szNetfx10SPxMSIRegKeyName = _T("Software\\Microsoft\\Active Setup\\Installed Components\\{78705f0d-e8db-4b2d-8193-982bdda15ecd}");
const TCHAR *g_szNetfx10SPxOCMRegKeyName = _T("Software\\Microsoft\\Active Setup\\Installed Components\\{FDC11A6F-17D1-48f9-9EA3-9051954BAA24}");
const TCHAR *g_szNetfx11RegKeyName = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v1.1.4322");
const TCHAR *g_szNetfx20RegKeyName = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v2.0.50727");
const TCHAR *g_szNetfx30RegKeyName = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0\\Setup");
const TCHAR *g_szNetfx30SpRegKeyName = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0");
const TCHAR *g_szNetfx30RegValueName = _T("InstallSuccess");
const TCHAR *g_szNetfx35RegKeyName = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v3.5");
const TCHAR *g_szNetfx40ClientRegKeyName = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Client");
const TCHAR *g_szNetfx40FullRegKeyName = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full");
const TCHAR *g_szNetfx40SPxRegValueName = _T("Servicing");
const TCHAR *g_szNetfx45RegKeyName = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full");
const TCHAR *g_szNetfx45RegValueName = _T("Release");
const TCHAR *g_szNetfxStandardRegValueName = _T("Install");
const TCHAR *g_szNetfxStandardSPxRegValueName = _T("SP");
const TCHAR *g_szNetfxStandardVersionRegValueName = _T("Version");

/* Version information for final release of .NET Framework 3.0 */
const int g_iNetfx30VersionMajor = 3;
const int g_iNetfx30VersionMinor = 0;
const int g_iNetfx30VersionBuild = 4506;
const int g_iNetfx30VersionRevision = 26;

/* Version information for final release of .NET Framework 3.5 */
const int g_iNetfx35VersionMajor = 3;
const int g_iNetfx35VersionMinor = 5;
const int g_iNetfx35VersionBuild = 21022;
const int g_iNetfx35VersionRevision = 8;

/* Version information for final release of .NET Framework 4 */
const int g_iNetfx40VersionMajor = 4;
const int g_iNetfx40VersionMinor = 0;
const int g_iNetfx40VersionBuild = 30319;
const int g_iNetfx40VersionRevision = 0;

/* Version information for final release of .NET Framework 4.5 */
const int g_dwNetfx45ReleaseVersion = 378389;

/* Version information for final release of .NET Framework 4.5.1 */
const int g_dwNetfx451ReleaseVersion = 378675;

/* Version information for final release of .NET Framework 4.5.2 */
const int g_dwNetfx452ReleaseVersion = 379893;

/* Version information for final release of .NET Framework 4.6 */
const int g_dwNetfx46ReleaseVersion = 393295;

/* Version information for final release of .NET Framework 4.6.1 */
const int g_dwNetfx461ReleaseVersion = 394254;

// Version information for final release of .NET Framework 4.6.2
const int g_dwNetfx462ReleaseVersion = 394802;

// Version information for final release of .NET Framework 4.7
const int g_dwNetfx47ReleaseVersion = 460798;

// Version information for final release of .NET Framework 4.7.1
const int g_dwNetfx471ReleaseVersion = 461308;

// Version information for final release of .NET Framework 4.7.2
const int g_dwNetfx472ReleaseVersion = 461808;

// Constants for known .NET Framework versions used with the GetRequestedRuntimeInfo API
const TCHAR *g_szNetfx10VersionString = _T("v1.0.3705");
const TCHAR *g_szNetfx11VersionString = _T("v1.1.4322");
const TCHAR *g_szNetfx20VersionString = _T("v2.0.50727");
const TCHAR *g_szNetfx40VersionString = _T("v4.0.30319");

// Function prototypes
BOOL CheckNetfxBuildNumber(const TCHAR*, const TCHAR*, const int, const int, const int, const int);
BOOL CheckNetfxVersionUsingMscoree(const TCHAR*);
int GetNetfx10SPLevel();
int GetNetfxSPLevel(const TCHAR*, const TCHAR*);
DWORD GetProcessorArchitectureFlag();
BOOL IsCurrentOSTabletMedCenter();
BOOL IsNetfx10Installed();
BOOL IsNetfx11Installed();
BOOL IsNetfx20Installed();
BOOL IsNetfx30Installed();
BOOL IsNetfx35Installed();
BOOL IsNetfx40ClientInstalled();
BOOL IsNetfx40FullInstalled();
BOOL IsNetfx45Installed();
BOOL IsNetfx451Installed();
BOOL IsNetfx452Installed();
BOOL IsNetfx46Installed();
BOOL IsNetfx461Installed();
BOOL IsNetfx462Installed();
BOOL IsNetfx47Installed();
BOOL IsNetfx471Installed();
BOOL IsNetfx472Installed();
BOOL RegistryGetValue(HKEY, const TCHAR*, const TCHAR*, DWORD, LPBYTE, DWORD);

/******************************************************************
Function Name:	IsNetfx10Installed
Description:	Uses the detection method recommended at
http://msdn.microsoft.com/library/ms994349.aspx
to determine whether the .NET Framework 1.0 is
installed on the machine
Inputs:	        NONE
Results:        TRUE if the .NET Framework 1.0 is installed
FALSE otherwise
******************************************************************/
BOOL IsNetfx10Installed()
{
	TCHAR szRegValue[MAX_PATH];
	return (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx10RegKeyName, g_szNetfx10RegKeyValue, NULL, (LPBYTE)szRegValue, MAX_PATH));
}


/******************************************************************
Function Name:	IsNetfx11Installed
Description:	Uses the detection method recommended at
http://msdn.microsoft.com/library/ms994339.aspx
to determine whether the .NET Framework 1.1 is
installed on the machine
Inputs:	        NONE
Results:        TRUE if the .NET Framework 1.1 is installed
FALSE otherwise
******************************************************************/
BOOL IsNetfx11Installed()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx11RegKeyName, g_szNetfxStandardRegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (1 == dwRegValue)
			bRetValue = TRUE;
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
Results:        TRUE if the .NET Framework 2.0 is installed
FALSE otherwise
******************************************************************/
BOOL IsNetfx20Installed()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx20RegKeyName, g_szNetfxStandardRegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (1 == dwRegValue)
			bRetValue = TRUE;
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
Results:        TRUE if the .NET Framework 3.0 is installed
FALSE otherwise
******************************************************************/
BOOL IsNetfx30Installed()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	/* Check that the InstallSuccess registry value exists and equals 1 */
	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx30RegKeyName, g_szNetfx30RegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (1 == dwRegValue)
			bRetValue = TRUE;
	}

	/* A system with a pre-release version of the .NET Framework 3.0 can
	have the InstallSuccess value.  As an added verification, check the
	version number listed in the registry */
	return (bRetValue && CheckNetfxBuildNumber(g_szNetfx30RegKeyName, g_szNetfxStandardVersionRegValueName, g_iNetfx30VersionMajor, g_iNetfx30VersionMinor, g_iNetfx30VersionBuild, g_iNetfx30VersionRevision));
}


/******************************************************************
Function Name:	IsNetfx35Installed
Description:	Uses the detection method recommended at
http://msdn.microsoft.com/library/cc160716.aspx
to determine whether the .NET Framework 3.5 is
installed on the machine
Inputs:	        NONE
Results:        TRUE if the .NET Framework 3.5 is installed
FALSE otherwise
******************************************************************/
BOOL IsNetfx35Installed()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	/* Check that the Install registry value exists and equals 1 */
	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx35RegKeyName, g_szNetfxStandardRegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (1 == dwRegValue)
			bRetValue = TRUE;
	}

	/* A system with a pre-release version of the .NET Framework 3.5 can
	have the Install value.  As an added verification, check the
	version number listed in the registry */
	return (bRetValue && CheckNetfxBuildNumber(g_szNetfx35RegKeyName, g_szNetfxStandardVersionRegValueName, g_iNetfx35VersionMajor, g_iNetfx35VersionMinor, g_iNetfx35VersionBuild, g_iNetfx35VersionRevision));
}


/******************************************************************
Function Name:	IsNetfx40ClientInstalled
Description:	Uses the detection method recommended at
http://msdn.microsoft.com/library/ee942965(v=VS.100).aspx
to determine whether the .NET Framework 4 Client is
installed on the machine
Inputs:         NONE
Results:        TRUE if the .NET Framework 4 Client is installed
FALSE otherwise
******************************************************************/
BOOL IsNetfx40ClientInstalled()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx40ClientRegKeyName, g_szNetfxStandardRegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (1 == dwRegValue)
			bRetValue = TRUE;
	}

	/* A system with a pre-release version of the .NET Framework 4 can
	have the Install value.  As an added verification, check the
	version number listed in the registry */
	return (bRetValue && CheckNetfxBuildNumber(g_szNetfx40ClientRegKeyName, g_szNetfxStandardVersionRegValueName, g_iNetfx40VersionMajor, g_iNetfx40VersionMinor, g_iNetfx40VersionBuild, g_iNetfx40VersionRevision));
}


/******************************************************************
Function Name:	IsNetfx40FullInstalled
Description:	Uses the detection method recommended at
http://msdn.microsoft.com/library/ee942965(v=VS.100).aspx
to determine whether the .NET Framework 4 Full is
installed on the machine
Inputs:         NONE
Results:        TRUE if the .NET Framework 4 Full is installed
FALSE otherwise
******************************************************************/
BOOL IsNetfx40FullInstalled()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx40FullRegKeyName, g_szNetfxStandardRegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (1 == dwRegValue)
			bRetValue = TRUE;
	}

	/* A system with a pre-release version of the .NET Framework 4 can
	have the Install value.  As an added verification, check the
	version number listed in the registry */
	return (bRetValue && CheckNetfxBuildNumber(g_szNetfx40FullRegKeyName, g_szNetfxStandardVersionRegValueName, g_iNetfx40VersionMajor, g_iNetfx40VersionMinor, g_iNetfx40VersionBuild, g_iNetfx40VersionRevision));
}


/******************************************************************
Function Name:	IsNetfx45Installed
Description:	Uses the detection method recommended at
http://msdn.microsoft.com/en-us/library/ee942965(v=vs.110).aspx
to determine whether the .NET Framework 4.5 is
installed on the machine
Inputs:         NONE
Results:        TRUE if the .NET Framework 4.5 is installed
FALSE otherwise
******************************************************************/
BOOL IsNetfx45Installed()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx45RegKeyName, g_szNetfx45RegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (g_dwNetfx45ReleaseVersion <= dwRegValue)
			bRetValue = TRUE;
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
Results:        TRUE if the .NET Framework 4.5.1 is installed
FALSE otherwise
******************************************************************/
BOOL IsNetfx451Installed()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx45RegKeyName, g_szNetfx45RegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (g_dwNetfx451ReleaseVersion <= dwRegValue)
			bRetValue = TRUE;
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
Results:        TRUE if the .NET Framework 4.5.2 is installed
FALSE otherwise
******************************************************************/
BOOL IsNetfx452Installed()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx45RegKeyName, g_szNetfx45RegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (g_dwNetfx452ReleaseVersion <= dwRegValue)
			bRetValue = TRUE;
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
Results:        TRUE if the .NET Framework 4.6 is installed
FALSE otherwise
******************************************************************/
BOOL IsNetfx46Installed()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx45RegKeyName, g_szNetfx45RegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (g_dwNetfx46ReleaseVersion <= dwRegValue)
			bRetValue = TRUE;
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
Results:        TRUE if the .NET Framework 4.6.1 is installed
FALSE otherwise
******************************************************************/
BOOL IsNetfx461Installed()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx45RegKeyName, g_szNetfx45RegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (g_dwNetfx461ReleaseVersion <= dwRegValue)
			bRetValue = TRUE;
	}

	return bRetValue;
}

/******************************************************************
Function Name:	IsNetfx462Installed
Description:	Uses the detection method recommended at
				http://msdn.microsoft.com/en-us/library/ee942965(v=vs.110).aspx
				to determine whether the .NET Framework 4.6.2 is
				installed on the machine
Inputs:         NONE
Results:        true if the .NET Framework 4.6.2 is installed
				false otherwise
******************************************************************/
BOOL IsNetfx462Installed()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx45RegKeyName, g_szNetfx45RegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (g_dwNetfx462ReleaseVersion <= dwRegValue)
			bRetValue = TRUE;
	}

	return bRetValue;
}


/******************************************************************
Function Name:	IsNetfx47Installed
Description:	Uses the detection method recommended at
				https://msdn.microsoft.com/en-us/library/ee942965(v=vs.110).aspx
				to determine whether the .NET Framework 4.7 is
				installed on the machine
Inputs:         NONE
Results:        true if the .NET Framework 4.7 is installed
				false otherwise
******************************************************************/
BOOL IsNetfx47Installed()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx45RegKeyName, g_szNetfx45RegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (g_dwNetfx47ReleaseVersion <= dwRegValue)
			bRetValue = TRUE;
	}

	return bRetValue;
}


/******************************************************************
Function Name:	IsNetfx471Installed
Description:	Uses the detection method recommended at
				https://msdn.microsoft.com/en-us/library/ee942965(v=vs.110).aspx
				to determine whether the .NET Framework 4.7.1 is
				installed on the machine
Inputs:         NONE
Results:        true if the .NET Framework 4.7.1 is installed
				false otherwise
******************************************************************/
BOOL IsNetfx471Installed()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx45RegKeyName, g_szNetfx45RegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (g_dwNetfx471ReleaseVersion <= dwRegValue)
			bRetValue = TRUE;
	}

	return bRetValue;
}


/******************************************************************
Function Name:	IsNetfx472Installed
Description:	Uses the detection method recommended at
				https://msdn.microsoft.com/en-us/library/ee942965(v=vs.110).aspx
				to determine whether the .NET Framework 4.7.2 is
				installed on the machine
Inputs:         NONE
Results:        true if the .NET Framework 4.7.2 is installed
				false otherwise
******************************************************************/
BOOL IsNetfx472Installed()
{
	BOOL bRetValue = FALSE;
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx45RegKeyName, g_szNetfx45RegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (g_dwNetfx472ReleaseVersion <= dwRegValue)
			bRetValue = TRUE;
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
int GetNetfxSPLevel(const TCHAR *pszNetfxRegKeyName, const TCHAR *pszNetfxRegValueName)
{
	DWORD dwRegValue = 0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, pszNetfxRegKeyName, pszNetfxRegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		return (int)dwRegValue;
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
Results:        TRUE if the build number in the registry is greater
than or equal to the passed in version; FALSE otherwise
******************************************************************/
BOOL CheckNetfxBuildNumber(const TCHAR *pszNetfxRegKeyName, const TCHAR *pszNetfxRegKeyValue, const int iRequestedVersionMajor, const int iRequestedVersionMinor, const int iRequestedVersionBuild, const int iRequestedVersionRevision)
{
	TCHAR szRegValue[MAX_PATH];
	TCHAR *pszToken = NULL;
	TCHAR *pszNextToken = NULL;
	int iVersionPartCounter = 0;
	int iRegistryVersionMajor = 0;
	int iRegistryVersionMinor = 0;
	int iRegistryVersionBuild = 0;
	int iRegistryVersionRevision = 0;
	BOOL bRegistryRetVal = FALSE;

	/* Attempt to retrieve the build number registry value */
	bRegistryRetVal = RegistryGetValue(HKEY_LOCAL_MACHINE, pszNetfxRegKeyName, pszNetfxRegKeyValue, NULL, (LPBYTE)szRegValue, MAX_PATH);

	if (bRegistryRetVal)
	{
		/* This registry value should be of the format
		#.#.#####.##.  Try to parse the 4 parts of
		the version here */
		pszToken = _tcstok_s(szRegValue, _T("."), &pszNextToken);
		while (NULL != pszToken)
		{
			iVersionPartCounter++;

			switch (iVersionPartCounter)
			{
			case 1:
				/* Convert the major version value to an integer */
				iRegistryVersionMajor = _tstoi(pszToken);
				break;
			case 2:
				/* Convert the minor version value to an integer */
				iRegistryVersionMinor = _tstoi(pszToken);
				break;
			case 3:
				/* Convert the build number value to an integer */
				iRegistryVersionBuild = _tstoi(pszToken);
				break;
			case 4:
				/* Convert the revision number value to an integer */
				iRegistryVersionRevision = _tstoi(pszToken);
				break;
			default:
				break;

			}

			/* Get the next part of the version number */
			pszToken = _tcstok_s(NULL, _T("."), &pszNextToken);
		}
	}

	/* Compare the version number retrieved from the registry with
	the version number of the final release of the .NET Framework
	that we are checking */
	if (iRegistryVersionMajor > iRequestedVersionMajor)
	{
		return TRUE;
	}
	else if (iRegistryVersionMajor == iRequestedVersionMajor)
	{
		if (iRegistryVersionMinor > iRequestedVersionMinor)
		{
			return TRUE;
		}
		else if (iRegistryVersionMinor == iRequestedVersionMinor)
		{
			if (iRegistryVersionBuild > iRequestedVersionBuild)
			{
				return TRUE;
			}
			else if (iRegistryVersionBuild == iRequestedVersionBuild)
			{
				if (iRegistryVersionRevision >= iRequestedVersionRevision)
				{
					return TRUE;
				}
			}
		}
	}

	/* If we get here, the version in the registry must be less than the
	// version of the final release of the .NET Framework we are checking,
	// so return FALSE */
	return FALSE;
}

/*
BOOL IsCurrentOSTabletMedCenter()
{
	return ((GetSystemMetrics(SM_TABLETPC) != 0) || (GetSystemMetrics(SM_MEDIACENTER) != 0));
}
*/

/******************************************************************
Function Name:	RegistryGetValue
Description:	Get the value of a reg key
Inputs:			HKEY hk - The hk of the key to retrieve
TCHAR *pszKey - Name of the key to retrieve
TCHAR *pszValue - The value that will be retrieved
DWORD dwType - The type of the value that will be retrieved
LPBYTE data - A buffer to save the retrieved data
DWORD dwSize - The size of the data retrieved
Results:		TRUE if successful, FALSE otherwise
******************************************************************/
BOOL RegistryGetValue(HKEY hk, const TCHAR * pszKey, const TCHAR * pszValue, DWORD dwType, LPBYTE data, DWORD dwSize)
{
	HKEY hkOpened;

	/* Try to open the key */
	if (RegOpenKeyEx(hk, pszKey, 0, KEY_READ, &hkOpened) != ERROR_SUCCESS)
	{
		return FALSE;
	}

	/* If the key was opened, try to retrieve the value */
	if (RegQueryValueEx(hkOpened, pszValue, 0, &dwType, (LPBYTE)data, &dwSize) != ERROR_SUCCESS)
	{
		RegCloseKey(hkOpened);
		return FALSE;
	}

	RegCloseKey(hkOpened);

	return TRUE;
}

int main()
{
	int iNetfx10SPLevel = -1;
	int iNetfx11SPLevel = -1;
	int iNetfx20SPLevel = -1;
	int iNetfx30SPLevel = -1;
	int iNetfx35SPLevel = -1;
	int iNetfx40ClientSPLevel = -1;
	int iNetfx40FullSPLevel = -1;
	int iNetfx45SPLevel = -1;
	int iNetfx451SPLevel = -1;
	int iNetfx452SPLevel = -1;
	int iNetfx46SPLevel = -1;
	int iNetfx461SPLevel = -1;
	int iNetfx462SPLevel = -1;
	int iNetfx47SPLevel = -1;
	int iNetfx471SPLevel = -1;
	int iNetfx472SPLevel = -1;
	TCHAR szMessage[MAX_PATH];
	TCHAR szOutputString[MAX_PATH * 20];

	/* Determine whether or not the .NET Framework
	// 1.0, 1.1, 2.0, 3.0, 3.5, 4, 4.5, 4.5.1, 4.5.2, 4.6, or 4.6.1 are installed */
	BOOL bNetfx10Installed = IsNetfx10Installed();
	BOOL bNetfx11Installed = IsNetfx11Installed();
	BOOL bNetfx20Installed = IsNetfx20Installed();

	/* The .NET Framework 3.0 is an add-in that installs
	// on top of the .NET Framework 2.0.  For this version
	// check, validate that both 2.0 and 3.0 are installed. */
	BOOL bNetfx30Installed = (IsNetfx20Installed() && IsNetfx30Installed());

	/* The .NET Framework 3.5 is an add-in that installs
	// on top of the .NET Framework 2.0 and 3.0.  For this version
	// check, validate that 2.0, 3.0 and 3.5 are installed. */
	BOOL bNetfx35Installed = (IsNetfx20Installed() && IsNetfx30Installed() && IsNetfx35Installed());

	BOOL bNetfx40ClientInstalled = (IsNetfx40ClientInstalled());
	BOOL bNetfx40FullInstalled = (IsNetfx40FullInstalled());

	BOOL bNetfx45Installed = (IsNetfx45Installed());
	BOOL bNetfx451Installed = (IsNetfx451Installed());
	BOOL bNetfx452Installed = (IsNetfx452Installed());
	BOOL bNetfx46Installed = (IsNetfx46Installed());
	BOOL bNetfx461Installed = (IsNetfx461Installed());

	// Leo, 16.08.2018
	BOOL bNetfx462Installed = (IsNetfx462Installed());
	BOOL bNetfx47Installed = (IsNetfx47Installed());
	BOOL bNetfx471Installed = (IsNetfx471Installed());
	BOOL bNetfx472Installed = (IsNetfx472Installed());

	BOOL bDotNet = bNetfx10Installed || bNetfx11Installed || bNetfx20Installed ||
		bNetfx30Installed || bNetfx35Installed || bNetfx451Installed ||
		bNetfx452Installed || bNetfx46Installed || bNetfx461Installed ||
		bNetfx462Installed || bNetfx47Installed || bNetfx471Installed ||
		bNetfx472Installed;

	if (bDotNet)
	{
		ShellExecute(HWND_DESKTOP,
			"open",
			"CheckDotNet.exe",
			NULL,
			NULL,
			SW_SHOW);
	}
	else
		MessageBox(0, _T("On your system has not been found any .NET framework!\n\r\n\rPlease consider to install the latest .NET Framework: https://www.microsoft.com/de-de/download/details.aspx?id=30653"),
			_T(".NET Framework NOT found"), MB_ICONSTOP);

	return (int)bDotNet;
}


