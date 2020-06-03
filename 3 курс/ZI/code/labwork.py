from PIL import Image
import numpy
from bitify import *


def encode(in_file, secret_data, out_file):
    img = Image.open(in_file)
    data = numpy.asarray(img)
    h, w, channels = data.shape
    data = data.reshape(h * w * channels)
    data = list(data)
    for i in range(len(data)):
        data[i] &= 0b11111110
    for i, x in enumerate(secret_data):
        data[i] = data[i] | x
    data = numpy.array(data, dtype=numpy.uint8).reshape(h, w, channels)
    Image.fromarray(data).save(out_file)


def decode(in_file):
    img = Image.open(in_file)
    data = numpy.asarray(img)
    h, w, channels = data.shape
    data = data.reshape(h * w * channels)
    data = list(data)
    for x in data:
        yield x & 0b00000001


if __name__ == "__main__":
    while True:
        print "1.Encode"
        print "2.Decode"
        print "e.Exit"
        table_val = raw_input()
        if table_val == '1':
            #secret_data = bitify(raw_input("Input data to hide: "))
            f=open("comm.txt","r+")
            secret_data=bitify(f.read())
            input_im = raw_input("Enter name of image: ")
            encode(input_im + ".png", secret_data, "encrypted_" + input_im + ".png")
        elif table_val == '2':
            input_dec = raw_input("Enter name of image to decode: ")
            print unbitify(decode(input_dec + ".png"))

        elif table_val == 'e':
            break
