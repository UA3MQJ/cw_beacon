\ software defined registers emulator :)
cold

create regs 16 allot

\ set_reg ( value, Rn-- )
: set_reg 2* regs + ! ;

\ get_reg ( Rn -- value )
: get_reg 2* regs + @ ;

\ inc_reg ( Rn -- ) increment of Rn
: inc_reg dup get_reg 1+ swap set_reg ;

\ add_reg ( r1 r2 r3 -- r1 = r2+r3) \\ as arm -> add r1,r2,r3 ; r1=r2+r3
: add_reg get_reg swap get_reg + swap set_reg ;

create message 70 allot

: pre $" VVV CQ DE " ;
: call $" UA3MQJ/B " ;

: emit" 34 emit ;
: print_str emit" count type emit" ;
: place_str over c@ 1+ cmove ;

: len_str c@ ;

\ append_str ( src dest -- )
: append_str
    0 set_reg                    \ ld r0, dest (ptr)
    1 set_reg                    \ ld r1, src  (ptr)
    0 get_reg len_str 2 set_reg  \ ld r2, len(dest)
    1 get_reg len_str 3 set_reg  \ ld r3, len(src)
    4 2 3 add_reg                \ ld r4, dest_len + src_len ; new_str_len
    4 get_reg 0 get_reg c!       \ ld dest(ptr), r4 ; set new new_str_len
    \ move pointers to end str
    0 inc_reg 0 0 2 add_reg      \ ld r0, r0 + r2 + 1
    1 inc_reg                    \ r1++
    1 get_reg 0 get_reg 3 get_reg cmove \ src dest len cmove
;

pre message place_str
call message append_str
message print_str
\ call message 0 set_reg 1 set_reg

: debug
cr
." call" call . cr
." message" message . cr 
." r0" 0 get_reg . cr
." r1" 1 get_reg . cr
." r2" 2 get_reg . cr
." r3" 3 get_reg . cr
." r4" 4 get_reg . cr
;
\ ./codeload.py -p /dev/tty.usbserial-1430 -r 9600 serial sr.fs