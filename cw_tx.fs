\ enum for tx_state
0 CONSTANT IN_SLEEP
1 CONSTANT IN_TRANSMIT

\ constants forr timer delay
10 CONSTANT SLEEP_TICKS
2  CONSTANT TRANSMIT_TICKS

variable tx_state
variable ticks
variable tick_start

: tx_state_init
    IN_SLEEP tx_state !
;

: tick_start_init
    0 tick_start !
;

: ticks_init
    0 ticks !
;

: set_start 
    ticks @ tick_start !
;

: get_start 
    tick_start @
;

: get_ticks 
    ticks @
;

: start_transmit
    \ set_start
    IN_TRANSMIT tx_state !
    \ set index in message string
    0 msg_idx !
    message count msg_len !
;

: start_sleep
    set_start
    IN_SLEEP tx_state !
;

: SLEEP
    IN_SLEEP tx_state @ = IF
        \ ." SLEEP" cr
        get_ticks get_start - SLEEP_TICKS >= IF
            start_transmit
        THEN
    THEN
;

: TRANSMIT
    IN_TRANSMIT tx_state @ = IF
        \ ." TRANSMIT" cr
        \ get_ticks get_start - TRANSMIT_TICKS >= IF
            \ start_sleep
        \ THEN

        \ get next symbol
        1 msg_idx +!
        msg_idx @ chars message + c@ emit cr

        \ check end of message
        msg_idx @ msg_len @ = IF
            start_sleep
        THEN
    THEN
;

: NEXT_TICK 1 ticks +! ;

: cw_tx_init
    tx_state_init
    tick_start_init
    ticks_init
;

cw_tx_init

: MAIN 90 0 DO
    SLEEP
    TRANSMIT
    NEXT_TICK
    \ ticks @ . cr
LOOP ;

\ : MAIN
\     BEGIN
\         NEXT_TICK
\         ticks @ . cr
\     AGAIN
\ ;

\ MAIN

\ bye
