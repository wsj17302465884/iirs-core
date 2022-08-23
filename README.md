##不动产登记+金融系统
英文名字：IIRS（Immovables Internet Register System）

### 采用的基础框架
后端（服务端）：.NET Core 3.1
前端（应用端）：Vue.js、微信小程序

### GitHub地址
[ASP.NET Core + Vue.js starter project](https://github.com/SoftwareAteliers/asp-net-core-vue-starter)

### 项目中目前需要的第三方插件(NuGet)
#### 日志记录：NLog
1. NLog.Web.AspNetCore
2. NLog

#### Office格式支持
1. EPPlus.Core [.NET Core使用EPPlus简单操作Excel(简单实现导入导出)](https://www.cnblogs.com/qmhuang/p/8306176.html)[ASP.NET Core 导出Excel文件](https://blog.csdn.net/qq_34220236/article/details/80841529)
2. NPOI .NET Core [NPOI .NET Core 2.0](http://www.cnblogs.com/savorboard/p/netcore-npoi.html) [GitHub](https://github.com/dotnetcore/NPOI)

#### 统计图
1. echarts

#### 数据库操作
1. EF Core（暂时不选）
2. SqlSugar[GitHub](https://github.com/sunkaixuan/SqlSugar)

#### 微信公众平台SDK
1. Senparc

#### API 文档（swagger）
1.swagger

### 版本控制
1. [码云](https://gitee.com/)
2. [GitHub](https://github.com/)


### 国内访问GitHub
修改`hosts`文件，加入内容

```
192.30.253.112 github.com
192.30.253.118 gist.github.com
151.101.112.133 assets-cdn.github.com
151.101.184.133 raw.githubusercontent.com
151.101.112.133 gist.githubusercontent.com
151.101.184.133 cloud.githubusercontent.com
151.101.112.133 camo.githubusercontent.com
151.101.112.133 avatars0.githubusercontent.com
151.101.112.133 avatars1.githubusercontent.com
151.101.184.133 avatars2.githubusercontent.com
151.101.12.133 avatars3.githubusercontent.com
151.101.12.133 avatars4.githubusercontent.com
151.101.184.133 avatars5.githubusercontent.com
151.101.184.133 avatars6.githubusercontent.com
151.101.184.133 avatars7.githubusercontent.com
151.101.12.133 avatars8.githubusercontent.com
```

### VUE的使用

#### 安装 Node.js
[Node.js](https://nodejs.org/)

#### 安装 VUE Cli
```shell
npm install vue
```

#### 创建项目
```shell
vue ui
```
#### 安装 Element-ui（其他 Component 类似）
```shell
npm i element-ui -S
```

**命令行窗口当前目录必须在`packeage.json`文件同级目录中**

#### 启动网站服务并监听文件改动
```shell
npm run serve
```

**命令行窗口当前目录必须在`packeage.json`文件同级目录中**

#### ElementUI
[ElementUI](https://element.eleme.cn/#/)

#### RT.Comb
[RT.Comb](https://github.com/richardtallent/RT.Comb)可以为项目生成`COMB`类型`GUID`，这种类型的`GUID`是有序的，有利于数据库主键的排序等操作。

```cs
using System;
using RT.Comb;

enum ExitCode : int {
  Success = 0,
  NotAGuid = 1
}

class CombMe {
    /// <summary>
    /// 控制台演示，当不带参数直接执行程序时，将返回一个 COMB 类型的 GUID；
    /// 当提供一个 COMB 类型的 GUID 作为参数执行程序时，返回该 COMB GUID 的 Timestamp。
    /// <summary>
    public static int Main(string[] args) {
        if(args.Length == 0) {
            Console.WriteLine(Provider.Sql.Create());
    		return (int)ExitCode.Success;
        }
        Guid g;
        if(!Guid.TryParse(args[0], out g)) {
        Console.WriteLine("Not a GUID.");
        return (int)ExitCode.NotAGuid;
        }
        Console.WriteLine(Provider.Sql.GetTimestamp(g));
        return (int)ExitCode.Success;
    }
}
```

### 2020年02月26日第一次碰头会议
#### 项目基本信息
```
沈阳广瑞科技有限公司 
税号：91210104079104020U
地址：沈阳市大东区滂江街86号409室
开户银行：华夏银行股份有限公司沈阳铁西支行
账号：11060000000228692
电话：13940014634

磋商保证金金额：25000.00人民币元
辽阳协成招标代理有限公司
保证金退还咨询电话：0419-3734422（联系人：高菲）

项目名称：《辽阳市不动产登记中心“不动产登记+金融”服务平台采购项目》
```
#### 参会人员
1. 沈阳广瑞： 龙飞、张宁益、葛磊
2. 不动产中心：王波
3. 银行：

#### 主要内容记录
1. 基本情况介绍--王波
2. 需求：
    1. 抵押业务，包括现房抵押等前置放到银行去。目前系统不适应需要
    2. 专线连接
    3. 身份认证、人脸识别、电子证照、图片优化
    4. 安全性：使用安全、业务安全
    5. 数据分析、风险预警、申请查封、轮候抵押

### 程序部署于IIS服务器上，日志记录警告信息的解决办法
当将程序部署于IIS服务器上时，会在日志中记录如下信息：
```
Using an in-memory repository. Keys will not be persisted to storage.
```
这是由于IIS服务器导致，具体原因为IIS服务器未将用户的数据保护密钥持久化保存。具体解释参见[这个网站](https://cypressnorth.com/programming/solved-using-memory-repository-keys-will-not-persisted-storage-asp-net-core-iis/)。解决办法为设置网站的应用程序池，将`加载用户配置文件`选项改为`True`，重新启动网站后问题解决。

### 生成RSA 非对称算法私钥与公钥
#### 生成私钥
```shell
openssl genrsa -out rsa_1024_priv.pem 1024
```
该命令将生成名为`rsa_1024_priv.pem`的私钥文件
#### 生成公钥
在上一步生成私钥的情况下，运行
```shell
openssl rsa -pubout -in rsa_1024_priv.pem -out rsa_1024_pub.pem
```
可以生成名为`rsa_1024_pub.pem`的公钥文件