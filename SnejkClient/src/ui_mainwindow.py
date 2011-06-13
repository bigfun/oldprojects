# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'MainWindow.ui'
#
# Created: Fri Jun 11 10:01:16 2010
#      by: PyQt4 UI code generator 4.7.2
#
# WARNING! All changes made in this file will be lost!

from PyQt4 import QtCore, QtGui

class Ui_MainWindow(object):
    def setupUi(self, MainWindow):
        MainWindow.setObjectName("MainWindow")
        MainWindow.resize(523, 768)
        MainWindow.setFocusPolicy(QtCore.Qt.StrongFocus)
        self.centralwidget = QtGui.QWidget(MainWindow)
        self.centralwidget.setObjectName("centralwidget")
        self.gridLayout = QtGui.QGridLayout(self.centralwidget)
        self.gridLayout.setObjectName("gridLayout")
        self.lblMainTitle = QtGui.QLabel(self.centralwidget)
        self.lblMainTitle.setStyleSheet("font: 20pt \"Aller Display\";\n"
"color: rgb(255, 106, 0);\n"
"")
        self.lblMainTitle.setAlignment(QtCore.Qt.AlignCenter)
        self.lblMainTitle.setObjectName("lblMainTitle")
        self.gridLayout.addWidget(self.lblMainTitle, 0, 0, 1, 1)
        self.arena = QtGui.QWidget(self.centralwidget)
        self.arena.setMinimumSize(QtCore.QSize(500, 500))
        self.arena.setMaximumSize(QtCore.QSize(500, 500))
        self.arena.setAutoFillBackground(False)
        self.arena.setStyleSheet("background-color: rgb(189, 196, 255);")
        self.arena.setObjectName("arena")
        self.gridLayout.addWidget(self.arena, 1, 0, 1, 2)
        self.horizontalLayout_3 = QtGui.QHBoxLayout()
        self.horizontalLayout_3.setObjectName("horizontalLayout_3")
        self.horizontalLayout = QtGui.QHBoxLayout()
        self.horizontalLayout.setObjectName("horizontalLayout")
        self.label_2 = QtGui.QLabel(self.centralwidget)
        self.label_2.setObjectName("label_2")
        self.horizontalLayout.addWidget(self.label_2)
        self.inputPort = QtGui.QLineEdit(self.centralwidget)
        self.inputPort.setObjectName("inputPort")
        self.horizontalLayout.addWidget(self.inputPort)
        self.horizontalLayout_3.addLayout(self.horizontalLayout)
        self.horizontalLayout_2 = QtGui.QHBoxLayout()
        self.horizontalLayout_2.setObjectName("horizontalLayout_2")
        self.label = QtGui.QLabel(self.centralwidget)
        self.label.setObjectName("label")
        self.horizontalLayout_2.addWidget(self.label)
        self.inputAddress = QtGui.QLineEdit(self.centralwidget)
        self.inputAddress.setObjectName("inputAddress")
        self.horizontalLayout_2.addWidget(self.inputAddress)
        self.horizontalLayout_3.addLayout(self.horizontalLayout_2)
        self.gridLayout.addLayout(self.horizontalLayout_3, 2, 0, 1, 1)
        self.verticalLayout_2 = QtGui.QVBoxLayout()
        self.verticalLayout_2.setObjectName("verticalLayout_2")
        self.btnConnect = QtGui.QPushButton(self.centralwidget)
        self.btnConnect.setObjectName("btnConnect")
        self.verticalLayout_2.addWidget(self.btnConnect)
        self.btnExit = QtGui.QPushButton(self.centralwidget)
        self.btnExit.setFocusPolicy(QtCore.Qt.NoFocus)
        self.btnExit.setObjectName("btnExit")
        self.verticalLayout_2.addWidget(self.btnExit)
        self.gridLayout.addLayout(self.verticalLayout_2, 2, 1, 1, 1)
        self.txtLog = QtGui.QTextEdit(self.centralwidget)
        self.txtLog.setMaximumSize(QtCore.QSize(500, 150))
        self.txtLog.setObjectName("txtLog")
        self.gridLayout.addWidget(self.txtLog, 3, 0, 1, 2)
        MainWindow.setCentralWidget(self.centralwidget)
        self.menubar = QtGui.QMenuBar(MainWindow)
        self.menubar.setGeometry(QtCore.QRect(0, 0, 523, 27))
        self.menubar.setObjectName("menubar")
        MainWindow.setMenuBar(self.menubar)
        self.statusbar = QtGui.QStatusBar(MainWindow)
        self.statusbar.setObjectName("statusbar")
        MainWindow.setStatusBar(self.statusbar)

        self.retranslateUi(MainWindow)
        QtCore.QMetaObject.connectSlotsByName(MainWindow)

    def retranslateUi(self, MainWindow):
        MainWindow.setWindowTitle(QtGui.QApplication.translate("MainWindow", "MainWindow", None, QtGui.QApplication.UnicodeUTF8))
        self.lblMainTitle.setText(QtGui.QApplication.translate("MainWindow", "Snejk!", None, QtGui.QApplication.UnicodeUTF8))
        self.label_2.setText(QtGui.QApplication.translate("MainWindow", "Port:", None, QtGui.QApplication.UnicodeUTF8))
        self.label.setText(QtGui.QApplication.translate("MainWindow", "Adres:", None, QtGui.QApplication.UnicodeUTF8))
        self.btnConnect.setText(QtGui.QApplication.translate("MainWindow", "Połącz", None, QtGui.QApplication.UnicodeUTF8))
        self.btnExit.setText(QtGui.QApplication.translate("MainWindow", "Wyjdź", None, QtGui.QApplication.UnicodeUTF8))

import resources_rc
