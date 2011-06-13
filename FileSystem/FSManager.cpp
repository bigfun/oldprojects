/* 
 * File:   FSManager.cpp
 * Author: bigfun
 * 
 * Created on 10 styczeń 2010, 18:37
 */

#include "FSManager.h"
#define DEBUG 1

void printDEBUG(stringstream& s)
{
    if (DEBUG == 1)
    {
        cout << s.str();
    }
    s.clear();
}

FSManager::FSManager(string fileName /* = "disk.vdi" */, unsigned int blockSize /* = 2KB */, unsigned int discSize /* = 4096KB */)
{
    cout << "FSManager - inicjalizacja...\n";
    DiskInfo info;
    this->blkSize = blockSize;
    this->dskSize = discSize;
    this->diskName = fileName;
    //char letter = 'a';
    diskFile.open(fileName.c_str(), fstream::in | fstream::out | fstream::binary);
    if (diskFile.is_open())
    {
        cout << "Plik o podanej nazwie juz istnieje, sprawdzam czy jest to dysk wirtualny : ";
        diskFile.read((char *) & info, sizeof (info));
        if (diskFile.fail() || diskFile.eof() || info.checker != 123)
        {
            cout << "NIE\n";
            cout << "Wartosc sumy kontrolnej: " << info.checker << std::endl;
            cout << "Wystapil blad?: " << diskFile.fail() << std::endl;
            cout << "Koniec pliku?: " << diskFile.eof() << std::endl;
            std::exit(1);
        }

        cout << "TAK\n";
    } else
    {
        cout << "Plik dysku wirtualnego nie istnieje, tworze nowy... ";
        diskFile.open(fileName.c_str(), fstream::out | fstream::binary);
        if (!diskFile.is_open())
        {
            cout << "BLAD\nNie udalo sie utworzyc pliku dysku wirtualnego.\n";
            exit(1);
        }
        diskFile.close();
        diskFile.open(fileName.c_str(), fstream::out | fstream::in | fstream::binary);

        char * buf = new char[dskSize];
        memset(buf, ' ', dskSize);
        diskFile.write(buf, dskSize);
        diskFile.put('a');
        diskFile.seekp(0, fstream::beg);
        this->filesCount = 0;
        info.blockSize = blockSize;
        info.diskSize = discSize;
        info.filesCount = 0;
        info.freeSpace = discSize - sizeof (info) - sizeof (Block);
        info.checker = 123;
        if (sizeof (info) > blockSize)
        {
            cout << "BLAD\nPodany rozmiar bloku jest za maly, nie pomiesci wszystkich potrzebnych informacji\n";
            std::exit(1);
        }
        diskFile.write((char*) & info, sizeof (info));
        Block block;
        block.next = 0;
        block.size = 0;
        block.current = 1;
        diskFile.write((char*) & block, sizeof (block));
        diskFile.seekp(0);
        cout << "UTWORZONO\n";
        cout << "Zakonczono inicjalizacje dysku wirtualnego\n";
    }
    cout << "PARAMETRY DYSKU: \n";
    this->blkSize = info.blockSize;
    cout << "Rozmiar bloku danych: " << blkSize << "B\n";
    this->dskSize = info.diskSize;
    cout << "Rozmiar dysku: " << dskSize << "B\n";
    this->freeSpace = info.freeSpace;
    cout << "Ilosc wolnego miejsca: " << freeSpace << "B\n";
    this->filesCount = info.filesCount;
    cout << "Ilosc plikow na dysku: " << filesCount << "\n";
}

