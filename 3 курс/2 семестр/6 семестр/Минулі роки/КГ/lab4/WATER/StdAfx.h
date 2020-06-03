// stdafx.h : include file for standard system include files,
//  or project specific include files that are used frequently, but
//      are changed infrequently
//

#if !defined(AFX_STDAFX_H__0E74614F_76A6_42E0_9672_D9AF172D5989__INCLUDED_)
#define AFX_STDAFX_H__0E74614F_76A6_42E0_9672_D9AF172D5989__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#define WIN32_LEAN_AND_MEAN		// Exclude rarely-used stuff from Windows headers

#include <windows.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <math.h>

#include "../IJL/ijl.h"
#include "../GLEW/glew.h"
#include "../GLUT/glut.h"

#pragma comment( lib, "../IJL/ijl15.lib" )
#pragma comment( lib, "../GLEW/glew32.lib" )
#pragma comment( lib, "../GLUT/glut32.lib" )


typedef unsigned __int32 uint32;
typedef unsigned __int16 uint16;

typedef signed __int32 sint32;
typedef signed __int16 sint16;


float inline rnd()
{
	return ((float)(rand()%RAND_MAX))/(float)(RAND_MAX-1);
}


float inline rnd(float mi,float ma)
{
	return rnd()*(ma-mi)+mi;
}

// TODO: reference additional headers your program requires here

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_STDAFX_H__0E74614F_76A6_42E0_9672_D9AF172D5989__INCLUDED_)
