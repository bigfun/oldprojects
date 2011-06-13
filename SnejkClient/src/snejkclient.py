import sys
from PyQt4 import QtCore, QtGui, QtNetwork
from avahi import txt_array_to_string_array
from ui_mainwindow import Ui_MainWindow
from random import *
from time import *
from collections import deque
from keywords import *
class SnejkClient(QtGui.QMainWindow, Ui_MainWindow):

	temp = QtCore.pyqtSignal('int','QString', 'int')
	def __init__(self,width,height):
		QtGui.QMainWindow.__init__(self)
		self.setupUi(self)
		seed()
		self.width = width
		self.height = height
		self.initGUI()
		self.initGameElements()
		self.address = ""
		self.port = 0
		self.socket = ""
		self.playerid = 0
		self.currentMovingDirection = "right"
	def initGUI(self):
		self.setWindowIcon(QtGui.QIcon(':/apple'))
		self.btnExit.clicked.connect(self.exit)

		self.arena.resize(self.width*20,self.height*20)
		self.arenawidth = self.width*20
		self.arenaheight = self.height*20
		self.btnConnect.clicked.connect(self.connect);
		self.inputAddress.setText( "192.168.1.1")
		self.inputPort.setText("31337")
	def initGameElements(self):
		self.isPlaying = 0
		self.heads = []		# list for images of snake's heads
		self.bodies = []
		self.userKeys = deque() # queue for user keys
		for i in range(0,4,1):
			self.heads.append("")
			self.heads[i] = []
			self.bodies.append("")
			for j in range(0,4,1):
				self.heads[i] = {}
				self.bodies[i] = {}
		self.heads[0]['left'] = QtGui.QPixmap(":/yellow_snake/head-left")
		self.heads[0]['right'] = QtGui.QPixmap(":/yellow_snake/head-right")
		self.heads[0]['up'] = QtGui.QPixmap(":/yellow_snake/head-up")
		self.heads[0]['down'] = QtGui.QPixmap(":/yellow_snake/head-down")
		self.bodies[0]['left'] = QtGui.QPixmap(":/yellow_snake/body-left")
		self.bodies[0]['right'] = QtGui.QPixmap(":/yellow_snake/body-right")
		self.bodies[0]['up'] = QtGui.QPixmap(":/yellow_snake/body-up")
		self.bodies[0]['down'] = QtGui.QPixmap(":/yellow_snake/body-down")

		self.heads[1]['left'] = QtGui.QPixmap(":/green_snake/head-left")
		self.heads[1]['right'] = QtGui.QPixmap(":/green_snake/head-right")
		self.heads[1]['up'] = QtGui.QPixmap(":/green_snake/head-up")
		self.heads[1]['down'] = QtGui.QPixmap(":/green_snake/head-down")
		self.bodies[1]['left'] = QtGui.QPixmap(":/green_snake/body-left")
		self.bodies[1]['right'] = QtGui.QPixmap(":/green_snake/body-right")
		self.bodies[1]['up'] = QtGui.QPixmap(":/green_snake/body-up")
		self.bodies[1]['down'] = QtGui.QPixmap(":/green_snake/body-down")

		self.heads[2]['left'] = QtGui.QPixmap(":/red_snake/head-left")
		self.heads[2]['right'] = QtGui.QPixmap(":/red_snake/head-right")
		self.heads[2]['up'] = QtGui.QPixmap(":/red_snake/head-up")
		self.heads[2]['down'] = QtGui.QPixmap(":/red_snake/head-down")
		self.bodies[2]['left'] = QtGui.QPixmap(":/red_snake/bodyi-left")
		self.bodies[2]['right'] = QtGui.QPixmap(":/red_snake/body-right")
		self.bodies[2]['up'] = QtGui.QPixmap(":/red_snake/body-up")
		self.bodies[2]['down'] = QtGui.QPixmap(":/red_snake/body-down")

		self.heads[3]['left'] = QtGui.QPixmap(":/blue_snake/head-left")
		self.heads[3]['right'] = QtGui.QPixmap(":/blue_snake/head-right")
		self.heads[3]['up'] = QtGui.QPixmap(":/blue_snake/head-up")
		self.heads[3]['down'] = QtGui.QPixmap(":/blue_snake/head-down")
		self.bodies[3]['left'] = QtGui.QPixmap(":/blue_snake/bodyi-left")
		self.bodies[3]['right'] = QtGui.QPixmap(":/blue_snake/body-right")
		self.bodies[3]['up'] = QtGui.QPixmap(":/blue_snake/body-up")
		self.bodies[3]['down'] = QtGui.QPixmap(":/blue_snake/body-down")
		self.timer = QtCore.QBasicTimer()

		self.timer2 = QtCore.QTimer()
		#self.timer2.timeout.connect(self.countdown)
		self.countdowncounter = 3
		self.snakes = []
		for i in range(4):
			self.snakes.append("")
			self.snakes[i] = []
			for j in range(2):
				self.snakes[i].append("")
		self.fruitposx = 0
		self.fruitposy = 0
		self.fruit = QtGui.QLabel(self.arena)
		self.fruit.setStyleSheet("QLabel { background-color: rgb(189, 196, 255); }")
		self.fruit.hide()

	def initPlayers(self, playercount):
		self.isPlaying = 1;
		for i in range(playercount ):
			self.setSnake(i, 5 + 2*i, 5+2*i)

	def connect(self):
		self.address = QtNetwork.QHostAddress(self.inputAddress.text())
		self.port = int(self.inputPort.text())
		self.socket = QtNetwork.QUdpSocket()
		if  self.send(PLAYER_READY) < 0:
			self.log("ERROR: ", "NIE UDALO SIE WYSLAC")
		data = self.read()
		self.initGameElements()
		#self.initPlayers(4)
		values = data[0].split(" ")
		self.playerid = int(values[1]) - 1
		self.setSnake(self.playerid, int(values[2]), int(values[3]))
		self.isPlaying = 1
		self.arena.setFocus()
		self.timer.start(80,self)
	def ready(self):
		pass
	def send(self,message):
		if self.address and self.port and self.socket:
			self.log("SEND: ", message)
			return self.socket.writeDatagram(message, self.address, self.port)

	def read(self):
		if self.address and self.port and self.socket:
			#size = self.socket.pendingDatagramSize()i
			data = self.socket.readDatagram(100)
			while not data[0]:
				data = self.socket.readDatagram(100)
			self.log("READ: ", data[0])
			return data
	def exit(self):
		self.close();
	def countdown(self):

		self.countdownlabel = QtGui.QLabel("<font size = 30>" + str(self.countdowncounter) + "</font>", self)
		self.countdownlabel.setStyleSheet("QLabel { background-color: #88BB88; font-size:40pt}")
		self.countdownlabel.setAlignment(QtCore.Qt.AlignCenter)
		self.countdownlabel.setGeometry(height//2-40, height//2-80, 80, 80)

		self.pauseinfo = QtGui.QLabel("Pause/Continue: Space-Bar", self)
		if height == 600:
			self.pauseinfo.setStyleSheet("QLabel { background-color: #88BB88; font-size:15pt}")
		else:
			self.pauseinfo.setStyleSheet("QLabel { background-color: #88BB88; font-size:10pt}")
		self.pauseinfo.setAlignment(QtCore.Qt.AlignCenter)
		self.pauseinfo.setGeometry(height//2-125, height//2+20, 250, 50)
		self.countdownlabel.show()
		self.pauseinfo.show()

		if self.countdowncounter > 0:
			self.countdownlabel.setText(str(self.countdowncounter))
			self.countdowncounter -= 1
		else:
			self.countdownlabel.clear()
			self.countdowncounter = 3
			self.pauseinfo.clear()
			self.timer2.stop()
			self.countdownactive = 1
	def setFruitPos(self, posx, posy):
		self.fruitposx = posx
		self.fruitposy = posy
		self.fruit.setGeometry(posx * 20, posy * 20, 20,20)
		self.fruit.setPixmap(QtGui.QPixmap(":/sprites/apple"))
		self.fruit.show()

	def keyPressEvent(self, event):
		if self.isPlaying:
			code = ""
			if event.key() == QtCore.Qt.Key_Left:
				self.userKeys.append("left")
			if event.key() == QtCore.Qt.Key_Right:
				self.userKeys.append("right")
			if event.key() == QtCore.Qt.Key_Up :
				self.userKeys.append("up")
			if event.key() == QtCore.Qt.Key_Down :
				self.userKeys.append("down")
	def setSnake(self,player, posx, posy):
		self.snakes[player][0] = QtGui.QLabel(self.arena)
		self.snakes[player][0].setGeometry( posx * 20 , posy * 20 , 20, 20)

		self.snakes[player][0].show()
		self.snakes[player][0].setPixmap(self.heads[player]['right'])
		self.snakes[player][1] = QtGui.QLabel(self.arena)
		self.snakes[player][1].setGeometry( posx * 20 - 20, posy * 20 , 20, 20)

		self.snakes[player][1].show()
		self.snakes[player][1].setPixmap(self.bodies[player]['right'])

	def moveSnake(self, player, direction,enlarge):
		snake = self.snakes[player]
		if type(snake) is type(""):
			self.setSnake(player, 100, 100)
		newposx = 0
		newposy = 0
		oldposx = snake[0].x()
		oldposy = snake[0].y()
		if direction=="left":
			newposx = oldposx - 20
			newposy = oldposy
		if direction=="right":
			newposx = oldposx + 20
			newposy = oldposy
		if direction=="up":
			newposx = oldposx
			newposy = oldposy - 20
		if direction=="down":
			newposx = oldposx
			newposy = oldposy + 20
		newPart = QtGui.QLabel(self.arena)
		if not enlarge:
			snake.pop().hide()
		newPart.setGeometry(newposx,newposy,20,20)
		direct = str(direction)
		newPart.setPixmap(self.heads[player][direct])
		if len(snake) > 0:
			snake[0].setPixmap(self.bodies[player][direct])

		snake.insert(0,newPart)
		newPart.show()

	def timerEvent(self, event):
		data = self.read()[0]
		if data.startswith(MAKE_MOVE):
			if len(self.userKeys):
				self.currentMovingDirection = self.userKeys.popleft()
			msg = PLAY + ' ' + str(self.playerid + 1) + ' ' + self.currentMovingDirection
			self.send(msg)
		elif data.startswith(PLAY):
			fields = data.split(" ")
			self.moveSnake(int(fields[1]) - 1, fields[2],0 )
		elif data.startswith(CRASH):
			id = int(data.split(" ")[1])
			if id == self.playerid + 1:
				self.timer.stop()
				self.log("KONIEC: ", "Przegrales")
		elif data.startswith(BUG):
			values = data.split(" ")
			x = int(values[1])
			y = int(values[2])
			self.setFruitPos(x, y)
		elif data.startswith(EATEN):
			playerid = int(data.split(" ")[1])
			direction = data.split(" ")[2]
			self.moveSnake(playerid - 1,direction,1)
			self.hideFruit()

	def hideFruit(self):
		self.fruit.hide()
	def log(self,type,msg):
		if msg:
			type = type + msg
		self.txtLog.append(type)

		
def main():
	app = QtGui.QApplication(sys.argv)
	mainwindow = SnejkClient(20,20)
	mainwindow.show()
	sys.exit(app.exec_())
if __name__ == "__main__":
	main()