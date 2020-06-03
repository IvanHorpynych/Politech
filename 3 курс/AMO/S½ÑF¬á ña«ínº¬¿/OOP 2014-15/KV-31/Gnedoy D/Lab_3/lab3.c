/************************************************************************
*file: lab3.с
*author: Gnedoj D. O.
*group: KV-31, FPM
*written: 10/10/2014
*last modified: 15/10/2014
************************************************************************/

#include "Lab3.h"

int save_to_dat(char* filename, SCAN_INFO* scan){
	FILE *f_dat;
	SCAN_INFO ch;
	long last, count=sizeof(int);
	int k=0;
	if (!(scan)) return 1;
	if ((f_dat=fopen(filename,"r+b"))==NULL) return -1;
	fseek(f_dat,0L,SEEK_END);
	last=ftell(f_dat);
	fseek(f_dat,0L,SEEK_SET);
	if (last==0L) {
		fwrite(&k,sizeof(int),1,f_dat);
		last=ftell(f_dat);
	}
	else fread(&k,sizeof(int),1,f_dat);
	while (count<last){
		fread(&ch,sizeof(SCAN_INFO),1,f_dat);
		if (strcmp(scan->model,ch.model)==0){
			fclose(f_dat);
			return 1;
		}
		count+=sizeof(SCAN_INFO);
	}
	k++;
	fseek(f_dat,last,SEEK_SET);
	fwrite(scan,sizeof(SCAN_INFO),1,f_dat);
	fseek(f_dat,0L,SEEK_SET);
	fwrite(&k,sizeof(int),1,f_dat);
	fclose(f_dat);
	return 0;
};

int create_file(char* filename_csv, char* filename_dba){
	FILE *f_csv;
	FILE *f_dat;
	if ((f_dat = fopen("Scaners.dba", "wb")) == NULL) return -1;
	fclose(f_dat);
	char *strdob = (char*)malloc(400 * sizeof(char));
	SCAN_INFO ch;
	if ((f_csv = fopen("Scaners.csv", "r")) == NULL) return -1;
	int count_of_field = 0;
	char *str = (char*)malloc(400 * sizeof(char));
	while (fscanf(f_csv,"%s",str) == 1){
		strcat(str,";");
		for (int i = 0, j = 0; i < strlen(str); i++){
			if (str[i] != ';')
				strdob[i-j] = str[i];
			else{
				++count_of_field;
				strdob[i-j] = '\0';
				switch(count_of_field){
					case 1:
						for (int k = 0; k <= strlen(strdob); ++k)
							ch.manufacturer[k] = strdob[k];
						break;
					case 2:
						ch.year = atoi(strdob);
						break;
					case 3:
						for (int k = 0; k <= strlen(strdob); ++k)
								ch.model[k] = strdob[k];
						break;
					case 4:
						ch.price = atof(strdob);
						break;
					case 5:
						ch.x_size = atoi(strdob);
						break;
					case 6:
						ch.y_size = atoi(strdob);
						break;
					case 7:
						ch.optr = atoi(strdob);
						break;
						
					default :
						break;
				};
				j = i+1;
			};
		};
		save_to_dat("Scaners.dba", &ch);
		count_of_field = 0;
	};
	fclose(f_csv);
	return 0;
};

