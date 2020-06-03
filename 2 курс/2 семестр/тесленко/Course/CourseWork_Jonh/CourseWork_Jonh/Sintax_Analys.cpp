#include "Analys_header.h"

int* sentence_struct(lexem* Parts_sent, int flag)
{
	int count = 0, count_lex = 0, j;;
	if (flag == 0)
	{
		j = 6;
	}
	else
	{
		 j = 2 + (2 * flag - (flag - 1)) * 2;
	}
	int* argz = new int[j];
	char* bufch = new char;
	for (int i = 0; i <= j; i++)
	{
		argz[i] = 0;
	}

	if (Parts_sent->TypeUser == eof)
	{
	}
	else
	{
		if (Parts_sent->TypeUser == user_identifier)
		{
			argz[0] = 1; count = 1;
			Parts_sent = Parts_sent->next;

			if (strchr(Parts_sent->lexem_str, ':') != NULL)
			{
				Parts_sent = Parts_sent->next;
				if (Parts_sent != NULL)
				if (Parts_sent->TypeUser == directive || Parts_sent->TypeUser == dir_type || Parts_sent->TypeUser == machine_command)
				{
					argz[1] = 3; count = 3; argz[2] = 1;
					Parts_sent = Parts_sent->next;
				}
			}
			if (Parts_sent != NULL)
			if (Parts_sent->TypeUser == directive || Parts_sent->TypeUser == dir_type || Parts_sent->TypeUser == machine_command || strchr(Parts_sent->lexem_str, '=') != NULL)
			{
				argz[1] = 2; argz[2] = 1; count = 2;
				Parts_sent = Parts_sent->next;
			}
		}
		else
		{
			if (Parts_sent->TypeUser == directive || Parts_sent->TypeUser == dir_type || Parts_sent->TypeUser == machine_command)
			{
				argz[0] = 0; argz[1] = 1; argz[2] = 1; count = 1;
				Parts_sent = Parts_sent->next;
			}
		}
			if (Parts_sent != NULL)
			{
				argz[3] = count + 1;
				count_lex = 0;
				while (Parts_sent != NULL && strchr(Parts_sent->lexem_str, ',') == NULL)
				{
					count_lex++; count++;
					Parts_sent = Parts_sent->next;
				}
				argz[4] = count_lex;
				if (Parts_sent != NULL)
				{
					if (strchr(Parts_sent->lexem_str, ',') != NULL)
					{
						argz[5] = count + 2; count_lex = 0;
						Parts_sent = Parts_sent->next;
						if (flag = 2)
						{
							while (Parts_sent != NULL && strchr(Parts_sent->lexem_str, ',') == NULL)
							{
								count_lex++; count++;
								Parts_sent = Parts_sent->next;
							}
							argz[6] = count_lex;
							if (Parts_sent != NULL)
							{
								if (strchr(Parts_sent->lexem_str, ',') != NULL)
								{
									argz[7] = count + 3; count_lex = 0;
									Parts_sent = Parts_sent->next;

									while (Parts_sent != NULL)
									{
										count_lex++; count++;
										Parts_sent = Parts_sent->next;
									}
									argz[8] = count_lex;
								}
							}
						}
						while (Parts_sent != NULL)
						{
							count_lex++; count++;
							Parts_sent = Parts_sent->next;
						}
						argz[6] = count_lex;
					}
				}
			}
	}
	
	return argz;
}