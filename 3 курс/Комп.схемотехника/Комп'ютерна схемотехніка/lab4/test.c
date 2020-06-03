#include <stdio.h>

int variables[][4] = {
                         { 0, 0, 0, 1 },
                         { 0, 0, 1, 0 },
                         { 0, 0, 1, 1 },
                         { 0, 1, 0, 1 },
                         { 0, 1, 1, 0 },
                         { 1, 0, 0, 0 },
                         { 1, 0, 1, 0 },
                         { 1, 0, 1, 1 },
                         { 1, 1, 0, 0 },
                         { 1, 1, 1, 0 }
                     };

int r4(int *p) { return p[2] && p[1] && p[0]; }
int s4(int *p) { return p[2] && p[1] && !p[0]; }
int r3(int *p) { return p[2] && p[1]; }
int s3(int *p) { return p[3] && p[2]; }
int r2(int *p) { return p[2] && p[1] || p[3] && p[2]; }
int s2(int *p) { return !p[2]; }
int r1(int *p) { return !p[2] || p[3] && p[0]; }
int s1(int *p) { return p[0] && p[1] && p[2] || !p[3] && p[2] && !p[1]; }

int main()
{
    int i;

    printf("Q4Q3Q2Q1|R4S4R3S3R2S2R1S1\n");
    for (i = 0; i < sizeof(variables) / sizeof(variables[0]); i++)
    {
        printf("%2d%2d%2d%2d|%2d%2d%2d%2d%2d%2d%2d%2d\n",
               variables[i][0],
               variables[i][1],
               variables[i][2],
               variables[i][3],
               r4(variables[i]),
               s4(variables[i]),
               r3(variables[i]),
               s3(variables[i]),
               r2(variables[i]),
               s2(variables[i]),
               r1(variables[i]),
               s1(variables[i]));
    }

    return 0;
}
