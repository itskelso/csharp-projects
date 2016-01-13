import socket
import sys
import threading
import os
import base64
import struct
import picamera
import time



def recvall(sock,count):
    buf = b''
    while count:
        newbuf = sock.recv(count)
        if not newbuf: return None
        buf += newbuf
        count -= len(newbuf)
    return buf

def loadBinary():
    with open("/home/pi/Desktop/image.jpg","rb") as file:
        string = base64.b64encode(file.read())
        return string

def takePhoto():    
    with picamera.PiCamera() as camera:
        
        
        camera.start_preview()
        print('started prevew')
        time.sleep(1)
        camera.capture('image.jpg')
        camera.stop_preview()
        camera.close()
        
        
        return loadBinary()

# Accept requests from any Ip as long as they get the port right
TCP_IP = '0.0.0.0'
TCP_PORT = 5000
BUFFER = 4096

String1 = struct.pack("I",1)
print(String1)

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.setsockopt(socket.SOL_SOCKET,socket.SO_REUSEADDR, 1)
s.bind((TCP_IP, TCP_PORT))

#LISTEN FOR ONE CONNECTION
s.listen(1)


#imagefile = open("/home/pi/Desktop/P1250025.JPG","rb")
#stringToSend = file.read(512);
#print(stringToSend)
#r = loadBinary()
#print("64string =",r)
# INFINITE LOOP

while True:
    print('waiting for connection')
    conn, addr = s.accept()
    print('recevied connection from %s', addr)
   

    
    try: 
        
        while 1:

            
            
            data = conn.recv(BUFFER)
            data = data.decode("utf-8")
            print('rec %s',data)
            

        
                
            
            #print(file)
            if data == "Send Photo":
                file = takePhoto()
                print('got past taking photo')
                length = len(file)
                length = struct.pack("I",length)
                print(length)
                #conn.send(String1)
                #print('sent : %s',"1")
                #while conn.recv
                conn.send(length)
                print('sent %s',length)
                conn.sendall( file)    
                
                print('sent : image')
                #data = "blah"
               
            if data == "Run Program":

#Run AJs Program here and loadBinary to ready file so we don't have to it everytime
#and so it doesn't have to decode right in the middle of the transfer communication
# i think that causes exceptions because it is expecting to send and receive immeadately
                
                
                break

            if data == "Take Photo":
#Just run picamera.py here to get photo
#might be able to do everything in one go but this gives dbug options
                break

            if data == "Register":

#save initial picture to new directory so we can manage and delete files when completed
                break
            if data == "Send Coordinates":
#might not need this one either just in case though
                break
    except: 
        conn.close()
        print('Connection Closed')
    
    
