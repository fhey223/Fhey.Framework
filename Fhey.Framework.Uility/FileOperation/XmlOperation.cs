using System;
using System.Text;
using System.Xml;

namespace Fhey.Framework.Uility.FileOperation
{
    public class XmlOperation 
    {

        #region 属性
        /// <summary>
        /// 文件路径
        /// </summary>
        /// <remarks>文件路径</remarks>
        public string XmlFilePath { get; set; }

        /// <summary>
        /// XML对象
        /// </summary>
        public XmlDocument xmlDoc { get; set; } = new XmlDocument();

        #endregion

        #region 构造函数
        public XmlOperation() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tempXmlFilePath">已有的文件的路径</param>
        public XmlOperation(string XmlFilePath)
        {
            this.XmlFilePath = XmlFilePath;
            GetXmlDocument();
        }

        ///<summary>
        ///获取XmlDocument实体类
        ///</summary>   
        /// <returns>指定的XML描述文件的一个xmldocument实例</returns>
        public XmlDocument GetXmlDocument()
            => GetXmlDocumentFromFile(XmlFilePath);


        public XmlDocument GetXmlDocumentFromFile(string XmlFilePath)
        {
            string xmlFileFullPath = XmlFilePath;

            try
            {
                xmlDoc.Load(xmlFileFullPath);
                //定义事件处理
                xmlDoc.NodeChanged += new XmlNodeChangedEventHandler(this.nodeUpdateEvent);
                xmlDoc.NodeInserted += new XmlNodeChangedEventHandler(this.nodeInsertEvent);
                xmlDoc.NodeRemoved += new XmlNodeChangedEventHandler(this.nodeDeleteEvent);
            }
            catch(Exception e)
            {
                xmlDoc = null;

            }
            return xmlDoc;
        }

        #endregion

        #region 获取
        /// <summary>
        /// 获取XML文件的根元素
        /// </summary>
        public XmlNode GetXmlRoot()
        {
            return xmlDoc.DocumentElement;
        }

