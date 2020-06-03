#include "l12_argz.h"

/*
 * The argz_create_sep function converts the null-terminated string string into an
 * argz vector (returned in argz and argz len) by splitting it into elements at every
 * occurrence of the character sep.
 */
error_t argz_create_sep (const char *string, int sep, char **argz, size_t *argz_len){
    int i = 0;
    if (!string){
        return ENOMEM;
    }
    *argz_len = strlen(string)+1;
    *argz = (char*)malloc(*argz_len);
    
    if (!(*argz)) {
        return ENOMEM;
    }

    for (i = 0; i<*argz_len; i++){
	if (string[i] != sep){
			(*argz)[i] = string[i];
        }else {
            (*argz)[i] = 0;
        }
    }

    return OK;
}

//Returns the number of elements in the argz vector.
size_t argz_count (char *argz, size_t arg_len){
    int countval = 0;
    //const char *temp = argz;
	char *temp = 0;

    if (!argz) {
        return 0;
    }
    while ( temp = argz_next(argz,arg_len,temp) ){
        countval++;
    } 

    return countval;
}

/* 
 * The argz_next function provides a convenient way of iterating over the elements in the argz vector argz.
 * It returns a pointer to the next element in argz after the element entry, or 0 if there are
 * no elements following entry. If entry is 0, the first element of argz is returned.
 * This behavior suggests two styles of iteration:
 * char *entry = 0;
 * while ((entry = argz_next (argz, argz_len, entry)))
 * 					action;
 * (the double parentheses are necessary to make some C compilers shut up about
 * what they consider a questionable while-test) and:
 * char *entry;
 * for (entry = argz; entry; entry = argz_next (argz, argz_len, entry))
 * 				action;
 * Note that the latter depends on argz having a value of 0 if it is empty (rather than a pointer to an empty block
 * of memory); this invariant is maintained for argz vectors created by the functions here.
 */
char* argz_next (char *argz, size_t argz_len, const char *entry){
    int offset;
	char* nextelem;    
    if (!argz){
        return 0;
    }
    if (!entry) {
        return argz; 
    }
	
   
   offset = entry - argz + strlen(entry) + 1;  //????
    if (offset < argz_len){
        return argz + offset;
    }else {
        return 0;
    }
}
/*
 * Searches for entry in argz, if it doesn't have it, returns NULL
 */
char *argz_find(char *argz, size_t argz_len, char* entry){
    char *temp = 0;

    if (!argz || !entry){
    	return 0;
    }

    while (temp = argz_next(argz,argz_len,temp)){
        if (!strcmp(entry,temp)) return temp;
    }

    return 0;
}

/*
 * The argz_add function adds the string str to the end of the argz vector *argz,
 * and updates *argz and *argz_len accordingly.
 */
error_t argz_add (char **argz, size_t *argz_len, const char *str){
    if (!(*argz) || !str) {
        return ENOMEM;
    }
    if (!(*argz = (char*)realloc(*argz,((*argz_len)+strlen(str)+1)))){
        return ENOMEM;
    }
    strcpy(*argz + *argz_len,str);
    *argz_len = strlen(str) + (*argz_len) +1;
    return OK;
}


/*
 * If entry points to the beginning of one of the elements in the argz vector *argz,
 * the argz_delete function will remove this entry and reallocate *argz, modifying *argz
 * and *argz_len accordingly. Note that as destructive argz functions usually reallocate
 * their argz argument, pointers into argz vectors such as entry will then become invalid.
 */
void argz_delete (char **argz, size_t *argz_len, char *entry){
    char *new_argz;  //poiner to memory where new argz will be placed   
    //char *argz_entry;//pointer to entry inside argz
    char *old_argz_counter = 0,*new_argz_counter = 0;  
    int   newlen,entry_len;
    
    if (!(*argz) || !entry) {
        return;
    }
    entry_len = strlen(entry)+1;    
    
    newlen = (*argz_len)-entry_len;
    
    if(!(new_argz = (char*)malloc(newlen))){
        return;
    }

    while(old_argz_counter = argz_next(*argz,*argz_len,old_argz_counter)){
        //if (old_argz_counter != argz_entry){
        if (strcmp(old_argz_counter,entry)){        
            new_argz_counter = argz_next(new_argz,newlen,new_argz_counter);
            strcpy(new_argz_counter,old_argz_counter);
        }
    }
    free(*argz);
    *argz = new_argz;
    *argz_len = newlen;
}

/*
 * The argz_insert function inserts the string entry into the argz vector *argz at a
 * point just before the existing element pointed to by before, reallocating *argz and
 * updating *argz and *argz_len. If before is 0, entry is added to the end instead
 * (as if by argz_add). Since the first element is in fact the same as *argz, passing in *argz as
 * the value of before will result in entry being inserted at the beginning.
 */
error_t argz_insert (char **argz, size_t *argz_len, char *before, const char *entry){
    char *new_argz;
    char *argz_before;//pointer to before entry inside argz
    int new_len;
    char *old_argz_counter = 0,*new_argz_counter=0;
 
    if(!(*argz) || !entry ){
        return ENOMEM;
    }
    
    if (!before){
         argz_add(argz,argz_len,entry);
        return OK;
    }
    
    argz_before = argz_find(*argz,*argz_len,before);
    
    if(!argz_before){
        return ENOMEM;
    }
    
    new_len = *argz_len + strlen(entry) + 1;

    if (!(new_argz = (char*)malloc(new_len))){
        return ENOMEM;
    }
    
    while(old_argz_counter = argz_next(*argz,*argz_len,old_argz_counter)){
        new_argz_counter = argz_next(new_argz,new_len,new_argz_counter);
        if(old_argz_counter == argz_before) {
            strcpy(new_argz_counter,entry);
            new_argz_counter = argz_next(new_argz,new_len,new_argz_counter);
        }
        strcpy(new_argz_counter,old_argz_counter);
    }

    free(*argz);
    *argz = new_argz;
    *argz_len = new_len;
    return OK;
}



/*
 * Replace the string str in argz with string with, reallocating argz as
 * necessary.
 */
error_t argz_replace (char **argz, size_t *argz_len, const char *str, const char *with){
    char *new_argz;
    char *argz_replace_entry; //pointer to replace entry inside argz
    int new_len;
    char *old_argz_counter = 0,*new_argz_counter = 0;
    
    if (!(*argz) || !str || !with){
        return ENOMEM;
    }
    
    argz_replace_entry = argz_find(*argz,*argz_len,str);
    if(!argz_replace_entry){
        return ENOMEM;
    }
    new_len = *argz_len - strlen(str) + strlen(with);
    new_argz = (char*)malloc(new_len);
    if (!new_argz){
        return ENOMEM;
    }
    
    while(old_argz_counter = argz_next(*argz,*argz_len,old_argz_counter)){
        new_argz_counter = argz_next(new_argz,new_len,new_argz_counter);
        if(old_argz_counter == argz_replace_entry) {
            strcpy(new_argz_counter,with);
        }else {
            strcpy(new_argz_counter,old_argz_counter);
        }
    }
    
    
    free(*argz);
    *argz = new_argz;
    *argz_len = new_len;
    return OK;
}

//prints argz vector
void argz_print(const char *argz, size_t argz_len){
    char *entry = 0;
    while (entry = argz_next (argz, argz_len, entry))
       printf("%s\n",entry);

	/*
	char *entry;
	for (entry = argz; entry; entry = argz_next (argz, argz_len, entry))
            printf("%s\n",entry);
	*/
}
