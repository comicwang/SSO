<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Domain.SSO.Server.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" lang="en" runat="server">
    <link rel="shortcut icon" href="img/key.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>统一认证平台</title>
        <style type="text/css">
        * {
            box-sizing: border-box;
        }
            body {
                font-size: 15px;
                font-family: Verdana, sans-serif;
               
                margin: 0px;
            }
            .title{
                 opacity: 0.8;
               
                font-size:50px;text-align:center;
            }
            .wrap {
                width: 100%;
                height: 400px;
                padding: 40px 0;
                position: fixed;
                top: 50%;
                margin-top: -200px;
                opacity: 0.8;
                background: linear-gradient(to bottom right,#50a3a2,#53e3a6);
                background: -webkit-linear-gradient(to bottom right,#50a3a2,#53e3a6);
                border-radius: 10px;
            }

            .container {
                width: 60%;
                margin: 0 auto;
            }
        .container h1 {
            text-align: center;
            color: #FFFFFF;
            font-weight: 500;
        }
        .container input {
            width: 400px;
            display: block;
            height: 45px;
            border: 0;
            outline: 0;
            padding: 6px 10px;
            line-height: 24px;
            margin: 32px auto;
            -webkit-transition: all 0s ease-in 0.1ms;
            -moz-transition: all 0s ease-in 0.1ms;
            transition: all 0s ease-in 0.1ms;
            border-radius:5px;
        }
        .container input[type="text"] , .container input[type="password"]  {
            background-color: #FFFFFF;
            font-size: 16px;
            color: #50a3a2;
        }
        .container #btnLogin {
            font-size: 16px;
            letter-spacing: 2px;
            color: #666666;
            background-color: #FFFFFF;
        }
        .container input:focus {
           /* width: 400px;*/
        }
        .container #btnLogin:hover {
            cursor: pointer;
            background-color:#153e50;
             /* width: 400px;*/
             color:#fff
        }

            .wrap ul {
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                z-index: -10;
            }
                .wrap ul li {
                    list-style-type: none;
                    display: block;
                    position: absolute;
                    bottom: -120px;
                    width: 15px;
                    height: 15px;
                    z-index: -8;
                    background-color: rgba(255, 255, 255, 0.15);
                    animotion: square 25s infinite;
                    -webkit-animation: square 25s infinite;
                }
        .wrap ul li:nth-child(1) {
            left: 0;
            animation-duration: 10s;
            -moz-animation-duration: 10s;
            -o-animation-duration: 10s;
            -webkit-animation-duration: 10s;
        }
        .wrap ul li:nth-child(2) {
            width: 40px;
            height: 40px;
            left: 10%;
            animation-duration: 15s;
            -moz-animation-duration: 15s;
            -o-animation-duration: 15s;
            -webkit-animation-duration: 15s;
        }
        .wrap ul li:nth-child(3) {
            left: 20%;
            width: 25px;
            height: 25px;
            animation-duration: 12s;
            -moz-animation-duration: 12s;
            -o-animation-duration: 12s;
            -webkit-animation-duration: 12s;
        }
        .wrap ul li:nth-child(4) {
            width: 50px;
            height: 50px;
            left: 30%;
            -webkit-animation-delay: 3s;
            -moz-animation-delay: 3s;
            -o-animation-delay: 3s;
            animation-delay: 3s;
            animation-duration: 12s;
            -moz-animation-duration: 12s;
            -o-animation-duration: 12s;
            -webkit-animation-duration: 12s;
        }
        .wrap ul li:nth-child(5) {
            width: 60px;
            height: 60px;
            left: 40%;
            animation-duration: 10s;
            -moz-animation-duration: 10s;
            -o-animation-duration: 10s;
            -webkit-animation-duration: 10s;
        }
        .wrap ul li:nth-child(6) {
            width: 75px;
            height: 75px;
            left: 50%;
            -webkit-animation-delay: 7s;
            -moz-animation-delay: 7s;
            -o-animation-delay: 7s;
            animation-delay: 7s;
        }
        .wrap ul li:nth-child(7) {
            left: 60%;
            animation-duration: 8s;
            -moz-animation-duration: 8s;
            -o-animation-duration: 8s;
            -webkit-animation-duration: 8s;
        }
        .wrap ul li:nth-child(8) {
            width: 90px;
            height: 90px;
            left: 70%;
            -webkit-animation-delay: 4s;
            -moz-animation-delay: 4s;
            -o-animation-delay: 4s;
            animation-delay: 4s;
        }
        .wrap ul li:nth-child(9) {
            width: 100px;
            height: 100px;
            left: 80%;
            animation-duration: 20s;
            -moz-animation-duration: 20s;
            -o-animation-duration: 20s;
            -webkit-animation-duration: 20s;
        }
        .wrap ul li:nth-child(10) {
            width: 120px;
            height: 120px;
            left: 90%;
            -webkit-animation-delay: 6s;
            -moz-animation-delay: 6s;
            -o-animation-delay: 6s;
            animation-delay: 6s;
            animation-duration: 30s;
            -moz-animation-duration: 30s;
            -o-animation-duration: 30s;
            -webkit-animation-duration: 30s;
        }

        @keyframes square {
            0%  {
                    -webkit-transform: translateY(0);
                    transform: translateY(0)
                }
            100% {
                    bottom: 400px;
                    transform: rotate(600deg);
                    -webit-transform: rotate(600deg);
                    -webkit-transform: translateY(-500);
                    transform: translateY(-500)
            }
        }
        @-webkit-keyframes square {
            0%  {
                -webkit-transform: translateY(0);
                transform: translateY(0)
            }
            100% {
                bottom: 400px;
                transform: rotate(600deg);
                -webit-transform: rotate(600deg);
                -webkit-transform: translateY(-500);
                transform: translateY(-500)
            }
        }
            #lblMessage {
                font-size: 16px;
            }
            .alert {
                width: 400px;
                display: block;
                height: 35px;
                border: 0;
                outline: 0;
                padding: 6px 10px;
                line-height: 24px;
                margin: 32px auto;
                border: 1px solid transparent;
                border-radius: 4px;
               display:none;
            }

            .alert-danger {
                color: #a94442;
                background-color: #f2dede;
                border-color: #ebccd1;
            }
        </style>
</head>
<body>
    <h1 class="title">SSO统一认证平台</h1>
   
           <%-- <h1>用户登陆</h1>--%>
    <form id="form1" runat="server">
         <div class="wrap">
        <div class="container">

        <asp:TextBox runat="server" ID="txtUserName"  placeholder="用户名"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtPassword" placeholder="密码" TextMode="Password"></asp:TextBox>
        <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" CssClass="login" Text="登录" />

        <p style="text-align:center;color:#ff0000">
           <asp:Label ID="lblMessage"  Text="" runat="server" />
        </p>
                        </div>
        </div>
        <div style="position:absolute;bottom:10px;text-align:center;width:100%">
         
        <p>出于安全考虑，一旦您访问过那些需要您提供凭证信息的应用时，请操作完成之后<a href="Logout.aspx">登出</a>并关闭浏览器。</p>
            
            </div>
    </form>

</body>
</html>
