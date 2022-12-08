import vtk
import numpy as np
import struct

# def save_vf(self, filename):
#         """ Write the vector field as .vf file format to disk. """
#         if not np.unique(self.resolution).size == 1:
#             raise ValueError("Vectorfield resolution must be the same for X, Y, Z when exporting to Unity3D.")
    
#         file_handle = open(filename, 'wb')
#         for val in [b'V', b'F', b'_', b'V',
#                     struct.pack('H', self.resolution[0]),
#                     struct.pack('H', self.resolution[1]),
#                     struct.pack('H', self.resolution[2])]:
#             file_handle.write(val)
        
#         # Layout data in required order.
#         u_stream = self.u.flatten('F')
#         v_stream = self.v.flatten('F')
#         w_stream = self.w.flatten('F')
#         for i in range(u_stream.size):
#             file_handle.write(struct.pack('f', v_stream[i]))
#             file_handle.write(struct.pack('f', u_stream[i]))
#             file_handle.write(struct.pack('f', w_stream[i]))
#         file_handle.close()



     

if __name__ == '__main__':
    path = "E:\\VIS22\\Assign3\\Data_Assign3\\Data_Assign3\\"
    #input_file_name = "bernard3D_Q.vtk"
    input_file_name = "FullHead.mhd"
    input_file_name = path + input_file_name
    if ".mhd" in input_file_name: #The input file is MetaImageData
            input_type = "mhd"
            reader = vtk.vtkMetaImageReader()
            reader.SetFileName(input_file_name)
            reader.Update()
    elif ".vtk" in input_file_name: # The input file is VTK
            input_type = "vtk"
            reader = vtk.vtkDataSetReader()
            reader.SetFileName(input_file_name)
            reader.Update()

    poly = reader.GetOutput()
    scalars = poly.GetPointData().GetScalars()
    array = np.array(reader.GetOutput().GetPointData().GetScalars())
    
    print(len(array))
    print(poly.GetScalarRange()[0])
    print(poly.GetScalarRange()[1])
    dimension = poly.GetDimensions()
    print(dimension)
    #print(poly.GetPointData())

    ini_file_name = input_file_name + ".raw.ini"
    
    file_handle = open(ini_file_name, 'w')
    file_handle.write("dimx:" + str(dimension[0]) +"\n")
    file_handle.write("dimy:" + str(dimension[1])+"\n")
    file_handle.write("dimz:" +str(dimension[2])+"\n")
    file_handle.write("skip:0"+"\n")
    file_handle.write("format:int32"+"\n")
    file_handle.close()


    file_name = input_file_name + ".raw.txt"
    
    file_handle = open(file_name, 'w')
    print(array[0])
    for i in range(len(array)):
        file_handle.write(str(array[i]) +"\n")
    file_handle.close()

    file_name_raw = input_file_name + ".raw"
    file_handle = open(file_name_raw, 'wb')
    print(array[0])
    for i in range(len(array)):
        file_handle.write(struct.pack('i', (int)(array[i])))
    file_handle.close()
