#include "Analys_header.h"


void create_lex_analys()
{
	string buf_line, buf_pre, buf_post;
	lexem* buf_struct;
	ifstream file_test;
	ofstream file_lexem;
	int count_line = 1, count_lex;

	file_test.open("D:\\Documents\\2 ����\\2 �������\\��\\Course\\CourseWork_Jonh\\test.asm");
	file_lexem.open("D:\\Documents\\2 ����\\2 �������\\��\\Course\\CourseWork_Jonh\\Lexical_Analys.txt");

	if (file_test.is_open())
	{
		while (!file_test.eof())
		{
			getline(file_test, buf_line);
			int a = buf_line.size();
			if (int pos = buf_line.find("equ") != -1)
			{
				buf_struct = del_sentence(string_correction(buf_line, a));
				buf_pre = buf_struct->lexem_str;
				buf_struct = buf_struct->next;
				if (strcmp(buf_struct->lexem_str, "EQU") == 0)
				{
					buf_struct = buf_struct->next;
					while (buf_struct != NULL)
					{
						buf_post = buf_post + buf_struct->lexem_str;
						buf_struct = buf_struct->next;
					}
				}
			}
			int pos1 = buf_line.find(buf_pre);
			int pos2 = buf_line.find("equ");

			if (pos1 != -1 && pos2 == -1)
			{
				buf_line.replace(pos1, buf_pre.size(), buf_post);
			}
			buf_struct = del_sentence(string_correction(buf_line, a));
			file_lexem << "����� �" << count_line << endl;
			file_lexem << "---------------------------------------------------------------------------------------" << endl;
			file_lexem << "|  �  |       �������       |   �������   |                ��� �������                |" << endl;
			file_lexem << "---------------------------------------------------------------------------------------" << endl;
			count_lex = 1;
			while (buf_struct != NULL)
			{
				
				if (buf_struct->TypeUser == eof)
				{
					file_lexem << "�����Ͳ� �����" << endl;
				}
				else
				{
					file_lexem << '|' << setw(3) << count_lex << setw(3) << '|';
					file_lexem << setw(21) << buf_struct->lexem_str << '|';
					file_lexem << setw(7) << buf_struct->lexem_length << setw(7) << '|';
					if (buf_struct->TypeUser == user_identifier)
					{
						file_lexem << setw(43) << "I������������ ����������� ��� �������������" << '|' << endl;
					}
					else
						if (buf_struct->TypeUser == reg_32)
						{
							file_lexem << setw(43) << "I������������ 32-������ �������" << '|' << endl;
						}
						else
							if (buf_struct->TypeUser == reg_16)
							{
								file_lexem << setw(43) << "I������������ 16-������ �������" << '|' << endl;
							}
							else
								if (buf_struct->TypeUser == reg_8)
								{
									file_lexem << setw(43) << "I������������ 8-������ �������" << '|' << endl;
								}
								else
									if (buf_struct->TypeUser == machine_command)
									{
										file_lexem << setw(43) << "������������� ��������� ������� ����������" << '|' << endl;
									}
									else
										if (buf_struct->TypeUser == segment_reg)
										{
											file_lexem << setw(43) << "������������� ����������� �������" << '|' << endl;
										}
										else
											if (buf_struct->TypeUser == single_symb)
											{
												file_lexem << setw(43) << "�������������" << '|' << endl;
											}
											else
												if (buf_struct->TypeUser == directive)
												{
													file_lexem << setw(43) << "������������� ���������" << '|' << endl;
												}
												else
													if (buf_struct->TypeUser == dir_type)
													{
														file_lexem << setw(43) << "������������� ��������� ������ ��� 1" << '|' << endl;
													}
													else
														if (buf_struct->TypeUser == type_4)
														{
															file_lexem << setw(43) << "������������� ��� 4" << '|' << endl;
														}
														else
															if (buf_struct->TypeUser == defin_type)
															{
																file_lexem << setw(43) << "������������� ��������� ���������� ����" << '|' << endl;
															}
															else
																if (buf_struct->TypeUser == const_16)
																{
																	file_lexem << setw(43) << "س����������� ���������" << '|' << endl;
																}
																else
																	if (buf_struct->TypeUser == const_10)
																	{
																		file_lexem << setw(43) << "��������� ���������" << '|' << endl;
																	}
																	else
																		if (buf_struct->TypeUser == const_8)
																		{
																			file_lexem << setw(43) << "³������ ���������" << '|' << endl;
																		}
																		else
																			if (buf_struct->TypeUser == const_binary)
																			{
																				file_lexem << setw(43) << "��i����� ���������" << '|' << endl;
																			}
																			else
																				if (buf_struct->TypeUser == const_string)
																				{
																					file_lexem << setw(43) << "�������� ���������" << '|' << endl;
																				}

					file_lexem << "---------------------------------------------------------------------------------------" << endl;
				}
				buf_struct = buf_struct->next;
				count_lex++;
			}
			count_line++;
		}
		file_test.close();
	}
	else cout << "Unable to open file";
	return;
}


