-------------------------------------------------------------------------------
--
-- Title       : or4
-- Design      : lab1
-- Author      : 
-- Company     : 
--
-------------------------------------------------------------------------------
--
-- File        : lab1_2.vhd
-- Generated   : Fri Oct  5 00:55:07 2012
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
--{entity {or4} architecture {or4}}

library IEEE;
use IEEE.STD_LOGIC_1164.all;

entity or4 is
	 port(
		 in1 : in STD_LOGIC;
		 in2 : in STD_LOGIC;
		 in3 : in STD_LOGIC;
		 in4 : in STD_LOGIC;
		 out1 : out STD_LOGIC
	     );
end or4;

--}} End of automatically maintained section

architecture or4 of or4 is
begin

	out1<=in1 or in2 or in3 or in4 after 15 ns;

end or4;
