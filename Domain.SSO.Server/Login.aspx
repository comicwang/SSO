<!DOCTYPE html>  
<html lang="en">
<head>
    <meta charset="UTF-8">  
    <title>统一认证平台</title>
    <script src="Scripts/jquery-3.5.1.min.js"></script>
    <link href="CSS/login.css" rel="stylesheet" />
    <script>
        $('#login').click(function () {
            $.ajax({
                url: '/login.ashx?userName=' + $("#userName").val() + "&password=" + $("#password").val(),
            }
            );
        })
    </script>
</head>
<body>
     <div>  
        <h1>统一认证平台</h1>  
        <form method="post">  
            <input type="text" required="required" placeholder="用户名" name="u" id="userName"/>  
            <input type="password" required="required" placeholder="密码" name="p" id="password"/>  
            <button class="but" type="submit" id="login">登录</button> 
         
        </form>  
    </div>  
</body>
</html>
