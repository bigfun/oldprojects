#include <stdio.h>
#include <stdlib.h>
#include <math.h>
//extern int ifft(float * source_re, float * source_im, float * result_re, float * result_im);
extern int ifft(float * source_re, float * source_im);

void iFFT(float *x, float *y)  {
int i,j,k,n1,n2;
float c,s,e,a,t1,t2;        
            
j = 0; /* bit-reverse */
for (i=1; i < 15; i++)
{
  n1 = 8;
  while ( j >= n1 )
   {
    j = j - n1;
    n1 = n1/2;
   }
  j = j + n1;
               
  if (i < j)
   {
    t1 = x[i];
    x[i] = x[j];
    x[j] = t1;
    t1 = y[i];
    y[i] = y[j];
    y[j] = t1;
   }
}                           
                                           
n1 = 0; /* FFT */
n2 = 1;
                                             
for (i=0; i < 4; i++)
{
  n1 = n2;
  n2 = n2 + n2;
  e = 6.283185307179586/n2;
  a = 0.0;
                                             
  for (j=0; j < n1; j++)
   {
    c = cos(a);

    
    s = sin(a);
    a = a + e;
                                            
    for (k=j; k < 16; k=k+n2)
     {
    
      t1 = c*x[k+n1] - s*y[k+n1];
      t2 = s*x[k+n1] + c*y[k+n1];
      x[k+n1] = x[k] - t1;
      y[k+n1] = y[k] - t2;
      x[k] = x[k] + t1;
      y[k] = y[k] + t2;
     }
   }

}
     for (i = 0; i < 16; i++)
    {
      x[i] = x[i]/16.0;
      y[i] = y[i]/16.0;
    }                               
}
int main(void){
	float * source_re, * source_im,*result_re,*result_im;
	float temp;
	source_re = (float *) malloc(16*sizeof(float));
        source_im = (float *) malloc(16*sizeof(float));
	result_re = (float *) malloc(16*sizeof(float));	
	result_im = (float *) malloc(16*sizeof(float));
	int i;
	for (i = 0; i < 16;i++)
	{	
		printf("\nPodaj czesc rzeczywista liczby %d: ",i+1);
		scanf("%f", &temp);
		source_re[i] = temp;
		printf("\nPodaj czesc urojona liczby %d: ",i+1);
		scanf("%f",&temp);
		source_im[i] = temp;
		result_re[i] = source_re[i];
		result_im[i] = source_im[i];	
	}
	ifft(source_re,source_im);
	//ifft(source_re,source_im,result_re,result_im);
	for (i= 0; i < 16; i++)
	{
		printf("\n%f",source_re[i]);
		printf("\n%f",source_im[i]);
		printf("\n");
	}
	iFFT(result_re,result_im);
/*	for (i= 0; i < 16; i++)
	{
		printf("\nCzesc rzeczywista liczby tej lepszej %d: %f",i+1,result_re[i]);
		printf("\nCzesc urojona liczby tej lepszej %d: %f",i+1,result_im[i]);
	}	*/
	return 0;
}