        /// <summary>
        /// 功能:
        /// 获取所有指定名称的节点(XmlNodeList)
        /// </summary>
        /// <param >节点名称</param>
        public XmlNodeList GetXmlNodeList(string strNode)
        {
            XmlNodeList xmlNode = null;
            try
            {
                //根据指定路径获取节点
                xmlNode = xmlDoc.SelectNodes(strNode);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return xmlNode;
        }

        /// <summary>
        /// 功能:
        /// 根据属性值获取节点(XmlNodeList)
        /// </summary>
        /// <param >节点名称</param>
        public XmlNodeList GetXmlNodeByAttribute(string strNode, string strAttribute, string attributeValue)
        {
            XmlNodeList xmlNode = null;
            try
            {
                //根据指定路径获取节点
                xmlNode = xmlDoc.SelectNodes($"{strNode}[@{strAttribute}={attributeValue}]");
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return xmlNode;
        }


        /// <summary>
        /// 功能:
        /// 读取指定节点的指定属性值(Value)
        /// </summary>
        /// <param >节点名称</param>
        /// <param >此节点的属性</param>
        /// <returns></returns>
        public string GetXmlNodeAttributeValue(string strNode, string strAttribute)
        {
            string strReturn = "";
            try
            {
                //根据指定路径获取节点
                XmlNode xmlNode = xmlDoc.SelectSingleNode("//"+strNode);
                if (!(xmlNode == null))
                {
                    strReturn = xmlNode.Attributes.GetNamedItem(strAttribute).Value;

                    ////获取节点的属性，并循环取出需要的属性值
                    //XmlAttributeCollection xmlAttr = xmlNode.Attributes;
                    //for (int i = 0; i < xmlAttr.Count; i++)
                    //{
                    //    if (xmlAttr.Item(i).Name == strAttribute)
                    //    {
                    //        strReturn = xmlAttr.Item(i).Value;
                    //        break;
                    //    }
                    //}
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return strReturn;
        }

        /// <summary>
        /// 功能:
        /// 读取指定节点的值(InnerText)
        /// </summary>
        /// <param >节点名称</param>
        /// <returns></returns>
        public string GetXmlNodeValue(string strNode)
        {
            string strReturn = String.Empty;
            try
            {
                //根据路径获取节点
                XmlNode xmlNode = xmlDoc.SelectSingleNode("//"+strNode);
                if (!(xmlNode == null))
                    strReturn = xmlNode.InnerText;
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return strReturn;
        }
        #endregion

        #region 设置
        /// <summary>
        /// 功能:
        /// 设置节点值(InnerText)
        /// </summary>
        /// <param >节点的名称</param>
        /// <param >节点值</param>
        public void SetXmlNodeValue(string xmlNodePath, string xmlNodeValue)
        {
            try
            {
                //可以批量为符合条件的节点进行付值
                XmlNodeList xmlNode = xmlDoc.SelectNodes("//"+xmlNodePath);
                if (!(xmlNode == null))
                {
                    foreach (XmlNode xn in xmlNode)
                    {
                        xn.InnerText = xmlNodeValue;
                    }
                }
                /**/
                /*
             * 根据指定路径获取节点
            XmlNode xmlNode = xmlDoc.SelectSingleNode("//"+xmlNodePath) ;           
            //设置节点值
            if (!(xmlNode==null))
                xmlNode.InnerText = xmlNodeValue ;*/
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        /// <summary>
        /// 功能:
        /// 设置节点的属性值   
        /// </summary>
        /// <param >节点名称</param>
        /// <param >属性名称</param>
        /// <param >属性值</param>
        public void SetXmlNodeValue(string xmlNodePath, string xmlNodeAttribute, string xmlNodeAttributeValue)
        {
            try
            {
                //可以批量为符合条件的节点的属性付值
                XmlNodeList xmlNode = xmlDoc.SelectNodes("//"+xmlNodePath);
                if (!(xmlNode == null))
                {
                    foreach (XmlNode xn in xmlNode)
                    {
                        XmlAttributeCollection xmlAttr = xn.Attributes;
                        for (int i = 0; i < xmlAttr.Count; i++)
                        {
                            if (xmlAttr.Item(i).Name == xmlNodeAttribute)
                            {
                                xmlAttr.Item(i).Value = xmlNodeAttributeValue;
                                break;
                            }
                        }
                    }
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        #endregion

        #region 添加
        /// <summary>
        /// 创建新的XML
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="rootNode">根节点</param>
        public XmlOperation CreateNewXmlFile(string rootNode)
        {
            var xtw = new XmlTextWriter(XmlFilePath, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            xtw.WriteStartDocument(false);
            xtw.WriteStartElement(rootNode);
            xtw.WriteEndElement();
            xtw.Flush();
            xtw.Close();
            xtw.Dispose();
            return new XmlOperation(XmlFilePath);
        }

        /// <summary>
        /// 在根节点下添加父节点
        /// </summary>
        public void AddParentNode(string parentNode)
        {
            try
            {
                XmlNode root = xmlDoc.DocumentElement;
                XmlNode parentXmlNode = xmlDoc.CreateElement(parentNode);
                root.AppendChild(parentXmlNode);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        /// <summary>
        /// 向一个已经存在的父节点中插入一个子节点
        /// </summary>
        /// <param >父节点</param>
        /// <param >字节点名称</param>
        public void AddChildNode(string parentNodePath, string childnodename)
        {
            try
            {
                XmlNode childXmlNode = null;
                XmlNodeList xmlNodeList = xmlDoc.SelectNodes("//" + parentNodePath);

                if (!(xmlNodeList == null))
                {
                    foreach (XmlNode Node in xmlNodeList)
                    {
                        childXmlNode = xmlDoc.CreateElement(childnodename);
                        Node.AppendChild(childXmlNode);
                    }
                }
                else
                {
                    childXmlNode = xmlDoc.CreateElement(childnodename);
                    xmlDoc.DocumentElement.AppendChild(childXmlNode);
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        /// <summary>
        /// 向一个已经存在的父节点中插入一个子节点,并添加一个属性
        /// </summary>
        public void AddChildNode(string parentNodePath, string childnodename, string NodeAttribute, string NodeAttributeValue)
        {
            try
            {
                XmlNode childXmlNode = null;
                XmlNodeList parentXmlNodeList = xmlDoc.SelectNodes("//" + parentNodePath);

                if (!(parentXmlNodeList == null))
                {
                    foreach (XmlNode Node in parentXmlNodeList)
                    {
                        childXmlNode = xmlDoc.CreateElement(childnodename);
                        //添加属性
                        XmlAttribute nodeAttribute = xmlDoc.CreateAttribute(NodeAttribute);
                        nodeAttribute.Value = NodeAttributeValue;
                        childXmlNode.Attributes.Append(nodeAttribute);
                        Node.AppendChild(childXmlNode);
                    }
                }
                else
                {
                    xmlDoc.DocumentElement.AppendChild(childXmlNode);
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        /// <summary>
        /// 向一个节点添加属性,值为空
        /// </summary>
        /// <param >节点路径</param>
        /// <param >属性名</param>
        public void AddAttribute(string NodePath, string NodeAttribute)
        {
            AddAttribute(NodePath, NodeAttribute, "");
        }

        /// <summary>
        ///  向一个节点添加属性,并赋值
        /// </summary>
        /// <param >节点</param>
        /// <param >属性名</param>
        /// <param >属性值</param>
        public void AddAttribute(string NodePath, string NodeAttribute, string NodeAttributeValue)
        {
            try
            {
                XmlNodeList xmlNodeList = xmlDoc.SelectNodes("//" + NodePath);

                if (!(xmlNodeList == null))
                {
                    foreach (XmlNode Node in xmlNodeList)
                    {
                        XmlAttribute nodeAttribute = xmlDoc.CreateAttribute(NodeAttribute);
                        nodeAttribute.Value = NodeAttributeValue;
                        Node.Attributes.Append(nodeAttribute);
                    }
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        /// <summary>
        /// 向一个节点添加属性,并赋值***
        /// </summary>
        public void AddAttribute(XmlNode childXmlNode, string NodeAttribute, string NodeAttributeValue)
        {
            XmlAttribute nodeAttribute = xmlDoc.CreateAttribute(NodeAttribute);
            nodeAttribute.Value = NodeAttributeValue;
            childXmlNode.Attributes.Append(nodeAttribute);
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除节点的一个属性
        /// </summary>
        /// <param >节点所在的xpath表达式</param>
        /// <param >属性名</param>
        public void DeleteAttribute(string NodePath, string NodeAttribute)
        {
            XmlNodeList nodePath = xmlDoc.SelectNodes("//" + NodePath);
            if (!(nodePath == null))
            {
                foreach (XmlNode tempxn in nodePath)
                {
                    XmlAttributeCollection xmlAttr = tempxn.Attributes;
                    for (int i = 0; i < xmlAttr.Count; i++)
                    {
                        if (xmlAttr.Item(i).Name == NodeAttribute)
                        {
                            tempxn.Attributes.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 删除节点的一个属性,当其属性值等于给定的值时
        /// </summary>
        /// <param >节点所在的xpath表达式</param>
        /// <param >属性</param>
        /// <param >值</param>
        public void DeleteAttribute(string NodePath, string NodeAttribute, string NodeAttributeValue)
        {
            XmlNodeList nodePath = this.xmlDoc.SelectNodes("//" + NodePath);
            if (!(nodePath == null))
            {
                foreach (XmlNode tempxn in nodePath)
                {
                    XmlAttributeCollection xmlAttr = tempxn.Attributes;
                    for (int i = 0; i < xmlAttr.Count; i++)
                    {
                        if (xmlAttr.Item(i).Name == NodeAttribute && xmlAttr.Item(i).Value == NodeAttributeValue)
                        {
                            tempxn.Attributes.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param ></param>
        /// <remarks></remarks>
        public void DeleteXmlNode(string tempXmlNode)
        {
            XmlNodeList nodePath = xmlDoc.SelectNodes("//"+tempXmlNode);
            if (!(nodePath == null))
            {
                foreach (XmlNode xn in nodePath)
                {
                    xn.ParentNode.RemoveChild(xn);
                }
            }
        }

        #endregion

        #region XML文档事件
        /// <summary>
        /// 节点插入事件
        /// </summary>
        /// <param ></param>
        /// <param ></param>
        private void nodeInsertEvent(Object src, XmlNodeChangedEventArgs args)
        {
            //保存设置
            SaveXmlDocument();
        }
        /// <summary>
        /// 节点删除事件
        /// </summary>
        /// <param ></param>
        /// <param ></param>
        private void nodeDeleteEvent(Object src, XmlNodeChangedEventArgs args)
        {
            //保存设置
            SaveXmlDocument();
        }
        /// <summary>
        /// 节点更新事件
        /// </summary>
        /// <param ></param>
        /// <param ></param>
        private void nodeUpdateEvent(Object src, XmlNodeChangedEventArgs args)
        {
            //保存设置
            SaveXmlDocument();
        }
        #endregion

        #region 保存XML文件
        /// <summary>
        /// 功能:
        /// 保存XML文件
        /// </summary>
        public void SaveXmlDocument()
        {
            try
            {
                //保存设置的结果
                Savexml(XmlFilePath);

            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }

        /// <summary>
        /// 功能:
        /// 保存XML文件   
        /// </summary>
        public void SaveXmlDocument(string tempXMLFilePath)
        {
            try
            {
                //保存设置的结果
                Savexml(tempXMLFilePath);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param ></param>
        private void Savexml(string filepath)
        {
            xmlDoc.Save(filepath);
        }

        #endregion
    }
}
