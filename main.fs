create callsign 10 chars allot
create location  8 chars allot
create message  50 chars allot

include str.fs

S" UA3MQJ/B" callsign place_str
S" KO98KA" location place_str

\ symbol in message string variables
variable msg_idx
variable msg_len

\ one morse symbol
variable m_symb
variable m_symb_len

include cw_tx.fs

\ make message
: make_message 
s" VVV CQ DE " message place_str
callsign count message append_str
s"  " message append_str
callsign count message append_str
s"  " message append_str
callsign count message append_str
s"  LOC " message append_str
location count message append_str
s"  " message append_str
location count message append_str
;

\ see result
\ message print_str

\ s" *** CW Beacon ***" type cr
\ s" Callsign: " type
\ callsign print_str cr
\ S" Location: " type
\ location print_str cr

\ make_message
\ ." Make beacon message:" cr 
\ message print_str cr cr

\ get len and data from packed morse symbol
\ %11111101 
\ dup
\ %111 and m_symb_len !
\ 2/ 2/ 2/ m_symb !


\ MAIN

\ bye

