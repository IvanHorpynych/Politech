#include "argz.h"

char* argz_occur(const char *argz, size_t argz_len, const char *string);

error_t argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len) {
	unsigned i;
	if (strlen(string) == 0) return ENOMEM;
	*argz_len = strlen(string) + 1;
	*argz = (char*)malloc(sizeof(char*)*(*argz_len));
	for (i = 0; i <= (*argz_len); ++i) {
		if (string[i] == sep) (*argz)[i] = '\0';
		else (*argz)[i] = string[i];
	}
	(*argz)[i] = '\0';
	return OK;
}

size_t argz_count(const char *argz, size_t argz_len) {
	unsigned i;
	size_t count = 0;
	for (i = 0; i < argz_len; ++i)
		if (argz[i] == '\0') ++count;
	return count;
}

error_t argz_add(char **argz, size_t *argz_len, const char *str) {
	unsigned i = 0;
	*argz = realloc(*argz, sizeof(*argz) + sizeof(char*)*(strlen(str) + 2));
	do {
		(*argz)[(*argz_len) + i] = str[i];
		++i;
	} while (str[i]);
	(*argz)[(*argz_len) + i] = '\0';
	(*argz_len) = (*argz_len) + i + 1;
	return OK;
}

void argz_delete(char **argz, size_t *argz_len, char *entry) {
	unsigned occur_index, entry_len; unsigned i;
	char *occur_ptr = argz_occur(*argz, *argz_len, entry);
	if (occur_ptr == NULL) return;
	occur_index = occur_ptr - (*argz);
	entry_len = strlen(entry) + 1;
	for (i = occur_index; i < (*argz_len) - entry_len; ++i)
		(*argz)[i] = (*argz)[i + entry_len];
	(*argz_len) = (*argz_len) - entry_len;
}

error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry) {
	unsigned occur_index, entry_len; unsigned i;
	char *occur_ptr = argz_occur(*argz, *argz_len, before);
	if (occur_ptr == NULL) ENOMEM;
	occur_index = occur_ptr - (*argz);
	entry_len = strlen(entry) + 1;
	*argz = realloc(*argz, sizeof(*argz) + sizeof(char*)*(strlen(entry) + 2));
	for (i = (*argz_len) - 1; i >= occur_index; --i)
		(*argz)[i + entry_len] = (*argz)[i];
	for (i = 0; i < entry_len; ++i)
		(*argz)[occur_index + i] = entry[i];
	(*argz_len) = (*argz_len) + entry_len;
	return OK;
}

char * argz_next(char *argz, size_t argz_len, const char *entry) {
	unsigned occur_index; unsigned i;
	char *occur_ptr;
	if (entry == 0) return argz;
	occur_ptr = argz_occur(argz, argz_len, entry);
	if (occur_ptr == NULL) return NULL;
	occur_index = occur_ptr - argz;
	i = occur_index;
	while (argz[i]) ++i;
	if (i == argz_len - 1) return NULL;
	++i;
	return(&argz[i]);
}

error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with) {
	unsigned occur_index, str_len, with_len; unsigned i;
	char *occur_ptr = argz_occur(*argz, *argz_len, str);
	if (occur_ptr == NULL) return ENOMEM;
	occur_index = occur_ptr - (*argz);
	i = occur_index;
	while ((*argz)[i]) ++i;
	str_len = i - occur_index;
	with_len = strlen(with);
	if (with_len > str_len) {
		*argz = realloc(*argz, sizeof(*argz) + sizeof(char*)*(with_len - str_len));
		for (i = (*argz_len) - 1; i >= occur_index; --i)
			(*argz)[i + with_len - str_len] = (*argz)[i];
	}
	if (str_len > with_len)
		for (i = occur_index + str_len; i < (*argz_len); ++i)
			(*argz)[i - str_len + with_len] = (*argz)[i];
	for (i = 0; i < with_len; ++i)
		(*argz)[occur_index + i] = with[i];
	(*argz_len) = (*argz_len) - str_len + with_len;
	return OK;
}

void argz_print(const char *argz, size_t argz_len) {
	unsigned i;
	for (i = 0; i < argz_len; ++i)
		if (argz[i] != '\0') printf("%c", argz[i]);
		else printf("'\\0'");
		printf("\n");
}

char* argz_occur(const char *argz, size_t argz_len, const char *string) {
	unsigned i = 0;
	char *occur_ptr=NULL;
	while (i < argz_len) {
		occur_ptr = strstr(argz + i, string);
		if (occur_ptr == NULL)
		{
			while (argz[i]) ++i;
			++i;
		}
		else break;
	}
	return occur_ptr;
}