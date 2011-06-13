section .bss	
	tmp resb 4
	temp resb 4
	tempi resb 4
	tempn1 resb 4
	tempn2 resb 4
	a resb 4
	e resb 4
	cos resb 4
	sin resb 4
	t1 resb 4
	t2 resb 4
	tempxxx resb 4
	tempcos	resb 4
section .data
	pi2		dd		6.28318530718		;pi/2 poniewaz 2PI/N = PI/2 przy N = 4 
	dlugosc		dd		16.0 			; dlugosc wektora danych
;	wynik		resb	64
;	liczbypi	dd		0,0.392699,0.785398,1.178097		;0,pi/8,pi/4,3pi/8	wykorzystywane jako argument cosinusa
;	mnoznik		dd		0.5,0.707106,0.707106,0.707106		;1/2,sqrt(2)/2,sqrt(2)/2,sqrt(2)/2 wykorzystywane jako mnoznik elementu
	silnie		dd		2.0,24.0,720.0,40320.0,3628800.0,479001600.0,87178291200.0
	pidrugich	dd		1.57079633
	cztery		dd		4.0
	dwa			dd		2.0									;stala wykorzystywane w liczeniu dct
	jeden		dd		1.0	

section .text
global	ifft

ifft:
	push	ebp			; prolog
	mov	ebp, esp
	
	

	; czesc algorytmu obliczania iFFT
	; zamieniamy miejscami dane wejsciowe, aby wykorzystac okresowosc funkcji trygonometrycznych
	; i zmniejszyc ilosc koniecznych dzialan
	xor eax,eax 	; i
	xor ebx,ebx 	; j = 0;  
	inc eax		; i = 1
	zamiana:
	    mov ecx,8 ; n1 = 8
	    subpetla: ; while ( j >= n1)
		cmp ecx,ebx ; jesli n1 > j
		ja koniecsubpetli ; to konczymy petle
		sub ebx,ecx ; j = j - n1
		shr ecx,1 ; n1 = n1/2
		jmp subpetla
	    koniecsubpetli:
	    add ebx,ecx; j = j + n1

	    ; jesli i >= j, omijamy ten kawalek
	    cmp eax,ebx; if (i >= j ) jump
	    jnb koniecif
		mov	edi, [ebp + 8] 	; tablica wejsciowa czesci rzeczywistej
		fld dword[edi + eax*4]	; zamieniamy miejscami wartosci wejsciowe
		fld dword[edi + ebx*4]	 
		fstp dword[edi + eax*4]	
		fstp dword[edi + ebx*4]	
		mov	edi, [ebp + 12] ; tablica wejsciowa czesci urojonej
		fld dword[edi + eax*4];
		fld dword[edi + ebx*4];
		fstp dword[edi + eax*4];
		fstp dword[edi + ebx*4];
	    koniecif:
	inc eax
	cmp eax,15
	jb zamiana	
	call obliczifft ; wywolujemy funkcje liczaca ifft

 	mov		esp, ebp  ;epilog
 	pop		ebp
 	ret

; oblicza ifft, wynik znajduje sie w pamieci z danymi wejsciowymi

