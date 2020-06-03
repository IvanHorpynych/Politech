#include "argz.h"

char* argz_occur (const char *argz, size_t argz_len, const char *string);

/* ARGZ_CREATE_SEP function makes ARGZ vector from null-terminated STRING. New element of ARGZ starts when SEP symbol is found in
STRING. ARGZ_LEN contains the length of ARGZ (including the last terminating null). */
error_t argz_create_sep (const char *string, int sep, char **argz, size_t *argz_len) {
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

/* ARGZ_COUNT function returns the number of elements in ARGZ vector. */
size_t argz_count (const char *argz, size_t argz_len) {
	unsigned i;
	size_t count = 0;
	for (i = 0; i < argz_len; ++i)
		if (argz[i] == '\0') ++count;
	return count;
}

/* ARGZ_ADD function adds STR to the end of ARGZ, modifying ARGZ and ARGZ_LEN accordingly. */
error_t argz_add (char **argz, size_t *argz_len, const char *str) {
	unsigned i = 0;
	*argz = realloc(*argz, sizeof(*argz)+sizeof(char*)*(strlen(str)+2));
	do {
		(*argz)[(*argz_len)+i] = str[i];
		++i;
	} while (str[i]);
	(*argz)[(*argz_len)+i] = '\0';
	(*argz_len) = (*argz_len) + i + 1;
	return OK;
}

/* ARGZ_DELETE function removes ENTRY element from ARGZ vector, modifying ARGZ and ARGZ_LEN accordingly. */
void argz_delete (char **argz, size_t *argz_len, char *entry) {
	unsigned occur_index, entry_len; unsigned i;
	char *occur_ptr = argz_occur(*argz, *argz_len, entry);
	if (occur_ptr == NULL) return;
	occur_index = occur_ptr - (*argz);
	entry_len = strlen(entry) + 1;
	for (i = occur_index; i < (*argz_len)-entry_len; ++i)
		(*argz)[i] = (*argz)[i + entry_len];
	(*argz_len) = (*argz_len) - entry_len;
}

/* ARGZ_INSERT function inserts ENTRY element into ARGZ vector in the position before BEFORE element, modifying ARGZ and ARGZ_LEN
accordingly */
error_t argz_insert (char **argz, size_t *argz_len, char *before, const char *entry) {
	unsigned occur_index, entry_len; unsigned i;
	char *occur_ptr = argz_occur(*argz, *argz_len, before);
	if (occur_ptr == NULL) ENOMEM;
	occur_index = occur_ptr - (*argz);
	entry_len = strlen(entry) + 1;
	*argz = realloc(*argz, sizeof(*argz)+sizeof(char*)*(strlen(entry)+2));
	for (i = (*argz_len)-1; i >= occur_index; --i)
		(*argz)[i+entry_len] = (*argz)[i];
	for (i = 0; i < entry_len; ++i)
		(*argz)[occur_index+i] = entry[i];
	(*argz_len) = (*argz_len) + entry_len;
	return OK;
}

/* ARGZ_NEXT function provides a convenient way of iterating over the elements in the ARGZ vector. It returns a pointer to the next
element in ARGZ after the element ENTRY, or 0 if there are no elements following ENTRY. If entry is 0, the first element of argz is
returned. */
char * argz_next (char *argz, size_t argz_len, const char *entry) {
	unsigned occur_index; unsigned i;
	char *occur_ptr;
	if (entry == 0) return argz;
	occur_ptr = argz_occur(argz, argz_len, entry);
	if (occur_ptr == NULL) return NULL;
	occur_index = occur_ptr - argz;
	i = occur_index;
	while (argz[i]) ++i;
	if (i == argz_len-1) return NULL;
	++i;
	return(&argz[i]);
}

/* ARGZ_REPLACE function replaces STR element of ARGZ vector with WITH element, modifying ARGZ and ARGZ_LEN accordingly */
error_t argz_replace (char **argz, size_t *argz_len, const char *str, const char *with) {
	unsigned occur_index, str_len, with_len; unsigned i;
	char *occur_ptr = argz_occur(*argz, *argz_len, str);
	if (occur_ptr == NULL) return ENOMEM;
	occur_index = occur_ptr - (*argz);
	i = occur_index;
	while ((*argz)[i]) ++i;
	str_len = i - occur_index;
	with_len = strlen(with);
	if (with_len > str_len) {
		*argz = realloc(*argz, sizeof(*argz)+sizeof(char*)*(with_len-str_len));
		for (i = (*argz_len)-1; i >= occur_index; --i)
			(*argz)[i+with_len-str_len] = (*argz)[i];
	}
	if (str_len > with_len)
		for (i = occur_index + str_len; i < (*argz_len); ++i)
			(*argz)[i-str_len+with_len] = (*argz)[i];
	for (i = 0; i < with_len; ++i)
		(*argz)[occur_index+i] = with[i];
	(*argz_len) = (*argz_len) - str_len + with_len;
	return OK;
}

/* Function for ARGZ printing */
void argz_print (const char *argz, size_t argz_len) {
	unsigned i;
	for (i = 0; i < argz_len; ++i)
		if (argz[i] != '\0') printf("%c", argz[i]);
		else printf("'\\0'");
	printf("\n");
}

/* ARGZ_OCCUR function returns a pointer of occuration of STRING in ARGZ */
char* argz_occur (const char *argz, size_t argz_len, const char *string) {
	unsigned i = 0;
	char *occur_ptr;
	while (i < argz_len) {
		occur_ptr = strstr(argz+i, string);
		if (occur_ptr == NULL) {
			while (argz[i]) ++i;
			++i;
		}
		else break;
	}
	return occur_ptr;
}
 