#!/usr/bin/spim
# Author: Karol Stępniewski - karol.stepniewski@gmail.com
# 
# Program do liczenia Dyskretnej Transformaty Fouriera (DFT) przy pomocy Szybkiej Transformacji Fouriera (FFT)
# Algorytm Cooleya-Tukeya
#
###############################################################

.data
real: .space 64
imag: .space 64
realtemp: .space 64
imagtemp: .space 64
promptnumber: .asciiz "Podaj liczbe nr "
promptreal: .asciiz "Podaj czesc rzeczywista: "
promptimag: .asciiz "Podaj czesc urojona: "
PI: .float 3.14159265358
nl: .asciiz "\n"
tab: .asciiz "\t\t"
spc: .asciiz " "

.text

# BEGIN main label ---------- #
main:

li $t6,16 			# counter, from 16 to 0
li $t7,0 			# keeps address  

# BEGIN data reading ------- #
readloop:			# loop for reading data
addiu $t6,$t6,-1		# decreases counter

li $v0,4			#prints prompt "Podaj liczbe.. "
la $a0,promptnumber	
syscall				

li $v0,1			# prints which number in a row is being provided
addiu $a0,$t6,-16		# 
neg $a0,$a0			# negation to make it positive number	
syscall					

li $v0,4			#prints new line char	
la $a0,nl			
syscall	

li $v0,4			# prints prompt for real number part input
la $a0,promptreal		
syscall	

li $v0,6			# reads float number from input
syscall					

s.s $f0,real($t7)		# saves provided number in buffer


li $v0,4			# prints prompt for imag number part input 
la $a0,promptimag		
syscall	

li $v0,6			# reads float number from input
syscall			
s.s $f0,imag($t7)

addiu $t7,$t7,4	


bnez $t6, readloop     #ends if all data has been provided
# END data reading loop 



li $t0,0			#temporary register for mem adressess	

#BEGIN rows transformation --- #
rows:

addiu $t1,$t0,0			# pointing registers to correct data in buffer
addiu $t2,$t0,4			
addiu $t3,$t0,8			
addiu $t4,$t0,12		
jal fftmain			# jump to FFT function

addiu $t0,$t0,16		# counter increasing

bleu $t0,64,rows		# jump again if some rows still need transformation
#END rows transformation -----#


li $t0,0

#BEGIN columns transformation --- #
columns:

addiu $t1,$t0,0			# pointing registers to correct data in buffer
addiu $t2,$t0,16		
addiu $t3,$t0,32		
addiu $t4,$t0,48
jal fftmain			# jump to FFT function

addiu $t0,$t0,4			

bleu $t0,12, columns		# jump again if still some columns need transformation
#END columns transformation  ---- #


li $t0,16				#printed values counter
li $t1,0				#memmory temp register

#BEGIN printing -------- #
printing:			
li $t2,4 				#register for counting numbers in row

loop:					
l.s $f12,real($t1)			# prints real part of number
li $v0,2				
syscall					

li $v0,4 				#space between real and imag
la $a0,spc			
syscall					

l.s $f12,imag($t1)			# prints imag part of number
li $v0,2				
syscall					

li $v0,4 				#tabulation
la $a0,tab			
syscall					

addiu $t1,$t1,4				# increasing mem pointer
addiu $t0,$t0,-1			# decreasing printed values counter
addiu $t2,$t2,-1			# decreasing row printed values counter

beqz $t0,end				# if all values printed end
bnez $t2,loop				# jumps again if there still some values to print in current row

li $v0,4 				# prints new line char for next row
la $a0,nl				
syscall					

j printing				# jumps again to printing 
#END printing ---- #
#BEGIN - END OF PROGRAM ---#

end:					# end of program

li $v0,4 				#prints new line char
la $a0,nl				
syscall					

li $v0,10				# syscall for program termination
syscall					

# END - END OF PROGRAM ---"

# BEGIN function sinus ---------- # 
sinus:
# $f20 - argument/result
# temp registers:
# 	$f15 - for calculating power
# 	$f16 - keeps factorials
# 	$f17 - result of x^n / n!
# 	$f18 - value of argument

