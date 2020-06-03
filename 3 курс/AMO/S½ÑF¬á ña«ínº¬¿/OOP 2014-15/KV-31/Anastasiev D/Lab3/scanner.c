/************************************************************************
*file: scanner.c
*purpose: file
*author: Anastasiev D.
*written: 24/10/2014
*last modified: 21/11/2014
*************************************************************************/
#include "Scanner.h"

void inputscanner(SCAN_INFO scan){
	FILE *f, *tmp;
	int count,new_count,i;
	SCAN_INFO scan_temp;
	f = fopen("Scanners.dba","rb");
	count = fgetc(f);
	new_count = count;
	if(count!= 0){
		for(i = 0;i<count;i++){
			fread(&scan_temp,sizeof(scan_temp),1,f);
			if(!strcmp(scan_temp.manufacturer,scan.manufacturer) && 
				scan_temp.year == scan.year &&
				!strcmp(scan_temp.model, scan.model) &&
				scan_temp.price == scan.price &&
				scan_temp.x_size == scan.x_size &&
				scan_temp.y_size == scan.y_size &&
				scan_temp.optr == scan.optr) return;
		}
	}
	new_count++;
	tmp = fopen("tmp.dba","w+b");
	fputc(new_count,tmp);
	rewind(f);
	count = fgetc(f);
	for(i = 0;i<count;i++){
			fread(&scan_temp,sizeof(scan_temp),1,f);
			fwrite(&scan_temp,sizeof(scan_temp),1,tmp);
	}
	fclose(f);
	f = fopen("Scanners.dba","wb");
	rewind(tmp);
	new_count = fgetc(tmp);
	fputc(new_count,f);
	for(i = 0;i<count;i++){
		fread(&scan_temp,sizeof(scan_temp),1,tmp);
		fwrite(&scan_temp,sizeof(scan_temp),1,f);
	}


	fwrite(&scan,sizeof(scan),1,f);
	
	fclose(f);
	fclose(tmp);
	remove("tmp.dba");
}

SCAN_INFO* get_rec(char *csv_line){
		SCAN_INFO *res;
		int i,num_len=0,k,p=0;
		int len = strlen(csv_line);
		char *st;
		res = (SCAN_INFO*)malloc(sizeof(SCAN_INFO));

		for(i = 0; i<=len; i++){
		if(csv_line[i]==';' || csv_line[i] == '\0'){
			st = (char*)calloc(num_len+1,sizeof(char));
			for(k = 0 ; k<num_len; k++) st[k] = csv_line[i-num_len+k];
			switch(p){
				case 0:
					strcpy(res->manufacturer,st);
					break;
				case 1:
					res->year = atoi(st);
					break;
				case 2:
					strcpy(res->model,st);
					break;
				case 3:
					res->price = atof(st);
					break;
				case 4:	
					res->x_size = atoi(st);
					break;
				case 5:
					res->y_size = atoi(st);
					break;
				case 6:
					res->optr = atoi(st);
					break;
			}
			p++;
			free(st);
			num_len = 0;
		}
		 else num_len++;
	}
	return res;
}


void first_input(){
	FILE *f, *scan;
	char buf[128], *str;
	int count = 0;
	SCAN_INFO *tmp_scan;
	f = fopen("Scanners.csv","r");
	if(f==NULL)return;
	while(fgets(buf,128,f)){		
		count++;
	}
	rewind(f);
	scan = fopen("Scanners.dba","wb");
	fputc(count,scan);
	while(fgets(buf,128,f)){		
		tmp_scan = get_rec(buf);
		fwrite(tmp_scan,sizeof(*tmp_scan),1,scan);
	}
	fclose(scan);
	fclose(f);
}

int namecmp(const void* a, const void* b);
int intcmp(const void* a, const void* b);
int floatcmp(const void* a, const void* b);
typedef struct {
		int id;
		void *el;
	}vect;