int FSManager::copyIn(string fileName)
{
    cout << "Rozpoczynam kopiowanie pliku " << fileName << " do dysku wirtualnego...\n";
    fstream file;
    unsigned fileBlock;
    unsigned int fileSize;
    file.open(fileName.c_str(), fstream::in);
    if (!file.is_open())
    {
        cout << "Blad podczas kopiowania pliku na dysk wirtualny: Podany plik nie istnieje\n";
        return -1;
    }
    if (findFile(fileName))
    {
        cout << "Blad podczas kopiowania pliku na dysk wirtualny: Podany plik juz istnieje.\n";
        return -1;
    }
    if (!diskFile.is_open())
        return 1;

    file.seekg(0, fstream::end);
    fileSize = file.tellg();
    file.seekg(0, fstream::beg);
    if (fileSize > this->freeSpace)
    {
        cout << "Brak miejsca na dysku\n";
        cout << "Rozmiar pliku: " << fileSize << "B" << std::endl;
        cout << "Ilosc dostepnego miejsca: " << this->freeSpace << "B" << std::endl;
        return -1;
    }
    file.close();
    fileBlock = saveFile(fileName);
    if (fileBlock <= 0)
    {
        cout << "Nie udalo sie zapisac pliku na dysku\n";
        cout << "saveFile() zwrocil " << fileBlock << "\n";
        return -1;
    }
    int savFAT = saveFATRecord(fileName, fileBlock, fileSize);
    if (savFAT < 0)
    {
        cout << "nie udalo sie zapisac rekordu FAT!\n";
        cout << "saveFATrecord zwrocil: " << savFAT << std::endl;
        exit(1);
    }
    this->filesCount++;
    cout << "\n Plik pomyslnie skopiowano na dysk.\nAktualna ilosc plikow: " << filesCount;
    cout << "\nAktualnie dostepne miejsce: " << freeSpace << "B\n";
}

int FSManager::copyOut(string fileName, string newFile)
{
    cout << "Rozpoczynam kopiowanie pliku " << fileName << " z dysku wirtualnego...\n";
    fstream file;
    char * blockData;
    Block block;
    unsigned int currentBlock = findFile(fileName);
    if (currentBlock == 0)
    {
        cout << "Blad: metoda copyOut(" << fileName << ", " << newFile << ") -> funkcja findFile zwrocila niepoprawny blok.\n";
        return -1;
    }
    diskFile.seekg(sizeof (DiskInfo) + currentBlock * blkSize);
    file.open(newFile.c_str(), fstream::out | fstream::binary);
    if (file.fail() || file.eof())
    {
        cout << "Blad: metoda copyOut(" << fileName << ", " << newFile << ") -> wystapil blad podczas otwarcia pliku do zapisu.\n";
        return -1;
    }
    blockData = new char[blkSize];
    if (!blockData)
    {
        cout << "Blad: metoda copyOut(" << fileName << ", " << newFile << ") -> nie udalo sie zainicjowac wskaznika blockData\n";
        return -1;
    }
    while (1)
    {
        diskFile.seekg(blkSize * currentBlock + sizeof (DiskInfo));

        diskFile.read((char *) & block, sizeof (block));
        diskFile.read(blockData, block.size);
        file.write(blockData, block.size);
        if (block.next == 0)
            break;
        else
            currentBlock = block.next;
    }
    file.close();
    delete[] blockData;
    cout << "Plik pomyślnie skopiowany z dysku wirtualnego.\n\n";
    return 0;

}

FSManager::FSManager(const FSManager& orig)
{
    this->blkSize = orig.blkSize;
    this->dskSize = orig.dskSize;
    this->diskName = orig.diskName;
    this->filesCount = orig.filesCount;
    this->freeSpace = orig.freeSpace;
    diskFile.open(diskName.c_str(), fstream::in | fstream::out | fstream::binary);
}

