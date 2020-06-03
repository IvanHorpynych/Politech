-------------------------------------------------------------------------------
--
-- Title       : not_or2
-- Design      : lab1
-- Author      : 
-- Company     : 
--
-------------------------------------------------------------------------------
--
-- File        : lab1_3.vhd
-- Generated   : Fri Oct  5 11:03:47 2012
-- From        : interface description file
-- By          : Itf2Vhdl ver. 1.22
--
-------------------------------------------------------------------------------
--
-- Description : 
--
-------------------------------------------------------------------------------

--{{ Section below this comment is automatically maintained
--   and may be overwritten
--{entity {not_or2} architecture {not_or2}}

library IEEE;
use IEEE.STD_LOGIC_1164.all;

entity not_or2 is
	 port(
		 in1 : in STD_LOGIC;
		 in2 : in STD_LOGIC;
		 out1 : out STD_LOGIC
	     );
end not_or2;

--}} End of automatically maintained section

architecture not_or2 of not_or2 is
begin

	out1<= ( not ( in1 or in2 )) after 15 ns;

end not_or2;
