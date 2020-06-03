#include "Global.h"
//проверка размера переменной.
int CheckSize(double num)
{
    int size = 0;

    if (num <= unsigned_byte_max && num >= neg_signed_byte_max)
    {
        size = 1;
    }
    else if (num <= unsigned_word_max && num >= neg_signed_word_max)
    {
        size = 2;
    }
    else if (num <= unsigned_dword_max && num >= neg_signed_dword_max)
    {
        size = 4;
    }

    return size;
}
/*
Для перевода двоичного числа в десятичное необходимо это число представить в виде суммы
произведений степеней основания двоичной системы счисления на соответствующие цифры в разрядах двоичного числа. 
разряды считаются, начиная с нулевого, которому соответствует младший бит
*/
double BinToDecimal(char *numstr)
{
    char ch;
    int length;
    double result = 0;
    int power = 0; 

    if (numstr[strlen(numstr) - 1] == 'B')
    {
        length = strlen(numstr) - 2;
    }
    else
    {
        length = strlen(numstr) - 1;
    }

    if (numstr[0] == '-')
    {
        while (length >= 0)
        {
            ch = numstr[length];
            if (ch == '1')
            {
                result = result + pow(2, power);
                ++power;
                --length;
            }
            else
            {
                ++power;
                --length;
            }
        }
        return -result;
    }
    else
    {
        while (length >= 0)
        {
            ch = numstr[length];

            if (ch == '1')
            {
                result = result + pow(2, power);
                ++power;
                --length;
            }
            else
            {
                ++power;
                --length;
            }
        }
        return result;
    }   
}

/*
Для перевода шестнадцатеричного числа в десятичное необходимо это число представить в виде суммы произведений степеней
основания шестнадцатеричной системы счисления на соответствующие цифры в разрядах шестнадцатеричного числа. 
разряды считаются, начиная с нулевого, которому соответствует младший бит.
*/

double HexToDecimal(char *numstr)
{
    char ch;
    double result = 0;
    int length = (int)strlen(numstr) - 2;
    int power = 0;

    if (numstr[0] == '-')
    {
        while (length >= 1)
        {
            ch = numstr[length];
            switch (ch)
            {
              case '0':
                result = result + (0 *  pow(16,power));
                break;
              case '1':
                result = result + (1 *  pow(16,power));
                break;

              case '2':
                result = result + (2 *  pow(16,power));
                break;

              case '3':
                result = result + (3 *  pow(16,power));
                break;

              case '4':
                result = result + (4 *  pow(16,power));
                break;

              case '5':
                result = result + (5 *  pow(16,power));
                break;    
              case '6':
                result = result + (6 *  pow(16,power));
                break;

              case '7':
                result = result + (7 *  pow(16,power));
                break;

              case '8':
                result = result + (8 *  pow(16,power));
                break;

              case '9':
                result = result + (9 *  pow(16,power));
                break;

              case 'A':
                result = result + (10 *  pow(16,power));
                break;  
              case 'B':
                result = result + (11 *  pow(16,power));
                break;

              case 'C':
                result = result + (12 *  pow(16,power));
                break;

              case 'D':
                result = result + (13 *  pow(16,power));
                break;

              case 'E':
                result = result + (14 *  pow(16,power));
                break;  

              case 'F':
                result = result + (15 *  pow(16,power));
                break;  
              default:
                break;
            }
            ++power;
            --length;
        }

        return -result;
    }
    else
    {
        while (length >= 0)
        {
            ch = numstr[length];

            switch (ch)
            {
              case '0':
                result = result + (0 *  pow(16,power));
                break;
              case '1':
                result = result + (1 *  pow(16,power));
                break;

              case '2':
                result = result + (2 *  pow(16,power));
                break;

              case '3':
                result = result + (3 *  pow(16,power));
                break;

              case '4':
                result = result + (4 *  pow(16,power));
                break;

              case '5':
                result = result + (5 *  pow(16,power));
                break;    
              case '6':
                result = result + (6 *  pow(16,power));
                break;

              case '7':
                result = result + (7 *  pow(16,power));
                break;

              case '8':
                result = result + (8 *  pow(16,power));
                break;

              case '9':
                result = result + (9 *  pow(16,power));
                break;

              case 'A':
                result = result + (10 *  pow(16,power));
                break;  
              case 'B':
                result = result + (11 *  pow(16,power));
                break;

              case 'C':
                result = result + (12 *  pow(16,power));
                break;

              case 'D':
                result = result + (13 *  pow(16,power));
                break;

              case 'E':
                result = result + (14 *  pow(16,power));
                break;  

              case 'F':
                result = result + (15 *  pow(16,power));
                break;  
              default:
                break;
            }

        ++power;
        --length;
        }

        return result;
    }
}