int make_index(char *dba, char *field_name){
	FILE *f;
	int i;
	SCAN_INFO scan_temp;
	
	int count = 0;
	char buf[128];
	vect *mas;
	int *pi;
	float *pf;
	char *name;
	f = fopen(dba,"rb");
	if(f == NULL)return 0;
	count = fgetc(f);
	mas = (vect*)malloc(sizeof(vect)*count);
	for(i = 0;i<count;i++){
		fread(&scan_temp, sizeof(scan_temp),1,f);
		if(!strcmp("manufacturer", field_name)){
			mas[i].el = (void*)malloc(sizeof(char)*(strlen(scan_temp.manufacturer)+1));
			strcpy((char*)mas[i].el,scan_temp.manufacturer);
		}
		if(!strcmp("model", field_name)){
			mas[i].el = (void*)malloc(sizeof(char)*(strlen(scan_temp.model)+1));
			strcpy((char*)mas[i].el,scan_temp.model);
		}
		if(!strcmp("year", field_name)){
			mas[i].el = (void*)malloc(sizeof(int));
			pi = (int*)mas[i].el;
			*pi = scan_temp.year;
		}
		if(!strcmp("price", field_name)){
			mas[i].el = (void*)malloc(sizeof(float));
			pf = (float*)mas[i].el;
			*pf = scan_temp.price;
		}
		if(!strcmp("x_size", field_name)){
			mas[i].el = (void*)malloc(sizeof(int));
			pi = (int*)mas[i].el;
			*pi = scan_temp.x_size;
		}
		if(!strcmp("y_size", field_name)){
			mas[i].el = (void*)malloc(sizeof(int));
			pi = (int*)mas[i].el;
			*pi = scan_temp.y_size;
		}
		if(!strcmp("optr", field_name)){
			mas[i].el = (void*)malloc(sizeof(int));
			pi = (int*)mas[i].el;
			*pi = scan_temp.optr;
		}
		mas[i].id = i;
	}
	fclose(f);
	if(field_name =="manufacturer" ||  field_name == "model") qsort(mas,count,sizeof(vect),namecmp);
		else if(field_name =="price")qsort(mas,count,sizeof(vect),floatcmp);
		else qsort(mas,count,sizeof(vect),intcmp);
	name = (char*)malloc(sizeof(char)*(strlen(field_name)+5));
	strcpy(name,field_name);
	strcat(name,".idx");	
	f = fopen(name,"wb");
	for(i = 0;i<count;i++){
		fputc(mas[i].id,f);
	}
	fclose(f);
	for(i = 0;i<count;i++)free(mas[i].el);
	free(mas);

	return 1;
}

int namecmp(const void* a, const void* b)
 {
     vect *pa = (vect*)a;
     vect *pb = (vect*)b;
	 return strcmp((char*)pa->el, (char*)pb->el);
 }

int intcmp(const void* a, const void* b)
 {
     vect *pa = (vect*)a;
     vect *pb = (vect*)b;
	 int *pia = (int*)pa->el;
	 int *pib = (int*)pb->el;
	 return *pia - *pib;
 }

int floatcmp(const void* a, const void* b)
 {
     vect *pa = (vect*)a;
     vect *pb = (vect*)b;
	 float *pfa = (float*)pa->el;
	 float *pfb = (float*)pb->el;
	 return *pfa - *pfb;
 }


RECORD_SET* get_recs_by_index(char *dba , char *indx_field ){
	RECORD_SET *res;
	FILE *f, *scan;
	int i,count = 0, cout_ind,j;
	SCAN_INFO scan_temp;
	char buf[128];

	f = fopen(indx_field, "rb");
	if(f==NULL)return NULL;
	while(!feof(f)){
		i = fgetc(f);
		if(i != -1)count++;
	}
	res = (RECORD_SET*)malloc(sizeof(RECORD_SET)*count);
	count = 0;
	rewind(f);

	scan = fopen(dba,"rb");

	count = fgetc(scan);
	rewind(scan);
	res->rec_nmb = count;
	res->recs = (SCAN_INFO*)malloc(sizeof(SCAN_INFO)*(res->rec_nmb));
	for(i = 0;i<res->rec_nmb;i++){
		count = fgetc(scan);
		cout_ind = fgetc(f);
		for(j = 0;j<=cout_ind;j++){
			fread(&res->recs[i],sizeof(SCAN_INFO),1,scan);
		}
		rewind(scan);
	}
	fclose(f);
	fclose(scan);

	return res;
}


void reindex(char *dba){
	make_index(dba,"manufacturer");
	make_index(dba,"year");
	make_index(dba,"model");
	make_index(dba,"price");
	make_index(dba,"x_size");
	make_index(dba,"y_size");
	make_index(dba,"optr");
}

int del_scan(char *dba, int n){
	FILE *f;
	int i;
	RECORD_SET buf;
	f = fopen(dba,"rb");
	if(f == NULL)return 0;
	buf.rec_nmb = fgetc(f);
	if(n<0 || n>buf.rec_nmb) return 0;
	buf.recs = (SCAN_INFO*)malloc(sizeof(SCAN_INFO)*buf.rec_nmb);
	for(i = 0;i<buf.rec_nmb;i++){
		fread(&buf.recs[i],sizeof(SCAN_INFO),buf.rec_nmb,f);
	}
	f = freopen(dba,"wb",f);
	fputc(buf.rec_nmb - 1,f);
	for(i = 0;i<buf.rec_nmb;i++){
		if(n != i){
			fwrite(&buf.recs[i],sizeof(SCAN_INFO),buf.rec_nmb,f);
		}
	}
	fclose(f);
	reindex(dba);
	free(buf.recs);
	return 1;
}

void read_price_txt(char *dba){
	FILE *f,*text;
	SCAN_INFO scan;
	int count,i, cost;
	f = fopen(dba,"rb");
	text = fopen("price_scan.txt","w");
	if(f==NULL)return;
	puts("\nEnter price:\n");
	scanf("%d",&cost);
	count = fgetc(f);
	for(i = 0;i<count;i++){
		fread(&scan,sizeof(SCAN_INFO),1,f);
		if(scan.price<=cost)fprintf(text,"%s %d %s %f %d %d %d\n",scan.manufacturer,
			scan.year, scan.model, scan.price, scan.x_size, scan.y_size, scan.optr);
	}
	fcloseall();
}
