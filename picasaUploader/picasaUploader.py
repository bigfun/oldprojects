#!/usr/bin/env python
# -*- coding: utf-8 -*-
#
# Authors:
#   Marcin `Walker` Pośpiech, marcin.pospiech [at] gmail [dot] com
#   Karol `Bigfun` Stępniewski, bigfunlx [at] gmail [dot] com
#
# 
# A Python's script with Graphic User Interface (GUI) to upload pictures to PicasaWeb.
#
#
# Under the terms of the GNU General Public License
#
#

import gdata.service
import gdata
import atom
import gdata.base
import sys, os
import pygtk
pygtk.require('2.0')
import gtk


#### MAIN CLASS
class Uploader:	
	def __init__(self):
		
		# list of files to upload
		self.fileList=[]
		self.count=0
		
### INTERFACE
		
        	# main window
        	self.window = gtk.Window(gtk.WINDOW_TOPLEVEL)
        	self.window.set_title("PicasaUploader")
		self.window.connect("delete_event", self.delete_event)
		self.window.connect("destroy", self.destroy)
		self.window.set_border_width(5)
		
		# main VBox
		main_VBox = gtk.VBox(False, 10)
		self.window.add(main_VBox)
		

		# options HBox - contains everything except statusbar
		options_HBox = gtk.HBox(False, 10)
		main_VBox.pack_start(options_HBox)
		
		# data VBox - contains data form and some buttons
		data_VBox = gtk.VBox()
		options_HBox.pack_start(data_VBox)
		

		# USERNAME
		# username HBox
		username_HBox = gtk.HBox()
		data_VBox.pack_start(username_HBox)
		
		# label for username
		username_label = gtk.Label()
		username_label.set_text("Username:")
		username_label.show()
		username_HBox.pack_start(username_label)
		
		# username entry
		self.username = gtk.Entry(25)
		self.username.show()
		username_HBox.pack_start(self.username)

			
		# PASSWORD
		# password HBox
		password_HBox = gtk.HBox()
		data_VBox.pack_start(password_HBox)
		
	        # label for password
		password_label = gtk.Label()
		password_label.set_text("Password:")
		password_label.show()
		password_HBox.pack_start(password_label)
		
		# password entry
		self.password = gtk.Entry(30)
		self.password.set_visibility(False)
	        self.password.show()
	        password_HBox.pack_start(self.password)


		# ALBUM NAME
		# album name HBox
		album_name_HBox = gtk.HBox()
		data_VBox.pack_start(album_name_HBox)
		
		# label for album name
		album_name_label = gtk.Label()
		album_name_label.set_text("Album name:")
		album_name_label.show()
		album_name_HBox.pack_start(album_name_label)
	
		# album name entry
		self.album_name = gtk.Entry(30)
		self.album_name.show()
		album_name_HBox.pack_start(self.album_name)
		
		# ALBUM CHOOSING
		# album choosing HBox
		album_choosing_HBox = gtk.HBox()
		data_VBox.pack_start(album_choosing_HBox)
		
		# label for album choosing
		album_choosing_label = gtk.Label()
		album_choosing_label.set_text("Choose album:")
		album_choosing_label.show()
		album_choosing_HBox.pack_start(album_choosing_label)
		
		# combobox for album choosing
		self.album_choosing_combo = gtk.combo_box_new_text()
		self.album_choosing_combo.show()
		album_choosing_HBox.pack_start(self.album_choosing_combo)
			
		
		#button to create a new album 
		
		create_button = gtk.Button("Create a new album")
		create_button.connect("clicked", self.create_album)
		create_button.show()
		data_VBox.pack_start(create_button)

		# button to choose files (in data VBox)
		select_button = gtk.Button("Choose files")
		select_button.connect("clicked", self.choose_file)
		select_button.show()
		data_VBox.pack_start(select_button)
		
		# button to connect and upload files (in data VBox)
		self.upload_button = gtk.Button("Connect")
		#self.upload_button = gtk.Button("Upload files")
		self.handler=self.upload_button.connect("clicked", self.picasa_connect)
		self.upload_button.set_sensitive(True)
		self.upload_button.show()
		data_VBox.pack_start(self.upload_button)

		# button to delete selected image (in data VBox)
		self.delete_button = gtk.Button("Delete selected from list")
		self.delete_button.connect("clicked", self.delete_image)
		self.delete_button.set_sensitive(False)
		self.delete_button.show()
		data_VBox.pack_start(self.delete_button)
		
		# button to clear the file list (in data VBox)
		self.clear_button = gtk.Button("Clear file list")
		self.clear_button.connect("clicked", self.clear_list)
		self.clear_button.set_sensitive(False)
		self.clear_button.set_size_request(-1, -1)
		self.clear_button.show()
		data_VBox.pack_start(self.clear_button) 
		
		# image files container and a scrolled window for it (in options HBox)
		scrolled_window = gtk.ScrolledWindow()
		scrolled_window.set_policy(gtk.POLICY_AUTOMATIC, gtk.POLICY_AUTOMATIC)
		scrolled_window.show()
		options_HBox.pack_start(scrolled_window)
		self.imagelist=gtk.ListStore(str)
		self.image_container = gtk.TreeView(self.imagelist)
		self.image_container.connect("cursor-changed", self.change_file)
		self.image_container.show()
		self.tvcolumn = gtk.TreeViewColumn('Files')
		self.image_container.append_column(self.tvcolumn)
		self.cell = gtk.CellRendererText()
		self.tvcolumn.pack_start(self.cell,True)
		self.tvcolumn.add_attribute(self.cell, 'text',0)
		self.image_container.set_search_column(0)
		self.tvcolumn.set_sort_column_id(0)
		self.image_container.set_reorderable(False)
		self.image_container.set_size_request(250, 0)
		scrolled_window.add(self.image_container)
		
		# image preview (in options HBox)
		self.image = gtk.Image()
		self.image.set_size_request(300, 300)
		self.image.show()
		options_HBox.pack_start(self.image)
	            	
		# statusbar (in main VBox)
		self.statusbar = gtk.Statusbar()
		self.context_id = self.statusbar.get_context_id("Statusbar")
		self.statusbar.show()
		main_VBox.pack_start(self.statusbar)

		self.window.show_all()
	
	