int make_index(char *dba, char *field_name){
	FILE *f_dba;
	FILE *f_idx;
	if ((f_dba = fopen(dba, "r")) == NULL) return -1;
	int ke;
	fread(&ke, sizeof(int), 1, f_dba);
	if (strcmp(field_name, "manufacturer") == 0){
		numbered_string *mass = (numbered_string*)malloc(ke*sizeof(numbered_string));
		int step_for_model = sizeof(int);
		fseek(f_dba,step_for_model,SEEK_SET);
		for (int i = 0; i < ke; ++i){
			fread(&mass[i].value, 60*sizeof(char), 1, f_dba);
			mass[i].position = i;
			step_for_model += 60*sizeof(char)+sizeof(float)+4*sizeof(int)+60*sizeof(char);
			fseek(f_dba, step_for_model, SEEK_SET);
		};
		fclose(f_dba);
		for(int i = 0 ; i < ke ; i++) { 
			for(int j = 0 ; j < ke - i - 1 ; j++) {  
			   if(strcmp(mass[j].value,mass[j+1].value) >= 0) {           
				  numbered_string tmp = mass[j];
				  mass[j] = mass[j+1]; 
				  mass[j+1] = tmp; 
			   };
			};
		};
		if ((f_idx = fopen("manufacturer.idx", "wb")) == NULL) return -1;
		fseek(f_idx,0L,SEEK_SET);
		for (int i = 0; i < ke; i++)
			fwrite(&mass[i].position,sizeof(int),1,f_idx);
		fclose(f_idx);
	}else
		if (strcmp(field_name, "year") == 0){
			numbered_integer *mass = (numbered_integer*)malloc(ke*sizeof(numbered_integer));
			int step_for_model = 60*sizeof(char)+sizeof(int);
			fseek(f_dba,step_for_model,SEEK_SET);
			for (int i = 0; i < ke; ++i){
				fread(&mass[i].value, sizeof(int), 1, f_dba);
				mass[i].position = i;
				step_for_model += 60*sizeof(char)+sizeof(float)+4*sizeof(int)+60*sizeof(char);
				fseek(f_dba, step_for_model, SEEK_SET);
			};
			fclose(f_dba);
			for(int i = 0 ; i < ke ; i++) { 
				for(int j = 0 ; j < ke - i - 1 ; j++) {  
				   if(mass[j].value >= mass[j+1].value){           
					  numbered_integer tmp = mass[j];
					  mass[j] = mass[j+1]; 
					  mass[j+1] = tmp; 
				   };
				};
			};
			if ((f_idx = fopen("year.idx", "wb")) == NULL) return -1;
			fseek(f_idx,0L,SEEK_SET);
			for (int i = 0; i < ke; i++)
				fwrite(&mass[i].position,sizeof(int),1,f_idx);
			fclose(f_idx);
	   }else
			if (strcmp(field_name, "model") == 0){
				numbered_string *mass = (numbered_string*)malloc(ke*sizeof(numbered_string));
				int step_for_model = 60*sizeof(char)+2*sizeof(int);
				fseek(f_dba,step_for_model,SEEK_SET);
				for (int i = 0; i < ke; ++i){
					fread(&mass[i].value, 60*sizeof(char), 1, f_dba);
					mass[i].position = i;
					step_for_model += 60*sizeof(char)+sizeof(float)+4*sizeof(int)+60*sizeof(char);
					fseek(f_dba, step_for_model, SEEK_SET);
				};
				fclose(f_dba);
				for(int i = 0 ; i < ke ; i++) { 
					for(int j = 0 ; j < ke - i - 1 ; j++) {  
					   if(strcmp(mass[j].value,mass[j+1].value) >= 0) {           
						  numbered_string tmp = mass[j];
						  mass[j] = mass[j+1]; 
						  mass[j+1] = tmp; 
					   };
					};
				};
				if ((f_idx = fopen("model.idx", "wb")) == NULL) return -1;
				fseek(f_idx,0L,SEEK_SET);
				for (int i = 0; i < ke; i++)
					fwrite(&mass[i].position,sizeof(int),1,f_idx);
				fclose(f_idx);
		   }else
				if (strcmp(field_name, "price") == 0){
					numbered_float *mass = (numbered_float*)malloc(ke*sizeof(numbered_float));
					int step_for_model = 2*60*sizeof(char)+2*sizeof(int);
					fseek(f_dba,step_for_model,SEEK_SET);
					for (int i = 0; i < ke; ++i){
						fread(&mass[i].value, sizeof(float), 1, f_dba);
						mass[i].position = i;
						step_for_model += 60*sizeof(char)+sizeof(float)+4*sizeof(int)+60*sizeof(char);
						fseek(f_dba, step_for_model, SEEK_SET);
					};
					fclose(f_dba);
					for(int i = 0 ; i < ke ; i++) { 
						for(int j = 0 ; j < ke - i - 1 ; j++) {  
						   if(mass[j].value >= mass[j+1].value){           
							  numbered_float tmp = mass[j];
							  mass[j] = mass[j+1]; 
							  mass[j+1] = tmp; 
						   };
						};
					};
					if ((f_idx = fopen("price.idx", "wb")) == NULL) return -1;
					fseek(f_idx,0L,SEEK_SET);
					for (int i = 0; i < ke; i++)
						fwrite(&mass[i].position,sizeof(int),1,f_idx);
					fclose(f_idx);
			   }else
					if (strcmp(field_name, "x_size") == 0){
						numbered_integer *mass = (numbered_integer*)malloc(ke*sizeof(numbered_integer));
						int step_for_model = 2*60*sizeof(char)+2*sizeof(int)+sizeof(float);
						fseek(f_dba,step_for_model,SEEK_SET);
						for (int i = 0; i < ke; ++i){
							fread(&mass[i].value, sizeof(int), 1, f_dba);
							mass[i].position = i;
							step_for_model += 60*sizeof(char)+sizeof(float)+4*sizeof(int)+60*sizeof(char);
							fseek(f_dba, step_for_model, SEEK_SET);
						};
						fclose(f_dba);
						for(int i = 0 ; i < ke ; i++) { 
							for(int j = 0 ; j < ke - i - 1 ; j++) {  
							   if(mass[j].value >= mass[j+1].value){           
								  numbered_integer tmp = mass[j];
								  mass[j] = mass[j+1]; 
								  mass[j+1] = tmp; 
							   };
							};
						};
						if ((f_idx = fopen("x_size.idx", "wb")) == NULL) return -1;
						fseek(f_idx,0L,SEEK_SET);
						for (int i = 0; i < ke; i++)
							fwrite(&mass[i].position,sizeof(int),1,f_idx);
						fclose(f_idx);
				   }else
						if (strcmp(field_name, "y_size") == 0){
							numbered_integer *mass = (numbered_integer*)malloc(ke*sizeof(numbered_integer));
							int step_for_model = 2*60*sizeof(char)+3*sizeof(int)+sizeof(float);
							fseek(f_dba,step_for_model,SEEK_SET);
							for (int i = 0; i < ke; ++i){
								fread(&mass[i].value, sizeof(int), 1, f_dba);
								mass[i].position = i;
								step_for_model += 60*sizeof(char)+sizeof(float)+4*sizeof(int)+60*sizeof(char);
								fseek(f_dba, step_for_model, SEEK_SET);
							};
							fclose(f_dba);
							for(int i = 0 ; i < ke ; i++) { 
								for(int j = 0 ; j < ke - i - 1 ; j++) {  
								   if(mass[j].value >= mass[j+1].value){           
									  numbered_integer tmp = mass[j];
									  mass[j] = mass[j+1]; 
									  mass[j+1] = tmp; 
								   };
								};
							};
							if ((f_idx = fopen("y_size.idx", "wb")) == NULL) return -1;
							fseek(f_idx,0L,SEEK_SET);
							for (int i = 0; i < ke; i++)
								fwrite(&mass[i].position,sizeof(int),1,f_idx);
							fclose(f_idx);
					   }else
							if (strcmp(field_name, "optr") == 0){
								numbered_integer *mass = (numbered_integer*)malloc(ke*sizeof(numbered_integer));
								int step_for_model = 2*60*sizeof(char)+4*sizeof(int)+sizeof(float);
								fseek(f_dba,step_for_model,SEEK_SET);
								for (int i = 0; i < ke; ++i){
									fread(&mass[i].value, sizeof(int), 1, f_dba);
									mass[i].position = i;
									step_for_model += 60*sizeof(char)+sizeof(float)+4*sizeof(int)+60*sizeof(char);
									fseek(f_dba, step_for_model, SEEK_SET);
								};
								fclose(f_dba);
								for(int i = 0 ; i < ke ; i++) { 
									for(int j = 0 ; j < ke - i - 1 ; j++) {  
									   if(mass[j].value >= mass[j+1].value){           
										  numbered_integer tmp = mass[j];
										  mass[j] = mass[j+1]; 
										  mass[j+1] = tmp; 
									   };
									};
								};
								if ((f_idx = fopen("optr.idx", "wb")) == NULL) return -1;
								fseek(f_idx,0L,SEEK_SET);
								for (int i = 0; i < ke; i++)
									fwrite(&mass[i].position,sizeof(int),1,f_idx);
								fclose(f_idx);	
						   }else{
				
						   };
	return 0;
};