obliczifft:

	push	eax   
	push	ebx
	push	ecx
	push	edx

	xor eax,eax; i,n1
	xor ebx,ebx; j,n2
	xor ecx,ecx; k = 0
	inc ebx  ; n2 = 1
	petlaglowna: 		; for ( i = 0; i < 4; i++)
		cmp eax,4	; sprawdzamy warunek
		jnb  koniecpetliglownej ; koniec wykonania petli
		mov dword[tempi],eax   ; zachowujemy wartosc licznika i
		mov eax,ebx	; eax stauje sie n1
		shl ebx,1;	; n2 = n2 * 2
		fldz;		; ladujemy zero (0.0)
		fstp dword[a]	; a = 0.0
		mov dword[temp], ebx	; kopiujemy n2 do pamieci, aby moc wczytac go do koprocesora
		fild dword[temp]	; wczytujemy n2
		fld dword[pi2]		; wczytujemy wartosc pi * 2
		fdiv st0,st1;		; dzielimy 2pi/n2
		fstp dword[e];		; wrzucamy wartosc do zmiennej e
		ffree st0;		; czyscimy stos ( co by sie nie przepelnil)
		mov DWORD[tempn2],ebx	; zachowujemy n2 dla przyszlych pokolen...
		xor ebx,ebx;		; zerujemy ebx, wykorzystany zostanie jako licznik w nastepnej petli
		petlapierwsza:		; for (j = 0; j < n1; j++)
			cmp ebx, eax   ;  czy j >= n1
			jnb koniecpetlipierwszej; jesli tak, konczymy petle
			fld dword[a]   ; wyciagamy z pamieci zmienna a
			call cosinus		; liczymy jej cosinus
			fstp dword[cos] ; zachowujemy wartosc cosinusa
			fld dword[a]	; teraz dla sinusa...
			call sinus
			fstp dword[sin]
			fld dword[a]	; operacja a = a + e
			fadd dword[e]
			fstp dword[a]	; a = a + e
			mov ecx,ebx; 	; ecx bedzie k
			petladruga: 	; for (k = j ; k < 16; k+=n2)
				cmp ecx,16; 
				jnb koniecpetlidrugiej ; koniec petli
				mov edx, eax	; eax to nasze n1
				add edx, ecx	; edx = n1 + k

				; wyliczamy t1
				; t1 = x[k + n1] * cos - y[k+n1] * sin
				mov	edi, [ebp+8] ; pobieramy wartosc rzeczywista
				fld DWORD[edi + edx*4]; ladujemy ja
				mov	edi, [ebp+12] ; to samo dla urojonej...
				fld DWORD[edi + edx*4]
				fmul DWORD[sin]	; mnozymy urojona razy sinus
				fxch st1	; zamiana urojonej z rzeczywista 
				fmul DWORD[cos] ; mnozymy rzeczywista razy cosinus
				fsub st0,st1	; odejmujemy (cos * re - sin *im )
				fstp dword[t1]  ; t1 = cos *re - sin * im
				ffree st0	; czyscimy stos aby sie nie przepelnil

				; wyliczamy t2
				; t2 = x[k + n1] * sin + y[k+n1] * cos
				mov	edi, [ebp+8]
				fld DWORD[edi + edx*4];
				mov	edi, [ebp+12]
				fld DWORD[edi + edx*4];
				fmul DWORD[cos]
				fxch st1
				fmul DWORD[sin]
				fadd st0,st1
				fstp dword[t2]
				ffree st0

				; x[k+n1] = x[k] - t1
				mov	edi, [ebp+8]
				fld DWORD[edi +ecx*4]
				fsub dword[t1]
				fstp DWORD[edi + edx*4]

				; y[k+n1] = y[k] - t2
				mov	edi, [ebp+12]
				fld DWORD[edi + ecx*4]
				fsub DWORD[t2]
				fstp DWORD[edi + edx*4]
				
				; x[k] = x[k] + t1
				mov	edi, [ebp+8]
				fld DWORD[edi + ecx*4]
				fadd DWORD[t1]
				fstp DWORD[edi + ecx*4]

				;y[k] = y[k] + t2
				mov	edi, [ebp+12]
				fld DWORD[edi + ecx*4]
				fadd DWORD[t2]
				fstp DWORD[edi + ecx*4]

				add ecx, DWORD[tempn2] ; dodajemy do licznika n2
				jmp petladruga
			koniecpetlidrugiej:
			inc ebx;
			jmp petlapierwsza;
		koniecpetlipierwszej:
	mov eax,DWORD[tempi];
	inc eax;
	mov ebx,DWORD[tempn2];
	jmp petlaglowna
	koniecpetliglownej: ; koniec obliczania ifft
	
	; normalizujemy powstaly wynik, dzielac wszystkie liczby przez ich ilosc
	xor eax,eax;
	normalizacja:
	    cmp eax, 16
	    jnb koniecnormalizacji
	    mov	edi, [ebp + 8] 	; liczby rzeczywiste
	    fld dword[edi + eax*4]	; ladujemy liczbe rzeczywista i dzielimy ja przez dlugosc wektora wejsciowego
	    fdiv dword[dlugosc];
	    fstp dword[edi + eax*4]	
	    mov	edi, [ebp + 12] ; to samo dla urojonych
	    fld dword[edi + eax*4];
	    fdiv dword[dlugosc];
	    fstp dword[edi + eax*4];
	    inc eax
	    jmp normalizacja
	koniecnormalizacji:

	pop		eax
	pop		ebx	
	pop		ecx
	pop		edx
	ret

	sinus:
	    fadd dword[pidrugich];
	    call cosinus
	    fchs
	    ret

	cosinus:	; funkcja obliczajaca cosinus danej liczby znajdujacej sie w st0
	
	okres:		; sprawdza, czy mozna zmniejszyc kat o okres w celu zwiekszenia dokladnosci wyniku
	fld dword[pi2]	;porownywanie z 2 pi
	fcomip st1	
	ja koniecokresu	;jezeli mniejsze od 2*pi, nic nie zmieniamy
	fsub dword[pi2] ; jezeli wieksze, odejmujemy okres
	jmp okres	; jesli kat byl na tyle duzy, ze mozemy po raz kolejny odjac 2pi, robimy to
	koniecokresu:													
	
	fst dword[tempcos]	; zapamietuje wartosc argumentu do pozniejszych obliczen
	
	;1 - x^2/2
	fmul st0,st0		
	fdiv dword[silnie]	
	fsubr dword[jeden]	
	
	;x^4/24
	fld dword[tempcos]	
	fmul st0,st0		
	fmul st0,st0		
	fdiv dword[silnie+4]	
	faddp st1,st0
	
	;x^6/720
	fld dword[tempcos]	
	fmul st0,st0		
	fmul st0,st0		
	fmul dword[tempcos]	
	fmul dword[tempcos]	
	fdiv dword[silnie+8]	
	fsubp st1,st0
	
	;x^8/40320
	fld dword[tempcos]	
	fmul st0,st0		
	fmul st0,st0		
	fmul st0,st0		
	fdiv dword[silnie+12]	
	faddp st1,st0	
	
	;x^10/10!
	fld dword[tempcos]	
	fmul st0,st0		
	fmul st0,st0		
	fmul st0,st0		
	fmul dword[tempcos]	
	fmul dword[tempcos]	
	fdiv dword[silnie+16]	
	fsubp st1,st0	

	;x^12/12!
	fld dword[tempcos]	
	fmul st0,st0		
	fmul st0,st0		
	fmul st0,st0		
	fmul dword[tempcos]	
	fmul dword[tempcos]	
	fmul dword[tempcos]	
	fmul dword[tempcos]	
	fdiv dword[silnie+20]
	faddp st1,st0	
	
	;x^14/14!
	fld dword[tempcos]	
	fmul st0,st0		
	fmul st0,st0		
	fmul st0,st0		
	fmul dword[tempcos]	
	fmul dword[tempcos]	
	fmul dword[tempcos]	
	fmul dword[tempcos]	
	fmul dword[tempcos]	
	fmul dword[tempcos]
	fdiv dword[silnie+24]	
	fsubp st1,st0

	ret