l.s $f12,PI
l.s $f13,PI
neg.s $f12,$f12
angleloop:
c.lt.s $f20,$f12
bc1f endangleloop
add.s $f20,$f20,$f13
add.s $f20,$f20,$f13
j angleloop
endangleloop:

mov.s $f15,$f20		# for calculating power

mov.s $f18,$f15		# keeps argument value for further use

mul.s $f15,$f15,$f18	# x^2
mul.s $f15,$f15,$f18    # x^3
li.s $f16,6.0		# 3!
div.s $f17,$f15,$f16	# x^3 / 3!

sub.s $f20,$f20,$f17	# x - x^3/3!
mul.s $f15,$f15,$f18    # x^4
mul.s $f15,$f15,$f18    # x^5
li.s $f16,120.0		# 5!
div.s $f17,$f15,$f16 	# x^5 / 120
add.s $f20,$f20,$f17 	# x - x^3 / 3! + x^5 / 5!

mul.s $f15,$f15,$f18 	# x^6
mul.s $f15,$f15,$f18	# x^7
li.s $f16,5040.0	# 7!
div.s $f17,$f15,$f16	# x^7 / 7!
sub.s $f20,$f20,$f17	# x - x^3 / 3! + x^5 / 5! -  x^7 / 7!	

mul.s $f15,$f15,$f18 	# x^8
mul.s $f15,$f15,$f18	# x^9
li.s $f16,362880.0	# 9!
div.s $f17,$f15,$f16	# x^9 / 9!

add.s $f20,$f20,$f17	# x - x^3 / 3! + x^5 / 5! - x^7 / 7! + x^9 / 9!

jr $ra					#return to call 
# END function sinus ----------- #

# BEGIN helper function mulltiplying complex numbers --- #

# f25 - real of first number / real of result
# f26 - imag of first number / imag of result
# f27 - real of second number
# f28 - imag of second number
# f29 - temp for real of first number
# f30 - temp for imag of first number

complexmul:

mov.s $f29,$f25
mov.s $f30,$f26

mul.s $f25,$f29,$f27
mul.s $f26,$f28,$f30
sub.s $f25,$f25,$f26

mul.s $f30,$f30,$f27
mul.s $f29,$f29,$f28
add.s $f26,$f30,$f29

jr $ra

# END helper function multiplying complex numbers --- #

# BEGIN multiplying complex and exponent  helper function #

# f20 - angle of exponent
# f21 - real of first number / real of result
# f22 - imag of first number / imag of result
# f23 - temp
# f24 - temp 

exponentmul:

# e^(i*alfa) * e^(i*beta) = e^(i*alfa) + e^(i*alfa) * (-2sin(beta/2)^2 + isin(beta))
# e^(i*alfa) = number in f20 and f21
# beta - f22
move $t9,$ra
jal sinus
mov.s $f23,$f20
li.s $f24,2.00
div.s $f20,$f20,$f24
jal sinus
mul.s $f20,$f20,$f20
li.s $f24,-2.00
mul.s $f20,$f20,$f24
mov.s $f25,$f21
mov.s $f26,$f22
mov.s $f27,$f20
mov.s $f28,$f23
jal complexmul
add.s $f21,$f21,$f25
add.s $f22,$f22,$f26
jr $t9
# END mulltiplying exponents helper function#

# BEGIN FFT 1D function ------------- #
#
# t1 , t2 , t3 , t4 - memmory addresses to arguments/results
# 
# calculating vector of Fourier's transform
# 
#

fftmain:

move $t6,$ra 			#saving return address
				
				#loading numbers from memmory
l.s $f0,real($t1)		#real part of first number
l.s $f1,real($t2)		#real part of second number
l.s $f2,real($t3)		#real part of third number
l.s $f3,real($t4)		#real part of fourth number

l.s $f4,imag($t1)		#imag part of first number
l.s $f5,imag($t2)               #imag part of second number
l.s $f6,imag($t3)		#imag part of third number
l.s $f7,imag($t4)		#imag part of fourth number


li $t5,0 			# iterator for temp buffers 
li $t8,0 			# keeps current element number (iterator for fftloop, k in FFT equation)

