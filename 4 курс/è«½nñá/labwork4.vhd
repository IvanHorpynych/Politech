library IEEE;
use IEEE.std_logic_1164.all;
use IEEE.std_logic_unsigned.all;

entity laba4 is
	generic (Delay:time := 130ns);
	port (
		CLK: in STD_LOGIC;
		Reset: in STD_LOGIC;
		DataIn: in STD_LOGIC_VECTOR (3 downto 0);
		DataOut: out STD_LOGIC_VECTOR (3 downto 0);
		Command: in STD_LOGIC_VECTOR (2 downto 0)
	);
end entity laba4;

architecture laba4 of laba4 is
begin
	process(CLK,Reset)
    	variable private:STD_LOGIC_VECTOR (3 downto 0);
    	begin
			if (Reset = '1') then private:="0000";
			elsif (CLK'event) and (CLK = '0') then
				case Command is
		    		when "000" => null;
    				when "001" => private := datain;
    				when "010" => private := private-1;
					when "011" => private := not (private or datain);
					when "100" => private := private(2 downto 0) & private(3);
    				when "101" => private := '0' & private(3 downto 1);
					when "110" => private := private(0) & private(1) & private(2) & private(3);
    				when others => null;
    			end case;
			end if;
			DataOut <= private after Delay;
    end process;
end architecture;