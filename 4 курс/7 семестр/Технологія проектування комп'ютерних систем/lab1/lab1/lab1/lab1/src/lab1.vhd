-------------------------------------------------------------------------------
--
-- Title       : and2
-- Design      : lab1
-- Author      : 
-- Company     : 
--
-------------------------------------------------------------------------------
--
-- File        : lab1.vhd
-- Generated   : Fri Oct  5 00:20:54 2012
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
--{entity {and2} architecture {and2}}

library IEEE;
use IEEE.STD_LOGIC_1164.all;

entity and2 is
	 port(
		 in1 : in STD_LOGIC;
		 in2 : in STD_LOGIC;
		 out1 : out STD_LOGIC
	     );
end and2;

--}} End of automatically maintained section

architecture and2 of and2 is
begin

	 out1<=in1 and in2 after 15 ns;

end and2;
