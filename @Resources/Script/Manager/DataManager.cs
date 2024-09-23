using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class DataManager
{
    public List<GloveData> GloveDatas = new List<GloveData>();
    public void ReadData(string data)
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(data);

        XmlNode HeadNode = xml.DocumentElement;
        XmlNodeList _childs = HeadNode.ChildNodes;
        DataType _datatype = (DataType)Enum.Parse(typeof(DataType), HeadNode.Name);
        switch(_datatype)
        {
            case DataType.PlayerData:
                break;
            case DataType.MonsterData:
                break;
            case DataType.GloveData:
                
                break;
        }    
        ReadSonData(_childs, 0);
    }
    void ReadSonData(XmlNodeList childlist, int depth)
    {
        int _depth = depth + 1;
        if (_depth == 1)
            GloveDatas.Add(new GloveData());
        for (int i =0;  i < childlist.Count; i++)
        {
            XmlNode child = childlist[i];
            XmlAttribute att_name = child.Attributes["value"];
            XmlAttribute att_type = child.Attributes["Datatype"];
            string NodeName = child.Name;
            string value = "";
            string type = "";
            if (att_name != null && att_type != null)
            {
                value = att_name.Value;
                type = att_type.Value;
                SetDataToVariable(type, value, NodeName);
            }
            
            if (child.ChildNodes.Count != 0)
            {

            }
        }
    }
    public enum DataType
    {
        PlayerData,
        MonsterData,
        GloveData,

    }

    void SetDataToVariable(string type, string data, string NodeName)
    {
        
        if (type == "int")
        {
            int value = int.Parse(data);
            switch (NodeName)
            {
                
                case "Attack":
                    GloveDatas[GloveDatas.Count - 1].Attk = value;
                    break;
                case "Speed":
                    GloveDatas[GloveDatas.Count - 1].Speed = value;
                    break;
            }
        }
        if (type == "float")
        {
            float value = float.Parse(data);
            switch (NodeName)
            {
                case "CoolDown":
                    GloveDatas[GloveDatas.Count - 1].SkillCoolDown.Add(value);
                    break;
            }
        }
        if (type == "string")
        {
            switch (NodeName)
            {
                case "Name":
                    GloveDatas[GloveDatas.Count - 1].Name = data;
                    break;
            }
        }

        
    }
}
public class GloveData
{
    public string Name;
    public int Attk;
    public float Speed;

    public List<float> SkillCoolDown = new List<float>();
}
