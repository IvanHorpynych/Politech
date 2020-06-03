void val (int val){
	val = 1;
}
void ref (int *ref){
	*ref = 1;
}
void add (int &add){
	 add = 1;
}
int a;
int main(){
	a = 0;
	val(a);
	ref(&a);
	add(a);
	return 0;
}