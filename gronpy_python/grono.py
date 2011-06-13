#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
# Author:
#   Karol "bigfun" Stępniewski, bigfunlx@gmail.com 
#
# Under the terms of the GNU General Public License

import urllib, urllib2, gzip, StringIO, cookielib
from xml.etree import ElementTree as ET


def set_global_cookies():
    openers = []
    cookiejar = cookielib.LWPCookieJar()
    openers.append(urllib2.HTTPCookieProcessor(cookiejar))
    urlopener = urllib2.build_opener(*openers)
    urllib2.install_opener(urlopener)
    
class GeneralError(Exception):
    pass

class LoginError(GeneralError):
    pass

class LimitError(GeneralError):
    pass


class grono(object):
    _mainurl = 'http://api.grono.net/'
    headers = {"Host" : "api.grono.net",
               "User-Agent" : "gronpy/0.1",
               "Accept" : "text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5",
               "Accept-Language" : "pl,en-us;q=0.7,en;q=0.3",
               "Accept-Encoding": "gzip",
               "Keep-Alive" : "300",
               "Connection" : "keep-alive",
               "Content-Type" : "application/x-www-form-urlencoded; charset=UTF-8"}
    mtn = 0

    def decode(self,strx, encoding='utf-8'):   
        if isinstance(strx, str):
            return(strx.decode(encoding))
        return(strx)
	
    def encode(self,strx, encoding='utf-8'):
        return(self.decode(strx, encoding).encode(encoding))
    
    def geturl(self,request):
        try:
            response = urllib2.urlopen(request)
            return response
        except urllib2.HTTPError, error:
            if error.code == 403:
                raise LoginError('Not authorized!')
            if error.code == 404:
                raise GeneralError(' User not found!')
            if error.code == 400:
                raise GeneralError('Bad Request!')
        
    def login(self, username, password, client = 'gronpy', lang='pl', version = '0.1', premium = 0):
        login_data = urllib.urlencode({"client" : client, "cliver" : version, "lang" : lang, \
                                       "login" : username,"password" : password,"premium" : premium})
        request = urllib2.Request(self._mainurl+"pub/apilogin/",login_data,self.headers)
        response = urllib2.urlopen(request)
        compressedstream = StringIO.StringIO(response.read())
        gzipper = gzip.GzipFile(fileobj=compressedstream)
        data = gzipper.read()
        data = "<xml>%s</xml>" % data
        tree = ET.XML(data)
        status_value = int(tree[0].attrib["value"])
        if status_value==0:
            pass
        elif status_value in range(8, 10):
            raise LimitError
        elif status_value==10:
            mtn = 1
        elif status_value==1:
            raise LoginError
        elif status_value in range(2, 5):
            raise GeneralError
        if tree[2] is not None:
            self.id = tree[2].text
    def message_get(self):
        messages = []
        request = urllib2.Request(self._mainurl+"api/messages/%s/?format=XML" % self.id,None,self.headers)
        response=self.geturl(request)
        compressedstream = StringIO.StringIO(response.read())
        gzipper = gzip.GzipFile(fileobj=compressedstream)
        data = gzipper.read()
        tree = ET.XML(data)
        tree = tree[0]
        
        for element in tree:
            dict = {"status" : element[0].text, "delivery" : element[1].text, \
                    "flag" : element[2].text, \
                    "sender_scrname" : element[3].text, "url" : element[4].text, \
                    "recipient_csrname" : element[5].text,\
                    "sender_id" : element[6].text,"subject" : element[7].text }
 
            messages.append(dict)
        return messages

    def message_send(self, recipient_id, subject, text):
        message_data = urllib.urlencode({"subject" : subject, "text" : text})
        request = urllib2.Request(self._mainurl+"api/messages/%s/" % recipient_id,message_data,self.headers)
        self.geturl(request)
        
    def gallery_get(self, gallery_id):
        dict={}
        request = urllib2.Request(self._mainurl+"api/gallery/%s/?format=XML" % gallery_id,None,self.headers)
        response=self.geturl(request)
        compressedstream = StringIO.StringIO(response.read())
        gzipper = gzip.GzipFile(fileobj=compressedstream)
        data = gzipper.read()
        tree = ET.XML(data)
        dict["description"]=tree[0].text
        dict["title"]=tree[1].text
        dict["thumbnail_url"]=tree[3].text
        dict["id"]=tree[4].text
        dict["owner_id"]=tree[5].text
        tree = tree[2]
        dict["photos"]=[]
        for element in tree:
            dict["photos"].append(element.text)
        return dict

    def gallerylist_get(self, owner_id):
        galleries = []
        request = urllib2.Request(self._mainurl+"api/galleries/%s/?format=XML" % owner_id,None,self.headers)
        response=self.geturl(request)
        compressedstream = StringIO.StringIO(response.read())
        gzipper = gzip.GzipFile(fileobj=compressedstream)
        data = gzipper.read()
        tree = ET.XML(data)
        tree = tree[0]
        for element in tree:
            dict = { "url" : element[0].text, "thumbnail_url":element[1].text}
            galleries.append(dict)
        return galleries
    
    def gallery_delete(self, gallery_id):
        delete_data=urllib.urlencode({"delete": 1})
        request = urllib2.Request(self._mainurl+"api/gallery/%s/" % gallery_id,delete_data,self.headers)
        response=self.geturl(request)
        
    def gallery_edit(self, gallery_id, name, description=''):
        edit_data=urllib.urlencode({"name": name, "description" : description})
        request = urllib2.Request(self._mainurl+"api/gallery/%s/" % gallery_id,edit_data,self.headers)
        response=self.geturl(request)
        
    def gallery_upload(self,photo_data,gallery_id, name, description=''):
        data = photo_data.read()
        upload_data=urllib.urlencode({"name": name, "description" : description, "data": data})
        request = urllib2.Request(self._mainurl+"api/gallery/%s/" % gallery_id,upload_data,self.headers)
        request.add_header('Content-type', "multipart/form-data")
        response=self.geturl(request)
        if hasattr(response, 'headers'):
            return response.headers.get('Location')
        else:
            return 1

    def photo_get(self, photo_id):
        dict = {}
        request = urllib2.Request(self._mainurl+"api/photo/%s/?format=XML" % photo_id,None,self.headers)
        response=self.geturl(request)
        compressedstream = StringIO.StringIO(response.read())
        gzipper = gzip.GzipFile(fileobj=compressedstream)
        data = gzipper.read()
        tree = ET.XML(data)
        dict={"map_alt" : tree[0].text, "description" : tree[1].text, "title" : tree[2].text, "orig_url":tree[3].text,\
              "exif_camera":tree[4].text, "map_lng":tree[5].text, "thumbnail_url": tree[6].text, "image_url":tree[7].text,\
              "map_zl":tree[8].text, "map_lat":tree[9].text, "owner_id":tree[10].text}
        return dict
    
    def photo_edit(self,photo_id, name, description=''):
        photo_data=urllib.urlencode({"name": name, "description" : description})
        request = urllib2.Request(self._mainurl+"api/photo/%s/" % photo_id,None,self.headers)
        response=self.geturl(request)
        
    def photo_delete(self, photo_id):
        photo_data=urllib.urlencode({"delete": 1})
        request = urllib2.Request(self._mainurl+"api/photo/%s/" % photo_id,None,self.headers)
        response=self.geturl(request)
        
    def gallery_create(self,owner_id,title, description=''):
        photo_data=urllib.urlencode({"title": title, "description" : description})
        request = urllib2.Request(self._mainurl+"api/galleries/%s/" % owner_id,None,self.headers)
        response=self.geturl(request)  
    def limit_get(self):
        pass
    
    def avatar_get(self):
        pass
    def avatar_send(self):
        pass
    def avatar_delete(self):
        pass

    def lastphotos_get(self):
        pass
    def friends_get(self):
        pass
    def grouplist_get(self):
        pass
    def group_get(self):
        pass
    def favorites_get(self):
        pass
    def pubcomm_get(self):
        pass
    def pubcomm_edit(self):
        pass
    def forum_get(self):
        pass
    def topic_send(self):
        pass
    def topic_get(self):
        pass
    def post_send(self):
        pass
    def blimp_get(self):
        pass
    def blimp_set(self):
        pass
    def profile_get(self):
        pass
    def profile_edit(self):
        pass
    
        


if __name__ == "__main__":
    set_global_cookies()
    obiekt = grono()
    obiekt.login( u"admin@example.com" , u"xxxxxxxx")
    print obiekt.message_get()