FSManager::~FSManager()
{
    cout << "Wykonuje sie destruktor...";
    diskFile.seekp(0);
    DiskInfo info;
    info.blockSize = this->blkSize;
    info.checker = 123;
    info.diskSize = this->dskSize;
    info.filesCount = this->filesCount;
    info.freeSpace = this->freeSpace;
    cout << "Zapisuje takie dane: \n";
    this->blkSize = info.blockSize;
    cout << "Rozmiar bloku danych: " << blkSize << "B\n";
    this->dskSize = info.diskSize;
    cout << "Rozmiar dysku: " << dskSize << "B\n";
    this->freeSpace = info.freeSpace;
    cout << "Ilosc wolnego miejsca: " << freeSpace << "B\n";
    this->filesCount = info.filesCount;
    cout << "Ilosc plikow na dysku: " << filesCount << "\n";
    diskFile.write((char *) &info, sizeof(info));
    diskFile.close();
    cout << "Destruktor sie wykonal...";
}

unsigned int FSManager::findFreeBlock(unsigned int current = 1)
{
    if (!diskFile.is_open())
        return 0;
    Block block;
    while (1)
    {
        diskFile.seekg(blkSize * current + sizeof (DiskInfo));
        diskFile.read((char*) & block, sizeof (block));
        if (diskFile.eof() || diskFile.fail())
        {
            diskFile.seekg(0, fstream::beg);
            cout << "findFreeBlock(): blad eof: " << diskFile.eof() << "lub fail: " << diskFile.fail() << "\n";
            cout << "current: " << current << std::endl;
            cout << "blkSize: " << blkSize << std::endl;
            return 0;
        }
        if (diskFile.gcount() < sizeof (block) || block.checker != 123 || block.next == -1)
        {
            diskFile.seekg(0, fstream::beg);
            return current;
        }
        if (block.next >= 0)
        {
            current++;
        }
    }

}

unsigned int FSManager::findFatBlock()
{

    stringstream debug;
    debug << "Wchodze do findFatBlock() \n";
    printDEBUG(debug);
    Block FATBlock;
    if (!diskFile.is_open())
    {
        std::cout << "diskFile nie otwarty w findFatBlock()!\n";
        cout << "fail: " << diskFile.fail();
        cout << "eof: " << diskFile.eof();
        return 0;
    }
    diskFile.seekg(sizeof (DiskInfo));

        debug << "Ustawiam seekg na " << diskFile.tellg() << "\n";
        printDEBUG(debug);
    
    while (1)
    {
        debug << "Wchodze do petli while(1) w findFatBlock\n";
        debug << "Czytam z pozycji " << diskFile.tellg() << "\n";
        printDEBUG(debug);
        diskFile.read((char *) & FATBlock, sizeof (FATBlock));
        if (diskFile.fail() || diskFile.eof())
        {
            std::cout << "fail albo eof w findFatBlock()\n";
            cout << "fail: " << diskFile.fail() << std::endl;
            cout << "eof: " << diskFile.eof() << std::endl;
            cout << "findFatBlock next: " << FATBlock.next << std::endl;
            return 0;
        }
        if (FATBlock.size < (blkSize - sizeof (FileInfo) - sizeof (Block)))
        {
            cout << "jeden fat wystarcza do zapisu... : " << FATBlock.current << "\n";
            return FATBlock.current;
        }
        if (FATBlock.next == 0)
        {
            diskFile.seekp(FATBlock.current * blkSize + sizeof (DiskInfo));
            break;
        } else
            diskFile.seekg(blkSize * FATBlock.next + sizeof (DiskInfo));

    }
    cout << "jeden fat nie wystarczyl\n";
    this->freeSpace -= sizeof (blkSize);
    FATBlock.next = this->findFreeBlock();
    cout << "nastepny: " << FATBlock.next << "\n";
    diskFile.write((char*) & FATBlock, sizeof (FATBlock));
    diskFile.seekp(FATBlock.next * blkSize + sizeof (DiskInfo));
    FATBlock.current = FATBlock.next;
    FATBlock.next = 0;
    FATBlock.size = 0;
    diskFile.write((char*) & FATBlock, sizeof (FATBlock));

    return FATBlock.current;
}

