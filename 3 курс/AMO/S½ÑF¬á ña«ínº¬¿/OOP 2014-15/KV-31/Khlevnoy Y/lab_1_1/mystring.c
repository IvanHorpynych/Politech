/***********************************************************************
*file: mystring.c
*synopsis: The string UDF-functions use calloc
* to allocate arrays\vectors.
* These functions are declared in the include file "mystring.h".
*related files: none
*author: Khlevnoy Y.A.
*written: 20/09/2014
*last modified: 12/10/2014
************************************************************************/


#include "mystring.h"


int substr(const char *pStr, const char *pSub)
{
    int i = 0, j = 0;                           // indexes
    int sizeStr = 0, sizeSub = 0;               // strings' size
    int match = 0;                              // variable to be returned


    // finding string sizes
    for ( i = 0; pStr[i] != '\0'; i++ )
        sizeStr++;
    for ( i = 0; pSub[i] != '\0'; i++ )
        sizeSub++;

    // simplified ERROR returning
    /// TODO improve the if statement
    if ( (sizeStr == 0) || (sizeStr < sizeSub) || (sizeSub == 0) )
        return -1;

    //until comparison makes sense
    for ( i = 0; i <= (sizeStr - sizeSub); i++ )
        // first symbols matching
        if (pStr[i] == pSub[0]) {
            // compare only needed number of symbols & calculate matching symbols
            for ( j = 0; j < sizeSub; j++ )
                if (pStr[i + j] == pSub[j])
                    match++;

            if (match == sizeSub)
                break; // success
            else
                match = 0; // reset (goes to the next position)
        }

    if (match == 0)
        return -1;

    return i;
}
///>--------------------------------------------------------------------------<

int subseq(const char *pStr, const char *pStr2)
{
    int i, j, index, matches;
    int maxMatches = 0;
    int len1 = 0, len2 = 0;


    // finding string sizes
    for ( i = 0; pStr[i] != '\0'; i++ )
        len1++;
    for ( i = 0; pStr2[i] != '\0'; i++ )
        len2++;

    if (len1 == 0 || len2 == 0)
        return 0;

    if (len1 >= len2) {
        for( i = 0; i < len1; i++ ) {
            index = 0;
            matches = 0;
            for ( j = 0; j < len2; j++ )
                if (pStr[i + index] == pStr2[j]) {
                    matches++;
                    index++;
                }
                else
                    index = 0;
            if(matches > maxMatches)
                maxMatches = matches;
        }
    }
    else {
        for( i = 0; i < len2; i++ ) {
            index = 0;
            matches = 0;
            for(j = 0; j < len1; j++)
                if(pStr[i + index] == pStr2[j]) {
                    matches++;
                    index++;
                }
                else
                    index = 0;
            if(matches > maxMatches)
                maxMatches = matches;
        }
    }

    return maxMatches;
}
///>--------------------------------------------------------------------------<

int ispal(const char *pStr)
{
    int i = 0;                                  // indexes
    int sizeStr = 0;                            // string's size


    // finding string sizes
    for ( i = 0; pStr[i] != '\0'; i++ )
        sizeStr++;
    // zero length string is neither palindrome nor notPalindrome
    if (sizeStr == 0)
        return 0;

    // even size
    if (sizeStr % 2 == 0) {
        i = sizeStr / 2;
        while ( i < sizeStr) {
            if( pStr[i] != pStr[sizeStr - i - 1])
                return 0; // STRING is not palindrome
            i++;
        }
    }
    // odd size
    else{
        i = (sizeStr + 1) / 2;
        while ( i < sizeStr) {
            if( pStr[i] != pStr[sizeStr - i - 1])
                return 0; // STRING is not palindrome
            i++;
        }
    }

    return 1;
}
///>--------------------------------------------------------------------------<

char* makepal(const char *pStr)
{
    int sizeStr = 0, offset = 0;
    int i;
    char *pal;


    // finding size of the string
    for ( i = 0; pStr[i] != '\0'; i++ )
        sizeStr++;
    // allocating memory or returning NULL depending on string's size
    if (sizeStr > 0) {
        pal = (char*)calloc(2*sizeStr, sizeof(char));
        for( i = 0; i < sizeStr; i++ )
            pal[i] = pStr[i];
    }
    else
        return NULL;

    // copy original string without NULL-terminated symbol '\0';
    for ( i = 0; i < sizeStr; i++ )
        pal[i] = pStr[i];

    while(ispal(pal) != 1){
        // copy new symbol in reversal order from original string
        for ( i = offset ; i >= 0 ; i-- )
             pal[sizeStr-i] = pStr[i];

        pal[sizeStr + 1] = '\0';
        offset++;
        sizeStr++; // after adding a new symbol
    }

    return pal;
}
///>--------------------------------------------------------------------------<

double* txt2double(const char *pStr, int *size)
{
    char *sNumber;
    double *v = NULL;
    int sizeStr = 0, numberLength = 0, vectorIndex = 0;
    int i, k;


    // finding size of the string
    for ( i = 0; pStr[i] != '\0'; i++ )
        sizeStr++;
    if (sizeStr == 0){
        *size = 0;
        return v;
    }

    // verifying is there is no unallowed symbol in the string
    for ( i = 0; i < sizeStr; i++ )
        if( (pStr[i] < '0' || pStr[i] > '9') && (pStr[i] != ';') && (pStr[i] != '.') ) {
            *size = 0;
            return v;
        }

    // finding number of ';' and '\0' occurrences and allocating memory
    for ( i = 0, k = 0; i <= sizeStr; i++ )
        if( (pStr[i] == ';') || (pStr[i] == '\0') )
            k++;
    *size = k;
    v = (double*)malloc(*size * sizeof(double));

    // processing string
    for ( i = 0; i <= sizeStr; i++ )
        if( (pStr[i] == ';') || (pStr[i] == '\0') )
        {
            sNumber = (char*)calloc(numberLength+1, sizeof(char));
            k = 0;
            for ( k = 0 ; k < numberLength; k++ )
                sNumber[k] = pStr[i - numberLength + k];
            v[vectorIndex] = atof(sNumber);
            vectorIndex++;
            free(sNumber);
            sNumber = NULL;
            numberLength = 0;
        }
        else numberLength++;

    return v;
}
///>--------------------------------------------------------------------------<
