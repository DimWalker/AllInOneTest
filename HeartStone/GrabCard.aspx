<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GrabCard.aspx.cs" Inherits="HeartStone.GrabCard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="Javascript/jquery-1.7.2.min.js"></script>

    <script type="text/javascript">
        String.prototype.endWith = function(s) {
            if (s == null || s == "" || this.length == 0 || s.length > this.length)
                return false;
            if (this.substring(this.length - s.length) == s)
                return true;
            else
                return false;
            return true;
        }
        String.prototype.startWith = function(s) {
            if (s == null || s == "" || this.length == 0 || s.length > this.length)
                return false;
            if (this.substr(0, s.length) == s)
                return true;
            else
                return false;
            return true;
        }
    </script>

    <script type="text/javascript">

        function loadPage() {
            //            $("#divFrame").load("http://db.duowan.com/lushi/card/list/eyJwIjoxLCJzb3J0IjoiaWQuZGVzYyJ9.html");

            $.ajax({
                url: "http://img.dwstatic.com/ls/pic/card/Unstable%20Ghoul.png",
                cache: false,
                success: function(html) {
                    $("#divFrame").append(html);
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    // 通常 textStatus 和 errorThrown 之中
                    // 只有一个会包含信息
                    alert(this); // 调用本次AJAX请求时传递的options参数
                }
            });

            $("#cardimg").attr("src", "http://img.dwstatic.com/ls/pic/card/Unstable%20Ghoul.png");
        }

        function Greb() {
            var xmldoc = new ActiveXObject("Microsoft.XMLDOM");
            //创建文件头
            var p = xmldoc.createProcessingInstruction("xml", "version='1.0' encoding='gb2312'");
            //添加文件头
            xmldoc.appendChild(p);
            //根节点
            var root = xmldoc.createElement("root");

            $(".table-s3").find("tbody > tr").each(function() {
                //名称 技能 描述 职业 类型 法力 攻击 生命

                //style
                //无 免费
                //color:#008000 普通
                //color:#3366ff 稀有
                //color:#c600ff 史诗
                //color:#FF6600 传说

                //data-src 图片
                var cols = $(this).find("td");
                cols[0].innerText;

                var cd = xmldoc.createElement("CardDetail");
                //基础属性
                var Name = xmldoc.createElement("Name");
                var Decription = xmldoc.createElement("Decription");
                var Cost = xmldoc.createElement("Cost");
                var Damage = xmldoc.createElement("Damage");
                var HP = xmldoc.createElement("HP");
                var CardType = xmldoc.createElement("CardType");
                var Occupation = xmldoc.createElement("Occupation");
                var Varity = xmldoc.createElement("Varity");
                var ImgSrc = xmldoc.createElement("ImgSrc");

                Name.text = cols[0].innerText;
                Decription.text = cols[2].innerText;
                Occupation.text = cols[3].innerText;
                CardType.text = cols[4].innerText;
                Cost.text = cols[5].innerText;
                Damage.text = cols[6].innerText;
                HP.text = cols[7].innerText;

                if (!$(cols[0]).children().attr("style") || $(cols[0]).children().attr("style") == "") {
                    Varity.text = "免费";
                } else {
                    switch ($(cols[0]).children().attr("style")) {
                        case "color: #008000":
                            Varity.text = "普通";
                            break;
                        case "color: #3366ff":
                            Varity.text = "稀有";
                            break;
                        case "color: #c600ff":
                            Varity.text = "史诗";
                            break;
                        case "color: #ff6600":
                            Varity.text = "传说";
                            break;
                        default:
                            break;
                    }
                }
                //Varity.text = $(cols[0]).children().attr("style");

                ImgSrc.text = $(cols[0]).children().attr("data-src");

                cd.appendChild(Name);
                cd.appendChild(Decription);
                cd.appendChild(Occupation);
                cd.appendChild(CardType);
                cd.appendChild(Cost);
                cd.appendChild(Damage);
                cd.appendChild(HP);
                cd.appendChild(Varity);
                cd.appendChild(ImgSrc);

                //技能
                if ($(cols[1]).children().length > 0) {
                    var pattern = /[（\(].+(?=[：:])/gi;
                    var SkillForm = xmldoc.createElement("SkillForm");
                    for (var i = 0; i < $(cols[1]).children().length; i++) {
                        var Skill = xmldoc.createElement("Skill");
                        if ($($(cols[1]).children()[i]).attr("data-tips").startWith("Freeze")) {
                            Skill.text = "冻结";
                        } else {
                            Skill.text = $($(cols[1]).children()[i]).attr("data-tips").match(pattern)[0].substring(1);
                        }

                        SkillForm.appendChild(Skill);
                    }
                    cd.appendChild(SkillForm);
                }
                root.appendChild(cd);




            });
            xmldoc.appendChild(root);
            //alert(xmldoc.xml.substr(1, 1000));

            $.ajax({
                type: "post",
                url: "HeartStone.ashx",
                data: { xmldata: xmldoc.xml },
                success: function(result) {
                    alert(result);
                }
            });
        }    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input type="button" id="loadFrame" name="loadFrame" onclick="Greb();" value="loadFrame" />
    <div id="divFrame" runat="server" style="border-style: solid; border-width: 3px;
        border-color: Black; height: 800px; width: auto;">
    </div>
    </form>
</body>
</html>
