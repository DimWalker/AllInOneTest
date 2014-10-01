<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GrabCard.aspx.cs" Inherits="HeartStone.GrabCard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="Javascript/jquery-1.7.2.min.js"></script>

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

            $("#cardimg").attr("src","http://img.dwstatic.com/ls/pic/card/Unstable%20Ghoul.png");
        }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input type="button" id="loadFrame" name="loadFrame" onclick="loadPage();" value="loadFrame" />
    <div id="divFrame" style="border-style: solid; border-width: 3px; border-color: Black;
        height: 100px; width:100px;">
    </div>
    <img id="cardimg" width="100" height="100"/>
    
    <iframe src="http://db.duowan.com/lushi/card/list/eyJwIjoxLCJzb3J0IjoiaWQuZGVzYyJ9.html" height="800" width="100%"></iframe>
    </form>
</body>
</html>
