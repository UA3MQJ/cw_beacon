\ : pre $" VVV CQ DE " ;
\ : call $" UA3MQJ/B " ;
\ : loc $" LOC KO98KA KO98KA " ;

\ create message 70 allot
: message $" VVV CQ DE UA3MQJ/B UA3MQJ/B UA3MQJ/B LOC KO98KA KO98KA " ;

\ \ enum for tx_state
0 CONSTANT IN_SLEEP
1 CONSTANT IN_TX

\ \ constants forr timer delay
10 CONSTANT SLEEP_TICKS
2  CONSTANT TRANSMIT_TICKS

variable tst \ tx_state
variable ts \ tick_start
variable mi \ msg_idx
variable ml \ msg_len

\ print string variable
\ Example:
\  message print_str
: print_str count type ;

\ : emit" 34 emit ;

\ : print_str emit" count type emit" ;

\ \ place string to variable
\ \ Example:
\ \  create callsign 10 chars allot
\ \  callsign message place_str
\ \ : place_str over over >r >r char+ swap chars cmove r> r> c! ;
\ \ : place_str over count nip 1 + cmove ;
\ : place_str over c@ 1+ cmove ;

\ \ 1st variant
\ \ cmove src dest len
\ : append_str2 over c@ >r count + >r 1+ r> r> cmove ;
\ : append_str3 over over c@ swap c@ + swap c! drop ;
\ : append_str over over append_str2 append_str3 ;

\ \ 2st variant 
\ \ : len_str c@ ;
\ \ append 
\ \       3          2            1
\ \ 1) (a1+1) (a2+1+len(a2)) (len(a1)) cmove
\ \ 2) (len(a1)+len(a2)) a2 c!
\ \
\ \ \ a1 a2
\ \ \             _______1_______ _________2______________ ________3__________  
\ \ : append_str1 over len_str >r dup 1+ over len_str + >r over 1+ r> r> cmove ;
\ \ : append_str2 dup rot len_str swap len_str + swap c! ;
\ \ : append_str append_str1 append_str2 ;

\ \ 3st variant
\ : f1 1+ ;                                   f1(x) = x+1
\ : f2 c@ ;                                   f2(x) = len(x)
\ : f3 dup f1 swap f2 + ;                     f3(x) = f1(x) + f2(x)
\ : f4 f2 swap f2 + ;                         f4(x,y) = f2(x) +f2(y)
\ : f5 over f2 >r over f1 over f3 r> cmove ;  f5(a1, a2) = f1(a1) f3(a2) f2(a1) cmove
\ : f6 over over f4 over c! ;                 f6(a1, a2) = f4(a1, a2) a2 c!
\ : append_str f5 f6 ;

\ : make_message
\ pre message place_str
\ call message append_str
\ call message append_str
\ call message append_str
\ loc message append_str 
\ ;

\ \ message print_str

: tx_state_init IN_SLEEP tst ! ;

: tick_start_init 0 ts ! ;

: set_start tim ts ! ;

: get_start ts @ ;

: get_ticks tim ;

\ \ symbol in message string variables

: start_tx
    \ set_start
    IN_TX tst !
    \ set index in message string
    0 mi !
    message count ml !
;

: start_sleep
    set_start
    IN_SLEEP tst !
;

\ true -1; false 0
: > swap < ;
: >= over over >r >r > r> r> = or ;


: SLEEP
    IN_SLEEP tst @ = IF
        tim ts @ - SLEEP_TICKS >= IF
            start_tx
        THEN
    THEN
;

: TX
    IN_TX tst @ = IF
        \ ." TRANSMIT" cr
        \ get_ticks get_start - TRANSMIT_TICKS >= IF
            \ start_sleep
        \ THEN

        \ get next symbol
        1 mi +!
        mi @ message + c@ emit cr

        \ check end of message
        mi @ ml @ = IF
            start_sleep
        THEN
    THEN
;

tx_state_init
tick_start_init

: bgt SLEEP TX ;

\ ' bgt BG !