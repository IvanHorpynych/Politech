	for (i = 0; i <= length-1; i) 
	{
		if (singlesymb(ch) == false)
			{
				g = 0;
				while (singlesymb(ch) == false) 
				{
					out_string[g] = ch;
					ch = in_string[i+1];
					g++;
					i++;
				}
				out_string[g] = 0;
				buf->lexem_str = out_string;
				buf->lexem_length = g;
				buf->TypeUser = analys_word(out_string);
				cout << buf->lexem_str << endl;
				cout << buf->lexem_length << endl;
				if (in_string[i] != '\0' || in_string[i] != ';')
				{
					buf->next = (lexem*)malloc(sizeof(lexem));
					buf = buf->next;
					buf->next = NULL;
				}
			}
		else 
		{
			if (ch == ';')
			{
				i = length;
			}
			else if (ch == '\n')
			{
				buf->lexem_str = out_string;
				buf->lexem_length = 0;
				buf->TypeUser = EOL;
				i = length;
			}
			else if (ch != ' ')
			{
				char* bufch = (char*)malloc(sizeof(char));
				bufch[0] = ch;
				bufch[1] = '\0';
				buf->lexem_str = bufch;
				buf->lexem_length = 1;
				cout << "single_symb" << endl;
				buf->TypeUser = single_symb;
				if (isalpha(in_string[i - 1]) != 0)
				{
					i++;
				}
				cout << buf->lexem_str << endl;
				cout << buf->lexem_length << endl;
				if (in_string[i] != '\0' || in_string[i+1] != '\0' || in_string[i] != ';' || in_string[i + 1] != ';')
				{
					buf->next = (lexem*)malloc(sizeof(lexem));
					buf = buf->next;
					buf->next = NULL;
				}
			}
			else{}			
		}
		ch = in_string[i];
	}
	return;
}

