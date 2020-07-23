## 作者联系方式
我们的discord频道是
https://discord.gg/dCDEPXx
如果有问题可以随时来

原作Github页面: https://baaron4.github.io/GW2-Elite-Insights-Parser/ 

# 激战2 Elite-Insights-Parser
## 下载

1. 点击 Code 栏

2. 找到 Release 分支

3. 下载 GW2EI.zip

4. 解压

5. 打开 GW2EI.exe (可自行添加桌面快捷方式)

6. OJBK

注意: ArcDPS 的日志文件目前默认存放于 "C:\Users\<USERNAME>\Documents\Guild Wars 2\addons\arcdps\arcdps.cbtlogs"
## 使用
![program](https://user-images.githubusercontent.com/30677999/38950127-284f2d10-430a-11e8-937b-67a325a2a296.PNG)

1. 拖拽1个或多个 .evtc, .evtc.zip 或 .zevtc 文件至对话框中

2. 点击解析

3. 解析完成后生成的HTML文件会在你的evtc目录下, 也可以指定存放目录。 默认的文件名差不多是 "samename_boss_result.html" 这样。

![htmldisplay](https://user-images.githubusercontent.com/30677999/38950250-816c559e-430a-11e8-8159-1cf073a5fa44.PNG)

## 控制台解析

https://github.com/baaron4/GW2-Elite-Insights-Parser/blob/24df62abfec74446a07816524a98b9d97d87d966/LuckParser/Program.cs#L15-L22

![how to](https://user-images.githubusercontent.com/30677999/40148954-6ec9215a-5936-11e8-94ad-d2520e7c4539.PNG)

以防万一还是要说一下，如果你抛弃了GUI想使用控制台解析日志文件的话 

可以通过.conf 文件来进行设置 (具体可以查看 Settings/sample.conf). 之后你可以使用 -c 来控制解析:

```
GuildWars2EliteInsights.exe -c [config path] [logs]
```

你还可以使用 -p:

```
GuildWars2EliteInsights.exe -p [logs]
```

你可以使用 -ui 选项呼出GUI界面:
```
GuildWars2EliteInsights.exe -c [config path] -ui [logs]
```

请注意解析并不是瞬间完成的。

## 设置

鉴于能跑到这里来看这个程序的各位应该都有相应的英文能力和打码水平，这里就不翻译了。

## JSON 文件

你可以在 [here] 这里找到JSON文件 (https://baaron4.github.io/GW2-Elite-Insights-Parser/Json/index.html)

## 贡献者
### 开发人员
- baaron4
- EliphasNUIT
- cordbleibaum
- QuiCM
- amgine
- Linus
- Sejsel
- Flomix
- Stonos
- Hobinjk

### 其他成员
- Linus (arena maps/ icons for combat replay)


