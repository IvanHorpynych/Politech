library IEEE;
use IEEE.STD_LOGIC_1164.all;

entity \4and\ is
	 port(
		 in1 : in STD_LOGIC;
		 in2 : in STD_LOGIC;
		 in3 : in STD_LOGIC;
		 in4 : in STD_LOGIC;
		 out1 : out STD_LOGIC
	     );
end \4and\;

architecture \4and\ of \4and\ is
begin
	out1 <= in1 and in2 and in3 and in4 after 20ns;
end \4and\;

----------------------------------

library IEEE;
use IEEE.STD_LOGIC_1164.all;

entity \2or\ is
	 port(
		 in1 : in STD_LOGIC;
		 in2 : in STD_LOGIC;
		 out1 : out STD_LOGIC
	     );
end \2or\;

architecture \2or\ of \2or\ is
begin
	 out1 <= in1 or in2 after 20ns;
end \2or\;

----------------------------------

library IEEE;
use IEEE.STD_LOGIC_1164.all;

entity \2not\ is
	 port(
		 in1 : in STD_LOGIC;
		 in2 : in STD_LOGIC;
		 out1 : out STD_LOGIC;
		 out2 : out STD_LOGIC
	     );
end \2not\;

architecture \2not\ of \2not\ is
begin
	out1 <= not in1 after 20ns;
	out2 <= not in2 after 20ns;
end \2not\;
