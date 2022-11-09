using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Unity.VisualScripting;
using System;

[ExecuteInEditMode]
public class RawDataLoader : MonoBehaviour
{
    private string fileToImport;

    private int dimX;
    private int dimY;
    private int dimZ;
    private int bytesToSkip = 0;
    private DataContentFormat dataFormat = DataContentFormat.Int16;
    private Endianness endianness = Endianness.LittleEndian;

    public void Load(string path)
    {
        fileToImport = path;

        if (Path.GetExtension(fileToImport) == ".ini")
            fileToImport = fileToImport.Substring(0, fileToImport.Length - 4);

        print(fileToImport);

        // Try parse ini file (if available)
        DatasetIniData initData = DatasetIniReader.ParseIniFile(fileToImport + ".ini");
        if (initData != null)
        {
            dimX = initData.dimX;
            dimY = initData.dimY;
            dimZ = initData.dimZ;
            bytesToSkip = initData.bytesToSkip;
            dataFormat = initData.format;
            endianness = initData.endianness;
        }

        VolumeData dataset = Import();

        if (dataset != null)
        {
            //if (EditorPrefs.GetBool("DownscaleDatasetPrompt"))
            //{
            //    if (EditorUtility.DisplayDialog("Optional DownScaling",
           //         $"Do you want to downscale the dataset? The dataset's dimension is: {dataset.dimX} x {dataset.dimY} x {dataset.dimZ}", "Yes", "No"))
          //      {
            //        dataset.DownScaleData();
           //     }
           // }
            VolumeRenderedObject obj = VolumeObjectFactory.CreateObject(dataset);
        }
        else
        {
            Debug.LogError("Failed to import datset");
        }
    }

    public VolumeData Import()
    {
        // Check that the file exists
        if (!File.Exists(fileToImport))
        {
            Debug.LogError("The file does not exist: " + fileToImport);
            return null;
        }

        FileStream fs = new FileStream(fileToImport, FileMode.Open);
        BinaryReader reader = new BinaryReader(fs);

        // Check that the dimension does not exceed the file size
        long expectedFileSize = (long)(dimX * dimY * dimZ) * GetSampleFormatSize(dataFormat) + bytesToSkip;
        if (fs.Length < expectedFileSize)
        {
            Debug.LogError($"The dimension({dimX}, {dimY}, {dimZ}) exceeds the file size. Expected file size is {expectedFileSize} bytes, while the actual file size is {fs.Length} bytes");
            reader.Close();
            fs.Close();
            return null;
        }

        VolumeData dataset = new VolumeData();
        dataset.datasetName = Path.GetFileName(fileToImport);
        dataset.filePath = fileToImport;
        dataset.dimX = dimX;
        dataset.dimY = dimY;
        dataset.dimZ = dimZ;

        // Skip header (if any)
        if (bytesToSkip > 0)
            reader.ReadBytes(bytesToSkip);

        int uDimension = dimX * dimY * dimZ;
        dataset.data = new float[uDimension];

        // Read the data/sample values
        for (int i = 0; i < uDimension; i++)
        {
            dataset.data[i] = (float)ReadDataValue(reader);
        }
        Debug.Log("Loaded dataset in range: " + dataset.GetMinDataValue() + "  -  " + dataset.GetMaxDataValue());

        reader.Close();
        fs.Close();

        dataset.FixDimensions();

        return dataset;
    }
    private int ReadDataValue(BinaryReader reader)
    {
        switch (dataFormat)
        {
            case DataContentFormat.Int8:
                {
                    sbyte dataval = reader.ReadSByte();
                    return (int)dataval;
                }
            case DataContentFormat.Int16:
                {
                    short dataval = reader.ReadInt16();
                    if (endianness == Endianness.BigEndian)
                    {
                        byte[] bytes = BitConverter.GetBytes(dataval);
                        Array.Reverse(bytes, 0, bytes.Length);
                        dataval = BitConverter.ToInt16(bytes, 0);
                    }
                    return (int)dataval;
                }
            case DataContentFormat.Int32:
                {
                    int dataval = reader.ReadInt32();
                    if (endianness == Endianness.BigEndian)
                    {
                        byte[] bytes = BitConverter.GetBytes(dataval);
                        Array.Reverse(bytes, 0, bytes.Length);
                        dataval = BitConverter.ToInt32(bytes, 0);
                    }
                    return (int)dataval;
                }
            case DataContentFormat.Uint8:
                {
                    return (int)reader.ReadByte();
                }
            case DataContentFormat.Uint16:
                {
                    ushort dataval = reader.ReadUInt16();
                    if (endianness == Endianness.BigEndian)
                    {
                        byte[] bytes = BitConverter.GetBytes(dataval);
                        Array.Reverse(bytes, 0, bytes.Length);
                        dataval = BitConverter.ToUInt16(bytes, 0);
                    }
                    return (int)dataval;
                }
            case DataContentFormat.Uint32:
                {
                    uint dataval = reader.ReadUInt32();
                    if (endianness == Endianness.BigEndian)
                    {
                        byte[] bytes = BitConverter.GetBytes(dataval);
                        Array.Reverse(bytes, 0, bytes.Length);
                        dataval = BitConverter.ToUInt32(bytes, 0);
                    }
                    return (int)dataval;
                }
            default:
                throw new NotImplementedException("Unimplemented data content format");
        }
    }
    private int GetSampleFormatSize(DataContentFormat format)
    {
        switch (format)
        {
            case DataContentFormat.Int8:
                return 1;
            case DataContentFormat.Uint8:
                return 1;
            case DataContentFormat.Int16:
                return 2;
            case DataContentFormat.Uint16:
                return 2;
            case DataContentFormat.Int32:
                return 4;
            case DataContentFormat.Uint32:
                return 4;
        }
        throw new NotImplementedException();
    }
}
