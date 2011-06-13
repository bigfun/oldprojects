/* 
 * File:   FSManager.h
 * Author: bigfun
 *
 * Created on 10 styczeń 2010, 18:37
 */

#ifndef _FSMANAGER_H
#define	_FSMANAGER_H
#define KB 1024
#include <string>
#include <fstream>
#include <iostream>
#include <cstdlib>
#include <cstring>
#include <sstream>
using std::string;
using std::fstream;
using std::cout;
using std::cin;
using std::strcpy;
using std::stringstream;

struct DiskInfo 
{
    unsigned int diskSize;
    unsigned int blockSize;
    unsigned int freeSpace;
    unsigned int filesCount;
    int checker;
};

struct Block
{
    int next;
    int current;
    unsigned int size;
    int checker;
    Block()
    {
        checker = 123;
    }
};
struct FileInfo
{
    unsigned int startBlock;
    char fileName[64];
    unsigned long fileSize;
    int checker;
    FileInfo()
    {
        checker =123;
    }
};


class FSManager
{
public:
    FSManager(string fileName /* = "disk.vdi" */, unsigned int blockSize /* = 2 */, unsigned int discSize /* = 4096 */);
    FSManager(const FSManager& orig);
    int remove(string fileName);
    int copyIn(string filePath);
    int copyOut(string fileName, string newFile);
    string showStructure();
    string showFATTable();
    string showInfo();
    virtual ~FSManager();
private:
    unsigned int findFreeBlock(unsigned int current);
    unsigned int findFatBlock();
    unsigned int saveFile(string filePath);
    unsigned int saveFATRecord(string fileName, unsigned int fileBlock, unsigned long fileSize);
    unsigned int findFile(string fileName);
    int removeFATrecord(string fileName);
    fstream diskFile;
    string diskName;
    unsigned int blkSize;
    unsigned int dskSize;
    unsigned int freeSpace;
    int filesCount;

};

#endif	/* _FSMANAGER_H */

