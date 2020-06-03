library IEEE;
use IEEE.STD_LOGIC_1164.all;

entity task1 is
	 port(
		 x1 : in STD_LOGIC;
		 x2 : in STD_LOGIC;
		 x3 : in STD_LOGIC;
		 y1 : out STD_LOGIC;
		 y2 : out STD_LOGIC
	     );
end task1;

architecture task1 of task1 is

component \4and\ is
	port(in1: in STD_LOGIC; in2: in STD_LOGIC; in3: in STD_LOGIC; in4: in STD_LOGIC; out1: out STD_LOGIC);
end component;

component \2or\ is
	port(in1: in STD_LOGIC; in2: in STD_LOGIC; out1: out STD_LOGIC);
end component;

component \2not\ is
	port(in1: in STD_LOGIC; in2: in STD_LOGIC; out1: out STD_LOGIC; out2: out STD_LOGIC);
end component;

signal S1, S2, S3, S4, S5, S6: STD_LOGIC;

begin
	D1: \2not\ port map(X3, X3, S1, S5);
	D2: \2not\ port map(X1, X2, S2, S3);
	D3: \4and\ port map(X1, X1, S1, S1, S4);
	D4: \4and\ port map(S2, S2, S3, S3, S6);
	D5: \2or\ port map(X2, S4, Y1);
	D6: \2or\ port map(S5, S6, Y2);
end task1;

---------------------------------------

library IEEE;
use IEEE.std_logic_1164.all;

entity task2 is
	generic(maxdelay:time := 130ns);
	port (
		indata: in STD_LOGIC_VECTOR (2 downto 0);
		outdata: out STD_LOGIC_VECTOR (1 downto 0)
	);
end entity task2;

architecture task2 of task2 is
begin
	with indata select outdata <=
		"01" after maxdelay when "000" ,
		"01" after maxdelay when "001" ,
		"11" after maxdelay when "010" ,
		"10" after maxdelay when "011" ,
		"11" after maxdelay when "100" ,
		"00" after maxdelay when "101" ,
		"11" after maxdelay when "110" ,
		"10" after maxdelay when others
	;
end architecture task2; 
