
работа со строками
https://thebeez.home.xs4all.nl/ForthPrimer/Forth_primer.html#toc-Chapter-3

печать строки

callsign count type cr

Morse code 

All eng symbols and numbers contain 1..5 elements

3 bits for count and 5 bits for dot/dash = 7 bits

Any symbol can placed to 1 byte!
len 
000 - 0
001 - 1
010 - 2
011 - 3
100 - 4
101 - 5

0 - dot / 1 - dash

len  = and %00000111
data = 2/ 2/ 2/

1 *----  %11110101
2 **---  %11100101
3 ***--  %11000101
4 ****-  %10000101
5 *****  %00000101
6 -****  %00001101
7 --***  %00011101
8 ---**  %00111101
9 ----*  %01111101
0 -----  %11111101

A *-
B -***
C -*-*
D -**
E *
F **-*
G --*
H ****
I **
J *---
K -*-
L *-**
M --
N -*
O ---
P *--*
Q --*-
R *-*
S ***
T -
U **-
V ***-
W *--
X -**-
Y -*--
Z --**


CREATE LIMITS  220 , 340 , 170 , 100 , 190 , 
1 CELL * LIMITS + @ .
> 340


мелкая плата stm8

Зажечь/погасить лампочку
1 out! - зажечь
0 out! - погасить

фоновая задача

\ blinky using the background task timer to flash all board outputs 
VARIABLE timemask ;
$40 timemask !
: blinky timemask TIM OVER AND = OUT! ; 

' blinky BG !  \ set background word, start flashing
$80 timemask ! \ interactive: slower flashing

' tt BG !

variable TX
1 TX !

variable SYM_ELEMENT_STATE
\ enum for SYM_ELEMENT_STATE
0 CONSTANT RDY
1 CONSTANT ON
2 CONSTANT DELAY

RDY SYM_ELEMENT_STATE !

5  CONSTANT DOT_TICKS
15 CONSTANT DASH_TICKS

\ for delay
VARIABLE TARGET_TIM


: bg_task 
TX @ OUT! 
;



' blinky BG !

load_code
./codeload.py -p /dev/tty.usbserial-1430 -r 9600 serial test.fs


screen /dev/tty.usbserial-1420 9600

exit
control+a control+d

restore terminal
screen -r 

kill
control+a k - 

# links

word list stm8ef

https://github.com/TG9541/stm8ef/blob/master/docs/words.md
https://github.com/TG9541/stm8ef/wiki/STM8-eForth-Example-Code


arrays

create syms 10 allot
syms 10 cells dump

127 syms !

: pre S" UA3MQJ/B" ;
pre cells dump

CREATE LIMITS  1 C, 2 C, 3 C, 4 C, 5 C,
LIMITS 5 cells dump
CREATE LIMITS  1 C, 2 C, 3 C, 4 C, 5 C, redefined LIMITS   ok
LIMITS 5 cells dump                    
7F6ED5721708: 01 02 03 04  05 F9 7C BF - F7 FF 2F FF  E5 FE FF E5  ......|.../.....
7F6ED5721718: 7C BF FF FF  F9 78 91 22 - 44 84 44 89  12 24 44 AF  |....x."D.D..$D.
7F6ED5721728: 2D B8 BF 22  CB 2C B2 C2 -                           -..".,..