unsigned int FSManager::saveFATRecord(string fileName, unsigned int fileBlock, unsigned long fileSize)
{
    Block FATBlock;
    unsigned int fatBlock = findFatBlock();
    if (!fatBlock)
        return -1;
    diskFile.seekg((fatBlock - 1) * blkSize + sizeof (DiskInfo));
    diskFile.read((char*) & FATBlock, sizeof (FATBlock));
    if (FATBlock.checker != 123)
    {
        cout << "Blad w saveFATRecord! checker\n";
        exit(1);
    }
    diskFile.seekp(blkSize * (fatBlock - 1) + sizeof (FATBlock) + FATBlock.size + sizeof (DiskInfo));
    FileInfo fInfo;
    if (fileName.size() > 63)
        fileName.resize(63);
    strcpy(fInfo.fileName, fileName.c_str());
    fInfo.startBlock = fileBlock;
    fInfo.fileSize = fileSize;
    diskFile.write((char*) & fInfo, sizeof (fInfo));
    FATBlock.size += sizeof (fInfo);
    diskFile.seekp((fatBlock - 1) * blkSize + sizeof (DiskInfo));
    diskFile.write((char*) & FATBlock, sizeof (FATBlock));
    return fatBlock;
}

unsigned int FSManager::saveFile(string filePath)
{
    char * fileContent;
    fstream file;
    if (!diskFile.is_open())
    {
        cout << "\n BLAD: saveFile(): dysk nie jest otwarty";
        return -1;
    }
    file.open(filePath.c_str(), fstream::in | fstream::binary);
    if (file.fail() || file.eof())
    {
        cout << "\n BLAD: saveFile(): wystapil eof lub fail \n";
        return -1;
    }


    fileContent = new char[blkSize + 1];
    int i = 1;
    int bytesRead;
    int freeBlock;
    int firstBlock = findFreeBlock();

    while (1)
    {
        freeBlock = findFreeBlock();
        // cout << "saveFile() : findFreeBlock() zwrocil: " << freeBlock << std::endl;
        this->freeSpace -= blkSize;
        file.read(fileContent, blkSize - sizeof (Block));
        bytesRead = file.gcount();
        // cout << "Odczytalem " << bytesRead << " bajtow z pliku\n";
        //cout << "Ustawiam pozycje " << blkSize << "*" << freeBlock << "+" <<sizeof(DiskInfo) << "= " << blkSize * freeBlock + sizeof (DiskInfo) << "\n";
        diskFile.seekp(blkSize * freeBlock + sizeof (DiskInfo));
        // cout << "ustawilo sie " << diskFile.tellp() << "\n";
        Block block;
        block.current = freeBlock;
        if (bytesRead != (blkSize - sizeof (Block)))
        {
            // cout << "dane zmieszcza sie w jednym pliku\n";
            block.next = 0;
            block.size = bytesRead;
            //cout << "Pozycja przed zapisem: " <<diskFile.tellp() << "\n";
            diskFile.write((char *) & block, sizeof (block));
            //cout << "Pozycja przed zapisem: " <<diskFile.tellp() << "\n";
            diskFile.write(fileContent, bytesRead);
            break;
        } else
        {
            block.next = findFreeBlock(freeBlock + 1);
            //        cout << "saveFile() : findFreeBlock() zwrocil: " << block.next << std::endl;
            block.size = bytesRead;
            diskFile.seekp(blkSize * freeBlock + sizeof (DiskInfo));
            //cout << "Pozycja przed zapisem: " <<diskFile.tellp() << "\n";

            diskFile.write((char *) & block, sizeof (block));
            // cout << "Pozycja przed zapisem: " <<diskFile.tellp() << "\n";
            diskFile.write(fileContent, bytesRead);
        }
        i++;
    }
    file.close();
    delete[] fileContent;
    return firstBlock;
}

