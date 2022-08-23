using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using RT.Comb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IIRS.Utilities.Common
{
    public class SysUtility
    {
        public static void Delete(string WebRootPath, string DelDirectory)
        {
            string path = string.Format(@"\upload\{0:yyyy}\{0:MM}\{0:dd}\{1}\", DateTime.Now, DelDirectory);
            string absPath = WebRootPath + path;//物理绝对路径
            Directory.Delete(absPath, true);
        }

        public static List<PUB_ATT_FILE> UploadSysBase64File(string WebRootPath, string saveDirectory, List<Base64FilesVModel> fileInfo)
        {
            List<PUB_ATT_FILE> uploadFile = new List<PUB_ATT_FILE>();
            PUB_ATT_FILE fileModel = null;
            DateTime filePathDay = DateTime.Now;
            //202009248003
            if (saveDirectory.Length == 12)//如果是受理编号格式
            {
                if (DateTime.TryParse(saveDirectory.Substring(0, 4) + "-" + saveDirectory.Substring(4, 2) + "-" + saveDirectory.Substring(6, 2), out filePathDay))
                {

                }
            }
            string path = string.Format(@"\upload\{0:yyyy}\{0:MM}\{0:dd}\{1}\", filePathDay, saveDirectory);
            string absPath = WebRootPath + path;//物理绝对路径
            if (fileInfo.Count > 0)
            {
                if (!Directory.Exists(absPath))
                {
                    Directory.CreateDirectory(absPath);
                }
                //else //如果目录已经存在，则说明是二次操作，则应该先删除对应数据库记录以及文件夹文件
                //{
                //    Directory.Delete(absPath, true);
                //    Directory.CreateDirectory(absPath);
                //}
                int groupOdrNum = -1;
                string mediaType = "jpg";
                foreach (var group in fileInfo)
                {
                    if (group.children.Count > 0)
                    {
                        groupOdrNum = 1;
                        foreach (var file in group.children)
                        {
                            string fileID = Provider.Sql.Create().ToString("N");
                            if (file.IsBase64 == 1)
                            {
                                mediaType = "jpg";
                                if (file.IMG.Length > 0)
                                {
                                    string fileName = fileID + "." + mediaType;
                                    string fullName = Path.Combine(absPath, fileName);
                                    SysUtility.Base64ToFileAndSave(file.IMG, fullName);

                                    fileModel = new PUB_ATT_FILE()
                                    {
                                        BAK_PK = saveDirectory,
                                        BUS_PK = saveDirectory,
                                        DISPLAY_NAME = file.label,//file.FileName;
                                        MEDIA_TYPE = mediaType,
                                        FILE_ID = fileID,
                                        FILE_NAME = fileName,
                                        PATH = path,
                                        CDATE = DateTime.Now,
                                        GROUP_ID = group.ID,
                                        GROUP_NAME = group.label,
                                        ODR = groupOdrNum
                                    };
                                    uploadFile.Add(fileModel);
                                    groupOdrNum++;
                                }
                            }
                            else//附件形式上传的文件
                            {
                                string tempFilePath = WebRootPath + file.IMG;
                                if (File.Exists(tempFilePath))
                                {
                                    string fileName = Path.GetFileName(tempFilePath);
                                    string fullName = Path.Combine(absPath, fileName);
                                    mediaType = Path.GetExtension(fullName).ToLower();
                                    if (Path.GetFullPath(tempFilePath) != Path.GetFullPath(fullName))
                                    {
                                        File.Copy(tempFilePath, fullName, true);
                                    }
                                    fileModel = new PUB_ATT_FILE()
                                    {
                                        BUS_PK = saveDirectory,
                                        DISPLAY_NAME = file.label,//file.FileName;
                                        MEDIA_TYPE = mediaType,
                                        FILE_ID = fileID,
                                        FILE_NAME = fileName,
                                        PATH = path,
                                        CDATE = DateTime.Now,
                                        GROUP_ID = group.ID,
                                        GROUP_NAME = group.label,
                                        ODR = groupOdrNum
                                    };
                                    uploadFile.Add(fileModel);
                                    groupOdrNum++;
                                    //File.Delete(tempFilePath);
                                }
                                else
                                {
                                    throw new ApplicationException("错误的附件上传路径:" + file.IMG);
                                }
                            }
                        }
                    }
                }
            }
            return uploadFile;
        }



        /// <summary>
        /// Base64字符串转换成文件
        /// </summary>
        /// <param name="strInput">base64字符串</param>
        /// <param name="fileName">保存文件的绝对路径</param>
        /// <returns></returns>
        public static bool Base64ToFileAndSave(string strInput, string fileName)
        {
            bool bTrue = false;

            try
            {
                strInput = strInput.Replace("data:image/jpeg;base64,", string.Empty);
                byte[] buffer = Convert.FromBase64String(strInput);
                using (FileStream fs = new FileStream(fileName, FileMode.CreateNew))
                {
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Close();
                    bTrue = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bTrue;
        }

        /// <summary>
        /// 文件转换Base64格式
        /// </summary>
        /// <param name="filePath">文件全路径</param>
        /// <returns></returns>
        public static string FileToBase64(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        byte[] arr = new byte[fs.Length];
                        fs.Read(arr, 0, arr.Length);
                        fs.Close();
                        return Convert.ToBase64String(arr);
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 字符串转base64
        /// </summary>
        /// <param name="Str">字符串</param>
        /// <returns></returns>
        public static string ToBase64Str(string Str)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(Str);
            return Convert.ToBase64String(b);

        }

        /// <summary>
        ///	base64转字符串
        /// </summary>
        /// <param name="Str">base64编码</param>
        /// <returns></returns>
        public static string FromBase64Str(String Str)
        {
            byte[] b = Convert.FromBase64String(Str);
            return System.Text.Encoding.Default.GetString(b);
        }

        /// <summary>
        /// 文本换行
        /// </summary>
        /// <param name="data">换行文本</param>
        /// <param name="maxWidth">每行最多汉字数</param>
        /// <returns></returns>
        public static string WrapText(string data, int maxWidth)
        {
            StringBuilder sbReturnStr = new StringBuilder();
            double counter = 0;
            int length = data.Length;
            for (int i = 0; i < length; i++)
            {
                sbReturnStr.Append(data[i]);
                if (IsChinese(data[i]))
                {
                    counter += 2;
                }
                else
                {
                    counter += 1f;
                }
                if (Math.Ceiling(counter / 2) == maxWidth)//如果进一法等于要加回车数量时
                {
                    if (counter % 2 == 1 && (i + 1) < length)//说明带小数
                    {
                        if (!IsChinese(data[i + 1]))//如果下一个也是小数并且非汉字
                        {
                            sbReturnStr.Append(data[i + 1]);
                            i++;
                            counter = 0;
                        }
                    }
                    counter = 0;
                    if ((i + 1) != length)//如果正好是最后一个字符，则不再添加换行了
                    {
                        sbReturnStr.Append('\n');
                    }
                }
            }
            return sbReturnStr.ToString();
        }
        /// <summary>
        /// 用于IsChinese方法计算是否当前字符为汉字，本数组记录非汉字并且与汉字占用相同宽度的特殊字符，用于判定时是否按汉字处理
        /// </summary>
        private static char[] ChineseCharacter = new char[] { '、' };

        /// <summary>
        /// 判定是否为汉字
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsChinese(char c)
        {
            if (ChineseCharacter.Contains(c))
            {
                return true;
            }
            else
            {
                return (new System.Text.RegularExpressions.Regex(@"[\u4e00-\u9fa5]")).IsMatch(c.ToString());
            }
            //return (int)c >= 0x4E00 && (int)c <= 0x9FA5;
        }

        /// <summary>
        /// 计算汉字数量
        /// </summary>
        /// <param name="text">计算文本</param>
        /// <param name="times">每个汉字顶几个非汉字</param>
        /// <returns></returns>
        public static int ComputeChineseCount(string text, float times = 2F)
        {
            float nowLen = 0;
            for (int i = 1; i < text.Length; i++)
            {
                if (IsChinese(text[i]))
                {
                    nowLen++;
                }
                else
                {
                    nowLen += 1 / times;
                }
            }
            return Convert.ToInt32(Math.Ceiling(nowLen));//返回值进一法
        }

        private static ArrayList buildInsertIndexList(string str, int maxLen)
        {
            int nowLen = 0;
            ArrayList list = new ArrayList();
            for (int i = 1; i < str.Length; i++)
            {
                if (IsChinese(str[i]))
                {
                    nowLen += 2;
                }
                else
                {
                    nowLen++;
                }
                if (nowLen > maxLen)
                {
                    nowLen = 0;
                    list.Add(i);
                }
            }
            return list;
        }
    }
}
