#include "mem.h"


int main()
{
	int numcom,block_id,size,flag=0;
	mem_dispatcher* head = malloc(sizeof(mem_chunk));
	init(head);
	while (flag == 0)
	{
		
		printf("1.Allocate\n");
		printf("2.Deallocate\n");
		printf("3.Show mem map\n");
		printf("4.Defragment\n");
		printf("5.Exit\n");
		printf("Enter command: ");
		scanf("%d", &numcom);

		switch (numcom)
		{
		case 1:
			printf("Enter size: ");
			scanf("%d", &size);
			head->last_id_used = allocate(head, size);
			break;
		case 2:
			printf("Enter block_id: ");
			scanf("%d", &block_id);
			deallocate(head, block_id);
			break;
		case 3:
			show_memory_map(head);
			break;
		case 4:
			defragment(head);
			break;
		case 5:
			flag = -1;
			break;
		default:
			printf("Wrong command\n");
		}
		printf("\n");
	}
}