unsigned int FSManager::findFile(string fileName)
{
    cout << "Szukam pliku na dysku...\n";
    Block FATBlock;
    FileInfo fInfo;
    if (fileName.size() > 63)
        fileName.resize(63);
    if (!diskFile.is_open())
    {
        cout << "Problem z diskFile w findFIle()!!!\n";
        return 0;
    }
    diskFile.seekg(sizeof (DiskInfo));
    while (1)
    {
        // cout << "Poczatek petli while()\n";
        diskFile.read((char *) & FATBlock, sizeof (FATBlock));
        if (diskFile.fail() || diskFile.eof())
        {
            //  cout << "Wystapil blad szukania pliku na dysku (zewnetrzny while)\n";
            return 0;
        }
        for (int i = 0; i < FATBlock.size; i += sizeof (FileInfo))
        {
            //cout << "wchodzimy do petli for w findFile()\n";
            diskFile.read((char*) & fInfo, sizeof (FileInfo));
            if (diskFile.fail() || diskFile.eof())
            {
                cout << "Wystapil blad szukania pliku na dysku (wewnetrzna petla for)\n";
                return 0;
            }
            if (!strcmp(fInfo.fileName, fileName.c_str()))
            {
                // cout << "Znalazlem plik w fat recordach o indeksie: " << fInfo.startBlock << std::endl;
                return fInfo.startBlock;
            }

        }
        if (FATBlock.next == 0)
        {
            return 0;
        } else
            diskFile.seekg(blkSize * FATBlock.next + sizeof (DiskInfo));
    }
    return 0;
}

int FSManager::remove(string fileName)
{
    Block block;
    cout << "Rozpoczynam usuwanie pliku " << fileName << " ...\n";
    unsigned long currentBlock = removeFATrecord(fileName);
    int nextvalue;
    if (!currentBlock)
    {
        cout << "Blad: Plik podany do usuniecia nie istnieje na dysku\n";
        return -1;
    }
    while (1)
    {
        diskFile.seekg(blkSize * currentBlock + sizeof (DiskInfo));

        diskFile.read((char *) & block, sizeof (Block));
        if (diskFile.eof() || diskFile.fail())
        {
            cout << "Wystapil blad podczas usuwania pliku " << fileName << "\n";
            cout << "Wartosc bitu eof: " << diskFile.eof() << "\n";
            cout << "wartosc bitu fail: " << diskFile.fail() << "\n";
            return -1;
        }
        nextvalue = block.next;
        freeSpace += blkSize;
        block.size = 0;
        block.next = -1;
        diskFile.seekp(blkSize * currentBlock + sizeof (DiskInfo));
        diskFile.write((char *) & block, sizeof (block));
        if (nextvalue == 0)
            break;
        else
            currentBlock = nextvalue;
    }
    filesCount--;
    cout << "Plik " << fileName << " zostal pomyslnie usuniety z dysku\n";
    cout << "\nAktualna ilosc plikow: " << filesCount;
    cout << "\nAktualnie dostepne miejsce: " << freeSpace << "B\n";
    return 0;
}