RECORD_SET * get_recs_by_index(char *dba , char *indx_field ){
	FILE *f_dba;
	FILE *f_idx;
	int ke;
	if ((f_dba = fopen(dba, "rb")) == NULL) return NULL;
	if ((f_idx = fopen(indx_field, "rb")) == NULL) return NULL;
	fseek(f_dba,0L,SEEK_SET);
	fread(&ke, sizeof(int), 1, f_dba);
	RECORD_SET *mass;
	mass = (RECORD_SET*)malloc(sizeof(RECORD_SET));
	mass->rec_nmb = ke;
	mass->recs = (SCAN_INFO*)malloc(ke*sizeof(SCAN_INFO));
	for (int i = 0; i < ke; i++){
		SCAN_INFO ch;
		int j;
		fseek(f_idx,i*sizeof(int),SEEK_SET);
		fread(&j, sizeof(int), 1, f_idx);
		fseek(f_dba,sizeof(int) + j*sizeof(SCAN_INFO),SEEK_SET);
		fread(&ch, sizeof(SCAN_INFO), 1, f_dba);
		strcpy(mass->recs[i].manufacturer, ch.manufacturer);
		mass->recs[i].year = ch.year;
		strcpy(mass->recs[i].model, ch.model);
		mass->recs[i].price = ch.price;
		mass->recs[i].x_size = ch.x_size;
		mass->recs[i].y_size = ch.y_size;
		mass->recs[i].optr = ch.optr;
	};
	fclose(f_dba);
	fclose(f_idx);
	return mass;
};

