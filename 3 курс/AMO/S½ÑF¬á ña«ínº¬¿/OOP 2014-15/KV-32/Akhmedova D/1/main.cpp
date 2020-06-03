#include  "Mystring.h"

int main()


{	int f2,i;

	    printf("Podstroka:\n");
        const char *str1 = "substrstring";
        const char *str2 = "string";
		printf("Poisk \"%s\" v \"%s\"\n", str1, str2);
		printf("Index %d""\n",substr(str1, str2));
      getchar();
 
	    printf("Max:\n");
        const char *str3 = "student";
        const char *str4 = "docent";
		printf("Poisk max dlinny v \"%s\" i \"%s\"\n", str3, str4);
		f2=subseq(str3, str4);
		printf("%d""\n",f2);
		 getchar();
		

        printf("ispal:\n");
        const char *str5 = "lol";
        const char *str6 = "look";
        printf("Result for \"%s\": %i\n", str5, ispal(str5));
		printf("Result for \"%s\": %i\n", str6, ispal(str6));
	  getchar();


	   printf("makepal:\n");
         str1 = "korom";
        printf("Result for \"%s\": %s\n\n", str1, makepal(str1));
	 getchar();


		 printf("txt2double:\n");
        const char *func5str = "12;145;Sb;0.254";
        int func5size;
        printf("\"%s\"\n", func5str);
        double *func5res = txt2double(func5str, &func5size);
        for (int i = 0; i < func5size; ++i)
            printf("%i: %f\n", i, func5res[i]);
		getchar();

	return 0;
}