###	METHODS
	
	# main function	
	def main(self):
        	gtk.main()
		
	#method to retrieve album's names	
	def retrieve_albums(self):
		self.statusbar.push(self.context_id, "Retrieving albums names...")
		self.picasa_client.server = 'picasaweb.google.com'
		self.feed=self.picasa_client.GetFeed("/data/feed/api/user/"+self.username.get_text()+"?kind=album")
		if self.feed is None:
			self.statusbar.push(self.context_id, "Error, unable to retrieve the data!")
    		else:
			for a in range(self.count,-1,-1):
				self.album_choosing_combo.remove_text(a)
			self.album_list=[]
			for i in self.feed.entry:
				self.count+=1
				self.album_choosing_combo.append_text(i.title.text)	
			self.statusbar.push(self.context_id, "Album's names retrieved")	
			
			
	# method to create new album
	def create_album(self,widget,data=None):
		if  self.album_name.get_text() == "":	
        		self.statusbar.push(self.context_id, "You must specify the album name!")
		else:
				self.statusbar.push(self.context_id, "Creating the album...")
				picasa_entry = gdata.GDataEntry()
				picasa_entry.title = atom.Title(text=self.album_name.get_text())
				picasa_entry.category.append(atom.Category(scheme='http://schemas.google.com/g/2005#kind', term='http://schemas.google.com/photos/2007#album'))
				try:
					album_entry = self.picasa_client.Post(picasa_entry, 'http://picasaweb.google.com/data/feed/api/user/' + self.username.get_text())
				except gdata.service.RequestError:
					self.statusbar.push(self.context_id, "Unexpected error!")
				else:
					self.statusbar.push(self.context_id, "Album %s created" % self.album_name.get_text())
					self.retrieve_albums()
				

		
	# method to connect and retreive data from picasaweb
	def picasa_connect(self,widget,data=None):
		if self.username.get_text() == "" or self.password.get_text() == "":
			self.statusbar.push(self.context_id, "You must specify your username and password")
		else:
			self.picasa_client = gdata.service.GDataService()
			self.picasa_client.email = self.username.get_text()
			self.picasa_client.password = self.password.get_text()
			self.picasa_client.service = 'lh2'
			self.picasa_client.source = ' PicasaUploader'
			self.statusbar.push(self.context_id, "Connecting...")
			try:
				self.picasa_client.ProgrammaticLogin()
			except gdata.service.CaptchaRequired:
				self.statusbar.push(self.context_id, "Required Captcha!")
			except gdata.service.BadAuthentication:
			        self.statusbar.push(self.context_id, "Bad Authentication!")
			except gdata.service.Error:
			        self.statusbar.push(self.context_id, "Login Error!")
			else:
				self.statusbar.push(self.context_id, "Connected")
				self.retrieve_albums()
				self.upload_button.set_label("Upload Files")
				self.upload_button.disconnect(self.handler)
				self.upload_button.connect("clicked", self.upload_files)	
				if self.fileList == []:
					self.upload_button.set_sensitive(False)

	 # method to upload files 	
	def upload_files(self, widget, data=None):
     			active = self.album_choosing_combo.get_active()
      			if active < 0:
          			self.statusbar.push(self.context_id, "You haven't selected an album!")
			else:		
				ms = gdata.MediaSource()
				for file in self.fileList:
					try:
						self.statusbar.push(self.context_id, "Sending file...")
						ms.setFile(file, 'image/jpeg')
						media_entry = self.picasa_client.Post(None,self.feed.entry[active].link[0].href,media_source=ms)
					except gdata.service.RequestError:
						self.statusbar.push(self.context_id, "File sending failed!")
					else:
						self.statusbar.push(self.context_id, "Files sent!")
						self.imagelist.clear()
						self.fileList=[]
						self.Files=()
						self.image.clear()
						self.upload_button.set_sensitive(False)
						self.delete_button.set_sensitive(False)
						self.clear_button.set_sensitive(False)

	# methods to close the application
	def delete_event(self, widget, event, data=None):
		gtk.main_quit()

	def destroy(self, widget, data=None):
		gtk.main_quit()

	# method to clear the file list	
	def clear_list(self, widget, data=None):
		self.imagelist.clear()
		self.fileList=[]
		self.Files=()
		self.upload_button.set_sensitive(False)
		self.delete_button.set_sensitive(False)
		self.clear_button.set_sensitive(False)
		self.image.clear()
		self.statusbar.push(self.context_id, "List cleared!")
		
	# method to change the image preview	
	def change_file(self, data=None):	
		self.set_image(self.imagelist.get_value(self.imagelist.get_iter(self.image_container.get_cursor()[0]),0))

	# image setting method	
	def set_image(self,pathfile):
		pixbuf = gtk.gdk.pixbuf_new_from_file(pathfile)
		height=pixbuf.get_height()
		width=pixbuf.get_width()
		if height > width and height >250:
			width = float(width)/float(height)
			height = 250
			scaled_buf = pixbuf.scale_simple(int(width*height),height,gtk.gdk.INTERP_BILINEAR)
		elif height <= width and width > 250:
			height=float(height)/float(width)
			width=250
			scaled_buf = pixbuf.scale_simple(width,int(width*height),gtk.gdk.INTERP_BILINEAR)
		else:
			scaled_buf = pixbuf
		self.image.set_from_pixbuf(scaled_buf)
		self.image.show()

	# method to choose files to upload	
	def choose_file(self, widget, data=None):
		choose=gtk.FileChooserDialog("Select files to upload: ", None, gtk.FILE_CHOOSER_ACTION_OPEN,(gtk.STOCK_CANCEL, gtk.RESPONSE_CANCEL,gtk.STOCK_OPEN, gtk.RESPONSE_OK))  # dialog
		choose.set_default_response(gtk.RESPONSE_OK)
		filter = gtk.FileFilter()
		filter.set_name("Images")
		filter.add_mime_type("image/png")
		filter.add_mime_type("image/jpeg")
		filter.add_mime_type("image/gif")
		filter.add_pattern("*.png")
		filter.add_pattern("*.jpg")
		filter.add_pattern("*.gif")
		choose.add_filter(filter)
		choose.set_select_multiple(True)
		response = choose.run()
   		if response == gtk.RESPONSE_OK:
			self.Files= choose.get_filenames()
			choose.destroy()
			self.set_image(self.Files[0])
			for file in self.Files :
				self.imagelist.append([file])
				self.fileList.append(file)
			self.upload_button.set_sensitive(True)
			self.clear_button.set_sensitive(True)
			self.delete_button.set_sensitive(True)
			self.statusbar.push(self.context_id, "Image(s) added!")
		elif response == gtk.RESPONSE_CANCEL:
			choose.destroy()

        # method to delete selected image
	def delete_image(self, widget, data=None):
	        rows = self.image_container.get_selection().get_selected_rows()[1][0]
	        row = self.imagelist.get_value(self.imagelist.get_iter(rows),0)
                self.imagelist.remove(self.imagelist.get_iter(rows))
	        self.fileList.remove(row)
	        self.image.clear()
	        self.statusbar.push(self.context_id, "Image %s deleted from list!" % row)
	        if self.fileList == []:
			self.upload_button.set_sensitive(False)
			self.clear_button.set_sensitive(False)
			self.delete_button.set_sensitive(False)


if  __name__ == "__main__":
	picasaUploader = Uploader()
        picasaUploader.main()
