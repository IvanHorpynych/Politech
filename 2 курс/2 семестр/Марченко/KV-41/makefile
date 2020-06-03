hello:*.o
	gcc -o hello *.o
*.o:hello.h
	gcc -c main.c hello.c
clean: clean_binary
	rm -f *.o
clean_binary:
	rm -f hello