/*
Для перевода чисел из десятичной системы счисления в шестнадцатеричную используют тот же "алгоритм замещения", 
что и при переводе из десятичной системы счисления в двоичную и восьмеричную, только в качестве делителя используют 16,
основание шестнадцатеричной системы счисления:

Делим десятичное число А на 16. Частное Q запоминаем для следующего шага, а остаток a записываем как младший бит шестнадцатеричного числа.

Если частное q не равно 0, принимаем его за новое делимое и повторяем процедуру, описанную в шаге 1. Каждый новый остаток записывается в разряды шестнадцатеричного числа в направлении от младшего бита к старшему.

Алгоритм продолжается до тех пор, пока в результате выполнения шагов 1 и 2 не получится частное Q = 0 и остаток a меньше 16.
*/
char *DecimalToHex(char *numstr, char *result)
{
    bool neg = false;
    char temp_string[100];
    char res[100];
    int array[100];
    double decNumber;
    long int quotient;
    int modulo = 16;
    int j = 0;

    if (numstr[0] == '-')
    {
        neg = true;
    }

    if (numstr[strlen(numstr) - 1] == 'D')
    {
        if (neg == true)
        {
            temp_string[0] = '-';

            for (int i = 1; i < strlen(numstr) - 1; ++i)
            {
                temp_string[i] = numstr[i];
            }
        }
        else
        {
            for (int i = 0; i < strlen(numstr) - 1; ++i)
            {
                temp_string[i] = numstr[i];
            }
        }
    }
    else
    {
        if (neg == true)
        {
            temp_string[0] = '-';

            for (int i = 1; i < strlen(numstr); ++i)
            {
                temp_string[i] = numstr[i];
            }
        }
    }

    decNumber = atof(temp_string);
    quotient = fabs(decNumber);

    while (quotient != 0)
    {
        modulo = (long int)quotient % (int)16;
        array[j] = modulo;
        quotient = (quotient - modulo) / 16;
        ++j;
    }

    for (int i = 0; i < j; ++i)
    {
        if (array[j - 1 - i] <= 9 && array[j - 1 - i] >= 0)
        {
            res[i] = (array[j - 1 - i] + '0');
        }
        else
        {
            int ch = array[j - 1 - i];
            switch (ch) {
                case 10:
                    res[i] = 'A';
                    break;
                case 11:
                    res[i] = 'B';
                    break;
                case 12:
                    res[i] = 'C';
                    break;
                case 13:
                    res[i] = 'D';
                    break;
                case 14:
                    res[i] = 'E';
                    break;
                case 15:
                    res[i] = 'F';
                    break;
            }
        }
    }  

    if (neg == true)
    {
        int length = strlen(res);

        for (int i = length; i >= 0; --i)
        {
            res[i + 1] = res[i];
        }
        res[0] = '-';
    }

    strcpy(result, res);

    return result;
}

// Обработка ошибки недопустимого алфавита.

void UnknownCharError(char *string, size_t line_number, size_t char_number, char ch)
{
    printf("(%2zu,%2zu) %c Error. Uknown character. %s\n", line_number, char_number, ch, string);
    exit(1);
}

//  Обработка ошибки длинны идентификатора.

void IdentNameError(char *string, size_t line_number)
{
    printf("(%zu) Error. Identifier's name \"%s\" is too long! \n", line_number, string);
}

// Обработка ошибки выполнения недопустимых команд за сегментом.
void NoSegmentError(char *string, size_t line_number)
{   
    char work_string[MAX_STRING_LENGTH];
    sprintf(work_string, "\t%s\n(%zu) Error. Code or data out of segment.\n", string, line_number);
    fputs(work_string, ftemp);
}