string string_correction(string buf_line, int a)
{
	char buf_ch;

		if (buf_line.empty()) return buf_line;
		for (int i = 0; i < a; i++)
		{
			if (buf_line[i] == '�') buf_line[i] = 'a';
			else if (buf_line[i] == '�') buf_line[i] = 'A';
			else if (buf_line[i] == '�') buf_line[i] = 'e';
			else if (buf_line[i] == '�') buf_line[i] = 'E';
			else if (buf_line[i] == '�') buf_line[i] = 'i';
			else if (buf_line[i] == '�') buf_line[i] = 'I';
			else if (buf_line[i] == '�') buf_line[i] = 'y';
			else if (buf_line[i] == '�') buf_line[i] = 'o';
			else if (buf_line[i] == '�') buf_line[i] = 'O';
			else if (buf_line[i] == '�') buf_line[i] = 'p';
			else if (buf_line[i] == '�') buf_line[i] = 'P';
			else if (buf_line[i] == '�') buf_line[i] = 'H';
			else if (buf_line[i] == '�') buf_line[i] = 'K';
			else if (buf_line[i] == '�') buf_line[i] = 'x';
			else if (buf_line[i] == '�') buf_line[i] = 'X';
			else if (buf_line[i] == '�') buf_line[i] = 'c';
			else if (buf_line[i] == '�') buf_line[i] = 'C';
			else if (buf_line[i] == '�') buf_line[i] = 'B';
			else if (buf_line[i] == '�') buf_line[i] = 'M';
			else if (buf_line[i] == '�') buf_line[i] = 'T';
		buf_ch = buf_line[i];
		buf_line[i] = toupper(buf_ch);
		} return buf_line;
}

int count_operand(lexem* buf_struct)
{
	int flag = 0;
	while (buf_struct != NULL)
	{
		if (strchr(buf_struct->lexem_str, ','))
			flag++;
		buf_struct = buf_struct->next;
	}
	return flag;
}

void create_sintax_analys()
{
	string buf_line, buf_pre, buf_post;
	lexem* buf_struct;
	ifstream file_test;
	ofstream file_sintax;
	int count_line = 1, flag;
	int* buf_vect;

	file_test.open("D:\\Documents\\2 ����\\2 �������\\��\\Course\\CourseWork_Jonh\\test.asm");
	file_sintax.open("D:\\Documents\\2 ����\\2 �������\\��\\Course\\CourseWork_Jonh\\Sintax_Analys.txt");

	if (file_test.is_open())
	{
		while (!file_test.eof())
		{
			getline(file_test, buf_line);
			int a = buf_line.size();
			if (int pos = buf_line.find("equ") != -1)
			{
				buf_struct = del_sentence(string_correction(buf_line, a));
				buf_pre = buf_struct->lexem_str;
				buf_struct = buf_struct->next;
				if (strcmp(buf_struct->lexem_str, "EQU") == 0)
				{
					buf_struct = buf_struct->next;
					while (buf_struct != NULL)
					{
						buf_post = buf_post + buf_struct->lexem_str;
						buf_struct = buf_struct->next;
					}
				}
			}
			int pos1 = buf_line.find(buf_pre);
			int pos2 = buf_line.find("equ");

			if (pos1 != -1 && pos2 == -1)
			{
				buf_line.replace(pos1, buf_pre.size(), buf_post);
			}
			buf_struct = del_sentence(string_correction(buf_line, a));
			flag = count_operand(buf_struct);
			buf_vect = sentence_struct(buf_struct, flag);
			if (flag != 2)
			{
				file_sintax << "����� �" << count_line << endl;
				file_sintax << "-------------------------------------------------------------------------------------" << endl;
				file_sintax << "|���� ���� |     ���� ���������    |    ������ �������     |     ������ �������    |" << endl;
				file_sintax << "-------------------------------------------------------------------------------------" << endl;
				file_sintax << "| � ������� |  � ����� | ʳ������ |  � ����� | ʳ������ |  � ����� | ʳ������ |" << endl;
				file_sintax << "-------------------------------------------------------------------------------------" << endl;
				int j = 6, i = 0;
				file_sintax << setw(12) << buf_vect[i] << '|';
				for ( i = 1; i <= j; i++)
				{
					file_sintax << setw(11) << buf_vect[i] << '|' ;
				}
				file_sintax << endl;
				file_sintax << "-------------------------------------------------------------------------------------" << endl;
			}
			else
			{
				file_sintax << "����� �" << count_line << endl;
				file_sintax << "-------------------------------------------------------------------------------------------------------------" << endl;
				file_sintax << "|���� ���� |     ���� ���������    |    ������ �������     |     ������ �������    |     ����� �������    |" << endl;
				file_sintax << "-------------------------------------------------------------------------------------------------------------" << endl;
				file_sintax << "| � ������� |  � ����� | ʳ������ |  � ����� | ʳ������ |  � ����� | ʳ������ |  � ����� | ʳ������ |" << endl;
				file_sintax << "-------------------------------------------------------------------------------------------------------------" << endl;
				int j = 2 + (2 * flag - (flag - 1)) * 2, i = 0;
				file_sintax << setw(12) << buf_vect[i] << '|';
				for (i = 1; i <= j; i++)
				{
					file_sintax << setw(11) << buf_vect[i] << '|';
				}
				file_sintax << endl;
				file_sintax << "-------------------------------------------------------------------------------------------------------------" << endl;
			}
			count_line++;
		}
		file_test.close();
	}
	else cout << "Unable to open file";
	return;
}