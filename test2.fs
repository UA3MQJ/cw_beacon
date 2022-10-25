\ create message 70 allot
\ : message $" VVV CQ DE UA3MQJ/B UA3MQJ/B UA3MQJ/B LOC KO98KA KO98KA " ;
: message s" VVV CQ DE UA3MQJ/B UA3MQJ/B UA3MQJ/B LOC KO98KA KO98KA " ;

variable stm1 \ (0)wait -> (1)tx 
variable stm2 \ (0)inactive -> (1)get_sym ->(2)tx_sym
variable mi \ msg_idx
variable ml \ msg_len

variable ticks \ TICKS emulator

0 ticks !

: NEXT_TICK 1 ticks +! 5 ms ; \ TIM 16 bit 328s period 0.005s == 5ms

variable tick_start \ delay by timer variable
variable tick_end

0 tick_start !


: stm1_tx
    1 stm1 !
    1 stm2 !
    0 mi !
    message swap drop ml !
    ticks @ 1000 + tick_end ! 
    ." Start TX" cr
;

: stm1_wait
    0 stm1 !
    0 stm2 !
    ticks @ 100 + tick_end !
    ." Start wait" cr
;

: sym. 
    mi @ message drop + c@ emit ."  "
    \ ." SYM"
;

: stm1_0
    stm1 @ 0 = IF
        \ ." in wait" cr
        tick_end @ ticks @ < IF
            stm1_tx
        THEN
    THEN ;


: stm2_1
    \ ." in tx" cr
    stm2 @ 1 = IF
        sym.
        1 mi +!
        2 stm2 !
    THEN ;

: stm2_2
    stm2 @ 2 = IF 
        ." TX symbol "
        3 stm2 !
    THEN ;

: stm2_3
    stm2 @ 3 = IF
        mi @ ml @ = IF
            stm1_wait
        ELSE
            1 stm2 !
        THEN
    THEN ;

: stm1_1
    stm1 @ 1 = IF
        stm2_1
        stm2_2
        stm2_3
    THEN ;

: check_states
    stm1_0
    stm1_1
;



: MAIN
    stm1_wait
    0 stm2 !

    BEGIN
        NEXT_TICK
        ticks @ . tick_end @ . stm1 @ . stm2 @ . mi @ . ml @ . cr

        check_states
    AGAIN
;

MAIN

bye