int FSManager::removeFATrecord(string fileName)
{
    cout << "Usuwam rekord z tablicy FAT...\n";
    Block FATBlock;
    FileInfo fInfo;
    fstream::streampos fatblockpos, temppos;
    char * temp;
    if (fileName.size() > 63)
        fileName.resize(63);
    if (!diskFile.is_open())
    {
        cout << "Problem z diskFile w removeFATrecord()!!!\n";
        return 0;
    }
    diskFile.seekg(sizeof (DiskInfo));
    while (1)
    {
        fatblockpos = diskFile.tellg();
        diskFile.read((char *) & FATBlock, sizeof (FATBlock));
        if (diskFile.fail() || diskFile.eof())
        {
            cout << "Wystapil blad szukania pliku na dysku (zewnetrzny while)\n";
            return 0;
        }

        for (unsigned int i = 0; i < FATBlock.size; i += sizeof (FileInfo))
        {
            //      cout << "wchodzimy do petli for w findFile()\n";
            temppos = diskFile.tellg();
            diskFile.read((char*) & fInfo, sizeof (FileInfo));
            if (diskFile.fail() || diskFile.eof())
            {
                cout << "Wystapil blad szukania pliku na dysku (wewnetrzna petla for)\n";
                return 0;
            }
            if (!strcmp(fInfo.fileName, fileName.c_str()))
            {
                if (FATBlock.size == i + sizeof (FileInfo))
                {

                    FATBlock.size -= sizeof (FileInfo);
                    diskFile.seekp(fatblockpos);
                    diskFile.write((char *) & FATBlock, sizeof (FATBlock));
                } else
                {

                    temp = new char[FATBlock.size - i + sizeof (FileInfo)];
                    diskFile.read(temp, FATBlock.size - i + sizeof (FileInfo));
                    diskFile.seekp(temppos);
                    diskFile.write(temp, FATBlock.size - i + sizeof (FileInfo));
                    delete temp;
                    diskFile.seekp(fatblockpos);
                    FATBlock.size -= sizeof (FileInfo);
                    ;
                    diskFile.write((char *) & FATBlock, sizeof (FATBlock));
                }
                cout << "Usunieto rekord z tablicy FAT dotyczacy pliku " << fileName << "\n";
                return fInfo.startBlock;
            }

        }
        if (FATBlock.next == 0)
        {
            cout << "Brak pliku o podanej nazwie\n";
            return 0;
        } else
            diskFile.seekg(blkSize * FATBlock.next + sizeof (DiskInfo));
    }

    return 0;
}

string FSManager::showStructure()
{
    Block block;
    stringstream s;
    diskFile.seekg(sizeof (DiskInfo));
    int i = 1;
    while (!diskFile.eof())
    {
        diskFile.read((char*) & block, sizeof (block));
        if (block.checker == 123 && block.size > 0)
        {

            s << "[" << block.current << " " << block.size + sizeof(Block) << "B " << block.next << "]";

        } else
        {
            s << "[X]";
        }
        if (!(i % 10))
        {
            s << "\n";
        }
        diskFile.seekg(i * blkSize + sizeof (DiskInfo));
        i++;
    }
    diskFile.close();
    diskFile.open(this->diskName.c_str(), fstream::in | fstream::out | fstream::binary);
    return s.str();
}

string FSManager::showFATTable()
{
    stringstream s;
    Block FATBlock;
    FileInfo fInfo;
    if (!diskFile.is_open())
    {
        cout << "Problem z diskFile w showFATTable()!!!\n";
        return "";
    }

    int counter = 1;
    diskFile.seekg(sizeof (DiskInfo));
    while (1)
    {
        diskFile.read((char *) & FATBlock, sizeof (FATBlock));
        if (diskFile.fail() || diskFile.eof())
        {
            cout << "Wystapil blad szukania pliku na dysku (zewnetrzny while)\n";
            cout << "tellg(): " << diskFile.tellg() << "\n";
            return "";
        }
        for (int i = 0; i < FATBlock.size; i += sizeof (FileInfo))
        {
            diskFile.read((char*) & fInfo, sizeof (FileInfo));
            if (diskFile.fail() || diskFile.eof())
            {
                cout << "Wystapil blad szukania pliku na dysku (wewnetrzna petla for)\n";
                return " ";
            }
            s << counter << ": " << fInfo.fileName << "  Size: " << fInfo.fileSize << "B  starting block: " << fInfo.startBlock;
            s << "  FATBlock: " << FATBlock.current << "\n";
            counter++;

        }
        if (FATBlock.next == 0)
        {
            return s.str();
        } else
            diskFile.seekg(blkSize * FATBlock.next + sizeof (DiskInfo));
    }
    return s.str();
}

string FSManager::showInfo()
{
    stringstream s;
    s << "Disk size: " << this->dskSize << "B" << std::endl;
    s << "Free space: " << this->freeSpace << "B" << std::endl;
    s << "Number of files: " << this->filesCount << std::endl;
    s << "Block size: " << this->blkSize << "B" << std::endl;
    return s.str();
}

