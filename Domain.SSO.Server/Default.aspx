<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Domain.SSO.Server.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="img/key.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SSO统一认证平台</title>
    <style>
        body {
            font-size: 14px;
            font-family: Verdana, sans-serif;
            background-color: #153e50;
            margin:0px;
        }
        .alert {
            padding: 15px;
            margin-bottom: 20px;
            border: 1px solid transparent;
            border-radius: 4px;
        }
        .alert-success {
            color: #3c763d;
            background-color: #dff0d8;
            border-color: #d6e9c6;
        }
        .content {
            background: #fff;
            padding: 20px;
            border-radius: 10px;
            font-size:20px;
        }
        h1{
            text-align:center;
            font-size:45px;
            color:#ffffff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>SSO统一认证平台</h1>
        <div style="background-color: #fff;box-sizing: border-box;border-radius:10px;">
            <div class="content">
            <div class="alert alert-success">
                <h2>登陆成功</h2>
                 <asp:Label runat="server" ID="lblMessage"></asp:Label>
                <p>您已成功登陆统一认证系统</p>
                <br />
                <p>出于安全考虑，一旦您访问过那些需要您提供凭证信息的应用时，请操作完成之后<a href="Logout.aspx">登出</a>并关闭浏览器。</p>
            </div>
           </div>
        </div>
    </form>
</body>
</html>
