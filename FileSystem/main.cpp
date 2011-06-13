/* 
 * File:   main.cpp
 * Author: bigfun
 *
 * Created on 10 styczeń 2010, 18:37
 */

#include <stdlib.h>
#include "FSManager.h"
#include <memory>
using std::auto_ptr;

int main(int argc, char** argv)
{

    string menu = "1. Pokaz informacje o dysku\n"
                  "2. Pokaz strukture blokow\n"
                  "3. Pokaz tablice FAT \n"
                  "4. Skopiuj plik NA dysk wirtualny\n"
                  "5. Skopiuj plik Z dysku wirtualnego\n"
                  "6. Usun plik z dysku\n"
                  "7. Wyjdz z programu\n\n"
                  "Podaj opcje: ";

    if (argc >= 3)
    {
        auto_ptr<FSManager> manager(new FSManager(string(argv[1]),atoi(argv[2]),atoi(argv[3])));
        cout << "\nWitaj w programie!\n\n\n";
        int choice;
        string fileName, newFile;
        bool leave = false;
        while (!leave)
        {
            cout << menu;
            cin >> choice;
            cout << "\n\n";
            switch (choice)
            {
                case 1:
                {
                    cout << "INFORMACJE o DYSKU:\n\n";
                    cout << manager->showInfo() << "\n";
                    break;
                }
                case 2:
                {
                    cout << "STRUKTURA:\n\n";
                    cout << manager->showStructure() << "\n";
                    break;
                }
                case 3:
                {
                    cout << "Tablica FAT: \n\n";
                    cout << manager->showFATTable() << "\n";
                    break;
                }
                case 4:
                {
                    cout << "Podaj nazwe pliku: ";
                    cin >> fileName;
                    manager->copyIn(fileName);
                    break;
                }
                case 5:
                {
                    cout << "Podaj nazwe pliku do odczytania: ";
                    cin >> fileName;
                    cout << "Podaj nazwe pliku do zapisu: ";
                    cin >> newFile;
                    manager->copyOut(fileName, newFile);
                    break;
                }
                case 6:
                {
                    cout << "Podaj nazwe pliku: ";
                    cin >> fileName;
                    manager->remove(fileName);
                    break;
                }
                case 7:
                {
                    leave = true;
                    break;
                }
                default:
                    break;
            }

        
        }
        return (EXIT_SUCCESS);
    }
    else
    {
        cout << "\n Podaj informacje o dysku \n";
        return (EXIT_FAILURE);
    }    
}

