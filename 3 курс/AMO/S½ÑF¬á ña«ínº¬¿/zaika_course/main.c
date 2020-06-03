#include "Global.h"

int main(int argc, const char * argv[]) {

    if (argc == 2)
    {
        finput = fopen(argv[1], "r");
        
        if (finput != NULL)
        {
            TEST = fopen("TEST.txt", "w");
            ProcessingText(finput);
        }
        else
        {
            printf("Open file error.\n");
            exit(1);
        }
    }
    else
    {
        exit(1);
    }

    fclose(ftemp);
    //remove("ftemp.txt");
    fclose(finput);
    fclose(TEST);

    return 0;
}
