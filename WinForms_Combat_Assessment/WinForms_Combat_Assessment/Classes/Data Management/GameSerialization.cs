﻿using System;
using System.Xml.Serialization;
using System.IO;

namespace WinForms_Combat_Assessment
{
    public static class GameSerialization<T>
    {
        public static void Serialize(string fileName, T data)
        {
            // Creates a new XMLSerializer object with an arguement of the type of the data passed in
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            // Creates a new TextWritter object and assgins it a StreamWritter object that thats in an argument of the file path
            TextWriter tw = new StreamWriter(Environment.CurrentDirectory + "../Saves/" + fileName + ".xml");

            // Invoke the Serialize function from the XmlSerializer class and pass in the TextWritter object created and tyhe data we are trying to wrtie to the file
            serializer.Serialize(tw, data);

            // Closes the file we where using to write our data
            tw.Close();
        }

        public static T Deserialize(string fileName)
        {
            // Creates a new Generic object we will use to hold the data we are trying to return
            T data;

            // Creates a new XmlSerializer objects with an arguement of the type of the generic we are trying to create
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            // Creates a new TextReader object with a child object of the class with the argument of a string that is trhe file directory of the file we are trying to get the data from
            TextReader reader = new StreamReader(Environment.CurrentDirectory + "../Saves/" + fileName + ".xml");

            // invokes the Deserialize function from the XmlSerializer class and casts the return as the generic type and passes in the TextReader object as an argument into the function call
            data = (T)serializer.Deserialize(reader);

            // Closes the TextReader object file
            reader.Close();

            // Returns our generic object
            return data;
        }
    }
}