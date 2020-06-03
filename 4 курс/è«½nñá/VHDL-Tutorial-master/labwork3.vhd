library IEEE;
use IEEE.STD_LOGIC_1164.all;

entity task1 is
	generic(delay:time := 130ns);
	port(
		T : in STD_LOGIC;
		notR : in STD_LOGIC;
		C : in STD_LOGIC;
		Q : out STD_LOGIC;
		notQ : out STD_LOGIC
	);
end task1;

architecture task1 of task1 is
begin								 
	process(C, notR)
		variable res:integer := 0;
		begin
			if (C'event and C = '0' and notR = '1' and T = '1') then res := 1-res;
			end if;
			if (notR'event and notR = '0') then res := 0;
			end if;
			if (res = 1) then Q <= '1' after delay; notQ <= '0' after delay;
			else Q <= '0' after delay; notQ <= '1' after delay;
			end if;
	end process;
end task1;	

---------------------------------------

library IEEE;
use IEEE.STD_LOGIC_1164.all;

entity task2 is
	port(
		T : in STD_LOGIC;
		notR : in STD_LOGIC;
		C : in STD_LOGIC;
		Q : out STD_LOGIC;
		notQ : out STD_LOGIC
	);
end task2;

architecture task2 of task2 is								 
														 
component \3nand\ is
	port(in1: in STD_LOGIC; in2: in STD_LOGIC; in3: in STD_LOGIC; out1: out STD_LOGIC);
end component;

signal S1, S2, S3, S4, S5, S6, S7: STD_LOGIC;

begin
	D1: \3nand\ port map(C, T, notQ, S1);
	D2: \3nand\ port map(C, T, Q, S2);
	D3: \3nand\ port map('1', S1, S4, S3);
	D4: \3nand\ port map(S3, S2, notR, S4);
	D5: \3nand\ port map(C, C, C, S5);
	D6: \3nand\ port map('1', S3, S5, S6);
	D7: \3nand\ port map('1', S4, S5, S7);
	D8: \3nand\ port map('1', S6, notQ, Q);
	D9: \3nand\ port map(notR, S7, Q, notQ);
end task2;	 

------------------------------------------

library IEEE;
use IEEE.STD_LOGIC_1164.all;

entity \3nand\ is
	 port(
		 in1 : in STD_LOGIC;
		 in2 : in STD_LOGIC;
		 in3 : in STD_LOGIC;
		 out1 : out STD_LOGIC
	     );
end \3nand\;

architecture \3nand\ of \3nand\ is
begin
	out1 <= not (in1 and in2 and in3) after 20ns;
end \3nand\;