fftloop:			#main loop, one iteration for one element of result

mov.s $f8,$f0                   # initial value for real part of number
mov.s $f9,$f4			# initial value for imag part of number
mtc1 $t8,$f10			# copy fftloop iterator value and converts it to float
cvt.s.w $f10,$f10	        # ^^

l.s $f11, PI			# pi value
li.s $f12, -2.00 		# const -2 in e^(-2 * (PI/N) *k * n)

mul.s $f11,$f11,$f12		# pi * -2
li.s $f12, 4.00			# we have 4x4 matrix, so N = 4
div.s $f11,$f11,$f12		# -2pi / 4
mul.s $f11,$f11,$f10		# -2pi / 4 * k
li.s $f12, 1.00			# n
mul.s $f11,$f11,$f12		# -2pi/4 *k * n

				# setting arguments for exponent multiplying function
mov.s $f20,$f11			# angle (-2pi/4 *k * n)

mov.s $f21,$f1			# real part of Xn
mov.s $f22,$f5			# imag part of Xn
jal exponentmul
add.s $f8,$f8,$f21

add.s $f9,$f9,$f22


l.s $f11, PI			# pi value
li.s $f12, -2.00 		# const -2 in e^(-2 * (PI/N) *k * n)

mul.s $f11,$f11,$f12		# pi * -2
li.s $f12, 4.00			# we have 4x4 matrix, so N = 4
div.s $f11,$f11,$f12		# -2pi / 4
mul.s $f11,$f11,$f10		# -2pi / 4 * k
li.s $f12, 2.00			# n
mul.s $f11,$f11,$f12		# -2pi/4 *k * n

				# setting arguments for exponent multiplying function
mov.s $f20,$f11			# angle (-2pi/4 *k * n)
mov.s $f21,$f2			# real part of Xn
mov.s $f22,$f6			# imag part of Xn
jal exponentmul
add.s $f8,$f8,$f21

add.s $f9,$f9,$f22



l.s $f11, PI			# pi value
li.s $f12, -2.00 		# const -2 in e^(-2 * (PI/N) *k * n)

mul.s $f11,$f11,$f12		# pi * -2
li.s $f12, 4.00			# we have 4x4 matrix, so N = 4
div.s $f11,$f11,$f12		# -2pi / 4
mul.s $f11,$f11,$f10		# -2pi / 4 * k
li.s $f12, 3.00			# n
mul.s $f11,$f11,$f12		# -2pi/4 *k * n

				# setting arguments for exponent multiplying function
mov.s $f20,$f11			# angle (-2pi/4 *k * n)

mov.s $f21,$f3			# real part of Xn
mov.s $f22,$f7			# imag part of Xn
jal exponentmul


add.s $f8,$f8,$f21

add.s $f9,$f9,$f22



s.s $f8,realtemp($t5)		# saving real part of result in temp buffer
s.s $f9,imagtemp($t5)		# saving imag part of result in temp buffer
addiu $t5,$t5,4			# moving pointer to the next element of temp buffer
addiu $t8,$t8,1			# increasing fttploop iterator value

bleu $t5,12,fftloop		# conditional branch for fftloop


l.s $f0,realtemp($0)		# loading back data from buffer to registers
addiu $t7,$0,4			
l.s $f1,realtemp($t7)		
addiu $t7,$t7,4			
l.s $f2,realtemp($t7)		
addiu $t7,$t7,4			
l.s $f3,realtemp($t7)		

l.s $f4,imagtemp($0)		
addiu $t7,$0,4			
l.s $f5,imagtemp($t7)		
addiu $t7,$t7,4			
l.s $f6,imagtemp($t7)		
addiu $t7,$t7,4			
l.s $f7,imagtemp($t7)		

				  # saving new values to data buffer
s.s $f0,real($t1)		
s.s $f1,real($t2)		
s.s $f2,real($t3)		
s.s $f3,real($t4)		
s.s $f4,imag($t1)
s.s $f5,imag($t2)
s.s $f6,imag($t3)
s.s $f7,imag($t4)

jr $t6					# returns to call
# END  FFT 1D function ------------- #