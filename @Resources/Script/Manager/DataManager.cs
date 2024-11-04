using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class DataManager : Iinit
{
    public List<GloveData> GloveDatas = new List<GloveData>();
    public Dictionary<Define.KeyEvents, KeyCode> KeyBinds = new Dictionary<Define.KeyEvents, KeyCode>();

    public void Init()
    {
        KeyBinds.Add(Define.KeyEvents.None, KeyCode.None);
        ReadData(Managers.Resource.Load<TextAsset>("KeyBinding.data").text);
        ReadData(Managers.Resource.Load<TextAsset>("GloveData.data").text);
        
    }
    public void ReadData(string data)
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(data);
        XmlNode HeadNode = xml.DocumentElement;
        XmlNodeList _childs = HeadNode.ChildNodes;
        //for (int i =0; i < _childs.Count; i++)
        //{
        //    Debug.Log(_childs.Item(i).OuterXml);
        //}
        DataType datatype = (DataType)Enum.Parse(typeof(DataType), HeadNode.Name);
        ReadSonData(_childs, 0, datatype);
    }
    void ReadSonData(XmlNodeList childlist, int depth, DataType datatype)
    {
        int _depth = depth + 1;
        for (int i =0;  i < childlist.Count; i++)
        {

            if (_depth == 1 && datatype == DataType.GloveData)
                GloveDatas.Add(new GloveData());
            XmlNode child = childlist.Item(i);
            
            XmlAttribute att_name = child.Attributes["value"];
            XmlAttribute att_type = child.Attributes["type"];
            string NodeName = child.Name;
            string value = "";
            string type = "";
            if (att_name != null && att_type != null)
            {
                value = att_name.Value;
                type = att_type.Value;
                SetDataToVariable(type, value, NodeName);
            }
            else if (child.ChildNodes.Count != 0)
            {
                ReadSonData(child.ChildNodes, _depth, datatype);
            }
        }
    }
    public enum DataType
    {
        PlayerData,
        MonsterData,
        GloveData,
        KeyBindingData
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
                case "EffectAmount":
                    GloveDatas[GloveDatas.Count - 1].EffectAmoutn.Add(value);
                    break;
                case "Speed":
                    GloveDatas[GloveDatas.Count - 1].Speed = value;
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
        if (type == "enum")
        {
            KeyCode value = (KeyCode)Enum.Parse(typeof(KeyCode), data);
            Define.KeyEvents _key = (Define.KeyEvents)Enum.Parse(typeof(Define.KeyEvents), NodeName);
            KeyBinds.Add(_key, value);
        }

    }

    
}
public class GloveData
{
    public string Name;
    public int Attk;
    public float Speed;

    public List<float> SkillCoolDown = new List<float>();
    public List<float> EffectAmoutn = new List<float>();
}