void reindex(char *dba){
	make_index(dba,"manufacturer");
	make_index(dba,"year");
	make_index(dba,"model");
	make_index(dba,"price");
	make_index(dba,"x_size");
	make_index(dba,"y_size");
	make_index(dba,"optr");
};

int del_scan(char *dba, int n){
	FILE *f_dba;
	int ke;
	if ((f_dba = fopen(dba, "rb")) == NULL) return -1;
	fseek(f_dba,0L,SEEK_SET);
	fread(&ke, sizeof(int), 1, f_dba);
	if (n <= ke){
		SCAN_INFO *mass = (SCAN_INFO*)malloc((ke - 1)*sizeof(SCAN_INFO));
		int position_for_del = 4 + n*sizeof(SCAN_INFO);
		int j = 0;
		for (int i = 0; i < ke; ++i){
			if (i != (n - 1)) {
				SCAN_INFO ch;
				fseek(f_dba, 4 + i*sizeof(SCAN_INFO), SEEK_SET);
				fread(&ch, sizeof(SCAN_INFO), 1, f_dba);
				mass[j] = ch;
				j++;
			};
		};
		fclose(f_dba);
		if ((f_dba = fopen(dba, "wb")) == NULL) return -1;
		fseek(f_dba, 0L, SEEK_SET);
		--ke;
		fwrite(&ke, sizeof(int), 1, f_dba);
		for (int i = 0; i < ke; ++i){
			fseek(f_dba, 4 + i*sizeof(SCAN_INFO), SEEK_SET);
			fwrite(&mass[i], sizeof(SCAN_INFO), 1, f_dba);
		};
		fclose(f_dba);
		reindex("Scaners.dba");
		return 1;
	}
	else return -1;
};

int from_dba_to_txt(char *dba, char *txt, float limit_price){
	FILE *f_txt;
	FILE *f_dba;
	if ((f_txt = fopen(txt, "wb")) == NULL) return -1;
	if ((f_dba = fopen(dba, "rb")) == NULL) return -1;
	int ke;
	fseek(f_dba,0L,SEEK_SET);
	fread(&ke, sizeof(int), 1, f_dba);
	make_index(dba, "price");
	RECORD_SET *p1 = get_recs_by_index( dba, "price.idx" );
	int i = 0;
	while ((p1->recs[i].price <= (float)limit_price) && (i < ke)){
		fprintf(f_txt, "%s;%d;%s;%.2f;%d;%d;%d;\n", p1->recs[i].manufacturer, p1->recs[i].year, p1->recs[i].model, p1->recs[i].price, p1->recs[i].x_size, p1->recs[i].y_size, p1->recs[i].optr);
		++i;	
	};
	fclose(f_txt);
	fclose(f_dba);
	return 0;
};